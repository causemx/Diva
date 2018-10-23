using Diva.Comms;
using Diva.Controls;
using Diva.Mavlink;
using Diva.Mission;
using Diva.Properties;
using Diva.Utilities;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net;
using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static Diva.Utilities.OverlayUtility.WPOverlay;
using ResStrings = Diva.Properties.Strings;

namespace Diva
{
	public partial class Planner : Form
	{
		public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public const double DEFAULT_LATITUDE = 24.773518;
		public const double DEFAULT_LONGITUDE = 121.0443385;
		public const double DEFAULT_ZOOM = 20;
		public const int TAKEOFF_HEIGHT = 30;
		public const int CURRENTSTATE_MULTIPLERDIST = 1;

        private static Planner Instance = null;
        internal static Planner GetPlannerInstance() => Instance;
        internal static MavlinkInterface GetActiveDrone() => Instance?.ActiveDrone;

        private MavlinkInterface ActiveDrone = new MavlinkInterface();
        private List<MavDrone> OnlineDrones = new List<MavDrone>();

        public bool autopan { get; set; }

		private static readonly double WARN_ALT = 2D;

		private class plannerOverlays
		{
			public GMapOverlay kmlpolygons;
			public GMapOverlay rallypoints;
			public GMapOverlay polygons;
			public GMapOverlay airports;
			public GMapOverlay objects;
			public GMapOverlay commons;
			public GMapOverlay drawnpolygons;
			public GMapOverlay geofence;
			public GMapOverlay POI;
			public GMapOverlay routes;
			internal plannerOverlays(MyGMap map)
				=> GetType().GetFields().ToList().ForEach(f =>
					{
						var o = new GMapOverlay(f.Name);
						if (map != null) map.Overlays.Add(o);
						f.SetValue(this, o);
					});
		}
		private plannerOverlays overlays;

		private GMapMarkerRect currentRectMarker;
		private GMapMarkerRallyPt currentRallyPt;

		private GMapMarker currentMarker;
		private GMapMarker center = new GMarkerGoogle(new PointLatLng(0.0, 0.0), GMarkerGoogleType.none);
		private GMapMarker currentGMapMarker;

		public GMapRoute route = new GMapRoute("wp route");
		public GMapRoute homeroute = new GMapRoute("home route");

		private bool isMouseDown;
		private bool isMouseDraging;
		private bool isMouseClickOffMenu;
		private PointLatLng MouseDownStart;
		internal PointLatLng MouseDownEnd;

		// polygon
		internal GMapPolygon geofencePolygon;
		internal GMapPolygon drawnPolygon;
		internal GMapPolygon wpPolygon;
		

		private bool quickadd = false;
		private int selectedrow = 0;

		private Dictionary<string, string[]> cmdParamNames = new Dictionary<string, string[]>();
		private List<List<Locationwp>> history = new List<List<Locationwp>>();
		private List<int> groupmarkers = new List<int>();
		private Object thisLock = new Object();
		public List<PointLatLngAlt> pointlist = new List<PointLatLngAlt>(); // used to calc distance
		public List<PointLatLngAlt> fullpointlist = new List<PointLatLngAlt>();


		// Thread setup
		private Thread mainThread = null;
		private Thread updateMapItemThread = null;
		private bool serialThread = false;

		private DateTime heartbeatSend = DateTime.Now;
		private DateTime lastupdate = DateTime.Now;
		private DateTime lastdata = DateTime.MinValue;
		private DateTime mapupdate = DateTime.MinValue;

		private long recorder_id = 0;

		private bool isMapFocusing = true;

		public enum AltitudeMode
		{
			Relative = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT,
			Absolute = MAVLink.MAV_FRAME.GLOBAL,
			Terrain = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT
		}

		public enum FlightMode
		{
			STABILIZE = 0,
			ACRO = 1,
			ALT_HOLD = 2,
			AUTO = 3,
			GUIDED = 4,
			LOITER = 5,
			RTL = 6,
			CIRCLE = 7,
			POSITION = 8,
			LAND = 9,
			OF_LOITER = 10,
			DRIFT = 11,
			SPORT = 13,
			FLIP = 14,
			AUTOTUNE = 15,
			POSHOLD = 16,
			BRAKE = 17,
			THROW = 18,
			AVOID_ADSB = 19,
			GUIDED_NOGPS = 20,
		}

		public enum Firmwares
		{
			ArduPlane,
			ArduCopter2,
			ArduRover,
			ArduSub,
			Ateryx,
			ArduTracker,
			Gymbal,
			PX4
		}

		internal MyGMap GMapControl => myMap;

		public Planner()
		{
			InitializeComponent();
            Instance = this;

			// control size may not be the same as designer (dpi setting?)
						
			quickadd = false;

			overlays = new plannerOverlays(myMap);

			overlays.objects.Markers.Clear();

			// set current marker
			currentMarker = new GMarkerGoogle(myMap.Position, GMarkerGoogleType.red);
			//top.Markers.Add(currentMarker);

			// map center
			center = new GMarkerGoogle(myMap.Position, GMarkerGoogleType.none);
			//top.Markers.Add(center);

			//myMap.Zoom = 3;

			// RegeneratePolygon();
			updateCMDParams();

			foreach (DataGridViewColumn commandsColumn in dgvWayPoints.Columns)
			{
				if (commandsColumn is DataGridViewTextBoxColumn)
					commandsColumn.CellTemplate.Value = "0";
			}

			dgvWayPoints.Columns[colDelete.Index].CellTemplate.Value = "X";

			//setup push toolstripbutton
			TSBtnTagging.CheckOnClick = true;
			TSBtnTagging.CheckedChanged += new EventHandler(BUT_Tagging_CheckedChanged);

			//setup toolstrip
			TSMainPanel.Renderer = new Controls.Components.MyTSRenderer();
			TSZoomPanel.Renderer = new Controls.Components.MyTSRenderer();
			//Collect DroneInfoPanels

			// setup geofence

			List<PointLatLng> polygonPoints = new List<PointLatLng>();
			geofencePolygon = new GMapPolygon(polygonPoints, "geofence");
			geofencePolygon.Stroke = new Pen(Color.Pink, 5);
			geofencePolygon.Fill = Brushes.Transparent;

			//setup drawnpolgon
			List<PointLatLng> polygonPoints2 = new List<PointLatLng>();
			drawnPolygon = new GMapPolygon(polygonPoints2, "drawnpoly");
			drawnPolygon.Stroke = new Pen(Color.Red, 2);
			drawnPolygon.Fill = Brushes.Transparent;

			//set home
			double lng = DEFAULT_LONGITUDE, lat = DEFAULT_LATITUDE, zoom = DEFAULT_ZOOM;
            try
			{
				string loc = ConfigData.GetOption(ConfigData.OptionName.MapInitialLocation);
				if (loc != "")
				{
					string[] locs = loc.Split(',');
					if (locs.Length > 1)
					{
						double.TryParse(locs[0], out lat);
						double.TryParse(locs[1], out lng);
						if (locs.Length > 2)
							double.TryParse(locs[2], out zoom);
					}
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
			myMap.Position = new PointLatLng(lat, lng);
			myMap.Zoom = zoom;
			TxtHomeLatitude.Text = lat.ToString();
			TxtHomeLongitude.Text = lng.ToString();

		}
	
		private void Planner_Load(object sender, EventArgs e)
		{
			FlightRecorder recorder = new FlightRecorder()
			{
				UserName = ResStrings.StrAnonymousAccount,
				StartTime = DatabaseManager.DateTimeSQLite(DateTime.Now),
				EndTime = DatabaseManager.DateTimeSQLite(DateTime.Now),
				TotalDistance = 0.0d,
				HomeLatitude = 0.0d,
				HomeLongitude = 0.0d,
				HomeAltitude = 0.0d,
			};

			DatabaseManager.InitialDatabase();
			recorder_id = DatabaseManager.InsertValue(recorder);

			mainThread = new Thread(MainLoop)
			{
				IsBackground = true,
				Name = "Main Serial reader",
				Priority = ThreadPriority.AboveNormal
			};
			mainThread.Start();

			updateMapItemThread = new Thread(MapitemUpdateLoop) { IsBackground = true };
			updateMapItemThread.Start();
			isUpdatemapThreadRun = true;
		}

		private void Planner_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (updateMapItemThread != null)
            {
                isUpdatemapThreadRun = false;
                updateMapItemThread = null;
            }
            if (mainThread != null)
			{
				serialThread = false;
				e.Cancel = true;
				mainThread = null;
			}
		}

		private void Planner_FormClosed(object sender, FormClosedEventArgs e)
		{
			DatabaseManager.UpdateEndTime(recorder_id, DatabaseManager.DateTimeSQLite(DateTime.Now));
			DatabaseManager.Dump(recorder_id);
            OnlineDrones.ForEach(d => d.Disconnect());
		}

		private void MainLoop()
		{
			if (serialThread == true)
				return;

			serialThread = true;

			while (serialThread)
			{
				Thread.Sleep(20);
				if (ActiveDrone.BaseStream.IsOpen)
				{
		
					Invoke((MethodInvoker)delegate
					{
						foreach (int mode in Enum.GetValues(typeof(FlightMode)))
						{
							if ((uint)mode == ActiveDrone.Status.mode)
							{
								TxtDroneMode.Text = Enum.GetName(typeof(FlightMode), mode);
							}
						}

                        DroneInfoPanel.UpdateDisplayInfo();
                    });

					PointLatLng currentloc = new PointLatLng(ActiveDrone.Status.current_lat, ActiveDrone.Status.current_lng);
					
					if (ActiveDrone.Status.current_lat != 0 && ActiveDrone.Status.current_lng != 0)
					{
						UpdateMapPosition(currentloc);
					}
				}
			}

			mainThread = null;
			Invoke((MethodInvoker)(() => Close()));
		}

		
		DateTime lastmapposchange = DateTime.MinValue;
		private void UpdateMapPosition(PointLatLng currentloc)
		{

			if (!isMapFocusing) return;

			Invoke((MethodInvoker)delegate
			{
				try
				{
					if (lastmapposchange.Second != DateTime.Now.Second)
					{
						if (Math.Abs(currentloc.Lat - myMap.Position.Lat) > 0.0001 || Math.Abs(currentloc.Lng - myMap.Position.Lng) > 0.0001)
						{
							myMap.Position = currentloc;
						}

						lastmapposchange = DateTime.Now;
					}
				}
				catch
				{
				}
			});
		}

		private void updateRowNumbers()
		{
			// number rows 
			this.BeginInvoke((MethodInvoker)delegate
			{
				// thread for updateing row numbers
				for (int a = 0; a < dgvWayPoints.Rows.Count - 0; a++)
				{
					try
					{
						if (dgvWayPoints.Rows[a].HeaderCell.Value == null)
						{
							//Commands.Rows[a].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
							dgvWayPoints.Rows[a].HeaderCell.Value = (a + 1).ToString();
						}
						// skip rows with the correct number
						string rowno = dgvWayPoints.Rows[a].HeaderCell.Value.ToString();
						if (!rowno.Equals((a + 1).ToString()))
						{
							// this code is where the delay is when deleting.
							dgvWayPoints.Rows[a].HeaderCell.Value = (a + 1).ToString();
						}
					}
					catch (Exception)
					{
					}
				}
			});
		}

		private void updateCMDParams()
		{
			cmdParamNames = readCMDXML();

			List<string> cmds = new List<string>();

			foreach (string item in cmdParamNames.Keys)
			{
				cmds.Add(item);
			}

			cmds.Add("UNKNOWN");

			colCommand.DataSource = cmds;
		}
		

		Dictionary<string, string[]> readCMDXML()
		{
			Dictionary<string, string[]> cmd = new Dictionary<string, string[]>();
						
			using (var file = new MemoryStream(Encoding.UTF8.GetBytes(Resources.mavcmd)))
			using (XmlReader reader = XmlReader.Create(file))
			{
				reader.Read();
				reader.ReadStartElement("CMD");
				if (ActiveDrone.Status.firmware == Firmwares.ArduPlane ||
					ActiveDrone.Status.firmware == Firmwares.Ateryx)
				{
					reader.ReadToFollowing("APM");
				}
				else if (ActiveDrone.Status.firmware == Firmwares.ArduRover)
				{
					reader.ReadToFollowing("APRover");
				}
				else
				{
					reader.ReadToFollowing("AC2");
				}

				XmlReader inner = reader.ReadSubtree();

				inner.Read();

				inner.MoveToElement();

				inner.Read();

				while (inner.Read())
				{
					inner.MoveToElement();
					if (inner.IsStartElement())
					{
						string cmdname = inner.Name;
						string[] cmdarray = new string[7];
						int b = 0;

						XmlReader inner2 = inner.ReadSubtree();

						inner2.Read();

						while (inner2.Read())
						{
							inner2.MoveToElement();
							if (inner2.IsStartElement())
							{
								cmdarray[b] = inner2.ReadString();
								b++;
							}
						}

						cmd[cmdname] = cmdarray;
					}
				}
			}

			return cmd;
		}


		#region GMap event handlers - move to MyGMap.cs when possible

		private void MainMap_OnCurrentPositionChanged(PointLatLng point)
		{
			if (point.Lat > 90)
			{
				point.Lat = 90;
			}
			if (point.Lat < -90)
			{
				point.Lat = -90;
			}
			if (point.Lng > 180)
			{
				point.Lng = 180;
			}
			if (point.Lng < -180)
			{
				point.Lng = -180;
			}
			center.Position = point;
		}

		void groupmarkeradd(GMapMarker marker)
		{
			System.Diagnostics.Debug.WriteLine("add marker " + marker.Tag.ToString());
			groupmarkers.Add(int.Parse(marker.Tag.ToString()));
			if (marker is GMapMarkerWP)
			{
				((GMapMarkerWP)marker).selected = true;
			}
			if (marker is GMapMarkerRect)
			{
				((GMapMarkerWP)((GMapMarkerRect)marker).InnerMarker).selected = true;
			}
		}

		private void MainMap_MouseUp(object sender, MouseEventArgs e)
		{
			if (isMouseClickOffMenu)
			{
				isMouseClickOffMenu = false;
				return;
			}

			// check if the mouse up happend over our button
			/*
			if (polyicon.Rectangle.Contains(e.Location))
			{
				polyicon.IsSelected = !polyicon.IsSelected;

				if (e.Button == MouseButtons.Right)
				{
					polyicon.IsSelected = false;
					clearPolygonToolStripMenuItem_Click(this, null);

					contextMenuStrip1.Visible = false;

					return;
				}

				if (polyicon.IsSelected)
				{
					polygongridmode = true;
				}
				else
				{
					polygongridmode = false;
				}

				return;
			}*/

			MouseDownEnd = myMap.FromLocalToLatLng(e.X, e.Y);

			// Console.WriteLine("MainMap MU");

			if (e.Button == MouseButtons.Right) // ignore right clicks
			{
				return;
			}

			if (isMouseDown) // mouse down on some other object and dragged to here.
			{
				// drag finished, update poi db
				/*
				if (CurrentPOIMarker != null)
				{
					POI.POIMove(CurrentPOIMarker);
					CurrentPOIMarker = null;
				}*/

				if (e.Button == MouseButtons.Left)
				{
					isMouseDown = false;
				}
				if (ModifierKeys == Keys.Control)
				{
					// group select wps
					GMapPolygon poly = new GMapPolygon(new List<PointLatLng>(), "temp");

					poly.Points.Add(MouseDownStart);
					poly.Points.Add(new PointLatLng(MouseDownStart.Lat, MouseDownEnd.Lng));
					poly.Points.Add(MouseDownEnd);
					poly.Points.Add(new PointLatLng(MouseDownEnd.Lat, MouseDownStart.Lng));

					foreach (var marker in overlays.objects.Markers)
					{
						if (poly.IsInside(marker.Position))
						{
							try
							{
								if (marker.Tag != null)
								{
									groupmarkeradd(marker);
								}
							}
							catch (Exception ex)
							{
								log.Error(ex);
							}
						}
					}

					isMouseDraging = false;
					return;
				}
				if (!isMouseDraging)
				{
					if (currentRectMarker != null)
					{
						// cant add WP in existing rect
					}
					else
					{
						AddWPToMap(currentMarker.Position.Lat, currentMarker.Position.Lng, 0);
					}
				}
				else
				{
					if (groupmarkers.Count > 0)
					{
						Dictionary<string, PointLatLng> dest = new Dictionary<string, PointLatLng>();

						foreach (var markerid in groupmarkers)
						{
							for (int a = 0; a < overlays.objects.Markers.Count; a++)
							{
								var marker = overlays.objects.Markers[a];

								if (marker.Tag != null && marker.Tag.ToString() == markerid.ToString())
								{
									dest[marker.Tag.ToString()] = marker.Position;
									break;
								}
							}
						}

						foreach (KeyValuePair<string, PointLatLng> item in dest)
						{
							var value = item.Value;
							quickadd = true;
							callMeDrag(item.Key, value.Lat, value.Lng, -1);
							quickadd = false;
						}

						myMap.SelectedArea = RectLatLng.Empty;
						groupmarkers.Clear();
						// redraw to remove selection
						writeKMLV2();

						currentRectMarker = null;
					}

					if (currentRectMarker != null && currentRectMarker.InnerMarker != null)
					{
						if (currentRectMarker.InnerMarker.Tag.ToString().Contains("grid")
							&& !currentRectMarker.InnerMarker.Tag.ToString().Contains("_cus_"))
						{
							try
							{
								drawnPolygon.Points[
									int.Parse(currentRectMarker.InnerMarker.Tag.ToString().Replace("grid", "")) - 1] =
									new PointLatLng(MouseDownEnd.Lat, MouseDownEnd.Lng);
								myMap.UpdatePolygonLocalPosition(drawnPolygon);
								myMap.Invalidate();
							}
							catch (Exception ex)
							{
								log.Error(ex);
							}
						}
						else
						{
							callMeDrag(currentRectMarker.InnerMarker.Tag.ToString(), currentMarker.Position.Lat,
								currentMarker.Position.Lng, -2);
						}
						
						currentRectMarker = null;
					}
				}
			}

			isMouseDraging = false;
		}

		private void MainMap_MouseDown(object sender, MouseEventArgs e)
		{
			if (isMouseClickOffMenu)
				return;

			MouseDownStart = myMap.FromLocalToLatLng(e.X, e.Y);

			// Console.WriteLine("MainMap MD");

			if (e.Button == MouseButtons.Left && (groupmarkers.Count > 0 || ModifierKeys == Keys.Control))
			{
				// group move
				isMouseDown = true;
				isMouseDraging = false;

				return;
			}

			if (e.Button == MouseButtons.Left && ModifierKeys != Keys.Alt && ModifierKeys != Keys.Control)
			{
				isMouseDown = true;
				isMouseDraging = false;

				if (currentMarker.IsVisible)
				{
					currentMarker.Position = myMap.FromLocalToLatLng(e.X, e.Y);
				}
			}
		}

		// move current marker with left holding
		private void MainMap_MouseMove(object sender, MouseEventArgs e)
		{
			PointLatLng point = myMap.FromLocalToLatLng(e.X, e.Y);

			if (MouseDownStart == point)
				return;

			//  Console.WriteLine("MainMap MM " + point);

			currentMarker.Position = point;

			if (!isMouseDown)
			{
				// update mouse pos display
				// SetMouseDisplay(point.Lat, point.Lng, 0);
			}

			//draging
			if (e.Button == MouseButtons.Left && isMouseDown)
			{
				isMouseDraging = true;
				if (currentRallyPt != null)
				{
					PointLatLng pnew = myMap.FromLocalToLatLng(e.X, e.Y);

					currentRallyPt.Position = pnew;
				}
				else if (groupmarkers.Count > 0)
				{
					// group drag

					double latdif = MouseDownStart.Lat - point.Lat;
					double lngdif = MouseDownStart.Lng - point.Lng;

					MouseDownStart = point;

					Hashtable seen = new Hashtable();

					foreach (var markerid in groupmarkers)
					{
						if (seen.ContainsKey(markerid))
							continue;

						seen[markerid] = 1;
						for (int a = 0; a < overlays.objects.Markers.Count; a++)
						{
							var marker = overlays.objects.Markers[a];

							if (marker.Tag != null && marker.Tag.ToString() == markerid.ToString())
							{
								var temp = new PointLatLng(marker.Position.Lat, marker.Position.Lng);
								temp.Offset(latdif, -lngdif);
								marker.Position = temp;
							}
						}
					}
				}
				else if (currentRectMarker != null) // left click pan
				{
					try
					{
						// check if this is a grid point
						if (currentRectMarker.InnerMarker.Tag.ToString().Contains("grid")
							&& !currentRectMarker.InnerMarker.Tag.ToString().Contains("_cus_"))
						{
							drawnPolygon.Points[
								int.Parse(currentRectMarker.InnerMarker.Tag.ToString().Replace("grid", "")) - 1] =
								new PointLatLng(point.Lat, point.Lng);
							myMap.UpdatePolygonLocalPosition(drawnPolygon);
							myMap.Invalidate();
						}
					}
					catch (Exception ex)
					{
						log.Error(ex);
					}

					PointLatLng pnew = myMap.FromLocalToLatLng(e.X, e.Y);

					// adjust polyline point while we drag
					try
					{
						if (currentGMapMarker != null && currentGMapMarker.Tag is int)
						{
							int? pIndex = (int?)currentRectMarker.Tag;
							if (pIndex.HasValue)
							{
								if (pIndex < wpPolygon.Points.Count)
								{
									wpPolygon.Points[pIndex.Value] = pnew;
									lock (thisLock)
									{
										myMap.UpdatePolygonLocalPosition(wpPolygon);
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						log.Error(ex);
					}

					// update rect and marker pos.
					if (currentMarker.IsVisible)
					{
						currentMarker.Position = pnew;
					}
					currentRectMarker.Position = pnew;

					if (currentRectMarker.InnerMarker != null)
					{
						currentRectMarker.InnerMarker.Position = pnew;
					}
				}
				/**else if (currentPOIMarker != null)
				{
					PointLatLng pnew = MainMap.FromLocalToLatLng(e.X, e.Y);

					CurrentPOIMarker.Position = pnew;
				}**/
				else if (currentGMapMarker != null)
				{
					PointLatLng pnew = myMap.FromLocalToLatLng(e.X, e.Y);

					currentGMapMarker.Position = pnew;
				}
				else if (ModifierKeys == Keys.Control)
				{
					// draw selection box
					double latdif = MouseDownStart.Lat - point.Lat;
					double lngdif = MouseDownStart.Lng - point.Lng;

					myMap.SelectedArea = new RectLatLng(Math.Max(MouseDownStart.Lat, point.Lat),
						Math.Min(MouseDownStart.Lng, point.Lng), Math.Abs(lngdif), Math.Abs(latdif));
				}
				else // left click pan
				{
					double latdif = MouseDownStart.Lat - point.Lat;
					double lngdif = MouseDownStart.Lng - point.Lng;

					try
					{
						lock (thisLock)
						{
							if (!isMouseClickOffMenu)
								myMap.Position = new PointLatLng(center.Position.Lat + latdif,
									center.Position.Lng + lngdif);
						}
					}
					catch (Exception ex)
					{
						log.Error(ex);
					}
				}
			}
			else if (e.Button == MouseButtons.None)
			{
				isMouseDown = false;
			}
		}

		// click on some marker
		private void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
		{
			int answer;
			try // when dragging item can sometimes be null
			{
				if (item.Tag == null)
				{
					// home.. etc
					return;
				}

				if (ModifierKeys == Keys.Control)
				{
					try
					{
						groupmarkeradd(item);

						log.Info("add marker to group");
					}
					catch (Exception ex)
					{
						log.Error(ex);
					}
				}
				if (int.TryParse(item.Tag.ToString(), out answer))
				{
					dgvWayPoints.CurrentCell = dgvWayPoints[0, answer - 1];
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
		}

		private void MainMap_OnMarkerEnter(GMapMarker item)
		{

			if (!isMouseDown)
			{
				if (item is GMapMarkerRect)
				{
					GMapMarkerRect rc = item as GMapMarkerRect;
					rc.Pen.Color = Color.Red;
					myMap.Invalidate(false);

					int answer;
					if (item.Tag != null && rc.InnerMarker != null &&
						int.TryParse(rc.InnerMarker.Tag.ToString(), out answer))
					{
						try
						{
							dgvWayPoints.CurrentCell = dgvWayPoints[0, answer - 1];
							item.ToolTipText = "Alt: " + dgvWayPoints[colAltitude.Index, answer - 1].Value;
							item.ToolTipMode = MarkerTooltipMode.OnMouseOver; 
						}
						catch (Exception ex)
						{
							log.Error(ex);
						}
					}

					currentRectMarker = rc;
				}
				if (item is GMapMarkerRallyPt)
				{
					currentRallyPt = item as GMapMarkerRallyPt;
				}
				/**if (item is GMapMarkerAirport)
				{
					// do nothing - readonly
					return;
				}
				if (item is GMapMarkerPOI)
				{
					CurrentPOIMarker = item as GMapMarkerPOI;
				}*/
				if (item is GMapMarkerWP)
				{
					currentGMapMarker = item;
				}
				if (item is GMapMarker)
				{
					currentGMapMarker = item;
				}
			}
		}

		private void MainMap_OnMarkerLeave(GMapMarker item)
		{
			if (!isMouseDown)
			{
				if (item is GMapMarkerRect)
				{
					currentRectMarker = null;
					GMapMarkerRect rc = item as GMapMarkerRect;
					rc.ResetColor();
					myMap.Invalidate(false);
				}
				if (item is GMapMarkerRallyPt)
				{
					currentRallyPt = null;
				}
				/**if (item is GMapMarkerPOI)
				{
					CurrentPOIMarker = null;
				}*/
				if (item is GMapMarker)
				{
					// when you click the context menu this triggers and causes problems
					currentGMapMarker = null;
				}
			}
		}

		private void But_ZoomIn_Click(object sender, EventArgs e)
		{
			if (myMap.Zoom > 0)
			{
				try
				{
					myMap.Zoom += 1;
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
				//textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
				center.Position = myMap.Position;
			}
		}

		private void But_ZoomOut_Click(object sender, EventArgs e)
		{
			if (myMap.Zoom > 0)
			{
				try
				{
					myMap.Zoom -= 1;
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
				//textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
				center.Position = myMap.Position;
			}
		}
		#endregion

		/// <summary>
		/// used to add a marker to the map display
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="lng"></param>
		/// <param name="lat"></param>
		/// <param name="alt"></param>
		private void addpolygonmarker(string tag, double lng, double lat, double alt, Color? color)
		{
			try
			{
				PointLatLng point = new PointLatLng(lat, lng);
				GMapMarkerWP m = new GMapMarkerWP(point, tag);

				m.ToolTipMode = MarkerTooltipMode.OnMouseOver;
				m.ToolTipText = colAltitude.HeaderText + ": " + alt.ToString("0");
				m.Tag = tag;

				int wpno = -1;
				if (int.TryParse(tag, out wpno))
				{
					// preselect groupmarker
					if (groupmarkers.Contains(wpno))
						m.selected = true;
				}

				//MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
				GMapMarkerRect mBorders = new GMapMarkerRect(point);
				{
					mBorders.InnerMarker = m;
					mBorders.Tag = tag;
					// mBorders.wprad = (int)(float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist);
					mBorders.wprad = (int)(30 / 1);
					if (color.HasValue)
					{
						mBorders.Color = color.Value;
					}
				}

				overlays.objects.Markers.Add(m);
				overlays.objects.Markers.Add(mBorders);
			}
			catch (Exception)
			{
			}
		}

		private void addpolygonmarker(string tag, double lng, double lat, int alt, Color? color, GMapOverlay overlay)
		{
			try
			{
				PointLatLng point = new PointLatLng(lat, lng);
				GMarkerGoogle m = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);
				m.ToolTipMode = MarkerTooltipMode.Always;
				m.ToolTipText = tag;
				m.Tag = tag;

				GMapMarkerRect mBorders = new GMapMarkerRect(point);
				{
					mBorders.InnerMarker = m;
					try
					{
						// mBorders.wprad = (int)(Settings.Instance.GetFloat("TXT_WPRad"));
						mBorders.wprad = 30;
					}
					catch
					{
					}
					if (color.HasValue)
					{
						mBorders.Color = color.Value;
					}
				}

				Invoke((MethodInvoker)delegate
				{
					overlay.Markers.Add(m);
					overlay.Markers.Add(mBorders);
				});
			}
			catch (Exception)
			{
			}
		}

		private void addpolygonmarkergrid(string tag, double lng, double lat, int alt)
		{
			try
			{
				PointLatLng point = new PointLatLng(lat, lng);
				GMarkerGoogle m = new GMarkerGoogle(point, GMarkerGoogleType.green);
				m.ToolTipMode = MarkerTooltipMode.Never;
				m.ToolTipText = "grid" + tag;
				m.Tag = "grid" + tag;

				//MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
				GMapMarkerRect mBorders = new GMapMarkerRect(point);
				{
					mBorders.InnerMarker = m;
				}

				overlays.drawnpolygons.Markers.Add(m);
				overlays.drawnpolygons.Markers.Add(mBorders);
			}
			catch (Exception ex)
			{
				log.Info(ex.ToString());
			}
		}

		private void Commands_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex < 0)
					return;
				if (e.ColumnIndex == colDelete.Index && (e.RowIndex + 0) < dgvWayPoints.RowCount) // delete
				{
					quickadd = true;
					// mono fix
					dgvWayPoints.CurrentCell = null;
					dgvWayPoints.Rows.RemoveAt(e.RowIndex);
					quickadd = false;
					writeKMLV2();
				}			
				// setgradanddistandaz();
			}
			catch (Exception)
			{
				MessageBox.Show(ResStrings.MsgRowError);
			}
		}

		public void callMeDrag(string pointno, double lat, double lng, int alt)
		{
			if (pointno == "")
			{
				return;
			}
			
			// dragging a WP
			if (pointno == "H")
			{
				// auto update home alt
				TxtHomeAltitude.Text = "0";
				TxtHomeLatitude.Text = lat.ToString();
				TxtHomeLongitude.Text = lng.ToString();
				return;
			}


			if (pointno == "Tracker Home")
			{
				ActiveDrone.Status.TrackerLocation = new PointLatLngAlt(lat, lng, alt, "");
				return;
			}

			try
			{
				selectedrow = int.Parse(pointno) - 1;
				dgvWayPoints.CurrentCell = dgvWayPoints[1, selectedrow];
				// depending on the dragged item, selectedrow can be reset 
				selectedrow = int.Parse(pointno) - 1;
			}
			catch
			{
				return;
			}

			setfromMap(lat, lng, alt);
		}

		public void setfromMap(double lat, double lng, int alt, double p1 = 0)
		{
			if (selectedrow > dgvWayPoints.RowCount)
			{
				MessageBox.Show(ResStrings.MsgInvalidCoordinate);
				return;
			}

			try
			{
				// get current command list
				var currentlist = GetCommandList();
				// add history
				history.Add(currentlist);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ResStrings.MsgInvalidEntry.FormatWith(ex.Message));
			}

			// remove more than 20 revisions
			if (history.Count > 20)
			{
				history.RemoveRange(0, history.Count - 20);
			}

			DataGridViewTextBoxCell cell;
			if (dgvWayPoints.Columns[colLatitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][4] /*"Lat"*/))
			{
				cell = dgvWayPoints.Rows[selectedrow].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				cell.Value = lat.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (dgvWayPoints.Columns[colLongitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][5] /*"Long"*/))
			{
				cell = dgvWayPoints.Rows[selectedrow].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
				cell.Value = lng.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (alt != -1 && alt != -2 &&
				dgvWayPoints.Columns[colAltitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6] /*"Alt"*/))
			{
				cell = dgvWayPoints.Rows[selectedrow].Cells[colAltitude.Index] as DataGridViewTextBoxCell;

				{
					double result;
					bool pass = double.TryParse(TxtHomeAltitude.Text, out result);

					if (pass == false)
					{
						//MessageBox.Show("You must have a home altitude");

						string homealt = "10";
						//if (DialogResult.Cancel == InputBox.Show("Home Alt", "Home Altitude", ref homealt))
						if (DialogResult.Cancel == InputBox.Show(ResStrings.MsgHomeAltitudeRequired, homealt, ref homealt))
							return;
						TxtHomeAltitude.Text = homealt;
					}
					int results1;
					if (!int.TryParse(TxtAltitudeValue.Text, out results1))
					{
						MessageBox.Show(ResStrings.MsgDefaultAltitudeInvalid);
						return;
					}

					if (results1 == 0)
					{
						string defalt = "10";
						if (DialogResult.Cancel == InputBox.Show(ResStrings.MsgDefaultAltitudeRequired, defalt, ref defalt))
							return;
						TxtAltitudeValue.Text = defalt;
					}
				}

				cell.Value = TxtAltitudeValue.Text;

				float ans;
				if (float.TryParse(cell.Value.ToString(), out ans))
				{
					ans = (int)ans;
					if (alt != 0) // use passed in value;
						cell.Value = alt.ToString();
					if (ans == 0) // default
						cell.Value = 50;


					cell.DataGridView.EndEdit();
				}
				else
				{
					MessageBox.Show(ResStrings.MsgInvalidHomeOrWPAltitide);
					cell.Style.BackColor = Color.Red;
				}
			}



			// convert to utm
			// convertFromGeographic(lat, lng);

			// Add more for other params
			if (dgvWayPoints.Columns[colParam1.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][1] /*"Delay"*/))
			{
				cell = dgvWayPoints.Rows[selectedrow].Cells[colParam1.Index] as DataGridViewTextBoxCell;
				cell.Value = p1;
				cell.DataGridView.EndEdit();
			}

            writeKMLV2();
            // writeKML();


            dgvWayPoints.EndEdit();
		}

		public void AddWPToMap(double lat, double lng, int alt)
		{
			// check home point setup.
			if (IsHomeEmpty())
			{
				MessageBox.Show(ResStrings.MsgSetHomeFirst);
				return;
			}

			// creating a WP
			selectedrow = dgvWayPoints.Rows.Add();

			
			dgvWayPoints.Rows[selectedrow].Cells[colCommand.Index].Value = MAVLink.MAV_CMD.WAYPOINT.ToString();
			ChangeColumnHeader(MAVLink.MAV_CMD.WAYPOINT.ToString());
			

			setfromMap(lat, lng, alt);
		}

		private bool IsHomeEmpty()
		{
			/*if (TxtHomeAltitude.Text != "" && TxtHomeLatitude.Text != "" && TxtHomeLongitude.Text != "")
				return false;
			else
				return true;*/
			double holder;
			return !double.TryParse(TxtHomeAltitude.Text, out holder) ||
					!double.TryParse(TxtHomeLatitude.Text, out holder) ||
					!double.TryParse(TxtHomeLongitude.Text, out holder);
		}

		private void ChangeColumnHeader(string command)
		{
			try
			{
				if (cmdParamNames.ContainsKey(command))
					for (int i = 1; i <= 7; i++)
						dgvWayPoints.Columns[i].HeaderText = cmdParamNames[command][i - 1];
				else
					for (int i = 1; i <= 7; i++)
						dgvWayPoints.Columns[i].HeaderText = "setme";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private List<Locationwp> GetCommandList()
		{
			List<Locationwp> commands = new List<Locationwp>();

			for (int a = 0; a < dgvWayPoints.Rows.Count - 0; a++)
			{
				var temp = DataViewtoLocationwp(a);

				commands.Add(temp);
			}

			return commands;
		}

		private Locationwp DataViewtoLocationwp(int a)
		{
			try
			{
				Locationwp temp = new Locationwp();
				if (dgvWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString().Contains("UNKNOWN"))
				{
					temp.id = (ushort)dgvWayPoints.Rows[a].Cells[colCommand.Index].Tag;
				}
				else
				{
					temp.id =
						(ushort)
								Enum.Parse(typeof(MAVLink.MAV_CMD),
									dgvWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString(),
									false);
				}

			// TODO: I don't know where currentstate come from..
	
				temp.alt =
					(float)
						(double.Parse(dgvWayPoints.Rows[a].Cells[colAltitude.Index].Value.ToString()));
			
				temp.lat = (double.Parse(dgvWayPoints.Rows[a].Cells[colLatitude.Index].Value.ToString()));
				temp.lng = (double.Parse(dgvWayPoints.Rows[a].Cells[colLongitude.Index].Value.ToString()));
				temp.p1 = float.Parse(dgvWayPoints.Rows[a].Cells[colParam1.Index].Value.ToString());
				temp.p2 = (float)(double.Parse(dgvWayPoints.Rows[a].Cells[colParam2.Index].Value.ToString()));
				temp.p3 = (float)(double.Parse(dgvWayPoints.Rows[a].Cells[colParam3.Index].Value.ToString()));
				temp.p4 = (float)(double.Parse(dgvWayPoints.Rows[a].Cells[colParam4.Index].Value.ToString()));

				temp.Tag = dgvWayPoints.Rows[a].Cells[colTagData.Index].Value;

				return temp;
			}
			catch (Exception ex)
			{
				throw new FormatException("Invalid number on row " + (a + 1).ToString(), ex);
			}
		}
                
        public void writeKMLV2()
        {


            // quickadd is for when loading wps from eeprom or file, to prevent slow, loading times
            if (quickadd)
                return;

            updateRowNumbers();

            var home = new PointLatLngAlt(
                    double.Parse(TxtHomeLatitude.Text), double.Parse(TxtHomeLongitude.Text),
                    double.Parse(TxtHomeAltitude.Text), "H");

            // var overlay = new WPOverlay(overlays.objects);
            var overlay = new OverlayUtility.WPOverlay(overlays.objects);

			overlay.RaiseFullPointsEvent += (s ,e) => {

				if (e.FullPoints.Count > 0)
				{
					double dist = 0.0d;

					for (int a = 1; a < e.FullPoints.Count; a++)
					{
						if (e.FullPoints[a - 1] == null)
							continue;

						if (e.FullPoints[a] == null)
							continue;

						dist += myMap.MapProvider.Projection.GetDistance(e.FullPoints[a - 1], e.FullPoints[a]);
						DroneInfoPanel.UpdateAssumeTime(dist);
						DatabaseManager.UpdateTotalDistance(recorder_id, dist);
					}
				}
			};

			overlay.CreateOverlay(home, GetCommandList(), 30, 30);

            myMap.HoldInvalidation = true;

            /*
            var existing = myMap.Overlays.Where(a => a.Id == overlay.overlay.Id).ToList();
            foreach (var b in existing)
            {
                myMap.Overlays.Remove(b);
            }*/

            // myMap.Overlays.Insert(1, overlay.overlay);
                       
            overlay.overlay.ForceUpdate();




			// setgradanddistandaz(overlay.pointlist, home);

			if (overlay.pointlist.Count <= 1)
            {
                RectLatLng? rect = myMap.GetRectOfAllMarkers(overlay.overlay.Id);
                if (rect.HasValue)
                {
                    myMap.Position = rect.Value.LocationMiddle;
                }

				DroneInfoPanel.ResetAssumeTime();

				// myMap_OnMapZoomChanged();
			}

            pointlist = overlay.pointlist;

			myMap.Refresh();
        }


		/// <summary>
		/// Format distance according to prefer distance unit
		/// </summary>
		/// <param name="distInKM">distance in kilometers</param>
		/// <param name="toMeterOrFeet">convert distance to meter or feet if true, covert to km or miles if false</param>
		/// <returns>formatted distance with unit</returns>
		private string FormatDistance(double distInKM, bool toMeterOrFeet)
		{
			string sunits = Utilities.Settings.Instance["distunits"];
			Utility.distances units = Utility.distances.Meters;

			if (sunits != null)
				try
				{
					units = (Utility.distances)Enum.Parse(typeof(Utility.distances), sunits);
				}
				catch (Exception)
				{
				}

			switch (units)
			{
				case Utility.distances.Feet:
					return toMeterOrFeet
						? string.Format((distInKM * 3280.8399).ToString("0.00 ft"))
						: string.Format((distInKM * 0.621371).ToString("0.0000 miles"));
				case Utility.distances.Meters:
				default:
					return toMeterOrFeet
						? string.Format((distInKM * 1000).ToString("0.00 m"))
						: string.Format(distInKM.ToString("0.0000 km"));
			}
		}

		private double find_angle(List<PointLatLngAlt> points)
		{
			log.Info("find_angle");

			for (int i = 1; i < points.Count-1; i++)
			{
				PointLatLng p1 = new PointLatLng(points[i-1].Lat, points[i-1].Lng);
				PointLatLng p2 = new PointLatLng(points[i].Lat, points[i].Lng);
				PointLatLng p3 = new PointLatLng(points[i+1].Lat, points[i+1].Lng);

				double p12 = Math.Sqrt(Math.Pow(p1.Lat - p2.Lat, 2) + Math.Pow(p1.Lng - p2.Lng, 2));
				double p23 = Math.Sqrt(Math.Pow(p2.Lat - p3.Lat, 2) + Math.Pow(p2.Lng - p3.Lng, 2));
				double p13 = Math.Sqrt(Math.Pow(p1.Lat - p3.Lat, 2) + Math.Pow(p1.Lng - p3.Lng, 2));

				double angle = Math.Acos((Math.Pow(p12,2) + Math.Pow(p13,2) - Math.Pow(p23,2)) / (2*p12*p13));
				
			}

			return 0;
		}

		private void RegenerateWPRoute(List<PointLatLngAlt> fullpointlist)
		{
			route.Clear();
			homeroute.Clear();

			overlays.polygons.Routes.Clear();

			PointLatLngAlt lastpnt = fullpointlist[0];
			PointLatLngAlt lastpnt2 = fullpointlist[0];
			PointLatLngAlt lastnonspline = fullpointlist[0];
			List<PointLatLngAlt> splinepnts = new List<PointLatLngAlt>();
			List<PointLatLngAlt> wproute = new List<PointLatLngAlt>();


			// add home - this causeszx the spline to always have a straight finish
			fullpointlist.Add(fullpointlist[0]);

			for (int a = 0; a < fullpointlist.Count; a++)
			{
				if (fullpointlist[a] == null)
					continue;

				if (fullpointlist[a].Tag2 == "spline")
				{
					if (splinepnts.Count == 0)
						splinepnts.Add(lastpnt);

					splinepnts.Add(fullpointlist[a]);
				}
				else
				{
					wproute.Add(fullpointlist[a]);

					lastpnt2 = lastpnt;
					lastpnt = fullpointlist[a];
				}
			}

			/**
			List<PointLatLng> list = new List<PointLatLng>();
			fullpointlist.ForEach(x => { list.Add(x); });
			route.Points.AddRange(list); */

			// route is full need to get 1, 2 and last point as "HOME" route

			int count = wproute.Count;
			int counter = 0;
			PointLatLngAlt homepoint = new PointLatLngAlt();
			PointLatLngAlt firstpoint = new PointLatLngAlt();
			PointLatLngAlt lastpoint = new PointLatLngAlt();

			if (count > 2)
			{
				// homeroute = last, home, first
				wproute.ForEach(x =>
				{
					counter++;
					if (counter == 1)
					{
						homepoint = x;
						return;
					}
					if (counter == 2)
					{
						firstpoint = x;
					}
					if (counter == count - 1)
					{
						lastpoint = x;
					}
					if (counter == count)
					{
						homeroute.Points.Add(lastpoint);
						homeroute.Points.Add(homepoint);
						homeroute.Points.Add(firstpoint);
						return;
					}
					route.Points.Add(x);
				});

				homeroute.Stroke = new Pen(Color.Yellow, 2);
				// if we have a large distance between home and the first/last point, it hangs on the draw of a the dashed line.
				if (homepoint.GetDistance(lastpoint) < 5000 && homepoint.GetDistance(firstpoint) < 5000)
					homeroute.Stroke.DashStyle = DashStyle.Dash;

				overlays.polygons.Routes.Add(homeroute);

				route.Stroke = new Pen(Color.Yellow, 4);
				route.Stroke.DashStyle = DashStyle.Custom;
				overlays.polygons.Routes.Add(route);
			}
		}

		private void Commands_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (quickadd)
				return;
			try
			{
				selectedrow = e.RowIndex;
				string option = dgvWayPoints[colCommand.Index, selectedrow].EditedFormattedValue.ToString();
				string cmd;
				try
				{
					if (dgvWayPoints[colCommand.Index, selectedrow].Value != null)
						cmd = dgvWayPoints[colCommand.Index, selectedrow].Value.ToString();
					else
						cmd = option;
				}
				catch
				{
					cmd = option;
				}
				//Console.WriteLine("editformat " + option + " value " + cmd);
				ChangeColumnHeader(cmd);

				if (cmd == "WAYPOINT")
				{
				}

				//  writeKML();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void Commands_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			for (int i = 0; i < dgvWayPoints.ColumnCount; i++)
			{
				DataGridViewCell tcell = dgvWayPoints.Rows[e.RowIndex].Cells[i];
				if (tcell.GetType() == typeof(DataGridViewTextBoxCell))
				{
					if (tcell.Value == null)
						tcell.Value = "0";
				}
			}

			DataGridViewComboBoxCell cell = dgvWayPoints.Rows[e.RowIndex].Cells[colCommand.Index] as DataGridViewComboBoxCell;
			if (cell.Value == null)
			{
				cell.Value = "WAYPOINT";
				cell.DropDownWidth = 200;
				dgvWayPoints.Rows[e.RowIndex].Cells[colDelete.Index].Value = "X";
				if (!quickadd)
				{
					Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex - 0)); // do header labels
					Commands_RowValidating(sender, new DataGridViewCellCancelEventArgs(0, e.RowIndex));
					// do default values
				}
			}

			if (quickadd)
				return;

			try
			{
				dgvWayPoints.CurrentCell = dgvWayPoints.Rows[e.RowIndex].Cells[0];

				if (dgvWayPoints.Rows.Count > 1)
				{
					if (dgvWayPoints.Rows[e.RowIndex - 1].Cells[colCommand.Index].Value.ToString() == "WAYPOINT")
					{
						dgvWayPoints.Rows[e.RowIndex].Selected = true; // highlight row
					}
					else
					{
						dgvWayPoints.CurrentCell = dgvWayPoints[1, e.RowIndex - 1];
						//Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex-1));
					}
				}
			}
			catch (Exception)
			{
			}
			// Commands.EndEdit();
		}

		private void Commands_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
		{
			selectedrow = e.RowIndex;
			Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex - 0));
			// do header labels - encure we dont 0 out valid colums
			int cols = dgvWayPoints.Columns.Count;
			for (int a = 1; a < cols; a++)
			{
				DataGridViewTextBoxCell cell;
				cell = dgvWayPoints.Rows[selectedrow].Cells[a] as DataGridViewTextBoxCell;

				if (dgvWayPoints.Columns[a].HeaderText.Equals("") && cell != null && cell.Value == null)
				{
					cell.Value = "0";
				}
				else
				{
					if (cell != null && (cell.Value == null || cell.Value.ToString() == ""))
					{
						cell.Value = "?";
					}
				}
			}
		}

		#region save waypoints
		void saveWPsFast(object sender, ProgressWorkerEventArgs e, object passdata)
		{
			var totalwpcountforupload = (ushort)(dgvWayPoints.RowCount + 1);
			var reqno = 0;
			MAVLink.MAV_MISSION_RESULT result = MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED;

			var sub1 = ActiveDrone.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.MISSION_ACK,
				message =>
				{
					var data = ((MAVLink.mavlink_mission_ack_t)message.data);
					var ans = (MAVLink.MAV_MISSION_RESULT)data.type;
					if (ActiveDrone.Status.sysid != message.sysid &&
						ActiveDrone.Status.compid != message.compid)
						return true;
					result = ans;
					log.Info("MISSION_ACK " + ans);
					return true;
				});

			var sub2 = ActiveDrone.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.MISSION_REQUEST,
				message =>
				{
					var data = ((MAVLink.mavlink_mission_request_t)message.data);
					if (ActiveDrone.Status.sysid != message.sysid &&
						ActiveDrone.Status.compid != message.compid)
						return true;
					reqno = data.seq;
					log.Info("MISSION_REQUEST " + reqno);
					return true;
				});

			((ProgressDialogV2)sender).UpdateProgressAndStatus(-1, "Set total wps ");
			ActiveDrone.setWPTotal(totalwpcountforupload);

			// define the home point
			Locationwp home = new Locationwp();
			try
			{
				home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
				home.lat = (double.Parse(TxtHomeLatitude.Text));
				home.lng = (double.Parse(TxtHomeLongitude.Text));
				home.alt = (float.Parse(TxtHomeAltitude.Text)); // use saved home
			}
			catch
			{
				ActiveDrone.UnSubscribeToPacketType(sub1);
				ActiveDrone.UnSubscribeToPacketType(sub2);
				throw new Exception("Your home location is invalid");
			}

			// define the default frame.
			MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

			// get the command list from the datagrid
			var commandlist = GetCommandList();

			commandlist.Insert(0, home);

			// process commandlist to the mav
			for (var a = 0; a < commandlist.Count; a++)
			{
				if (a % 10 == 0 && a != 0)
				{
					var start = DateTime.Now;
					while (true)
					{
						if (((ProgressDialogV2)sender).doWorkArgs.CancelRequested)
						{
							ActiveDrone.setWPTotal(0);
							ActiveDrone.UnSubscribeToPacketType(sub1);
							ActiveDrone.UnSubscribeToPacketType(sub2);
							return;
						}

						if (reqno == a)
						{
							// all received
							break;
						}

						if (start.AddSeconds(1.1) < DateTime.Now)
						{
							// do next 10 starting at reqno
							a = reqno;
							break;
						}

						if (result == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
							Thread.Sleep(500);

						if (result == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
						{
							// resend for partial upload
							ActiveDrone.setWPPartialUpdate((ushort)(reqno), totalwpcountforupload);
							a = reqno;
							break;
						}

						if (result == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE)
						{
							log.Error("Upload failed, please reduce the number of wp's");
							ActiveDrone.UnSubscribeToPacketType(sub1);
							ActiveDrone.UnSubscribeToPacketType(sub2);
							return;
						}
						if (result == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
						{
							log.Error(
								"Upload failed, mission was rejected byt the Mav,\n item had a bad option wp# " + a + " " +
								result);
							ActiveDrone.UnSubscribeToPacketType(sub1);
							ActiveDrone.UnSubscribeToPacketType(sub2);
							return;
						}
						if (result != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
						{
							log.Error("Upload wps failed " + reqno +
											 " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), result.ToString()));
							ActiveDrone.UnSubscribeToPacketType(sub1);
							ActiveDrone.UnSubscribeToPacketType(sub2);
							return;
						}

						System.Threading.Thread.Sleep(10);
					}
				}

				var loc = commandlist[a];

				// make sure we are using the correct frame for these commands
				if (loc.id < (ushort)MAVLink.MAV_CMD.LAST || loc.id == (ushort)MAVLink.MAV_CMD.DO_SET_HOME)
				{
					var mode = AltitudeMode.Relative;

					if (mode == AltitudeMode.Terrain)
					{
						frame = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT;
					}
					else if (mode == AltitudeMode.Absolute)
					{
						frame = MAVLink.MAV_FRAME.GLOBAL;
					}
					else
					{
						frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
					}
				}

				MAVLink.mavlink_mission_item_int_t req = new MAVLink.mavlink_mission_item_int_t();

				req.target_system = ActiveDrone.Status.sysid;
				req.target_component = ActiveDrone.Status.compid;

				req.command = loc.id;

				req.current = 0;
				req.autocontinue = 1;

				req.frame = (byte)frame;
				if (loc.id == (ushort)MAVLink.MAV_CMD.DO_DIGICAM_CONTROL || loc.id == (ushort)MAVLink.MAV_CMD.DO_DIGICAM_CONFIGURE)
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

				req.seq = (ushort)a;

				((ProgressDialogV2)sender).UpdateProgressAndStatus(-1,	"Setting WP " + a);
				log.Info("WP no " + a);

				ActiveDrone.sendPacket(req, ActiveDrone.Status.sysid, ActiveDrone.Status.compid);
			}

			ActiveDrone.UnSubscribeToPacketType(sub1);
			ActiveDrone.UnSubscribeToPacketType(sub2);

			ActiveDrone.setWPACK();
		}

		private void saveWPs(object sender, ProgressWorkerEventArgs e, object passdata)
        {
            try
            {
                
                if (!ActiveDrone.BaseStream.IsOpen)
                {
                    throw new Exception("Please connect first!");
                    // MessageBox.Show(ResStrings.MsgConnectFirst);
                 
                }

				ActiveDrone.giveComport = true;
				int a = 0;

				// define the home point
				Locationwp home = new Locationwp();
				try
				{
					home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
					home.lat = (double.Parse(TxtHomeLatitude.Text));
					home.lng = (double.Parse(TxtHomeLongitude.Text));
					home.alt = (float.Parse(TxtHomeAltitude.Text)); // use saved home
				}
				catch
				{
					throw new Exception("Your home location is invalid");
				}

				// log
				log.Info("wps values " + ActiveDrone.Status.wps.Values.Count);
				log.Info("cmd rows " + (dgvWayPoints.Rows.Count + 1)); // + home

				// check for changes / future mod to send just changed wp's
				if (ActiveDrone.Status.wps.Values.Count == (dgvWayPoints.Rows.Count + 1))
				{
					Hashtable wpstoupload = new Hashtable();

					a = -1;
					foreach (var item in ActiveDrone.Status.wps.Values)
					{
						// skip home
						if (a == -1)
						{
							a++;
							continue;
						}

						MAVLink.mavlink_mission_item_t temp = DataViewtoLocationwp(a);

						if (temp.command == item.command &&
							temp.x == item.x &&
							temp.y == item.y &&
							temp.z == item.z &&
							temp.param1 == item.param1 &&
							temp.param2 == item.param2 &&
							temp.param3 == item.param3 &&
							temp.param4 == item.param4
							)
						{
							log.Info("wp match " + (a + 1));
						}
						else
						{
							log.Info("wp no match" + (a + 1));
							wpstoupload[a] = "";
						}

						a++;
					}
				}

				uint capabilities = (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_FLOAT;
				bool use_int = (capabilities & (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;

				// set wp total
				((ProgressDialogV2)sender).UpdateProgressAndStatus(-1, "Set total wps ");

				ushort totalwpcountforupload = (ushort)(dgvWayPoints.Rows.Count + 1);

				if (ActiveDrone.Status.apname == MAVLink.MAV_AUTOPILOT.PX4)
				{
					totalwpcountforupload--;
				}

				try
				{
					ActiveDrone.setWPTotal(totalwpcountforupload);
				}
				catch (TimeoutException)
				{
					MessageBox.Show(ResStrings.MsgSaveWPTimeout);
				}
				 // + home

				// set home location - overwritten/ignored depending on firmware.
				// ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set home");

				// upload from wp0
				a = 0;

				if (ActiveDrone.Status.apname != MAVLink.MAV_AUTOPILOT.PX4)
				{
					try
					{
						var homeans = ActiveDrone.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
						if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
						{
							if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
							{
								MessageBox.Show(ResStrings.MsgSaveWPRejected.FormatWith(1));
								return;
							}
						}
						a++;
					}
					catch (TimeoutException)
					{
						use_int = false;
						// added here to prevent timeout errors
						ActiveDrone.setWPTotal(totalwpcountforupload);
						var homeans = ActiveDrone.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
						if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
						{
							if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
							{
								MessageBox.Show(ResStrings.MsgSaveWPRejected.FormatWith(2));
								return;
							}
						}
						a++;
					}
				}
				else
				{
					use_int = false;
				}

				// define the default frame.
				MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

				// get the command list from the datagrid
				var commandlist = GetCommandList();

				// process commandlist to the mav
				for (a = 1; a <= commandlist.Count; a++)
				{
					var temp = commandlist[a - 1];

					((ProgressDialogV2)sender).UpdateProgressAndStatus(-1, "Setting WP " + a);

					// make sure we are using the correct frame for these commands
					if (temp.id < (ushort)MAVLink.MAV_CMD.LAST || temp.id == (ushort)MAVLink.MAV_CMD.DO_SET_HOME)
					{
						var mode = AltitudeMode.Relative;

						if (mode == AltitudeMode.Terrain)
						{
							frame = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT;
						}
						else if (mode == AltitudeMode.Absolute)
						{
							frame = MAVLink.MAV_FRAME.GLOBAL;
						}
						else
						{
							frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
						}
					}

					// handle current wp upload number
					int uploadwpno = a;
					if (ActiveDrone.Status.apname == MAVLink.MAV_AUTOPILOT.PX4)
						uploadwpno--;

					// try send the wp
					MAVLink.MAV_MISSION_RESULT ans = ActiveDrone.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);

					// we timed out while uploading wps/ command wasnt replaced/ command wasnt added
					if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
					{
						// resend for partial upload
						ActiveDrone.setWPPartialUpdate((ushort)(uploadwpno), totalwpcountforupload);
						// reupload this point.
						ans = ActiveDrone.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);
					}

					if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE)
					{
						MessageBox.Show(ResStrings.MsgMissionRejectedTooManyWaypoints);
						log.Error("Upload failed, please reduce the number of wp's");
						return;
					}
					if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
					{

						MessageBox.Show(ResStrings.MsgMissionRejectedBadWP.FormatWith(a, ans));
						log.Error("Upload failed, mission was rejected byt the Mav,\n " +
							"item had a bad option wp# " + a + " " +
							ans);
						return;
					}
					if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
					{
						// invalid sequence can only occur if we failed to see a response from the apm when we sent the request.
						// or there is io lag and we send 2 mission_items and get 2 responces, one valid, one a ack of the second send

						// the ans is received via mission_ack, so we dont know for certain what our current request is for. as we may have lost the mission_request

						// get requested wp no - 1;
						a = ActiveDrone.getRequestedWPNo() - 1;

						continue;
					}
					if (ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
					{

						MessageBox.Show(ResStrings.MsgMissionRejectedGeneral.FormatWith(
							Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()),
							Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString())));
						log.Error("Upload wps failed " + Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()) +
										 " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString()));
						return;
					}
				}

				ActiveDrone.setWPACK();
				
				((ProgressDialogV2)sender).UpdateProgressAndStatus(-1, "Setting params");

				// m
				ActiveDrone.setParam("WP_RADIUS", float.Parse("30") / 1);

				// cm's
				ActiveDrone.setParam("WPNAV_RADIUS", float.Parse("30") / 1 * 100.0);

				// Remind the user after uploading the mission into firmware.
				MessageBox.Show(ResStrings.MsgMissionAcceptWP.FormatWith(a));
				
				try
				{
					ActiveDrone.setParam(new[] { "LOITER_RAD", "WP_LOITER_RAD" },
						float.Parse("45"));
				}
				catch
				{
				}

				((ProgressDialogV2)sender).UpdateProgressAndStatus(-1, "Done.");
			}
			catch (Exception ex)
			{
				log.Error(ex);
				ActiveDrone.giveComport = false;
				throw;
			}

			ActiveDrone.giveComport = false;
		}
		#endregion

		void getWPs(object sender, ProgressWorkerEventArgs e, object passdata = null)
		{
			List<Locationwp> cmds = new List<Locationwp>();

			try
			{

                if (!ActiveDrone.BaseStream.IsOpen)
                {
                    // prevent application termination
                    //throw new Exception(Diva.Properties.Strings.MsgConnectFirst);
                    MessageBox.Show(ResStrings.MsgConnectFirst);
                    return;
                }

				ActiveDrone.giveComport = true;

				// param = port.MAV.param;

				log.Info("Getting Home");

				((ProgressDialogV2)sender).UpdateProgressAndStatus(-1, "Getting WP count");

				log.Info("Getting WP #");

				int cmdcount = ActiveDrone.getWPCount();

				for (ushort a = 0; a < cmdcount; a++)
				{

					if (((ProgressDialogV2)sender).doWorkArgs.CancelRequested)
					{
						((ProgressDialogV2)sender).doWorkArgs.CancelAcknowledged = true;
						throw new Exception("Cancel Requested");
					}

					log.Info("Getting WP" + a);
					cmds.Add(ActiveDrone.getWP(a));
				}

				ActiveDrone.setWPACK();

				((ProgressDialogV2)sender).UpdateProgressAndStatus(-1, "Done");

				log.Info("Done");
			}
			catch (Exception ex)
			{
				throw;
			}

			WPtoScreen(cmds);
		}

		public void WPtoScreen(List<Locationwp> cmds, bool withrally = true)
		{
			try
			{
				Invoke((MethodInvoker)delegate
				{
					try
					{
						Console.WriteLine("Process " + cmds.Count);
						processToScreen(cmds);
					}
					catch (Exception exx)
					{
						Console.WriteLine(exx.ToString());
					}

					
					try
					{
						if (withrally && ActiveDrone.Status.param.ContainsKey("RALLY_TOTAL") &&
							int.Parse(ActiveDrone.Status.param["RALLY_TOTAL"].ToString()) >= 1)
						{
							Console.WriteLine("get rally points");
							getRallyPoints();
						}
							
					}
					catch
					{
					}

					ActiveDrone.giveComport = false;

					// BUT_ReadWPs.Enabled = true;

					writeKMLV2();
				});
			}
			catch (Exception exx)
			{
				log.Info(exx.ToString());
			}
		}

		/// <summary>
		/// Processes a loaded EEPROM to the map and datagrid
		/// </summary>
		void processToScreen(List<Locationwp> cmds, bool append = false)
		{
			quickadd = true;


			// mono fix
			dgvWayPoints.CurrentCell = null;

			while (dgvWayPoints.Rows.Count > 0 && !append)
				dgvWayPoints.Rows.Clear();

			if (cmds.Count == 0)
			{
				quickadd = false;
				return;
			}

			dgvWayPoints.SuspendLayout();
			dgvWayPoints.Enabled = false;

			int i = dgvWayPoints.Rows.Count - 1;
			foreach (Locationwp temp in cmds)
			{
				i++;
				//Console.WriteLine("FP processToScreen " + i);
				if (temp.id == 0 && i != 0) // 0 and not home
					break;
				if (temp.id == 255 && i != 0) // bad record - never loaded any WP's - but have started the board up.
					break;
				if (i == 0 && append) // we dont want to add home again.
					continue;
				if (i + 1 >= dgvWayPoints.Rows.Count)
				{
					selectedrow = dgvWayPoints.Rows.Add();
				}
				//if (i == 0 && temp.alt == 0) // skip 0 home
				//  continue;
				DataGridViewTextBoxCell cell;
				DataGridViewComboBoxCell cellcmd;
				cellcmd = dgvWayPoints.Rows[i].Cells[colCommand.Index] as DataGridViewComboBoxCell;
				cellcmd.Value = "UNKNOWN";
				cellcmd.Tag = temp.id;

				foreach (object value in Enum.GetValues(typeof(MAVLink.MAV_CMD)))
				{
					
					if ((ushort)value == temp.id)
					{
						cellcmd.Value = value.ToString();
						break;
					}
				}

				cell = dgvWayPoints.Rows[i].Cells[colAltitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.alt;
				cell = dgvWayPoints.Rows[i].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.lat;
				cell = dgvWayPoints.Rows[i].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.lng;

				cell = dgvWayPoints.Rows[i].Cells[colParam1.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.p1;
				cell = dgvWayPoints.Rows[i].Cells[colParam2.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.p2;
				cell = dgvWayPoints.Rows[i].Cells[colParam3.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.p3;
				cell = dgvWayPoints.Rows[i].Cells[colParam4.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.p4;

				// convert to utm
				// convertFromGeographic(temp.lat, temp.lng);
			}

			dgvWayPoints.Enabled = true;
			dgvWayPoints.ResumeLayout();

			// We don't have parameter panel.
			// setWPParams();

			try
			{
				DataGridViewTextBoxCell cellhome;
				cellhome = dgvWayPoints.Rows[0].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				if (cellhome.Value != null)
				{
					if (cellhome.Value.ToString() != TxtHomeLatitude.Text && cellhome.Value.ToString() != "0")
					{
						DialogResult dr = MessageBox.Show(ResStrings.MsgResetHomeCoordinate,
							ResStrings.MsgResetHomeCoordinateTitle, MessageBoxButtons.YesNo);

						if (dr == DialogResult.Yes)
						{
							TxtHomeLatitude.Text = (double.Parse(cellhome.Value.ToString())).ToString();
							cellhome = dgvWayPoints.Rows[0].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
							TxtHomeLongitude.Text = (double.Parse(cellhome.Value.ToString())).ToString();
							cellhome = dgvWayPoints.Rows[0].Cells[colAltitude.Index] as DataGridViewTextBoxCell;
							TxtHomeAltitude.Text =
								(double.Parse(cellhome.Value.ToString())).ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());

			} // if there is no valid home

			if (dgvWayPoints.RowCount > 0)
			{
				log.Info("remove home from list");
				dgvWayPoints.Rows.Remove(dgvWayPoints.Rows[0]); // remove home row
			}

			quickadd = false;

			writeKMLV2();

			myMap.ZoomAndCenterMarkers("objects");

			// MainMap_OnMapZoomChanged();
		}

		public void getRallyPoints()
		{
			if (ActiveDrone.Status.param["RALLY_TOTAL"] == null)
			{
				MessageBox.Show(ResStrings.MsgUnsupported);
				return;
			}

			if (int.Parse(ActiveDrone.Status.param["RALLY_TOTAL"].ToString()) < 1)
			{
				MessageBox.Show(ResStrings.MsgNoRallyPoint);
				return;
			}

			overlays.rallypoints.Markers.Clear();

			int count = int.Parse(ActiveDrone.Status.param["RALLY_TOTAL"].ToString());

			for (int a = 0; a < (count); a++)
			{
				try
				{
					PointLatLngAlt plla = ActiveDrone.getRallyPoint(a, ref count);
					overlays.rallypoints.Markers.Add(new GMapMarkerRallyPt(new PointLatLng(plla.Lat, plla.Lng))
					{
						Alt = (int)plla.Alt,
						ToolTipMode = MarkerTooltipMode.OnMouseOver,
						ToolTipText = ResStrings.StrRallyPointToolTipText.FormatWith(plla.Alt * 1)
					});
				}
				catch
				{
					MessageBox.Show("Failed to get rally point");
					return;
				}
			}

			myMap.UpdateMarkerLocalPosition(overlays.rallypoints.Markers[0]);

			myMap.Invalidate();
		}


		private void clearMissionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			quickadd = true;

			// mono fix
			dgvWayPoints.CurrentCell = null;

			dgvWayPoints.Rows.Clear();

			selectedrow = 0;
			quickadd = false;
			writeKMLV2();
		}

		private void goHereToolStripMenuItem_Click(object sender, EventArgs e)
		{

			if (!ActiveDrone.BaseStream.IsOpen)
			{
				// CustomMessageBox.Show(Strings.PleaseConnect, Strings.ERROR);
				MessageBox.Show(ResStrings.MsgNoConnection);
				return;
			}

			if (ActiveDrone.Status.GuidedMode.z == 0)
			{
				// flyToHereAltToolStripMenuItem_Click(null, null);

				if (ActiveDrone.Status.GuidedMode.z == 0)
					return;
			}

			if (MouseDownStart.Lat == 0 || MouseDownStart.Lng == 0)
			{
				// CustomMessageBox.Show(Strings.BadCoords, Strings.ERROR);
				MessageBox.Show(Diva.Properties.Strings.MsgCantGetPosition);
				return;
			}


			if (ActiveDrone.Status.sys_status != (byte)MAVLink.MAV_STATE.ACTIVE)
			{
				DialogResult dr = MessageBox.Show("The Drone must be actived.", "Warning", MessageBoxButtons.OK);
				if (dr == DialogResult.OK) return;
			}


			float targetHeight = 0.0f;
			InputDataDialog _dialog = new InputDataDialog()
			{
				Hint = "Please enter the height",
				Unit = "m",
			};
			
			_dialog.DoClick += (s2, e2) => targetHeight = float.Parse(_dialog.Value);
			_dialog.ShowDialog();

			Locationwp gotohere = new Locationwp();

			gotohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
			gotohere.alt = targetHeight; // back to m
			gotohere.lat = (MouseDownStart.Lat);
			gotohere.lng = (MouseDownStart.Lng);
			

			try
			{
				ActiveDrone.setGuidedModeWP(gotohere);
			}
			catch (Exception ex)
			{
				ActiveDrone.giveComport = false;
				MessageBox.Show(ex.Message);
			}


			overlays.commons.Markers.Clear();
			
			addpolygonmarker("Click & GO", gotohere.lng,
								  gotohere.lat, (int)gotohere.alt, Color.Blue, overlays.commons);


		}

		public int AddCommand(MAVLink.MAV_CMD cmd, double p1, double p2, double p3, double p4, double x, double y,
			double z, object tag = null)
		{
			selectedrow = dgvWayPoints.Rows.Add();

			FillCommand(this.selectedrow, cmd, p1, p2, p3, p4, x, y, z, tag);

			writeKMLV2();

			return selectedrow;
		}

		public void InsertCommand(int rowIndex, MAVLink.MAV_CMD cmd, double p1, double p2, double p3, double p4, double x, double y,
			double z, object tag = null)
		{
			if (dgvWayPoints.Rows.Count <= rowIndex)
			{
				AddCommand(cmd, p1, p2, p3, p4, x, y, z, tag);
				return;
			}

			dgvWayPoints.Rows.Insert(rowIndex);

			this.selectedrow = rowIndex;

			FillCommand(this.selectedrow, cmd, p1, p2, p3, p4, x, y, z, tag);

			writeKMLV2();
		}

		private void FillCommand(int rowIndex, MAVLink.MAV_CMD cmd, double p1, double p2, double p3, double p4, double x,
			double y, double z, object tag = null)
		{
			dgvWayPoints.Rows[rowIndex].Cells[colCommand.Index].Value = cmd.ToString();
			dgvWayPoints.Rows[rowIndex].Cells[colTagData.Index].Tag = tag;
			dgvWayPoints.Rows[rowIndex].Cells[colTagData.Index].Value = tag;

			ChangeColumnHeader(cmd.ToString());

			if (cmd == MAVLink.MAV_CMD.WAYPOINT)
			{
				// add delay if supplied
				dgvWayPoints.Rows[rowIndex].Cells[colParam1.Index].Value = p1;

				setfromMap(y, x, (int)z, Math.Round(p1, 1));
			}
			else if (cmd == MAVLink.MAV_CMD.LOITER_UNLIM)
			{
				setfromMap(y, x, (int)z);
			}
			else
			{
				dgvWayPoints.Rows[rowIndex].Cells[colParam1.Index].Value = p1;
				dgvWayPoints.Rows[rowIndex].Cells[colParam2.Index].Value = p2;
				dgvWayPoints.Rows[rowIndex].Cells[colParam3.Index].Value = p3;
				dgvWayPoints.Rows[rowIndex].Cells[colParam4.Index].Value = p4;
				dgvWayPoints.Rows[rowIndex].Cells[colLatitude.Index].Value = y;
				dgvWayPoints.Rows[rowIndex].Cells[colLongitude.Index].Value = x;
				dgvWayPoints.Rows[rowIndex].Cells[colAltitude.Index].Value = z;
			}
		}

		private void BUT_Connect_Click(object sender, EventArgs e)
		{
            if (OnlineDrones.Count > 0)
            {
                if (MessageBox.Show("Close existing connections before making new ones?", "Drones already connected",
                        MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    return;
                DroneInfoPanel.Clear();
                OnlineDrones.Clear();
            }
            var dsettings = ConfigData.GetTypeList<DroneSetting>();
            if (dsettings.Count == 0)
            {
                BUT_Configure_Click("Vehicle", null);
                return;
            }
            foreach (var dsetting in dsettings)
            {
                try
                {
                    MavDrone drone = null;
                    try
                    {
                        drone = new MavDrone(dsetting);
                        drone?.Connect();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ResStrings.MsgCannotEstablishConnection
                            .FormatWith(ex.Message));
                        drone?.Disconnect();
                        drone = null;
                    }
                    if (drone == null)
                        return;
                    OnlineDrones.Add(DroneInfoPanel.AddDrone(drone)?.Drone);
                }
                catch (Exception exception)
                {
                    log.Debug(exception);
                }
            }
        }

        #region Button click event handlers

        private void BUT_Arm_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.BaseStream.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			// arm the MAV
			try
			{
				log.InfoFormat("mav armed: {0}", ActiveDrone.Status.armed);
				bool ans = ActiveDrone.doARM(!ActiveDrone.Status.armed);
				if (ans == false)
					log.Error("arm failed");
			}
			catch
			{
				log.Error("unknown arm failed");
			}
		}

		private void BUT_Takeoff_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.BaseStream.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			InputDataDialog _dialog = new InputDataDialog()
			{
				Hint = "Please enter the height",
				Unit = "m",
			};

			_dialog.DoClick += (s2, e2) => {
				ActiveDrone.setMode("GUIDED");
				ActiveDrone.doCommand(MAVLink.MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, float.Parse(_dialog.Value));
			};

			_dialog.ShowDialog();

			
		}

		private void BUT_Auto_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.BaseStream.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			if (ActiveDrone.BaseStream.IsOpen)
			{
				// flyToHereAltToolStripMenuItem_Click(null, null);
				ActiveDrone.doCommand(MAVLink.MAV_CMD.MISSION_START, 0, 0, 0, 0, 0, 0, 0);
			}
		}


		private void BUT_RTL_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.BaseStream.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			if (ActiveDrone.BaseStream.IsOpen)
			{
				ActiveDrone.doCommand(MAVLink.MAV_CMD.RETURN_TO_LAUNCH, 0, 0, 0, 0, 0, 0, 0);
			}
		}

		ProgressDialogV2 downloadWPReporter = null;

		/// <summary>
		/// Reads the EEPROM from a com port
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void BUT_read_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.BaseStream.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}


			if (dgvWayPoints.Rows.Count > 0)
			{
				
				if (MessageBox.Show(ResStrings.MsgConfirmClearExistingMission,
					ResStrings.MsgConfirmTitle, MessageBoxButtons.OKCancel) != DialogResult.OK)
				{
					return;
				}
				
			}

			downloadWPReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				HintImage = Resources.icon_info,
				Text = "Downloading waypoints",
			};

			downloadWPReporter.DoWork += getWPs;
			downloadWPReporter.RunBackgroundOperationAsync();
			downloadWPReporter.Dispose();
						
		}


		private ProgressDialogV2 uploadWPReporter;

		public void BUT_write_Click(object sender, EventArgs e)
		{

			if (!ActiveDrone.BaseStream.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			// check home
			Locationwp home = new Locationwp();
			try
			{
				home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
				home.lat = (double.Parse(TxtHomeLatitude.Text));
				home.lng = (double.Parse(TxtHomeLongitude.Text));
				home.alt = (float.Parse(TxtHomeAltitude.Text)); // use saved home
			}
			catch
			{
				MessageBox.Show(ResStrings.MsgHomeLocationInvalid);
				return;
			}

			// check for invalid grid data
			for (int a = 0; a < dgvWayPoints.Rows.Count - 0; a++)
			{
				for (int b = 0; b < dgvWayPoints.ColumnCount - 0; b++)
				{
					double answer;
					if (b >= 1 && b <= 7)
					{
						if (!double.TryParse(dgvWayPoints[b, a].Value.ToString(), out answer))
						{
							MessageBox.Show(ResStrings.MsgMissionError);
							return;
						}
					}

					// if (TXT_altwarn.Text == "")
					// 	TXT_altwarn.Text = (0).ToString();

					if (dgvWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString().Contains("UNKNOWN"))
						continue;

					ushort cmd =
						(ushort)
								Enum.Parse(typeof(MAVLink.MAV_CMD),
									dgvWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString(), false);

					if (cmd < (ushort)MAVLink.MAV_CMD.LAST &&
						double.Parse(dgvWayPoints[colAltitude.Index, a].Value.ToString()) < WARN_ALT)
					{
                        if (cmd != (ushort)MAVLink.MAV_CMD.TAKEOFF &&
                            cmd != (ushort)MAVLink.MAV_CMD.LAND &&
                            cmd != (ushort)MAVLink.MAV_CMD.DELAY &&
							cmd != (ushort)MAVLink.MAV_CMD.RETURN_TO_LAUNCH)
						{
							MessageBox.Show(ResStrings.MsgWarnWPAltitiude.FormatWith(a + 1));
							return;
						}
					}
				}
			}

			uploadWPReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				HintImage = Resources.icon_info,
				Text = "Uploading waypoints",
			};

			uploadWPReporter.DoWork += saveWPs;
			uploadWPReporter.RunBackgroundOperationAsync();
			uploadWPReporter.Dispose();

			myMap.Focus();
		}

		#endregion

		private void setHomeHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TxtHomeAltitude.Text = "0";
			TxtHomeLatitude.Text = MouseDownStart.Lat.ToString();
			TxtHomeLongitude.Text = MouseDownStart.Lng.ToString();

			DatabaseManager.UpdateHomeLocation(recorder_id, MouseDownStart.Lat, MouseDownStart.Lng, 0.0d);
		}

		private Rotation rotationMission = null;

		private void Btn_Rotation_Click(object sender, EventArgs e)
		{
			if (rotationMission == null)
				rotationMission = new Rotation(OnlineDrones);

			rotationMission.Start();
		}
		
		private void BUT_Land_Click(object sender, EventArgs e)
		{
			if (ActiveDrone.BaseStream.IsOpen)
			{
				ActiveDrone.setMode(
					ActiveDrone.Status.sysid,
					ActiveDrone.Status.compid,
					new MAVLink.mavlink_set_mode_t()
					{
						target_system = ActiveDrone.Status.sysid,
						base_mode = (byte)MAVLink.MAV_MODE_FLAG.CUSTOM_MODE_ENABLED,
						custom_mode = (uint)9,
					});
			}
		}


		private Demux demultiplexer = new Demux();
		private bool isDeplexClicked = false;
		private void BUT_Deplex_Click(object sender, EventArgs e)
		{
			isDeplexClicked = !isDeplexClicked;
			if (isDeplexClicked)
			{
				demultiplexer.Active();
			}
			else
			{
				demultiplexer.Deactive();
			}
		}

		private void BUT_Configure_Click(object sender, EventArgs e)
		{
			ConfigureForm config = new ConfigureForm();
            config.InitPage = sender as string;
			config.ShowDialog(this);
		}

		private bool isTagging = false;

		private void BUT_Tagging_CheckedChanged(object sender, EventArgs e)
		{
			isTagging = !isTagging;
		}

		private void BUT_Tagging_Click(object sender, EventArgs e)
		{
			if (!isTagging)
			{
				_tokenSource.Cancel();
			}
			else
			{
				Task.Run(async () => await CleanupAsync());
			}
			
		}


		private CancellationTokenSource _tokenSource = new CancellationTokenSource();

		public async Task CleanupAsync()
		{
			while (!_tokenSource.Token.IsCancellationRequested)
			{
				// Do whatever cleanup you need to.
				Console.WriteLine("ping timer");

				AddWPToMap(ActiveDrone.Status.current_lat, ActiveDrone.Status.current_lng, 10);

				// set the time span
				await Task.Delay(System.TimeSpan.FromSeconds(5), _tokenSource.Token);
			}
		}

		private void VideoDemo_Click(object sender, EventArgs e)
		{
			// string uri = Microsoft.VisualBasic.Interaction.InputBox(ResStrings.MsgSpecifyVideoURI);

			var dsetting = ConfigData.GetTypeList<DroneSetting>()[0];
			string streamUri = dsetting.StreamURI;

			if (streamUri == null || streamUri.Length == 0)
				return;

			try
			{
				MyVideoForm form = new MyVideoForm();
				VideoPlayer player = new VideoPlayer(streamUri);
				form.Controls.Add(player);
				player.Start();
				form.Show();
			}
			catch (Exception ex)
			{
				log.Error(ex.ToString());
			}
		}


		private void BtnSaveMission_Click(object sender, EventArgs e)
		{
			KMLFileUtility kUtility = new KMLFileUtility();

			Locationwp home = new Locationwp()
			{
				id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
				lat = (double.Parse(TxtHomeLatitude.Text)),
				lng = (double.Parse(TxtHomeLongitude.Text)),
				alt = (float.Parse(TxtHomeAltitude.Text)),
			};

			kUtility.SaveKMLMission(GetCommandList(), home);
			Thread.Sleep(1000);
			MessageBox.Show("Save Mission");

			writeKMLV2();
		}

		private void BtnReadMission_Click(object sender, EventArgs e)
		{
			KMLFileUtility kUtility = new KMLFileUtility();
			List<Locationwp> cmds = kUtility.ReadKMLMission();

			
			processToScreen(cmds, false);
			writeKMLV2();
			myMap.ZoomAndCenterMarkers("objects");
		}


		private void But_MapFocus_Click(object sender, EventArgs e)
		{
			isMapFocusing = !isMapFocusing;
		}
		
		private void addPolygonPointToolStripMenuItem_Click(object sender, EventArgs e)
		{
			/**
			if (polygongridmode == false)
			{
				CustomMessageBox.Show(
					"You will remain in polygon mode until you clear the polygon or create a grid/upload a fence");
			}

			polygongridmode = true;*/

			List<PointLatLng> polygonPoints = new List<PointLatLng>();
			if (overlays.drawnpolygons.Polygons.Count == 0)
			{
				drawnPolygon.Points.Clear();
				overlays.drawnpolygons.Polygons.Add(drawnPolygon);
			}

			drawnPolygon.Fill = Brushes.AliceBlue;

			// remove full loop is exists
			if (drawnPolygon.Points.Count > 1 &&
				drawnPolygon.Points[0] == drawnPolygon.Points[drawnPolygon.Points.Count - 1])
				drawnPolygon.Points.RemoveAt(drawnPolygon.Points.Count - 1); // unmake a full loop

			drawnPolygon.Points.Add(new PointLatLng(MouseDownStart.Lat, MouseDownStart.Lng));

			addpolygonmarkergrid(drawnPolygon.Points.Count.ToString(), MouseDownStart.Lng, MouseDownStart.Lat, 0);

			myMap.UpdatePolygonLocalPosition(drawnPolygon);

			myMap.Invalidate();
		}

		private void clearPolygonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// polygongridmode = false;
			if (drawnPolygon == null)
				return;
			drawnPolygon.Points.Clear();
			overlays.drawnpolygons.Markers.Clear();
			myMap.Invalidate();

			writeKMLV2();
		}

		private void savePolygonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (drawnPolygon.Points.Count == 0)
			{
				return;
			}


			using (SaveFileDialog sf = new SaveFileDialog())
			{
				sf.Filter = "Polygon (*.poly)|*.poly";
				sf.ShowDialog();
				if (sf.FileName != "")
				{
					try
					{
						StreamWriter sw = new StreamWriter(sf.OpenFile());

						sw.WriteLine("#saved by Mission Planner " + Application.ProductVersion);

						if (drawnPolygon.Points.Count > 0)
						{
							foreach (var pll in drawnPolygon.Points)
							{
								sw.WriteLine(pll.Lat + " " + pll.Lng);
							}

							PointLatLng pll2 = drawnPolygon.Points[0];

							sw.WriteLine(pll2.Lat + " " + pll2.Lng);
						}

						sw.Close();
					}
					catch
					{
						MessageBox.Show("Failed to write fence file");
					}
				}
			}
		}

		private void loadPolygonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog fd = new OpenFileDialog())
			{
				fd.Filter = "Polygon (*.poly)|*.poly";
				fd.ShowDialog();
				if (File.Exists(fd.FileName))
				{
					StreamReader sr = new StreamReader(fd.OpenFile());

					overlays.drawnpolygons.Markers.Clear();
					overlays.drawnpolygons.Polygons.Clear();
					drawnPolygon.Points.Clear();

					int a = 0;

					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						if (line.StartsWith("#"))
						{
						}
						else
						{
							string[] items = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

							if (items.Length < 2)
								continue;

							drawnPolygon.Points.Add(new PointLatLng(double.Parse(items[0]), double.Parse(items[1])));
							addpolygonmarkergrid(drawnPolygon.Points.Count.ToString(), double.Parse(items[1]),
								double.Parse(items[0]), 0);

							a++;
						}
					}

					// remove loop close
					if (drawnPolygon.Points.Count > 1 &&
						drawnPolygon.Points[0] == drawnPolygon.Points[drawnPolygon.Points.Count - 1])
					{
						drawnPolygon.Points.RemoveAt(drawnPolygon.Points.Count - 1);
					}

					overlays.drawnpolygons.Polygons.Add(drawnPolygon);

					myMap.UpdatePolygonLocalPosition(drawnPolygon);

					myMap.Invalidate();

					myMap.ZoomAndCenterMarkers(overlays.drawnpolygons.Id);
				}
			}
		}

		/// <summary>
		/// from http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
		/// </summary>
		/// <param name="array"> a closed polygon</param>
		/// <param name="testx"></param>
		/// <param name="testy"></param>
		/// <returns> true = outside</returns>
		bool pnpoly(PointLatLng[] array, double testx, double testy)
		{
			int nvert = array.Length;
			int i, j = 0;
			bool c = false;
			for (i = 0, j = nvert - 1; i < nvert; j = i++)
			{
				if (((array[i].Lng > testy) != (array[j].Lng > testy)) &&
					(testx <
					 (array[j].Lat - array[i].Lat) * (testy - array[i].Lng) / (array[j].Lng - array[i].Lng) + array[i].Lat))
					c = !c;
			}
			return c;
		}

		private void GeoFenceuploadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// polygongridmode = false;
			//FENCE_ENABLE ON COPTER
			//FENCE_ACTION ON PLANE
			if (!ActiveDrone.Status.param.ContainsKey("FENCE_ENABLE") && !ActiveDrone.Status.param.ContainsKey("FENCE_ACTION"))
			{
				MessageBox.Show("Not Supported");
				return;
			}

			if (drawnPolygon == null)
			{
				MessageBox.Show("No polygon to upload");
				return;
			}

			if (overlays.geofence.Markers.Count == 0)
			{
				MessageBox.Show("No return location set");
				return;
			}

			if (drawnPolygon.Points.Count == 0)
			{
				MessageBox.Show("No polygon drawn");
				return;
			}

			// check if return is inside polygon
			List<PointLatLng> plll = new List<PointLatLng>(drawnPolygon.Points.ToArray());
			// close it
			plll.Add(plll[0]);
			// check it
			if (
				!pnpoly(plll.ToArray(), overlays.geofence.Markers[0].Position.Lat, overlays.geofence.Markers[0].Position.Lng))
			{
				MessageBox.Show("Your return location is outside the polygon");
				return;
			}

			int minalt = 0;
			int maxalt = 0;

			if (ActiveDrone.Status.param.ContainsKey("FENCE_MINALT"))
			{
				string minalts =
					(int.Parse(ActiveDrone.Status.param["FENCE_MINALT"].ToString()) * CURRENTSTATE_MULTIPLERDIST)
						.ToString("0");
				if (DialogResult.Cancel == InputBox.Show("Min Alt", "Box Minimum Altitude?", ref minalts))
					return;

				if (!int.TryParse(minalts, out minalt))
				{
					MessageBox.Show("Bad Min Alt");
					return;
				}
			}

			if (ActiveDrone.Status.param.ContainsKey("FENCE_MAXALT"))
			{
				string maxalts =
					(int.Parse(ActiveDrone.Status.param["FENCE_MAXALT"].ToString()) * CURRENTSTATE_MULTIPLERDIST)
						.ToString(
							"0");
				if (DialogResult.Cancel == InputBox.Show("Max Alt", "Box Maximum Altitude?", ref maxalts))
					return;

				if (!int.TryParse(maxalts, out maxalt))
				{
					MessageBox.Show("Bad Max Alt");
					return;
				}
			}

			try
			{
				if (ActiveDrone.Status.param.ContainsKey("FENCE_MINALT"))
					ActiveDrone.setParam("FENCE_MINALT", minalt);
				if (ActiveDrone.Status.param.ContainsKey("FENCE_MAXALT"))
					ActiveDrone.setParam("FENCE_MAXALT", maxalt);
			}
			catch (Exception ex)
			{
				log.Error(ex);
				MessageBox.Show("Failed to set min/max fence alt");
				return;
			}

			float oldaction = (float)ActiveDrone.Status.param["FENCE_ACTION"];

			try
			{
				ActiveDrone.setParam("FENCE_ACTION", 0);
			}
			catch
			{
				MessageBox.Show("Failed to set FENCE_ACTION");
				return;
			}

			// points + return + close
			byte pointcount = (byte)(drawnPolygon.Points.Count + 2);


			try
			{
				ActiveDrone.setParam("FENCE_TOTAL", pointcount);
			}
			catch
			{
				MessageBox.Show("Failed to set FENCE_TOTAL");
				return;
			}

			try
			{
				byte a = 0;
				// add return loc
				ActiveDrone.setFencePoint(a, new PointLatLngAlt(overlays.geofence.Markers[0].Position), pointcount);
				a++;
				// add points
				foreach (var pll in drawnPolygon.Points)
				{
					ActiveDrone.setFencePoint(a, new PointLatLngAlt(pll), pointcount);
					a++;
				}

				// add polygon close
				ActiveDrone.setFencePoint(a, new PointLatLngAlt(drawnPolygon.Points[0]), pointcount);

				try
				{
					ActiveDrone.setParam("FENCE_ACTION", oldaction);
				}
				catch
				{
					MessageBox.Show("Failed to restore FENCE_ACTION");
					return;
				}

				// clear everything
				overlays.polygons.Polygons.Clear();
				overlays.polygons.Markers.Clear();
				overlays.geofence.Polygons.Clear();
				geofencePolygon.Points.Clear();

				// add polygon
				geofencePolygon.Points.AddRange(drawnPolygon.Points.ToArray());

				drawnPolygon.Points.Clear();

				overlays.geofence.Polygons.Add(geofencePolygon);

				/**
				// update flightdata
				FlightData.geofence.Markers.Clear();
				FlightData.geofence.Polygons.Clear();
				FlightData.geofence.Polygons.Add(new GMapPolygon(geofencePolygon.Points, "gf fd")
				{
					Stroke = geofencePolygon.Stroke,
					Fill = Brushes.Transparent
				});
				FlightData.geofence.Markers.Add(new GMarkerGoogle(overlays.geofence.Markers[0].Position,
					GMarkerGoogleType.red)
				{
					ToolTipText = overlays.geofence.Markers[0].ToolTipText,
					ToolTipMode = overlays.geofence.Markers[0].ToolTipMode
				});*/

				myMap.UpdatePolygonLocalPosition(geofencePolygon);
				myMap.UpdateMarkerLocalPosition(overlays.geofence.Markers[0]);

				myMap.Invalidate();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to send new fence points " + ex, Strings.ERROR);
			}
		}
	
		private void setReturnLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			overlays.geofence.Markers.Clear();
			overlays.geofence.Markers.Add(new GMarkerGoogle(new PointLatLng(MouseDownStart.Lat, MouseDownStart.Lng),
				GMarkerGoogleType.red)
			{ ToolTipMode = MarkerTooltipMode.OnMouseOver, ToolTipText = "GeoFence Return" });

			myMap.Invalidate();
		}


		private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog fd = new OpenFileDialog())
			{
				fd.Filter = "Fence (*.fen)|*.fen";
				fd.ShowDialog();
				if (File.Exists(fd.FileName))
				{
					StreamReader sr = new StreamReader(fd.OpenFile());

					overlays.drawnpolygons.Markers.Clear();
					overlays.drawnpolygons.Polygons.Clear();
					drawnPolygon.Points.Clear();

					int a = 0;

					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						if (line.StartsWith("#"))
						{
						}
						else
						{
							string[] items = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

							if (a == 0)
							{
								overlays.geofence.Markers.Clear();
								overlays.geofence.Markers.Add(
									new GMarkerGoogle(new PointLatLng(double.Parse(items[0]), double.Parse(items[1])),
										GMarkerGoogleType.red)
									{
										ToolTipMode = MarkerTooltipMode.OnMouseOver,
										ToolTipText = "GeoFence Return"
									});
								myMap.UpdateMarkerLocalPosition(overlays.geofence.Markers[0]);
							}
							else
							{
								drawnPolygon.Points.Add(new PointLatLng(double.Parse(items[0]), double.Parse(items[1])));
								addpolygonmarkergrid(drawnPolygon.Points.Count.ToString(), double.Parse(items[1]),
									double.Parse(items[0]), 0);
							}
							a++;
						}
					}

					// remove loop close
					if (drawnPolygon.Points.Count > 1 &&
						drawnPolygon.Points[0] == drawnPolygon.Points[drawnPolygon.Points.Count - 1])
					{
						drawnPolygon.Points.RemoveAt(drawnPolygon.Points.Count - 1);
					}

					overlays.drawnpolygons.Polygons.Add(drawnPolygon);

					myMap.UpdatePolygonLocalPosition(drawnPolygon);

					myMap.Invalidate();
				}
			}
		}


		private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (overlays.geofence.Markers.Count == 0)
			{
				MessageBox.Show("Please set a return location");
				return;
			}


			using (SaveFileDialog sf = new SaveFileDialog())
			{
				sf.Filter = "Fence (*.fen)|*.fen";
				sf.ShowDialog();
				if (sf.FileName != "")
				{
					try
					{
						StreamWriter sw = new StreamWriter(sf.OpenFile());

						sw.WriteLine("#saved by APM Planner " + Application.ProductVersion);

						sw.WriteLine(overlays.geofence.Markers[0].Position.Lat + " " +
									 overlays.geofence.Markers[0].Position.Lng);
						if (drawnPolygon.Points.Count > 0)
						{
							foreach (var pll in drawnPolygon.Points)
							{
								sw.WriteLine(pll.Lat + " " + pll.Lng);
							}

							PointLatLng pll2 = drawnPolygon.Points[0];

							sw.WriteLine(pll2.Lat + " " + pll2.Lng);
						}
						else
						{
							foreach (var pll in geofencePolygon.Points)
							{
								sw.WriteLine(pll.Lat + " " + pll.Lng);
							}

							PointLatLng pll2 = geofencePolygon.Points[0];

							sw.WriteLine(pll2.Lat + " " + pll2.Lng);
						}

						sw.Close();
					}
					catch
					{
						MessageBox.Show("Failed to write fence file");
					}
				}
			}
		}
		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//FENCE_ENABLE ON COPTER
			//FENCE_ACTION ON PLANE

			try
			{
				ActiveDrone.setParam("FENCE_ENABLE", 0);
			}
			catch
			{
				MessageBox.Show("Failed to set FENCE_ENABLE");
				return;
			}

			try
			{
				ActiveDrone.setParam("FENCE_ACTION", 0);
			}
			catch
			{
				MessageBox.Show("Failed to set FENCE_ACTION");
				return;
			}

			try
			{
				ActiveDrone.setParam("FENCE_TOTAL", 0);
			}
			catch
			{
				MessageBox.Show("Failed to set FENCE_TOTAL");
				return;
			}

			// clear all
			overlays.drawnpolygons.Polygons.Clear();
			overlays.drawnpolygons.Markers.Clear();
			overlays.geofence.Polygons.Clear();
			geofencePolygon.Points.Clear();
		}

		/// <summary>
		/// Draw an mav icon, and update tracker location icon and guided mode wp on FP screen
		/// Frame rate about 20 ms
		/// </summary>

		public static bool isUpdatemapThreadRun = false;
		private void MapitemUpdateLoop()
		{

			while (isUpdatemapThreadRun)
			{
				//if (OnlineDrones.Count == 0) { overlays.routes.Markers.Clear(); }

				try
				{
                    Invoke((MethodInvoker)delegate { overlays.routes.Markers.Clear(); });
                    foreach (MavlinkInterface mav in OnlineDrones)
                    {
						if (mav.Status.current_lat == 0 || mav.Status.current_lng == 0) { continue; }
						var marker = new GMapMarkerQuad(new PointLatLng(mav.Status.current_lat, mav.Status.current_lng),
							mav.Status.yaw, mav.Status.groundcourse, mav.Status.nav_bearing, mav.Status.sysid);
						overlays.routes.Markers.Add(marker);
					}
					
					//autopan
					if (autopan)
					{
						if (route.Points[route.Points.Count - 1].Lat != 0 && (mapupdate.AddSeconds(3) < DateTime.Now))
						{
							PointLatLng currentloc = new PointLatLng(ActiveDrone.Status.current_lat, ActiveDrone.Status.current_lng);
							UpdateMapPosition(currentloc);
							mapupdate = DateTime.Now;
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}

                Thread.Sleep(500);
            }
        }

		#region customized overlay related functions



		private void ReadCustomizedOverlayFile(string file)
		{
			
			List<Customizewp> cmds = new List<Customizewp>();

			try
			{
				Dictionary<string, List<Customizewp>> items = Customizewp.ImportOverlayXML(file);
				foreach (var k in items.Keys) { RenderToMap(k, items[k]); }
		
				// myMap.ZoomAndCenterMarkers("objects");
				string filename = (Path.GetFileName(file)).Split('.')[0];
				MessageBox.Show(ResStrings.MsgCustomizeOverlayImport.FormatWith(filename));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ResStrings.MsgCantOpenFile.FormatWith(ex.Message));
			}
		}

		public void RenderToMap(string areaname, List<Customizewp> cmds)
		{
			// quickadd is for when loading wps from eeprom or file, to prevent slow, loading times
			if (quickadd)
				return;

			
			// generate new polygon every time.
			List<PointLatLng> polygonPointsCus = new List<PointLatLng>();
			GMapCustomizedPolygon customizePolygon = new GMapCustomizedPolygon(polygonPointsCus, "customize", areaname);
			customizePolygon.Stroke = new Pen(Color.Aqua, 2);
			customizePolygon.Fill = Brushes.AliceBlue;
		

			try
			{
				// cmds.ForEach(i => addpolygonmarker("", i.Lng, i.Lat, (int)i.Alt, Color.Aqua, overlays.customize));

				cmds.ForEach(i => {
					StringBuilder sb = new StringBuilder("_cus_");
					customizePolygon.Points.Add(Customizewp.ConvertToPoint(i));
					addpolygonmarkergrid(sb.Append(customizePolygon.Points.Count.ToString()).ToString(), i.Lng, i.Lat, 0);
				});

				overlays.drawnpolygons.Polygons.Add(customizePolygon);
				myMap.UpdatePolygonLocalPosition(customizePolygon);
				myMap.Invalidate();
			}
			catch (Exception ex)
			{
				log.Info(ex.ToString());
			}
		}


		private void LoadCustomizedOverlay_Click(object o, EventArgs e)
		{
			using (OpenFileDialog op = new OpenFileDialog())
			{
				op.Filter = "All Supported Types|*.overlay;*.xml;";
				DialogResult result = op.ShowDialog();
				string file = op.FileName;

				if (File.Exists(file))
				{
					ReadCustomizedOverlayFile(file);
				}
			}
		}

        #endregion

        private void DroneInfoPanel_DroneClosed(object sender, EventArgs e)
        {
            OnlineDrones.Remove((sender as DroneInfo)?.Drone);
            ActiveDrone = DroneInfoPanel.ActiveDroneInfo?.Drone ?? new MavlinkInterface();
        }

        private void DroneInfoPanel_ActiveDroneChanged(object sender, EventArgs e)
        {
            ActiveDrone = (sender as DroneInfo)?.Drone;
        }
	}
}
