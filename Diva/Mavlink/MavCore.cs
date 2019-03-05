using Diva.Controls;
using Diva.Properties;
using Diva.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using static MAVLink;

namespace Diva.Mavlink
{
	public class MavCore<StatusType> where StatusType : MavStatus
	{
        #region Constants
        public const double CONNECT_TIMEOUT_SECONDS = 30;
        public const int GET_PARAM_TIMEOUT = 1000;
        private const int GROUNDCONTROLSTATION_SYSTEM_ID = 255;
        #endregion Constants

        private MavStream baseStream;
        public MavStream BaseStream
        {
            get => baseStream;
            set
            {
                baseStream?.Dispose();
                baseStream = value;
            }
        }

        public StatusType Status = Activator.CreateInstance<StatusType>();

        public byte SysId { get; private set; }
        public byte CompId { get; private set; }

        public BufferedStream LogFile { get; set; }
		public BufferedStream RawLogFile { get; set; }

		internal string plaintxtline = "";

		protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private readonly object writeLock = new object();
		private int pacCount = 0;
		private string plainTextLine = "";
		private byte mavlinkVersion = 0;
		private bool GPSLocationMode = false;
		private ProgressDialogV2 frmProgressReporter;


        #region Core
        public MavCore()
		{
            InitializeMavLinkMessageHandler();
        }

        public void Close()
		{
            //bgReaderLoop.Cancel();
            //bgReaderLoop = null;
            lock (mavs) mavs.Remove(this);
            try { BaseStream.Close(); } catch { }
        }

		public void Open()
		{
			frmProgressReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				HintImage = Resources.icon_warn,
				Text = Strings.TextFormProgressConnection,
			};

			frmProgressReporter.DoWork += OpenBg;
			frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgFormProgressSync);
			frmProgressReporter.RunBackgroundOperationAsync();
			frmProgressReporter.Dispose();
        }

		public void OpenBg(object PRsender, ProgressWorkerEventArgs progressWorkerEventArgs, object param = null)
		{
			frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgFormProgressMavlinkConnecting);

			try
			{
				BaseStream.ReadBufferSize = 16 * 1024;

				lock (writeLock) // so we dont have random traffic
				{
					log.Info("Open port with " + BaseStream.StreamDescription);

					progressWorkerEventArgs.CancelRequestChanged += (o, e) =>
							((ProgressWorkerEventArgs)o).CancelAcknowledged = true;

					BaseStream.Open();
				}

                lock (mavs) mavs.Add(this);
                //if (bgReaderLoop == null)
                //    bgReaderLoop = BackgroundLoop.Start(SerialReader);

                MAVLinkMessage buffer = MAVLinkMessage.Invalid;
				MAVLinkMessage buffer1 = MAVLinkMessage.Invalid;

				DateTime start = DateTime.Now;
				DateTime deadline = start.AddSeconds(CONNECT_TIMEOUT_SECONDS);

				// CountDown timer for connecting.
				var countDown = new Timer { Interval = 1000, AutoReset = false };
				countDown.Elapsed += (sender, e) =>
				{
					int secondsRemaining = (deadline - e.SignalTime).Seconds;
					frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgFormProgressTrying.FormatWith(secondsRemaining));
					if (secondsRemaining > 0) countDown.Start();
				};
				countDown.Start();

				while (true)
				{
					if (progressWorkerEventArgs.CancelRequested)
					{
						progressWorkerEventArgs.CancelAcknowledged = true;
						countDown.Stop();
						if (BaseStream.IsOpen)
							BaseStream.Close();
						return;
					}
					log.Info(DateTime.Now.Millisecond + " Start connect loop ");
					if (DateTime.Now > deadline)
					{
						countDown.Stop();
                        if (BaseStream.IsOpen)
                            BaseStream.Close();
						progressWorkerEventArgs.ErrorMessage = Strings.MsgNoHeartBeat;
						throw new Exception("Can not establish a connection\n");
					}
					Thread.Sleep(1);

                    frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgWaitHeartBeat);
                    // since we're having only 1 sysid/compid in this link, skip id checks
                    if (buffer.Length == 0)
						buffer = GetHeartBeat(true);
                    if (buffer.Length > 0)
                        break;
					// skip 2 hbs match test
				}
				countDown.Stop();

				byte[] temp = ASCIIEncoding.ASCII.GetBytes("Diva GCS " + Assembly.GetEntryAssembly().GetName().Version + "\0");
				Array.Resize(ref temp, 50);

				SendPacket(MAVLINK_MSG_ID.STATUSTEXT,
					new mavlink_statustext_t { severity = (byte)MAV_SEVERITY.INFO, text = temp });

				GetHeartBeat();

                frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgAskFlightControllerVersion);
                GetVersion();

				frmProgressReporter.UpdateProgressAndStatus(0,
					Strings.MsgGettingParams.FormatWith(new object[] { SysId, CompId }));
				GetParamListBG();

				if (frmProgressReporter.doWorkArgs.CancelAcknowledged == true)
				{
					if (BaseStream.IsOpen)
						BaseStream.Close();
					return;
				}
			}
			catch (Exception e)
			{
				try
				{
					BaseStream.Close();
				}
				catch {	}
				if (string.IsNullOrEmpty(progressWorkerEventArgs.ErrorMessage))
					progressWorkerEventArgs.ErrorMessage = Strings.MsgConnectionFailed;
				log.Error(e);
				throw e;
			}
			//frmProgressReporter.Close();
			frmProgressReporter.UpdateProgressAndStatus(100, Strings.MsgFormProgressDone);
			log.Info($"Done open {SysId} {CompId}");
			Status.PacketsLost = 0;
			Status.SyncLost = 0;
		}

		public MAVLinkMessage ReadPacket()
		{
			byte[] buffer = new byte[MAVLINK_MAX_PACKET_LEN + 25];
			int count = 0;
			int length = 0;
			int readcount = 0;
			MAVLinkMessage message = null;

			DateTime start = DateTime.Now;
			while (BaseStream.IsOpen)
			{
				try
				{
					if (readcount > 300)
					{
						break;
					}
					readcount++;

					// time updated for internal reference
					Status.PacketTime = DateTime.Now;
                    // 1200 ms between chars - the gps detection requires this.
                    DateTime to = DateTime.Now.AddMilliseconds(1200);

					while (BaseStream.IsOpen && BaseStream.BytesAvailable <= 0)
					{
						if (DateTime.Now > to)
						{
							log.InfoFormat("MAVLINK: 1 wait time out btr {0} len {1}", BaseStream.BytesAvailable,
								length);
							throw new TimeoutException("Timeout");
						}
						Thread.Sleep(1);
					}
					if (BaseStream.IsOpen)
					{
						BaseStream.Read(buffer, count, 1);
						if (RawLogFile != null && RawLogFile.CanWrite)
							RawLogFile.WriteByte(buffer[count]);
					}
				}
				catch (Exception e)
				{
					log.Info("MAVLink readpacket read error: " + e.ToString());
					break;
				}

				// check if looks like a mavlink packet and check for exclusions and write to console
				if (buffer[0] != 0xfe && buffer[0] != 'U' && buffer[0] != 0xfd)
				{
					if (buffer[0] >= 0x20 && buffer[0] <= 127 || buffer[0] == '\n' || buffer[0] == '\r')
					{
						// check for line termination
						if (buffer[0] == '\r' || buffer[0] == '\n')
						{
							// check new line is valid
							if (plainTextLine.Length > 3)
								plaintxtline = plainTextLine;

							log.Info(plaintxtline);
							// reset for next line
							plainTextLine = "";
						}
						
						plainTextLine += (char)buffer[0];
					}
					count = 0;
					buffer[1] = 0;
					continue;
				}
				readcount = 0;

                if (buffer[0] == 0xfe || buffer[0] == 0xfd || buffer[0] == 'U')
				{
					var mavlinkv2 = buffer[0] == MAVLINK_STX ? true : false;

					int headerlength = mavlinkv2 ? MAVLINK_CORE_HEADER_LEN : MAVLINK_CORE_HEADER_MAVLINK1_LEN;
					int headerlengthstx = headerlength + 1;

					// if we have the header, and no other chars, get the length and packet identifiers
					if (count == 0)
					{
						DateTime to = DateTime.Now.AddMilliseconds(BaseStream.ReadTimeout);

						while (BaseStream.IsOpen && BaseStream.BytesAvailable < headerlength)
						{
							if (DateTime.Now > to)
							{
								log.InfoFormat("MAVLINK: 2 wait time out btr {0} len {1}", BaseStream.BytesAvailable,
									length);
								throw new TimeoutException("Timeout");
							}
							Thread.Sleep(1);
						}
						int read = BaseStream.Read(buffer, 1, headerlength);
						count = read;
						if (RawLogFile != null && RawLogFile.CanWrite)
							RawLogFile.Write(buffer, 1, read);
					}

					// packet length
					if (buffer[0] == MAVLINK_STX)
					{
						length = buffer[1] + headerlengthstx + MAVLINK_NUM_CHECKSUM_BYTES; // data + header + checksum - magic - length
						if ((buffer[2] & MAVLINK_IFLAG_SIGNED) > 0)
						{
							length += MAVLINK_SIGNATURE_BLOCK_LEN;
						}
					}
					else
					{
						length = buffer[1] + headerlengthstx + MAVLINK_NUM_CHECKSUM_BYTES; // data + header + checksum - U - length    
					}

					if (count >= headerlength)
					{
						try
						{
							DateTime to = DateTime.Now.AddMilliseconds(BaseStream.ReadTimeout);

							while (BaseStream.IsOpen && BaseStream.BytesAvailable < (length - (headerlengthstx)))
							{
								if (DateTime.Now > to)
								{
									log.InfoFormat("MAVLINK: 3 wait time out btr {0} len {1}",
										BaseStream.BytesAvailable, length);
									break;
								}
								Thread.Sleep(1);
							}
							if (BaseStream.IsOpen)
							{
								int read = BaseStream.Read(buffer, headerlengthstx, length - (headerlengthstx));
								if (read != (length - headerlengthstx))
									log.InfoFormat("MAVLINK: bad read {0}, {1}, {2}", headerlengthstx, length,
										count);
								if (RawLogFile != null && RawLogFile.CanWrite)
								{
									// write only what we read, temp is the whole packet, so 6-end
									RawLogFile.Write(buffer, headerlengthstx, read);
								}
							}
							count = length;
						}
						catch
						{
							break;
						}
						break;
					}
				}

				count++;
				if (count == 299)
					break;
			}

			// resize the packet to the correct length
			Array.Resize(ref buffer, count);

			if (buffer.Length == 0)
				return MAVLinkMessage.Invalid;

			if (message == null)
				message = new MAVLinkMessage(buffer);

			uint msgid = message.msgid;

			message_info msginfo = MAVLINK_MESSAGE_INFOS.GetMessageInfo(msgid);

			// calc crc
			var sigsize = (message.sig != null) ? MAVLINK_SIGNATURE_BLOCK_LEN : 0;
			ushort crc = MavlinkCRC.crc_calculate(buffer, message.Length - sigsize - MAVLINK_NUM_CHECKSUM_BYTES);

			// calc extra bit of crc for mavlink 1.0/2.0
			if (message.header == 0xfe || message.header == 0xfd)
			{
				crc = MavlinkCRC.crc_accumulate(msginfo.crc, crc);
			}

			// check message length size vs table (mavlink1 explicit size check | mavlink2 oversize check, no undersize because of 0 trimming)
			if ((!message.ismavlink2 && message.payloadlength != msginfo.minlength) || (message.ismavlink2 && message.payloadlength > msginfo.length))
			{
				if (msginfo.length == 0) // pass for unknown packets
				{
					// log.InfoFormat("unknown packet type {0}", message.msgid);
				}
				else
				{
					log.InfoFormat("Mavlink Bad Packet (Len Fail) len {0} pkno {1}", buffer.Length, message.msgid);
					return MAVLinkMessage.Invalid;
				}
			}

			// check crc
			if ((message.crc16 >> 8) != (crc >> 8) ||
				(message.crc16 & 0xff) != (crc & 0xff))
			{
				if (buffer.Length > 5 && msginfo.name != null)
					log.InfoFormat("Mavlink Bad Packet (crc fail) len {0} crc {1} vs {4} pkno {2} {3}", buffer.Length,
						crc, message.msgid, msginfo.name.ToString(),
						message.crc16);
				return MAVLinkMessage.Invalid;
			}

            if (message.IsGCSPacket())
                return message;

			byte sysid = message.sysid;
			byte compid = message.compid;
			byte packetSeqNo = message.seq;

			if (buffer.Length >= 5 && (sysid == 255 || sysid == 253)) // gcs packet
				return message;

            if (SysId == 0)
            {
                log.Info($"MavCore opened with (sysid, compid)=({sysid},{compid})");
                SysId = sysid;
                CompId = compid;
                Status.MavLinkV2 = message.buffer[0] == MAVLINK_STX ? true : false;
            } else if (sysid != SysId)
            {
                log.Info($"MavCore ({SysId},{CompId}) received foreign packet ({sysid},{compid}), dropped");
                return MAVLinkMessage.Invalid;
            } else if (compid != CompId)
            {
                // handle multi component here
                log.Info($"MavCore ({SysId},{CompId}) received foreign packet ({sysid},{compid}), dropped");
                return MAVLinkMessage.Invalid;
            }

			// update packet loss statistics
			if (Status.PacketLostTimer.AddSeconds(5) < DateTime.Now)
			{
                Status.PacketLostTimer = DateTime.Now;
                Status.PacketsLost = Status.PacketsLost * 0.8f;
                Status.PacketsNotLost = Status.PacketsNotLost * 0.8f;
			}

			try
			{
				if ((message.header == 'U' || message.header == 0xfe || message.header == 0xfd) && buffer.Length >= message.payloadlength)
				{
					// check if we lost pacakets based on seqno
					int expectedSeqNo = ((Status.recvPacketCount + 1) % 0x100);

					// the second part is to work around a 3dr radio bug sending dup seqno's
					if (packetSeqNo != expectedSeqNo && packetSeqNo != Status.recvPacketCount)
					{
                        Status.SyncLost++; // actualy sync loss's
						int numLost = 0;
						if (packetSeqNo < ((Status.recvPacketCount + 1)))
						// recvpacketcount = 255 then   10 < 256 = true if was % 0x100 this would fail
						{
							numLost = 0x100 - expectedSeqNo + packetSeqNo;
						}
						else
						{
							numLost = packetSeqNo - expectedSeqNo;
						}
                        Status.PacketsLost += numLost;
					}
                    Status.PacketsNotLost++;
                    //Console.WriteLine("{0} {1}", sysid, packetSeqNo);
                    Status.recvPacketCount = packetSeqNo;

					// packet stats per mav
					if (!Status.PacketsPerSecond.ContainsKey(msgid) || double.IsInfinity(Status.PacketsPerSecond[msgid]))
                        Status.PacketsPerSecond[msgid] = 0;
					if (!Status.PacketsPerSecondBuild.ContainsKey(msgid))
                        Status.PacketsPerSecondBuild[msgid] = DateTime.Now;

                    Status.PacketsPerSecond[msgid] = (((1000 /
                        ((DateTime.Now - Status.PacketsPerSecondBuild[msgid]).TotalMilliseconds) +
                                                        Status.PacketsPerSecond[msgid]) / 2));

                    Status.PacketsPerSecondBuild[msgid] = DateTime.Now;

                    // store packet history
                    Status.AddPacket(message);

					try
					{
						if (LogFile != null && LogFile.CanWrite)
						{
							lock (LogFile)
							{
								byte[] datearray =
									BitConverter.GetBytes(
										(UInt64)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds * 1000));
								Array.Reverse(datearray);
								LogFile.Write(datearray, 0, datearray.Length);
								LogFile.Write(buffer, 0, buffer.Length);

								if (msgid == 0)
								{
									// flush on heartbeat - 1 seconds
									LogFile.Flush();
									RawLogFile.Flush();
								}
							}
						}
					}
					catch (Exception ex)
					{
						log.Error(ex);
					}
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}

			// update last valid packet receive time
			Status.LastPacket = DateTime.Now;

			return message;
		}

        private byte[] PreparePacket(MAVLINK_MSG_ID msgid, object indata)
        {
            int messageType = (byte)msgid;
            byte[] data = null;
            try
            {
                data = MavlinkUtil.StructureToByteArray(indata);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                int len = Marshal.SizeOf(indata);
                byte[] arr = new byte[len];
                IntPtr ptr = Marshal.AllocHGlobal(len);
                Marshal.StructureToPtr(indata, ptr, true);
                Marshal.Copy(ptr, arr, 0, len);
                Marshal.FreeHGlobal(ptr);
                data = arr;
            }
            var info = MAVLINK_MESSAGE_INFOS.SingleOrDefault(p => p.msgid == messageType);
            if (data.Length != info.minlength) Array.Resize(ref data, (int)info.minlength);
            byte[] packet = new byte[data.Length + 6 + 2];
            packet[0] = MAVLINK_STX_MAVLINK1;
            packet[1] = (byte)data.Length;
            packet[2] = (byte)++pacCount;
            packet[3] = GROUNDCONTROLSTATION_SYSTEM_ID;
            packet[4] = (byte)MAV_COMPONENT.MAV_COMP_ID_MISSIONPLANNER;
            packet[5] = (byte)messageType;
            Array.Copy(data, 0, packet, 6, data.Length);

            ushort checksum = MavlinkCRC.crc_calculate(packet, packet[1] + 6);
            checksum = MavlinkCRC.crc_accumulate(MAVLINK_MESSAGE_INFOS.GetMessageInfo((uint)messageType).crc, checksum);
            packet[packet.Length - 2] = (byte)(checksum & 0xFF);
            packet[packet.Length - 1] = (byte)(checksum >> 8);
            return packet;
        }

        public void SendPacket(MAVLINK_MSG_ID msgid, object indata)
        {
            byte[] pktdata = PreparePacket(msgid, indata);
            lock (writeLock) BaseStream.Write(pktdata, 0, pktdata.Length);
        }

        public delegate bool WaitPacketFilter(MAVLinkMessage packet);
        public delegate bool ReplyPacketFilter(MAVLinkMessage packet, ref bool more);

        public MAVLinkMessage WaitPacket(MAVLINK_MSG_ID msgid,
            WaitPacketFilter filter = null, int timeoutms = Timeout.Infinite, bool gcs = false)
        {
            MAVLinkMessage reply = null;
            using (AutoResetEvent ev = new AutoResetEvent(false))
            {
                void eh(object o, MAVLinkMessage p)
                {
                    if (filter == null || filter(p))
                    {
                        reply = p;
                        ev.Set();
                    }
                }
                var pkt = Status.GetPacket(msgid);
                var lasttime = pkt?.rxtime ?? DateTime.Now;
                RegisterMavMessageHandler(msgid, eh, gcs);
                ev.WaitOne(timeoutms);
                UnregisterMavMessageHandler(msgid, eh, gcs);
                // last minute ride
                if (reply == null)
                {
                    pkt = Status.GetPacket(msgid);
                    if (pkt != null && pkt.rxtime > lasttime && (filter == null || filter(pkt)))
                        reply = pkt;
                }
            }
            return reply;
        }

        public MAVLinkMessage WaitPackets(MAVLINK_MSG_ID[] msgids,
            WaitPacketFilter[] filters, int timeoutms = Timeout.Infinite, bool gcs = false)
        {
            MAVLinkMessage reply = null;
            using (AutoResetEvent ev = new AutoResetEvent(false))
            {
                DateTime lasttime = DateTime.MinValue;
                var ehs = new EventHandler<MAVLinkMessage>[msgids.Length];
                for (var i = msgids.Length; i > 0;)
                {
                    int ival = --i;
                    ehs[ival] = (object o, MAVLinkMessage p) =>
                    {
                        if (filters[ival] == null || filters[ival](p))
                        {
                            reply = p;
                            ev.Set();
                        }
                    };
                    RegisterMavMessageHandler(msgids[ival], ehs[ival], gcs);
                    var ipkt = Status.GetPacket(msgids[ival]);
                    if (ipkt != null && ipkt.rxtime > lasttime)
                        lasttime = ipkt.rxtime;
                }
                if (lasttime == DateTime.MinValue)
                    lasttime = DateTime.Now;
                ev.WaitOne(timeoutms);
                for (var i = msgids.Length; --i >= 0; )
                {
                    UnregisterMavMessageHandler(msgids[i], ehs[i], gcs);
                    if (reply == null)
                    {
                        // last minute ride
                        var ipkt = Status.GetPacket(msgids[i]);
                        if (ipkt != null && ipkt.rxtime > lasttime &&
                                (filters[i] == null || filters[i](ipkt)))
                            reply = ipkt;
                    }
                }
            }
            return reply;
        }

        public MAVLinkMessage SendPacketWaitReply(MAVLINK_MSG_ID msgid, object indata,
            MAVLINK_MSG_ID replyid, ReplyPacketFilter filter = null, int timeoutms = 1000, bool gcs = false)
        {
            MAVLinkMessage reply = null;
            DateTime due;
            using (ManualResetEvent ev = new ManualResetEvent(false))
            {
                void eh(object o, MAVLinkMessage p)
                {
                    bool more = false;
                    if (filter == null || filter(p, ref more))
                    {
                        if (more)
                            due = DateTime.Now.AddMilliseconds(timeoutms);
                        else
                        {
                            reply = p;
                            ev.Set();
                        }
                    }
                }
                byte[] pktdata = PreparePacket(msgid, indata);
                var pkt = Status.GetPacket(replyid);
                var lasttime = pkt?.rxtime ?? DateTime.Now;
                due = DateTime.Now.AddMilliseconds(timeoutms);
                RegisterMavMessageHandler(replyid, eh, gcs);
                //packetNotifier += eh;
                lock (writeLock) BaseStream.Write(pktdata, 0, pktdata.Length);
                while (!ev.WaitOne(due - DateTime.Now) && DateTime.Now < due);
                //packetNotifier -= eh;
                UnregisterMavMessageHandler(replyid, eh, gcs);
                // last minute ride
                if (reply == null)
                {
                    pkt = Status.GetPacket(replyid);
                    bool more = false;
                    if (pkt != null && pkt.rxtime > lasttime && filter(pkt, ref more))
                        reply = pkt;
                }
            }
            return reply;
        }

        public MAVLinkMessage SendPacketWaitReplies(MAVLINK_MSG_ID msgid, object indata,
            MAVLINK_MSG_ID[] rids, ReplyPacketFilter[] filters, int timeoutms = 1000, bool gcs = false)
        {
            if (rids == null || filters == null || filters.Length == 0 
                || rids.Length != filters.Length)
            {
                SendPacket(msgid, indata);
                return null;
            }
            MAVLinkMessage reply = null;
            DateTime due;
            using (ManualResetEvent ev = new ManualResetEvent(false))
            {
                byte[] pktdata = PreparePacket(msgid, indata);
                due = DateTime.Now.AddMilliseconds(timeoutms);
                var ehs = new EventHandler<MAVLinkMessage>[rids.Length];
                for (var i = rids.Length; i > 0; )
                {
                    int ival = --i;
                    ehs[ival] = (object o, MAVLinkMessage p) =>
                    {
                        bool more = false;
                        if (filters[ival] == null || filters[ival](p, ref more))
                        {
                            if (more)
                                due = DateTime.Now.AddMilliseconds(timeoutms);
                            else
                            {
                                reply = p;
                                ev.Set();
                            }
                        }
                    };
                    RegisterMavMessageHandler(rids[ival], ehs[ival], gcs);
                }
                lock (writeLock) BaseStream.Write(pktdata, 0, pktdata.Length);
                while (!ev.WaitOne(due - DateTime.Now) && DateTime.Now < due);
                for (var i = rids.Length; --i >= 0; )
                    UnregisterMavMessageHandler(rids[i], ehs[i], gcs);
            }
            return reply;
        }
        #endregion Core

        #region Background Loop
        private static readonly List<MavCore<StatusType>> mavs = new List<MavCore<StatusType>>();
        static MavCore() { BackgroundLoop.Start(MavCoreBackgroundLoop); }

        private static void MavCoreBackgroundLoop(CancellationToken token)
        {
            DateTime lastHeartBeatSent = DateTime.Now;
            DateTime nextUpdateTime = DateTime.Now.AddSeconds(30);
            DateTime lastUpdatedTime = DateTime.MinValue;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    lock (mavs) foreach (var mav in mavs)
                    {
                        if (!mav.BaseStream.IsOpen) continue;
                        try
                        {
                            while (mav.BaseStream.BytesAvailable > 5)
                            {
                                MAVLinkMessage packet = mav.ReadPacket();
                                if (packet.Length < 5) break;
                                mav.HandleMavLinkMessage(packet);
                            }
                        }
                        catch { }
                    }

                    if (lastHeartBeatSent.Second != DateTime.Now.Second)
                    {
                        lastHeartBeatSent = DateTime.Now;
                        lock (mavs) foreach (var mav in mavs)
                        {
                            if (!mav.BaseStream.IsOpen) continue;
                            try
                            {
                                mav.SendPacket(MAVLINK_MSG_ID.HEARTBEAT, new mavlink_heartbeat_t
                                {
                                    type = (byte)MAV_TYPE.GCS,
                                    autopilot = (byte)MAV_AUTOPILOT.INVALID,
                                    mavlink_version = 3 // MAVLINK_VERSION
                                });
                            }
                            catch { }
                        }
                    }

                    if (DateTime.Now > nextUpdateTime)
                    {
                        nextUpdateTime = DateTime.Now.AddSeconds(30);
                        lock (mavs) foreach (var mav in mavs)
                        {
                            if (!mav.BaseStream.IsOpen) continue;
                            // re-request streams
                            try
                            {
                                mav.GetDataStream(MAV_DATA_STREAM.EXTENDED_STATUS, 2);
                                mav.GetDataStream(MAV_DATA_STREAM.POSITION, 2);
                                mav.GetDataStream(MAV_DATA_STREAM.EXTRA1, 4);
                                mav.GetDataStream(MAV_DATA_STREAM.EXTRA2, 4);
                                mav.GetDataStream(MAV_DATA_STREAM.EXTRA3, 2);
                                mav.GetDataStream(MAV_DATA_STREAM.RAW_SENSORS, 2);
                                mav.GetDataStream(MAV_DATA_STREAM.RC_CHANNELS, 2);
                            }
                            catch
                            {
                                Console.WriteLine("Failed to request rates");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("MavCoreBackgroundLoop got exception: " + ex);
                }
            }
            Console.WriteLine("MavCoreBackgroundLoop done");
        }
        #endregion Background Loop

        #region Retrieve packets from flight control.
        public class MessageHolder { public object Message = null; };

        public const int MAX_MAVLINK_MSGID = 256;
        private readonly EventHandler<MAVLinkMessage>[] msgMap = new EventHandler<MAVLinkMessage>[MAX_MAVLINK_MSGID];
        private readonly EventHandler<MAVLinkMessage>[] gcsMsgMap = new EventHandler<MAVLinkMessage>[MAX_MAVLINK_MSGID];
        protected void RegisterMavMessageHandler(MAVLINK_MSG_ID msgid, EventHandler<MAVLinkMessage> handler, bool gcs = false)
            => (gcs ? gcsMsgMap : msgMap)[(uint)msgid] += handler;
        protected void UnregisterMavMessageHandler(MAVLINK_MSG_ID msgid, EventHandler<MAVLinkMessage> handler, bool gcs = false)
            => (gcs ? gcsMsgMap : msgMap)[(uint)msgid] -= handler;
        protected static T GetMessage<T>(MAVLinkMessage packet, ref object h)
        {
            var mh = h as MessageHolder;
            return (T)(mh.Message ?? (mh.Message = packet.ToStructure<T>()));
        }

        protected void HandleMavLinkMessage(MAVLinkMessage message)
        {
            if (message != null && message.msgid < MAX_MAVLINK_MSGID)
                (message.IsGCSPacket() ? gcsMsgMap : msgMap)[message.msgid]?.
                    Invoke(new MessageHolder(), message);
        }

        protected void InitializeMavLinkMessageHandler()
        {
            RegisterMavMessageHandler(MAVLINK_MSG_ID.HEARTBEAT, HeartBeatPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.TIMESYNC, TimeSyncPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.PARAM_VALUE, ParamValuePacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GLOBAL_POSITION_INT, GPSPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GPS_RAW_INT, GPSRawPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.SYS_STATUS, SysStatusPacketHandler);
        }

        private void HeartBeatPacketHandler(object holder, MAVLinkMessage packet) =>
            GetMessage<mavlink_heartbeat_t>(packet, ref holder);

        private void TimeSyncPacketHandler(object holder, MAVLinkMessage packet)
        {
            var tsync = GetMessage<mavlink_timesync_t>(packet, ref holder);
            Int64 now_ns =
                (Int64)((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds *
                         1000000);
            // tc1 - client
            // ts1 - server

            // system does not know the time
            if (tsync.tc1 == 0)
            {
                tsync.tc1 = now_ns;
                SendPacket(MAVLINK_MSG_ID.TIMESYNC, tsync);
            } // system knows the time 
            else if (tsync.tc1 > 0)
            {
                Int64 offset_ns = (tsync.ts1 + now_ns - tsync.tc1 * 2) / 2;
                Int64 dt = Status.TimeOffset_ns - offset_ns;

                if (Math.Abs(dt) > 10000000) // 10 millisecond skew
                {
                    Status.TimeOffset_ns = offset_ns; // hard-set it.
                }
                else
                {
                    var offset_avg_alpha = 0.6;
                    var avg = (offset_avg_alpha * offset_ns) +
                              (1.0 - offset_avg_alpha) * Status.TimeOffset_ns;
                    Status.TimeOffset_ns = (long)avg;
                }
            }
        }

        private void ParamValuePacketHandler(object holder, MAVLinkMessage packet)
        {
            var value = GetMessage<mavlink_param_value_t>(packet, ref holder);
            string st = ASCIIEncoding.ASCII.GetString(value.param_id).Split('\0')[0];

            Status.ParamTypes[st] = (MAV_PARAM_TYPE)value.param_type;
            if (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA && packet.compid != (byte)MAV_COMPONENT.MAV_COMP_ID_UDP_BRIDGE)
            {
                var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(value.param_value),
                    MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)value.param_type);
            }
            else
            {
                var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(value.param_value),
                    (MAV_PARAM_TYPE)value.param_type, (MAV_PARAM_TYPE)value.param_type);
            }
            Status.ParamTypes[st] = (MAV_PARAM_TYPE)value.param_type;
            Status.Params.TotalReported = value.param_count;
            //Console.WriteLine($"ParamValuePacketHandler: idx={value.param_index}, name={st}");
        }

        private void GPSPacketHandler(object holder, MAVLinkMessage packet)
        {
            var loc = GetMessage<mavlink_global_position_int_t>(packet, ref holder);

            // some flight control reserves 0 alt and 0 long
            if (loc.lat == 0 && loc.lon == 0)
            {
                GPSLocationMode = false;
            }
            else
            {
                GPSLocationMode = true;
                Status.Latitude = loc.lat * 1.0e-7;
                Status.Longitude = loc.lon * 1.0e-7;
                Status.AbsoluteAltitude = loc.alt * 1.0e-3;
            }
            Status.Altitude = loc.relative_alt * 1.0e-3f;
        }

        private void GPSRawPacketHandler(object holder, MAVLinkMessage packet)
        {
            var gps = GetMessage<mavlink_gps_raw_int_t>(packet, ref holder);

            if (!GPSLocationMode)
            {
                Status.Latitude = gps.lat * 1.0e-7;
                Status.Longitude = gps.lon * 1.0e-7;
                Status.AbsoluteAltitude = gps.alt * 1.0e-3;
            }

            Status.SatteliteCount = gps.satellites_visible;
        }

        private void SysStatusPacketHandler(object holder, MAVLinkMessage packet)
        {
            var sysstatus = GetMessage<mavlink_sys_status_t>(packet, ref holder);

            float load = (float)sysstatus.load / 10.0f;

            float battery_voltage = (float)sysstatus.voltage_battery / 1000.0f;

            byte battery_remaining = sysstatus.battery_remaining;
            float current = (float)sysstatus.current_battery / 100.0f;

            ushort packetdropremote = sysstatus.drop_rate_comm;

            Status.BatteryVoltage = battery_voltage;
        }
        #endregion Retrieve sensor data from flight control

        #region Parameters
        private mavlink_param_value_t VerifyParam(MAVLINK_MSG_ID msgid, object outp, string name, int timeout, int retries = 3)
        {
            mavlink_param_value_t pv = new mavlink_param_value_t();
            while (SendPacketWaitReply(MAVLINK_MSG_ID.PARAM_SET, outp,
                MAVLINK_MSG_ID.PARAM_VALUE, (MAVLinkMessage p, ref bool more) =>
                {
                    pv = p.ToStructure<mavlink_param_value_t>();
                    string st = ASCIIEncoding.ASCII.GetString(pv.param_id).Split('\0')[0];
                    if (st != name)
                        log.Error($"Wrong Param Replied {pv.param_index} - " +
                            $"{ASCIIEncoding.ASCII.GetString(pv.param_id)} - {pv.param_value}" +
                            $"Expected: '{name}' Received: '{st}'");
                    return st == name;
                }, timeout) == null)
                if (retries-- < 0)
                    throw new TimeoutException("Timeout on waiting PARAM_VALUE for " + name);
            return pv;
        }

        public float GetParam(string name = "")
		{
			if (name == "") return 0;

			log.Info("GetParam name: '" + name + "' " + SysId + ":" + CompId);

            mavlink_param_request_read_t req = new mavlink_param_request_read_t
            {
                target_component = CompId,
                target_system = SysId,
                param_id = ASCIIEncoding.ASCII.GetBytes(name),
                param_index = -1
            };
			Array.Resize(ref req.param_id, 16);

            mavlink_param_value_t pv = VerifyParam(MAVLINK_MSG_ID.PARAM_REQUEST_READ, req, name, 1000);
            var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
            Status.Params[name] = new MAVLinkParam(name, BitConverter.GetBytes(pv.param_value),
                Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA ?
                   MAV_PARAM_TYPE.REAL32 : (MAV_PARAM_TYPE)pv.param_type,
                (MAV_PARAM_TYPE)pv.param_type);
            Status.ParamTypes[name] = (MAV_PARAM_TYPE)pv.param_type;
            log.Info($"{DateTime.Now.Millisecond} got param {pv.param_index} of {pv.param_count} name: {name}");
            return pv.param_value;
        }

        public bool SetParam(string name, double value)
        {
            if (!Status.Params.ContainsKey(name))
            {
                log.Warn("Trying to set Param that doesnt exist " + name + "=" + value);
                return false;
            }

            if (Status.Params[name].Value == value)
            {
                log.Warn("setParam " + name + " not modified as same");
                return true;
            }

            var req = new mavlink_param_set_t
            {
                target_system = SysId,
                target_component = CompId,
                // param type is set here, however it is always sent over the air as a float 100int = 100f.
                param_type = (byte)Status.ParamTypes[name],
                param_value = new MAVLinkParam(name, value,
                    (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA) ?
                        MAV_PARAM_TYPE.REAL32 : Status.ParamTypes[name]).float_value,
                param_id = ASCIIEncoding.ASCII.GetBytes(name)
            };
            Array.Resize(ref req.param_id, 16);

            log.Info($"setParam '{name}'='{value}' sysid {SysId} compid {CompId}");
            mavlink_param_value_t pv = VerifyParam(MAVLINK_MSG_ID.PARAM_SET, req, name, 700);
            var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
            Status.Params[name] = new MAVLinkParam(name, BitConverter.GetBytes(pv.param_value),
                (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA) ?
                    MAV_PARAM_TYPE.REAL32 : (MAV_PARAM_TYPE)pv.param_type,
                (MAV_PARAM_TYPE)pv.param_type);
            log.Info($"SetParam got ack {name} : {Status.Params[name]}");
            return true;
        }

        public bool SetAnyParam(string[] names, double value) => names.Any(n => SetParam(n, value));

        private Dictionary<string, double> GetParamListBG()
		{
            int paramTotal = 0;
			List<int> indices = new List<int>();
			MAVLinkParamList paramList = new MAVLinkParamList();
            bool waitMore = true;
            bool ParamValueHandler(MAVLinkMessage m, ref bool more)
            {
                if (frmProgressReporter.doWorkArgs.CancelRequested) return false;

                mavlink_param_value_t pv = m.ToStructure<mavlink_param_value_t>();
                more = waitMore;
                if (indices.Contains(pv.param_index)) return true;

                paramTotal = (pv.param_count);
                paramList.TotalReported = paramTotal;
                string pid = ASCIIEncoding.ASCII.GetString(pv.param_id).Split('\0')[0];
                var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                paramList[pid] = new MAVLinkParam(pid, BitConverter.GetBytes(pv.param_value),
                    (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA) ?
                        MAV_PARAM_TYPE.REAL32 : (MAV_PARAM_TYPE)pv.param_type,
                    (MAV_PARAM_TYPE)pv.param_type);
                Status.ParamTypes[pid] = (MAV_PARAM_TYPE)pv.param_type;
                if (pv.param_index != 65535) indices.Add(pv.param_index);

                frmProgressReporter.UpdateProgressAndStatus(
                    (indices.Count * 100) / paramTotal, Strings.GotParam + pid);

                more &= pv.param_index != (paramTotal - 1);
                return true;
            }

            int retries = 6;
            do
            {
                SendPacketWaitReplies(MAVLINK_MSG_ID.PARAM_REQUEST_LIST, new
                    mavlink_param_request_list_t { target_system = SysId, target_component = CompId },
                    new MAVLINK_MSG_ID[] { MAVLINK_MSG_ID.PARAM_VALUE, MAVLINK_MSG_ID.STATUSTEXT },
                    new ReplyPacketFilter[]
                    {
                        ParamValueHandler,
                        (MAVLinkMessage m, ref bool more) =>
                        {
                            mavlink_statustext_t pv = m.ToStructure<mavlink_statustext_t>();
                            string st = ASCIIEncoding.ASCII.GetString(pv.text).Split('\0')[0];
                            switch (st)
                            {
                                case string c when c.Contains("copter"):
                                case string r when r.Contains("rover"):
                                case string p when p.Contains("plane"):
                                    Status.VersionString = st;
                                    break;
                                case string n when n.Contains("nuttx"):
                                    Status.SoftwareVersions = st;
                                    break;
                                case string x when x.Contains("px4v2"):
                                    Status.SerialString = st;
                                    break;
                                case string f when f.Contains("frame"):
                                    Status.FrameString = st;
                                    break;
                            }
                            return more = true;
                        }
                    }, 4000);
                if (paramTotal > 0 && indices.Count == paramTotal)
                {
                    Status.Params.Clear();
                    Status.Params.TotalReported = paramTotal;
                    Status.Params.AddRange(paramList);
                    return Status.Params;
                }
                if (frmProgressReporter.doWorkArgs.CancelRequested)
                {
                    frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
                    frmProgressReporter.doWorkArgs.ErrorMessage = Strings.MsgFormProgressUserCanceled;
                    return Status.Params;
                }
            } while (retries-- > 0);

            if (indices.Count != paramTotal || paramTotal == 0)
			{
                if (paramTotal > 0)
                {
                    Console.WriteLine("Not all params read, start one by one mode.");
                    waitMore = false;
                    mavlink_param_request_read_t req = new mavlink_param_request_read_t
                    {
                        target_system = SysId,
                        target_component = CompId,
                        param_id = new byte[] { 0x0 }
                    };
                    Array.Resize(ref req.param_id, 16);
                    for (short i = 0; i < paramTotal; i++)
                    {
                        if (frmProgressReporter.doWorkArgs.CancelRequested) break;
                        if (indices.Contains(i)) continue;
                        req.param_index = i;
                        SendPacketWaitReply(MAVLINK_MSG_ID.PARAM_REQUEST_READ, req,
                            MAVLINK_MSG_ID.PARAM_VALUE, ParamValueHandler);
                        if (!indices.Contains(i)) break;
                    }
                }

                if (indices.Count != paramTotal || paramTotal == 0)
                {
                    if (frmProgressReporter.doWorkArgs.CancelRequested)
                    {
                        frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
                        frmProgressReporter.doWorkArgs.ErrorMessage = Strings.MsgFormProgressUserCanceled;
                        return Status.Params;
                    }
                    else
                        throw new Exception($"Missing Params: got {indices.Count}/{paramTotal}");
                }
            }

			Status.Params.Clear();
			Status.Params.TotalReported = paramTotal;
			Status.Params.AddRange(paramList);
			return Status.Params;
		}
        #endregion Parameters

		void SetupMavConnect(MAVLinkMessage message, mavlink_heartbeat_t hb)
		{
            /*if (SysId != message.sysid || CompId != message.compid)
            {
                log.Info($"MavCore ({SysId},{CompId}) received foreign packet ({message.sysid},{message.compid}), dropped");
            }*/
            mavlinkVersion = hb.mavlink_version;
			Status.APType = (MAV_TYPE)hb.type;
			Status.APName = (MAV_AUTOPILOT)hb.autopilot;
			Status.recvPacketCount = message.seq;
		}

        public MAVLinkMessage GetHeartBeat(bool setup = false)
		{
			MAVLinkMessage buffer = WaitPacket(MAVLINK_MSG_ID.HEARTBEAT, null, 2200);
            if (buffer != null)
            {
                mavlink_heartbeat_t hb = buffer.ToStructure<mavlink_heartbeat_t>();
                if ((MAV_TYPE)hb.type == MAV_TYPE.GCS)
                    buffer = null;
                else if (setup)
                    SetupMavConnect(buffer, hb);
            }

            return buffer ?? MAVLinkMessage.Invalid;
        }

        public void SetAPType(MAV_TYPE type, MAV_AUTOPILOT name)
		{
            Status.APType = type;
            Status.APName = name;
			switch (name)
			{
				case MAV_AUTOPILOT.ARDUPILOTMEGA:
					switch (type)
					{
						case MAV_TYPE.FIXED_WING:
                            Status.Firmware = Firmwares.ArduPlane;
							break;
						case MAV_TYPE.QUADROTOR:
                            Status.Firmware = Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.TRICOPTER:
                            Status.Firmware = Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.HEXAROTOR:
                            Status.Firmware = Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.OCTOROTOR:
                            Status.Firmware = Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.HELICOPTER:
                            Status.Firmware = Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.GROUND_ROVER:
                            Status.Firmware = Firmwares.ArduRover;
							break;
						case MAV_TYPE.SUBMARINE:
                            Status.Firmware = Firmwares.ArduSub;
							break;
						case MAV_TYPE.ANTENNA_TRACKER:
                            Status.Firmware = Firmwares.ArduTracker;
							break;
						default:
							log.Error(Status.APType + " not registered as valid type");
							break;
					}
					break;
				case MAV_AUTOPILOT.UDB:
					switch (type)
					{
						case MAV_TYPE.FIXED_WING:
                            Status.Firmware = Firmwares.ArduPlane;
							break;
					}
					break;
				case MAV_AUTOPILOT.GENERIC:
					switch (type)
					{
						case MAV_TYPE.FIXED_WING:
                            Status.Firmware = Firmwares.Ateryx;
							break;
					}
					break;
				case MAV_AUTOPILOT.PX4:
                    Status.Firmware = Firmwares.PX4;
					break;
				default:
					switch (type)
					{
						case MAV_TYPE.GIMBAL: // storm32 - name 83
                            Status.Firmware = Firmwares.Gymbal;
							break;
					}
					break;
			}
		}

		public bool GetVersion()
		{
            int retries = 3;
            MAVLinkMessage reply = null;
            do
            {
                reply = SendPacketWaitReply(MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST,
                    new mavlink_autopilot_version_request_t
                    {
                        target_component = CompId,
                        target_system = SysId
                    }, MAVLINK_MSG_ID.AUTOPILOT_VERSION);
            } while (retries-- > 0);
            return reply != null;
		}

        public void SendCommand(MAV_CMD cmd, float p1, float p2, float p3, float p4, float p5, float p6, float p7)
        {
            log.Info($"SendCommand cmd {cmd.ToString()} {p1} {p2} {p3} {p4} {p5} {p6} {p7}");
            SendPacket(MAVLINK_MSG_ID.COMMAND_LONG, new mavlink_command_long_t
            {
                target_system = SysId,
                target_component = CompId,
                command = (ushort)cmd,
                param1 = p1,
                param2 = p2,
                param3 = p3,
                param4 = p4,
                param5 = p5,
                param6 = p6,
                param7 = p7
            });
        }

        public bool SendCommandWaitAck(MAV_CMD cmd, float p1, float p2, float p3, float p4, float p5, float p6, float p7, int timeoutms = 2000)
        {
            log.Info($"SendCommandWaitAck cmd {cmd.ToString()} {p1} {p2} {p3} {p4} {p5} {p6} {p7}");
            int retries = 3;
            MAVLinkMessage ack = null;
            do
            {
                ack = SendPacketWaitReply(MAVLINK_MSG_ID.COMMAND_LONG,
                    new mavlink_command_long_t
                    {
                        target_system = SysId,
                        target_component = CompId,
                        command = (ushort)cmd,
                        param1 = p1,
                        param2 = p2,
                        param3 = p3,
                        param4 = p4,
                        param5 = p5,
                        param6 = p6,
                        param7 = p7
                    }, MAVLINK_MSG_ID.COMMAND_ACK,
                    (MAVLinkMessage p, ref bool more) =>
                        (MAV_CMD)p.ToStructure<mavlink_command_ack_t>().command == cmd);
            } while (ack == null && retries-- > 0);
            if (ack != null)
                return (MAV_RESULT)ack.ToStructure<mavlink_command_ack_t>().result == MAV_RESULT.ACCEPTED;
            throw new TimeoutException($"Timeout on waiting ack from command {cmd}");
        }

        public void GetDataStream(MAV_DATA_STREAM id, byte hzrate)
		{
			mavlink_request_data_stream_t req = new mavlink_request_data_stream_t
            {
                target_component = CompId,
                target_system = SysId,
                req_message_rate = hzrate,
                start_stop = 1, //start
                req_stream_id = (byte)id
            };

			// send each one twice.
			SendPacket(MAVLINK_MSG_ID.REQUEST_DATA_STREAM, req);
			SendPacket(MAVLINK_MSG_ID.REQUEST_DATA_STREAM, req);
		}
	}
}
