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
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using FooApplication.Controls;

namespace FooApplication
{
	public partial class Main : Form
	{

		public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static readonly byte REQUEST_DATA_STREAM_RATE = 1; // hz
		public static readonly int NUM_TRACK_POINT = 1;

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
		private List<PointLatLng> trackPoints = new List<PointLatLng>();
		private GMapOverlay routes;
		private GMapOverlay markers;
		private GMapRoute route;

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
				updateMapPosition(new PointLatLng(24.7726628, 121.0468916));


				// Update the tracking point
				if (route == null)
				{
					route = new GMapRoute(trackPoints, "track");
					routes.Routes.Add(route);
				}

				PointLatLng currentloc = new PointLatLng(comPort.MAV.current_lat, comPort.MAV.current_lng);

				gMapControl1.HoldInvalidation = true;

				// maintain route history length
				if (route.Points.Count > NUM_TRACK_POINT)
				{
					route.Points.RemoveRange(0,
						route.Points.Count - NUM_TRACK_POINT);
				}
				

				// add new route point
				if (comPort.MAV.current_lat != 0 && comPort.MAV.current_lng != 0)
				{
					route.Points.Add(currentloc);
				}

				if (!this.IsHandleCreated)
					continue;

				updateDronePosition(new PointLatLng(24.7726628, 121.0468916));
				//updateRoutePosition();
				// updateClearRoutesMarkers();



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
				}
			}
		}


		private void gMapControl1_Load(object sender, EventArgs e)
		{

			gmapControl.MapProvider = OpenStreetMapProvider.Instance;

			routes = new GMapOverlay("routes");
			markers = new GMapOverlay("markers");
			gmapControl.Overlays.Add(routes);
			gmapControl.Overlays.Add(markers);

		}

		internal PointLatLng MouseDownStart;

		private void gMapControl1_MouseDown(object sender, MouseEventArgs e)
		{
			Console.WriteLine("mouse down");

			MouseDownStart = gMapControl1.FromLocalToLatLng(e.X, e.Y);

			if (ModifierKeys == Keys.Control)
			{
				goHereToolStripMenuItem_Click(null, null);
			}
			/**
			if (gMapControl1.IsMouseOverMarker)
			{
				if (CurrentGMapMarker is GMapMarkerADSBPlane)
				{
					var marker = CurrentGMapMarker as GMapMarkerADSBPlane;
					if (marker.Tag is adsb.PointLatLngAltHdg)
					{
						var plla = marker.Tag as adsb.PointLatLngAltHdg;
						plla.DisplayICAO = !plla.DisplayICAO;
					}
				}
			}*/
		}

		private void goHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Console.WriteLine("go here");
			
			if (!comPort.BaseStream.IsOpen)
			{
				// CustomMessageBox.Show(Strings.PleaseConnect, Strings.ERROR);
				MessageBox.Show("no connection");
				return;
			}
			
			if (comPort.MAV.GuidedMode.z == 0)
			{
				// flyToHereAltToolStripMenuItem_Click(null, null);

				if (comPort.MAV.GuidedMode.z == 0)
					return;
			}

			if (MouseDownStart.Lat == 0 || MouseDownStart.Lng == 0)
			{
				// CustomMessageBox.Show(Strings.BadCoords, Strings.ERROR);
				MessageBox.Show("can not get position");
				return;
			}

			Locationwp gotohere = new Locationwp();

			gotohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
			gotohere.alt = comPort.MAV.GuidedMode.z; // back to m
			gotohere.lat = (MouseDownStart.Lat);
			gotohere.lng = (MouseDownStart.Lng);

			try
			{
				comPort.setGuidedModeWP(gotohere);
			}
			catch (Exception ex)
			{
				comPort.giveComport = false;
				MessageBox.Show(ex.Message);
			}
		}


		private void updateDronePosition(PointLatLng loc)
		{
			Invoke((MethodInvoker)delegate
			{
				try
				{
					markers.Markers.Clear();
					markers.Markers.Add(new GMarkerGoogle(loc, GMarkerGoogleType.green));
				}
				catch
				{
				}
			});
		}

		DateTime lastmapposchange = DateTime.MinValue;
		private void updateMapPosition(PointLatLng currentloc)
		{
			Invoke((MethodInvoker)delegate
			{
				try
				{
					if (lastmapposchange.Second != DateTime.Now.Second)
					{
						if (Math.Abs(currentloc.Lat - gmapControl.Position.Lat) > 0.0001 || Math.Abs(currentloc.Lng - gmapControl.Position.Lng) > 0.0001)
						{
							gmapControl.Position = currentloc;
						}

						lastmapposchange = DateTime.Now;
					}
					//hud1.Refresh();
				}
				catch
				{
				}
			});
		}

		private void updateRoutePosition()
		{
			// not async
			Invoke((MethodInvoker)delegate
			{
				gmapControl.UpdateRouteLocalPosition(route);
			});
		}

		private void updateClearRoutesMarkers()
		{
			Invoke((MethodInvoker)delegate
			{
				routes.Markers.Clear();
			});
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
					// comPort.doCommand(MAVLink.MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, comPort.MAV.GuidedMode.z);
					comPort.doCommand(MAVLink.MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, 1);
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
