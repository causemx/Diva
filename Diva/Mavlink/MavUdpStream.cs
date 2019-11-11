using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Diva.Mavlink
{
    class MavUdpStream : MavStream, IDisposable
    {
        internal MavUdpStream(DroneSetting setting) : base(setting)
        {
            int.TryParse(setting.PortNumber, out port);
            ReadTimeout = 500;
        }
        ~MavUdpStream() { Dispose(false);  }
        // IDisposable override
        protected override void Dispose(bool disposing)
        {
            if (!disposed && disposing) Close();
        }

        // UDP specific
        private UdpClient client;
        private class ReadBufferStruct
        {
            private byte[] buffer;
            private int dataBegin;
            public int Length => buffer.Length - dataBegin;

            public void Reset() { buffer = new byte[0]; dataBegin = 0; }
            public void Set(MemoryStream stream)
            {
                buffer = stream.ToArray();
                dataBegin = 0;
            }
            public int Get(byte[] dst, int offset, int len)
            {
                int size = Length;
                if (size > 0)
                {
                    if (size > len)
                        size = len;
                    Array.Copy(buffer, dataBegin, dst, offset, size);
                    dataBegin += size;
                }
                return size;
            }
        };
        private ReadBufferStruct readBuffer = new ReadBufferStruct();
        private readonly int port;
        private IPEndPoint Remote;

        // common overrides
        public override string StreamDescription => "UDP" + port;
        public override int BytesAvailable { get => readBuffer.Length + client.Available; }
        protected override bool StreamOpened => client?.Client != null;

        public override void Open()
        {
            if (IsOpen)
            {
                log.Info("udpserial socket already open");
                return;
            }
            Close();
            client = new UdpClient(port);
            try
            {
                client.Client.ReceiveTimeout = ReadTimeout;
                client.Receive(ref Remote);
                Console.WriteLine($"MavUDPStream connecting to {Remote.Address} : {Remote.Port}");
                IsOpen = true;
                readBuffer.Reset();
            } catch (Exception ex)
            {
                Close();
                throw new Exception("MavUDPStream is closed " + ex);
            }
        }

        public override void Close()
        {
            IsOpen = false;
            try { client?.Close(); } catch { }
            client = null;
        }

        public override int Read(byte[] buffer, int offset, int length)
        {
            if (!IsOpen || length < 1) return 0;

            int copied = readBuffer.Get(buffer, offset, length);
            if (copied == length) return length;
            offset += copied;
            length -= copied;

            using (MemoryStream ms = new MemoryStream())
            {
                DateTime timeout = DateTime.Now.AddMilliseconds(ReadTimeout);
                do
                {
                    Byte[] bytes = client.Receive(ref Remote);
                    ms.Write(bytes, 0, bytes.Length);
                } while (client.Available > 0 &&
                    ms.Length < ReadBufferSize &&
                    DateTime.Now < timeout);
                readBuffer.Set(ms);
            }

            return copied + readBuffer.Get(buffer, offset, length);
        }

        public override void Write(byte[] buffer, int offset, int length)
        {
            if (IsOpen && length > 0)
                try { client.Send(buffer, length, Remote); } catch { }
        }
    }
}
