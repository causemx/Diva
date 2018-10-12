using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Diva.Comms
{
	public class UdpSerial : ICommsSerial, IDisposable
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public UdpClient client = new UdpClient();
		public IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
		byte[] rbuffer = new byte[0];
		int rbufferread = 0;

		public UdpSerial(string portnumber)
		{
            int.TryParse(portnumber, out port);
			ReadTimeout = 5000;
		}

        private int port;
        private int recvTimeout;
		public int ReadTimeout
        {
            get => recvTimeout;
            set => recvTimeout = value;
        }
		public int ReadBufferSize { get; set; }
		public int BaudRate { get; set; }
		public Parity Parity { get; set; }

		public string PortName { get; set; }

		public int BytesToRead
		{
			get { return client.Available + rbuffer.Length - rbufferread; }
		}

		private bool _isopen = false;
		public bool IsOpen
        {
            get => _isopen && client.Client != null;
            set => _isopen = value;
        }

		public bool CancelConnect = false;

		public void Open()
		{
			if (client.Client.Connected)
			{
				log.Info("udpserial socket already open");
				return;
			}

			try
			{
				if (client != null)
				{
					client.Close();
				}
			}
			catch { }

			client = new UdpClient(port);
			client.Client.ReceiveTimeout = ReadTimeout;

			try
			{
				client.Receive(ref RemoteIpEndPoint);
				Console.WriteLine("UDPSerial connecting to {0} : {1}", RemoteIpEndPoint.Address, RemoteIpEndPoint.Port);
				_isopen = true;
				
			}
			catch (Exception ex)
			{
				if (client != null && client.Client.Connected)
				{
					client.Close();
				}
				throw new Exception("The socket/UDPSerial is closed " + ex);
			}
		}

        public void DiscardInBuffer()
        {
            VerifyConnected();
            int size = client.Available;
            byte[] crap = new byte[size];
            log.InfoFormat("UdpSerial DiscardInBuffer {0}", size);
            Read(crap, 0, size);
        }

        void VerifyConnected()
		{
			if (client == null || !IsOpen)
			{
				Close();
				throw new Exception("The socket/serialproxy is closed");
			}
		}

		public int Read(byte[] readto, int offset, int length)
		{
			VerifyConnected();
			try
			{
				if (length < 1) { return 0; }

				// check if we are at the end of our current allocation
				if (rbufferread == rbuffer.Length)
				{
					DateTime deadline = DateTime.Now.AddMilliseconds(this.ReadTimeout);

					MemoryStream r = new MemoryStream();
					do
					{
						// read more
						while (client.Available > 0 && r.Length < (1024 * 1024))
						{
							Byte[] b = client.Receive(ref RemoteIpEndPoint);
							r.Write(b, 0, b.Length);
						}
						// copy mem stream to byte array.
						rbuffer = r.ToArray();
						// reset head.
						rbufferread = 0;
					} while (rbuffer.Length < length && DateTime.Now < deadline);
				}

				// prevent read past end of array
				if ((rbuffer.Length - rbufferread) < length)
				{
					length = (rbuffer.Length - rbufferread);
				}

				Array.Copy(rbuffer, rbufferread, readto, offset, length);

				rbufferread += length;

				return length;
			}
			catch { throw; }
		}

		public void Write(byte[] write, int offset, int length)
		{
			VerifyConnected();
			try
			{
				client.Send(write, length, RemoteIpEndPoint);
			}
			catch { }//throw new Exception("Comport / Socket Closed"); }
		}

		public void Close()
		{
			_isopen = false;
			if (client != null)
			{
				client.Close();
			}

			client = new UdpClient();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources
				Close();
				client = null;
			}
			// free native resources
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
