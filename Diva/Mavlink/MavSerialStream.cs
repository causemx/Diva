using System;
using System.IO.Ports;

namespace Diva.Mavlink
{
    class MavSerialStream : MavStream, IDisposable
    {
        internal MavSerialStream(DroneSetting setting) : base(setting)
        {
            portname = setting.PortName;
            int.TryParse(setting.Baudrate, out baudrate);
            DataBits = serialPort.DataBits;
            Parity = serialPort.Parity;
            StopBits = serialPort.StopBits;
        }
        ~MavSerialStream() { Dispose(false); }
        // IDisposable override
        protected override void Dispose(bool disposing)
        {
            if (!disposed && disposing) Close();
        }

        // Serial port specific
        private SerialPort serialPort = new SerialPort();
        public const int SERIALPORT_DEFAULT_OPEN_DELAY = 1000;
        public int OpenDelay = SERIALPORT_DEFAULT_OPEN_DELAY;
        private int baudrate, databits;
        public int BaurdRate {
            get => baudrate;
            set => serialPort.BaudRate = baudrate = value;
        }
        public int DataBits
        {
            get => databits;
            set => serialPort.DataBits = databits = value;
        }
        private StopBits stopbits;
        public StopBits StopBits
        {
            get => stopbits;
            set => serialPort.StopBits = stopbits = value;
        }
        private Parity parity;
        public Parity Parity
        {
            get => parity;
            set => serialPort.Parity = parity = value;
        }
        private string portname;

        // common method overrides
        public new int ReadBufferSize
        {
            get => base.ReadBufferSize;
            set
            {
                base.ReadBufferSize = value;
                if (serialPort != null)
                    serialPort.ReadBufferSize = value;
            }
        }
        public new int ReadTimeout
        {
            get => base.ReadTimeout;
            set
            {
                base.ReadTimeout = value;
                if (serialPort != null)
                    serialPort.ReadTimeout = value;
            }
        }
        public override string StreamDescription => portname + "@" + baudrate;
        public override int BytesAvailable { get => serialPort.BytesToRead; }
        protected override bool streamOpened => serialPort.IsOpen;

        public override void Open()
        {
            if (IsOpen) return;
            Close();
            try
            {
                serialPort = new SerialPort(portname, BaurdRate, Parity, DataBits);
                serialPort.ReadTimeout = ReadTimeout;
                serialPort.ReadBufferSize = ReadBufferSize;
                serialPort.Open();
                System.Threading.Thread.Sleep(OpenDelay);
                serialPort.DiscardInBuffer();
                IsOpen = true;
            } catch
            {
                log.Info("Open port " + portname + " failed.");
                Close();
            }
        }

        public override void Close()
        {
            IsOpen = false;
            if (serialPort != null)
            {
                log.Info("Closing port " + serialPort.PortName);
                try { serialPort.Close(); } catch { }
                serialPort = new SerialPort();
            }
        }

        public override int Read(byte[] buffer, int offset, int length) => serialPort.Read(buffer, offset, length);

        public override void Write(byte[] buffer, int offset, int length) => serialPort.Write(buffer, offset, length);
    }
}
