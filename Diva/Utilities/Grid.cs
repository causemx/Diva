using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diva.Mavlink;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using ProjNet.CoordinateSystems.Transformations;


namespace Diva.Utilities
{
    public class Grid
    {
        const double rad2deg = (180 / Math.PI);
        const double deg2rad = (1.0 / rad2deg);

        double angle = 0.0d;
        GMapOverlay Overlay;
        GMapPolygon Polygon;
        PointLatLngAlt Home;
        List<PointLatLngAlt> list = new List<PointLatLngAlt>();
		List<PointLatLngAlt> grid;

        public readonly MavDrone Drone;
        public static PointLatLngAlt StartPointLatLngAlt = PointLatLngAlt.Zero;

        public struct linelatlng
        {
            // start of line
            public UTMpos p1;
            // end of line
            public UTMpos p2;
            // used as a base for grid along line (initial setout)
            public UTMpos basepnt;
        }

        public struct Rect
        {
            public double Top;
            public double Bottom;
            public double Left;
            public double Right;

            public double Width { get { return Right - Left; } }
            public double Height { get { return Top - Bottom; } }

            public double MidWidth { get { return ((Right - Left) / 2) + Left; } }
            public double MidHeight { get { return ((Top - Bottom) / 2) + Bottom; } }

            public Rect(double Left, double Top, double Width, double Height)
            {
                this.Left = Left;
                this.Top = Top;
                this.Right = Left + Width;
                this.Bottom = Top + Height;
            }

            public double DiagDistance() => Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2));
        }


        public enum StartPosition
        {
            Home = 0,
            BottomLeft = 1,
            TopLeft = 2,
            BottomRight = 3,
            TopRight = 4,
            Point = 5,
        }

        public Grid(MavDrone drone, PointLatLngAlt home)
        {
            Drone = drone;
            Home = home;
            Overlay = new GMapOverlay("Grid_" + this.GetHashCode());
            Overlay.IsVisibile = Planner.GetPlannerInstance().FullControl;
            Planner.GetPlannerInstance().GMapControl?.Overlays.Add(Overlay);

            Polygon = new GMapPolygon(new List<PointLatLng>(), "drawGrid")
            {
                Stroke = new Pen(Color.Blue, 2),
                Fill = Brushes.Transparent
            };

            var points = new GMapPolygon(new List<PointLatLng>(Planner.GetPlannerInstance().drawnPolygon?.Points), "Poly_Copy");
            points.Points.ForEach(x => { list.Add(x); });
            points.Dispose();

            angle = (double)(getAngleOfLongestSide(list) + 360) % 360;
        }

        public async Task Accept(double altitude, double distance, double spacing, double tangle, StartPosition startPos, PointLatLngAlt home)
        {
			// quickadd = true;

			// var gridobject = savegriddata();

            grid = await CreateGridAsync(list, 100, 50, 0, angle, StartPosition.Home, Home).ConfigureAwait(true);

            foreach (var plla in grid)
            {
                AddWP(plla.Lng, plla.Lat, plla.Alt, plla.Tag);
            }
            // Redraw the polygon in MainMap
            // plugin.Host.RedrawFPPolygon(list);
            redrawPolygonSurvey(list);

            // savesettings();

            // MainV2.instance.FlightPlanner.quickadd = false;

            // MainV2.instance.FlightPlanner.writeKML();
            Planner.GetPlannerInstance()?.WriteKMLV2();

        }

        private void AddWP(double Lng, double Lat, double Alt, string tag)
        {
            // plugin.Host.AddWPtoList(MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0, Lng, Lat, (int)Alt);
            Planner.GetPlannerInstance()?.AddCommand(MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0, Lng, Lat, (int)Alt);
        }

        public void redrawPolygonSurvey(List<PointLatLngAlt> list)
        {
            Polygon.Points.Clear();
            Overlay.Clear();

            int tag = 0;
            list.ForEach(x =>
            {
                tag++;
                Polygon.Points.Add(x);
                addpolygonmarkergrid(tag.ToString(), x.Lng, x.Lat, 0);
            });

            Overlay.Polygons.Add(Polygon);
            Planner.GetPlannerInstance().GMapControl?.UpdatePolygonLocalPosition(Polygon);
            /*
			{
				foreach (var pointLatLngAlt in drawnPolygon.Points.CloseLoop().PrevNowNext())
				{
					var now = pointLatLngAlt.Item2;
					var next = pointLatLngAlt.Item3;

					if (now == null || next == null)
						continue;

					var mid = new PointLatLngAlt((now.Lat + next.Lat) / 2, (now.Lng + next.Lng) / 2, 0);

					var pnt = new GMapMarkerPlus(mid);
					pnt.Tag = new midline() { now = now, next = next };
					drawnpolygonsoverlay.Markers.Add(pnt);
				}
			}*/

            Planner.GetPlannerInstance().GMapControl?.Invalidate();
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
                GMapRectMarker mBorders = new GMapRectMarker(point);
                {
                    mBorders.InnerMarker = m;
                }

                Overlay.Markers.Add(m);
                Overlay.Markers.Add(mBorders);
            }
            catch (Exception ex)
            {
            }
        }

        public static async Task<List<PointLatLngAlt>> CreateGridAsync(List<PointLatLngAlt> polygon, double altitude,
            double distance, double spacing, double angle, StartPosition startpos, PointLatLngAlt HomeLocation, 
            bool shutter=false, float minLaneSeparation=0, float leadin1=0, float leadin2=0, double overshoot1=0, double overshoot2=0, bool useextendedendpoint = true)
        {
            return await Task.Run((() => CreateGrid(polygon, altitude, distance, spacing, angle, overshoot1, overshoot2,
                    startpos, shutter, minLaneSeparation, leadin1, leadin2, HomeLocation, useextendedendpoint)))
                .ConfigureAwait(false);
        }

        public static List<PointLatLngAlt> CreateGrid(List<PointLatLngAlt> polygon, double altitude, double distance, double spacing, double angle, double overshoot1, double overshoot2, StartPosition startpos, bool shutter, float minLaneSeparation, float leadin1, float leadin2, PointLatLngAlt HomeLocation, bool useextendedendpoint = true)
        {
            //DoDebug();

            if (spacing < 0.1 && spacing != 0)
                spacing = 0.1;

            if (distance < 0.1)
                distance = 0.1;

            if (polygon.Count == 0)
                return new List<PointLatLngAlt>();


            // Make a non round number in case of corner cases
            if (minLaneSeparation != 0)
                minLaneSeparation += 0.5F;
            // Lane Separation in meters
            double minLaneSeparationINMeters = minLaneSeparation * distance;

            List<PointLatLngAlt> ans = new List<PointLatLngAlt>();

            // utm zone distance calcs will be done in
            int utmzone = polygon[0].GetUTMZone();

            // utm position list
            List<UTMpos> utmpositions = UTMpos.ToList(PointLatLngAlt.ToUTM(utmzone, polygon), utmzone);

            // close the loop if its not already
            if (utmpositions[0] != utmpositions[utmpositions.Count - 1])
                utmpositions.Add(utmpositions[0]); // make a full loop

            // get mins/maxs of coverage area
            Rect area = getPolyMinMax(utmpositions);

            // get initial grid

            // used to determine the size of the outer grid area
            double diagdist = area.DiagDistance();

            // somewhere to store out generated lines
            List<linelatlng> grid = new List<linelatlng>();
            // number of lines we need
            int lines = 0;

            // get start point middle
            double x = area.MidWidth;
            double y = area.MidHeight;

            // get left extent
            double xb1 = x;
            double yb1 = y;
            // to the left
            newpos(ref xb1, ref yb1, angle - 90, diagdist / 2 + distance);
            // backwards
            newpos(ref xb1, ref yb1, angle + 180, diagdist / 2 + distance);

            UTMpos left = new UTMpos(xb1, yb1, utmzone);

            // get right extent
            double xb2 = x;
            double yb2 = y;
            // to the right
            newpos(ref xb2, ref yb2, angle + 90, diagdist / 2 + distance);
            // backwards
            newpos(ref xb2, ref yb2, angle + 180, diagdist / 2 + distance);

            UTMpos right = new UTMpos(xb2, yb2, utmzone);

            // set start point to left hand side
            x = xb1;
            y = yb1;

            // draw the outergrid, this is a grid that cover the entire area of the rectangle plus more.
            while (lines < ((diagdist + distance * 2) / distance))
            {
                // copy the start point to generate the end point
                double nx = x;
                double ny = y;
                newpos(ref nx, ref ny, angle, diagdist + distance * 2);

                linelatlng line = new linelatlng();
                line.p1 = new UTMpos(x, y, utmzone);
                line.p2 = new UTMpos(nx, ny, utmzone);
                line.basepnt = new UTMpos(x, y, utmzone);
                grid.Add(line);

                // addtomap(line);

                newpos(ref x, ref y, angle + 90, distance);
                lines++;
            }

            // find intersections with our polygon

            // store lines that dont have any intersections
            List<linelatlng> remove = new List<linelatlng>();

            int gridno = grid.Count;

            // cycle through our grid
            for (int a = 0; a < gridno; a++)
            {
                double closestdistance = double.MaxValue;
                double farestdistance = double.MinValue;

                UTMpos closestpoint = UTMpos.Zero;
                UTMpos farestpoint = UTMpos.Zero;

                // somewhere to store our intersections
                List<UTMpos> matchs = new List<UTMpos>();

                int b = -1;
                int crosses = 0;
                UTMpos newutmpos = UTMpos.Zero;
                foreach (UTMpos pnt in utmpositions)
                {
                    b++;
                    if (b == 0)
                    {
                        continue;
                    }
                    newutmpos = FindLineIntersection(utmpositions[b - 1], utmpositions[b], grid[a].p1, grid[a].p2);
                    if (!newutmpos.IsZero)
                    {
                        crosses++;
                        matchs.Add(newutmpos);
                        if (closestdistance > grid[a].p1.GetDistance(newutmpos))
                        {
                            closestpoint.y = newutmpos.y;
                            closestpoint.x = newutmpos.x;
                            closestpoint.zone = newutmpos.zone;
                            closestdistance = grid[a].p1.GetDistance(newutmpos);
                        }
                        if (farestdistance < grid[a].p1.GetDistance(newutmpos))
                        {
                            farestpoint.y = newutmpos.y;
                            farestpoint.x = newutmpos.x;
                            farestpoint.zone = newutmpos.zone;
                            farestdistance = grid[a].p1.GetDistance(newutmpos);
                        }
                    }
                }
                if (crosses == 0) // outside our polygon
                {
                    if (!PointInPolygon(grid[a].p1, utmpositions) && !PointInPolygon(grid[a].p2, utmpositions))
                        remove.Add(grid[a]);
                }
                else if (crosses == 1) // bad - shouldnt happen
                {

                }
                else if (crosses == 2) // simple start and finish
                {
                    linelatlng line = grid[a];
                    line.p1 = closestpoint;
                    line.p2 = farestpoint;
                    grid[a] = line;
                }
                else // multiple intersections
                {
                    linelatlng line = grid[a];
                    remove.Add(line);

                    while (matchs.Count > 1)
                    {
                        linelatlng newline = new linelatlng();

                        closestpoint = findClosestPoint(closestpoint, matchs);
                        newline.p1 = closestpoint;
                        matchs.Remove(closestpoint);

                        closestpoint = findClosestPoint(closestpoint, matchs);
                        newline.p2 = closestpoint;
                        matchs.Remove(closestpoint);

                        newline.basepnt = line.basepnt;

                        grid.Add(newline);
                    }
                }
            }

            // cleanup and keep only lines that pass though our polygon
            foreach (linelatlng line in remove)
            {
                grid.Remove(line);
            }

           
            if (grid.Count == 0)
                return ans;

            // pick start positon based on initial point rectangle
            UTMpos startposutm;

            switch (startpos)
            {
                default:
                case StartPosition.Home:
                    startposutm = new UTMpos(HomeLocation);
                    break;
                case StartPosition.BottomLeft:
                    startposutm = new UTMpos(area.Left, area.Bottom, utmzone);
                    break;
                case StartPosition.BottomRight:
                    startposutm = new UTMpos(area.Right, area.Bottom, utmzone);
                    break;
                case StartPosition.TopLeft:
                    startposutm = new UTMpos(area.Left, area.Top, utmzone);
                    break;
                case StartPosition.TopRight:
                    startposutm = new UTMpos(area.Right, area.Top, utmzone);
                    break;
                case StartPosition.Point:
                    startposutm = new UTMpos(StartPointLatLngAlt);
                    break;
            }

            // find the closes polygon point based from our startpos selection
            startposutm = findClosestPoint(startposutm, utmpositions);

            // find closest line point to startpos
            linelatlng closest = findClosestLine(startposutm, grid, 0 /*Lane separation does not apply to starting point*/, angle);

            UTMpos lastpnt;

            // get the closes point from the line we picked
            if (closest.p1.GetDistance(startposutm) < closest.p2.GetDistance(startposutm))
            {
                lastpnt = closest.p1;
            }
            else
            {
                lastpnt = closest.p2;
            }

            // S =  start
            // E = end
            // ME = middle end
            // SM = start middle

            while (grid.Count > 0)
            {
                // for each line, check which end of the line is the next closest
                if (closest.p1.GetDistance(lastpnt) < closest.p2.GetDistance(lastpnt))
                {
                    UTMpos newstart = newpos(closest.p1, angle, -leadin1);
                    newstart.Tag = "S";
                    ans.Add(newstart);

                    if (leadin1 < 0)
                    {
                        var p2 = new UTMpos(newstart) { Tag = "SM" };
                        ans.Add(p2);
                    }
                    else
                    {
                        closest.p1.Tag = "SM";
                        ans.Add(closest.p1);
                    }

                    if (spacing > 0)
                    {
                        for (double d = (spacing - ((closest.basepnt.GetDistance(closest.p1)) % spacing));
                            d < (closest.p1.GetDistance(closest.p2));
                            d += spacing)
                        {
                            double ax = closest.p1.x;
                            double ay = closest.p1.y;

                            newpos(ref ax, ref ay, angle, d);
                            var utmpos1 = new UTMpos(ax, ay, utmzone) { Tag = "M" };
                            ans.Add(utmpos1);
                        }
                    }

                    UTMpos newend = newpos(closest.p2, angle, overshoot1);

                    if (overshoot1 < 0)
                    {
                        var p2 = new UTMpos(newend) { Tag = "ME" };
                        ans.Add(p2);
                    }
                    else
                    {
                        closest.p2.Tag = "ME";
                        ans.Add(closest.p2);
                    }

                    newend.Tag = "E";
                    ans.Add(newend);

                    lastpnt = closest.p2;

                    grid.Remove(closest);
                    if (grid.Count == 0)
                        break;

                    if (useextendedendpoint)
                        closest = findClosestLine(newend, grid, minLaneSeparationINMeters, angle);
                    else
                        closest = findClosestLine(closest.p2, grid, minLaneSeparationINMeters, angle);
                }
                else
                {
                    UTMpos newstart = newpos(closest.p2, angle, leadin2);
                    newstart.Tag = "S";
                    ans.Add(newstart);

                    if (leadin2 < 0)
                    {
                        var p2 = new UTMpos(newstart) { Tag = "SM" };
                        ans.Add(p2);
                    }
                    else
                    {
                        closest.p2.Tag = "SM";
                        ans.Add(closest.p2);
                    }

                    if (spacing > 0)
                    {
                        for (double d = ((closest.basepnt.GetDistance(closest.p2)) % spacing);
                            d < (closest.p1.GetDistance(closest.p2));
                            d += spacing)
                        {
                            double ax = closest.p2.x;
                            double ay = closest.p2.y;

                            newpos(ref ax, ref ay, angle, -d);
                            var utmpos2 = new UTMpos(ax, ay, utmzone) { Tag = "M" };
                            ans.Add(utmpos2);
                        }
                    }

                    UTMpos newend = newpos(closest.p1, angle, -overshoot2);

                    if (overshoot2 < 0)
                    {
                        var p2 = new UTMpos(newend) { Tag = "ME" };
                        ans.Add(p2);
                    }
                    else
                    {
                        closest.p1.Tag = "ME";
                        ans.Add(closest.p1);
                    }

                    newend.Tag = "E";
                    ans.Add(newend);

                    lastpnt = closest.p1;

                    grid.Remove(closest);
                    if (grid.Count == 0)
                        break;

                    if (useextendedendpoint)
                        closest = findClosestLine(newend, grid, minLaneSeparationINMeters, angle);
                    else
                        closest = findClosestLine(closest.p1, grid, minLaneSeparationINMeters, angle);
                }
            }

            // set the altitude on all points
            ans.ForEach(plla => { plla.Alt = altitude; });

            return ans;
        }

        /// <summary>
        /// from http://stackoverflow.com/questions/1119451/how-to-tell-if-a-line-intersects-a-polygon-in-c
        /// </summary>
        /// <param name="start1"></param>
        /// <param name="end1"></param>
        /// <param name="start2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static UTMpos FindLineIntersection(UTMpos start1, UTMpos end1, UTMpos start2, UTMpos end2)
        {
            double denom = ((end1.x - start1.x) * (end2.y - start2.y)) - ((end1.y - start1.y) * (end2.x - start2.x));
            //  AB & CD are parallel         
            if (denom == 0)
                return UTMpos.Zero;
            double numer = ((start1.y - start2.y) * (end2.x - start2.x)) - ((start1.x - start2.x) * (end2.y - start2.y));
            double r = numer / denom;
            double numer2 = ((start1.y - start2.y) * (end1.x - start1.x)) - ((start1.x - start2.x) * (end1.y - start1.y));
            double s = numer2 / denom;
            if ((r < 0 || r > 1) || (s < 0 || s > 1))
                return UTMpos.Zero;
            // Find intersection point      
            UTMpos result = new UTMpos();
            result.x = start1.x + (r * (end1.x - start1.x));
            result.y = start1.y + (r * (end1.y - start1.y));
            result.zone = start1.zone;
            return result;
        }

        /// <summary>
        /// from http://stackoverflow.com/questions/1119451/how-to-tell-if-a-line-intersects-a-polygon-in-c
        /// </summary>
        /// <param name="start1"></param>
        /// <param name="end1"></param>
        /// <param name="start2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static UTMpos FindLineIntersectionExtension(UTMpos start1, UTMpos end1, UTMpos start2, UTMpos end2)
        {
            double denom = ((end1.x - start1.x) * (end2.y - start2.y)) - ((end1.y - start1.y) * (end2.x - start2.x));
            //  AB & CD are parallel         
            if (denom == 0)
                return UTMpos.Zero;
            double numer = ((start1.y - start2.y) * (end2.x - start2.x)) -
                           ((start1.x - start2.x) * (end2.y - start2.y));
            double r = numer / denom;
            double numer2 = ((start1.y - start2.y) * (end1.x - start1.x)) -
                            ((start1.x - start2.x) * (end1.y - start1.y));
            double s = numer2 / denom;
            if ((r < 0 || r > 1) || (s < 0 || s > 1))
            {
                // line intersection is outside our lines.
            }
            // Find intersection point      
            UTMpos result = new UTMpos();
            result.x = start1.x + (r * (end1.x - start1.x));
            result.y = start1.y + (r * (end1.y - start1.y));
            result.zone = start1.zone;
            return result;
        }

        private static List<UTMpos> GenerateOffsetPath(List<UTMpos> UTMpositions, double distance, double spacing, int utmzone)
        {
            List<UTMpos> ans = new List<UTMpos>();

            UTMpos oldpos = UTMpos.Zero;

            for (int a = 0; a < UTMpositions.Count - 2; a++)
            {
                var prevCenter = UTMpositions[a];
                var currCenter = UTMpositions[a + 1];
                var nextCenter = UTMpositions[a + 2];

                var l1bearing = prevCenter.GetBearing(currCenter);
                var l2bearing = currCenter.GetBearing(nextCenter);

                var l1prev = newpos(prevCenter, l1bearing + 90, distance);
                var l1curr = newpos(currCenter, l1bearing + 90, distance);

                var l2curr = newpos(currCenter, l2bearing + 90, distance);
                var l2next = newpos(nextCenter, l2bearing + 90, distance);

                var l1l2center = FindLineIntersectionExtension(l1prev, l1curr, l2curr, l2next);

                //start
                if (a == 0)
                {
                    // add start
                    l1prev.Tag = "S";
                    ans.Add(l1prev);

                    // add start/trigger
                    l1prev.Tag = "SM";
                    ans.Add(l1prev);

                    oldpos = l1prev;
                }

                //spacing
                if (spacing > 0)
                {
                    for (int d = (int)((oldpos.GetDistance(l1l2center)) % spacing);
                        d < (oldpos.GetDistance(l1l2center));
                        d += (int)spacing)
                    {
                        double ax = oldpos.x;
                        double ay = oldpos.y;

                        newpos(ref ax, ref ay, l1bearing, d);
                        var UTMpos2 = new UTMpos(ax, ay, utmzone) { Tag = "M" };
                        ans.Add(UTMpos2);
                    }
                }

                //end of leg
                l1l2center.Tag = "S";
                ans.Add(l1l2center);
                oldpos = l1l2center;

                // last leg
                if ((a + 3) == UTMpositions.Count)
                {
                    if (spacing > 0)
                    {
                        for (int d = (int)((l1l2center.GetDistance(l2next)) % spacing);
                            d < (l1l2center.GetDistance(l2next));
                            d += (int)spacing)
                        {
                            double ax = l1l2center.x;
                            double ay = l1l2center.y;

                            newpos(ref ax, ref ay, l2bearing, d);
                            var UTMpos2 = new UTMpos(ax, ay, utmzone) { Tag = "M" };
                            ans.Add(UTMpos2);
                        }
                    }

                    l2next.Tag = "ME";
                    ans.Add(l2next);

                    l2next.Tag = "E";
                    ans.Add(l2next);
                }
            }

            return ans;
        }

        // polar to rectangular
        static void newpos(ref double x, ref double y, double bearing, double distance)
        {
            double degN = 90 - bearing;
            if (degN < 0)
                degN += 360;
            x = x + distance * Math.Cos(degN * deg2rad);
            y = y + distance * Math.Sin(degN * deg2rad);
        }

        // polar to rectangular
        static UTMpos newpos(UTMpos input, double bearing, double distance)
        {
            double degN = 90 - bearing;
            if (degN < 0)
                degN += 360;
            double x = input.x + distance * Math.Cos(degN * deg2rad);
            double y = input.y + distance * Math.Sin(degN * deg2rad);

            return new UTMpos(x, y, input.zone);
        }

        static Rect getPolyMinMax(List<UTMpos> UTMpos)
        {
            if (UTMpos.Count == 0)
                return new Rect();

            double minx, miny, maxx, maxy;

            minx = maxx = UTMpos[0].x;
            miny = maxy = UTMpos[0].y;

            foreach (UTMpos pnt in UTMpos)
            {
                minx = Math.Min(minx, pnt.x);
                maxx = Math.Max(maxx, pnt.x);

                miny = Math.Min(miny, pnt.y);
                maxy = Math.Max(maxy, pnt.y);
            }

            return new Rect(minx, maxy, maxx - minx, miny - maxy);
        }

        static UTMpos findClosestPoint(UTMpos start, List<UTMpos> list)
        {
            UTMpos answer = UTMpos.Zero;
            double currentbest = double.MaxValue;

            foreach (UTMpos pnt in list)
            {
                double dist1 = start.GetDistance(pnt);

                if (dist1 < currentbest)
                {
                    answer = pnt;
                    currentbest = dist1;
                }
            }

            return answer;
        }

        static double getAngleOfLongestSide(List<PointLatLngAlt> list)
        {
            if (list.Count == 0)
                return 0;
            double angle = 0;
            double maxdist = 0;
            PointLatLngAlt last = list[list.Count - 1];
            foreach (var item in list)
            {
                if (item.GetDistance(last) > maxdist)
                {
                    angle = item.GetBearing(last);
                    maxdist = item.GetDistance(last);
                }
                last = item;
            }

            return (angle + 360) % 360;
        }

        static bool PointInPolygon(UTMpos p, List<UTMpos> poly)
        {
            UTMpos p1, p2;
            bool inside = false;

            if (poly.Count < 3)
            {
                return inside;
            }
            UTMpos oldPoint = new UTMpos(poly[poly.Count - 1]);

            for (int i = 0; i < poly.Count; i++)
            {

                UTMpos newPoint = new UTMpos(poly[i]);

                if (newPoint.y > oldPoint.y)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.y < p.y) == (p.y <= oldPoint.y)
                    && ((double)p.x - (double)p1.x) * (double)(p2.y - p1.y)
                    < ((double)p2.x - (double)p1.x) * (double)(p.y - p1.y))
                {
                    inside = !inside;
                }
                oldPoint = newPoint;
            }
            return inside;
        }

        static double AddAngle(double angle, double degrees)
        {
            angle += degrees;

            angle = angle % 360;

            while (angle < 0)
            {
                angle += 360;
            }
            return angle;
        }

        static linelatlng findClosestLine(UTMpos start, List<linelatlng> list, double minDistance, double angle)
        {
            if (minDistance == 0)
            {
                linelatlng answer = list[0];
                double shortest = double.MaxValue;

                foreach (linelatlng line in list)
                {
                    double ans1 = start.GetDistance(line.p1);
                    double ans2 = start.GetDistance(line.p2);
                    UTMpos shorterpnt = ans1 < ans2 ? line.p1 : line.p2;

                    if (shortest > start.GetDistance(shorterpnt))
                    {
                        answer = line;
                        shortest = start.GetDistance(shorterpnt);
                    }
                }

                return answer;
            }


            // By now, just add 5.000 km to our lines so they are long enough to allow intersection
            double METERS_TO_EXTEND = 5000;

            double perperndicularOrientation = AddAngle(angle, 90);

            // Calculation of a perpendicular line to the grid lines containing the "start" point
            /*
             *  --------------------------------------|------------------------------------------
             *  --------------------------------------|------------------------------------------
             *  -------------------------------------start---------------------------------------
             *  --------------------------------------|------------------------------------------
             *  --------------------------------------|------------------------------------------
             *  --------------------------------------|------------------------------------------
             *  --------------------------------------|------------------------------------------
             *  --------------------------------------|------------------------------------------
             */
            UTMpos start_perpendicular_line = newpos(start, perperndicularOrientation, -METERS_TO_EXTEND);
            UTMpos stop_perpendicular_line = newpos(start, perperndicularOrientation, METERS_TO_EXTEND);

            // Store one intersection point per grid line
            Dictionary<UTMpos, linelatlng> intersectedPoints = new Dictionary<UTMpos, linelatlng>();
            // lets order distances from every intersected point per line with the "start" point
            Dictionary<double, UTMpos> ordered_min_to_max = new Dictionary<double, UTMpos>();

            foreach (linelatlng line in list)
            {
                // Calculate intersection point
                UTMpos p = FindLineIntersectionExtension(line.p1, line.p2, start_perpendicular_line, stop_perpendicular_line);

                // Store it
                intersectedPoints[p] = line;

                // Calculate distances between interesected point and "start" (i.e. line and start)
                double distance_p = start.GetDistance(p);

                if (!ordered_min_to_max.ContainsKey(distance_p))
                    ordered_min_to_max.Add(distance_p, p);
            }

            // Acquire keys and sort them.
            List<double> ordered_keys = ordered_min_to_max.Keys.ToList();
            ordered_keys.Sort();

            // Lets select a line that is the closest to "start" point but "mindistance" away at least.
            // If we have only one line, return that line whatever the minDistance says
            double key = double.MaxValue;
            int i = 0;
            while (key == double.MaxValue && i < ordered_keys.Count)
            {
                if (ordered_keys[i] >= minDistance)
                    key = ordered_keys[i];
                i++;
            }

            // If no line is selected (because all of them are closer than minDistance, then get the farest one
            if (key == double.MaxValue)
                key = ordered_keys[ordered_keys.Count - 1];

            var filteredlist = intersectedPoints.Where(a => a.Key.GetDistance(start) >= key);

            return findClosestLine(start, filteredlist.Select(a => a.Value).ToList(), 0, angle);
        }

    }
}
