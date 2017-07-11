using FooApplication.Comms;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace FooApplication.Mavlink
{
	public class MavlinkInterface : MAVLink, IDisposable
	{
		public static readonly int SLEEP_TIME_SETMODE = 10;
		public static readonly double CONNECT_TIMEOUT_SECONDS = 30;
		public bool giveComport
		{
			get { return _giveComport; }
			set { _giveComport = value; }
		}


		public DateTime lastlogread { get; set; }
		public BinaryReader logplaybackfile
		{
			get { return _logplaybackfile; }
			set
			{
				_logplaybackfile = value;
				if (_logplaybackfile != null && _logplaybackfile.BaseStream is FileStream)
					log.Info("Logplaybackfile set " + ((FileStream)_logplaybackfile.BaseStream).Name);
			}
		}

		public ICommsSerial BaseStream
		{
			get { return _baseStream; }
			set
			{
				// This is called every time user changes the port selection, so we need to make sure we cleanup
				// any previous objects so we don't leave the cleanup of system resources to the garbage collector.
				if (_baseStream != null)
				{
					try
					{
						if (_baseStream.IsOpen)
						{
							_baseStream.Close();
						}
					}
					catch { }
					IDisposable dsp = _baseStream as IDisposable;
					if (dsp != null)
					{
						try
						{
							dsp.Dispose();
						}
						catch { }
					}
				}
				_baseStream = value;
			}
		}

		public BufferedStream logfile { get; set; }
		public BufferedStream rawlogfile { get; set; }
		public DateTime _bpstime { get; set; }
		public byte sysid { get { return _sysid; } set { _sysid = value; } }
		public byte compid { get { return _compid; } set { _compid = value; } }
		public MavStatus MAV { get { return _mav; } set { _mav = value; } }


		internal string plaintxtline = "";

		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private volatile bool _giveComport = false;
		private volatile object objlock = new object();
		private readonly Subject<int> _bytesReceivedSubj = new Subject<int>();
		private readonly Subject<int> _bytesSentSubj = new Subject<int>();
		private byte[] lastbad = new byte[2];
		private volatile object readlock = new object();
		private int pacCount = 0;
		private const int gcssysid = 255;
		private ICommsSerial _baseStream = null;
		private MavStatus _mav;
		private byte sysidcurrent;
		private byte compidcurrent;
		private BinaryReader _logplaybackfile;
		private string buildplaintxtline = "";
		private byte _sysid = 1;
		private byte _compid = 0;

		public MavlinkInterface()
		{
			_mav = new MavStatus(this, 1, 0);
		}

		public void connect()
		{
		}

		public void disConnect()
		{
		}

		public void close()
		{
		}

		public void openBg(object PRsender, bool getparams)
		{
			// frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MavlinkConnecting);

			giveComport = true;

			if (BaseStream is SerialPort)
			{
				// allow settings to settle - previous dtr 
				Thread.Sleep(1000);
			}

			// Terrain = new TerrainFollow(this);	// TODO Terrain not used now.
			bool hbseen = false;

			try
			{
				BaseStream.ReadBufferSize = 16 * 1024;

				lock (objlock) // so we dont have random traffic
				{
					log.Info("Open port with " + BaseStream.PortName + " " + BaseStream.BaudRate);

					if (BaseStream is UdpSerial)
					{
						// TODO: Read/Write prarmeters future	
						((UdpSerial)BaseStream).CancelConnect = true;
					
					}

					BaseStream.Open();

					BaseStream.DiscardInBuffer();

					// other boards seem to have issues if there is no delay? posible bootloader timeout issue
					if (BaseStream is SerialPort)
					{
						Thread.Sleep(1000);
					}
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
					// frmProgressReporter.UpdateProgressAndStatus(-1, string.Format(Strings.Trying, secondsRemaining));
					if (secondsRemaining > 0) countDown.Start();
				};
				countDown.Start();

				// px4 native
				// BaseStream.WriteLine("sh /etc/init.d/rc.usb");

				int count = 0;

				while (true)
				{
					/**
					if (CancelRequested)
					{
						progressWorkerEventArgs.CancelAcknowledged = true;
						countDown.Stop();
						if (BaseStream.IsOpen)
							BaseStream.Close();
						giveComport = false;
						return;
					} */

					log.Info(DateTime.Now.Millisecond + " Start connect loop ");

					if (DateTime.Now > deadline)
					{
						//if (Progress != null)
						//    Progress(-1, "No Heartbeat Packets");
						countDown.Stop();
						this.close();

						if (hbseen)
						{
							throw new Exception(Strings.Only1HbD);
						}
						else
						{
							throw new Exception("Can not establish a connection\n");
						}
					}

					Thread.Sleep(1);

					// can see 2 heartbeat packets at any time, and will connect - was one after the other

					if (buffer.Length == 0)
						buffer = getHeartBeat();

					Thread.Sleep(1);

					if (buffer1.Length == 0)
						buffer1 = getHeartBeat();


					if (buffer.Length > 0 || buffer1.Length > 0)
						hbseen = true;

					count++;

					// 2 hbs that match
					if (buffer.Length > 5 && buffer1.Length > 5 && buffer.sysid == buffer1.sysid && buffer.compid == buffer1.compid)
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
					}
				}

				countDown.Stop();

				byte[] temp = ASCIIEncoding.ASCII.GetBytes("Mission Planner " + getAppVersion() + "\0");
				Array.Resize(ref temp, 50);
				// 
				generatePacket((byte)MAVLINK_MSG_ID.STATUSTEXT,
					new mavlink_statustext_t() { severity = (byte)MAV_SEVERITY.INFO, text = temp }, sysid, compid);
				// mavlink2
				generatePacket((byte)MAVLINK_MSG_ID.STATUSTEXT,
					new mavlink_statustext_t() { severity = (byte)MAV_SEVERITY.INFO, text = temp }, sysidcurrent,
					compidcurrent, true, true);

				// this ensures a mavlink2 change has been noticed
				getHeartBeat();

				getVersion();

				/**
				if (getparams)
				{
					frmProgressReporter.UpdateProgressAndStatus(0,
						"Getting Params.. (sysid " + MAV.sysid + " compid " + MAV.compid + ") ");

					getParamListBG();
				}

				if (frmProgressReporter.doWorkArgs.CancelAcknowledged == true)
				{
					giveComport = false;
					if (BaseStream.IsOpen)
						BaseStream.Close();
					return;
				} */
			}
			catch (Exception e)
			{
				try
				{
					BaseStream.Close();
				}
				catch {	}
				giveComport = false;
				log.Error(e);
				throw;
			}
			//frmProgressReporter.Close();
			giveComport = false;
			// frmProgressReporter.UpdateProgressAndStatus(100, Strings.Done);
			log.Info("Done open " + MAV.sysid + " " + MAV.compid);
			MAV.packetslost = 0;
			MAV.synclost = 0;
		}

		private string getAppVersion()
		{
			try
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				if (entryAssembly != null)
				{
					object[] customAttributes =
						entryAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						return ((AssemblyFileVersionAttribute)customAttributes[0]).Version;
						;
					}
				}
			}
			catch { }

			return "0.0";
		}

		void SetupMavConnect(MAVLinkMessage message, mavlink_heartbeat_t hb)
		{
			sysidcurrent = message.sysid;
			compidcurrent = message.compid;
			MAV.sysid = message.sysid;
			MAV.compid = message.compid;

			// TODO: get parameters future.
			/**
			mavlinkversion = hb.mavlink_version;
			MAV.aptype = (MAV_TYPE)hb.type;
			MAV.apname = (MAV_AUTOPILOT)hb.autopilot;

			setAPType(message.sysid, message.compid);

			MAV.sysid = message.sysid;
			MAV.compid = message.compid;
			MAV.recvpacketcount = message.seq;
			log.InfoFormat("ID sys {0} comp {1} ver{2} type {3} name {4}", MAV.sysid, MAV.compid, mavlinkversion,
				MAV.aptype.ToString(), MAV.apname.ToString()); */
		}

		public MAVLinkMessage readPacket()
		{
			byte[] buffer = new byte[MAVLINK_MAX_PACKET_LEN + 25];
			int count = 0;
			int length = 0;
			int readcount = 0;
			MAVLinkMessage message = null;

			BaseStream.ReadTimeout = 1200; // 1200 ms between chars - the gps detection requires this.

			DateTime start = DateTime.Now;
			

			lock (readlock)
			{
				lastbad = new byte[2];


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
						// MAV.cs.datetime = DateTime.Now;

						DateTime to = DateTime.Now.AddMilliseconds(BaseStream.ReadTimeout);

						// Console.WriteLine(DateTime.Now.Millisecond + " SR1a " + BaseStream.BytesToRead);

						while (BaseStream.IsOpen && BaseStream.BytesToRead <= 0)
						{
							if (DateTime.Now > to)
							{
								log.InfoFormat("MAVLINK: 1 wait time out btr {0} len {1}", BaseStream.BytesToRead,
									length);
								throw new TimeoutException("Timeout");
							}
							Thread.Sleep(1);
						}
						
						if (BaseStream.IsOpen)
						{
							BaseStream.Read(buffer, count, 1);
							if (rawlogfile != null && rawlogfile.CanWrite)
								rawlogfile.WriteByte(buffer[count]);
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
								if (buildplaintxtline.Length > 3)
									plaintxtline = buildplaintxtline;

								log.Info(plaintxtline);
								// reset for next line
								buildplaintxtline = "";
							}

							// TCPConsole.Write(buffer[0]);
							Console.Write((char)buffer[0]);
							buildplaintxtline += (char)buffer[0];
						}
						_bytesReceivedSubj.OnNext(1);
						count = 0;
						lastbad[0] = lastbad[1];
						lastbad[1] = buffer[0];
						buffer[1] = 0;
						continue;
					}
					// reset count on valid packet
					readcount = 0;

					//Console.WriteLine(DateTime.Now.Millisecond + " SR2 " + BaseStream.BytesToRead);

					// check for a header
					if (buffer[0] == 0xfe || buffer[0] == 0xfd || buffer[0] == 'U')
					{
						var mavlinkv2 = buffer[0] == MAVLINK_STX ? true : false;

						int headerlength = mavlinkv2 ? MAVLINK_CORE_HEADER_LEN : MAVLINK_CORE_HEADER_MAVLINK1_LEN;
						int headerlengthstx = headerlength + 1;

						// if we have the header, and no other chars, get the length and packet identifiers
						if (count == 0)
						{
							DateTime to = DateTime.Now.AddMilliseconds(BaseStream.ReadTimeout);

							while (BaseStream.IsOpen && BaseStream.BytesToRead < headerlength)
							{
								if (DateTime.Now > to)
								{
									log.InfoFormat("MAVLINK: 2 wait time out btr {0} len {1}", BaseStream.BytesToRead,
										length);
									throw new TimeoutException("Timeout");
								}
								Thread.Sleep(1);
							}
							int read = BaseStream.Read(buffer, 1, headerlength);
							count = read;
							if (rawlogfile != null && rawlogfile.CanWrite)
								rawlogfile.Write(buffer, 1, read);
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

								while (BaseStream.IsOpen && BaseStream.BytesToRead < (length - (headerlengthstx)))
								{
									if (DateTime.Now > to)
									{
										log.InfoFormat("MAVLINK: 3 wait time out btr {0} len {1}",
											BaseStream.BytesToRead, length);
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
									if (rawlogfile != null && rawlogfile.CanWrite)
									{
										// write only what we read, temp is the whole packet, so 6-end
										rawlogfile.Write(buffer, headerlengthstx, read);
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
			Array.Resize<byte>(ref buffer, count);

			// add byte count
			_bytesReceivedSubj.OnNext(buffer.Length);

			// update bps statistics
			if (_bpstime.Second != DateTime.Now.Second)
			{
				long btr = 0;
				if (BaseStream != null && BaseStream.IsOpen)
				{
					btr = BaseStream.BytesToRead;
				}
			}

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
					log.InfoFormat("unknown packet type {0}", message.msgid);
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

			return message;
		}


		public MAVLinkMessage getHeartBeat()
		{
			giveComport = true;
			DateTime start = DateTime.Now;
			int readcount = 0;
			while (true)
			{
				MAVLinkMessage buffer = readPacket();
				readcount++;
				if (buffer.Length > 5)
				{
					//log.Info("getHB packet received: " + buffer.Length + " btr " + BaseStream.BytesToRead + " type " + buffer.msgid );
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.HEARTBEAT)
					{
						mavlink_heartbeat_t hb = buffer.ToStructure<mavlink_heartbeat_t>();

						if (hb.type != (byte)MAV_TYPE.GCS)
						{
							SetupMavConnect(buffer, hb);

							giveComport = false;
							return buffer;
						}
					}
				}
				if (DateTime.Now > start.AddMilliseconds(2200) || readcount > 200) // was 1200 , now 2.2 sec
				{
					giveComport = false;
					return MAVLinkMessage.Invalid;
				}
			}
		}

		public void sendPacket(object indata, int sysid, int compid)
		{
			bool validPacket = false;
			foreach (var ty in MAVLINK_MESSAGE_INFOS)
			{
				if (ty.type == indata.GetType())
				{
					validPacket = true;
					generatePacket((int)ty.msgid, indata, (byte)sysid, (byte)compid);
					return;
				}
			}
			if (!validPacket)
			{
				log.Info("Mavlink : NOT VALID PACKET sendPacket() " + indata.GetType().ToString());
			}
		}

		public bool getVersion()
		{
			mavlink_autopilot_version_request_t req = new mavlink_autopilot_version_request_t();

			req.target_component = MAV.compid;
			req.target_system = MAV.sysid;

			// request point
			generatePacket((byte)MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST, req, sysid, compid);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(200) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("getVersion Retry " + retrys + " - giv com " + giveComport);
						generatePacket((byte)MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST, req, sysid, compid);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					return false;
				}

				MAVLinkMessage buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.AUTOPILOT_VERSION)
					{
						giveComport = false;

						return true;
					}
				}
			}
		}

		private void generatePacket(MAVLINK_MSG_ID messageType, object indata, byte sysid, byte compid)
		{
			generatePacket((int)messageType, indata, sysid, compid);
		}

		public void generatePacket(int messageType, object indata, byte sysid, byte compid)
		{
			byte[] data = MavlinkUtil.StructureToByteArray(indata);
			var info = MAVLink.MAVLINK_MESSAGE_INFOS.SingleOrDefault(p => p.msgid == messageType);
			if (data.Length != info.minlength)
			{
				Array.Resize(ref data, (int)info.minlength);
			}

			byte[] packet = new byte[data.Length + 6 + 2];

			packet[0] = MAVLink.MAVLINK_STX_MAVLINK1;
			packet[1] = (byte)data.Length;
			packet[2] = (byte)++pacCount;
			packet[3] = gcssysid;
			packet[4] = (byte)MAVLink.MAV_COMPONENT.MAV_COMP_ID_MISSIONPLANNER;
			packet[5] = (byte)messageType;

			int i = 6;
			foreach (byte b in data)
			{
				packet[i] = b;
				i++;
			}

			ushort checksum = MAVLink.MavlinkCRC.crc_calculate(packet, packet[1] + 6);
			checksum = MAVLink.MavlinkCRC.crc_accumulate(MAVLink.MAVLINK_MESSAGE_INFOS.GetMessageInfo((uint)messageType).crc, checksum);

			byte ck_a = (byte)(checksum & 0xFF);
			byte ck_b = (byte)(checksum >> 8);

			packet[i] = ck_a;
			i += 1;
			packet[i] = ck_b;
			i += 1;

			BaseStream.Write(packet, 0, packet.Length);
		}


		public bool doARM(bool armit)
		{
			return doARM(MAV.sysid, MAV.compid, armit);
		}

		public bool doARM(byte sysid, byte compid, bool armit)
		{
			return doCommand(sysid, compid, MAV_CMD.COMPONENT_ARM_DISARM, armit ? 1 : 0, 21196, 0, 0, 0, 0, 0);
		}

		public bool doAbortLand()
		{
			return doCommand(MAV_CMD.DO_GO_AROUND, 0, 0, 0, 0, 0, 0, 0);
		}

		public bool doMotorTest(int motor, MOTOR_TEST_THROTTLE_TYPE thr_type, int throttle, int timeout, int motorcount = 0)
		{
			return doCommand(MAV_CMD.DO_MOTOR_TEST, (float)motor, (float)(byte)thr_type,
				(float)throttle, (float)timeout, (float)motorcount, 0, 0);
		}

		public bool doCommand(MAV_CMD actionid, float p1, float p2, float p3, float p4, float p5, float p6, float p7,
			bool requireack = true)
		{
			return doCommand(MAV.sysid, MAV.compid, actionid, p1, p2, p3, p4, p5, p6, p7, requireack, null);
		}

		public bool doCommand(byte sysid, byte compid, MAV_CMD actionid, float p1, float p2, float p3, float p4, float p5, float p6, float p7, bool requireack = true, MethodInvoker uicallback = null)
		{
			giveComport = true;
			MAVLinkMessage buffer;

			mavlink_command_long_t req = new mavlink_command_long_t();

			req.target_system = sysid;
			req.target_component = compid;

			req.command = (ushort)actionid;

			req.param1 = p1;
			req.param2 = p2;
			req.param3 = p3;
			req.param4 = p4;
			req.param5 = p5;
			req.param6 = p6;
			req.param7 = p7;

			log.InfoFormat("doCommand cmd {0} {1} {2} {3} {4} {5} {6} {7}", actionid.ToString(), p1, p2, p3, p4, p5, p6,
				p7);

			generatePacket((byte)MAVLINK_MSG_ID.COMMAND_LONG, req, sysid, compid);

			if (!requireack)
			{
				giveComport = false;
				return true;
			}

			DateTime GUI = DateTime.Now;

			DateTime start = DateTime.Now;
			int retrys = 3;

			int timeout = 2000;

			// imu calib take a little while
			if (actionid == MAV_CMD.PREFLIGHT_CALIBRATION && p5 == 1)
			{
				// this is for advanced accel offsets, and blocks execution
				giveComport = false;
				return true;
			}
			else if (actionid == MAV_CMD.PREFLIGHT_CALIBRATION)
			{
				retrys = 1;
				timeout = 25000;
			}
			else if (actionid == MAV_CMD.PREFLIGHT_REBOOT_SHUTDOWN)
			{
				generatePacket((byte)MAVLINK_MSG_ID.COMMAND_LONG, req, sysid, compid);
				giveComport = false;
				return true;
			}
			else if (actionid == MAV_CMD.COMPONENT_ARM_DISARM)
			{
				// 10 seconds as may need an imu calib
				timeout = 10000;
			}
			else if (actionid == MAV_CMD.PREFLIGHT_CALIBRATION && p6 == 1)
			{
				// compassmot
				// send again just incase
				generatePacket((byte)MAVLINK_MSG_ID.COMMAND_LONG, req, sysid, compid);
				giveComport = false;
				return true;
			}
			else if (actionid == MAV_CMD.GET_HOME_POSITION)
			{
				giveComport = false;
				return true;
			}

			while (true)
			{
				if (DateTime.Now > GUI.AddMilliseconds(100))
				{
					GUI = DateTime.Now;

					uicallback?.Invoke();
				}

				if (!(start.AddMilliseconds(timeout) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("doCommand Retry " + retrys);
						generatePacket((byte)MAVLINK_MSG_ID.COMMAND_LONG, req, sysid, compid);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - doCommand");
				}

				buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.COMMAND_ACK && buffer.sysid == sysid && buffer.compid == compid)
					{
						var ack = buffer.ToStructure<mavlink_command_ack_t>();

						if (ack.command != req.command)
						{
							log.InfoFormat("doCommand cmd resp {0} - {1} - Commands dont match", (MAV_CMD)ack.command, (MAV_RESULT)ack.result);
							continue;
						}

						log.InfoFormat("doCommand cmd resp {0} - {1}", (MAV_CMD)ack.command, (MAV_RESULT)ack.result);

						if (ack.result == (byte)MAV_RESULT.ACCEPTED)
						{
							giveComport = false;
							return true;
						}
						else
						{
							giveComport = false;
							return false;
						}
					}
				}
			}
		}

		public void SendAck()
		{
			mavlink_command_ack_t ack = new mavlink_command_ack_t();
			ack.command = (ushort)MAV_CMD.PREFLIGHT_CALIBRATION;
			ack.result = 0;

			// send twice
			generatePacket(MAVLINK_MSG_ID.COMMAND_ACK, ack, sysid, compid);
			Thread.Sleep(20);
			generatePacket(MAVLINK_MSG_ID.COMMAND_ACK, ack, sysid, compid);
		}

		public void setMode(byte sysid, byte compid, MAVLink.mavlink_set_mode_t mode, MAVLink.MAV_MODE_FLAG base_mode = 0)
		{
			mode.base_mode |= (byte)base_mode;
			generatePacket((byte)(byte)MAVLink.MAVLINK_MSG_ID.SET_MODE, mode, sysid, compid);
			System.Threading.Thread.Sleep(SLEEP_TIME_SETMODE);
			generatePacket((byte)(byte)MAVLink.MAVLINK_MSG_ID.SET_MODE, mode, sysid, compid);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
