using log4net;
using System;
using System.IO;

namespace Diva.Comms
{
	public class SerialPort : System.IO.Ports.SerialPort, ICommsSerial
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(SerialPort));

		static object locker = new object();

		public new void Open()
		{
			if (IsOpen)
				return;

            if (BaudRate > 115200)
            {
                // ref: mission planner's SerialPortFixer class if problem occurs
            }

			if (PortName.StartsWith("/"))
				if (!File.Exists(PortName))
					throw new Exception("No such device");

			try
			{
				base.Open();
			}
			catch
			{
				try { Close(); }
				catch { }
				throw;
			}
		}

		public new void Close()
		{
			log.Info("Closing port " + PortName);
			base.Close();
		}
	}
}
