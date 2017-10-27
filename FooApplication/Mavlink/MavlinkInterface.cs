using FooApplication.Comms;
using FooApplication.Utilities;
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

		public Subject<int> WhenPacketLost { get; set; }
		public Subject<int> WhenPacketReceived { get; set; }

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

		public MavList MAVlist;

		public MavStatus MAV
		{
			get { return MAVlist[sysidcurrent, compidcurrent]; }
			set { MAVlist[sysidcurrent, compidcurrent] = value; }
		}

		public event EventHandler MavChanged;

		int _sysidcurrent = 0;

		public int sysidcurrent
		{
			get { return _sysidcurrent; }
			set
			{
				if (_sysidcurrent == value)
					return;
				_sysidcurrent = value;
				if (MavChanged != null) MavChanged(this, null);
			}
		}

		int _compidcurrent = 0;

		public int compidcurrent
		{
			get { return _compidcurrent; }
			set
			{
				if (_compidcurrent == value)
					return;
				_compidcurrent = value;
				if (MavChanged != null) MavChanged(this, null);
			}
		}


		public BufferedStream logfile { get; set; }
		public BufferedStream rawlogfile { get; set; }
		public DateTime _bpstime { get; set; }
		


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
		private BinaryReader _logplaybackfile;
		private string buildplaintxtline = "";
		private byte mavlinkversion = 0;
		private int _mavlink1count = 0;
		private int _mavlink2count = 0;
		private int _mavlink2signed = 0;
		private int _bps1 = 0;
		private int _bps2 = 0;
		private bool useLocation = false;

		// threading
		private bool threadRunnable = false;
		private Thread SerialReaderThread = null;

		// ticker
		private DateTime heartbeatSend = DateTime.Now;
		private DateTime lastupdate = DateTime.Now;
		private DateTime lastdata = DateTime.MinValue;

		public MavlinkInterface()
		{
			MAVlist = new MavList(this);
			this.BaseStream = new SerialPort();
			this.WhenPacketLost = new Subject<int>();
			this.WhenPacketReceived = new Subject<int>();
		}


		private void SerialReader()
		{
			if (threadRunnable == true)
				return;

			threadRunnable = true;


			while (threadRunnable)
			{
				Thread.Sleep(10);

				if (heartbeatSend.Second != DateTime.Now.Second)
				{
					MAVLink.mavlink_heartbeat_t htb = new MAVLink.mavlink_heartbeat_t()
					{
						type = (byte)MAVLink.MAV_TYPE.GCS,
						autopilot = (byte)MAVLink.MAV_AUTOPILOT.INVALID,
						mavlink_version = 3 // MAVLink.MAVLINK_VERSION
					};

					if (!BaseStream.IsOpen) continue;
					sendPacket(htb, MAV.sysid, MAV.compid);

				}

				heartbeatSend = DateTime.Now;

				if (!BaseStream.IsOpen || giveComport == true)
				{
					if (!BaseStream.IsOpen)
					{
						System.Threading.Thread.Sleep(1000);
					}
				}

				UpdateCurrentSettings(true);
				//updateMapPosition(new PointLatLng(current_lat, current_lng));

				// gMapControl1.HoldInvalidation = true;

				//updateRoutePosition();

			}

			Console.WriteLine("serialreader done");

		}

		public void UpdateCurrentSettings(bool updatenow)
		{
			MAVLink.MAVLinkMessage mavlinkMessage = readPacket();

			lock (this)
			{
				if (DateTime.Now > lastupdate.AddMilliseconds(50) || updatenow) // 20 hz
				{
					lastupdate = DateTime.Now;


					// re-request streams
					if (!(lastdata.AddSeconds(8) > DateTime.Now) && BaseStream.IsOpen)
					{
						try
						{
							getDatastream(1, 0, MAVLink.MAV_DATA_STREAM.EXTENDED_STATUS, 2);
							getDatastream(1, 0, MAVLink.MAV_DATA_STREAM.POSITION, 2);
							getDatastream(1, 0, MAVLink.MAV_DATA_STREAM.EXTRA1, 4);
							getDatastream(1, 0, MAVLink.MAV_DATA_STREAM.EXTRA2, 4);
							getDatastream(1, 0, MAVLink.MAV_DATA_STREAM.EXTRA3, 2);
							getDatastream(1, 0, MAVLink.MAV_DATA_STREAM.RAW_SENSORS, 2);
							getDatastream(1, 0, MAVLink.MAV_DATA_STREAM.RC_CHANNELS, 2);
						}
						catch
						{
							Console.WriteLine("Failed to request rates");
						}
						lastdata = DateTime.Now.AddSeconds(30); // prevent flooding
					}

					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.HEARTBEAT))
					{
						if (mavlinkMessage != null)
						{
							var hb = mavlinkMessage.ToStructure<MAVLink.mavlink_heartbeat_t>();

							// Console.WriteLine("hb_sys_status: " + hb.system_status);
							MAV.mode = hb.custom_mode;
							MAV.sys_status = hb.system_status;

							if (hb.type == (byte)MAVLink.MAV_TYPE.GCS)
							{
								// TODO: do something when recived GCS hb
							}
						}
					}

					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.GLOBAL_POSITION_INT))
					{
						if (mavlinkMessage != null)
						{
							var loc = mavlinkMessage.ToStructure<MAVLink.mavlink_global_position_int_t>();

							// the new arhs deadreckoning may send 0 alt and 0 long. check for and undo

							float alt = loc.relative_alt / 1000.0f;

							bool useLocation = true;
							if (loc.lat == 0 && loc.lon == 0)
							{
								useLocation = false;
							}
							else
							{
								double lat = loc.lat / 10000000.0;
								double lng = loc.lon / 10000000.0;

								MAV.current_lat = lat;
								MAV.current_lng = lng;

								double altasl = loc.alt / 1000.0f;

								double vx = loc.vx * 0.01;
								double vy = loc.vy * 0.01;
								double vz = loc.vz * 0.01;
							}
						}
					}

					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.ATTITUDE))
					{
						if (mavlinkMessage != null)
						{
							var att = mavlinkMessage.ToStructure<MAVLink.mavlink_attitude_t>();

							float roll = (float)(att.roll * MathHelper.rad2deg);
							float pitch = (float)(att.pitch * MathHelper.rad2deg);
							float yaw = (float)(att.yaw * MathHelper.rad2deg);

							MAV.yaw = yaw;
						}
					}

					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.SYS_STATUS))
					{
						if (mavlinkMessage != null)
						{
							var sysstatus = mavlinkMessage.ToStructure<MAVLink.mavlink_sys_status_t>();

							float load = (float)sysstatus.load / 10.0f;

							float battery_voltage = (float)sysstatus.voltage_battery / 1000.0f;

							byte battery_remaining = sysstatus.battery_remaining;
							float current = (float)sysstatus.current_battery / 100.0f;

							ushort packetdropremote = sysstatus.drop_rate_comm;

							MAV.battery_voltage = battery_voltage;

							/*
							Invoke((MethodInvoker)delegate
							{
								this.ts_lbl_battery.Text = battery_voltage.ToString() + "%";
							});*/
						}
					}


					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.GPS_RAW_INT))
					{
						if (mavlinkMessage != null)
						{
							var gps = mavlinkMessage.ToStructure<MAVLink.mavlink_gps_raw_int_t>();

							if (!useLocation)
							{
								double lat = gps.lat * 1.0e-7;
								double lng = gps.lon * 1.0e-7;

								double altasl = gps.alt / 1000.0f;
								// alt = gps.alt; // using vfr as includes baro calc

								MAV.altasl = altasl;
								/*
								Invoke((MethodInvoker)delegate
								{

									this.Gauge_alt.Value = (float)altasl;
									this.lbl_alt.Text = altasl.ToString();
								});*/

							}

							byte gpsstatus = gps.fix_type;

							float gpshdop = (float)Math.Round((double)gps.eph / 100.0, 2);

							byte satcount = gps.satellites_visible;

							float groundspeed = gps.vel * 1.0e-2f;
							float groundcourse = gps.cog * 1.0e-2f;

							/*
							Invoke((MethodInvoker)delegate
							{
								this.Gauge_speed.Value = groundspeed;
								this.lbl_speed.Text = groundspeed.ToString();
							});*/

							MAV.groundspeed = groundspeed;
							MAV.groundcourse = groundcourse;
							//MAVLink.packets[(byte)MAVLink.MSG_NAMES.GPS_RAW);
						}
					}


					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.NAV_CONTROLLER_OUTPUT))
					{
						if (mavlinkMessage != null)
						{
							var nav = mavlinkMessage.ToStructure<MAVLink.mavlink_nav_controller_output_t>();

							float nav_roll = nav.nav_roll;
							float nav_pitch = nav.nav_pitch;
							short nav_bearing = nav.nav_bearing;
							short target_bearing = nav.target_bearing;
							ushort wp_dist = nav.wp_dist;
							float alt_error = nav.alt_error;
							float aspd_error = nav.aspd_error / 100.0f;
							float xtrack_error = nav.xtrack_error;

							MAV.nav_bearing = nav_bearing;

						}
					}

				}
			}
		}


		/// <summary>
		/// Called when object was created
		/// </summary>
		/// <returns></returns>
		public void onCreate()
		{
			SerialReaderThread = new Thread(SerialReader)
			{
				IsBackground = true,
				Name = "mav serial reader",
				// Priority = ThreadPriority.AboveNormal
			};
			SerialReaderThread.Start();
		}


		/// <summary>
		/// Called when object was not used
		/// </summary>
		/// <returns></returns>
		public void onDestroy()
		{
			threadRunnable = false;
			if (SerialReaderThread != null)
				SerialReaderThread.Join();

			close();
		}

		public void open()
		{
			MAVlist.Clear();
			openBg(false);
			log.Info("connection establish");
		}

		public void close()
		{
			try
			{
				if (BaseStream.IsOpen)
					BaseStream.Close();
			}
			catch
			{
			}
		}

		public void openBg(bool getparams)
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
						Console.WriteLine("2 hbs that match");
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
						Console.WriteLine("2 hbs that dont match. more than one sysid here");
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
					new mavlink_statustext_t() { severity = (byte)MAV_SEVERITY.INFO, text = temp });
				// mavlink2
				/**
				generatePacket((byte)MAVLINK_MSG_ID.STATUSTEXT,
					new mavlink_statustext_t() { severity = (byte)MAV_SEVERITY.INFO, text = temp }, sysidcurrent,
					compidcurrent, true, true); */

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

		public List<PointLatLngAlt> getRallyPoints()
		{
			List<PointLatLngAlt> points = new List<PointLatLngAlt>();

			if (!MAV.param.ContainsKey("RALLY_TOTAL"))
				return points;

			int count = int.Parse(MAV.param["RALLY_TOTAL"].ToString());

			for (int a = 0; a < (count - 1); a++)
			{
				try
				{
					PointLatLngAlt plla = getRallyPoint(a, ref count);
					points.Add(plla);
				}
				catch
				{
					return points;
				}
			}

			return points;
		}

		public PointLatLngAlt getRallyPoint(int no, ref int total)
		{
			MAVLinkMessage buffer;

			giveComport = true;

			PointLatLngAlt plla = new PointLatLngAlt();
			mavlink_rally_fetch_point_t req = new mavlink_rally_fetch_point_t();

			req.idx = (byte)no;
			req.target_component = MAV.compid;
			req.target_system = MAV.sysid;

			// request point
			generatePacket((byte)MAVLINK_MSG_ID.RALLY_FETCH_POINT, req);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(700) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("getRallyPoint Retry " + retrys + " - giv com " + giveComport);
						generatePacket((byte)MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - getRallyPoint");
				}

				buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.RALLY_POINT)
					{
						mavlink_rally_point_t fp = buffer.ToStructure<mavlink_rally_point_t>();

						if (req.idx != fp.idx)
						{
							generatePacket((byte)MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);
							continue;
						}

						plla.Lat = fp.lat / 1.0e7;
						plla.Lng = fp.lng / 1.0e7;
						plla.Tag = fp.idx.ToString();
						plla.Alt = fp.alt;

						total = fp.count;

						giveComport = false;

						return plla;
					}
				}
			}
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

			mavlinkversion = hb.mavlink_version;
			MAV.aptype = (MAV_TYPE)hb.type;
			MAV.apname = (MAV_AUTOPILOT)hb.autopilot;

			// for different firmwares.
			// setAPType(message.sysid, message.compid);

			MAV.sysid = message.sysid;
			MAV.compid = message.compid;
			MAV.recvpacketcount = message.seq;

			Console.WriteLine("mav_sysid: " + MAV.sysid);
		}

		/// <summary>
		/// Serial Reader to read mavlink packets. POLL method
		/// </summary>
		/// <returns></returns>
		public MAVLinkMessage readPacket()
		{
			byte[] buffer = new byte[MAVLINK_MAX_PACKET_LEN + 25];
			int count = 0;
			int length = 0;
			int readcount = 0;
			MAVLinkMessage message = null;

			BaseStream.ReadTimeout = 1200; // 1200 ms between chars - the gps detection requires this.

			DateTime start = DateTime.Now;

			//Console.WriteLine(DateTime.Now.Millisecond + " SR0 " + BaseStream.BytesToRead);

			lock (readlock)
			{
				lastbad = new byte[2];

				//Console.WriteLine(DateTime.Now.Millisecond + " SR1 " + BaseStream.BytesToRead);

				while (BaseStream.IsOpen)
				{
					try
					{
						if (readcount > 300)
						{
							break;
						}
						readcount++;
						

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
							//Console.WriteLine(DateTime.Now.Millisecond + " SR0b " + BaseStream.BytesToRead);
						}
						//Console.WriteLine(DateTime.Now.Millisecond + " SR1a " + BaseStream.BytesToRead);
						if (BaseStream.IsOpen)
						{
							BaseStream.Read(buffer, count, 1);
							if (rawlogfile != null && rawlogfile.CanWrite)
								rawlogfile.WriteByte(buffer[count]);
						}
						//Console.WriteLine(DateTime.Now.Millisecond + " SR1b " + BaseStream.BytesToRead);
						
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

				//Console.WriteLine(DateTime.Now.Millisecond + " SR3 " + BaseStream.BytesToRead);
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
			
				_bps2 = _bps1; // prev sec
				_bps1 = 0; // current sec
				_bpstime = DateTime.Now;
				_mavlink1count = 0;
				_mavlink2count = 0;
				_mavlink2signed = 0;
			}

			_bps1 += buffer.Length;

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


			byte sysid = message.sysid;
			byte compid = message.compid;
			byte packetSeqNo = message.seq;

			// create a state for any sysid/compid includes gcs on log playback
			if (!MAVlist.Contains(sysid, compid))
			{
				// create an item - hidden
				MAVlist.AddHiddenList(sysid, compid);
				// prevent packetloss counter on connect
				MAVlist[sysid, compid].recvpacketcount = unchecked(packetSeqNo - (byte)1);
			}

			// once set it cannot be reverted
			if (!MAVlist[sysid, compid].mavlinkv2)
				MAVlist[sysid, compid].mavlinkv2 = message.buffer[0] == MAVLINK_STX ? true : false;

			// stat count
			if (message.buffer[0] == MAVLINK_STX)
				_mavlink2count++;
			else if (message.buffer[0] == MAVLINK_STX_MAVLINK1)
				_mavlink1count++;


			// if its a gcs packet - dont process further
			if (buffer.Length >= 5 && (sysid == 255 || sysid == 253)) // gcs packet
			{
				return message;
			}

			// update packet loss statistics
			if (MAVlist[sysid, compid].packetlosttimer.AddSeconds(5) < DateTime.Now)
			{
				MAVlist[sysid, compid].packetlosttimer = DateTime.Now;
				MAVlist[sysid, compid].packetslost = (MAVlist[sysid, compid].packetslost * 0.8f);
				MAVlist[sysid, compid].packetsnotlost = (MAVlist[sysid, compid].packetsnotlost * 0.8f);
			}
		

			try
			{
				if ((message.header == 'U' || message.header == 0xfe || message.header == 0xfd) && buffer.Length >= message.payloadlength)
				{
					// check if we lost pacakets based on seqno
					int expectedPacketSeqNo = ((MAVlist[sysid, compid].recvpacketcount + 1) % 0x100);

					{
						// the second part is to work around a 3dr radio bug sending dup seqno's
						if (packetSeqNo != expectedPacketSeqNo && packetSeqNo != MAVlist[sysid, compid].recvpacketcount)
						{
							MAVlist[sysid, compid].synclost++; // actualy sync loss's
							int numLost = 0;

							if (packetSeqNo < ((MAVlist[sysid, compid].recvpacketcount + 1)))
							// recvpacketcount = 255 then   10 < 256 = true if was % 0x100 this would fail
							{
								numLost = 0x100 - expectedPacketSeqNo + packetSeqNo;
							}
							else
							{
								numLost = packetSeqNo - expectedPacketSeqNo;
							}

							MAVlist[sysid, compid].packetslost += numLost;
							WhenPacketLost.OnNext(numLost);

			
						}

						MAVlist[sysid, compid].packetsnotlost++;

						//Console.WriteLine("{0} {1}", sysid, packetSeqNo);

						MAVlist[sysid, compid].recvpacketcount = packetSeqNo;
					}
					WhenPacketReceived.OnNext(1);

					// packet stats per mav
					if (!MAVlist[sysid, compid].packetspersecond.ContainsKey(msgid) || double.IsInfinity(MAVlist[sysid, compid].packetspersecond[msgid]))
						MAVlist[sysid, compid].packetspersecond[msgid] = 0;
					if (!MAVlist[sysid, compid].packetspersecondbuild.ContainsKey(msgid))
						MAVlist[sysid, compid].packetspersecondbuild[msgid] = DateTime.Now;

					MAVlist[sysid, compid].packetspersecond[msgid] = (((1000 /
																			((DateTime.Now -
																			  MAVlist[sysid, compid]
																				  .packetspersecondbuild[msgid])
																				.TotalMilliseconds) +
																			MAVlist[sysid, compid].packetspersecond[
																				msgid]) / 2));

					MAVlist[sysid, compid].packetspersecondbuild[msgid] = DateTime.Now;

					//Console.WriteLine("Packet {0}",temp[5]);
					// store packet history
					lock (objlock)
					{
						MAVlist[sysid, compid].addPacket(message);

						// 3dr radio status packet are injected into the current mav
						if (msgid == (byte)MAVLINK_MSG_ID.RADIO_STATUS ||
							msgid == (byte)MAVLINK_MSG_ID.RADIO)
						{
							MAVlist[sysidcurrent, compidcurrent].addPacket(message);
						}

						// adsb packets are forwarded and can be from any sysid/compid
						
						if (msgid == (byte)MAVLINK_MSG_ID.ADSB_VEHICLE)
						{
							var adsb = message.ToStructure<mavlink_adsb_vehicle_t>();
							var id = adsb.ICAO_address.ToString("X5");

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
					}

					// set seens sysid's based on hb packet - this will hide 3dr radio packets
					if (msgid == (byte)MAVLINK_MSG_ID.HEARTBEAT)
					{
						mavlink_heartbeat_t hb = message.ToStructure<mavlink_heartbeat_t>();

						// not a gcs
						if (hb.type != (byte)MAV_TYPE.GCS)
						{
							// add a seen sysid
							if (!MAVlist.Contains(sysid, compid, false))
							{
								// ensure its set from connect or log playback
								MAVlist.Create(sysid, compid);
								MAVlist[sysid, compid].aptype = (MAV_TYPE)hb.type;
								MAVlist[sysid, compid].apname = (MAV_AUTOPILOT)hb.autopilot;
								// setAPType(sysid, compid);
							}

							// attach to the only remote device. / default to first device seen
							if (MAVlist.Count == 1)
							{
								// set it private as compidset will trigger new mavstate
								_sysidcurrent = sysid;
								compidcurrent = compid;
							}
						}
					}

					// only process for active mav
					if (sysidcurrent == sysid && compidcurrent == compid)
					{
						// PacketReceived(message);
					}



					if (msgid == (byte)MAVLINK_MSG_ID.STATUSTEXT) // status text
					{
						var msg = message.ToStructure<mavlink_statustext_t>();
						byte sev = msg.severity;

					}

					/***
					if (lastparamset != DateTime.MinValue && lastparamset.AddSeconds(10) < DateTime.Now)
					{
						lastparamset = DateTime.MinValue;

						if (BaseStream.IsOpen)
						{
							doCommand(MAV_CMD.PREFLIGHT_STORAGE, 0, 0, 0, 0, 0, 0, 0, false);
						}
					}*/

				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}

			// update last valid packet receive time
			MAVlist[sysid, compid].lastvalidpacket = DateTime.Now;

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

		

		public bool getVersion()
		{
			mavlink_autopilot_version_request_t req = new mavlink_autopilot_version_request_t();

			req.target_component = MAV.compid;
			req.target_system = MAV.sysid;

			// request point
			generatePacket((byte)MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST, req);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(200) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("getVersion Retry " + retrys + " - giv com " + giveComport);
						generatePacket((byte)MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST, req);
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

		private void generatePacket(MAVLINK_MSG_ID messageType, object indata)
		{
			generatePacket((int)messageType, indata);
		}

		void generatePacket(int messageType, object indata)
		{
			//uses currently targeted mavs sysid and compid
			generatePacket(messageType, indata, MAV.sysid, MAV.compid);
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
						Console.WriteLine(ack.ToString());

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
			generatePacket(MAVLINK_MSG_ID.COMMAND_ACK, ack);
			Thread.Sleep(20);
			generatePacket(MAVLINK_MSG_ID.COMMAND_ACK, ack);
		}

		public void setMode(byte sysid, byte compid, MAVLink.mavlink_set_mode_t mode, MAVLink.MAV_MODE_FLAG base_mode = 0)
		{
			mode.base_mode |= (byte)base_mode;
			generatePacket((byte)(byte)MAVLink.MAVLINK_MSG_ID.SET_MODE, mode, sysid, compid);
			System.Threading.Thread.Sleep(SLEEP_TIME_SETMODE);
			generatePacket((byte)(byte)MAVLink.MAVLINK_MSG_ID.SET_MODE, mode, sysid, compid);
		}

		public void setGuidedModeWP(Locationwp gotohere, bool setguidedmode = true)
		{
			setGuidedModeWP(MAV.sysid, MAV.compid, gotohere, setguidedmode);
		}

		public void setGuidedModeWP(byte sysid, byte compid, Locationwp gotohere, bool setguidedmode = true)
		{
			if (gotohere.alt == 0 || gotohere.lat == 0 || gotohere.lng == 0)
				return;

			giveComport = true;

			try
			{
				gotohere.id = (ushort)MAV_CMD.WAYPOINT;

				// Set guided mode first

				MAV_MISSION_RESULT ans = setWP(sysid, compid, gotohere, 0, MAV_FRAME.GLOBAL_RELATIVE_ALT,
					(byte)2);

				if (ans != MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
					throw new Exception("Guided Mode Failed");
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}

			giveComport = false;
		}

		public MAV_MISSION_RESULT setWP(Locationwp loc, ushort index, MAV_FRAME frame, byte current = 0,
			byte autocontinue = 1, bool use_int = false)
		{
			return setWP(MAV.sysid, MAV.compid, loc, index, frame, current, autocontinue, use_int);
		}

		/// <summary>
		/// Save wp to eeprom
		/// </summary>
		/// <param name="loc">location struct</param>
		/// <param name="index">wp no</param>
		/// <param name="frame">global or relative</param>
		/// <param name="current">0 = no , 2 = guided mode</param>
		public MAV_MISSION_RESULT setWP(byte sysid, byte compid, Locationwp loc, ushort index, MAV_FRAME frame, byte current = 0,
			byte autocontinue = 1, bool use_int = false)
		{
			if (use_int)
			{
				mavlink_mission_item_int_t req = new mavlink_mission_item_int_t();

				req.target_system = sysid;
				req.target_component = compid;

				req.command = loc.id;

				req.current = current;
				req.autocontinue = autocontinue;

				req.frame = (byte)frame;
				if (loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONTROL || loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE)
				{
					req.y = (int)(loc.lng);
					req.x = (int)(loc.lat);
				}
				else
				{
					req.y = (int)(loc.lng * 1.0e7);
					req.x = (int)(loc.lat * 1.0e7);
				}
				req.z = (float)(loc.alt);

				req.param1 = loc.p1;
				req.param2 = loc.p2;
				req.param3 = loc.p3;
				req.param4 = loc.p4;

				req.seq = index;

				return setWP(req);
			}
			else
			{
				mavlink_mission_item_t req = new mavlink_mission_item_t();

				req.target_system = sysid;
				req.target_component = compid;

				req.command = loc.id;

				req.current = current;
				req.autocontinue = autocontinue;

				req.frame = (byte)frame;
				req.y = (float)(loc.lng);
				req.x = (float)(loc.lat);
				req.z = (float)(loc.alt);

				req.param1 = loc.p1;
				req.param2 = loc.p2;
				req.param3 = loc.p3;
				req.param4 = loc.p4;

				req.seq = index;

				return setWP(req);
			}
		}


		public MAV_MISSION_RESULT setWP(mavlink_mission_item_t req)
		{
			giveComport = true;

			ushort index = req.seq;
			
			// request
			generatePacket((byte)MAVLINK_MSG_ID.MISSION_ITEM, req);


			DateTime start = DateTime.Now;
			int retrys = 10;

			while (true)
			{
				if (!(start.AddMilliseconds(400) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("setWP Retry " + retrys);
						generatePacket((byte)MAVLINK_MSG_ID.MISSION_ITEM, req);

						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - setWP");
				}
				MAVLinkMessage buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ACK)
					{
						var ans = buffer.ToStructure<mavlink_mission_ack_t>();
						log.Info("set wp " + index + " ACK 47 : " + buffer.msgid + " ans " +
								 Enum.Parse(typeof(MAV_MISSION_RESULT), ans.type.ToString()));


						if (req.current == 2)
						{
							// MAVlist[req.target_system, req.target_component].GuidedMode = req;
						}
						else if (req.current == 3)
						{
						}
						else
						{
							// MAVlist[req.target_system, req.target_component].wps[req.seq] = req;
						}

						//if (ans.target_system == req.target_system && ans.target_component == req.target_component)
						{
							giveComport = false;
							return (MAV_MISSION_RESULT)ans.type;
						}
					}
					else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
					{
						var ans = buffer.ToStructure<mavlink_mission_request_t>();
						if (ans.seq == (index + 1))
						{
							log.Info("set wp doing " + index + " req " + ans.seq + " REQ 40 : " + buffer.msgid);
							giveComport = false;

							if (req.current == 2)
							{
								// MAVlist[req.target_system, req.target_component].GuidedMode = req;
							}
							else if (req.current == 3)
							{
							}
							else
							{
								// MAVlist[req.target_system, req.target_component].wps[req.seq] = req;
							}

							//if (ans.target_system == req.target_system && ans.target_component == req.target_component)
							{
								giveComport = false;
								return MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED;
							}
						}
						else
						{
							start = DateTime.MinValue;
						}
					}
					else
					{
						// Console.WriteLine(DateTime.Now + " PC setwp " + buffer.msgid);
					}
				}
			}

			// return MAV_MISSION_RESULT.MAV_MISSION_INVALID;
		}

		public MAV_MISSION_RESULT setWP(mavlink_mission_item_int_t req)
		{
			giveComport = true;

			ushort index = req.seq;

			// request
			generatePacket((byte)MAVLINK_MSG_ID.MISSION_ITEM_INT, req);

			DateTime start = DateTime.Now;
			int retrys = 10;

			while (true)
			{
				if (!(start.AddMilliseconds(400) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("setWP Retry " + retrys);
						generatePacket((byte)MAVLINK_MSG_ID.MISSION_ITEM_INT, req);

						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - setWP");
				}
				MAVLinkMessage buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ACK)
					{
						var ans = buffer.ToStructure<mavlink_mission_ack_t>();
						log.Info("set wp " + index + " ACK 47 : " + buffer.msgid + " ans " +
								 Enum.Parse(typeof(MAV_MISSION_RESULT), ans.type.ToString()));
						giveComport = false;

						if (req.current == 2)
						{
							// MAVlist[req.target_system, req.target_component].GuidedMode = (Locationwp)req;
						}
						else if (req.current == 3)
						{
						}
						else
						{
							// MAVlist[req.target_system, req.target_component].wps[req.seq] = (Locationwp)req;
						}

						//if (ans.target_system == req.target_system && ans.target_component == req.target_component)
						{
							giveComport = false;
							return (MAV_MISSION_RESULT)ans.type;
						}
					}
					else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
					{
						var ans = buffer.ToStructure<mavlink_mission_request_t>();
						if (ans.seq == (index + 1))
						{
							log.Info("set wp doing " + index + " req " + ans.seq + " REQ 40 : " + buffer.msgid);
							giveComport = false;

							if (req.current == 2)
							{
								// MAVlist[req.target_system, req.target_component].GuidedMode = (Locationwp)req;
							}
							else if (req.current == 3)
							{

							}
							else
							{
								// MAVlist[req.target_system, req.target_component].wps[req.seq] = (Locationwp)req;
							}

							//if (ans.target_system == req.target_system && ans.target_component == req.target_component)
							{
								giveComport = false;
								return MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED;
							}
						}
						else
						{
							
							// resend point now
							start = DateTime.MinValue;
						}
					}
					else
					{
						Console.WriteLine(DateTime.Now + " PC setwp " + buffer.msgid);
					}
				}
			}

			// return MAV_MISSION_RESULT.MAV_MISSION_INVALID;
		}

		/// <summary>
		/// Returns WP count
		/// </summary>
		/// <returns></returns>
		public ushort getWPCount()
		{
			giveComport = true;
			MAVLinkMessage buffer;
			mavlink_mission_request_list_t req = new mavlink_mission_request_list_t();

			req.target_system = MAV.sysid;
			req.target_component = MAV.compid;

			// request list
			generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST_LIST, req);

			DateTime start = DateTime.Now;
			int retrys = 6;

			while (true)
			{
				if (!(start.AddMilliseconds(700) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("getWPCount Retry " + retrys + " - giv com " + giveComport);
						generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST_LIST, req);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					//return (byte)int.Parse(param["WP_TOTAL"].ToString());
					throw new TimeoutException("Timeout on read - getWPCount");
				}

				buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_COUNT)
					{
						var count = buffer.ToStructure<mavlink_mission_count_t>();


						log.Info("wpcount: " + count.count);
						giveComport = false;
						return count.count; // should be ushort, but apm has limited wp count < byte
					}
				}
			}
		}

		/// <summary>
		/// Gets specfied WP
		/// </summary>
		/// <param name="index"></param>
		/// <returns>WP</returns>
		public Locationwp getWP(ushort index)
		{
			while (giveComport)
			{
				System.Threading.Thread.Sleep(100);
			}

			// bool use_int = (MAV.cs.capabilities & MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;
			bool use_int = true;

			object req;

			if (use_int)
			{
				mavlink_mission_request_int_t reqi = new mavlink_mission_request_int_t();

				reqi.target_system = MAV.sysid;
				reqi.target_component = MAV.compid;

				reqi.seq = index;

				// request
				generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST_INT, reqi);

				req = reqi;
			}
			else
			{
				mavlink_mission_request_t reqf = new mavlink_mission_request_t();

				reqf.target_system = MAV.sysid;
				reqf.target_component = MAV.compid;

				reqf.seq = index;

				// request
				generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST, reqf);

				req = reqf;
			}

			giveComport = true;
			Locationwp loc = new Locationwp();

			DateTime start = DateTime.Now;
			int retrys = 5;

			while (true)
			{
				if (!(start.AddMilliseconds(3500) > DateTime.Now)) // apm times out after 5000ms
				{
					if (retrys > 0)
					{
						log.Info("getWP Retry " + retrys);
						if (use_int)
							generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST_INT, req);
						else
							generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST, req);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - getWP");
				}
				//Console.WriteLine("getwp read " + DateTime.Now.Millisecond);
				MAVLinkMessage buffer = readPacket();
				//Console.WriteLine("getwp readend " + DateTime.Now.Millisecond);
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM)
					{
						//Console.WriteLine("getwp ans " + DateTime.Now.Millisecond);

						var wp = buffer.ToStructure<mavlink_mission_item_t>();

						// received a packet, but not what we requested
						if (index != wp.seq)
						{
							generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST, req);
							continue;
						}

						loc.options = (byte)(wp.frame);
						loc.id = (ushort)(wp.command);
						loc.p1 = (wp.param1);
						loc.p2 = (wp.param2);
						loc.p3 = (wp.param3);
						loc.p4 = (wp.param4);

						loc.alt = ((wp.z));
						loc.lat = ((wp.x));
						loc.lng = ((wp.y));

						log.InfoFormat("getWP {0} {1} {2} {3} {4} opt {5}", loc.id, loc.p1, loc.alt, loc.lat, loc.lng,
							loc.options);

						break;
					}
					else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM_INT)
					{
						//Console.WriteLine("getwp ans " + DateTime.Now.Millisecond);

						var wp = buffer.ToStructure<mavlink_mission_item_int_t>();

						// received a packet, but not what we requested
						if (index != wp.seq)
						{
							generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST_INT, req);
							continue;
						}

						loc.options = (byte)(wp.frame);
						loc.id = (ushort)(wp.command);
						loc.p1 = (wp.param1);
						loc.p2 = (wp.param2);
						loc.p3 = (wp.param3);
						loc.p4 = (wp.param4);

						loc.alt = ((wp.z));
						loc.lat = ((wp.x / 1.0e7));
						loc.lng = ((wp.y / 1.0e7));

						if (loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONTROL || loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE)
						{
							loc.lat = wp.x;
						}

						log.InfoFormat("getWPint {0} {1} {2} {3} {4} opt {5}", loc.id, loc.p1, loc.alt, loc.lat, loc.lng,
							loc.options);

						break;
					}
					else
					{
						//log.Info(DateTime.Now + " PC getwp " + buffer.msgid);
					}
				}
			}
			giveComport = false;
			return loc;
		}

		public void getDatastream(MAV_DATA_STREAM id, byte hzrate)
		{
			getDatastream((byte)sysidcurrent, (byte)compidcurrent, id, hzrate);
		}

		public void getDatastream(byte sysid, byte compid, MAV_DATA_STREAM id, byte hzrate)
		{
			mavlink_request_data_stream_t req = new mavlink_request_data_stream_t();
			req.target_system = sysid;
			req.target_component = compid;

			req.req_message_rate = hzrate;
			req.start_stop = 1; // start
			req.req_stream_id = (byte)id; // id

			// send each one twice.
			generatePacket((byte)MAVLINK_MSG_ID.REQUEST_DATA_STREAM, req, sysid, compid);
			generatePacket((byte)MAVLINK_MSG_ID.REQUEST_DATA_STREAM, req, sysid, compid);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Set start and finish for partial wp upload.
		/// </summary>
		/// <param name="startwp"></param>
		/// <param name="endwp"></param>
		public void setWPPartialUpdate(ushort startwp, ushort endwp)
		{
			mavlink_mission_write_partial_list_t req = new mavlink_mission_write_partial_list_t();

			req.target_system = MAV.sysid;
			req.target_component = MAV.compid;

			req.start_index = (short)startwp;
			req.end_index = (short)endwp;

			generatePacket((byte)MAVLINK_MSG_ID.MISSION_WRITE_PARTIAL_LIST, req, MAV.sysid, MAV.compid);
		}

		/// <summary>
		/// Sets wp total count
		/// </summary>
		/// <param name="wp_total"></param>
		public void setWPTotal(ushort wp_total)
		{
			giveComport = true;
			mavlink_mission_count_t req = new mavlink_mission_count_t();

			req.target_system = MAV.sysid;
			req.target_component = MAV.compid; // MSG_NAMES.MISSION_COUNT

			req.count = wp_total;

			generatePacket((byte)MAVLINK_MSG_ID.MISSION_COUNT, req, MAV.sysid, MAV.compid);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(700) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("setWPTotal Retry " + retrys);
						generatePacket((byte)MAVLINK_MSG_ID.MISSION_COUNT, req, MAV.sysid, MAV.compid);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - setWPTotal");
				}
				MAVLinkMessage buffer = readPacket();
				if (buffer.Length > 9)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
					{
						var request = buffer.ToStructure<mavlink_mission_request_t>();

						if (request.seq == 0)
						{
							if (MAV.param["WP_TOTAL"] != null)
								MAV.param["WP_TOTAL"].Value = wp_total - 1;
							if (MAV.param["CMD_TOTAL"] != null)
								MAV.param["CMD_TOTAL"].Value = wp_total - 1;
							if (MAV.param["MIS_TOTAL"] != null)
								MAV.param["MIS_TOTAL"].Value = wp_total - 1;

							MAV.wps.Clear();

							giveComport = false;
							return;
						}
					}
					else
					{
						//Console.WriteLine(DateTime.Now + " PC getwp " + buffer.msgid);
					}
				}
			}
		}

		public int getRequestedWPNo()
		{
			giveComport = true;
			DateTime start = DateTime.Now;

			while (true)
			{
				if (!(start.AddMilliseconds(5000) > DateTime.Now))
				{
					giveComport = false;
					throw new TimeoutException("Timeout on read - getRequestedWPNo");
				}
				MAVLinkMessage buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
					{
						var ans = buffer.ToStructure<mavlink_mission_request_t>();

						log.InfoFormat("getRequestedWPNo seq {0} ts {1} tc {2}", ans.seq, ans.target_system, ans.target_component);

						giveComport = false;

						return ans.seq;
					}
				}
			}
		}


		public void setWPACK()
		{
			mavlink_mission_ack_t req = new mavlink_mission_ack_t();
			req.target_system = MAV.sysid;
			req.target_component = MAV.compid;
			req.type = 0;

			generatePacket((byte)MAVLINK_MSG_ID.MISSION_ACK, req, MAV.sysid, MAV.compid);
		}

	}
}
