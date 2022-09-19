using Diva.Controls;
using Diva.Controls.Dialogs;
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
using MyTSButton = Diva.Controls.Components.MyTSButton;

namespace Diva
{
	public partial class Planner : Form
	{
		public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public const int CURRENTSTATE_MULTIPLERDIST = 1;

        private static Planner Instance = null;
        internal static Planner GetPlannerInstance() => Instance;
        internal static MavDrone DummyDrone = new MavDrone();
        internal static MavDrone GetActiveDrone() => Instance?.ActiveDrone;
        private MavDrone currentDrone = DummyDrone;
        private MavDrone ActiveDrone
        {
            get => currentDrone;
            set => currentDrone = value ?? DummyDrone;
        }
        internal static DroneInfo GetActiveDroneInfo() => Instance.DroneInfoPanel?.ActiveDroneInfo;
        internal int MissionListItemCount => DGVWayPoints.Rows.Count;
        private List<MavDrone> OnlineDrones = new List<MavDrone>();

        public bool Autopan { get; set; }

		private static readonly double WARN_ALT = 2D;

        public class FloatingIcon : Controls.Icons.Icon
        {
			private Bitmap bmp = null;

			public FloatingIcon(Bitmap _bmp)
            {
				this.bmp = _bmp;
            }

            internal override void doPaint(Graphics g)
            {	
				g.DrawImage(bmp, (Width/2-bmp.Width/2), (Height/2-bmp.Height/2));
			}
        }

		private FloatingIcon polyicon = new FloatingIcon(Resources.icon_fish_24);
		private FloatingIcon ekficon = new FloatingIcon(Resources.icon_ekf_24);

        private class PlannerOverlays
		{
			public GMapOverlay RallyPoints;
			public GMapOverlay Commons;
			public GMapOverlay DrawnPolygons;
			public GMapOverlay Geofence;
			public GMapOverlay Drones;
            public GMapOverlay Routes;
			internal PlannerOverlays(MyGMap map)
				=> GetType().GetFields().ToList().ForEach(f =>
					{
						var o = new GMapOverlay(f.Name);
						if (map != null) map.Overlays.Add(o);
						f.SetValue(this, o);
					});
		}
		private PlannerOverlays Overlays;
        private Dictionary<MavDrone, GMapRoute> DroneRoutes =
            new Dictionary<MavDrone, GMapRoute>();

		private GMapRectMarker CurrentRectMarker;
		private GMapMarkerRallyPt CurrentRallyPt;
		private GMapPlusMarker CurrentMidline;

		private GMapMarker CurrentMarker;
		private GMapMarker CenterMarker = new GMarkerGoogle(new PointLatLng(0.0, 0.0), GMarkerGoogleType.none);
		private GMapMarker CurrentGMapMarker;

		public GMapRoute route = new GMapRoute("wp route");

		private bool isMouseDown;
		private bool isMouseDraging;
		private PointLatLng MouseDownStart;
		internal PointLatLng MouseDownEnd;

		// polygon
		internal GMapPolygon geofencePolygon;
		internal GMapPolygon drawnPolygon;
        internal GMapPolygon wpPolygon;

        public bool quickadd = false;
		private int selectedRow = 0;
		private bool polygongridmode;

		private Dictionary<string, string[]> cmdParamNames = new Dictionary<string, string[]>();
		private List<List<WayPoint>> history = new List<List<WayPoint>>();
		private List<int> groupmarkers = new List<int>();

		// Thread setup
		private BackgroundLoop mainThread = null;
		private DateTime mapupdate = DateTime.MinValue;


		private bool IsMapFocusing
        {
            get => BtnMapFocus.FlatAppearance.BorderColor == Color.Red;
            set
            {
                //BtnMapFocus.Image = value ? Resources.
				//_zoom_focus_locked : Resources.icon_zoom_focus;
                BtnMapFocus.FlatAppearance.BorderColor = value ? Color.Red : Color.Black;
            }
        }

		internal MyGMap GMapControl => Map;
        internal WayPoint GetHomeWP() => new WayPoint
        {
            Id = (ushort)MAV_CMD.WAYPOINT,
            Latitude = (double.Parse(TxtHomeLatitude.Text)),
            Longitude = (double.Parse(TxtHomeLongitude.Text)),
            Altitude = (float.Parse(TxtHomeAltitude.Text)),
            Tag = "H",
            Frame = MAV_FRAME.GLOBAL
        };


		public Planner()
		{
			InitializeComponent();
			Instance = this;

			// control size may not be the same as designer (dpi setting?)

			quickadd = false;

			Overlays = new PlannerOverlays(Map);

			// set current marker
			CurrentMarker = new GMarkerGoogle(Map.Position, GMarkerGoogleType.red);
			//top.Markers.Add(currentMarker);

			// map center
			CenterMarker = new GMarkerGoogle(Map.Position, GMarkerGoogleType.none);
			//top.Markers.Add(center);

			//myMap.Zoom = 3;

			// RegeneratePolygon();
			UpdateCMDParams();

			DataGridView_Initialize();

			//setup toolstrip
			// TSMainPanel.Renderer = new Controls.Components.MyTSRenderer();
			flyToolStrip.Renderer = new Controls.Components.MyTSRenderer();
			//Collect DroneInfoPanels

			// setup geofence
			geofencePolygon = new GMapPolygon(new List<PointLatLng>(), "geofence")
            {
                Stroke = new Pen(Color.Pink, 5),
                Fill = Brushes.Transparent
            };

            //setup drawnpolgon
            drawnPolygon = new GMapPolygon(new List<PointLatLng>(), "drawnpoly")
            {
                Stroke = new Pen(Color.Tomato, 2),
                Fill = Brushes.Transparent
            };

			//set home
			double lng = DefaultValues.Longitude, lat = DefaultValues.Latitude, zoom = DefaultValues.ZoomLevel;
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
			Map.Position = new PointLatLng(lat, lng);
			Map.Zoom = zoom;
			TxtHomeLatitude.Text = lat.ToString();
			TxtHomeLongitude.Text = lng.ToString();
            TxtHomeAltitude.Text = DefaultValues.TakeoffHeight.ToString();

            Overlays.Routes.IsVisibile = ConfigData.GetBoolOption("DisplayDroneRoute");

            HUD.BorderColor = BackColor;
            HUD.TextFont = Font;
            HUD.GetReferencePoint = () => {
                if (DroneInfoPanel.HUDShown)
                    return new Point(DroneInfoPanel.Left, DroneInfoPanel.Bottom);
                return null;
            };
            HUD.GetAttitude = () =>
            {
                if (ActiveDrone != null)
                {
                    var s = ActiveDrone.Status;
                    return (s.Roll, s.Yaw, s.Pitch);
                }
                return (float.NaN, float.NaN, float.NaN);
            };

            IsMapFocusing = true;

            SetupMIRDC();

            mainThread = BackgroundLoop.Start(MainLoop);
        }

		private void Map_Paint(object sender, PaintEventArgs e)
		{
			polyicon.Location = new Point(20, 575);
			polyicon.Paint(e.Graphics);

			ekficon.Location = new Point(20, 585);
			ekficon.Paint(e.Graphics);
		}

		private void DataGridView_Initialize()
		{
			foreach (DataGridViewColumn commandsColumn in DGVWayPoints.Columns)
			{
                if (commandsColumn is DataGridViewTextBoxColumn)
                    commandsColumn.CellTemplate.Value = "0";
            }
			DGVWayPoints.Columns[colDelete.Index].CellTemplate.Value = "X";
		}

        private void Planner_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (mainThread?.IsRunning ?? false)
            {
                mainThread.Cancel();
                FlyTo.DisposeAll();
            }
        }

		private void Planner_FormClosed(object sender, FormClosedEventArgs e)
		{
            OnlineDrones.ForEach(d => d.Disconnect());
            BackgroundLoop.FreeTasks(5000);
		}

        public event EventHandler BackgroundTimer;
        private GMapDroneMarker BaseMarker = new GMapDroneMarker(BaseLocation.AsDrone);
		private void MainLoop(CancellationToken token)
		{
            DateTime nextUpdateTime = DateTime.Now.AddMilliseconds(500);
            BaseMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            BaseMarker.ToolTip = new GMapToolTip(BaseMarker);
			while (!token.IsCancellationRequested)
			{
                Thread.Sleep(100);
                if (Disposing) break;
                if (DateTime.Now < nextUpdateTime) continue;
                nextUpdateTime = DateTime.Now.AddMilliseconds(500);

                try
                {
                    var ship = FullControl ? OnlineDrones.Find(d => d.IsShip()) : null;
                    HomeLocation = ship?.Status.Location ?? BaseLocation.Location;

                    bool markerShown = Overlays.Commons.Markers.Contains(BaseMarker);
                    var overlay = Overlays.Commons;
                    if (ship == null && BaseLocation.Ready)
                    {
                        if (!markerShown)
                            overlay.Markers.Add(BaseMarker);
                        else
                            BaseMarker.Position = BaseLocation.Location;
                    }
                    else if (markerShown)
                        overlay.Markers.Remove(BaseMarker);

                    bool noGPS = ship == null && !(BaseLocation.Initialized && BaseLocation.Ready);
                    if (noGPS != IconGPSLostWarning.Visible)
                        BeginInvoke((MethodInvoker)(() => IconGPSLostWarning.Visible = noGPS));

                    string toFixed(double d, int digits = 1) => d.ToString($"N{digits}");
                    GMapDroneMarker findMarker(MavDrone drone)
                    {
                        GMapDroneMarker marker = null;
                        try
                        {
                            if (drone != null)
                                marker = Overlays.Drones.Markers.FirstOrDefault(m =>
                                    (m as GMapDroneMarker)?.Drone == drone) as GMapDroneMarker;
                        }
                        catch { }
                        return marker;
                    }

                    if (overlay.Markers.Contains(BaseMarker))
                    {
                        BaseMarker.ToolTipText = "";
                        if (OnlineDrones.Count > 0)
                        {
                            var from = BaseLocation.Location;
                            var to = OnlineDrones[0].Status.Location;
                            var kms = overlay.Control.MapProvider.Projection.GetDistance(from, to);
                            var d = (kms > 10 ? $"{toFixed(kms, 3)}km" : $"{toFixed(kms * 1000)}m");
                            BaseMarker.ToolTipText = "To drone: " + d;
                            var marker = findMarker(OnlineDrones.FirstOrDefault());
                            if (marker != null) marker.ToolTipText = "To ship: " + d;
                        }
                    }
                    else if (ship != null)
                    {
                        var from = ship.Status.Location;
                        var smarker = findMarker(ship);
                        OnlineDrones.ForEach(d => {
                            if (d != ship)
                            {
                                var to = d.Status.Location;
                                var kms = overlay.Control.MapProvider.Projection.GetDistance(from, to);
                                var dist = (kms > 10 ? $"{toFixed(kms, 3)}km" : $"{toFixed(kms * 1000)}m");
                                if (smarker != null) smarker.ToolTipText = "To drone: " + dist;
                                var marker = findMarker(d);
                                if (marker != null) marker.ToolTipText = "To ship: " + dist;
                            }
                        });
                    }
                }
                catch { }

                try
                {
                    if (AltitudeControl.VerifyDroneStates(ActiveDrone))
                        AltitudeControlPanel.Targeting = false;

                    // drone info panel must be updated whether active drone presents or not
                    BeginInvoke((MethodInvoker)DroneInfoPanel.UpdateDisplayInfo);

                    if (ActiveDrone?.IsOpen == true)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            // should be handled by ActiveDrone's FlightModeChanged event
                            /*string mode = ActiveDrone.Status.FlightMode.GetName();
                            if (mode != null)
                                LBLMode.Text = mode;*/
                            if (IconModeWarning.BackgroundImage == Resources.rc_controlling &&
                                0 != (ActiveDrone.Status.SensorError & (UInt32)MAV_SYS_STATUS_SENSOR.RC_RECEIVER))
                                BeginInvoke((MethodInvoker)(() => IconModeWarning.Text = "No Signal"));
                        });

                        PointLatLng currentloc = ActiveDrone.Status.Location;

                        if (currentloc.Lat != 0 && currentloc.Lng != 0)
                        {
                            UpdateMapPosition(currentloc);
                        }
                    }
                    UpdateMapItems();
                    BackgroundTimer?.Invoke(this, null);

                    // update drone location
                    if (CurrentFlyTo?.State == FlyToState.Setting)
                        CurrentFlyTo.SetDestination(CurrentFlyTo.To);
                }
                catch { }
            }
            Invoke((MethodInvoker)(() => Close()));
		}

		private DateTime lastmapposchange = DateTime.MinValue;
		private void UpdateMapPosition(PointLatLng currentloc)
		{
			if (!IsMapFocusing) return;

			BeginInvoke((MethodInvoker)delegate
			{
				try
				{
					if (lastmapposchange.Second != DateTime.Now.Second)
					{
						if (Math.Abs(currentloc.Lat - Map.Position.Lat) > 0.0001 || Math.Abs(currentloc.Lng - Map.Position.Lng) > 0.0001)
						{
							Map.Position = currentloc;
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
				for (int a = 0; a < DGVWayPoints.Rows.Count - 0; a++)
				{
					try
					{
						if (DGVWayPoints.Rows[a].HeaderCell.Value == null)
						{
							//Commands.Rows[a].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
							DGVWayPoints.Rows[a].HeaderCell.Value = (a + 1).ToString();
						}
						// skip rows with the correct number
						string rowno = DGVWayPoints.Rows[a].HeaderCell.Value.ToString();
						if (!rowno.Equals((a + 1).ToString()))
						{
							// this code is where the delay is when deleting.
							DGVWayPoints.Rows[a].HeaderCell.Value = (a + 1).ToString();
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

            ComBoxModeSwitch.DataSource = ActiveDrone?.Status.FlightModeType?.Values ?? FlightMode.CopterMode.Values;
        }

        private static Dictionary<string, string[]> PlaneCmds, CopterCmds;
        private Dictionary<string, string[]> ReadCMDXML()
		{
            if ((ActiveDrone.Status.Firmware == Firmwares.ArduPlane ||
                ActiveDrone.Status.Firmware == Firmwares.Ateryx) && PlaneCmds != null)
                return PlaneCmds;
            if (ActiveDrone.Status.Firmware == Firmwares.ArduCopter2
                && CopterCmds != null)
                return CopterCmds;

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
                    PlaneCmds = cmd;

                }
				else if (ActiveDrone.Status.Firmware == Firmwares.ArduRover)
				{
					reader.ReadToFollowing("APRover");
				}
				else
				{
					reader.ReadToFollowing("AC2");
                    CopterCmds = cmd;
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
			CenterMarker.Position = point;
		}

		private void AddGroupMarkers(GMapMarker marker)
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
            MouseDownEnd = Map.FromLocalToLatLng(e.X, e.Y);

            if (FlyToClicked && !isMouseDraging)
            {
                try
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (CurrentFlyTo?.ValidTarget != true)
                            CurrentFlyTo?.Dispose();
                        else if (CurrentFlyTo.Start())
                        {
                            CurrentFlyTo.DestinationReached += (o, r) => BeginInvoke((MethodInvoker)(() =>
                            {
                                if (((FlyTo)o).Drone == ActiveDrone)
                                    BtnFlyTo.Image = Resources.left_free2_off;
                            }));
                            CurrentFlyTo.Destroyed += (o, v) => BeginInvoke((MethodInvoker)(() =>
                            {
                                if (((FlyTo)o).Drone == ActiveDrone)
                                    BtnFlyTo.Image = Resources.left_free2_none;
                            }));
                            SetButtonStates();
                        }
                    }
                    else if (e.Button == MouseButtons.Right)
                        CurrentFlyTo?.Dispose();
                    CurrentFlyTo = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                FlyToClicked = false;
                return;
            }

			// check if the mouse up happend over our button
			if (polyicon.Rectangle.Contains(e.Location))
			{
				if (e.Button == MouseButtons.Left)
				{
					polyicon.IsSelected = true;
					polygongridmode = true;
					
				}
				else if (e.Button == MouseButtons.Right)
				{
					polyicon.IsSelected = false;
					clearPolygonToolStripMenuItem_Click(this, null);
				}
				return;
			}

			if (!FullControl) {
                return;
            }

			if (e.Button == MouseButtons.Right) // ignore right clicks
			{
				return;
			}

			if (isMouseDown) // mouse down on some other object and dragged to here.
			{

				if (CurrentMidline is GMapPlusMarker)
				{
					int pnt2 = 0;
					var midline = CurrentMidline.Tag as midline;
					var idx = drawnPolygon.Points.IndexOf(midline.now);

					if (polygongridmode && midline.now != null)
					{
						drawnPolygon.Points.Insert(idx + 1,
						new PointLatLng(CurrentMidline.Position.Lat, CurrentMidline.Position.Lng));

						RedrawPolygonSurvey(drawnPolygon.Points.Select(a => new PointLatLngAlt(a)).ToList());
					}
						
					return;
				}

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

					foreach (var marker in ActiveDrone.GetOverlay().Markers)
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
					if (CurrentRectMarker != null)
					{
						// cant add WP in existing rect
					}
					else
					{
                        if (!FullControl) return;
                        AddWPToMap(CurrentMarker.Position.Lat, CurrentMarker.Position.Lng, 0);
					}
				}
				else
				{
					if (groupmarkers.Count > 0)
					{
						Dictionary<string, PointLatLng> dest = new Dictionary<string, PointLatLng>();

						foreach (var markerid in groupmarkers)
						{
							for (int a = 0; a < ActiveDrone.GetOverlay().Markers.Count; a++)
							{
								var marker = ActiveDrone.GetOverlay().Markers[a];

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

						Map.SelectedArea = RectLatLng.Empty;
						groupmarkers.Clear();
						// redraw to remove selection
						WriteKMLV2();

						CurrentRectMarker = null;
					}

					if (CurrentRectMarker != null && CurrentRectMarker.InnerMarker != null)
					{
						if (CurrentRectMarker.InnerMarker.Tag.ToString().Contains("grid")
							&& !CurrentRectMarker.InnerMarker.Tag.ToString().Contains("_cus_"))
						{
							try
							{
								drawnPolygon.Points[
									int.Parse(CurrentRectMarker.InnerMarker.Tag.ToString().Replace("grid", "")) - 1] =
									new PointLatLng(MouseDownEnd.Lat, MouseDownEnd.Lng);
								Map.UpdatePolygonLocalPosition(drawnPolygon);
								Map.Invalidate();
							}
							catch (Exception ex)
							{
								log.Error(ex);
							}
						}
						else
						{
							DragCallback(CurrentRectMarker.InnerMarker.Tag.ToString(), CurrentMarker.Position.Lat,
								CurrentMarker.Position.Lng, -2);
						}

						CurrentRectMarker = null;
					}
				}
			}

			isMouseDraging = false;
		}

		private void MainMap_MouseDown(object sender, MouseEventArgs e)
		{
			MouseDownStart = Map.FromLocalToLatLng(e.X, e.Y);

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

				if (CurrentMarker.IsVisible)
				{
					CurrentMarker.Position = Map.FromLocalToLatLng(e.X, e.Y);
				}
			}
		}

        private object dragLock = new object();
        // move current marker with left holding
        private void MainMap_MouseMove(object sender, MouseEventArgs e)
		{
			PointLatLng point = Map.FromLocalToLatLng(e.X, e.Y);

            if (FlyToClicked)
            {
                CurrentFlyTo?.SetDestination(point);
                return;
            }

			if (MouseDownStart == point)
				return;

			//  Console.WriteLine("MainMap MM " + point);

			CurrentMarker.Position = point;

			if (!isMouseDown)
			{
				// update mouse pos display
				// SetMouseDisplay(point.Lat, point.Lng, 0);
			}

			//draging
			if (e.Button == MouseButtons.Left && isMouseDown)
			{
				isMouseDraging = true;
				if (CurrentRallyPt != null)
				{
					PointLatLng pnew = Map.FromLocalToLatLng(e.X, e.Y);
					CurrentRallyPt.Position = pnew;
					// Re-organize the poly grid
					//gridAcceptbutton_Click(sender , e);

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
						for (int a = 0; a < ActiveDrone.GetOverlay().Markers.Count; a++)
						{
							var marker = ActiveDrone.GetOverlay().Markers[a];

							if (marker.Tag != null && marker.Tag.ToString() == markerid.ToString())
							{
								var temp = new PointLatLng(marker.Position.Lat, marker.Position.Lng);
								temp.Offset(latdif, -lngdif);
								marker.Position = temp;
							}
						}
					}
				}
				else if (CurrentRectMarker != null) // left click pan
				{
					try
					{
						// check if this is a grid point
						if (CurrentRectMarker.InnerMarker.Tag.ToString().Contains("grid")
							&& !CurrentRectMarker.InnerMarker.Tag.ToString().Contains("_cus_"))
						{
							drawnPolygon.Points[
								int.Parse(CurrentRectMarker.InnerMarker.Tag.ToString().Replace("grid", "")) - 1] =
								new PointLatLng(point.Lat, point.Lng);
							Map.UpdatePolygonLocalPosition(drawnPolygon);
							Map.Invalidate();
						}
					}
					catch (Exception ex)
					{
						log.Error(ex);
					}

					PointLatLng pnew = Map.FromLocalToLatLng(e.X, e.Y);

					// adjust polyline point while we drag
					try
					{
						if (CurrentGMapMarker != null && CurrentGMapMarker.Tag is int)
						{
							int? pIndex = (int?)CurrentRectMarker.Tag;
							if (pIndex.HasValue)
							{
								if (pIndex < wpPolygon.Points.Count)
								{
									wpPolygon.Points[pIndex.Value] = pnew;
									lock (dragLock)
									{
										Map.UpdatePolygonLocalPosition(wpPolygon);
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
					if (CurrentMarker.IsVisible)
					{
						CurrentMarker.Position = pnew;
					}
					CurrentRectMarker.Position = pnew;

					if (CurrentRectMarker.InnerMarker != null)
					{
						CurrentRectMarker.InnerMarker.Position = pnew;
					}
				}
				else if (CurrentGMapMarker != null)
				{
					PointLatLng pnew = Map.FromLocalToLatLng(e.X, e.Y);

					CurrentGMapMarker.Position = pnew;
				}
				else if (ModifierKeys == Keys.Control)
				{
					// draw selection box
					double latdif = MouseDownStart.Lat - point.Lat;
					double lngdif = MouseDownStart.Lng - point.Lng;

					Map.SelectedArea = new RectLatLng(Math.Max(MouseDownStart.Lat, point.Lat),
						Math.Min(MouseDownStart.Lng, point.Lng), Math.Abs(lngdif), Math.Abs(latdif));
				}
				else // left click pan
				{
					double latdif = MouseDownStart.Lat - point.Lat;
					double lngdif = MouseDownStart.Lng - point.Lng;

					try
					{
						lock (dragLock)
						{
							Map.Position = new PointLatLng(CenterMarker.Position.Lat + latdif,
								CenterMarker.Position.Lng + lngdif);
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
					DGVWayPoints.CurrentCell = DGVWayPoints[0, answer - 1];
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
                switch (item)
                {
                    case GMapRectMarker rc:
                        rc.Pen.Color = Color.Transparent;
                        Map.Invalidate(false);
                        if (ActiveDrone.GetOverlay().Markers.Contains(item) &&
                            item.Tag != null && rc.InnerMarker != null &&
                            int.TryParse(rc.InnerMarker.Tag.ToString(), out int answer))
                        {
                            try
                            {
                                DGVWayPoints.CurrentCell = DGVWayPoints[0, answer - 1];
                                item.ToolTipText = "Alt: " + DGVWayPoints[colAltitude.Index, answer - 1].Value;
                                item.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex);
                            }
                        }
                        CurrentRectMarker = rc;
                        break;
					case GMapPlusMarker ps:
						CurrentMidline = ps;
						break;
                    case GMapMarkerRallyPt rpt:
                        CurrentRallyPt = rpt;
                        break;
                    default:    // GMapMarker
                        CurrentGMapMarker = item;
                        break;
                }
			}
		}

		private void MainMap_OnMarkerLeave(GMapMarker item)
		{
			if (!isMouseDown)
			{
                switch (item)
                {
                    case GMapRectMarker rc:
                        CurrentRectMarker = null;
                        rc.ResetColor();
                        Map.Invalidate(false);
                        break;
                    case GMapMarkerRallyPt rpt:
                        CurrentRallyPt = null;
                        break;
                    default:
                        CurrentGMapMarker = null;
                        break;
                }
			}
		}

		private void But_ZoomIn_Click(object sender, EventArgs e)
		{
			if (Map.Zoom > 0)
			{
				try
				{
					Map.Zoom += 1;
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
				//textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
				CenterMarker.Position = Map.Position;
			}
		}

		private void But_ZoomOut_Click(object sender, EventArgs e)
		{
			if (Map.Zoom > 0)
			{
				try
				{
					Map.Zoom -= 1;
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
				//textBoxZoomCurrent.Text = MainMap.Zoom.ToString();
				CenterMarker.Position = Map.Position;
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

				GMarkerGoogle m = new GMarkerGoogle(point, new Bitmap(Resources.icon_fish_32))
				{
					ToolTipMode = MarkerTooltipMode.Never,
					ToolTipText = "grid" + tag,
					Tag = "grid" + tag
				};

				//MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
				GMapRectMarker mBorders = new GMapRectMarker(point) { InnerMarker = m };

				Overlays.DrawnPolygons.Markers.Add(m);
				Overlays.DrawnPolygons.Markers.Add(mBorders);
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
				if (e.ColumnIndex == colDelete.Index && (e.RowIndex + 0) < DGVWayPoints.RowCount) // delete
				{
					quickadd = true;
					// mono fix
					DGVWayPoints.CurrentCell = null;
					DGVWayPoints.Rows.RemoveAt(e.RowIndex);
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
				DGVWayPoints.CurrentCell = DGVWayPoints[1, selectedRow];
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
			if (selectedRow > DGVWayPoints.RowCount)
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
			if (DGVWayPoints.Columns[colLatitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][4])) // Lat
			{
				cell = DGVWayPoints.Rows[selectedRow].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				cell.Value = lat.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (DGVWayPoints.Columns[colLongitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][5])) // Lng
			{
				cell = DGVWayPoints.Rows[selectedRow].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
				cell.Value = lng.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (alt != -1 && alt != -2 &&
				DGVWayPoints.Columns[colAltitude.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6])) // Alt
			{
				cell = DGVWayPoints.Rows[selectedRow].Cells[colAltitude.Index] as DataGridViewTextBoxCell;

                if (!double.TryParse(TxtHomeAltitude.Text, out double homealt))
				{
					string alttext = "10";
					if (DialogResult.Cancel == InputBox.Show(Strings.MsgHomeAltitudeRequired, alttext, ref alttext))
						return;
					TxtHomeAltitude.Text = alttext;
				}
                if (!int.TryParse(TxtAltitudeValue.Text, out int altval))
                {
                    MessageBox.Show(Strings.MsgDefaultAltitudeInvalid);
                    return;
                }

                if (altval == 0)
				{
					string defalt = "10";
					if (DialogResult.Cancel == InputBox.Show(Strings.MsgDefaultAltitudeRequired, defalt, ref defalt))
						return;
					TxtAltitudeValue.Text = defalt;
				}

				cell.Value = TxtAltitudeValue.Text;

                if (float.TryParse(cell.Value.ToString(), out float ans))
                {
                    int altint = (int)ans;
                    if (alt != 0) // use passed in value;
                        cell.Value = alt.ToString();
                    if (altint == 0) // default
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
			if (DGVWayPoints.Columns[colParam1.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][1])) // Delay
			{
				cell = DGVWayPoints.Rows[selectedRow].Cells[colParam1.Index] as DataGridViewTextBoxCell;
				cell.Value = p1;
				cell.DataGridView.EndEdit();
			}

            WriteKMLV2();

            DGVWayPoints.EndEdit();
		}

		public void AddWPToMap(double lat, double lng, int alt)
		{

			if (polygongridmode)
			{
				addPolygonPointToolStripMenuItem_Click(null, null);
				return;
			}

			// check home point setup.
			if (IsHomeEmpty())
			{
				MessageBox.Show(Strings.MsgSetHomeFirst);
				return;
			}

			// creating a WP
			selectedRow = DGVWayPoints.Rows.Add();

			DGVWayPoints.Rows[selectedRow].Cells[colCommand.Index].Value = MAV_CMD.WAYPOINT.ToString();
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
						DGVWayPoints.Columns[i].HeaderText = cmdParamNames[command][i - 1];
				else
					for (int i = 1; i <= 7; i++)
						DGVWayPoints.Columns[i].HeaderText = "setme";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		public List<WayPoint> GetCommandList()
		{
			List<WayPoint> commands = new List<WayPoint>();

			for (int a = 0; a < DGVWayPoints.Rows.Count - 0; a++)
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
                DataGridViewCell getC(DataGridViewBand col) => DGVWayPoints.Rows[i].Cells[col.Index];
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

			var mission = ActiveDrone.GetMission();
            mission.Home = GetHomeWP();
            mission.Items = GetCommandList();

            mission.DrawMission();

            var xwps = mission.ExpandedWaypoints;
            var p1 = xwps.FirstOrDefault();
            var wps = xwps.Skip(1).Where(p => p != null);
            if (wps.Count() > 1)
            {
                double d = 0.0; 
                foreach (var p in wps)
                {
                    d += Map.MapProvider.Projection.GetDistance(p, p1);
                    p1 = p;
                }
                //DroneInfoPanel.UpdateEstmatedTime(d);
            }

            Map.HoldInvalidation = true;

			mission.Overlay.ForceUpdate();

			if (!(OnlineDrones.Contains(ActiveDrone) && IsMapFocusing)
                && mission.Waypoints.Count <= 1)
			{
				RectLatLng? rect = Map.GetRectOfAllMarkers(mission.Overlay.Id);
				if (rect.HasValue)
				{
					Map.Position = rect.Value.LocationMiddle;
				}

				//DroneInfoPanel.ResetEstimatedTime();

				// myMap_OnMapZoomChanged();
			}


			Map.Refresh();
		}

		private void Commands_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (quickadd)
				return;
			try
			{
				selectedRow = e.RowIndex;
				string option = DGVWayPoints[colCommand.Index, selectedRow].EditedFormattedValue.ToString();
				string cmd;
				try
				{
					if (DGVWayPoints[colCommand.Index, selectedRow].Value != null)
						cmd = DGVWayPoints[colCommand.Index, selectedRow].Value.ToString();
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
			for (int i = 0; i < DGVWayPoints.ColumnCount; i++)
			{
				DataGridViewCell tcell = DGVWayPoints.Rows[e.RowIndex].Cells[i];
				if (tcell.GetType() == typeof(DataGridViewTextBoxCell))
				{
					if (tcell.Value == null)
						tcell.Value = "0";
				}
			}

			DataGridViewComboBoxCell cell = DGVWayPoints.Rows[e.RowIndex].Cells[colCommand.Index] as DataGridViewComboBoxCell;
			if (cell.Value == null)
			{
				cell.Value = "WAYPOINT";
				cell.DropDownWidth = 200;
				DGVWayPoints.Rows[e.RowIndex].Cells[colDelete.Index].Value = "X";
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
				DGVWayPoints.CurrentCell = DGVWayPoints.Rows[e.RowIndex].Cells[0];

				if (DGVWayPoints.Rows.Count > 1)
				{
					if (DGVWayPoints.Rows[e.RowIndex - 1].Cells[colCommand.Index].Value.ToString() == "WAYPOINT")
					{
						DGVWayPoints.Rows[e.RowIndex].Selected = true; // highlight row
					}
					else
					{
						DGVWayPoints.CurrentCell = DGVWayPoints[1, e.RowIndex - 1];
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
			int cols = DGVWayPoints.Columns.Count;
			for (int a = 1; a < cols; a++)
			{
				DataGridViewTextBoxCell cell;
				cell = DGVWayPoints.Rows[selectedRow].Cells[a] as DataGridViewTextBoxCell;

				if (DGVWayPoints.Columns[a].HeaderText.Equals("") && cell != null && cell.Value == null)
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
				log.Info("cmd rows " + (DGVWayPoints.Rows.Count + 1)); // + home

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
                if (passdata == null)
                {
                    log.Info("Done");
                    WPsToScreen(cmds);
                }
                else
                {
                    var drone = passdata as MavDrone;
                    log.Info("WPs loaded from " + drone.Name + " on connect");
                    drone.Status.Mission = cmds;
                }
            }
            else if (passdata != null)
            {
                // supress exception on connection
                // decrement counter for calculating
                if (connectingDrones)
                    Interlocked.Decrement(ref droneConnectingCounter);
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
						log.Error(exx.ToString());
					}
                    finally
                    {
                        if (connectingDrones)
                            Interlocked.Decrement(ref droneConnectingCounter);
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
			DGVWayPoints.CurrentCell = null;

			while (DGVWayPoints.Rows.Count > 0 && !append)
				DGVWayPoints.Rows.Clear();

			if (cmds.Count == 0)
			{
				quickadd = false;
				return;
			}

			DGVWayPoints.SuspendLayout();
			DGVWayPoints.Enabled = false;

			int i = DGVWayPoints.Rows.Count - 1;
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
				if (i + 1 >= DGVWayPoints.Rows.Count)
				{
					selectedRow = DGVWayPoints.Rows.Add();
				}
				//if (i == 0 && temp.alt == 0) // skip 0 home
				//  continue;
				DataGridViewTextBoxCell cell;
				DataGridViewComboBoxCell cellcmd;
				cellcmd = DGVWayPoints.Rows[i].Cells[colCommand.Index] as DataGridViewComboBoxCell;
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

				cell = DGVWayPoints.Rows[i].Cells[colAltitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Altitude;
				cell = DGVWayPoints.Rows[i].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Latitude;
				cell = DGVWayPoints.Rows[i].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Longitude;

				cell = DGVWayPoints.Rows[i].Cells[colParam1.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param1;
				cell = DGVWayPoints.Rows[i].Cells[colParam2.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param2;
				cell = DGVWayPoints.Rows[i].Cells[colParam3.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param3;
				cell = DGVWayPoints.Rows[i].Cells[colParam4.Index] as DataGridViewTextBoxCell;
				cell.Value = temp.Param4;

				// convert to utm
				// convertFromGeographic(temp.lat, temp.lng);
			}

			DGVWayPoints.Enabled = true;
			DGVWayPoints.ResumeLayout();

			// We don't have parameter panel.
			// setWPParams();

			try
			{
				DataGridViewTextBoxCell cellhome;
				cellhome = DGVWayPoints.Rows[0].Cells[colLatitude.Index] as DataGridViewTextBoxCell;
				if (cellhome.Value != null)
				{
					if (cellhome.Value.ToString() != TxtHomeLatitude.Text && cellhome.Value.ToString() != "0")
					{
						DialogResult dr = connectingDrones || !FullControl ? DialogResult.Yes :
                            MessageBox.Show(Strings.MsgResetHomeCoordinate,
							    Strings.MsgResetHomeCoordinateTitle, MessageBoxButtons.YesNo);

						if (dr == DialogResult.Yes)
						{
							TxtHomeLatitude.Text = (double.Parse(cellhome.Value.ToString())).ToString();
							cellhome = DGVWayPoints.Rows[0].Cells[colLongitude.Index] as DataGridViewTextBoxCell;
							TxtHomeLongitude.Text = (double.Parse(cellhome.Value.ToString())).ToString();
							cellhome = DGVWayPoints.Rows[0].Cells[colAltitude.Index] as DataGridViewTextBoxCell;
							TxtHomeAltitude.Text = (double.Parse(cellhome.Value.ToString())).ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());

			} // if there is no valid home

			if (DGVWayPoints.RowCount > 0)
			{
				log.Info("remove home from list");
				DGVWayPoints.Rows.Remove(DGVWayPoints.Rows[0]); // remove home row
			}

			quickadd = false;

			WriteKMLV2();

			Map.ZoomAndCenterMarkers("objects");

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

			Overlays.RallyPoints.Markers.Clear();

			int count = int.Parse(ActiveDrone.Status.Params["RALLY_TOTAL"].ToString());

			for (int a = 0; a < (count); a++)
			{
				try
				{
					PointLatLngAlt plla = ActiveDrone.GetRallyPoint(a, ref count);
					Overlays.RallyPoints.Markers.Add(new GMapMarkerRallyPt(plla)
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

			Map.UpdateMarkerLocalPosition(Overlays.RallyPoints.Markers[0]);

			Map.Invalidate();
		}

        internal void ClearMission() => clearMissionToolStripMenuItem_Click(null, null);

        private void clearMissionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			quickadd = true;

			// mono fix
			DGVWayPoints.CurrentCell = null;

			DGVWayPoints.Rows.Clear();

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

			if (ActiveDrone.Status.State != MAV_STATE.ACTIVE)
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

			Overlays.Commons.Markers.Clear();

			AddPolygonMarker("Click & GO", gotohere.Longitude,
								  gotohere.Latitude, (int)gotohere.Altitude, Color.Blue, Overlays.Commons);
		}

        public int AddCommand(MAV_CMD cmd, double p1, double p2, double p3, double p4, double x, double y,
			double z, object tag = null)
		{
			selectedRow = DGVWayPoints.Rows.Add();

			FillCommand(this.selectedRow, cmd, p1, p2, p3, p4, x, y, z, tag);

			WriteKMLV2();

			return selectedRow;
		}

		public void InsertCommand(int rowIndex, MAV_CMD cmd, double p1, double p2, double p3, double p4, double x, double y,
			double z, object tag = null)
		{
			if (DGVWayPoints.Rows.Count <= rowIndex)
			{
				AddCommand(cmd, p1, p2, p3, p4, x, y, z, tag);
				return;
			}

			DGVWayPoints.Rows.Insert(rowIndex);

			this.selectedRow = rowIndex;

			FillCommand(this.selectedRow, cmd, p1, p2, p3, p4, x, y, z, tag);

			WriteKMLV2();
		}

		private void FillCommand(int rowIndex, MAV_CMD cmd, double p1, double p2, double p3, double p4, double x,
			double y, double z, object tag = null)
		{
			DGVWayPoints.Rows[rowIndex].Cells[colCommand.Index].Value = cmd.ToString();
			DGVWayPoints.Rows[rowIndex].Cells[colTagData.Index].Tag = tag;
			DGVWayPoints.Rows[rowIndex].Cells[colTagData.Index].Value = tag;

			ChangeColumnHeader(cmd.ToString());

			if (cmd == MAV_CMD.WAYPOINT)
			{
				// add delay if supplied
				DGVWayPoints.Rows[rowIndex].Cells[colParam1.Index].Value = p1;

				SetFromMap(y, x, (int)z, Math.Round(p1, 1));
			}
			else if (cmd == MAV_CMD.LOITER_UNLIM)
			{
				SetFromMap(y, x, (int)z);
			}
			else
			{
				DGVWayPoints.Rows[rowIndex].Cells[colParam1.Index].Value = p1;
				DGVWayPoints.Rows[rowIndex].Cells[colParam2.Index].Value = p2;
				DGVWayPoints.Rows[rowIndex].Cells[colParam3.Index].Value = p3;
				DGVWayPoints.Rows[rowIndex].Cells[colParam4.Index].Value = p4;
				DGVWayPoints.Rows[rowIndex].Cells[colLatitude.Index].Value = y;
				DGVWayPoints.Rows[rowIndex].Cells[colLongitude.Index].Value = x;
				DGVWayPoints.Rows[rowIndex].Cells[colAltitude.Index].Value = z;
			}
		}

        #region Button click event handlers
        private int droneConnectingCounter;
        private bool connectingDrones => droneConnectingCounter != 0;
		private void BUT_Connect_Click(object sender, EventArgs e)
		{
            // clicked event fires after state change
            if (!TSBtnConnect.Checked)
            {
                if (OnlineDrones.Count > 0)
                {
                    OnlineDrones.ForEach(DroneDisconnect);
                    OnlineDrones.Clear();
                    DroneInfoPanel.Clear();
                    Overlays.Drones.Markers.Clear();
                    Overlays.Routes.Routes.Clear();
                }
                return;
            }
            var dsettings = ConfigData.GetTypeList<DroneSetting>().Where(d => d.Checked);
            if (!dsettings.Any())
            {
                BUT_Configure_Click("Vehicle", null);
                TSBtnConnect.Checked = false;
                return;
            }
            droneConnectingCounter = 0;
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
                    if (drone?.IsOpen == true)
                    {
                        Interlocked.Increment(ref droneConnectingCounter);
                        drone.FlightModeChanged += UpdateDroneMode;
                        drone.StateChangedEvent += NotifyDroneState;
                        DroneInfoPanel.AddDrone(drone);
                        OnlineDrones.Add(drone);
                        var marker = new GMapDroneMarker(drone) { ToolTipMode = MarkerTooltipMode.OnMouseOver };
                        marker.ToolTip = new GMapToolTip(marker);
                        Overlays.Drones.Markers.Add(marker);
                        var route = new GMapRoute("DroneRoute_" + drone.GetHashCode())
                        {    Stroke = new Pen(Color.FromArgb(drone.GetHashCode() | unchecked((int)0xFF000000)), 2) };
                        DroneRoutes.Add(drone, route);
                        Overlays.Routes.Routes.Add(route);
                        var readWPWorker = new ProgressDialogV2
                        {
                            StartPosition = FormStartPosition.CenterScreen,
                            HintImage = Resources.icon_info,
                            Text = Strings.MsgDialogDownloadWps,
                        };

                        readWPWorker.DoWork += (o, ev, d) =>
                        {
                            LoadWPsFromDrone(o, ev, drone);
                            var mission = DroneMission.GetMission(drone);
                            mission.DrawMission(false);
                        };
                        readWPWorker.RunBackgroundOperationAsync();
                        readWPWorker.Dispose();
                        if (MIRDCMode) break;
                    }
                }
                catch (Exception exception)
                {
                    log.Debug(exception);
                }
            }
            if (OnlineDrones.Count > 0)
            {
                Interlocked.Increment(ref droneConnectingCounter);
                WPsToScreen(ActiveDrone.Status.Mission);
            }
            else
            {
                TSBtnConnect.Checked = false;
            }
        }

		private void BUT_Arm_Click(object sender, EventArgs e)
		{
			if (!ActiveDrone.IsOpen)
			{
				log.Error("basestream have opened");
				return;
			}

            if (ActiveDrone.Status.IsArmed &&
                MessageBox.Show("Already armed, disarm?", "Warning",
                        MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            // arm the MAV
            try
            {
                ActiveDrone.SetMode("GUIDED");
				log.InfoFormat("mav armed: {0}", ActiveDrone.Status.IsArmed);
				if (!ActiveDrone.DoArm(!ActiveDrone.Status.IsArmed))
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
                if (float.TryParse(_dialog.Value, out float alt))
                {
                    ActiveDrone.SetMode("GUIDED");
                    ActiveDrone.TakeOff(alt);
                    AltitudeControl.UpdateDroneTargetAltitude(ActiveDrone, alt);
                }
                else
                    MessageBox.Show(Strings.DroneSetting_MsgValueInvalid, Strings.DialogTitleError);
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

			if (DGVWayPoints.Rows.Count > 0)
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
			for (int a = 0; a < DGVWayPoints.Rows.Count - 0; a++)
			{
				for (int b = 0; b < DGVWayPoints.ColumnCount - 0; b++)
				{
                    if (b >= 1 && b <= 7)
                    {
                        if (!double.TryParse(DGVWayPoints[b, a].Value.ToString(), out double answer))
                        {
                            MessageBox.Show(Strings.MsgMissionError);
                            return;
                        }
                    }

                    // if (TXT_altwarn.Text == "")
                    // 	TXT_altwarn.Text = (0).ToString();

                    if (DGVWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString().Contains("UNKNOWN"))
						continue;

					ushort cmd = (ushort)Enum.Parse(typeof(MAV_CMD),
									DGVWayPoints.Rows[a].Cells[colCommand.Index].Value.ToString(),
                                    false);

					if (cmd < (ushort)MAV_CMD.LAST &&
						double.Parse(DGVWayPoints[colAltitude.Index, a].Value.ToString()) < WARN_ALT)
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

			Map.Focus();
		}

		private void setHomeHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TxtHomeAltitude.Text = "0";
			TxtHomeLatitude.Text = MouseDownStart.Lat.ToString();
			TxtHomeLongitude.Text = MouseDownStart.Lng.ToString();
		}

		private void BUT_Land_Click(object sender, EventArgs e)
		{
			if (ActiveDrone.IsOpen)
			{
                ActiveDrone.SetMode(ActiveDrone.Status.FlightModeType.LandMode);
			}
		}

		private void BUT_Configure_Click(object sender, EventArgs e)
		{
            ConfigureForm.InitPage = sender as string;
            ConfigureForm config = new ConfigureForm();
			config.ShowDialog(this);
		}

		private bool isTagging = false;

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

        private void But_MapFocus_Click(object sender, EventArgs e)
		{
			IsMapFocusing = !IsMapFocusing;
		}

		private void addPolygonPointToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
			if (polygongridmode == false)
            {
				polygongridmode = true;
				return;
			}
			List<PointLatLng> polygonPoints = new List<PointLatLng>();
			if (Overlays.DrawnPolygons.Polygons.Count == 0)
			{
				drawnPolygon.Points.Clear();
				Overlays.DrawnPolygons.Polygons.Add(drawnPolygon);
			}

			drawnPolygon.Fill = Brushes.Transparent;

			// remove full loop is exists
			if (drawnPolygon.Points.Count > 1 &&
				drawnPolygon.Points[0] == drawnPolygon.Points[drawnPolygon.Points.Count - 1])
				drawnPolygon.Points.RemoveAt(drawnPolygon.Points.Count - 1); // unmake a full loop

			drawnPolygon.Points.Add(new PointLatLng(MouseDownStart.Lat, MouseDownStart.Lng));

			RedrawPolygonSurvey(drawnPolygon.Points.Select(a => new PointLatLngAlt(a)).ToList());

			// AddPolygonMarkerGrid(drawnPolygon.Points.Count.ToString(), MouseDownStart.Lng, MouseDownStart.Lat, 0);
			// Map.UpdatePolygonLocalPosition(drawnPolygon);

			Map.Invalidate();
		}

		public class midline
        {
			public PointLatLngAlt now { get; set; }
			public PointLatLngAlt next { get; set; }
		}

		public void RedrawPolygonSurvey(List<PointLatLngAlt> list)
		{
			drawnPolygon.Points.Clear();
			Overlays.DrawnPolygons.Clear();

			int tag = 0;
			list.ForEach(x =>
			{
				tag++;
				drawnPolygon.Points.Add(x);
				AddPolygonMarkerGrid(tag.ToString(), x.Lng, x.Lat, 0);
			});

			Overlays.DrawnPolygons.Polygons.Add(drawnPolygon);
			Map.UpdatePolygonLocalPosition(drawnPolygon);

			var ps = drawnPolygon.Points.ToArray();

			if (ps.Length < 2) return; // line include at least two points
			for (int i = 1; i < ps.Length; i++)
			{
				var now = ps[i-1];
				var next = ps[i];
				var mid = new PointLatLngAlt((now.Lat + next.Lat) / 2, (now.Lng + next.Lng) / 2, 0);
				var pnt = new GMapPlusMarker(mid);
				pnt.Tag = new midline() { now = now, next = next };
				Overlays.DrawnPolygons.Markers.Add(pnt);
			}

			Map.Invalidate();
		}

		private void clearPolygonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			polygongridmode = false;
			if (drawnPolygon == null)
				return;
			drawnPolygon.Points.Clear();
			Overlays.DrawnPolygons.Markers.Clear();
			Map.Invalidate();

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

					Overlays.DrawnPolygons.Markers.Clear();
					Overlays.DrawnPolygons.Polygons.Clear();
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

					Overlays.DrawnPolygons.Polygons.Add(drawnPolygon);

					Map.UpdatePolygonLocalPosition(drawnPolygon);

					Map.Invalidate();

					Map.ZoomAndCenterMarkers(Overlays.DrawnPolygons.Id);
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

			if (Overlays.Geofence.Markers.Count == 0)
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
            if (!Overlays.Geofence.Markers[0].Position.InsideOf(plll))
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
				ActiveDrone.SetFencePoint(a, new PointLatLngAlt(Overlays.Geofence.Markers[0].Position), pointcount);
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
				Overlays.Geofence.Polygons.Clear();
				geofencePolygon.Points.Clear();

				// add polygon
				geofencePolygon.Points.AddRange(drawnPolygon.Points.ToArray());

				drawnPolygon.Points.Clear();

				Overlays.Geofence.Polygons.Add(geofencePolygon);

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

				Map.UpdatePolygonLocalPosition(geofencePolygon);
				Map.UpdateMarkerLocalPosition(Overlays.Geofence.Markers[0]);

				Map.Invalidate();
			}
			catch (Exception ex)
			{
				MessageBox.Show(Strings.MsgFailedToSendNewFencePoints + ex, Strings.DialogTitleError);
			}
		}

		private void setReturnLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Overlays.Geofence.Markers.Clear();
			Overlays.Geofence.Markers.Add(new GMarkerGoogle(new PointLatLng(MouseDownStart.Lat, MouseDownStart.Lng),
				GMarkerGoogleType.red)
			{ ToolTipMode = MarkerTooltipMode.OnMouseOver, ToolTipText = "GeoFence Return" });

			Map.Invalidate();
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

					Overlays.DrawnPolygons.Markers.Clear();
					Overlays.DrawnPolygons.Polygons.Clear();
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
								Overlays.Geofence.Markers.Clear();
								Overlays.Geofence.Markers.Add(
									new GMarkerGoogle(new PointLatLng(double.Parse(items[0]), double.Parse(items[1])),
										GMarkerGoogleType.red)
									{
										ToolTipMode = MarkerTooltipMode.OnMouseOver,
										ToolTipText = "GeoFence Return"
									});
								Map.UpdateMarkerLocalPosition(Overlays.Geofence.Markers[0]);
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

					Overlays.DrawnPolygons.Polygons.Add(drawnPolygon);

					Map.UpdatePolygonLocalPosition(drawnPolygon);

					Map.Invalidate();
				}
			}
		}

		private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Overlays.Geofence.Markers.Count == 0)
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

						sw.WriteLine(Overlays.Geofence.Markers[0].Position.Lat + " " +
									 Overlays.Geofence.Markers[0].Position.Lng);
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
			Overlays.DrawnPolygons.Polygons.Clear();
			Overlays.DrawnPolygons.Markers.Clear();
			Overlays.Geofence.Polygons.Clear();
			geofencePolygon.Points.Clear();
		}

		#endregion

        private void UpdateMapItems()
        {
            try
            {
                // altitude control panel
                AltitudeControlPanel.SetSource(ActiveDrone);

                int maxPoints = ConfigData.GetIntOption("MaxRouteEntries");
                if (maxPoints == 0) maxPoints = 200;
                OnlineDrones.ForEach(d =>
                {
                    GMapRoute r = DroneRoutes[d];
                    r.Points.Add(d.Status.Location);

                    int pts = r.Points.Count;
                    if (pts > maxPoints)
                        r.Points.RemoveRange(0, pts - maxPoints);
                });

                BeginInvoke((MethodInvoker)delegate
                {
                    // overlay marks has to be touched for updating
                    try
                    {
                        if (Overlays.Drones.Markers.Count > 0)
                            Overlays.Drones.Markers[0] = Overlays.Drones.Markers[0];
                    }
                    catch { }
                    foreach (var r in Overlays.Routes.Routes)
                    {
                        try
                        {
                            Map.UpdateRouteLocalPosition(r);
                        }
                        catch { }
                    }
                });

                //autopan
                if (Autopan)
                {
                    if (route.Points[route.Points.Count - 1].Lat != 0 && (mapupdate.AddSeconds(3) < DateTime.Now))
                    {
                        UpdateMapPosition(ActiveDrone.Status.Location);
                        mapupdate = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public HUDPanel HUD = new HUDPanel
        {
            GroundLineColor = Color.Green,
            IndicatorColor = Color.Red,
            ScaleLineColor = Color.White
        };

        private void DroneInfoPanel_DroneClosed(object sender, EventArgs e)
        {
            var drone = (sender as DroneInfo)?.Drone;
            DroneDisconnect(drone);
            OnlineDrones.Remove(drone);
            if (OnlineDrones.Count == 0)
                TSBtnConnect.Checked = false;
        }

        private void DroneDisconnect(MavDrone drone)
        {
            try
            {
                if (drone != null)
                {
                    DroneMission.RemoveMission(drone);
                    FlyTo.DropFlight(drone);
                    Overlays.Drones.Markers.Remove(Overlays.Drones.Markers.Single(
                        x => (x as GMapDroneMarker)?.Drone == drone));
                    var r = DroneRoutes[drone];
                    Overlays.Routes.Routes.Remove(r);
                    DroneRoutes.Remove(drone);
                    if (drone == ActiveDrone)
                    {
                        AltitudeControl.Remove(drone);
                        AltitudeControlPanel.ClearSource();
                        ActiveDrone = null;
                        SetButtonStates();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception on drone close: " + ex);
            }
        }

        private void DroneInfoPanel_ActiveDroneChanged(object sender, EventArgs e)
        {
            if (!connectingDrones) try
            {
                var mission = ActiveDrone.Status.Mission;
                mission.Clear();
                mission.Add(GetHomeWP());
                mission.AddRange(GetCommandList());
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
            if ((ActiveDrone = (sender as DroneInfo)?.Drone) != null)
            {
                DGVWayPoints.Rows.Clear();
                UpdateCMDParams();
                UpdateDroneMode(ActiveDrone, ActiveDrone.Status.FlightMode);
                WPsToScreen(ActiveDrone.Status.Mission);
                // add marker again to ensure drone icon is topmost
                var marker = Overlays.Drones.Markers.SingleOrDefault(
                    x => (x as GMapDroneMarker).Drone == ActiveDrone);
                if (marker != null)
                {
                    Overlays.Drones.Markers.Remove(marker);
                    Overlays.Drones.Markers.Add(marker);
                }
                var overlay = ActiveDrone.GetOverlay();
                Map.Overlays.Remove(overlay);
                Map.Overlays.Add(overlay);
                AltitudeControlPanel.SetSource(ActiveDrone);
            }
            FlyToClicked = false;
            SetButtonStates();
        }

        public void UpdateDroneMode(object obj, uint mode)
        {
            void updateText(MavDrone drone)
            {
                string modename = drone.Status.FlightModeType[mode];
                LblMode.Text = drone.Status.IsArmed ? modename : "DISARMED";
                int modeidx = ComBoxModeSwitch.Items.IndexOf(modename);
                if (ComBoxModeSwitch.SelectedIndex != modeidx)
                    ComBoxModeSwitch.SelectedIndex = modeidx;
                UpdateWarningIcon();
            }
            if (obj is MavDrone d && d == ActiveDrone)
            {
                if (InvokeRequired)
                    BeginInvoke((MethodInvoker)delegate { updateText(d); });
                else
                    updateText(d);
            }
        }

        private bool comboChangedByUser;
        private void ComBoxModeSwitch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboChangedByUser = true;
        }

        private void ComBoxModeSwitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool commit = comboChangedByUser;
            comboChangedByUser = false;
            if (!ActiveDrone.IsOpen || !(sender is ComboBox modeSwitch))
                return;

            try
            {
                string modename = modeSwitch.SelectedItem.ToString();
                if (commit && !ActiveDrone.IsMode(modename))
                    ActiveDrone.SetMode(modename);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void NotifyDroneState(object obj, MAV_STATE state)
        {
            if (obj as MavDrone == ActiveDrone)
            {
                /*void notifyAction() {  }
                if (InvokeRequired)
                    BeginInvoke((MethodInvoker)delegate { notifyAction(); });
                else
                    notifyAction();*/
            }
        }

        private void RotationInfoPanel_SizeChanged(object sender, EventArgs e)
        {
            if (sender is FlowLayoutPanel panel)
                Map.MsgWindowOffset = new PointF(0, -panel.Height);
        }

        #region MIRDC features
        private bool fullControl;
        public bool FullControl
        {
            get => fullControl;
            set
            {
                fullControl = value;
				flyToolStrip.Visible = value;
                LblMode.Visible = value;
                LblModeDesc.Visible = value;
                BtnMapFocus.Left = BtnZoomIn.Left = BtnZoomOut.Left = BtnBreakAction.Left
                    = AltitudeControlPanel.Left = value ? 184 : 12;
                Map.ContextMenuStrip = value ? cmMap : null;
                DroneMission.SetVisible(value);

                int newHeight = value ? 120 : 0;
                int diff = newHeight - SplitContainer.Panel2.Height;
                SplitContainer.Panel2MinSize = newHeight;
                SplitContainer.SplitterDistance -= diff;
                SplitContainer.IsSplitterFixed = !value;
                SplitContainer.FixedPanel = value ? FixedPanel.None : FixedPanel.Panel2;

                SetButtonStates();
            }
        }
        public static bool MIRDCMode => !Instance?.FullControl ?? true;

        private MyTSButton BtnFullCtrl = new MyTSButton
        {
            AutoSize = false,
            CheckedText = Strings.BtnSimplifiedControlText,
            CheckOnClick = true,
            Height = 80,
            Text = Strings.BtnFullControlText,
            Width = 80,
            Visible = false
        };
        public bool FlyToClicked
        {
            get => BtnFlyTo.Checked;
            set => BtnFlyTo.Checked = value;
        }
        private MyTSButton BtnFlyTo = new MyTSButton
        {
            AutoSize = false,
            CheckedText = Strings.BtnChooseDestinationText,
            CheckOnClick = true,
            Height = 80,
            Image = Resources.left_free2_none,
            Text = Strings.BtnFlyToText,
            Width = 80,
            TextImageRelation = TextImageRelation.ImageAboveText,
            DisplayStyle = ToolStripItemDisplayStyle.Image,
            ImageScaling = ToolStripItemImageScaling.None,
        };
        private MyTSButton BtnTrack = new MyTSButton
        {
            AutoSize = false,
            Height = 80,
            Width = 80,
            CheckedForeColor = Color.Green,
            Enabled = false,
            Image = Resources.left_relative2_disabled,
            TextImageRelation = TextImageRelation.ImageAboveText,
            DisplayStyle = ToolStripItemDisplayStyle.Image,
            ImageScaling = ToolStripItemImageScaling.None,
        };

        public PointLatLng HomeLocation = PointLatLng.Empty;

        private FlyTo CurrentFlyTo;

        private bool IsActiveDroneReady()
        {
            if (ActiveDrone == null || ActiveDrone == DummyDrone
                || !ActiveDrone.Status.IsArmed
                || ActiveDrone.Status.State != MAV_STATE.ACTIVE &&
                ActiveDrone.Status.Firmware == Firmwares.ArduCopter2)
            {
                MessageBox.Show(Strings.MsgActiveUnavailableOrNotReady);
                return false;
            }
            return true;
        }

        private void SetupMIRDC()
        {
            Icon = Resources.logo;
            FullControl = false;

            BtnFlyTo.Click += BtnFlyTo_Clicked;
            TSMainPanel.Items.Add(BtnFlyTo);
            BtnTrack.Click += BtnTrack_Clicked;
            TSMainPanel.Items.Add(BtnTrack);
            SetButtonStates();
            BackgroundTimer += BaseLocationInitialized;
        }

        private void BaseLocationInitialized(object sender, EventArgs e)
        {
            if (BaseLocation.Ready)
            {
                BackgroundTimer -= BaseLocationInitialized;
                BeginInvoke((MethodInvoker)(() =>
                {
                    BtnTrack.Enabled = true;
                    BtnTrack.Image = Resources.left_relative2_none;
                    SetButtonStates();
                }));
            }
        }

        private void BtnFlyTo_Clicked(object sender, EventArgs e)
        {
            if (FlyToClicked)
            {
                if (!IsActiveDroneReady())
                {
                    FlyToClicked = false;
                    return;
                }
                CurrentFlyTo?.Dispose();
                CurrentFlyTo = new FlyTo(ActiveDrone);
            }
            else
            {
                CurrentFlyTo?.Dispose();
                CurrentFlyTo = null;
                FlyToClicked = false;
            }
        }

        private void AltitudeControlPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (ActiveDrone != null && ActiveDrone != DummyDrone
                    && ActiveDrone.Status.IsArmed)
            {
                if (e.Button == MouseButtons.Left)
                    AltitudeControl.TargetAltitudes[ActiveDrone]
                        = AltitudeControlPanel.PointValue;
                else if (e.Button == MouseButtons.Right)
                    AltitudeControl.Remove(ActiveDrone);
            }
        }

        private void Planner_KeyUp(object sender, KeyEventArgs e)
        {
            bool keyOnMap = sender == Map;
            if (keyOnMap && FlyToClicked && e.KeyCode == Keys.Escape)
            {
                try
                {
                    CurrentFlyTo?.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                CurrentFlyTo = null;
                FlyToClicked = false;
            }
            else if (e.Modifiers == Keys.Alt &&
                (e.KeyCode == Keys.Pause || e.KeyCode == Keys.Insert))
            {
                FullControl = !FullControl;
            }
        }

        private void BtnTrack_Clicked(object sender, EventArgs e)
        {
            if (!IsActiveDroneReady()) return;
            if (!BaseLocation.Initialized && DialogResult.Yes !=
                MessageBox.Show(Strings.MsgLocationApiNotReady,
                    Strings.MsgLocationApiNotReadyTitle, MessageBoxButtons.YesNo))
                return;
            using (var form = new TrackerDialog(OnlineDrones, ActiveDrone))
            {
                if (form.Sources.Count < 1)
                {
                    MessageBox.Show(Strings.MsgNoAvailableTrackSource);
                    return;
                }
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var f = new FlyTo(ActiveDrone, true);
                    if (f.StartTracking(form.Target, form.Distance, form.BearingAngle))
                    {
                        SetButtonStates();
                        f.TrackUpdate += (o, r) => BeginInvoke((MethodInvoker)(() =>
                        {
                            if (f.Drone == ActiveDrone)
                                BtnTrack.Image = r ? Resources.left_relative2_off : Resources.left_relative2_on;
                        }));
                        f.Destroyed += (o, v) => BeginInvoke((MethodInvoker)SetButtonStates);
                    }
                }
            }
        }

        private void BtnBreakAction_Click(object sender, EventArgs e)
        {
            if (IsActiveDroneReady())
            {
                var fly = FlyTo.GetFlyToFrom(ActiveDrone);
                if (fly != null)
                {
                    fly.Stop();
                    SetButtonStates();
                }
                else if (ActiveDrone.Status.Firmware == Firmwares.ArduPlane)
                    ActiveDrone.SetGuidedModeWP(new WayPoint
                    {
                        Id = (ushort)MAV_CMD.WAYPOINT,
                        Altitude = AltitudeControl.GetWPAltitude(ActiveDrone,
                                    ActiveDrone.Status.Altitude),
                        Latitude = ActiveDrone.Status.Latitude,
                        Longitude = ActiveDrone.Status.Longitude,
                        Frame = MAV_FRAME.GLOBAL_RELATIVE_ALT
                    });
                else
                    ActiveDrone.SetMode(ActiveDrone.Status.FlightModeType.PauseMode);
                AltitudeControl.Remove(ActiveDrone);
                AltitudeControlPanel.Targeting = false;
            }
        }

        private void BtnMapFocus_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender != BtnMapFocus || e.Button != MouseButtons.Right) return;
            var mpos = BtnMapFocus.PointToClient(Cursor.Position);
            if (mpos.X >= 0 && mpos.X < BtnMapFocus.Width &&
                mpos.Y >= 0 && mpos.Y < BtnMapFocus.Height)
            {
                IsMapFocusing = false;
                PointLatLng loc = PointLatLng.Empty;
                if (!MIRDCMode && OnlineDrones.Any(d => d.Name.StartsWith("ship", StringComparison.InvariantCultureIgnoreCase)))
                {
                    MavDrone drone = null;
                    OnlineDrones.Any(d => d.IsOpen &&
                        d.Name.StartsWith("ship", StringComparison.InvariantCultureIgnoreCase) &&
                        (drone = d) != null);
                    if (drone != null)
                        loc = drone.Status.Location;
                }
                if (loc == PointLatLng.Empty && BaseLocation.Ready)
                    loc = BaseLocation.Location;
                if (loc != PointLatLng.Empty)
                    try { Map.Position = loc; } catch { }
            }
        }

        private void SetButtonStates()
        {
            bool isValidShip(GMapMarker m)
            {
                if (m is GMapDroneMarker d)
                    return d.IsShip && d.Drone != ActiveDrone;
                return false;
            }

            BtnFlyTo.Image = Resources.left_free2_none;
            if (BaseLocation.Ready || Overlays.Drones.Markers.Any(isValidShip))
            {
                BtnTrack.Image = Resources.left_relative2_none;
                BtnTrack.Text = Strings.BtnTrackText;
                BtnTrack.Enabled = true;
            }
            else
            {
                BtnTrack.Image = Resources.left_relative2_disabled;
                BtnTrack.Text = Strings.BnTrackText_GPSNotReady;
                BtnTrack.Enabled = false;
            }
            var flyto = FlyTo.GetFlyToFrom(ActiveDrone);
            if (flyto != null)
            {
                if (flyto.TrackMode)
                {
                    BtnTrack.Enabled = true;
                    BtnTrack.Image = flyto.Reached ? Resources.left_relative2_off
                        : Resources.left_relative2_on;
                    BtnFlyTo.Image = Resources.left_free2_none;
                }
                else
                {
                    BtnFlyTo.Image = flyto.Reached ? Resources.left_free2_off
                        : Resources.left_free2_on;
                    if (BtnTrack.Enabled)
                        BtnTrack.Image = Resources.left_relative2_none;
                }
            }
            UpdateWarningIcon();
        }

        private void UpdateWarningIcon()
        {
            IconModeWarning.Visible = false;
            if (ActiveDrone?.IsOpen == true)
            {
                if (ActiveDrone.Status.Firmware == Firmwares.ArduPlane && ActiveDrone.IsMode("QLAND") ||
                    ActiveDrone.Status.Firmware == Firmwares.ArduCopter2 && ActiveDrone.IsMode("LAND"))
                {
                    IconModeWarning.Visible = true;
                    IconModeWarning.BackgroundImage = Resources.VTOL_landing;
                    IconModeWarning.Text = "";
                }
                else if (ActiveDrone.Status.IsArmed && !ActiveDrone.IsMode("GUIDED")
                    && (ActiveDrone.Status.Firmware != Firmwares.ArduCopter2 || !ActiveDrone.IsMode("BRAKE")))
                {
                    IconModeWarning.Visible = true;
                    IconModeWarning.BackgroundImage = Resources.rc_controlling;
                }
            }
        }

        #endregion

		private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var t = (ToolStripMenuItem)sender;
			Grid grid = new Grid(currentDrone, HomeLocation);
			if (drawnPolygon.Points.Count < 2)
            {
				DialogResult dialogResult = MessageBox.Show("No polygon defined. Load a file?", "Load File", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					// grid.LoadGrid();
					return;
				}
				else if (dialogResult == DialogResult.No)
					MessageBox.Show("Please define a polygon.", "Error");
			} else
            {
				switch (t.Name)
				{
					case "surveyToolStripMenuItem":
						DialogConfigSimplegrid dcs = new DialogConfigSimplegrid(grid, HomeLocation);
						dcs.ShowDialog();
						break;
					case "corridorScanToolStripMenuItem":
						// AddGridWPsToMap(grid, Grid.ScanMode.Corridor);
						break;
				};
			}
		}

	

		private void DGVWayPoints_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine("DataGridError: " + e.Exception);
            Console.WriteLine("StackTrace: " + e.Exception.StackTrace);
            e.Cancel = true;
        }
	}
}
