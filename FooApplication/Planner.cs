using FooApplication.Controls;
using FooApplication.Mavlink;
using FooApplication.Utilities;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FooApplication
{
	public partial class Planner : Form
	{

		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly double MY_LAT = 24.773518;
		private static readonly double MY_LNG = 121.0443385;
		private static readonly double WARN_ALT = 2D;
		private GMapOverlay top;
		private GMapOverlay routesOverlay;
		private GMapOverlay polygonsOverlay;
		private GMapOverlay airportsOverlay;
		private GMapOverlay poiOverlay = new GMapOverlay("POI");
		private GMapOverlay drawnPolygonsOverlay;
		private GMapOverlay objectsOverlay;
		private GMapOverlay kmlPolygonsOverlay;
		private GMapOverlay geofenceOverlay;
		private GMapOverlay rallypointOverlay;

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
		bool polygongridmode = false;
		private PointLatLng MouseDownStart;
		internal PointLatLng MouseDownEnd;

		// polygon
		internal GMapPolygon drawnPolygon;
		internal GMapPolygon wpPolygon;

		private bool quickadd = false;
		private bool sethome = false;
		private int selectedrow = 0;

		private Dictionary<string, string[]> cmdParamNames = new Dictionary<string, string[]>();
		private List<List<Locationwp>> history = new List<List<Locationwp>>();
		private List<int> groupmarkers = new List<int>();
		private Object thisLock = new Object();
		public List<PointLatLngAlt> pointlist = new List<PointLatLngAlt>(); // used to calc distance
		public List<PointLatLngAlt> fullpointlist = new List<PointLatLngAlt>();
		private MavlinkInterface port = new MavlinkInterface();

		public enum altmode
		{
			Relative = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT,
			Absolute = MAVLink.MAV_FRAME.GLOBAL,
			Terrain = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT
		}


		public Planner()
		{
			InitializeComponent();

			quickadd = false;

			// setup map events
			myMap.OnPositionChanged += MainMap_OnCurrentPositionChanged;
			myMap.OnMarkerClick += MainMap_OnMarkerClick;
			myMap.MouseMove += MainMap_MouseMove;
			myMap.MouseDown += MainMap_MouseDown;
			myMap.MouseUp += MainMap_MouseUp;
			myMap.OnMarkerEnter += MainMap_OnMarkerEnter;
			myMap.OnMarkerLeave += MainMap_OnMarkerLeave;

			myMap.MapScaleInfoEnabled = false;
			myMap.DisableFocusOnMouseEnter = true;
			myMap.RoutesEnabled = true;
			myMap.ForceDoubleBuffer = false;



			// draw this layer first
			kmlPolygonsOverlay = new GMapOverlay("kmlpolygons");
			myMap.Overlays.Add(kmlPolygonsOverlay);

			rallypointOverlay = new GMapOverlay("rallypoints");
			myMap.Overlays.Add(rallypointOverlay);

			routesOverlay = new GMapOverlay("routes");
			myMap.Overlays.Add(routesOverlay);

			polygonsOverlay = new GMapOverlay("polygons");
			myMap.Overlays.Add(polygonsOverlay);

			airportsOverlay = new GMapOverlay("airports");
			myMap.Overlays.Add(airportsOverlay);

			objectsOverlay = new GMapOverlay("objects");
			myMap.Overlays.Add(objectsOverlay);

			drawnPolygonsOverlay = new GMapOverlay("drawnpolygons");
			myMap.Overlays.Add(drawnPolygonsOverlay);

			myMap.Overlays.Add(poiOverlay);

			top = new GMapOverlay("top");
			// myMap.Overlays.Add(top);

			objectsOverlay.Markers.Clear();

			// set current marker
			currentMarker = new GMarkerGoogle(myMap.Position, GMarkerGoogleType.red);
			//top.Markers.Add(currentMarker);

			// map center
			center = new GMarkerGoogle(myMap.Position, GMarkerGoogleType.none);
			top.Markers.Add(center);

			myMap.Zoom = 3;

			updateCMDParams();

			//set home
			try
			{
				
				myMap.Position = new PointLatLng(MY_LAT, MY_LNG);
				myMap.Zoom = 15;
				
			}
			catch (Exception ex)
			{
				log.Error(ex);
			}
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

			Command.DataSource = cmds;
		}

		private Dictionary<string, string[]> readCMDXML()
		{
			Dictionary<string, string[]> cmd = new Dictionary<string, string[]>();

			// do lang stuff here
			
			string file = Settings.GetRunningDirectory() + "mavcmd.xml";
		

			if (!File.Exists(file))
			{
				MessageBox.Show("Missing mavcmd.xml file");
				return cmd;
			}
			

			using (XmlReader reader = XmlReader.Create(file))
			{
				reader.Read();
				reader.ReadStartElement("CMD");
				// read part of firmware - APM, because mavcmd.xml very large...
				reader.ReadToFollowing("APM");
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

		private void myMap_Load(object sender, EventArgs e)
		{
			myMap.MapProvider = BingSatelliteMapProvider.Instance;
			myMap.Position = new PointLatLng(24.773518, 121.0443385);
			myMap.MinZoom = 0;
			myMap.MaxZoom = 24;
			myMap.Zoom = 15;
		}

	
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
					Commands.CurrentCell = Commands[0, answer - 1];
				}
			}
			catch (Exception ex)
			{
				log.Error(ex);
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
						for (int a = 0; a < objectsOverlay.Markers.Count; a++)
						{
							var marker = objectsOverlay.Markers[a];

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
						if (currentRectMarker.InnerMarker.Tag.ToString().Contains("grid"))
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


		private void MainMap_MouseUp(object sender, MouseEventArgs e)
		{
			
			if (isMouseClickOffMenu)
			{
				isMouseClickOffMenu = false;
				return;
			}

			// check if the mouse up happend over our button
			/**
			if (polyIcon.Rectangle.Contains(e.Location))
			{
				polyIcon.IsSelected = !polyIcon.IsSelected;

				if (e.Button == MouseButtons.Right)
				{
					polyIcon.IsSelected = false;
					// clearPolygonToolStripMenuItem_Click(this, null);

					// contextMenuStrip1.Visible = false;

					return;
				}

				if (polyIcon.IsSelected)
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
				/**if (CurrentPOIMarker != null)
				{
					POI.POIMove(CurrentPOIMarker);
					CurrentPOIMarker = null;
				}**/

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

					foreach (var marker in objectsOverlay.Markers)
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
							for (int a = 0; a < objectsOverlay.Markers.Count; a++)
							{
								var marker = objectsOverlay.Markers[a];

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
							// callMeDrag(item.Key, value.Lat, value.Lng, -1);
							quickadd = false;
						}

						myMap.SelectedArea = RectLatLng.Empty;
						groupmarkers.Clear();
						// redraw to remove selection
						writeKML();


						currentRectMarker = null;
					}

					if (currentRectMarker != null && currentRectMarker.InnerMarker != null)
					{
						if (currentRectMarker.InnerMarker.Tag.ToString().Contains("grid"))
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
							Commands.CurrentCell = Commands[0, answer - 1];
							item.ToolTipText = "Alt: " + Commands[Alt.Index, answer - 1].Value;
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

		void MainMap_OnMarkerLeave(GMapMarker item)
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
				m.ToolTipText = "Alt: " + alt.ToString("0");
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
					if (color.HasValue)
					{
						mBorders.Color = color.Value;
					}
				}

				objectsOverlay.Markers.Add(m);
				//objectsOverlay.Markers.Add(mBorders);
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
				GMarkerGoogle m = new GMarkerGoogle(point, GMarkerGoogleType.red);
				m.ToolTipMode = MarkerTooltipMode.Never;
				m.ToolTipText = "grid" + tag;
				m.Tag = "grid" + tag;

				//MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
				GMapMarkerRect mBorders = new GMapMarkerRect(point);
				{
					mBorders.InnerMarker = m;
				}

				drawnPolygonsOverlay.Markers.Add(m);
				drawnPolygonsOverlay.Markers.Add(mBorders);
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
				if (e.ColumnIndex == Delete.Index && (e.RowIndex + 0) < Commands.RowCount) // delete
				{
					quickadd = true;
					// mono fix
					Commands.CurrentCell = null;
					Commands.Rows.RemoveAt(e.RowIndex);
					quickadd = false;
					writeKML();
				}			
				// setgradanddistandaz();
			}
			catch (Exception)
			{
				MessageBox.Show("Row error");
			}
		}

		public void callMeDrag(string pointno, double lat, double lng, int alt)
		{
			if (pointno == "")
			{
				return;
			}
			// TODO: tracker home for future.
			/**
			if (pointno == "Tracker Home")
			{
				MainV2.comPort.MAV.cs.TrackerLocation = new PointLatLngAlt(lat, lng, alt, "");
				return;
			}*/

			try
			{
				selectedrow = int.Parse(pointno) - 1;
				Commands.CurrentCell = Commands[1, selectedrow];
				// depending on the dragged item, selectedrow can be reset 
				selectedrow = int.Parse(pointno) - 1;
			}
			catch
			{
				return;
			}

			setfromMap(lat, lng, alt);
		}

		public void AddWPToMap(double lat, double lng, int alt)
		{
			
			if (sethome)
			{
				sethome = false;
				callMeDrag("H", lat, lng, alt);
				return;
			}
			// creating a WP

			selectedrow = Commands.Rows.Add();

			
			Commands.Rows[selectedrow].Cells[Command.Index].Value = MAVLink.MAV_CMD.WAYPOINT.ToString();
			ChangeColumnHeader(MAVLink.MAV_CMD.WAYPOINT.ToString());
			

			setfromMap(lat, lng, alt);
		}

		private void ChangeColumnHeader(string command)
		{
			try
			{
				if (cmdParamNames.ContainsKey(command))
					for (int i = 1; i <= 7; i++)
						Commands.Columns[i].HeaderText = cmdParamNames[command][i - 1];
				else
					for (int i = 1; i <= 7; i++)
						Commands.Columns[i].HeaderText = "setme";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private List<Locationwp> GetCommandList()
		{
			List<Locationwp> commands = new List<Locationwp>();

			for (int a = 0; a < Commands.Rows.Count - 0; a++)
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
				if (Commands.Rows[a].Cells[Command.Index].Value.ToString().Contains("UNKNOWN"))
				{
					temp.id = (ushort)Commands.Rows[a].Cells[Command.Index].Tag;
				}
				else
				{
					temp.id =
						(ushort)
								Enum.Parse(typeof(MAVLink.MAV_CMD),
									Commands.Rows[a].Cells[Command.Index].Value.ToString(),
									false);
				}

			// TODO: I don't know where currentstate come from..
	
				temp.alt =
					(float)
						(double.Parse(Commands.Rows[a].Cells[Alt.Index].Value.ToString()));
			
				temp.lat = (double.Parse(Commands.Rows[a].Cells[Lat.Index].Value.ToString()));
				temp.lng = (double.Parse(Commands.Rows[a].Cells[Lon.Index].Value.ToString()));
				temp.p1 = float.Parse(Commands.Rows[a].Cells[Param1.Index].Value.ToString());
				temp.p2 = (float)(double.Parse(Commands.Rows[a].Cells[Param2.Index].Value.ToString()));
				temp.p3 = (float)(double.Parse(Commands.Rows[a].Cells[Param3.Index].Value.ToString()));
				temp.p4 = (float)(double.Parse(Commands.Rows[a].Cells[Param4.Index].Value.ToString()));

				temp.Tag = Commands.Rows[a].Cells[TagData.Index].Value;

				return temp;
			}
			catch (Exception ex)
			{
				throw new FormatException("Invalid number on row " + (a + 1).ToString(), ex);
			}
		}


		public void setfromMap(double lat, double lng, int alt, double p1 = 0)
		{
			if (selectedrow > Commands.RowCount)
			{
				MessageBox.Show("Invalid coord, How did you do this?");
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
				MessageBox.Show("A invalid entry has been detected\n" + ex.Message);
			}

			// remove more than 20 revisions
			if (history.Count > 20)
			{
				history.RemoveRange(0, history.Count - 20);
			}

			DataGridViewTextBoxCell cell;
			if (Commands.Columns[Lat.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][4] /*"Lat"*/))
			{
				cell = Commands.Rows[selectedrow].Cells[Lat.Index] as DataGridViewTextBoxCell;
				cell.Value = lat.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (Commands.Columns[Lon.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][5] /*"Long"*/))
			{
				cell = Commands.Rows[selectedrow].Cells[Lon.Index] as DataGridViewTextBoxCell;
				cell.Value = lng.ToString("0.0000000");
				cell.DataGridView.EndEdit();
			}
			if (alt != -1 && alt != -2 &&
				Commands.Columns[Alt.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6] /*"Alt"*/))
			{
				cell = Commands.Rows[selectedrow].Cells[Alt.Index] as DataGridViewTextBoxCell;

				{
					double result;
					bool pass = double.TryParse(TXT_homealt.Text, out result);

					if (pass == false)
					{
						MessageBox.Show("You must have a home altitude");
						string homealt = "10";
						if (DialogResult.Cancel == InputBox.Show("Home Alt", "Home Altitude", ref homealt))
							return;
						TXT_homealt.Text = homealt;
					}
					int results1;
					if (!int.TryParse(TXT_DefaultAlt.Text, out results1))
					{
						MessageBox.Show("Your default alt is not valid");
						return;
					}

					if (results1 == 0)
					{
						string defalt = "10";
						if (DialogResult.Cancel == InputBox.Show("Default Alt", "Default Altitude", ref defalt))
							return;
						TXT_DefaultAlt.Text = defalt;
					}
				}

				cell.Value = TXT_DefaultAlt.Text;

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
					MessageBox.Show("Invalid Home or wp Alt");
					cell.Style.BackColor = Color.Red;
				}
			}

			// convert to utm
			// convertFromGeographic(lat, lng);

			// Add more for other params
			if (Commands.Columns[Param1.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][1] /*"Delay"*/))
			{
				cell = Commands.Rows[selectedrow].Cells[Param1.Index] as DataGridViewTextBoxCell;
				cell.Value = p1;
				cell.DataGridView.EndEdit();
			}

			writeKML();
			Commands.EndEdit();
		}


		public void writeKML()
		{
			// quickadd is for when loading wps from eeprom or file, to prevent slow, loading times
			if (quickadd)
				return;

			// this is to share the current mission with the data tab
			pointlist = new List<PointLatLngAlt>();

			fullpointlist.Clear();

			Console.WriteLine("write_KML: " + DateTime.Now);
			try
			{
				if (objectsOverlay != null) // hasnt been created yet
				{
					objectsOverlay.Markers.Clear();
				}

				// process and add home to the list
				string home;
				if (TXT_homealt.Text != "" && TXT_homelat.Text != "" && TXT_homelng.Text != "")
				{
					home = string.Format("{0},{1},{2}\r\n", TXT_homelng.Text, TXT_homelat.Text, TXT_DefaultAlt.Text);
					if (objectsOverlay != null) // during startup
					{
						pointlist.Add(new PointLatLngAlt(double.Parse(TXT_homelat.Text), double.Parse(TXT_homelng.Text),
							double.Parse(TXT_homealt.Text), "H"));
						fullpointlist.Add(pointlist[pointlist.Count - 1]);
						addpolygonmarker("H", double.Parse(TXT_homelng.Text), double.Parse(TXT_homelat.Text), 0, null);
					}
				}
				else
				{
					home = "";
					pointlist.Add(null);
					fullpointlist.Add(pointlist[pointlist.Count - 1]);
				}

				// setup for centerpoint calc etc.
				double avglat = 0;
				double avglong = 0;
				double maxlat = -180;
				double maxlong = -180;
				double minlat = 180;
				double minlong = 180;
				double homealt = 0;
				try
				{
					if (!String.IsNullOrEmpty(TXT_homealt.Text))
						homealt = (int)double.Parse(TXT_homealt.Text);
				}
				catch (Exception ex)
				{
					log.Error(ex);
				}
			
				int usable = 0;

				updateRowNumbers();

				long temp = Stopwatch.GetTimestamp();

				string lookat = "";
				for (int a = 0; a < Commands.Rows.Count - 0; a++)
				{
					try
					{
						if (Commands.Rows[a].Cells[Command.Index].Value.ToString().Contains("UNKNOWN"))
							continue;

						ushort command =
							(ushort)
									Enum.Parse(typeof(MAVLink.MAV_CMD),
										Commands.Rows[a].Cells[Command.Index].Value.ToString(), false);
						if (command < (ushort)MAVLink.MAV_CMD.LAST &&
							command != (ushort)MAVLink.MAV_CMD.TAKEOFF && // doesnt have a position
							command != (ushort)MAVLink.MAV_CMD.VTOL_TAKEOFF && // doesnt have a position
							command != (ushort)MAVLink.MAV_CMD.RETURN_TO_LAUNCH &&
							command != (ushort)MAVLink.MAV_CMD.CONTINUE_AND_CHANGE_ALT &&
							command != (ushort)MAVLink.MAV_CMD.GUIDED_ENABLE
							|| command == (ushort)MAVLink.MAV_CMD.DO_SET_ROI)
						{
							string cell2 = Commands.Rows[a].Cells[Alt.Index].Value.ToString(); // alt
							string cell3 = Commands.Rows[a].Cells[Lat.Index].Value.ToString(); // lat
							string cell4 = Commands.Rows[a].Cells[Lon.Index].Value.ToString(); // lng

							// land can be 0,0 or a lat,lng
							if (command == (ushort)MAVLink.MAV_CMD.LAND && cell3 == "0" && cell4 == "0")
								continue;

							if (cell4 == "?" || cell3 == "?")
								continue;

							if (command == (ushort)MAVLink.MAV_CMD.DO_SET_ROI)
							{
								pointlist.Add(new PointLatLngAlt(double.Parse(cell3), double.Parse(cell4),
									double.Parse(cell2) + homealt, "ROI" + (a + 1))
								{ color = Color.Red });
								// do set roi is not a nav command. so we dont route through it
								//fullpointlist.Add(pointlist[pointlist.Count - 1]);
								GMarkerGoogle m =
									new GMarkerGoogle(new PointLatLng(double.Parse(cell3), double.Parse(cell4)),
										GMarkerGoogleType.red);
								m.ToolTipMode = MarkerTooltipMode.Always;
								m.ToolTipText = (a + 1).ToString();
								m.Tag = (a + 1).ToString();

								GMapMarkerRect mBorders = new GMapMarkerRect(m.Position);
								{
									mBorders.InnerMarker = m;
									mBorders.Tag = "Dont draw line";
								}

								// check for clear roi, and hide it
								if (m.Position.Lat != 0 && m.Position.Lng != 0)
								{
									// order matters
									objectsOverlay.Markers.Add(m);
									objectsOverlay.Markers.Add(mBorders);
								}
							}
							else if (command == (ushort)MAVLink.MAV_CMD.LOITER_TIME ||
									 command == (ushort)MAVLink.MAV_CMD.LOITER_TURNS ||
									 command == (ushort)MAVLink.MAV_CMD.LOITER_UNLIM)
							{
								pointlist.Add(new PointLatLngAlt(double.Parse(cell3), double.Parse(cell4),
									double.Parse(cell2) + homealt, (a + 1).ToString())
								{
									color = Color.LightBlue
								});
								fullpointlist.Add(pointlist[pointlist.Count - 1]);
								addpolygonmarker((a + 1).ToString(), double.Parse(cell4), double.Parse(cell3),
									double.Parse(cell2), Color.LightBlue);
							}
							else if (command == (ushort)MAVLink.MAV_CMD.SPLINE_WAYPOINT)
							{
								pointlist.Add(new PointLatLngAlt(double.Parse(cell3), double.Parse(cell4),
									double.Parse(cell2) + homealt, (a + 1).ToString())
								{ Tag2 = "spline" });
								fullpointlist.Add(pointlist[pointlist.Count - 1]);
								addpolygonmarker((a + 1).ToString(), double.Parse(cell4), double.Parse(cell3),
									double.Parse(cell2), Color.Green);
							}
							else
							{
								pointlist.Add(new PointLatLngAlt(double.Parse(cell3), double.Parse(cell4),
									double.Parse(cell2) + homealt, (a + 1).ToString()));
								fullpointlist.Add(pointlist[pointlist.Count - 1]);
								addpolygonmarker((a + 1).ToString(), double.Parse(cell4), double.Parse(cell3),
									double.Parse(cell2), null);
							}

							avglong += double.Parse(Commands.Rows[a].Cells[Lon.Index].Value.ToString());
							avglat += double.Parse(Commands.Rows[a].Cells[Lat.Index].Value.ToString());
							usable++;

							maxlong = Math.Max(double.Parse(Commands.Rows[a].Cells[Lon.Index].Value.ToString()), maxlong);
							maxlat = Math.Max(double.Parse(Commands.Rows[a].Cells[Lat.Index].Value.ToString()), maxlat);
							minlong = Math.Min(double.Parse(Commands.Rows[a].Cells[Lon.Index].Value.ToString()), minlong);
							minlat = Math.Min(double.Parse(Commands.Rows[a].Cells[Lat.Index].Value.ToString()), minlat);

							Debug.WriteLine(temp - Stopwatch.GetTimestamp());
						}
						else if (command == (ushort)MAVLink.MAV_CMD.DO_JUMP) // fix do jumps into the future
						{
							pointlist.Add(null);

							int wpno = int.Parse(Commands.Rows[a].Cells[Param1.Index].Value.ToString());
							int repeat = int.Parse(Commands.Rows[a].Cells[Param2.Index].Value.ToString());

							List<PointLatLngAlt> list = new List<PointLatLngAlt>();

							// cycle through reps
							for (int repno = repeat; repno > 0; repno--)
							{
								// cycle through wps
								for (int no = wpno; no <= a; no++)
								{
									if (pointlist[no] != null)
										list.Add(pointlist[no]);
								}
							}

							fullpointlist.AddRange(list);
						}
						else
						{
							pointlist.Add(null);
						}
					}
					catch (Exception e)
					{
						log.Info("writekml - bad wp data " + e);
					}
				}

				if (usable > 0)
				{
					avglat = avglat / usable;
					avglong = avglong / usable;
					double latdiff = maxlat - minlat;
					double longdiff = maxlong - minlong;
					float range = 4000;

					Locationwp loc1 = new Locationwp();
					loc1.lat = (minlat);
					loc1.lng = (minlong);
					Locationwp loc2 = new Locationwp();
					loc2.lat = (maxlat);
					loc2.lng = (maxlong);

					//double distance = getDistance(loc1, loc2);  // same code as ardupilot
					double distance = 2000;

					if (usable > 1)
					{
						range = (float)(distance * 2);
					}
					else
					{
						range = 4000;
					}

					if (avglong != 0 && usable < 3)
					{
						// no autozoom
						lookat = "<LookAt>     <longitude>" + (minlong + longdiff / 2).ToString(new CultureInfo("en-US")) +
								 "</longitude>     <latitude>" + (minlat + latdiff / 2).ToString(new CultureInfo("en-US")) +
								 "</latitude> <range>" + range + "</range> </LookAt>";
						// MainMap.ZoomAndCenterMarkers("objects");
						//MainMap.Zoom -= 1;
						//MainMap_OnMapZoomChanged();
					}
				}
				else if (home.Length > 5 && usable == 0)
				{
					lookat = "<LookAt>     <longitude>" + TXT_homelng.Text.ToString(new CultureInfo("en-US")) +
							 "</longitude>     <latitude>" + TXT_homelat.Text.ToString(new CultureInfo("en-US")) +
							 "</latitude> <range>4000</range> </LookAt>";

					RectLatLng? rect = myMap.GetRectOfAllMarkers("objects");
					if (rect.HasValue)
					{
						myMap.Position = rect.Value.LocationMiddle;
					}

					//MainMap.Zoom = 17;

					// MainMap_OnMapZoomChanged();
				}

				//RegeneratePolygon();

				RegenerateWPRoute(fullpointlist);

				if (fullpointlist.Count > 0)
				{
					double homedist = 0;

					if (home.Length > 5)
					{
						homedist = myMap.MapProvider.Projection.GetDistance(fullpointlist[fullpointlist.Count - 1],
							fullpointlist[0]);
					}

					double dist = 0;

					for (int a = 1; a < fullpointlist.Count; a++)
					{
						if (fullpointlist[a - 1] == null)
							continue;

						if (fullpointlist[a] == null)
							continue;

						dist += myMap.MapProvider.Projection.GetDistance(fullpointlist[a - 1], fullpointlist[a]);
					}

				}

				// setgradanddistandaz();
			}
			catch (Exception ex)
			{
				log.Info(ex.ToString());
			}
		}


		private void RegenerateWPRoute(List<PointLatLngAlt> fullpointlist)
		{
			route.Clear();
			homeroute.Clear();

			polygonsOverlay.Routes.Clear();

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

				polygonsOverlay.Routes.Add(homeroute);

				route.Stroke = new Pen(Color.Yellow, 4);
				route.Stroke.DashStyle = DashStyle.Custom;
				polygonsOverlay.Routes.Add(route);
			}
		}


		private void updateRowNumbers()
		{
			// number rows 
			this.BeginInvoke((MethodInvoker)delegate
			{
				// thread for updateing row numbers
				for (int a = 0; a < Commands.Rows.Count - 0; a++)
				{
					try
					{
						if (Commands.Rows[a].HeaderCell.Value == null)
						{
							//Commands.Rows[a].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
							Commands.Rows[a].HeaderCell.Value = (a + 1).ToString();
						}
						// skip rows with the correct number
						string rowno = Commands.Rows[a].HeaderCell.Value.ToString();
						if (!rowno.Equals((a + 1).ToString()))
						{
							// this code is where the delay is when deleting.
							Commands.Rows[a].HeaderCell.Value = (a + 1).ToString();
						}
					}
					catch (Exception)
					{
					}
				}
			});
		}


		private void Commands_RowEnter(object sender, DataGridViewCellEventArgs e)
		{
			if (quickadd)
				return;
			try
			{
				selectedrow = e.RowIndex;
				string option = Commands[Command.Index, selectedrow].EditedFormattedValue.ToString();
				string cmd;
				try
				{
					if (Commands[Command.Index, selectedrow].Value != null)
						cmd = Commands[Command.Index, selectedrow].Value.ToString();
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
			for (int i = 0; i < Commands.ColumnCount; i++)
			{
				DataGridViewCell tcell = Commands.Rows[e.RowIndex].Cells[i];
				if (tcell.GetType() == typeof(DataGridViewTextBoxCell))
				{
					if (tcell.Value == null)
						tcell.Value = "0";
				}
			}

			DataGridViewComboBoxCell cell = Commands.Rows[e.RowIndex].Cells[Command.Index] as DataGridViewComboBoxCell;
			if (cell.Value == null)
			{
				cell.Value = "WAYPOINT";
				cell.DropDownWidth = 200;
				Commands.Rows[e.RowIndex].Cells[Delete.Index].Value = "X";
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
				Commands.CurrentCell = Commands.Rows[e.RowIndex].Cells[0];

				if (Commands.Rows.Count > 1)
				{
					if (Commands.Rows[e.RowIndex - 1].Cells[Command.Index].Value.ToString() == "WAYPOINT")
					{
						Commands.Rows[e.RowIndex].Selected = true; // highlight row
					}
					else
					{
						Commands.CurrentCell = Commands[1, e.RowIndex - 1];
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
			int cols = Commands.Columns.Count;
			for (int a = 1; a < cols; a++)
			{
				DataGridViewTextBoxCell cell;
				cell = Commands.Rows[selectedrow].Cells[a] as DataGridViewTextBoxCell;

				if (Commands.Columns[a].HeaderText.Equals("") && cell != null && cell.Value == null)
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

		public void BUT_write_Click(object sender, EventArgs e)
		{
		

			// check home
			Locationwp home = new Locationwp();
			try
			{
				home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
				home.lat = (double.Parse(TXT_homelat.Text));
				home.lng = (double.Parse(TXT_homelng.Text));
				home.alt = (float.Parse(TXT_homealt.Text)); // use saved home
			}
			catch
			{
				MessageBox.Show("Your home location is invalid");
				return;
			}

			// check for invalid grid data
			for (int a = 0; a < Commands.Rows.Count - 0; a++)
			{
				for (int b = 0; b < Commands.ColumnCount - 0; b++)
				{
					double answer;
					if (b >= 1 && b <= 7)
					{
						if (!double.TryParse(Commands[b, a].Value.ToString(), out answer))
						{
							MessageBox.Show("There are errors in your mission");
							return;
						}
					}

					// if (TXT_altwarn.Text == "")
					// 	TXT_altwarn.Text = (0).ToString();

					if (Commands.Rows[a].Cells[Command.Index].Value.ToString().Contains("UNKNOWN"))
						continue;

					ushort cmd =
						(ushort)
								Enum.Parse(typeof(MAVLink.MAV_CMD),
									Commands.Rows[a].Cells[Command.Index].Value.ToString(), false);

					if (cmd < (ushort)MAVLink.MAV_CMD.LAST &&
						double.Parse(Commands[Alt.Index, a].Value.ToString()) < WARN_ALT)
					{
						if (cmd != (ushort)MAVLink.MAV_CMD.TAKEOFF &&
							cmd != (ushort)MAVLink.MAV_CMD.LAND &&
							cmd != (ushort)MAVLink.MAV_CMD.RETURN_TO_LAUNCH)
						{
							MessageBox.Show("Low alt on WP#" + (a + 1) +
												  "\nPlease reduce the alt warning, or increase the altitude");
							return;
						}
					}
				}
			}

			saveWPs();
			

			myMap.Focus();
		}

		

		private void saveWPs()
		{
			try
			{
				
				if (!port.BaseStream.IsOpen)
				{
					throw new Exception("Please connect first!");
				}

				port.giveComport = true;
				int a = 0;

				// define the home point
				Locationwp home = new Locationwp();
				try
				{
					home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
					home.lat = (double.Parse(TXT_homelat.Text));
					home.lng = (double.Parse(TXT_homelng.Text));
					home.alt = (float.Parse(TXT_homealt.Text)); // use saved home
				}
				catch
				{
					throw new Exception("Your home location is invalid");
				}

				// log
				log.Info("wps values " + port.MAV.wps.Values.Count);
				log.Info("cmd rows " + (Commands.Rows.Count + 1)); // + home

				// check for changes / future mod to send just changed wp's
				if (port.MAV.wps.Values.Count == (Commands.Rows.Count + 1))
				{
					Hashtable wpstoupload = new Hashtable();

					a = -1;
					foreach (var item in port.MAV.wps.Values)
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
				// ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set total wps ");

				ushort totalwpcountforupload = (ushort)(Commands.Rows.Count + 1);

				if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
				{
					totalwpcountforupload--;
				}

				port.setWPTotal(totalwpcountforupload); // + home

				// set home location - overwritten/ignored depending on firmware.
				// ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set home");

				// upload from wp0
				a = 0;

				if (port.MAV.apname != MAVLink.MAV_AUTOPILOT.PX4)
				{
					try
					{
						var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
						if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
						{
							if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
							{
								MessageBox.Show("reject by mav1");
								return;
							}
						}
						a++;
					}
					catch (TimeoutException)
					{
						use_int = false;
						// added here to prevent timeout errors
						port.setWPTotal(totalwpcountforupload);
						var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
						if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
						{
							if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
							{
								MessageBox.Show("reject by mav2");
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

					// ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(a * 100 / Commands.Rows.Count,
					//	"Setting WP " + a);

					// make sure we are using the correct frame for these commands
					if (temp.id < (ushort)MAVLink.MAV_CMD.LAST || temp.id == (ushort)MAVLink.MAV_CMD.DO_SET_HOME)
					{
						var mode = altmode.Relative;

						if (mode == altmode.Terrain)
						{
							frame = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT;
						}
						else if (mode == altmode.Absolute)
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
					if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
						uploadwpno--;

					// try send the wp
					MAVLink.MAV_MISSION_RESULT ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);

					// we timed out while uploading wps/ command wasnt replaced/ command wasnt added
					if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
					{
						// resend for partial upload
						port.setWPPartialUpdate((ushort)(uploadwpno), totalwpcountforupload);
						// reupload this point.
						ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);
					}

					if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE)
					{
						Console.WriteLine("Upload failed, please reduce the number of wp's");
						return;
					}
					if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
					{
						
							Console.WriteLine("Upload failed, mission was rejected byt the Mav,\n " +
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
						a = port.getRequestedWPNo() - 1;

						continue;
					}
					if (ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
					{
						Console.WriteLine("Upload wps failed " + Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()) +
										 " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString()));
						return;
					}
				}

				port.setWPACK();
				/**
				// ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(95, "Setting params");

				// m
				port.setParam("WP_RADIUS", float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist);

				// cm's
				port.setParam("WPNAV_RADIUS", float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist * 100.0);

				try
				{
					port.setParam(new[] { "LOITER_RAD", "WP_LOITER_RAD" },
						float.Parse(TXT_loiterrad.Text) / CurrentState.multiplierdist);
				}
				catch
				{
				}

				((ProgressReporterDialogue)sender).UpdateProgressAndStatus(100, "Done.");*/
			}
			catch (Exception ex)
			{
				log.Error(ex);
				port.giveComport = false;
				throw;
			}

			port.giveComport = false;
		}

		private void BUT_Connect_Click(object sender, EventArgs e)
		{
			port.open();
		}

		private void BUT_Arm_Click(object sender, EventArgs e)
		{
			if (!port.BaseStream.IsOpen)
			{
				log.Info("basestream have opened");
				return;
			}

			// arm the MAV
			try
			{
				bool ans = port.doARM(true);
				if (ans == false)
					log.Info("arm failed");
			}
			catch
			{
				log.Info("unknown arm failed");
			}
		}

		private void BUT_Auto_Click(object sender, EventArgs e)
		{
			if (port.BaseStream.IsOpen)
			{
				// flyToHereAltToolStripMenuItem_Click(null, null);

				port.setMode(
					port.sysid,
					port.compid,
					new MAVLink.mavlink_set_mode_t()
					{
						target_system = port.sysid,
						base_mode = (byte)MAVLink.MAV_MODE_FLAG.CUSTOM_MODE_ENABLED,
						custom_mode = (uint)4,
					});
			}
		}


		private void setHomeHereToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TXT_homealt.Text = "0";
			TXT_homelat.Text = MouseDownStart.Lat.ToString();
			TXT_homelng.Text = MouseDownStart.Lng.ToString();
		}


		private void deleteWPToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int no = 0;
			if (currentRectMarker != null)
			{
				if (int.TryParse(currentRectMarker.InnerMarker.Tag.ToString(), out no))
				{
					try
					{
						Commands.Rows.RemoveAt(no - 1); // home is 0
					}
					catch (Exception ex)
					{
						log.Error(ex);
						MessageBox.Show("error selecting wp, please try again.");
					}
				}
				else if (int.TryParse(currentRectMarker.InnerMarker.Tag.ToString().Replace("grid", ""), out no))
				{
					try
					{
						drawnPolygon.Points.RemoveAt(no - 1);
						drawnPolygonsOverlay.Markers.Clear();

						int a = 1;
						foreach (PointLatLng pnt in drawnPolygon.Points)
						{
							addpolygonmarkergrid(a.ToString(), pnt.Lng, pnt.Lat, 0);
							a++;
						}

						myMap.UpdatePolygonLocalPosition(drawnPolygon);

						myMap.Invalidate();
					}
					catch (Exception ex)
					{
						log.Error(ex);
						MessageBox.Show("Remove point Failed. Please try again.");
					}
				}
			}
			else if (currentRallyPt != null)
			{
				rallypointOverlay.Markers.Remove(currentRallyPt);
				myMap.Invalidate(true);

				currentRallyPt = null;
			}
			else if (groupmarkers.Count > 0)
			{
				for (int a = Commands.Rows.Count; a > 0; a--)
				{
					try
					{
						if (groupmarkers.Contains(a))
							Commands.Rows.RemoveAt(a - 1); // home is 0
					}
					catch (Exception ex)
					{
						log.Error(ex);
						MessageBox.Show("error selecting wp, please try again.");
					}
				}

				groupmarkers.Clear();
			}


			if (currentMarker != null)
				currentRectMarker = null;

			writeKML();
		}


	}
}
