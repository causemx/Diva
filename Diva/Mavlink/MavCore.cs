using Diva.Controls;
using Diva.Properties;
using Diva.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static readonly double CONNECT_TIMEOUT_SECONDS = 30;
        public static int GET_PARAM_TIMEOUT = 1000;
        private const int GROUNDCONTROLSTATION_SYSTEM_ID = 255;
        #endregion Constants

        private int portInUseLevel = 0;
        private StackTrace portLockTracer;
        private Timer portUnlockTimer;
        public bool PortInUse
        {
            get => portInUseLevel > 0;
            set
            {
                if (value)
                {
                    portInUseLevel++;
                    if (!portUnlockTimer.Enabled)
                    {
                        portUnlockTimer.Start();
                        portLockTracer = new StackTrace();
                    }
                }
                else if (portInUseLevel > 0)
                {
                    if (--portInUseLevel == 0)
                    {
                        portUnlockTimer.Stop();
                    }
                }
            }
        }

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

		public event EventHandler ParamListChanged;

        public byte SysId { get; private set; }
        public byte CompId { get; private set; }

        public BufferedStream LogFile { get; set; }
		public BufferedStream RawLogFile { get; set; }

		internal string plaintxtline = "";

		protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private readonly object writeLock = new object();
		private readonly object readLock = new object();
		private int pacCount = 0;
		private string plainTextLine = "";
		private byte mavlinkVersion = 0;
		private bool GPSLocationMode = false;
		private ProgressDialogV2 frmProgressReporter;

        #region Core
        public MavCore()
		{
            portUnlockTimer = new Timer { Interval = 5000, AutoReset = false };
            portUnlockTimer.Elapsed += (s, e) =>
            {
                if (PortInUse)
                {
                    Console.WriteLine($"MavCore: port locked too long (called from " +
                        $"{portLockTracer.GetFrame(1).GetMethod().Name} from " +
                        $"{portLockTracer.GetFrame(2).GetMethod().Name}), released.");
                    portInUseLevel = 0;
                }
            };
            RegisterMavMessageHandler(MAVLINK_MSG_ID.HEARTBEAT, HeartBeatPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GLOBAL_POSITION_INT, GPSPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GPS_RAW_INT, GPSRawPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.SYS_STATUS, SysStatusPacketHandler);
        }

        public void Close()
		{
            bgReaderLoop.Cancel();
            try { BaseStream.Close(); } catch { }
        }

		public void Open()
		{
			frmProgressReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				HintImage = Resources.icon_warn,
				Text = "Connection",
			};

			frmProgressReporter.DoWork += OpenBg;
			frmProgressReporter.UpdateProgressAndStatus(-1, "sync...");
			frmProgressReporter.RunBackgroundOperationAsync();
			frmProgressReporter.Dispose();

            bgReaderLoop = BackgroundLoop.Start(SerialReader);
        }

		public void OpenBg(object PRsender, ProgressWorkerEventArgs progressWorkerEventArgs, object param = null)
		{
			frmProgressReporter.UpdateProgressAndStatus(-1, "mavlink connecting");

			PortInUse = true;

			bool hbseen = false;

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
				
				MAVLinkMessage buffer = MAVLinkMessage.Invalid;
				MAVLinkMessage buffer1 = MAVLinkMessage.Invalid;

				DateTime start = DateTime.Now;
				DateTime deadline = start.AddSeconds(CONNECT_TIMEOUT_SECONDS);

				// CountDown timer for connecting.
				var countDown = new Timer { Interval = 1000, AutoReset = false };
				countDown.Elapsed += (sender, e) =>
				{
					int secondsRemaining = (deadline - e.SignalTime).Seconds;
					frmProgressReporter.UpdateProgressAndStatus(-1, string.Format("trying", secondsRemaining));
					if (secondsRemaining > 0) countDown.Start();
				};
				countDown.Start();

				int count = 0;
				while (true)
				{
					if (progressWorkerEventArgs.CancelRequested)
					{
						progressWorkerEventArgs.CancelAcknowledged = true;
						countDown.Stop();
						if (BaseStream.IsOpen)
							BaseStream.Close();
						PortInUse = false;
						return;
					}
					log.Info(DateTime.Now.Millisecond + " Start connect loop ");
					if (DateTime.Now > deadline)
					{
						countDown.Stop();
                        if (BaseStream.IsOpen)
                            BaseStream.Close();
						if (hbseen)
						{
							progressWorkerEventArgs.ErrorMessage = Strings.MsgOnlyOneHBReceived;
							throw new Exception(Strings.MsgOnlyOneHBReceived);
						}
						else
						{
							progressWorkerEventArgs.ErrorMessage = Strings.MsgOnlyOneHBReceived;
							throw new Exception("Can not establish a connection\n");
						}
					}
					Thread.Sleep(1);

                    // since we're having only 1 sysid/compid in this link, no need to check this
					// can see 2 heartbeat packets at any time, and will connect - was one after the other
					if (buffer.Length == 0)
						buffer = GetHeartBeat(true);
                    if (buffer.Length > 0)
                        break;
                    Thread.Sleep(1);
					/*if (buffer1.Length == 0)
						buffer1 = GetHeartBeat();

					if (buffer.Length > 0 || buffer1.Length > 0)
						hbseen = true;*/
					count++;

					// 2 hbs that match
					/*if (buffer.Length > 5 && buffer1.Length > 5 && buffer.sysid == buffer1.sysid && buffer.compid == buffer1.compid)
					{
						mavlink_heartbeat_t hb = buffer.ToStructure<mavlink_heartbeat_t>();

						if (hb.type != (byte)MAV_TYPE.GCS)
						{
							SetupMavConnect(buffer, hb);
							break;
						}
					}

					// 2 hb's that dont match. more than one sysid here
					if (buffer.Length > 5 && buffer1.Length > 5 && (buffer.sysid == buffer1.sysid || buffer.compid == buffer1.compid))
					{
						mavlink_heartbeat_t hb = buffer.ToStructure<mavlink_heartbeat_t>();

						if (hb.type != (byte)MAV_TYPE.ANTENNA_TRACKER && hb.type != (byte)MAV_TYPE.GCS)
						{
							SetupMavConnect(buffer, hb);
							break;
						}

						hb = buffer1.ToStructure<mavlink_heartbeat_t>();

						if (hb.type != (byte)MAV_TYPE.ANTENNA_TRACKER && hb.type != (byte)MAV_TYPE.GCS)
						{
							SetupMavConnect(buffer1, hb);
							break;
						}
					}*/
				}
				countDown.Stop();

				byte[] temp = ASCIIEncoding.ASCII.GetBytes("Diva GCS " + Assembly.GetEntryAssembly().GetName().Version + "\0");
				Array.Resize(ref temp, 50);

				SendPacket(MAVLINK_MSG_ID.STATUSTEXT,
					new mavlink_statustext_t { severity = (byte)MAV_SEVERITY.INFO, text = temp });

				GetHeartBeat();

				GetVersion();

				frmProgressReporter.UpdateProgressAndStatus(0,
					$"Getting Params.. (sysid {SysId} compid {CompId}) ");
				GetParamListBG();

				if (frmProgressReporter.doWorkArgs.CancelAcknowledged == true)
				{
					PortInUse = false;
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
				PortInUse = false;
				if (string.IsNullOrEmpty(progressWorkerEventArgs.ErrorMessage))
					progressWorkerEventArgs.ErrorMessage = Strings.MsgConnectionFailed;
				log.Error(e);
				throw;
			}
			//frmProgressReporter.Close();
			PortInUse = false;
			frmProgressReporter.UpdateProgressAndStatus(100, "done");
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

			lock (readLock)
			{
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
			} // end readlock

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

			byte sysid = message.sysid;
			byte compid = message.compid;
			byte packetSeqNo = message.seq;

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

			if (buffer.Length >= 5)
			{
				GetInfoFromStream(ref message, sysid, compid);
			}

			// if its a gcs packet - dont process further
			if (buffer.Length >= 5 && (sysid == 255 || sysid == 253)) // gcs packet
			{
				return message;
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
					int expectedSeqNo = ((Status.recvpacketcount + 1) % 0x100);

					// the second part is to work around a 3dr radio bug sending dup seqno's
					if (packetSeqNo != expectedSeqNo && packetSeqNo != Status.recvpacketcount)
					{
                        Status.SyncLost++; // actualy sync loss's
						int numLost = 0;
						if (packetSeqNo < ((Status.recvpacketcount + 1)))
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
                    Status.recvpacketcount = packetSeqNo;

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
                    /*lock (writeLock)
					{
                        Status.AddPacket(message);

						// 3dr radio status packet are injected into the current mav
						if (msgid == (byte)MAVLINK_MSG_ID.RADIO_STATUS ||
							msgid == (byte)MAVLINK_MSG_ID.RADIO)
						{
							Status.AddPacket(message);
						}


						if (msgid == (byte)MAVLINK_MSG_ID.COLLISION)
						{
							var coll = message.ToStructure<mavlink_collision_t>();
							var id = coll.id.ToString("X5");
							var coll_type = (MAV_COLLISION_SRC)coll.src;
							var action = (MAV_COLLISION_ACTION)coll.action;
							if (action > MAV_COLLISION_ACTION.REPORT)
							{
								// we are reacting to a threat
							}
							var threat_level = (MAV_COLLISION_THREAT_LEVEL)coll.threat_level;
						}
					}*/

                    // set seens sysid's based on hb packet - this will hide 3dr radio packets
                    /*if (msgid == (byte)MAVLINK_MSG_ID.HEARTBEAT)
					{
						mavlink_heartbeat_t hb = message.ToStructure<mavlink_heartbeat_t>();

						// not a gcs
						if (hb.type != (byte)MAV_TYPE.GCS)
						{
                            if (Status.APType != (MAV_TYPE)hb.type ||
                                    Status.APName != (MAV_AUTOPILOT)hb.autopilot)
    							SetAPType((MAV_TYPE)hb.type, (MAV_AUTOPILOT)hb.autopilot);
						}
					}*/

					// only process for active mav
					//if (sysidcurrent == sysid && compidcurrent == compid)
						//PacketReceived(message);

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

        public void SendPacket(MAVLINK_MSG_ID msgid, object indata)
        {
            int messageType = (byte)msgid;
            byte[] data = MavlinkUtil.StructureToByteArray(indata);
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

            lock (writeLock)
                BaseStream.Write(packet, 0, packet.Length);
        }
        #endregion Core

        #region Background Loop
        private BackgroundLoop bgReaderLoop;
        protected Action bgLoopAction;
        private DateTime lastHeartBeatSent = DateTime.Now;

        private void SerialReader(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				Thread.Sleep(10);

                if (PortInUse) continue;

				if (lastHeartBeatSent.Second != DateTime.Now.Second)
				{
					mavlink_heartbeat_t htb = new mavlink_heartbeat_t
					{
						type = (byte)MAV_TYPE.GCS,
						autopilot = (byte)MAV_AUTOPILOT.INVALID,
						mavlink_version = 3 // MAVLINK_VERSION
					};

					if (!BaseStream.IsOpen) continue;
					SendPacket(MAVLINK_MSG_ID.HEARTBEAT, htb);
	    			lastHeartBeatSent = DateTime.Now;
				}

                UpdateCurrentSettings();
			}

			Console.WriteLine("serialreader done");
		}
        #endregion Background Loop

        #region Retrieve packets from flight control.
        public const int MAX_MAVLINK_MSGID = 256;
        private DateTime nextUpdateTime = DateTime.Now.AddMilliseconds(50);
        private DateTime lastUpdatedTime = DateTime.MinValue;
        private readonly EventHandler<MAVLinkMessage>[] msgMap = new EventHandler<MAVLinkMessage>[MAX_MAVLINK_MSGID];
        protected void RegisterMavMessageHandler(MAVLINK_MSG_ID msgid, EventHandler<MAVLinkMessage> handler)
            => msgMap[(uint)msgid] += handler;
        public class MessageHolder { public object Message = null; };
        protected static T GetMessage<T>(MAVLinkMessage packet, ref object h)
        {
            var mh = h as MessageHolder;
            return (T)(mh.Message ?? (mh.Message = packet.ToStructure<T>()));
        }

        public void UpdateCurrentSettings()
		{
            DateTime timeout = DateTime.Now.AddMilliseconds(10);
            while (BaseStream.IsOpen && !PortInUse &&
                DateTime.Now < timeout && BaseStream.BytesAvailable > 5)
            {
                MAVLinkMessage packet = ReadPacket();
                if (packet.Length == 0) return;
                HandleMavLinkMessage(packet);
            }

            if (DateTime.Now > nextUpdateTime) lock (this)
			{
				nextUpdateTime = DateTime.Now.AddMilliseconds(50);

				// re-request streams
				if (!(lastUpdatedTime.AddSeconds(8) > DateTime.Now) && BaseStream.IsOpen)
				{
					try
					{
						GetDataStream(MAV_DATA_STREAM.EXTENDED_STATUS, 2);
						GetDataStream(MAV_DATA_STREAM.POSITION, 2);
						GetDataStream(MAV_DATA_STREAM.EXTRA1, 4);
						GetDataStream(MAV_DATA_STREAM.EXTRA2, 4);
						GetDataStream(MAV_DATA_STREAM.EXTRA3, 2);
						GetDataStream(MAV_DATA_STREAM.RAW_SENSORS, 2);
						GetDataStream(MAV_DATA_STREAM.RC_CHANNELS, 2);
					}
					catch
					{
						Console.WriteLine("Failed to request rates");
					}
					lastUpdatedTime = DateTime.Now.AddSeconds(30); // prevent flooding
				}
			}
		}

        protected void HandleMavLinkMessage(MAVLinkMessage message)
        {
            if (message != null && message.msgid < MAX_MAVLINK_MSGID)
                msgMap[message.msgid]?.Invoke(new MessageHolder(), message);
        }

        private void HeartBeatPacketHandler(object holder, MAVLinkMessage packet) =>
            GetMessage<mavlink_heartbeat_t>(packet, ref holder);

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
        public void GetParamList()
		{
			log.InfoFormat("getParamList {0} {1}", SysId, CompId);

			frmProgressReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				Text = "Getting Params" + " " + SysId
            };

			frmProgressReporter.DoWork += (o, e, d) => GetParamListBG();
			frmProgressReporter.UpdateProgressAndStatus(-1, "Get params SD");

			frmProgressReporter.RunBackgroundOperationAsync();

			frmProgressReporter.Dispose();

			ParamListChanged?.Invoke(this, null);
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
			SendPacket(MAVLINK_MSG_ID.PARAM_REQUEST_READ, req);

			PortInUse = true;
			DateTime start = DateTime.Now;
			int retrys = 3;

            MAVLinkMessage buffer;
            while (true)
			{
				if (!(start.AddMilliseconds(GET_PARAM_TIMEOUT) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("GetParam Retry " + retrys);
						SendPacket(MAVLINK_MSG_ID.PARAM_REQUEST_READ, req);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					PortInUse = false;
					throw new TimeoutException("Timeout on read - GetParam");
				}

				buffer = ReadPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE &&
                        buffer.sysid == req.target_system &&
                        buffer.compid == req.target_component)
					{
						PortInUse = false;

						mavlink_param_value_t par = buffer.ToStructure<mavlink_param_value_t>();
						string st = ASCIIEncoding.ASCII.GetString(par.param_id);
						int pos = st.IndexOf('\0');
						if (pos != -1) st = st.Substring(0, pos);

						// not the correct id
						if (st != name)
						{
							log.ErrorFormat("Wrong Answer {0} - {1} - {2}    --- '{3}' vs '{4}'", par.param_index,
								ASCIIEncoding.ASCII.GetString(par.param_id), par.param_value,
								ASCIIEncoding.ASCII.GetString(req.param_id).TrimEnd(), st);
							continue;
						}

						// update table
						if (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA)
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                            Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)par.param_type);
						}
						else
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), (MAV_PARAM_TYPE)par.param_type, (MAV_PARAM_TYPE)par.param_type);
						}

						Status.ParamTypes[st] = (MAV_PARAM_TYPE)par.param_type;

						log.Info(DateTime.Now.Millisecond + " got param " + (par.param_index) + " of " +
								 (par.param_count) + " name: " + st);

                        return par.param_value;
					}
				}
			}
		}

        public bool SetParam(string paramname, double value)
        {
            if (!Status.Params.ContainsKey(paramname))
            {
                log.Warn("Trying to set Param that doesnt exist " + paramname + "=" + value);
                return false;
            }

            if (Status.Params[paramname].Value == value)
            {
                log.Warn("setParam " + paramname + " not modified as same");
                return true;
            }

            PortInUse = true;

            // param type is set here, however it is always sent over the air as a float 100int = 100f.
            var req = new mavlink_param_set_t
            {
                target_system = SysId,
                target_component = CompId,
                param_type = (byte)Status.ParamTypes[paramname]
            };

            byte[] temp = Encoding.ASCII.GetBytes(paramname);

            Array.Resize(ref temp, 16);
            req.param_id = temp;
            if (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA)
            {
                req.param_value = new MAVLinkParam(paramname, value, (MAV_PARAM_TYPE.REAL32)).float_value;
            }
            else
            {
                req.param_value = new MAVLinkParam(paramname, value, (MAV_PARAM_TYPE)Status.ParamTypes[paramname]).float_value;
            }

            int currentparamcount = Status.Params.Count;
            SendPacket(MAVLINK_MSG_ID.PARAM_SET, req);
            log.InfoFormat("setParam '{0}' = '{1}' sysid {2} compid {3}",
                paramname, value, SysId, CompId);

            DateTime start = DateTime.Now;
            int retrys = 3;
            while (true)
            {
                if (!(start.AddMilliseconds(700) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("setParam Retry " + retrys);
                        SendPacket(MAVLINK_MSG_ID.PARAM_SET, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - setParam " + paramname);
                }

                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE)
                    {
                        mavlink_param_value_t par = buffer.ToStructure<mavlink_param_value_t>();
                        string st = ASCIIEncoding.ASCII.GetString(par.param_id);
                        int pos = st.IndexOf('\0');
                        if (pos != -1) st = st.Substring(0, pos);

                        if (st != paramname)
                        {
                            log.InfoFormat("MAVLINK bad param response - {0} vs {1}", paramname, st);
                            continue;
                        }

                        if (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA)
                        {
                            var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                            Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)par.param_type);
                        }
                        else
                        {
                            var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                            Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), (MAV_PARAM_TYPE)par.param_type, (MAV_PARAM_TYPE)par.param_type);
                        }
                        log.Info("setParam gotback " + st + " : " + Status.Params[st]);
                        PortInUse = false;
                        return true;
                    }
                }
            }
        }

        public bool SetAnyParam(string[] names, double value) => names.Any(n => SetParam(n, value));

        private Dictionary<string, double> GetParamListBG()
		{
			PortInUse = true;
			List<int> indexsreceived = new List<int>();
			MAVLinkParamList newparamlist = new MAVLinkParamList();
            mavlink_param_request_list_t req = new mavlink_param_request_list_t
            {
                target_system = SysId,
                target_component = CompId
            };

            SendPacket(MAVLINK_MSG_ID.PARAM_REQUEST_LIST, req);

			DateTime start = DateTime.Now;
			DateTime restart = DateTime.Now;
			DateTime lastmessage = DateTime.MinValue;
            DateTime lastonebyone = DateTime.MinValue;

            int param_total = 1;
            int packets = 0;
			int retry = 0;
			bool onebyone = false;

			do
			{
				if (frmProgressReporter.doWorkArgs.CancelRequested)
				{
					frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
					PortInUse = false;
					frmProgressReporter.doWorkArgs.ErrorMessage = "User Canceled";
					return Status.Params;
				}
				if (!(start.AddMilliseconds(4000) > DateTime.Now))
				{
					if (retry < 6)
					{
						retry++;
						SendPacket(MAVLINK_MSG_ID.PARAM_REQUEST_LIST, req);
						start = DateTime.Now;
						continue;
					}
					onebyone = true;
					if (lastonebyone.AddMilliseconds(600) < DateTime.Now)
					{
						log.Info("Get param 1 by 1 - got " + indexsreceived.Count + " of " + param_total);
						int queued = 0;
						for (short i = 0; i <= (param_total - 1); i++)
						{
							if (!indexsreceived.Contains(i))
							{
								if (frmProgressReporter.doWorkArgs.CancelRequested)
								{
									frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
									PortInUse = false;
									frmProgressReporter.doWorkArgs.ErrorMessage = "User Canceled";
									return Status.Params;
								}
								try
								{
									queued++;
                                    mavlink_param_request_read_t req2 = new mavlink_param_request_read_t
                                    {
                                        target_system = SysId,
                                        target_component = CompId,
                                        param_index = i,
                                        param_id = new byte[16]
                                    };
									SendPacket(MAVLINK_MSG_ID.PARAM_REQUEST_READ, req2);
									if (queued >= 10)
									{
										lastonebyone = DateTime.Now;
										break;
									}
								}
								catch (Exception excp)
								{
									log.Info("GetParam Failed index: " + i + " " + excp);
									throw excp;
								}
							}
						}
					}
				}

				MAVLinkMessage buffer = ReadPacket();
				if (buffer.Length > 5)
				{
					packets++;
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE && buffer.sysid == req.target_system && buffer.compid == req.target_component)
					{
						restart = DateTime.Now;
						if (!onebyone)
							start = DateTime.Now;

						mavlink_param_value_t par = buffer.ToStructure<mavlink_param_value_t>();

						// set new target
						param_total = (par.param_count);
						newparamlist.TotalReported = param_total;

						string paramID = ASCIIEncoding.ASCII.GetString(par.param_id);

						int pos = paramID.IndexOf('\0');
						if (pos != -1)
						{
							paramID = paramID.Substring(0, pos);
						}

						if (indexsreceived.Contains(par.param_index))
						{
							continue;
						}
						if (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA)
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							newparamlist[paramID] = new MAVLinkParam(paramID, BitConverter.GetBytes(par.param_value), MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)par.param_type);
						}
						else
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							newparamlist[paramID] = new MAVLinkParam(paramID, BitConverter.GetBytes(par.param_value), (MAV_PARAM_TYPE)par.param_type, (MAV_PARAM_TYPE)par.param_type);
						}
						if (par.param_index != 65535)
							indexsreceived.Add(par.param_index);

						Status.ParamTypes[paramID] = (MAV_PARAM_TYPE)par.param_type;

						this.frmProgressReporter.UpdateProgressAndStatus((indexsreceived.Count * 100) / param_total,
						"get param" + paramID);

						if (par.param_index == (param_total - 1))
							start = DateTime.MinValue;
					}
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.STATUSTEXT)
					{
						var msg = buffer.ToStructure<mavlink_statustext_t>();

						string logdata = Encoding.ASCII.GetString(msg.text);

						int ind = logdata.IndexOf('\0');
						if (ind != -1)
							logdata = logdata.Substring(0, ind);

						if (logdata.ToLower().Contains("copter") || logdata.ToLower().Contains("rover") ||
							logdata.ToLower().Contains("plane"))
						{
							Status.VersionString = logdata;
						}
						else if (logdata.ToLower().Contains("nuttx"))
						{
							Status.SoftwareVersions = logdata;
						}
						else if (logdata.ToLower().Contains("px4v2"))
						{
							Status.SerialString = logdata;
						}
						else if (logdata.ToLower().Contains("frame"))
						{
							Status.FrameString = logdata;
						}
					}
				}
				if (!BaseStream.IsOpen)
				{
					var exp = new Exception("Not Connected");
					throw exp;
				}
			} while (indexsreceived.Count < param_total);

			if (indexsreceived.Count != param_total)
			{
				var exp = new Exception("Missing Params " + indexsreceived.Count + " vs " + param_total);
				throw exp;
			}
			PortInUse = false;

			Status.Params.Clear();
			Status.Params.TotalReported = param_total;
			Status.Params.AddRange(newparamlist);
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
			Status.recvpacketcount = message.seq;
		}

        public MAVLinkMessage GetHeartBeat(bool setup = false)
		{
			PortInUse = true;
			DateTime start = DateTime.Now;
			int readcount = 0;
			while (true)
			{
				MAVLinkMessage buffer = ReadPacket();
				readcount++;
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.HEARTBEAT)
					{
						mavlink_heartbeat_t hb = buffer.ToStructure<mavlink_heartbeat_t>();

						if (hb.type != (byte)MAV_TYPE.GCS)
						{
                            if (setup) SetupMavConnect(buffer, hb);

							PortInUse = false;
							return buffer;
						}
					}
				}
				if (DateTime.Now > start.AddMilliseconds(2200) || readcount > 200) // was 1200 , now 2.2 sec
				{
					PortInUse = false;
					return MAVLinkMessage.Invalid;
				}
			}
		}

		private void GetInfoFromStream(ref MAVLinkMessage buffer, byte sysid, byte compid)
		{
            if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE)
			{
				mavlink_param_value_t value = buffer.ToStructure<mavlink_param_value_t>();

				string st = ASCIIEncoding.ASCII.GetString(value.param_id);

				int pos = st.IndexOf('\0');

				if (pos != -1)
				{
					st = st.Substring(0, pos);
				}

				Status.ParamTypes[st] = (MAV_PARAM_TYPE)value.param_type;

				if (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA && buffer.compid != (byte)MAV_COMPONENT.MAV_COMP_ID_UDP_BRIDGE)
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

                Status.Params.TotalReported = value.param_count;
			}
			else if (buffer.msgid == (byte)MAVLINK_MSG_ID.TIMESYNC)
			{
				Int64 now_ns =
					(Int64)((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds *
							 1000000);

				mavlink_timesync_t tsync = buffer.ToStructure<mavlink_timesync_t>();
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
                            Status.firmware = MavUtlities.Firmwares.ArduPlane;
							break;
						case MAV_TYPE.QUADROTOR:
                            Status.firmware = MavUtlities.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.TRICOPTER:
                            Status.firmware = MavUtlities.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.HEXAROTOR:
                            Status.firmware = MavUtlities.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.OCTOROTOR:
                            Status.firmware = MavUtlities.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.HELICOPTER:
                            Status.firmware = MavUtlities.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.GROUND_ROVER:
                            Status.firmware = MavUtlities.Firmwares.ArduRover;
							break;
						case MAV_TYPE.SUBMARINE:
                            Status.firmware = MavUtlities.Firmwares.ArduSub;
							break;
						case MAV_TYPE.ANTENNA_TRACKER:
                            Status.firmware = MavUtlities.Firmwares.ArduTracker;
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
                            Status.firmware = MavUtlities.Firmwares.ArduPlane;
							break;
					}
					break;
				case MAV_AUTOPILOT.GENERIC:
					switch (type)
					{
						case MAV_TYPE.FIXED_WING:
                            Status.firmware = MavUtlities.Firmwares.Ateryx;
							break;
					}
					break;
				case MAV_AUTOPILOT.PX4:
                    Status.firmware = MavUtlities.Firmwares.PX4;
					break;
				default:
					switch (type)
					{
						case MAV_TYPE.GIMBAL: // storm32 - name 83
                            Status.firmware = MavUtlities.Firmwares.Gymbal;
							break;
					}
					break;
			}
		}

		public bool GetVersion()
		{
            PortInUse = true;
            var req = new mavlink_autopilot_version_request_t
            {
                target_component = CompId,
                target_system = SysId
            };

            // request point
            SendPacket(MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST, req);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(200) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("getVersion Retry " + retrys + " - giv com " + PortInUse);
						SendPacket(MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST, req);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					PortInUse = false;
					return false;
				}

				MAVLinkMessage buffer = ReadPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.AUTOPILOT_VERSION)
					{
						PortInUse = false;
						return true;
					}
				}
			}
		}

		public bool DoCommand(MAV_CMD actionid, float p1, float p2, float p3, float p4, float p5, float p6, float p7)
		{
			PortInUse = true;
			mavlink_command_long_t req = new mavlink_command_long_t
            {
                target_system = SysId,
                target_component = CompId,
                command = (ushort)actionid,
                param1 = p1,
                param2 = p2,
                param3 = p3,
                param4 = p4,
                param5 = p5,
                param6 = p6,
                param7 = p7
            };

			log.InfoFormat("DoCommand cmd {0} {1} {2} {3} {4} {5} {6} {7}", actionid.ToString(), p1, p2, p3, p4, p5, p6,
				p7);

			SendPacket(MAVLINK_MSG_ID.COMMAND_LONG, req);

			DateTime start = DateTime.Now;
			int retrys = 3;
			int timeout = 2000;

			// imu calib take a little while
			if (actionid == MAV_CMD.COMPONENT_ARM_DISARM)
			{
				// 10 seconds as may need an imu calib
				timeout = 10000;
			}

            while (true)
            {
                var buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.COMMAND_ACK &&
                        buffer.sysid == SysId && buffer.compid == CompId)
                    {
                        var ack = buffer.ToStructure<mavlink_command_ack_t>();
                        log.InfoFormat($"DoCommand: ack {((MAV_CMD)ack.command).ToString()}({ack.command}) recieved," +
                            $" result {((MAV_RESULT)ack.result).ToString()}({ack.result})");
                        if (ack.command != req.command)
                        {
                            log.InfoFormat($"DoCommand: ack {ack.command} does not match cmd {req.command}");
                            continue;
                        }
                        PortInUse = false;
                        return ack.result == (byte)MAV_RESULT.ACCEPTED;
                    }
                }
                if (start.AddMilliseconds(timeout) < DateTime.Now)
                {
                    if (retrys > 0)
                    {
                        log.Info("DoCommand retry countdown: " + retrys--);
                        SendPacket(MAVLINK_MSG_ID.COMMAND_LONG, req);
                        start = DateTime.Now;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException($"Timeout on waiting ack from DoCommand {req.command}");
                }
            }
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
