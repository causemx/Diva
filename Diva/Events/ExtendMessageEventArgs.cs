using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Diva.Events
{
    public class ExtendMessageEventArgs
    {
        public MessageEventArgs messageEventArgs;

        private PointLatLng[] geoData;

        public PointLatLng[] GeoData
        {
            get => geoData;
            set
            {
                geoData = value;
            }
        }

        public string GetData()
        {
            return messageEventArgs.Data;
        }

        public byte[] GetRawData()
        {
            return messageEventArgs.RawData;
        }

        public ExtendMessageEventArgs(MessageEventArgs _messageEventArgs)
        {
            messageEventArgs = _messageEventArgs;
        }
    }
}
