using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FooApplication.Utilities
{
	public class Demux
	{
		public static byte[] readPacket(byte[] buf, int len, ref int sysId, ref int msgId)
		{
			byte[] ret = null;
			if (buf[0] == 0xfe && len > 8)
			{
				sysId = buf[3];
				msgId = buf[5];
				int pktSize = 6 + 2 + buf[1];
				if (len >= pktSize)
				{
					ret = new byte[pktSize];
					Array.Copy(buf, ret, pktSize);
				}
			}
			else if (buf[0] == 0xfd && len > 12) //mavlink 2.0, no sig
			{
				sysId = buf[5];
				msgId = (buf[9] << 16) + (buf[8] << 8) + buf[7];
				int pktSize = 10 + 2 + buf[1];
				if (len >= pktSize)
				{
					ret = new byte[pktSize];
					Array.Copy(buf, ret, pktSize);
				}
			}
			return ret;
		}

		public Socket fromServer = null, toGCS = null;

		public void Active()
		{
			IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 14550);
			EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
			fromServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			Dictionary<int, IPEndPoint> vehicles = new Dictionary<int, IPEndPoint>();
			//fromServer.Blocking = false;
			fromServer.Bind(localEP);
			//fromServer.ReceiveTimeout = 1;
			toGCS = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			IPEndPoint localEP2 = new IPEndPoint(IPAddress.Any, 18888);
			toGCS.Bind(localEP2);
			//toGCS.Blocking = false;
			//toGCS.ReceiveTimeout = 1;
			byte[] buf = new byte[1024];
			int curPort = 16000;
			int sysId = 0;
			int msgId = 0;
			while (true)
			{
				List<Socket> readList = new List<Socket>() { fromServer, toGCS };
				Socket.Select(readList, null, null, -1);
				int len = 0;
				if (readList.Contains(fromServer))
				{
					len = fromServer.ReceiveFrom(buf, ref remoteEP);
				}
				if (len > 0)
				{
					//Console.WriteLine("recv " + len + " bytes, start with " + buf[0]);
					int remains = len;
					while (remains > 0)
					{
						byte[] pkt = readPacket(buf, remains, ref sysId, ref msgId);
						if (pkt == null) break;
						remains -= pkt.Length;
						//Console.WriteLine("from sys " + sysId);
						IPEndPoint ep;
						if (vehicles.ContainsKey(sysId))
						{
							ep = vehicles[sysId];
							toGCS.SendTo(buf, 0, len, SocketFlags.None, ep);
							//Console.WriteLine("to " + ep.Port);
						}
						else if (msgId == 0) //heartbeat
						{
							ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), curPort);
							vehicles.Add(sysId, ep);
							toGCS.SendTo(buf, 0, len, SocketFlags.None, ep);

							// TODO: add content into rich textbox.
							Console.WriteLine("new mav " + sysId + " to port " + ep.Port);
							curPort++;
						}
					}
				}
				len = 0;
				if (readList.Contains(toGCS))
				{
					len = toGCS.Receive(buf);
				}
				if (len > 0)
				{
					//Console.WriteLine("recv " + len + " bytes from GCS");
					fromServer.SendTo(buf, len, SocketFlags.None, remoteEP);
				}
			}
		}

		public void Deactive()
		{
			fromServer.Close();
			toGCS.Close();
		}

	}
}
