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
            /*bool isUdp = setting.PortName.ToLower() == "udp";
            MavBaseStream stream;
            if (isUdp)
                stream = new MavUdpStream(setting);
            else
                stream = new MavSerialStream(setting);
            return stream;*/
            return (setting.PortName.ToLower() == "udp") ?
                new MavUdpStream(setting) as MavBaseStream :
                new MavSerialStream(setting) as MavBaseStream;
        }

        // stream status property
        private bool opened = false;
        protected virtual bool? streamOpened { get => false; }
        public bool IsOpen { get => opened && (streamOpened ?? false); protected set => opened = value; }
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
