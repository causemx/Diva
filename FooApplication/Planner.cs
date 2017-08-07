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
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooApplication
{
	public partial class Planner : Form
	{
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
		}

		private void myMap_Load(object sender, EventArgs e)
		{
			myMap.MapProvider = BingSatelliteMapProvider.Instance;
			myMap.Position = new PointLatLng(24.773518, 121.0443385);
			myMap.MinZoom = 0;
			myMap.MaxZoom = 24;
			myMap.Zoom = 15;
		}

		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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

		private bool isMouseDown;
		private bool isMouseDraging;
		private bool isMouseClickOffMenu;
		bool polygongridmode = false;
		private PointLatLng MouseDownStart;
		internal PointLatLng MouseDownEnd;

		// polygon
		internal GMapPolygon drawnPolygon;
		internal GMapPolygon wpPolygon;

		private List<int> groupmarkers = new List<int>();
		private Object thisLock = new Object();

		private bool quickadd;


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
					// Commands.CurrentCell = Commands[0, answer - 1];
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

			Console.WriteLine("MainMap MD");

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
			Console.WriteLine("mouseup");
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
						// TODO: add data to gridview. 
						// AddWPToMap(currentMarker.Position.Lat, currentMarker.Position.Lng, 0);

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
						// writeKML();


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
							// callMeDrag(CurentRectMarker.InnerMarker.Tag.ToString(), currentMarker.Position.Lat,
							//	currentMarker.Position.Lng, -2);
						}
						currentRectMarker = null;
					}
				}
			}
			addpolygonmarker("H", MouseDownEnd.Lng, MouseDownEnd.Lat, 0, Color.Green);

			Console.WriteLine(MouseDownEnd.Lat + "," + MouseDownEnd.Lng);

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
							/**
							Commands.CurrentCell = Commands[0, answer - 1];
							item.ToolTipText = "Alt: " + Commands[Alt.Index, answer - 1].Value;
							item.ToolTipMode = MarkerTooltipMode.OnMouseOver; */
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
					//CurrentGMapMarker = item;
				}
				if (item is GMapMarker)
				{
					//CurrentGMapMarker = item;
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

	}
}
