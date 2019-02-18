using Diva.Comms;
using Diva.Controls;
using Diva.Data;
using Diva.Properties;
using Diva.Utilities;
using GMap.NET.WindowsForms;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Diva.Planner;
using Timer = System.Timers.Timer;

namespace Diva.Mavlink
{
	public class MavlinkInterface : MAVLink, IDisposable
	{

		public static readonly int SLEEP_TIME_SETMODE = 10;
		public static readonly double CONNECT_TIMEOUT_SECONDS = 30;
        public static int GET_PARAM_TIMEOUT = 1000;

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

		public MavStatus Status
		{
			get { return MAVlist[sysidcurrent, compidcurrent]; }
			set { MAVlist[sysidcurrent, compidcurrent] = value; }
		}

		public event EventHandler ParamListChanged;
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


		List<KeyValuePair<MAVLINK_MSG_ID, Func<MAVLinkMessage, bool>>> Subscriptions =
			new List<KeyValuePair<MAVLINK_MSG_ID, Func<MAVLinkMessage, bool>>>();


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
		private ProgressDialogV2 frmProgressReporter;
		private ObjectsOverlay objectsOverlay;

		// threading
		private bool threadRunnable = false;
		private Thread SerialReaderThread = null;

		// ticker
		private DateTime heartbeatSend = DateTime.Now;
		private DateTime lastupdate = DateTime.Now;
		private DateTime lastdata = DateTime.MinValue;
		private DateTime lastparamset = DateTime.MinValue;

		public MavlinkInterface()
		{
			MAVlist = new MavList(this);
			this.BaseStream = new SerialPort();
			this.WhenPacketLost = new Subject<int>();
			this.WhenPacketReceived = new Subject<int>();
			this.objectsOverlay = new ObjectsOverlay();
		}

		public ObjectsOverlay ObjOverlay => objectsOverlay;
		public List<Locationwp> LastCmds { get; set; } = new List<Locationwp>();

		public class ObjectsOverlay
		{
			public static readonly string OVERLAY_KEY = "objects";
			public static readonly int RANDOM_MAXIMUM = 10;
			private Random random = new Random(RANDOM_MAXIMUM);

			public ObjectsOverlay()
			{
				this.Overlay = new GMapOverlay(OVERLAY_KEY + random.Next());
			}

			public GMapOverlay Overlay { get; }
			public Color RoutingColor { get; } = Utility.RandomColor();
			public Bitmap Marker { get; } = MarkerPicker.GetMarker();
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
					sendPacket(htb, Status.sysid, Status.compid);

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

		uint _mode = 99999;

		#region retrieve sensor data from flight control.

		public void UpdateCurrentSettings(bool updatenow)
		{
			MAVLink.MAVLinkMessage mavlinkMessage = readPacket();

			if (mavlinkMessage.Length == 0) return;

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
							getDatastream(Status.sysid, Status.compid, MAVLink.MAV_DATA_STREAM.EXTENDED_STATUS, 2);
							getDatastream(Status.sysid, Status.compid, MAVLink.MAV_DATA_STREAM.POSITION, 2);
							getDatastream(Status.sysid, Status.compid, MAVLink.MAV_DATA_STREAM.EXTRA1, 4);
							getDatastream(Status.sysid, Status.compid, MAVLink.MAV_DATA_STREAM.EXTRA2, 4);
							getDatastream(Status.sysid, Status.compid, MAVLink.MAV_DATA_STREAM.EXTRA3, 2);
							getDatastream(Status.sysid, Status.compid, MAVLink.MAV_DATA_STREAM.RAW_SENSORS, 2);
							getDatastream(Status.sysid, Status.compid, MAVLink.MAV_DATA_STREAM.RC_CHANNELS, 2);
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

							Status.mode = hb.custom_mode;


							Status.sys_status = hb.system_status;

							if (hb.type == (byte)MAVLink.MAV_TYPE.GCS)
							{
								// TODO: do something when recived GCS hb
							}
							else
							{

								// Console.WriteLine("base_mode:" + hb.base_mode);
								//Console.WriteLine("custom_mode:" + hb.custom_mode);
								//Console.WriteLine("armd: " + (hb.base_mode & (byte)MAVLink.MAV_MODE_FLAG.SAFETY_ARMED));

								Status.armed = (hb.base_mode & (byte)MAVLink.MAV_MODE_FLAG.SAFETY_ARMED) ==
								   (byte)MAVLink.MAV_MODE_FLAG.SAFETY_ARMED;

								// saftey switch
								/*
								if (armed && sensors_enabled.motor_control == false && sensors_enabled.seen)
								{
									messageHigh = "(SAFE)";
									messageHighTime = DateTime.Now;
								}*/

								// for future use
								/*
								Status.landed = hb.system_status == (byte)MAVLink.MAV_STATE.STANDBY;
								Status.actived = hb.system_status == (byte)MAVLink.MAV_STATE.ACTIVE;
								Status.failsafe = hb.system_status == (byte)MAVLink.MAV_STATE.CRITICAL;*/
								Status.sys_status = hb.system_status;
							}
						}
					}

					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.MISSION_CURRENT))
					{
						if (mavlinkMessage != null)
						{
							var wpcur = mavlinkMessage.ToStructure<MAVLink.mavlink_mission_current_t>();

							int wpno = 0;
							int lastautowp = 0;

							int oldwp = (int)wpno;

							wpno = wpcur.seq;

							if (Status.mode == 4 && wpno != 0)
							{
								lastautowp = (int)wpno;
							}

							/**
							if (mode.ToLower() == "auto" && wpno != 0)
							{
								lastautowp = (int)wpno;
							}

							if (oldwp != wpno && MainV2.speechEnable && MainV2.comPort.MAV.cs == this &&
								Settings.Instance.GetBoolean("speechwaypointenabled"))
							{
								MainV2.speechEngine.SpeakAsync(Common.speechConversion("" + Settings.Instance["speechwaypoint"]));
							}**/

							//MAVLink.packets[(byte)MAVLink.MSG_NAMES.WAYPOINT_CURRENT);
						}
					}


					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.GLOBAL_POSITION_INT))
					{
						if (mavlinkMessage != null)
						{
							var loc = mavlinkMessage.ToStructure<MAVLink.mavlink_global_position_int_t>();

							// the new arhs deadreckoning may send 0 alt and 0 long. check for and undo

							
							Status.alt = loc.relative_alt / 1000.0f;

							useLocation = true;
							if (loc.lat == 0 && loc.lon == 0)
							{
								useLocation = false;
							}
							else
							{
								Status.current_lat = loc.lat / 10000000.0;
								Status.current_lng = loc.lon / 10000000.0;
								Status.altasl = loc.alt / 1000.0f;

								double vx = loc.vx * 0.01;
								double vy = loc.vy * 0.01;
								double vz = loc.vz * 0.01;
							}
						}
					
					}

					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.GPS_RAW_INT))
					{
						if (mavlinkMessage != null)
						{
							var gps = mavlinkMessage.ToStructure<MAVLink.mavlink_gps_raw_int_t>();

							if (!useLocation)
							{
								Status.current_lat = gps.lat * 1.0e-7;
								Status.current_lng = gps.lon * 1.0e-7;

								Status.altasl = gps.alt / 1000.0f;
								// alt = gps.alt; // using vfr as includes baro calc
								
							}

							byte gpsstatus = gps.fix_type;

							float gpshdop = (float)Math.Round((double)gps.eph / 100.0, 2);

							Status.satcount = gps.satellites_visible;
							Status.groundspeed = gps.vel * 1.0e-2f;
							Status.groundcourse = gps.cog * 1.0e-2f;
														
							//MAVLink.packets[(byte)MAVLink.MSG_NAMES.GPS_RAW);
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

							Status.yaw = yaw;
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

							Status.battery_voltage = battery_voltage;

							/*
							Invoke((MethodInvoker)delegate
							{
								this.ts_lbl_battery.Text = battery_voltage.ToString() + "%";
							});*/
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

							Status.nav_bearing = nav_bearing;

						}
					}

				}
			}
		}

		#endregion

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
			Open(true);
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

		public void Open(bool getparams, bool skipconnectedcheck = false)
		{
			if (BaseStream.IsOpen && !skipconnectedcheck)
			{
				return;
			}

			MAVlist.Clear();

			frmProgressReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				HintImage = Resources.icon_warn,
				Text = Diva.Properties.Strings.MsgDialogConnect,
			};

			if (getparams)
			{
				frmProgressReporter.DoWork += FrmProgressReporterDoWorkAndParams;
			}
			else
			{
				frmProgressReporter.DoWork += FrmProgressReporterDoWorkNOParams;
			}
			frmProgressReporter.UpdateProgressAndStatus(-1, "sync...");
			// ThemeManager.ApplyThemeTo(frmProgressReporter);

			frmProgressReporter.RunBackgroundOperationAsync();

			frmProgressReporter.Dispose();

			/**
			if (ParamListChanged != null)
			{
				ParamListChanged(this, null);
			}*/


		}

		void FrmProgressReporterDoWorkAndParams(object sender, ProgressWorkerEventArgs e, object passdata = null)
		{
			OpenBg(sender, true, e);
		}

		void FrmProgressReporterDoWorkNOParams(object sender, ProgressWorkerEventArgs e, object passdata = null)
		{
			OpenBg(sender, false, e);
		}

		public void OpenBg(object PRsender, bool getparams, ProgressWorkerEventArgs progressWorkerEventArgs)
		{
			frmProgressReporter.UpdateProgressAndStatus(-1, "mavlink connecting");

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
						progressWorkerEventArgs.CancelRequestChanged += (o, e) =>
						{
							((UdpSerial)BaseStream).CancelConnect = true;
							((ProgressWorkerEventArgs)o)
								.CancelAcknowledged = true;
						};
					
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
					frmProgressReporter.UpdateProgressAndStatus(-1, string.Format("trying", secondsRemaining));
					if (secondsRemaining > 0) countDown.Start();
				};
				countDown.Start();

				// px4 native
				// BaseStream.WriteLine("sh /etc/init.d/rc.usb");

				int count = 0;

				while (true)
				{

					if (progressWorkerEventArgs.CancelRequested)
					{
						progressWorkerEventArgs.CancelAcknowledged = true;
						countDown.Stop();
						if (BaseStream.IsOpen)
							BaseStream.Close();
						giveComport = false;
						return;
					}

					log.Info(DateTime.Now.Millisecond + " Start connect loop ");

					if (DateTime.Now > deadline)
					{
						//if (Progress != null)
						//    Progress(-1, "No Heartbeat Packets");
						countDown.Stop();
						this.close();

						if (hbseen)
						{
							progressWorkerEventArgs.ErrorMessage = Strings.Only1Hb;
							throw new Exception(Strings.Only1HbD);
						}
						else
						{
							progressWorkerEventArgs.ErrorMessage = Strings.Only1Hb;
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
					new mavlink_statustext_t() { severity = (byte)MAV_SEVERITY.INFO, text = temp });
				// mavlink2
				/**
				generatePacket((byte)MAVLINK_MSG_ID.STATUSTEXT,
					new mavlink_statustext_t() { severity = (byte)MAV_SEVERITY.INFO, text = temp }, sysidcurrent,
					compidcurrent, true, true); */

				// this ensures a mavlink2 change has been noticed
				getHeartBeat();

				getVersion();

				if (getparams)
				{
					frmProgressReporter.UpdateProgressAndStatus(0,
					   "Getting Params.. (sysid " + Status.sysid + " compid " + Status.compid + ") ");
					getParamListBG() ;

					// set the default low voltage warning: 10.5d
					
					Status.low_voltage = 10.5d;
					
				}

				
				if (frmProgressReporter.doWorkArgs.CancelAcknowledged == true)
				{
					giveComport = false;
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
				giveComport = false;
				if (string.IsNullOrEmpty(progressWorkerEventArgs.ErrorMessage))
					progressWorkerEventArgs.ErrorMessage = Strings.ConnectFailed;
				log.Error(e);
				throw;
			}
			//frmProgressReporter.Close();
			giveComport = false;
			frmProgressReporter.UpdateProgressAndStatus(100, "done");
			log.Info("Done open " + Status.sysid + " " + Status.compid);
			Status.packetslost = 0;
			Status.synclost = 0;
		}

		

		public void getParamList()
		{
			log.InfoFormat("getParamList {0} {1}", sysidcurrent, compidcurrent);

			frmProgressReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				Text = "Getting Params" + " " + sysidcurrent
			};

			frmProgressReporter.DoWork += FrmProgressReporterGetParams;
			frmProgressReporter.UpdateProgressAndStatus(-1, "Get params SD");
			// ThemeManager.ApplyThemeTo(frmProgressReporter);

			frmProgressReporter.RunBackgroundOperationAsync();

			frmProgressReporter.Dispose();

			if (ParamListChanged != null)
			{
				ParamListChanged(this, null);
			}
		}

		void FrmProgressReporterGetParams(object sender, ProgressWorkerEventArgs e, object passdata = null)
		{
			getParamListBG();
		}


		private int _parampoll = 0;

		public void getParamPoll()
		{
			// check if we have all
			if (Status.param.TotalReceived >= Status.param.TotalReported)
			{
				return;
			}

			// if we are connected as primary to a vechile where we dont have all the params, poll for them
			short i = (short)(_parampoll % Status.param.TotalReported);

			GetParam("", i, false);

			_parampoll++;
		}

		public float GetParam(string name)
		{
			return GetParam(name, -1);
		}

		public float GetParam(short index)
		{
			return GetParam("", index);
		}

		public float GetParam(string name = "", short index = -1, bool requireresponce = true)
		{
			return GetParam(Status.sysid, Status.compid, name, index, requireresponce);
		}

		/// <summary>
		/// Get param by either index or name
		/// </summary>
		/// <param name="index"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public float GetParam(byte sysid, byte compid, string name = "", short index = -1, bool requireresponce = true)
		{
			if (name == "" && index == -1)
				return 0;

			log.Info("GetParam name: '" + name + "' or index: " + index + " " + sysid + ":" + compid);

			MAVLinkMessage buffer;

			mavlink_param_request_read_t req = new mavlink_param_request_read_t();
			req.target_system = sysid;
			req.target_component = compid;
			req.param_index = index;
			req.param_id = new byte[] { 0x0 };
			if (index == -1)
			{
				req.param_id = ASCIIEncoding.ASCII.GetBytes(name);
			}

			Array.Resize(ref req.param_id, 16);

			generatePacket((byte)MAVLINK_MSG_ID.PARAM_REQUEST_READ, req, sysid, compid);

			if (!requireresponce)
			{
				return 0f;
			}

			giveComport = true;

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(GET_PARAM_TIMEOUT) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("GetParam Retry " + retrys);
						generatePacket((byte)MAVLINK_MSG_ID.PARAM_REQUEST_READ, req, sysid, compid);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - GetParam");
				}

				buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE && buffer.sysid == req.target_system && buffer.compid == req.target_component)
					{
						giveComport = false;

						mavlink_param_value_t par = buffer.ToStructure<mavlink_param_value_t>();

						string st = ASCIIEncoding.ASCII.GetString(par.param_id);

						int pos = st.IndexOf('\0');

						if (pos != -1)
						{
							st = st.Substring(0, pos);
						}

						// not the correct id
						if (!(par.param_index == index || st == name))
						{
							log.ErrorFormat("Wrong Answer {0} - {1} - {2}    --- '{3}' vs '{4}'", par.param_index,
								ASCIIEncoding.ASCII.GetString(par.param_id), par.param_value,
								ASCIIEncoding.ASCII.GetString(req.param_id).TrimEnd(), st);
							continue;
						}

						// update table
						if (MAVlist[sysid, compid].apname == MAV_AUTOPILOT.ARDUPILOTMEGA)
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							MAVlist[sysid, compid].param[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)par.param_type);
						}
						else
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							MAVlist[sysid, compid].param[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), (MAV_PARAM_TYPE)par.param_type, (MAV_PARAM_TYPE)par.param_type);
						}

						MAVlist[sysid, compid].param_types[st] = (MAV_PARAM_TYPE)par.param_type;

						log.Info(DateTime.Now.Millisecond + " got param " + (par.param_index) + " of " +
								 (par.param_count) + " name: " + st);

						return par.param_value;
					}
				}
			}
		}


		/// <summary>
		/// Get param list from apm
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, double> getParamListBG()
		{
			giveComport = true;
			List<int> indexsreceived = new List<int>();

			// create new list so if canceled we use the old list
			MAVLinkParamList newparamlist = new MAVLinkParamList();

			int param_total = 1;

			mavlink_param_request_list_t req = new mavlink_param_request_list_t();
			req.target_system = Status.sysid;
			req.target_component = Status.compid;

			generatePacket((byte)MAVLINK_MSG_ID.PARAM_REQUEST_LIST, req);

			DateTime start = DateTime.Now;
			DateTime restart = DateTime.Now;

			DateTime lastmessage = DateTime.MinValue;

			//hires.Stopwatch stopwatch = new hires.Stopwatch();
			int packets = 0;
			int retry = 0;
			bool onebyone = false;
			DateTime lastonebyone = DateTime.MinValue;

			do
			{
				
				if (frmProgressReporter.doWorkArgs.CancelRequested)
				{
					frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
					giveComport = false;
					frmProgressReporter.doWorkArgs.ErrorMessage = "User Canceled";
					return Status.param;
				}

				// 4 seconds between valid packets
				if (!(start.AddMilliseconds(4000) > DateTime.Now) && !logreadmode)
				{
					if (retry < 6)
					{
						retry++;
						generatePacket((byte)MAVLINK_MSG_ID.PARAM_REQUEST_LIST, req);
						start = DateTime.Now;
						continue;
					}

					onebyone = true;

					if (lastonebyone.AddMilliseconds(600) < DateTime.Now)
					{
						log.Info("Get param 1 by 1 - got " + indexsreceived.Count + " of " + param_total);

						int queued = 0;
						// try getting individual params
						for (short i = 0; i <= (param_total - 1); i++)
						{
							if (!indexsreceived.Contains(i))
							{
								
								if (frmProgressReporter.doWorkArgs.CancelRequested)
								{
									frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
									giveComport = false;
									frmProgressReporter.doWorkArgs.ErrorMessage = "User Canceled";
									return Status.param;
								}

								// prevent dropping out of this get params loop
								try
								{
									queued++;

									mavlink_param_request_read_t req2 = new mavlink_param_request_read_t();
									req2.target_system = Status.sysid;
									req2.target_component = Status.compid;
									req2.param_index = i;
									req2.param_id = new byte[] { 0x0 };

									Array.Resize(ref req2.param_id, 16);

									generatePacket((byte)MAVLINK_MSG_ID.PARAM_REQUEST_READ, req2);

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

				//Console.WriteLine(DateTime.Now.Millisecond + " gp0 ");

				MAVLinkMessage buffer = readPacket();
				//Console.WriteLine(DateTime.Now.Millisecond + " gp1 ");
				if (buffer.Length > 5)
				{
					packets++;
					// stopwatch.Start();
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE && buffer.sysid == req.target_system && buffer.compid == req.target_component)
					{
						restart = DateTime.Now;
						// if we are doing one by one dont update start time
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

						// check if we already have it
						if (indexsreceived.Contains(par.param_index))
						{
							// log.Info("Already got " + (par.param_index) + " '" + paramID + "'");
							// this.frmProgressReporter.UpdateProgressAndStatus((indexsreceived.Count * 100) / param_total,
							//	"Already Got param " + paramID);
							continue;
						}

						//Console.WriteLine(DateTime.Now.Millisecond + " gp2 ");

						/**
						if (!MainV2.MONO)
							log.Info(DateTime.Now.Millisecond + " got param " + (par.param_index) + " of " +
									 (par.param_count) + " name: " + paramID);*/

						//Console.WriteLine(DateTime.Now.Millisecond + " gp2a ");

						if (Status.apname == MAV_AUTOPILOT.ARDUPILOTMEGA)
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							newparamlist[paramID] = new MAVLinkParam(paramID, BitConverter.GetBytes(par.param_value), MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)par.param_type);
						}
						else
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							newparamlist[paramID] = new MAVLinkParam(paramID, BitConverter.GetBytes(par.param_value), (MAV_PARAM_TYPE)par.param_type, (MAV_PARAM_TYPE)par.param_type);
						}

						//Console.WriteLine(DateTime.Now.Millisecond + " gp2b ");

						// exclude index of 65535
						if (par.param_index != 65535)
							indexsreceived.Add(par.param_index);

						Status.param_types[paramID] = (MAV_PARAM_TYPE)par.param_type;

						//Console.WriteLine(DateTime.Now.Millisecond + " gp3 ");

						this.frmProgressReporter.UpdateProgressAndStatus((indexsreceived.Count * 100) / param_total,
						"get param" + paramID);

						// we hit the last param - lets escape eq total = 176 index = 0-175
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
					//stopwatch.Stop();
					// Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
					// Console.WriteLine(DateTime.Now.Millisecond + " gp4 " + BaseStream.BytesToRead);
				}
				if (logreadmode && logplaybackfile.BaseStream.Position >= logplaybackfile.BaseStream.Length)
				{
					break;
				}
				if (!logreadmode && !BaseStream.IsOpen)
				{
					var exp = new Exception("Not Connected");
					// frmProgressReporter.doWorkArgs.ErrorMessage = exp.Message;
					throw exp;
				}
			} while (indexsreceived.Count < param_total);

			if (indexsreceived.Count != param_total)
			{
				var exp = new Exception("Missing Params " + indexsreceived.Count + " vs " + param_total);
				// frmProgressReporter.doWorkArgs.ErrorMessage = exp.Message;
				throw exp;
			}
			giveComport = false;

			Status.param.Clear();
			Status.param.TotalReported = param_total;
			Status.param.AddRange(newparamlist);
			return Status.param;
		}

		public List<PointLatLngAlt> getRallyPoints()
		{
			List<PointLatLngAlt> points = new List<PointLatLngAlt>();

			if (!Status.param.ContainsKey("RALLY_TOTAL"))
				return points;

			int count = int.Parse(Status.param["RALLY_TOTAL"].ToString());

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
			req.target_component = Status.compid;
			req.target_system = Status.sysid;

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
			Status.aptype = (MAV_TYPE)hb.type;
			Status.apname = (MAV_AUTOPILOT)hb.autopilot;

			// for different firmwares.
			// setAPType(message.sysid, message.compid);

			Status.sysid = message.sysid;
			Status.compid = message.compid;
			Status.recvpacketcount = message.seq;

		}

		bool logreadmode = false;

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

				while (BaseStream.IsOpen || logreadmode)
				{
					try
					{
						if (readcount > 300)
						{
							break;
						}
						readcount++;
						if (logreadmode)
						{
							message = readlogPacketMavlink();
							buffer = message.buffer;
							if (buffer == null || buffer.Length == 0)
								return MAVLinkMessage.Invalid;
						}
						else
						{
							// time updated for internal reference
							Status.datetime = DateTime.Now;

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
						if (count == 0 && !logreadmode)
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

						if (count >= headerlength || logreadmode)
						{
							try
							{
								if (logreadmode)
								{
								}
								else
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
				else if (logreadmode)
				{
					btr = logplaybackfile.BaseStream.Length - logplaybackfile.BaseStream.Position;
				}
				/*Console.Write("bps {0} loss {1} left {2} mem {3} mav2 {4} sign {5} mav1 {6} mav2 {7} signed {8}      \n", _bps1, MAV.synclost, btr,
					GC.GetTotalMemory(false) / 1024 / 1024.0, MAV.mavlinkv2, MAV.signing, _mavlink1count, _mavlink2count, _mavlink2signed); */
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
				if (logreadmode)
					log.InfoFormat("bad packet pos {0} ", logplaybackfile.BaseStream.Position);
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

			//check if sig was included in packet, and we are not ignoring the signature (signing isnt checked else we wont enable signing)
			//logreadmode we always ignore signing as they would not be in the log if they failed the signature
			/*if (message.sig != null && !MAVlist[sysid, compid].signingignore && !logreadmode)
			{
				_mavlink2signed++;

				bool valid = true;

				foreach (var AuthKey in MAVAuthKeys.Keys.Values)
				{
					using (SHA256Managed signit = new SHA256Managed())
					{
						signit.TransformBlock(AuthKey.Key, 0, AuthKey.Key.Length, null, 0);
						signit.TransformFinalBlock(message.buffer, 0, message.Length - MAVLINK_SIGNATURE_BLOCK_LEN + 7);
						var ctx = signit.Hash;
						// trim to 48
						Array.Resize(ref ctx, 6);

						//Console.WriteLine("rec linkid {0}, time {1} {2} {3} {4} {5} {6} {7}", message.sig[0], message.sig[1], message.sig[2], message.sig[3], message.sig[4], message.sig[5], message.sig[6], message.sigTimestamp);

						for (int i = 0; i < ctx.Length; i++)
						{
							if (ctx[i] != message.sig[7 + i])
							{
								// not this key, check next
								valid = false;
								break;
							}
						}

						if (!valid)
							continue;

						// got valid key
						MAVlist[sysid, compid].linkid = message.sig[0];

						MAVlist[sysid, compid].signingKey = AuthKey.Key;

						enableSigning(sysid, compid);

						break;
					}
				}

				if (!valid)
				{
					log.InfoFormat("Packet failed signature but passed crc");
					return MAVLinkMessage.Invalid;
				}
			} */

			// packet is now verified

			// extract wp's/rally/fence/camera feedback/params from stream, including gcs packets on playback
			if (buffer.Length >= 5)
			{
				getInfoFromStream(ref message, sysid, compid);
			}

			// if its a gcs packet - dont process further
			if (buffer.Length >= 5 && (sysid == 255 || sysid == 253) && logreadmode) // gcs packet
			{
				return message;
			}

			// update packet loss statistics
			if (!logreadmode && MAVlist[sysid, compid].packetlosttimer.AddSeconds(5) < DateTime.Now)
			{
				MAVlist[sysid, compid].packetlosttimer = DateTime.Now;
				MAVlist[sysid, compid].packetslost = (MAVlist[sysid, compid].packetslost * 0.8f);
				MAVlist[sysid, compid].packetsnotlost = (MAVlist[sysid, compid].packetsnotlost * 0.8f);
			}
			else if (logreadmode && MAVlist[sysid, compid].packetlosttimer.AddSeconds(5) < lastlogread)
			{
				MAVlist[sysid, compid].packetlosttimer = lastlogread;
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
								setAPType(sysid, compid);
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
						PacketReceived(message);

					/*if (debugmavlink)
						DebugPacket(message);*/

					try
					{
						if (logfile != null && logfile.CanWrite && !logreadmode)
						{
							lock (logfile)
							{
								byte[] datearray =
									BitConverter.GetBytes(
										(UInt64)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds * 1000));
								Array.Reverse(datearray);
								logfile.Write(datearray, 0, datearray.Length);
								logfile.Write(buffer, 0, buffer.Length);

								if (msgid == 0)
								{
									// flush on heartbeat - 1 seconds
									logfile.Flush();
									rawlogfile.Flush();
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


		private void PacketReceived(MAVLinkMessage buffer)
		{
			MAVLINK_MSG_ID type = (MAVLINK_MSG_ID)buffer.msgid;

			lock (Subscriptions)
			{
				foreach (var item in Subscriptions.ToArray())
				{
					if (item.Key == type)
					{
						try
						{
							item.Value(buffer);
						}
						catch (Exception ex)
						{
							log.Error(ex);
						}
					}
				}
			}
		}

		/// <summary>
		/// Used to extract mission from log file - both sent or received
		/// </summary>
		/// <param name="buffer">packet</param>
		private void getInfoFromStream(ref MAVLinkMessage buffer, byte sysid, byte compid)
		{
			

			if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_COUNT)
			{
				// clear old
				mavlink_mission_count_t wp = buffer.ToStructure<mavlink_mission_count_t>();

				if (wp.target_system == gcssysid)
				{
					wp.target_system = sysid;
					wp.target_component = compid;
				}

				MAVlist[wp.target_system, wp.target_component].wps.Clear();
			}
			else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM)
			{
				mavlink_mission_item_t wp = buffer.ToStructure<mavlink_mission_item_t>();

				if (wp.target_system == gcssysid)
				{
					wp.target_system = sysid;
					wp.target_component = compid;
				}


				if (wp.current == 2)
				{
					// guide mode wp
					MAVlist[wp.target_system, wp.target_component].GuidedMode = wp;
				}
				else
				{
					MAVlist[wp.target_system, wp.target_component].wps[wp.seq] = wp;
				}

				/*Console.WriteLine("WP # {7} cmd {8} p1 {0} p2 {1} p3 {2} p4 {3} x {4} y {5} z {6}", wp.param1, wp.param2,
					wp.param3, wp.param4, wp.x, wp.y, wp.z, wp.seq, wp.command);*/
			}
			else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM_INT)
			{
				mavlink_mission_item_int_t wp = buffer.ToStructure<mavlink_mission_item_int_t>();

				if (wp.target_system == gcssysid)
				{
					wp.target_system = sysid;
					wp.target_component = compid;
				}

				if (wp.current == 2)
				{
					// guide mode wp
					MAVlist[wp.target_system, wp.target_component].GuidedMode = (mavlink_mission_item_t)(Locationwp)wp;
				}
				else
				{
					MAVlist[wp.target_system, wp.target_component].wps[wp.seq] =
						(mavlink_mission_item_t)(Locationwp)wp;
				}

				/*Console.WriteLine("WP INT # {7} cmd {8} p1 {0} p2 {1} p3 {2} p4 {3} x {4} y {5} z {6}", wp.param1,
					wp.param2,
					wp.param3, wp.param4, wp.x, wp.y, wp.z, wp.seq, wp.command);*/
			}
			else if (buffer.msgid == (byte)MAVLINK_MSG_ID.RALLY_POINT)
			{
				mavlink_rally_point_t rallypt = buffer.ToStructure<mavlink_rally_point_t>();

				if (rallypt.target_system == gcssysid)
				{
					rallypt.target_system = sysid;
					rallypt.target_component = compid;
				}


				MAVlist[rallypt.target_system, rallypt.target_component].rallypoints[rallypt.idx] = rallypt;

				/*Console.WriteLine("RP # {0} {1} {2} {3} {4}", rallypt.idx, rallypt.lat, rallypt.lng, rallypt.alt,
					rallypt.break_alt);*/
			}
			else if (buffer.msgid == (byte)MAVLINK_MSG_ID.CAMERA_FEEDBACK)
			{
				mavlink_camera_feedback_t camerapt = buffer.ToStructure<mavlink_camera_feedback_t>();

				if (MAVlist[sysid, compid].camerapoints.Count == 0 ||
					MAVlist[sysid, compid].camerapoints.Last().time_usec != camerapt.time_usec)
				{
					MAVlist[sysid, compid].camerapoints.Add(camerapt);
				}
			}
			else if (buffer.msgid == (byte)MAVLINK_MSG_ID.FENCE_POINT)
			{
				mavlink_fence_point_t fencept = buffer.ToStructure<mavlink_fence_point_t>();

				if (fencept.target_system == gcssysid)
				{
					fencept.target_system = sysid;
					fencept.target_component = compid;
				}

				MAVlist[fencept.target_system, fencept.target_component].fencepoints[fencept.idx] = fencept;
			}
			else if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE)
			{
				mavlink_param_value_t value = buffer.ToStructure<mavlink_param_value_t>();

				string st = ASCIIEncoding.ASCII.GetString(value.param_id);

				int pos = st.IndexOf('\0');

				if (pos != -1)
				{
					st = st.Substring(0, pos);
				}

				MAVlist[sysid, compid].param_types[st] = (MAV_PARAM_TYPE)value.param_type;

				if (Status.apname == MAV_AUTOPILOT.ARDUPILOTMEGA && buffer.compid != (byte)MAV_COMPONENT.MAV_COMP_ID_UDP_BRIDGE)
				{
					var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
					MAVlist[sysid, compid].param[st] = new MAVLinkParam(st, BitConverter.GetBytes(value.param_value),
						MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)value.param_type);
				}
				else
				{
					var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
					MAVlist[sysid, compid].param[st] = new MAVLinkParam(st, BitConverter.GetBytes(value.param_value),
						(MAV_PARAM_TYPE)value.param_type, (MAV_PARAM_TYPE)value.param_type);
				}

				MAVlist[sysid, compid].param.TotalReported = value.param_count;
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
					sendPacket(tsync, buffer.sysid, buffer.compid);
				} // system knows the time 
				else if (tsync.tc1 > 0)
				{
					Int64 offset_ns = (tsync.ts1 + now_ns - tsync.tc1 * 2) / 2;
					Int64 dt = MAVlist[buffer.sysid, buffer.compid].time_offset_ns - offset_ns;

					if (Math.Abs(dt) > 10000000) // 10 millisecond skew
					{
						MAVlist[buffer.sysid, buffer.compid].time_offset_ns = offset_ns; // hard-set it.
					}
					else
					{
						var offset_avg_alpha = 0.6;
						var avg = (offset_avg_alpha * offset_ns) +
								  (1.0 - offset_avg_alpha) * MAVlist[buffer.sysid, buffer.compid].time_offset_ns;
						MAVlist[buffer.sysid, buffer.compid].time_offset_ns = (long)avg;
					}
				}
			}
		}


		public void setAPType(byte sysid, byte compid)
		{
			MAVlist[sysid, compid].sysid = sysid;
			MAVlist[sysid, compid].compid = compid;

			switch (MAVlist[sysid, compid].apname)
			{
				case MAV_AUTOPILOT.ARDUPILOTMEGA:
					switch (MAVlist[sysid, compid].aptype)
					{
						case MAV_TYPE.FIXED_WING:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduPlane;
							break;
						case MAV_TYPE.QUADROTOR:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.TRICOPTER:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.HEXAROTOR:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.OCTOROTOR:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.HELICOPTER:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduCopter2;
							break;
						case MAV_TYPE.GROUND_ROVER:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduRover;
							break;
						case MAV_TYPE.SUBMARINE:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduSub;
							break;
						case MAV_TYPE.ANTENNA_TRACKER:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduTracker;
							break;
						default:
							log.Error(MAVlist[sysid, compid].aptype + " not registered as valid type");
							break;
					}
					break;
				case MAV_AUTOPILOT.UDB:
					switch (MAVlist[sysid, compid].aptype)
					{
						case MAV_TYPE.FIXED_WING:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.ArduPlane;
							break;
					}
					break;
				case MAV_AUTOPILOT.GENERIC:
					switch (MAVlist[sysid, compid].aptype)
					{
						case MAV_TYPE.FIXED_WING:
							MAVlist[sysid, compid].firmware = Planner.Firmwares.Ateryx;
							break;
					}
					break;
				case MAV_AUTOPILOT.PX4:
					MAVlist[sysid, compid].firmware = Planner.Firmwares.PX4;
					break;
				default:
					switch (MAVlist[sysid, compid].aptype)
					{
						case MAV_TYPE.GIMBAL: // storm32 - name 83
							MAVlist[sysid, compid].firmware = Planner.Firmwares.Gymbal;
							break;
					}
					break;
			}
		}

		MAVLinkMessage readlogPacketMavlink()
		{
			byte[] datearray = new byte[8];

			bool missingtimestamp = false;

			if (logplaybackfile.BaseStream is FileStream)
			{
				if (((FileStream)_logplaybackfile.BaseStream).Name.ToLower().EndsWith(".rlog"))
					missingtimestamp = true;
			}

			if (!missingtimestamp)
			{
				int tem = logplaybackfile.BaseStream.Read(datearray, 0, datearray.Length);

				Array.Reverse(datearray);

				DateTime date1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

				UInt64 dateint = BitConverter.ToUInt64(datearray, 0);

				try
				{
					// array is reversed above
					if (datearray[7] == 254 || datearray[7] == 253)
					{
						//rewind 8bytes
						logplaybackfile.BaseStream.Seek(-8, SeekOrigin.Current);
					}
					else
					{
						if ((dateint / 1000 / 1000 / 60 / 60) < 9999999)
						{
							date1 = date1.AddMilliseconds(dateint / 1000);

							lastlogread = date1.ToLocalTime();
						}
					}
				}
				catch
				{
				}
			}

			byte[] temp = new byte[0];

			byte byte0 = 0;
			byte byte1 = 0;
			byte byte2 = 0;

			var filelength = logplaybackfile.BaseStream.Length;
			var filepos = logplaybackfile.BaseStream.Position;

			if (filelength == filepos)
				return MAVLinkMessage.Invalid;

			int length = 5;
			int a = 0;
			while (a < length)
			{
				if (filelength == filepos)
					return MAVLinkMessage.Invalid;

				var tempb = (byte)logplaybackfile.ReadByte();
				filepos++;

				switch (a)
				{
					case 0:
						byte0 = tempb;
						if (byte0 != 'U' && byte0 != MAVLINK_STX_MAVLINK1 && byte0 != MAVLINK_STX)
						{
							log.DebugFormat("logread - lost sync byte {0} pos {1}", byte0,
								logplaybackfile.BaseStream.Position);
							// seek to next valid
							do
							{
								byte0 = logplaybackfile.ReadByte();
							}
							while (byte0 != 'U' && byte0 != MAVLINK_STX_MAVLINK1 && byte0 != MAVLINK_STX);
							a = 1;
							continue;
						}
						break;
					case 1:
						byte1 = tempb;
						// handle length
						{
							int headerlength = byte0 == MAVLINK_STX ? 9 : 5;
							int headerlengthstx = headerlength + 1;

							length = byte1 + headerlengthstx + 2; // header + 2 checksum
						}
						break;
					case 2:
						byte2 = tempb;
						// handle signing and mavlink2
						if (byte0 == MAVLINK_STX)
						{
							if ((byte2 & MAVLINK_IFLAG_SIGNED) > 0)
								length += MAVLINK_SIGNATURE_BLOCK_LEN;
						}
						// handle rest
						{
							temp = new byte[length];
							temp[0] = byte0;
							temp[1] = byte1;
							temp[2] = byte2;

							var readto = a + 1;
							var readlength = length - (a + 1);
							logplaybackfile.Read(temp, readto, readlength);
							a = length;
						}
						break;
				}

				a++;
			}

			MAVLinkMessage tmp = new MAVLinkMessage(temp);

			MAVlist[tmp.sysid, tmp.compid].datetime = lastlogread;

			return tmp;
		}


		public bool getVersion()
		{
			mavlink_autopilot_version_request_t req = new mavlink_autopilot_version_request_t();

			req.target_component = Status.compid;
			req.target_system = Status.sysid;

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
			generatePacket(messageType, indata, Status.sysid, Status.compid);
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
			return doARM(Status.sysid, Status.compid, armit);
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
			return doCommand(Status.sysid, Status.compid, actionid, p1, p2, p3, p4, p5, p6, p7, requireack, null);
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

			return true;

			/*
			DateTime fromNow = DateTime.Now;
			log.WarnFormat("datefromnow: {0}", DateTime.Now);
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
								
			}*/
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

		public bool translateMode(byte sysid, byte compid, string modein, ref mavlink_set_mode_t mode)
		{
			mode.target_system = sysid;

			if (modein == null || modein == "")
				return false;


			try
			{
				FlightMode _out;
				foreach (string key in Enum.GetNames(typeof(FlightMode)))
				{
					if (modein == key)
					{
						mode.base_mode = (byte)MAV_MODE_FLAG.CUSTOM_MODE_ENABLED;
						if (Enum.TryParse(key, out _out))
						{
							uint value = (uint)_out;
							mode.custom_mode = value;
						}
						else
						{
							throw new Exception();
						}
						
					}
				}
					

				if (mode.base_mode == 0)
				{
					Console.WriteLine("No Mode Changed " + modein);
					return false;
				}
			}
			catch
			{
				Console.WriteLine("Failed to find Mode");
				return false;
			}

			return true;
		}

		public void setMode(string modein)
		{
			setMode(Status.sysid, Status.compid, modein);
		}

		public void setMode(byte sysid, byte compid, string modein)
		{
			mavlink_set_mode_t mode = new mavlink_set_mode_t();

			if (translateMode(sysid, compid, modein, ref mode))
			{
				setMode(sysid, compid, mode);
			}
		}

		public void setMode(mavlink_set_mode_t mode, MAV_MODE_FLAG base_mode = 0)
		{
			setMode(Status.sysid, Status.compid, mode, base_mode);
		}

		public void setMode(byte sysid, byte compid, mavlink_set_mode_t mode, MAV_MODE_FLAG base_mode = 0)
		{
			log.Info("mode switching");
			mode.base_mode |= (byte)base_mode;

			generatePacket((byte)(byte)MAVLINK_MSG_ID.SET_MODE, mode, sysid, compid);
			Thread.Sleep(10);
			generatePacket((byte)(byte)MAVLINK_MSG_ID.SET_MODE, mode, sysid, compid);
		}

		/// <summary>
		/// set param on apm, used for param rename
		/// </summary>
		/// <param name="paramname"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool setParam(string[] paramnames, double value)
		{
			foreach (string paramname in paramnames)
			{
				if (setParam(paramname, value))
				{
					return true;
				}
			}
			return false;
		}

		public bool setParam(string paramname, double value, bool force = false)
		{
			return setParam((byte)sysidcurrent, (byte)compidcurrent, paramname, value, force);
		}

		/// <summary>
		/// Set parameter on apm
		/// </summary>
		/// <param name="paramname">name as a string</param>
		/// <param name="value"></param>
		public bool setParam(byte sysid, byte compid, string paramname, double value, bool force = false)
		{
			if (!MAVlist[sysid, compid].param.ContainsKey(paramname))
			{
				log.Warn("Trying to set Param that doesnt exist " + paramname + "=" + value);
				return false;
			}

			if (MAVlist[sysid, compid].param[paramname].Value == value && !force)
			{
				log.Warn("setParam " + paramname + " not modified as same");
				return true;
			}

			giveComport = true;

			// param type is set here, however it is always sent over the air as a float 100int = 100f.
			var req = new mavlink_param_set_t
			{
				target_system = sysid,
				target_component = compid,
				param_type = (byte)MAVlist[sysid, compid].param_types[paramname]
			};

			byte[] temp = Encoding.ASCII.GetBytes(paramname);

			Array.Resize(ref temp, 16);
			req.param_id = temp;
			if (MAVlist[sysid, compid].apname == MAV_AUTOPILOT.ARDUPILOTMEGA)
			{
				req.param_value = new MAVLinkParam(paramname, value, (MAV_PARAM_TYPE.REAL32)).float_value;
			}
			else
			{
				req.param_value = new MAVLinkParam(paramname, value, (MAV_PARAM_TYPE)MAVlist[sysid, compid].param_types[paramname]).float_value;
			}

			int currentparamcount = MAVlist[sysid, compid].param.Count;

			generatePacket((byte)MAVLINK_MSG_ID.PARAM_SET, req, sysid, compid);

			log.InfoFormat("setParam '{0}' = '{1}' sysid {2} compid {3}", paramname, value, sysid,
				compid);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(700) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("setParam Retry " + retrys);
						generatePacket((byte)MAVLINK_MSG_ID.PARAM_SET, req, sysid, compid);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - setParam " + paramname);
				}

				MAVLinkMessage buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.PARAM_VALUE)
					{
						mavlink_param_value_t par = buffer.ToStructure<mavlink_param_value_t>();

						string st = ASCIIEncoding.ASCII.GetString(par.param_id);

						int pos = st.IndexOf('\0');

						if (pos != -1)
						{
							st = st.Substring(0, pos);
						}

						if (st != paramname)
						{
							log.InfoFormat("MAVLINK bad param response - {0} vs {1}", paramname, st);
							continue;
						}

						if (MAVlist[sysid, compid].apname == MAV_AUTOPILOT.ARDUPILOTMEGA)
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							MAVlist[sysid, compid].param[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)par.param_type);
						}
						else
						{
							var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
							MAVlist[sysid, compid].param[st] = new MAVLinkParam(st, BitConverter.GetBytes(par.param_value), (MAV_PARAM_TYPE)par.param_type, (MAV_PARAM_TYPE)par.param_type);
						}

						log.Info("setParam gotback " + st + " : " + MAVlist[sysid, compid].param[st]);

						lastparamset = DateTime.Now;

						giveComport = false;
						//System.Threading.Thread.Sleep(100);//(int)(8.5 * 5)); // 8.5ms per byte

						// check if enabeling this param has added subparams, queue on gui thread
						if (currentparamcount < par.param_count)
						{
							/*
							MainV2.instance.BeginInvoke((Action) delegate
							{
								Loading.ShowLoading(String.Format(Strings.ParamRefreshRequired, currentparamcount,
									par.param_count));
							});
							*/
						}

						return true;
					}
				}
			}
		}


		public void setGuidedModeWP(Locationwp gotohere, bool setguidedmode = true)
		{
			setGuidedModeWP(Status.sysid, Status.compid, gotohere, setguidedmode);
		}

		public void setGuidedModeWP(byte sysid, byte compid, Locationwp gotohere, bool setguidedmode = true)
		{
			if (gotohere.alt == 0 || gotohere.lat == 0 || gotohere.lng == 0)
				return;

			giveComport = true;

			try
			{
				gotohere.id = (ushort)MAV_CMD.WAYPOINT;

				// Must be Guided mode.s
				if (setguidedmode)
				{
					// fix for followme change
					setMode(sysid, compid, "GUIDED");
				}

				log.InfoFormat("setGuidedModeWP {0}:{1} lat {2} lng {3} alt {4}", sysid, compid, gotohere.lat, gotohere.lng, gotohere.alt);

				if (MAVlist[sysid, compid].firmware == Firmwares.ArduPlane)
				{
					MAV_MISSION_RESULT ans = setWP(sysid, compid, gotohere, 0, MAV_FRAME.GLOBAL_RELATIVE_ALT, (byte)2);

					if (ans != MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
						throw new Exception("Guided Mode Failed");
				}
				else
				{
					setPositionTargetGlobalInt((byte)sysid, (byte)compid,
						true, false, false, false, MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT_INT,
						gotohere.lat, gotohere.lng, gotohere.alt, 0, 0, 0, 0, 0);
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}

			giveComport = false;
		}


		public void setPositionTargetGlobalInt(byte sysid, byte compid, bool pos, bool vel, bool acc, bool yaw, MAV_FRAME frame, double lat, double lng, double alt, double vx, double vy, double vz, double yawangle, double yawrate)
		{
			// for mavlink SET_POSITION_TARGET messages
			const ushort MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE = ((1 << 0) | (1 << 1) | (1 << 2));
			const ushort MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE = ((0 << 0) | (0 << 1) | (1 << 2));
			const ushort MAVLINK_SET_POS_TYPE_MASK_VEL_IGNORE = ((1 << 3) | (1 << 4) | (1 << 5));
			const ushort MAVLINK_SET_POS_TYPE_MASK_ACC_IGNORE = ((1 << 6) | (1 << 7) | (1 << 8));
			const ushort MAVLINK_SET_POS_TYPE_MASK_FORCE = ((1 << 9));
			const ushort MAVLINK_SET_POS_TYPE_MASK_YAW_IGNORE = ((1 << 10) | (1 << 11));

			mavlink_set_position_target_global_int_t target = new mavlink_set_position_target_global_int_t()
			{
				target_system = sysid,
				target_component = compid,
				alt = (float)alt,
				lat_int = (int)(lat * 1e7),
				lon_int = (int)(lng * 1e7),
				coordinate_frame = (byte)frame,
				vx = (float)vx,
				vy = (float)vy,
				vz = (float)vz,
				yaw = (float)yawangle,
				yaw_rate = (float)yawrate
			};

			target.type_mask = ushort.MaxValue;

			if (pos && lat != 0 && lng != 0)
				target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE;
			if (pos && lat == 0 && lng == 0)
				target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE;
			if (vel)
				target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_VEL_IGNORE;
			if (acc)
				target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_ACC_IGNORE;
			if (yaw)
				target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_YAW_IGNORE;

			if (pos)
			{
				if (lat != 0)
					MAVlist[sysid, compid].GuidedMode.x = (float)lat;
				if (lng != 0)
					MAVlist[sysid, compid].GuidedMode.y = (float)lng;
				MAVlist[sysid, compid].GuidedMode.z = (float)alt;
			}

			bool pos_ignore = (target.type_mask & MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE) > 0;
			bool vel_ignore = (target.type_mask & MAVLINK_SET_POS_TYPE_MASK_VEL_IGNORE) > 0;
			bool acc_ignore = (target.type_mask & MAVLINK_SET_POS_TYPE_MASK_ACC_IGNORE) > 0;

			generatePacket((byte)MAVLINK_MSG_ID.SET_POSITION_TARGET_GLOBAL_INT, target, sysid, compid);
		}


		public MAV_MISSION_RESULT setWP(Locationwp loc, ushort index, MAV_FRAME frame, byte current = 0,
			byte autocontinue = 1, bool use_int = false)
		{
			return setWP(Status.sysid, Status.compid, loc, index, frame, current, autocontinue, use_int);
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

			req.target_system = Status.sysid;
			req.target_component = Status.compid;

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

				reqi.target_system = Status.sysid;
				reqi.target_component = Status.compid;

				reqi.seq = index;

				// request
				generatePacket((byte)MAVLINK_MSG_ID.MISSION_REQUEST_INT, reqi);

				req = reqi;
			}
			else
			{
				mavlink_mission_request_t reqf = new mavlink_mission_request_t();

				reqf.target_system = Status.sysid;
				reqf.target_component = Status.compid;

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

			req.target_system = Status.sysid;
			req.target_component = Status.compid;

			req.start_index = (short)startwp;
			req.end_index = (short)endwp;

			generatePacket((byte)MAVLINK_MSG_ID.MISSION_WRITE_PARTIAL_LIST, req, Status.sysid, Status.compid);
		}

		/// <summary>
		/// Sets wp total count
		/// </summary>
		/// <param name="wp_total"></param>
		public void setWPTotal(ushort wp_total)
		{
			giveComport = true;
			mavlink_mission_count_t req = new mavlink_mission_count_t();

			req.target_system = Status.sysid;
			req.target_component = Status.compid; // MSG_NAMES.MISSION_COUNT

			req.count = wp_total;

			generatePacket((byte)MAVLINK_MSG_ID.MISSION_COUNT, req, Status.sysid, Status.compid);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(700) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("setWPTotal Retry " + retrys);
						generatePacket((byte)MAVLINK_MSG_ID.MISSION_COUNT, req, Status.sysid, Status.compid);
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

						log.Info("receive mission request feedback");

						if (request.seq == 0)
						{
							if (Status.param["WP_TOTAL"] != null)
								Status.param["WP_TOTAL"].Value = wp_total - 1;
							if (Status.param["CMD_TOTAL"] != null)
								Status.param["CMD_TOTAL"].Value = wp_total - 1;
							if (Status.param["MIS_TOTAL"] != null)
								Status.param["MIS_TOTAL"].Value = wp_total - 1;

							Status.wps.Clear();

							giveComport = false;
							return;
						}
					}
					else
					{
						log.Info(DateTime.Now + " PC getwp " + buffer.msgid);
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

		public KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID type,
			Func<MAVLink.MAVLinkMessage, bool> function, bool exclusive = false)
		{
			var item = new KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>>(type, function);

			lock (Subscriptions)
			{
				if (exclusive)
				{
					foreach (var subitem in Subscriptions)
					{
						if (subitem.Key == item.Key)
						{
							Subscriptions.Remove(subitem);
							break;
						}
					}
				}

				log.Info("SubscribeToPacketType " + item.Key + " " + item.Value);

				Subscriptions.Add(item);
			}

			return item;
		}

		public void UnSubscribeToPacketType(KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> item)
		{
			lock (Subscriptions)
			{
				log.Info("UnSubscribeToPacketType " + item.Key + " " + item.Value);
				Subscriptions.Remove(item);
			}
		}

		public void UnSubscribeToPacketType(MAVLink.MAVLINK_MSG_ID msgtype, Func<MAVLink.MAVLinkMessage, bool> item)
		{
			lock (Subscriptions)
			{
				log.Info("UnSubscribeToPacketType " + msgtype + " " + item);
				var ans = Subscriptions.Where(a => { return a.Key == msgtype && a.Value == item; });
				Subscriptions.Remove(ans.First());
			}
		}

		public void setWPACK()
		{
			mavlink_mission_ack_t req = new mavlink_mission_ack_t();
			req.target_system = Status.sysid;
			req.target_component = Status.compid;
			req.type = 0;

			generatePacket((byte)MAVLINK_MSG_ID.MISSION_ACK, req, Status.sysid, Status.compid);
		}

		public bool setFencePoint(byte index, PointLatLngAlt plla, byte fencepointcount)
		{
			mavlink_fence_point_t fp = new mavlink_fence_point_t();

			fp.idx = index;
			fp.count = fencepointcount;
			fp.lat = (float)plla.Lat;
			fp.lng = (float)plla.Lng;
			fp.target_component = Status.compid;
			fp.target_system = Status.sysid;

			int retry = 3;

			PointLatLngAlt newfp;

			while (retry > 0)
			{
				generatePacket((byte)MAVLINK_MSG_ID.FENCE_POINT, fp);
				int counttemp = 0;
				newfp = getFencePoint(fp.idx, ref counttemp);

				if (newfp.GetDistance(plla) < 5)
					return true;
				retry--;
			}

			throw new Exception("Could not verify GeoFence Point");
		}

		public PointLatLngAlt getFencePoint(int no, ref int total)
		{
			MAVLinkMessage buffer;

			giveComport = true;

			PointLatLngAlt plla = new PointLatLngAlt();
			mavlink_fence_fetch_point_t req = new mavlink_fence_fetch_point_t();

			req.idx = (byte)no;
			req.target_component = Status.compid;
			req.target_system = Status.sysid;

			// request point
			generatePacket((byte)MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);

			DateTime start = DateTime.Now;
			int retrys = 3;

			while (true)
			{
				if (!(start.AddMilliseconds(700) > DateTime.Now))
				{
					if (retrys > 0)
					{
						log.Info("getFencePoint Retry " + retrys + " - giv com " + giveComport);
						generatePacket((byte)MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);
						start = DateTime.Now;
						retrys--;
						continue;
					}
					giveComport = false;
					throw new TimeoutException("Timeout on read - getFencePoint");
				}

				buffer = readPacket();
				if (buffer.Length > 5)
				{
					if (buffer.msgid == (byte)MAVLINK_MSG_ID.FENCE_POINT)
					{
						giveComport = false;

						mavlink_fence_point_t fp = buffer.ToStructure<mavlink_fence_point_t>();

						plla.Lat = fp.lat;
						plla.Lng = fp.lng;
						plla.Tag = fp.idx.ToString();

						total = fp.count;

						return plla;
					}
				}
			}
		}

		public WPsDataManager wpsManager = new WPsDataManager();

		public void SaveWpsTemp(List<Locationwp> temp) => wpsManager.Set(temp);

		public List<Locationwp> LoadWpsTemp() => wpsManager.Get();

	}
}
