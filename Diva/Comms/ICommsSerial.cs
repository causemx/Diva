using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Comms
{
	public interface ICommsSerial
	{
		// from serialport class
		// Methods
		void Close();
		void DiscardInBuffer();
		void Open();
		int Read(byte[] buffer, int offset, int count);
		void Write(byte[] buffer, int offset, int count);

		// Properties
		int BaudRate { get; set; }
		int BytesToRead { get; }
		bool IsOpen { get; }
		Parity Parity { get; set; }
		string PortName { get; set; }
		int ReadBufferSize { get; set; }
		int ReadTimeout { get; set; }
	}
}
