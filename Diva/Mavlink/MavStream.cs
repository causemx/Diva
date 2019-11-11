using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Mavlink
{
    public abstract class MavStream : IDisposable
    {
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected MavStream(DroneSetting setting) { }

        public static MavStream CreateStream(DroneSetting setting)
        {
            return (setting.PortName.ToLower() == "udp") ?
                new MavUdpStream(setting) as MavStream :
                new MavSerialStream(setting) as MavStream;
        }

        // stream status property
        private bool opened = false;
        protected virtual bool StreamOpened => false;
        public bool IsOpen { get => opened && StreamOpened; protected set => opened = value; }
        public int ReadTimeout { get; set; }

        // general property access
        public int ReadBufferSize { get; set; }
        public abstract string StreamDescription { get; }
        public abstract int BytesAvailable { get; }

        // common methods
        public abstract void Open();
        public abstract void Close();
        public abstract int Read(byte[] buffer, int offset, int length);
        public abstract void Write(byte[] buffer, int offset, int length);

        // for derived class, base stream has nothing to dispose
        protected bool disposed = false;
        protected virtual void Dispose(bool disposing) { }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
