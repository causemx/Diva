using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Utilities
{
    class OverlayUtility
    {
        public class WPOverlay
        {
            public WPOverlay(GMapOverlay _overlay) { overlay = _overlay; }

            public GMapOverlay overlay = null;
            public GMapRoute route = new GMapRoute("wp route");
            public GMapRoute homeroute = new GMapRoute("home route");
            /// list of points as per the mission
            public List<PointLatLngAlt> pointlist = new List<PointLatLngAlt>();
            /// list of point as per mission including jump repeats
            public List<PointLatLngAlt> fullpointlist = new List<PointLatLngAlt>();

            public void AddPolygonMarker(string tag, double lng, double lat, double alt, Color? color, double wpradius)
            {
                try
                {
                    PointLatLng point = new PointLatLng(lat, lng);
                    GMapTaggedMarker m = new GMapTaggedMarker(point, tag)
                    {
                        ToolTipMode = MarkerTooltipMode.OnMouseOver,
                        ToolTipText = "Alt: " + alt.ToString("0"),
                    };

                    GMapRectMarker mBorders = new GMapRectMarker(point)
                    {
                        InnerMarker = m,
                        Tag = tag,
                        wprad = (int)wpradius
                    };
                    if (color.HasValue)
                    {
                        mBorders.Color = color.Value;
                    }

                    overlay.Markers.Add(m);
                    overlay.Markers.Add(mBorders);
                }
                catch (Exception)
                {
                }
            }

            public void CreateOverlay(PointLatLngAlt home, List<WayPoint> missionitems, double wpradius, double loiterradius)
            {
                overlay.Clear();

                double maxlat = -180;
                double maxlong = -180;
                double minlat = 180;
                double minlong = 180;

                double gethomealt(double lat, double lng)
                    => GetHomeAlt(MAVLink.MAV_FRAME.GLOBAL_INT, home.Alt, lat, lng);

                home.Tag = "H";
                // home.Tag2 = altmode.ToString();

                pointlist.Add(home);
                fullpointlist.Add(pointlist[pointlist.Count - 1]);
                AddPolygonMarker("H", home.Lng, home.Lat, home.Alt, null, 0);

                int a = 0;
                foreach (var item in missionitems)
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

                        // land can be 0,0 or a lat,lng
                        if (command == (ushort)MAVLink.MAV_CMD.LAND && lat == 0 && lng == 0)
                            continue;

                        if (command == (ushort)MAVLink.MAV_CMD.DO_SET_ROI)
                        {
                            pointlist.Add(new PointLatLngAlt(lat, lng,
                                alt + gethomealt(lat, lng), "ROI" + (a + 1))
                            { Color = Color.Red });
                            // do set roi is not a nav command. so we dont route through it
                            //fullpointlist.Add(pointlist[pointlist.Count - 1]);
                            GMarkerGoogle m =
                                new GMarkerGoogle(new PointLatLng(lat, lng),
                                    GMarkerGoogleType.red)
                                {
                                    ToolTipMode = MarkerTooltipMode.Always,
                                    ToolTipText = (a + 1).ToString(),
                                    Tag = (a + 1).ToString()
                                };

                            GMapRectMarker mBorders = new GMapRectMarker(m.Position);
                            {
                                mBorders.InnerMarker = m;
                                mBorders.Tag = "Dont draw line";
                            }

                            // check for clear roi, and hide it
                            if (m.Position.Lat != 0 && m.Position.Lng != 0)
                            {
                                // order matters
                                overlay.Markers.Add(m);
                                overlay.Markers.Add(mBorders);
                            }
                        }
                        else if (command == (ushort)MAVLink.MAV_CMD.LOITER_TIME ||
                                 command == (ushort)MAVLink.MAV_CMD.LOITER_TURNS ||
                                 command == (ushort)MAVLink.MAV_CMD.LOITER_UNLIM)
                        {
                            pointlist.Add(new PointLatLngAlt(lat, lng,
                                alt + gethomealt(lat, lng), (a + 1).ToString())
                            {
                                Color = Color.LightBlue
                            });
                            fullpointlist.Add(pointlist[pointlist.Count - 1]);
                            AddPolygonMarker((a + 1).ToString(), lng, lat,
                                alt, Color.LightBlue, loiterradius);
                        }
                        else if (command == (ushort)MAVLink.MAV_CMD.SPLINE_WAYPOINT)
                        {
                            pointlist.Add(new PointLatLngAlt(lat, lng,
                                alt + gethomealt(lat, lng), (a + 1).ToString())
                            { Tag2 = "spline" });
                            fullpointlist.Add(pointlist[pointlist.Count - 1]);
                            AddPolygonMarker((a + 1).ToString(), lng, lat,
                                alt, Color.Green, wpradius);
                        }
                        else
                        {
                            pointlist.Add(new PointLatLngAlt(lat, lng,
                                alt + gethomealt(lat, lng), (a + 1).ToString()));
                            fullpointlist.Add(pointlist[pointlist.Count - 1]);
                            AddPolygonMarker((a + 1).ToString(), lng, lat,
                                alt, null, wpradius);
                        }

                        maxlong = Math.Max(lng, maxlong);
                        maxlat = Math.Max(lat, maxlat);
                        minlong = Math.Min(lng, minlong);
                        minlat = Math.Min(lat, minlat);
                    }
                    else if (command == (ushort)MAVLink.MAV_CMD.DO_JUMP) // fix do jumps into the future
                    {
                        pointlist.Add(null);

                        int wpno = (int)item.Param1;
                        int repeat = (int)item.Param2;

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

                    a++;
                }

                RegenerateWPRoute(fullpointlist, home);
            }

			public event EventHandler<FullPointsEventArgs> RaiseFullPointsEvent;

			public void RegenerateWPRoute(List<PointLatLngAlt> fullpointlist, PointLatLngAlt HomeLocation)
            {
                route.Clear();
                homeroute.Clear();

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


					// raise the fullpointslist outside
					RaiseFullPointsEvent?.Invoke(this, new FullPointsEventArgs(fullpointlist));


					homeroute.Stroke = new Pen(Color.BlanchedAlmond, 2);
                    // if we have a large distance between home and the first/last point, it hangs on the draw of a the dashed line.
                    if (homepoint.GetDistance(lastpoint) < 5000 && homepoint.GetDistance(firstpoint) < 5000)
                        homeroute.Stroke.DashStyle = DashStyle.Dash;

                    overlay.Routes.Add(homeroute);

                    route.Stroke = new Pen(Color.BlanchedAlmond, 4);
                    route.Stroke.DashStyle = DashStyle.Custom;
                    overlay.Routes.Add(route);
                }
            }

            private double GetHomeAlt(MAVLink.MAV_FRAME altmode, double homealt, double lat, double lng)
            {
                if (altmode == MAVLink.MAV_FRAME.GLOBAL_INT || altmode == MAVLink.MAV_FRAME.GLOBAL)
                {
                    return 0; // for absolute we dont need to add homealt
                }

                if (altmode == MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT_INT || altmode == MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT)
                {
                    return 100;
                    //return srtm.getAltitude(lat, lng).alt;
                }

                return homealt;
            }

			public class FullPointsEventArgs : EventArgs
			{
				public FullPointsEventArgs(List<PointLatLngAlt> _fullPoints)
				{
					this.FullPoints = _fullPoints;
				}

				public List<PointLatLngAlt> FullPoints { get; set; }
			}
        }
    }
}
