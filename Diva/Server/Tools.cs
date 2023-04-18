using GMap.NET;
using System;
using WebSocketSharp;

namespace Diva.Server
{
    public class Tools
    {

        public class ExtendMessageEventArgs : EventArgs
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
}
