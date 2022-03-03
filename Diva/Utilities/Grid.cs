using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjNet.CoordinateSystems.Transformations;


namespace Diva.Utilities
{
    public class Grid
    {
        const double rad2deg = (180 / Math.PI);
        const double deg2rad = (1.0 / rad2deg);

        public enum StartPosition
        {
            Home = 0,
            BottomLeft = 1,
            TopLeft = 2,
            BottomRight = 3,
            TopRight = 4,
            Point = 5,
        }

        public static async Task<List<PointLatLngAlt>> CreateCorridorAsync(List<PointLatLngAlt> polygon, double altitude,
            double distance,
            double spacing, double angle, double overshoot1, double overshoot2, StartPosition startpos, bool shutter,
            float minLaneSeparation, double width, float leadin = 0)
        {
            return await Task.Run(() => CreateCorridor(polygon, altitude, distance, spacing, angle, overshoot1, overshoot2,
                startpos, shutter, minLaneSeparation, width, leadin)).ConfigureAwait(false);
        }

        public static List<PointLatLngAlt> CreateCorridor(List<PointLatLngAlt> polygon, double altitude, double distance,
            double spacing, double angle, double overshoot1, double overshoot2, StartPosition startpos, bool shutter,
            float minLaneSeparation, double width, float leadin = 0)
        {
            if (spacing < 4 && spacing != 0)
                spacing = 4;

            if (distance < 0.1)
                distance = 0.1;

            if (polygon.Count == 0)
                return new List<PointLatLngAlt>();

            List<PointLatLngAlt> ans = new List<PointLatLngAlt>();

            // utm zone distance calcs will be done in
            int utmzone = polygon[0].GetUTMZone();

            // utm position list
            List<UTMpos> UTMpositions = UTMpos.ToList(PointLatLngAlt.ToUTM(utmzone, polygon), utmzone);

            var lanes = (width / distance);
            var start = (int)((lanes / 2) * -1);
            var end = start * -1;

            for (int lane = start; lane <= end; lane++)
            {
                // correct side of the line we are on because of list reversal
                int multi = 1;
                if ((lane - start) % 2 == 1)
                    multi = -1;

                if (startpos != StartPosition.Home)
                    UTMpositions.Reverse();

                GenerateOffsetPath(UTMpositions, distance * multi * lane, spacing, utmzone)
                    .ForEach(pnt => { ans.Add(pnt); });

                if (startpos == StartPosition.Home)
                    UTMpositions.Reverse();
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
            x += distance * Math.Cos(degN * deg2rad);
            y += distance * Math.Sin(degN * deg2rad);
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

    }
}
