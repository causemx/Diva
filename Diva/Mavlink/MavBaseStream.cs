using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Mavlink
{
    public abstract class MavBaseStream : IDisposable
    {
        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected MavBaseStream(DroneSetting setting) { }

        public static MavBaseStream CreateStream(DroneSetting setting)
        {
            return (setting.PortName.ToLower() == "udp") ?
                new MavUdpStream(setting) as MavBaseStream :
                new MavSerialStream(setting) as MavBaseStream;
        }

        // stream status property
        private bool opened = false;
        protected virtual bool streamOpened => false;
        public bool IsOpen { get => opened && streamOpened; protected set => opened = value; }
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
