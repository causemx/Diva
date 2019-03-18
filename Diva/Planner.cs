using Diva.Controls;
using Diva.Mavlink;
using Diva.Mission;
using Diva.Properties;
using Diva.Utilities;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static MAVLink;

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
        internal static MavDrone DummyDrone = new MavDrone();
        internal static MavDrone GetActiveDrone() => Instance?.ActiveDrone;
        private MavDrone currenDrone = DummyDrone;
        private MavDrone ActiveDrone
        {
            get => currenDrone;
            set => currenDrone = value ?? DummyDrone;
        }
        internal static DroneInfo GetActiveDroneInfo() => Instance.DroneInfoPanel?.ActiveDroneInfo;
        internal int MissionListItemCount => dgvWayPoints.Rows.Count;
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

		private GMapRectMarker currentRectMarker;
		private GMap3DPointMarker currentRallyPt;

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
		private int selectedRow = 0;

		private Dictionary<string, string[]> cmdParamNames = new Dictionary<string, string[]>();
		private List<List<WayPoint>> history = new List<List<WayPoint>>();
		private List<int> groupmarkers = new List<int>();
		private Object thisLock = new Object();
		public List<PointLatLngAlt> pointlist = new List<PointLatLngAlt>(); // used to calc distance
		public List<PointLatLngAlt> fullpointlist = new List<PointLatLngAlt>();

		// Thread setup
		private BackgroundLoop mainThread = null;

		private DateTime heartbeatSend = DateTime.Now;
		private DateTime lastupdate = DateTime.Now;
		private DateTime lastdata = DateTime.MinValue;
		private DateTime mapupdate = DateTime.MinValue;

		private long recorder_id = 0;

		private bool isMapFocusing = true;

		private Rotation rotationMission = null;
		private RotationInfo rotationInfo = null;

		internal MyGMap GMapControl => myMap;
        internal WayPoint GetHomeWP() => new WayPoint
        {
            Id = (ushort)MAV_CMD.WAYPOINT,
            Latitude = (double.Parse(TxtHomeLatitude.Text)),
            Longitude = (double.Parse(TxtHomeLongitude.Text)),
            Altitude = (float.Parse(TxtHomeAltitude.Text)),
            Frame = MAV_FRAME.GLOBAL
        };

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
			UpdateCMDParams();

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

			//setup rotationinfo panel
			rotationInfo = new RotationInfo() { Visible = false };
			PanelRotationInfo.Controls.Add(rotationInfo);

			//set home
			double lng = DEFAULT_LONGITUDE, lat = DEFAULT_LATITUDE, zoom = DEFAULT_ZOOM;
            try
			{
				string loc = ConfigData.GetOption(ConfigData.OptionName.MapInitialLocation);
				if (loc != "")
				{
					string[] locs = loc.Split('|');
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
				UserName = Strings.StrAnonymousAccount,
				StartTime = DatabaseManager.DateTimeSQLite(DateTime.Now),
				EndTime = DatabaseManager.DateTimeSQLite(DateTime.Now),
				TotalDistance = 0.0d,
				HomeLatitude = 0.0d,
				HomeLongitude = 0.0d,
				HomeAltitude = 0.0d,
			};

			DatabaseManager.InitialDatabase();
			recorder_id = DatabaseManager.InsertValue(recorder);

            mainThread = BackgroundLoop.Start(MainLoop);
		}

		private void Planner_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (mainThread?.IsRunning ?? false)
                mainThread.Cancel();
		}

		private void Planner_FormClosed(object sender, FormClosedEventArgs e)
		{
			DatabaseManager.UpdateEndTime(recorder_id, DatabaseManager.DateTimeSQLite(DateTime.Now));
			DatabaseManager.Dump(recorder_id);
            OnlineDrones.ForEach(d => d.Disconnect());
            BackgroundLoop.FreeTasks(5000);
		}

		private void MainLoop(CancellationToken token)
		{
            DateTime nextUpdateTime = DateTime.Now.AddMilliseconds(500);
			while (!token.IsCancellationRequested)
			{
                Thread.Sleep(100);
                if (DateTime.Now < nextUpdateTime) continue;
                nextUpdateTime = DateTime.Now.AddMilliseconds(500);

                try
                {
                    if (ActiveDrone.IsOpen)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            string mode = ActiveDrone.Status.FlightMode.GetName();
                            if (mode != null)
                                TxtDroneMode.Text = mode;
                            DroneInfoPanel.UpdateDisplayInfo();
                        });

                        PointLatLng currentloc = new PointLatLng(ActiveDrone.Status.Latitude, ActiveDrone.Status.Longitude);

                        if (ActiveDrone.Status.Latitude != 0 && ActiveDrone.Status.Longitude != 0)
                        {
                            UpdateMapPosition(currentloc);
                        }
                    }
                    UpdateMapItems();
                }
                catch { }
            }
            Invoke((MethodInvoker)(() => Close()));
		}
		
		DateTime lastmapposchange = DateTime.MinValue;
		private void UpdateMapPosition(PointLatLng currentloc)
		{
			if (!isMapFocusing) return;

			BeginInvoke((MethodInvoker)delegate
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
				catch { }
			});
		}

		private void UpdateRowNumbers()
		{
			// number rows 
			BeginInvoke((MethodInvoker)delegate
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

		private void UpdateCMDParams()
		{
			cmdParamNames = ReadCMDXML();

			List<string> cmds = new List<string>();

			foreach (string item in cmdParamNames.Keys)
			{
				cmds.Add(item);
			}

			cmds.Add("UNKNOWN");

			colCommand.DataSource = cmds;
		}

		Dictionary<string, string[]> ReadCMDXML()
		{
			Dictionary<string, string[]> cmd = new Dictionary<string, string[]>();
						
			using (var file = new MemoryStream(Encoding.UTF8.GetBytes(Resources.mavcmd)))
			using (XmlReader reader = XmlReader.Create(file))
			{
				reader.Read();
				reader.ReadStartElement("CMD");
				if (ActiveDrone.Status.Firmware == Firmwares.ArduPlane ||
					ActiveDrone.Status.Firmware == Firmwares.Ateryx)
				{
					reader.ReadToFollowing("APM");
				}
				else if (ActiveDrone.Status.Firmware == Firmwares.ArduRover)
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

		void AddGroupMarkers(GMapMarker marker)
		{
			Debug.WriteLine("add marker " + marker.Tag.ToString());
			groupmarkers.Add(int.Parse(marker.Tag.ToString()));
			if (marker is GMapTaggedMarker)
			{
				((GMapTaggedMarker)marker).Selected = true;
			}
			if (marker is GMapRectMarker)
			{
				((GMapTaggedMarker)((GMapRectMarker)marker).InnerMarker).Selected = true;
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
									AddGroupMarkers(marker);
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
							DragCallback(item.Key, value.Lat, value.Lng, -1);
							quickadd = false;
						}

						myMap.SelectedArea = RectLatLng.Empty;
						groupmarkers.Clear();
						// redraw to remove selection
						WriteKMLV2();

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
							DragCallback(currentRectMarker.InnerMarker.Tag.ToString(), currentMarker.Position.Lat,
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
						AddGroupMarkers(item);

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
				if (item is GMapRectMarker)
				{
					GMapRectMarker rc = item as GMapRectMarker;
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
				if (item is GMap3DPointMarker)
				{
					currentRallyPt = item as GMap3DPointMarker;
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
				if (item is GMapTaggedMarker)
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
				if (item is GMapRectMarker)
				{
					currentRectMarker = null;
					GMapRectMarker rc = item as GMapRectMarker;
					rc.ResetColor();
					myMap.Invalidate(false);
				}
				if (item is GMap3DPointMarker)
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

		private void AddPolygonMarker(string tag, double lng, double lat, int alt, Color color, GMapOverlay overlay)
		{
			try
			{
				PointLatLng point = new PointLatLng(lat, lng);
                GMarkerGoogle m = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot)
                {
                    ToolTipMode = MarkerTooltipMode.Always,
                    ToolTipText = tag,
                    Tag = tag
                };

                GMapRectMarker mBorders = new GMapRectMarker(point)
                {
                    InnerMarker = m,
                    Color = color
                };

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

		private void AddPolygonMarkerGrid(string tag, double lng, double lat, int alt)
		{
			try
			{
				PointLatLng point = new PointLatLng(lat, lng);
                GMarkerGoogle m = new GMarkerGoogle(point, GMarkerGoogleType.green)
                {
                    ToolTipMode = MarkerTooltipMode.Never,
                    ToolTipText = "grid" + tag,
                    Tag = "grid" + tag
                };

                //MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
                GMapRectMarker mBorders = new GMapRectMarker(point) { InnerMarker = m };

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
					WriteKMLV2();
                    DroneInfoPanel.NotifyMissionChanged();
                }
                // setgradanddistandaz();
            }
			catch (Exception)
			{
				MessageBox.Show(Strings.MsgRowError);
			}
		}

		public void DragCallback(string pointno, double lat, double lng, int alt)
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

			try
			{
				selectedRow = int.Parse(pointno) - 1;
				dgvWayPoints.CurrentCell = dgvWayPoints[1, selectedRow];
				// depending on the dragged item, selectedrow can be reset 
				selectedRow = int.Parse(pointno) - 1;
			}
			catch
			{
				return;
			}

			SetFromMap(lat, lng, alt);
		}

		public void SetFromMap(double lat, double lng, int alt, double p1 = 0)
		{
			if (selectedRow > dgvWayPoints.RowCount)
			{
				MessageBox.Show(Strings.MsgInvalidCoordinate);
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
				MessageBox.Show(Strings.MsgInvalidEntry.FormatWith(ex.Message));
			}

			// remove more than 20 revisions
			if (history.Count > 20)
			{
				history.RemoveRange(0, history.Count - 20);
			}

			DataGridViewTextBoxCell cell;
			if (dgvWayPoints.Columns[colLatitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][4] /*"Lat"*/))
			{
				cell = dgvWayPoints.Rows[selectedRow].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				cell.Value = lat.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (dgvWayPoints.Columns[colLongitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][5] /*"Long"*/))
			{
				cell = dgvWayPoints.Rows[selectedRow].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
				cell.Value = lng.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (alt != -1 && alt != -2 &&
				dgvWayPoints.Columns[colAltitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6] /*"Alt"*/))
			{
				cell = dgvWayPoints.Rows[selectedRow].Cells[colAltitude.Index] as DataGridViewTextBoxCell;

				{
					double result;
					bool pass = double.TryParse(TxtHomeAltitude.Text, out result);

					if (pass == false)
					{
						//MessageBox.Show("You must have a home altitude");

						string homealt = "10";
						//if (DialogResult.Cancel == InputBox.Show("Home Alt", "Home Altitude", ref homealt))
						if (DialogResult.Cancel == InputBox.Show(Strings.MsgHomeAltitudeRequired, homealt, ref homealt))
							return;
						TxtHomeAltitude.Text = homealt;
					}
					int results1;
					if (!int.TryParse(TxtAltitudeValue.Text, out results1))
					{
						MessageBox.Show(Strings.MsgDefaultAltitudeInvalid);
						return;
					}

					if (results1 == 0)
					{
						string defalt = "10";
						if (DialogResult.Cancel == InputBox.Show(Strings.MsgDefaultAltitudeRequired, defalt, ref defalt))
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
					MessageBox.Show(Strings.MsgInvalidHomeOrWPAltitide);
					cell.Style.BackColor = Color.Red;
				}
			}

			// convert to utm
			// convertFromGeographic(lat, lng);

			// Add more for other params
			if (dgvWayPoints.Columns[colParam1.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][1] /*"Delay"*/))
			{
				cell = dgvWayPoints.Rows[selectedRow].Cells[colParam1.Index] as DataGridViewTextBoxCell;
				cell.Value = p1;
				cell.DataGridView.EndEdit();
			}

            WriteKMLV2();
            // writeKML();

            dgvWayPoints.EndEdit();
		}

		public void AddWPToMap(double lat, double lng, int alt)
		{
			// check home point setup.
			if (IsHomeEmpty())
			{
				MessageBox.Show(Strings.MsgSetHomeFirst);
				return;
			}

			// creating a WP
			selectedRow = dgvWayPoints.Rows.Add();

			
			dgvWayPoints.Rows[selectedRow].Cells[colCommand.Index].Value = MAV_CMD.WAYPOINT.ToString();
			ChangeColumnHeader(MAV_CMD.WAYPOINT.ToString());
			

			SetFromMap(lat, lng, alt);
            DroneInfoPanel.NotifyMissionChanged();
        }

        private bool IsHomeEmpty() =>
            !double.TryParse(TxtHomeAltitude.Text, out double holder) ||
                    !double.TryParse(TxtHomeLatitude.Text, out holder) ||
                    !double.TryParse(TxtHomeLongitude.Text, out holder);

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

		public List<WayPoint> GetCommandList()
		{
			List<WayPoint> commands = new List<WayPoint>();

			for (int a = 0; a < dgvWayPoints.Rows.Count - 0; a++)
			{
				var temp = DataViewToWayPoint(a);

				commands.Add(temp);
			}

			return commands;
		}

		private WayPoint DataViewToWayPoint(int i)
		{
            try
			{
                DataGridViewCell getC(DataGridViewBand col) => dgvWayPoints.Rows[i].Cells[col.Index];
                string getS(DataGridViewBand col) => getC(col).Value.ToString();
                double getD(DataGridViewBand col) => double.Parse(getS(col));
                float getF(DataGridViewBand col) => (float)getD(col);

                return new WayPoint()
                {
                    Id = Enum.TryParse<MAV_CMD>(getS(colCommand), out var cmdid) ?
                            (ushort)cmdid : (ushort)getC(colCommand).Tag,
                    // TODO: I don't know where multiplieralt come from..
                    Altitude = getF(colAltitude) / 1,
                    // Gridview does not contains frame field, set waypoint frame to relative by default
                    Frame = cmdid == MAV_CMD.WAYPOINT ? MAV_FRAME.GLOBAL_RELATIVE_ALT : MAV_FRAME.GLOBAL,
                    Latitude = getD(colLatitude),
                    Longitude = getD(colLongitude),
                    Param1 = getF(colParam1),
                    Param2 = getF(colParam2),
                    Param3 = getF(colParam3),
                    Param4 = getF(colParam4),
                    SeqNo = (ushort)(ActiveDrone.Status.APName != MAV_AUTOPILOT.PX4 ? i + 1 : i),
                    Tag = getC(colTagData).Value
                };
			}
			catch (Exception ex)
			{
				throw new FormatException("Invalid number on row " + (i + 1).ToString(), ex);
			}
		}
                
        public void WriteKMLV2()
        {
            // quickadd is for when loading wps from eeprom or file, to prevent slow, loading times
            if (quickadd)
                return;

            UpdateRowNumbers();

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
				selectedRow = e.RowIndex;
				string option = dgvWayPoints[colCommand.Index, selectedRow].EditedFormattedValue.ToString();
				string cmd;
				try
				{
					if (dgvWayPoints[colCommand.Index, selectedRow].Value != null)
						cmd = dgvWayPoints[colCommand.Index, selectedRow].Value.ToString();
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
			selectedRow = e.RowIndex;
			Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex - 0));
			// do header labels - encure we dont 0 out valid colums
			int cols = dgvWayPoints.Columns.Count;
			for (int a = 1; a < cols; a++)
			{
				DataGridViewTextBoxCell cell;
				cell = dgvWayPoints.Rows[selectedRow].Cells[a] as DataGridViewTextBoxCell;

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

		private void SaveWPsToDrone(object sender, ProgressWorkerEventArgs e, object passdata)
        {
            Action<int, string> updateStatus = ((ProgressDialogV2)sender).UpdateProgressAndStatus;
            try
            {
                if (!ActiveDrone.IsOpen)
                {
                    throw new Exception("Please connect first!");
                    // MessageBox.Show(Strings.MsgConnectFirst);
                 
                }

				// log
				log.Info("cmd rows " + (dgvWayPoints.Rows.Count + 1)); // + home

                var dlg = sender as ProgressDialogV2;
                dlg.UpdateProgressAndStatus(0, Strings.MsgDialogSetTotalWps);

                try
                {
                    var cmds = GetCommandList();
                    int totalwps = cmds.Count() +
                        (ActiveDrone.Status.APName != MAV_AUTOPILOT.PX4 ? 1 : 0);
                    var result = ActiveDrone.SetWPs(cmds, GetHomeWP(),
                        (i) =>
                        {
                            dlg.UpdateProgressAndStatus((i * 100 / totalwps), i < 0 ?
                       Strings.MsgDialogSetParams : Strings.MsgDialogSetWp + i);
                            Console.WriteLine("setwps callback: #" + i);
                        });
                }
                catch (Exception ex) when (
                    (ex is InsufficientMemoryException ||
                        ex is NotSupportedException ||
                        ex is InvalidOperationException) &&
                    ex.InnerException != null &&
                    ex.InnerException.Message == "SetWPs")
                {
                    dlg.doWorkArgs.ErrorMessage = ex.Message;
                    return;
                }

                dlg.UpdateProgressAndStatus(-1, Strings.MsgDialogDone);
			}
			catch (Exception ex)
			{
				log.Error(ex);
				throw;
			}
		}

        private void LoadWPsFromDrone(object sender, ProgressWorkerEventArgs e, object passdata = null)
        {
            if (!ActiveDrone.IsOpen)
            {
                // prevent application termination
                //throw new Exception(Diva.Properties.Strings.MsgConnectFirst);
                MessageBox.Show(Strings.MsgConnectFirst);
                return;
            }

            var dlg = sender as ProgressDialogV2;
            int totalWps = -1;

            dlg.UpdateProgressAndStatus(0, Strings.MsgDialogDownloadWps);
            log.Info("Getting WP #");
            List<WayPoint> cmds = ActiveDrone.GetWPs((i) =>
                {
                    if (totalWps < 0)
                        totalWps = i;
                    else
                        dlg.UpdateProgressAndStatus(i * 100 / totalWps, Strings.MsgDialogDownloadWps);
                },
                () => dlg.doWorkArgs.CancelRequested);

            if (cmds != null)
            {
                dlg.UpdateProgressAndStatus(-1, Strings.MsgDialogDone);
                log.Info("Done");
                WPsToScreen(cmds);
            }
            else if (dlg.doWorkArgs.CancelRequested)
            {
                dlg.doWorkArgs.CancelAcknowledged = true;
                throw new Exception("Cancel Requested");
            }
            else
                throw new Exception("LoadWPs: Unknown error");
		}

		public void WPsToScreen(List<WayPoint> cmds, bool withrally = true)
		{
			try
			{
				BeginInvoke((MethodInvoker)delegate
				{
					try
					{
						Console.WriteLine("Process " + cmds.Count);
						WPsToDataView(cmds);
                        DroneInfoPanel.NotifyMissionChanged();
					}
					catch (Exception exx)
					{
						Console.WriteLine(exx.ToString());
					}
					
					try
					{
						if (withrally && ActiveDrone.Status.Params.ContainsKey("RALLY_TOTAL") &&
							int.Parse(ActiveDrone.Status.Params["RALLY_TOTAL"].ToString()) >= 1)
						{
							Console.WriteLine("get rally points");
							GetRallyPoints();
						}
							
					}
					catch
					{
					}

					// BUT_ReadWPs.Enabled = true;

					WriteKMLV2();
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
		public void WPsToDataView(List<WayPoint> cmds, bool append = false)
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
			foreach (WayPoint temp in cmds)
			{
				i++;
				//Console.WriteLine("FP processToScreen " + i);
				if (temp.Id == 0 && i != 0) // 0 and not home
					break;
				if (temp.Id == 255 && i != 0) // bad record - never loaded any WP's - but have started the board up.
					break;
				if (i == 0 && append) // we dont want to add home again.
					continue;
				if (i + 1 >= dgvWayPoints.Rows.Count)
				{
					selectedRow = dgvWayPoints.Rows.Add();
				}
				//if (i == 0 && temp.alt == 0) // skip 0 home
				//  continue;
				DataGridViewTextBoxCell cell;
				DataGridViewComboBoxCell cellcmd;
				cellcmd = dgvWayPoints.Rows[i].Cells[colCommand.Index] as DataGridViewComboBoxCell;
				cellcmd.Value = "UNKNOWN";
				cellcmd.Tag = temp.Id;

				foreach (object value in Enum.GetValues(typeof(MAV_CMD)))
				{
					
					if ((ushort)value == temp.Id)
					{
						cellcmd.Value = value.ToString();
						break;
					}
				}

				cell = dgvWayPoints.Rows[i].Cells[colAltitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Altitude;
				cell = dgvWayPoints.Rows[i].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Latitude;
				cell = dgvWayPoints.Rows[i].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Longitude;

				cell = dgvWayPoints.Rows[i].Cells[colParam1.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param1;
				cell = dgvWayPoints.Rows[i].Cells[colParam2.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param2;
				cell = dgvWayPoints.Rows[i].Cells[colParam3.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param3;
				cell = dgvWayPoints.Rows[i].Cells[colParam4.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param4;

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
						DialogResult dr = MessageBox.Show(Strings.MsgResetHomeCoordinate,
							Strings.MsgResetHomeCoordinateTitle, MessageBoxButtons.YesNo);

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

			WriteKMLV2();

			myMap.ZoomAndCenterMarkers("objects");

			// MainMap_OnMapZoomChanged();
		}

		public void GetRallyPoints()
		{
			if (ActiveDrone.Status.Params["RALLY_TOTAL"] == null)
			{
				MessageBox.Show(Strings.MsgUnsupported);
				return;
			}

			if (int.Parse(ActiveDrone.Status.Params["RALLY_TOTAL"].ToString()) < 1)
			{
				MessageBox.Show(Strings.MsgNoRallyPoint);
				return;
			}

			overlays.rallypoints.Markers.Clear();

			int count = int.Parse(ActiveDrone.Status.Params["RALLY_TOTAL"].ToString());

			for (int a = 0; a < (count); a++)
			{
				try
				{
					PointLatLngAlt plla = ActiveDrone.GetRallyPoint(a, ref count);
					overlays.rallypoints.Markers.Add(new GMap3DPointMarker(plla)
					{
						ToolTipMode = MarkerTooltipMode.OnMouseOver,
						ToolTipText = Strings.StrRallyPointToolTipText.FormatWith(plla.Alt * 1)
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

        internal void ClearMission() => clearMissionToolStripMenuItem_Click(null, null);

        private void clearMissionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			quickadd = true;

			// mono fix
			dgvWayPoints.CurrentCell = null;

			dgvWayPoints.Rows.Clear();

			selectedRow = 0;
			quickadd = false;
			WriteKMLV2();
            DroneInfoPanel.NotifyMissionChanged();
        }

        private void goHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.IsOpen)
			{
				// CustomMessageBox.Show(Strings.PleaseConnect, Strings.ERROR);
				MessageBox.Show(Strings.MsgNoConnection);
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

			if (ActiveDrone.Status.State != (byte)MAV_STATE.ACTIVE)
			{
				DialogResult dr = MessageBox.Show(Strings.MsgWarnDroneMustActive, Strings.DialogTitleWarning, MessageBoxButtons.OK);
				if (dr == DialogResult.OK) return;
			}

			float targetHeight = 0.0f;
			InputDataDialog _dialog = new InputDataDialog()
			{
				Hint = Strings.MsgInputDialogHeightHint,
				Unit = "m",
			};
			
			_dialog.DoClick += (s2, e2) => targetHeight = float.Parse(_dialog.Value);
			_dialog.ShowDialog();

            WayPoint gotohere = new WayPoint
            {
                Id = (ushort)MAV_CMD.WAYPOINT,
                Altitude = targetHeight, // back to m
                Latitude = MouseDownStart.Lat,
                Longitude = MouseDownStart.Lng,
                Frame = MAV_FRAME.GLOBAL_RELATIVE_ALT
            };

            try
			{
				ActiveDrone.SetGuidedModeWP(gotohere);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			overlays.commons.Markers.Clear();
			
			AddPolygonMarker("Click & GO", gotohere.Longitude,
								  gotohere.Latitude, (int)gotohere.Altitude, Color.Blue, overlays.commons);
		}

		public int AddCommand(MAV_CMD cmd, double p1, double p2, double p3, double p4, double x, double y,
			double z, object tag = null)
		{
			selectedRow = dgvWayPoints.Rows.Add();

			FillCommand(this.selectedRow, cmd, p1, p2, p3, p4, x, y, z, tag);

			WriteKMLV2();

			return selectedRow;
		}

		public void InsertCommand(int rowIndex, MAV_CMD cmd, double p1, double p2, double p3, double p4, double x, double y,
			double z, object tag = null)
		{
			if (dgvWayPoints.Rows.Count <= rowIndex)
			{
				AddCommand(cmd, p1, p2, p3, p4, x, y, z, tag);
				return;
			}

			dgvWayPoints.Rows.Insert(rowIndex);

			this.selectedRow = rowIndex;

			FillCommand(this.selectedRow, cmd, p1, p2, p3, p4, x, y, z, tag);

			WriteKMLV2();
		}

		private void FillCommand(int rowIndex, MAV_CMD cmd, double p1, double p2, double p3, double p4, double x,
			double y, double z, object tag = null)
		{
			dgvWayPoints.Rows[rowIndex].Cells[colCommand.Index].Value = cmd.ToString();
			dgvWayPoints.Rows[rowIndex].Cells[colTagData.Index].Tag = tag;
			dgvWayPoints.Rows[rowIndex].Cells[colTagData.Index].Value = tag;

			ChangeColumnHeader(cmd.ToString());

			if (cmd == MAV_CMD.WAYPOINT)
			{
				// add delay if supplied
				dgvWayPoints.Rows[rowIndex].Cells[colParam1.Index].Value = p1;

				SetFromMap(y, x, (int)z, Math.Round(p1, 1));
			}
			else if (cmd == MAV_CMD.LOITER_UNLIM)
			{
				SetFromMap(y, x, (int)z);
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
                OnlineDrones.Clear();
                DroneInfoPanel.Clear();
                overlays.routes.Markers.Clear();
            }
            var dsettings = ConfigData.GetTypeList<DroneSetting>().Where(d => d.Checked);
            if (!dsettings.Any())
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
                        MessageBox.Show(Strings.MsgCannotEstablishConnection
                            .FormatWith(ex.Message));
                        drone?.Disconnect();
                        drone = null;
                    }
                    if (drone?.IsOpen ?? false)
                    {
                        DroneInfoPanel.AddDrone(drone);
                        OnlineDrones.Add(drone);
                        overlays.routes.Markers.Add(new GMapDroneMarker(drone));
                    }
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
			if (!ActiveDrone.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			// arm the MAV
			try
			{
				log.InfoFormat("mav armed: {0}", ActiveDrone.Status.IsArmed);
				bool ans = ActiveDrone.DoArm(!ActiveDrone.Status.IsArmed);
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
			if (!ActiveDrone.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			InputDataDialog _dialog = new InputDataDialog()
			{
				Hint = Strings.MsgInputDialogHeightHint,
				Unit = "m",
			};

			_dialog.DoClick += (s2, e2) => {
				ActiveDrone.SetMode("GUIDED");
				ActiveDrone.TakeOff(float.Parse(_dialog.Value));
			};

			_dialog.ShowDialog();

			
		}

		private void BUT_Auto_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			if (ActiveDrone.IsOpen)
			{
                ActiveDrone.StartMission();
            }
		}

		private void BUT_RTL_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			if (ActiveDrone.IsOpen)
			{
				ActiveDrone.ReturnToLaunch();
			}
		}

		public void BUT_read_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			if (dgvWayPoints.Rows.Count > 0)
			{
				
				if (MessageBox.Show(Strings.MsgConfirmClearExistingMission,
					Strings.MsgConfirmTitle, MessageBoxButtons.OKCancel) != DialogResult.OK)
				{
					return;
				}
				
			}

			var downloadWPReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				HintImage = Resources.icon_info,
				Text = Strings.MsgDialogDownloadWps,
			};

			downloadWPReporter.DoWork += LoadWPsFromDrone;
			downloadWPReporter.RunBackgroundOperationAsync();
			downloadWPReporter.Dispose();
						
		}

		public void BUT_write_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

			// check home
			WayPoint home;
			try
			{
                home = GetHomeWP();
			}
			catch
			{
				MessageBox.Show(Strings.MsgHomeLocationInvalid);
				return;
			}

			// check for invalid grid data
			for (int a = 0; a < dgvWayPoints.Rows.Count - 0; a++)
			{
				for (int b = 0; b < dgvWayPoints.ColumnCount - 0; b++)
				{
                    if (b >= 1 && b <= 7)
                    {
                        if (!double.TryParse(dgvWayPoints[b, a].Value.ToString(), out double answer))
                        {
                            MessageBox.Show(Strings.MsgMissionError);
                            return;
                        }
                    }

                    // if (TXT_altwarn.Text == "")
                    // 	TXT_altwarn.Text = (0).ToString();

                    if (dgvWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString().Contains("UNKNOWN"))
						continue;

					ushort cmd = (ushort)Enum.Parse(typeof(MAV_CMD),
									dgvWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString(),
                                    false);

					if (cmd < (ushort)MAV_CMD.LAST &&
						double.Parse(dgvWayPoints[colAltitude.Index, a].Value.ToString()) < WARN_ALT)
					{
                        if (cmd != (ushort)MAV_CMD.TAKEOFF &&
                            cmd != (ushort)MAV_CMD.LAND &&
                            cmd != (ushort)MAV_CMD.DELAY &&
							cmd != (ushort)MAV_CMD.RETURN_TO_LAUNCH)
						{
							MessageBox.Show(Strings.MsgWarnWPAltitiude.FormatWith(a + 1));
							return;
						}
					}
				}
			}

			var uploadWPReporter = new ProgressDialogV2
			{
				StartPosition = FormStartPosition.CenterScreen,
				HintImage = Resources.icon_info,
				Text = Strings.MsgDialogUploadWps,
			};

			uploadWPReporter.DoWork += SaveWPsToDrone;
			uploadWPReporter.RunBackgroundOperationAsync();
			uploadWPReporter.Dispose();

			myMap.Focus();
		}

		private void setHomeHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TxtHomeAltitude.Text = "0";
			TxtHomeLatitude.Text = MouseDownStart.Lat.ToString();
			TxtHomeLongitude.Text = MouseDownStart.Lng.ToString();

			DatabaseManager.UpdateHomeLocation(recorder_id, MouseDownStart.Lat, MouseDownStart.Lng, 0.0d);
		}

		private void Btn_Rotation_Click(object sender, EventArgs e)
		{
			if (OnlineDrones.Count == 0) return;

			if (rotationMission == null) { rotationMission = new Rotation(OnlineDrones, rotationInfo); }
			try
			{
				if (rotationMission.IsActived()) {
					MessageBox.Show(Strings.MsgWarnRotationExcuteing);
					return;
				}
				rotationMission.ShowDialog();
				rotationMission.Start(); 
			}
			catch (Exception e1)
			{
				log.Error(e1.ToString());
			}

		}
		
		private void BUT_Land_Click(object sender, EventArgs e)
		{
			if (ActiveDrone.IsOpen)
			{
                ActiveDrone.SetMode("LAND");
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

				AddWPToMap(ActiveDrone.Status.Latitude, ActiveDrone.Status.Longitude, 10);

				// set the time span
				await Task.Delay(System.TimeSpan.FromSeconds(5), _tokenSource.Token);
			}
		}

		private void VideoDemo_Click(object sender, EventArgs e)
		{
			// string uri = Microsoft.VisualBasic.Interaction.InputBox(Strings.MsgSpecifyVideoURI);

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
			WayPoint home = new WayPoint
			{
				Id = (ushort)MAV_CMD.WAYPOINT,
				Latitude = (double.Parse(TxtHomeLatitude.Text)),
				Longitude = (double.Parse(TxtHomeLongitude.Text)),
				Altitude = (float.Parse(TxtHomeAltitude.Text)),
			};

            KMLFileUtility.SaveKMLMission(GetCommandList(), home);

			WriteKMLV2();
		}

		private void BtnReadMission_Click(object sender, EventArgs e)
		{
			try
			{
				List<WayPoint> cmds = KMLFileUtility.ReadKMLMission();
                if (cmds != null)
                {
                    WPsToDataView(cmds, false);
                    WriteKMLV2();
                    myMap.ZoomAndCenterMarkers("objects");
                }
            }
			catch (Exception ex)
			{
				log.Warn(ex);
                MessageBox.Show(Strings.MsgErrorReadingKmlFile + ex);
			}
			
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

			AddPolygonMarkerGrid(drawnPolygon.Points.Count.ToString(), MouseDownStart.Lng, MouseDownStart.Lat, 0);

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

			WriteKMLV2();
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
							AddPolygonMarkerGrid(drawnPolygon.Points.Count.ToString(), double.Parse(items[1]),
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

		private void GeoFenceuploadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// polygongridmode = false;
			//FENCE_ENABLE ON COPTER
			//FENCE_ACTION ON PLANE
			if (!ActiveDrone.Status.Params.ContainsKey("FENCE_ENABLE") && !ActiveDrone.Status.Params.ContainsKey("FENCE_ACTION"))
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
            if (!overlays.geofence.Markers[0].Position.InsideOf(plll))
			{
				MessageBox.Show("Your return location is outside the polygon");
				return;
			}

			int minalt = 0;
			int maxalt = 0;

			if (ActiveDrone.Status.Params.ContainsKey("FENCE_MINALT"))
			{
				string minalts =
					(int.Parse(ActiveDrone.Status.Params["FENCE_MINALT"].ToString()) * CURRENTSTATE_MULTIPLERDIST)
						.ToString("0");
				if (DialogResult.Cancel == InputBox.Show("Min Alt", "Box Minimum Altitude?", ref minalts))
					return;

				if (!int.TryParse(minalts, out minalt))
				{
					MessageBox.Show("Bad Min Alt");
					return;
				}
			}

			if (ActiveDrone.Status.Params.ContainsKey("FENCE_MAXALT"))
			{
				string maxalts =
					(int.Parse(ActiveDrone.Status.Params["FENCE_MAXALT"].ToString()) * CURRENTSTATE_MULTIPLERDIST)
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
				if (ActiveDrone.Status.Params.ContainsKey("FENCE_MINALT"))
					ActiveDrone.SetParam("FENCE_MINALT", minalt);
				if (ActiveDrone.Status.Params.ContainsKey("FENCE_MAXALT"))
					ActiveDrone.SetParam("FENCE_MAXALT", maxalt);
			}
			catch (Exception ex)
			{
				log.Error(ex);
				MessageBox.Show("Failed to set min/max fence alt");
				return;
			}

			float oldaction = (float)ActiveDrone.Status.Params["FENCE_ACTION"];

			try
			{
				ActiveDrone.SetParam("FENCE_ACTION", 0);
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
				ActiveDrone.SetParam("FENCE_TOTAL", pointcount);
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
				ActiveDrone.SetFencePoint(a, new PointLatLngAlt(overlays.geofence.Markers[0].Position), pointcount);
				a++;
				// add points
				foreach (var pll in drawnPolygon.Points)
				{
					ActiveDrone.SetFencePoint(a, new PointLatLngAlt(pll), pointcount);
					a++;
				}

				// add polygon close
				ActiveDrone.SetFencePoint(a, new PointLatLngAlt(drawnPolygon.Points[0]), pointcount);

				try
				{
					ActiveDrone.SetParam("FENCE_ACTION", oldaction);
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
				MessageBox.Show(Strings.MsgFailedToSendNewFencePoints + ex, Strings.DialogTitleError);
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
								AddPolygonMarkerGrid(drawnPolygon.Points.Count.ToString(), double.Parse(items[1]),
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

		private void powerConsumptionToolStripMenuItem_Click(object sender, EventArgs e)
		{
            DroneInfoPanel.NotifyMissionChanged();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//FENCE_ENABLE ON COPTER
			//FENCE_ACTION ON PLANE

			try
			{
				ActiveDrone.SetParam("FENCE_ENABLE", 0);
			}
			catch
			{
				MessageBox.Show("Failed to set FENCE_ENABLE");
				return;
			}

			try
			{
				ActiveDrone.SetParam("FENCE_ACTION", 0);
			}
			catch
			{
				MessageBox.Show("Failed to set FENCE_ACTION");
				return;
			}

			try
			{
				ActiveDrone.SetParam("FENCE_TOTAL", 0);
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

		#endregion

        private void UpdateMapItems()
        {
            try
            {
                // overlay marks has to be touched for updating
                if (overlays.routes.Markers.Count > 0)
                    BeginInvoke((MethodInvoker)delegate
                    {
                        lock (overlays)
                            if (overlays.routes.Markers.Count > 0)
                                overlays.routes.Markers[0] = overlays.routes.Markers[0];
                    });

                //autopan
                if (autopan)
                {
                    if (route.Points[route.Points.Count - 1].Lat != 0 && (mapupdate.AddSeconds(3) < DateTime.Now))
                    {
                        PointLatLng currentloc = new PointLatLng(ActiveDrone.Status.Latitude, ActiveDrone.Status.Longitude);
                        UpdateMapPosition(currentloc);
                        mapupdate = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
				MessageBox.Show(Strings.MsgCustomizeOverlayImport.FormatWith(filename));
			}
			catch (Exception ex)
			{
				MessageBox.Show(Strings.MsgCantOpenFile.FormatWith(ex.Message));
			}
		}

		public void RenderToMap(string areaname, List<Customizewp> cmds)
		{
			// quickadd is for when loading wps from eeprom or file, to prevent slow, loading times
			if (quickadd)
				return;
			
			// generate new polygon every time.
			List<PointLatLng> polygonPointsCus = new List<PointLatLng>();
			GMapCustomizedPolygonMarker customizePolygon = new GMapCustomizedPolygonMarker(polygonPointsCus, "customize", areaname);
			customizePolygon.Stroke = new Pen(Color.Aqua, 2);
			customizePolygon.Fill = Brushes.AliceBlue;

			try
			{
				// cmds.ForEach(i => addpolygonmarker("", i.Lng, i.Lat, (int)i.Alt, Color.Aqua, overlays.customize));

				cmds.ForEach(i => {
					StringBuilder sb = new StringBuilder("_cus_");
					customizePolygon.Points.Add(Customizewp.ConvertToPoint(i));
					AddPolygonMarkerGrid(sb.Append(customizePolygon.Points.Count.ToString()).ToString(), i.Lng, i.Lat, 0);
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
            var drone = (sender as DroneInfo)?.Drone;
            OnlineDrones.Remove(drone);
            try
            {
                overlays.routes.Markers.Remove(overlays.routes.Markers.Single(
                    x => (x as GMapDroneMarker).Drone == drone));
            }
            catch
            {
            }
        }

        private void DroneInfoPanel_ActiveDroneChanged(object sender, EventArgs e)
        {
            if ((ActiveDrone = (sender as DroneInfo)?.Drone) != null)
            {
                DroneModeChangedCallback(ActiveDrone);
            }
        }

        public void DroneModeChangedCallback(MavDrone drone)
        {
            void updateText() { TxtDroneMode.Text = drone.Status.FlightMode.GetName(); }
            if (drone == ActiveDrone)
            {
                if (InvokeRequired)
                    BeginInvoke((MethodInvoker)delegate { updateText(); });
                else
                    updateText();
            }
        }
	}
}
