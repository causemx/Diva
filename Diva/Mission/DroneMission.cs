using Diva.Utilities;
using Diva.Mavlink;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Diva.Mission
{
    public static class DroneMissionHelper
    {
        public static DroneMission GetMission(this MavDrone drone) => DroneMission.GetMission(drone);
        public static GMapOverlay GetOverlay(this MavDrone drone) => DroneMission.GetMission(drone).Overlay;
    }

    public class DroneMission
    {
        private readonly static Random RNG = new Random();
        private readonly static Dictionary<MavDrone, DroneMission> Overlays = new Dictionary<MavDrone, DroneMission>();
        public static DroneMission GetMission(MavDrone drone)
        {
            if (!Overlays.ContainsKey(drone))
            {
                new DroneMission(drone);
                Planner.GetPlannerInstance().GMapControl?.Overlays.Add(Overlays[drone].Overlay);
            }
            return Overlays[drone];
        }
        public static bool RemoveMission(MavDrone drone)
        {
            bool found = Overlays.ContainsKey(drone);
            if (found)
            {
                try
                {
                    Planner.GetPlannerInstance().GMapControl?.Overlays.Remove(Overlays[drone].Overlay);
                    Overlays.Remove(drone);
                }
                catch (Exception e)
                {
                    Console.WriteLine("RemoveMission exception: " + e);
                }
            }
            return found;
        }
        public static void Reset() => Overlays.Clear();
        public static void SetVisible(bool visible)
        {
            foreach (var p in Overlays)
                p.Value.Overlay.IsVisibile = visible;
        }

        // public Color RoutingPathColor { get; private set; } = Color.FromArgb(RNG.Next(256), RNG.Next(256), RNG.Next(256));
        public Color RoutingPathColor { get; private set; } = Color.WhiteSmoke;
        public double WPRadius { get; set; } = 30.0d;
        public double LoiterRadius { get; set; } = 30.0d;
        public readonly MavDrone Drone;
        public readonly GMapOverlay Overlay;

        private List<PointLatLngAlt> waypoints, expandedWaypoints;
        private readonly List<WayPoint> missionItems = new List<WayPoint>();
        public WayPoint Home = Planner.GetPlannerInstance().GetHomeWP();
        public List<WayPoint> Items {
            get => missionItems;
            set
            {
                missionItems.Clear();
                missionItems.AddRange(value);
            }
        }
        public ReadOnlyCollection<PointLatLngAlt> Waypoints => waypoints.AsReadOnly();
        public ReadOnlyCollection<PointLatLngAlt> ExpandedWaypoints => expandedWaypoints.AsReadOnly();

        private DroneMission(MavDrone drone)
        {
            Drone = drone;
            Overlay = new GMapOverlay("DroneMission_" + drone.Name + this.GetHashCode());
            Overlay.IsVisibile = Planner.GetPlannerInstance().FullControl;
            Overlays.Add(drone, this);
        }

        private void AddPolygonMarker(string tag, double lng, double lat, double alt, double radius)
		{
			try
			{
				PointLatLng point = new PointLatLng(lat, lng);
                DefalutBitmapRender defaultRender = new DefalutBitmapRender();
                GMapTaggedMarker m = new GMapTaggedMarker(point, tag, defaultRender)
                {
                    ToolTipMode = MarkerTooltipMode.OnMouseOver,
                    ToolTipText = "Alt: " + alt.ToString("0"),
                    Tag = tag
                };

                GMapRectMarker mBorders = new GMapRectMarker(point)
                {
                    InnerMarker = m,
                    Tag = tag,
                    wprad = (int)radius
                };

				Overlay.Markers.Add(m);
				Overlay.Markers.Add(mBorders);
			}
			catch (Exception)
			{
			}
		}

        private void AddPolygonMarker(PointLatLngAlt p, double radius)
        {
            try
            {
                DefalutBitmapRender defaultRender = new DefalutBitmapRender();
                GMapTaggedMarker m = new GMapTaggedMarker(p, p.Tag, defaultRender)
                {
                    ToolTipMode = MarkerTooltipMode.OnMouseOver,
                    ToolTipText = "Alt: " + p.Alt.ToString("0")
                };

                GMapRectMarker mBorders = new GMapRectMarker(p)
                {
                    InnerMarker = m,
                    Tag = p.Tag,
                    wprad = (int)radius
                };
                mBorders.Color = p.Color;
                Overlay.Markers.Add(m);
                Overlay.Markers.Add(mBorders);
            }
            catch (Exception)
            {
            }
        }

        public void DrawMission(bool isPlane = true, bool active = true)
        {
            Overlay.Clear();

            PointLatLngAlt home;
            waypoints = new List<PointLatLngAlt>();
            expandedWaypoints = new List<PointLatLngAlt>();
            if (Drone.Status.Mission == null || Drone.Status.Mission.Count == 0)
            {
                if (active)
                {
                    home = Home.ToPointLatLngAlt();
                    AddPolygonMarker("H", home.Lng, home.Lat, home.Alt, 0);
                    waypoints.Add(home);
                }
                return;
            }

			double maxlat = -180;
            double maxlng = -180;
            double minlat = 180;
            double minlng = 180;

            if (!active)
            {
                Home = Drone.Status.Mission[0];
                Home.Tag = "H";
                Items = Drone.Status.Mission.Skip(1).ToList();
            }
            home = Home.ToPointLatLngAlt();
            AddPolygonMarker("H", home.Lng, home.Lat, home.Alt, 0);
            waypoints.Add(home);
            if (waypoints.Count > 0)
                expandedWaypoints.Add(waypoints[waypoints.Count - 1]);

            int i = 0;
            foreach (var item in Items)
            {
                ushort command = item.Id;

                if (command < (ushort)MAVLink.MAV_CMD.LAST &&
                    command != (ushort)MAVLink.MAV_CMD.TAKEOFF && // doesnt have a position
                    command != (ushort)MAVLink.MAV_CMD.VTOL_TAKEOFF && // doesnt have a position
                    command != (ushort)MAVLink.MAV_CMD.RETURN_TO_LAUNCH &&
                    command != (ushort)MAVLink.MAV_CMD.CONTINUE_AND_CHANGE_ALT &&
                    command != (ushort)MAVLink.MAV_CMD.DELAY &&
                    command != (ushort)MAVLink.MAV_CMD.GUIDED_ENABLE
                    || command == (ushort)MAVLink.MAV_CMD.DO_SET_ROI)
                {
                    var lat = item.Latitude;
                    var lng = item.Longitude;
                    var alt = item.Altitude;
                    PointLatLngAlt p;

                    // land can be 0,0 or a lat,lng
                    if (command == (ushort)MAVLink.MAV_CMD.LAND && lat == 0 && lng == 0)
                        continue;

                    if (command == (ushort)MAVLink.MAV_CMD.DO_SET_ROI)
                    {
                        p = new PointLatLngAlt(lat, lng, alt, "ROI" + (i + 1), Color.Red);
                        var m = new GMarkerGoogle(p, GMarkerGoogleType.red)
                        {
                            ToolTipMode = MarkerTooltipMode.Always,
                            ToolTipText = (i + 1).ToString(),
                            Tag = (i + 1).ToString()
                        };

                        GMapRectMarker mBorders = new GMapRectMarker(p)
                        {
                            InnerMarker = m,
                            Tag = "Dont draw line"
                        };

                        // check for clear roi, and hide it
                        if (lat != 0 && lng != 0)
                        {
                            // order matters
                            Overlay.Markers.Add(m);
                            Overlay.Markers.Add(mBorders);
                        }
                    }
                    else if (command == (ushort)MAVLink.MAV_CMD.LOITER_TIME ||
                                command == (ushort)MAVLink.MAV_CMD.LOITER_TURNS ||
                                command == (ushort)MAVLink.MAV_CMD.LOITER_UNLIM)
                    {
                        p = new PointLatLngAlt(lat, lng, alt, (i + 1).ToString(), Color.LightBlue);
                        AddPolygonMarker(p, LoiterRadius);
                    }
                    else if (command == (ushort)MAVLink.MAV_CMD.SPLINE_WAYPOINT)
                    {
                        p = new PointLatLngAlt(lat, lng, alt, (i + 1).ToString(), Color.Green)
                        { Tag2 = "spline" };
                        AddPolygonMarker(p, WPRadius);
                    }
                    else
                    {
                        p = new PointLatLngAlt(lat, lng, alt, (i + 1).ToString());
                        AddPolygonMarker((i + 1).ToString(), lng, lat, alt, WPRadius);
                    }

                    waypoints.Add(p);
                    // do set roi is not a nav command. so we dont route through it
                    if (command != (ushort)MAVLink.MAV_CMD.DO_SET_ROI)
                        expandedWaypoints.Add(p);

                    maxlng = Math.Max(lng, maxlng);
                    maxlat = Math.Max(lat, maxlat);
                    minlng = Math.Min(lng, minlng);
                    minlat = Math.Min(lat, minlat);
                }
                else if (command == (ushort)MAVLink.MAV_CMD.DO_JUMP) // fix do jumps into the future
                {
                    waypoints.Add(null);

                    int wpno = (int)item.Param1;
                    int repeat = (int)item.Param2;

                    List<PointLatLngAlt> list = new List<PointLatLngAlt>();

                    // cycle through reps
                    for (int repno = repeat; repno > 0; repno--)
                    {
                        // cycle through wps
                        for (int no = wpno; no <= i; no++)
                        {
                            if (waypoints[no] != null)
                                list.Add(waypoints[no]);
                        }
                    }

                    expandedWaypoints.AddRange(list);
                }
                else
                {
                    waypoints.Add(null);
                }

                i++;
            }

            // add home - this causeszx the spline to always have a straight finish
            expandedWaypoints.Add(home);
            // DrawCurve(waypoints);
            DrawRoute();
        }

        private void DrawCurve(List<PointLatLngAlt> waypoints)
        {
            var pairs = waypoints.Where((e, i) => i < waypoints.Count - 1)
                .Select((e, i) => new { A = e, B = waypoints[i + 1] });
            var enumPairs = pairs.GetEnumerator();
            while (enumPairs.MoveNext())
            {
                Console.WriteLine(enumPairs.Current.A+","+ enumPairs.Current.B);
            }
        }

		private void DrawRoute()
        {
            PointLatLngAlt home = expandedWaypoints[expandedWaypoints.Count - 1];
            /*List<PointLatLng> wproute = new List<PointLatLng>();
            PointLatLngAlt lastPoint = home;
            PointLatLngAlt secondLast = home;
            PointLatLngAlt lastnonspline = home;
            List<PointLatLngAlt> splinepnts = new List<PointLatLngAlt>();

            for (int i = 0; i < expandedWaypoints.Count; i++)
            {
                if (expandedWaypoints[i] == null)
                    continue;

                if (fullpointlist[i].Tag2 == "spline")
                {
                    if (splinepnts.Count == 0)
                        splinepnts.Add(lastpnt);

                    splinepnts.Add(fullpointlist[i]);
                    continue;
                }
                wproute.Add(expandedWaypoints[i]);

                //secondLast = lastPoint;
                //lastPoint = pointList[i];
            }*/

            List<PointLatLng> wproute = expandedWaypoints.Where(w => w != null)
                                            .Select(w => (PointLatLng)w).ToList();
            wproute.RemoveAt(wproute.Count - 1);
            wproute.RemoveAt(0);
            if (wproute.Count > 0)
            {
                PointLatLngAlt first = wproute[0];
                PointLatLngAlt last = wproute[wproute.Count - 1];
                GMapRouteExtend route = new GMapRouteExtend("wp route")
                { Stroke = new Pen(RoutingPathColor, 4) { DashStyle = DashStyle.Custom } };
                GMapRouteExtend homeRoute = new GMapRouteExtend("home route") { Stroke = new Pen(RoutingPathColor, 2) };

                route.Points.AddRange(wproute);
										
                // if we have a large distance between home and the first/last point, it hangs on the draw of a the dashed line.
                if (home.GetDistance(last) < 5000 && home.GetDistance(first) < 5000)
                    homeRoute.Stroke.DashStyle = DashStyle.Dash;
                homeRoute.Points.Add(last);
                homeRoute.Points.Add(home);
                homeRoute.Points.Add(first);

                Overlay.Routes.Add(homeRoute);
                Overlay.Routes.Add(route);
            }
        }

        public void ChangeRouteColor()
        {
            RoutingPathColor = Color.FromArgb(RNG.Next(256), RNG.Next(256), RNG.Next(256));
            DrawMission();
            Overlay.ForceUpdate();
        }
    }
}
