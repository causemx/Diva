using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using GMap.NET.WindowsForms;
using GMap.NET;
using FooApplication.Mavlink;
using System.Reflection;
using System.Threading;
using System.Net;
using FooApplication.Utilities;

namespace FooApplication
{
	public partial class Main : Form
	{

		public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static readonly byte REQUEST_DATA_STREAM_RATE = 1; // hz 

		public static MavlinkInterface comPort
		{
			get
			{
				return _comPort;
			}
			set
			{
				if (_comPort == value)
					return;
				_comPort = value;
			}
		}

		public static MavlinkInterface _comPort = new MavlinkInterface();

		private Thread SerialReaderThread = null;
		private bool serialThread = false;  // control the thread behavior
		private DateTime heartbeatSend = DateTime.Now;
		private ManualResetEvent SerialThreadrunner = new ManualResetEvent(false); // control the thread waiting behavior
		private DateTime lastupdate = DateTime.Now;
		private DateTime lastdata = DateTime.MinValue;

		public Main()
		{
			InitializeComponent();
			comPort.MAV.GuidedMode.x = 0;
			comPort.MAV.GuidedMode.y = 0;
			comPort.MAV.GuidedMode.z = 1;
		}

		private void SerialReader()
		{
			if (serialThread == true)
				return;

			serialThread = true;
			SerialThreadrunner.Reset();

			
			while (serialThread)
			{
				Thread.Sleep(1000);

				if (heartbeatSend.Second != DateTime.Now.Second)
				{
					MAVLink.mavlink_heartbeat_t htb = new MAVLink.mavlink_heartbeat_t()
					{
						type = (byte)MAVLink.MAV_TYPE.GCS,
						autopilot = (byte)MAVLink.MAV_AUTOPILOT.INVALID,
						mavlink_version = 3 // MAVLink.MAVLINK_VERSION
					};

					if (!comPort.BaseStream.IsOpen) continue;
					comPort.sendPacket(htb, comPort.sysid, comPort.compid);

				}

				heartbeatSend = DateTime.Now;

				if (!comPort.BaseStream.IsOpen || comPort.giveComport == true)
				{
					if (!comPort.BaseStream.IsOpen)
					{
						System.Threading.Thread.Sleep(100);
					}
				}

				UpdateCurrentSettings(true);

			}

			Console.WriteLine("serialreader done");
			SerialThreadrunner.Set();

		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			SerialReaderThread = new Thread(SerialReader)
			{
				IsBackground = true,
				Name = "Main Serial reader",
				Priority = ThreadPriority.AboveNormal
			};
			SerialReaderThread.Start();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			serialThread = false;
			if (SerialReaderThread != null)
				SerialReaderThread.Join();
		}

		public void UpdateCurrentSettings(bool updatenow)
		{
			MAVLink.MAVLinkMessage mavlinkMessage = comPort.readPacket();

			lock (this)
			{
				if (DateTime.Now > lastupdate.AddMilliseconds(50) || updatenow) // 20 hz
				{
					lastupdate = DateTime.Now;


					// re-request streams
					if (!(lastdata.AddSeconds(8) > DateTime.Now) && comPort.BaseStream.IsOpen)
					{
						try
						{
							comPort.getDatastream(MAVLink.MAV_DATA_STREAM.ALL, REQUEST_DATA_STREAM_RATE);
							comPort.getDatastream(MAVLink.MAV_DATA_STREAM.EXTENDED_STATUS, REQUEST_DATA_STREAM_RATE);
							comPort.getDatastream(MAVLink.MAV_DATA_STREAM.EXTRA1, REQUEST_DATA_STREAM_RATE);
							comPort.getDatastream(MAVLink.MAV_DATA_STREAM.EXTRA2, REQUEST_DATA_STREAM_RATE);
							comPort.getDatastream(MAVLink.MAV_DATA_STREAM.EXTRA3, REQUEST_DATA_STREAM_RATE);
							comPort.getDatastream(MAVLink.MAV_DATA_STREAM.POSITION, REQUEST_DATA_STREAM_RATE);
						}
						catch
						{
							log.Error("Failed to request rates");
						}
						lastdata = DateTime.Now.AddSeconds(30); // prevent flooding
					}

					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.POWER_STATUS))
					{
						if (mavlinkMessage != null)
						{
							var power = mavlinkMessage.ToStructure<MAVLink.mavlink_power_status_t>();

							ushort boardvoltage = power.Vcc;
							ushort servovoltage = power.Vservo;

							try
							{
								MAVLink.MAV_POWER_STATUS voltageflag = (MAVLink.MAV_POWER_STATUS)power.flags;
							}
							catch
							{

							}
						}
					}


					if (mavlinkMessage.msgid == ((uint)MAVLink.MAVLINK_MSG_ID.HEARTBEAT))
					{
						if (mavlinkMessage != null)
						{
							var hb = mavlinkMessage.ToStructure<MAVLink.mavlink_heartbeat_t>();

							if (hb.type == (byte)MAVLink.MAV_TYPE.GCS)
							{
								// skip gcs hb's
								// only happens on log playback - and shouldnt get them here
							}
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
								Console.WriteLine("no position");
							}
							else
							{
								double lat = loc.lat / 10000000.0;
								double lng = loc.lon / 10000000.0;

								double altasl = loc.alt / 1000.0f;

								double vx = loc.vx * 0.01;
								double vy = loc.vy * 0.01;
								double vz = loc.vz * 0.01;
							}
						}
					}
					
					/**

					mavLinkMessage = MAV.getPacket((uint)MAVLink.MAVLINK_MSG_ID.GPS_RAW_INT);
					if (mavLinkMessage != null)
					{
						var gps = mavLinkMessage.ToStructure<MAVLink.mavlink_gps_raw_int_t>();

						if (!useLocation)
						{
							lat = gps.lat * 1.0e-7;
							lng = gps.lon * 1.0e-7;

							altasl = gps.alt / 1000.0f;
							// alt = gps.alt; // using vfr as includes baro calc
						}

						gpsstatus = gps.fix_type;
						//                    Console.WriteLine("gpsfix {0}",gpsstatus);

						gpshdop = (float)Math.Round((double)gps.eph / 100.0, 2);

						satcount = gps.satellites_visible;

						groundspeed = gps.vel * 1.0e-2f;
						groundcourse = gps.cog * 1.0e-2f;

						//MAVLink.packets[(byte)MAVLink.MSG_NAMES.GPS_RAW);
					}

					mavLinkMessage = MAV.getPacket((uint)MAVLink.MAVLINK_MSG_ID.GPS2_RAW);
					if (mavLinkMessage != null)
					{
						var gps = mavLinkMessage.ToStructure<MAVLink.mavlink_gps2_raw_t>();

						lat2 = gps.lat * 1.0e-7;
						lng2 = gps.lon * 1.0e-7;
						altasl2 = gps.alt / 1000.0f;

						gpsstatus2 = gps.fix_type;
						gpshdop2 = (float)Math.Round((double)gps.eph / 100.0, 2);

						satcount2 = gps.satellites_visible;

						groundspeed2 = gps.vel * 1.0e-2f;
						groundcourse2 = gps.cog * 1.0e-2f;
					}

					mavLinkMessage = MAV.getPacket((uint)MAVLink.MAVLINK_MSG_ID.GPS_STATUS);
					if (mavLinkMessage != null)
					{
						var gps = mavLinkMessage.ToStructure<MAVLink.mavlink_gps_status_t>();
						satcount = gps.satellites_visible;
					}

					mavLinkMessage = MAV.getPacket((uint)MAVLink.MAVLINK_MSG_ID.RADIO);
					if (mavLinkMessage != null)
					{
						var radio = mavLinkMessage.ToStructure<MAVLink.mavlink_radio_t>();
						rssi = radio.rssi;
						remrssi = radio.remrssi;
						txbuffer = radio.txbuf;
						rxerrors = radio.rxerrors;
						noise = radio.noise;
						remnoise = radio.remnoise;
						fixedp = radio.@fixed;
					}

					mavLinkMessage = MAV.getPacket((uint)MAVLink.MAVLINK_MSG_ID.RADIO_STATUS);
					if (mavLinkMessage != null)
					{
						var radio = mavLinkMessage.ToStructure<MAVLink.mavlink_radio_status_t>();
						rssi = radio.rssi;
						remrssi = radio.remrssi;
						txbuffer = radio.txbuf;
						rxerrors = radio.rxerrors;
						noise = radio.noise;
						remnoise = radio.remnoise;
						fixedp = radio.@fixed;
					}
					**/
				}
			}
		}


		private void gMapControl1_Load(object sender, EventArgs e)
		{
			gmapControl.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
			gmapControl.SetPositionByKeywords("Paris, France");
			GMapOverlay overlay1 = new GMapOverlay("overlay1");
			gmapControl.Overlays.Add(overlay1);


			gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap
			gMapControl1.MinZoom = 0;
			gMapControl1.MaxZoom = 24;
			gMapControl1.Zoom = 3;

			gMapControl1.OnMapZoomChanged += gMapControl1_OnMapZoomChanged;

			gMapControl1.DisableFocusOnMouseEnter = true;

			gMapControl1.OnMarkerEnter += gMapControl1_OnMarkerEnter;
			gMapControl1.OnMarkerLeave += gMapControl1_OnMarkerLeave;

			gMapControl1.RoutesEnabled = true;
			gMapControl1.PolygonsEnabled = true;
		}

		private void btn_connect_Click(object sender, EventArgs e)
		{
			comPort.open();
		}

		private void btn_arm_Click(object sender, EventArgs e)
		{
			if (!comPort.BaseStream.IsOpen)
			{
				log.Info("basestream have opened");
				return;
			}
			
			// arm the MAV
			try
			{
				bool ans = comPort.doARM(true);
				if (ans == false)
					log.Info("arm failed");
			}
			catch
			{
				log.Info("unknown arm failed");
			}
		}

		private void btn_takeoff_Click(object sender, EventArgs e)
		{
			if (comPort.BaseStream.IsOpen)
			{
				// flyToHereAltToolStripMenuItem_Click(null, null);

				comPort.setMode(
					comPort.sysid, 
					comPort.compid, 
					new MAVLink.mavlink_set_mode_t()
					{
						target_system = comPort.sysid,
						base_mode = (byte)MAVLink.MAV_MODE_FLAG.CUSTOM_MODE_ENABLED,
						custom_mode = (uint)5,
					});

				try
				{
					comPort.doCommand(MAVLink.MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, comPort.MAV.GuidedMode.z);
				}
				catch
				{
					log.Info("unknown takeoff failed");
				}
			}
		}

		private void btn_land_Click(object sender, EventArgs e)
		{
			if (comPort.BaseStream.IsOpen)
			{
				comPort.setMode(
					comPort.sysid,
					comPort.compid,
					new MAVLink.mavlink_set_mode_t()
					{
						target_system = comPort.sysid,
						base_mode = (byte)MAVLink.MAV_MODE_FLAG.CUSTOM_MODE_ENABLED,
						custom_mode = (uint)9,
					});
				try
				{
					comPort.doCommand(MAVLink.MAV_CMD.LAND, 0, 0, 0, 0, 0, 0, 0);
				}
				catch
				{
					log.Info("unknown land failed");
				}
			}
		}
	}

}
