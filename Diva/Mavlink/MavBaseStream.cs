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
        protected object streamObject = null;
        private bool opened = false;
        public bool IsOpen { get => opened && streamObject != null; protected set => opened = value; }
        public int ReadTimeout { get; set; }

        // general property access
        private PropertyInfo GetProperty(string prop) => this.GetType().GetProperty(prop);
        public bool SetPropertyValue(string prop, object value)
        {
            bool ret = false;
            try
            {
                var setter = GetProperty(prop).GetSetMethod();
                setter.Invoke(this, new [] { value });
                ret = true;
            } catch { }
            return ret;
        }
        public bool GetPropertyValue(string prop, out object value)
        {
            bool ret = false;
            try
            {
                var setter = GetProperty(prop).GetGetMethod();
                value = setter.Invoke(this, null);
                ret = true;
            }
            catch { value = null; }
            return ret;
        }
        public int ReadBufferSize { get; set; }

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
