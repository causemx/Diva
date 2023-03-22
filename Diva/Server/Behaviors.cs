using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;
using System.Drawing;

namespace Diva.Server
{
    internal class Behaviors
    {
        public class Echo : WebSocketBehavior
        {
            public object ClassName
            {
                get => GetType().Name;
            }

            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.RawData));
                Send(e.Data);
            }
        }
    }
}
