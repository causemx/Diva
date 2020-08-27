using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PointLatLng = GMap.NET.PointLatLng;

namespace Diva.Utilities
{
    public static class MathHelper
    {
        public const double rad2deg = (180 / Math.PI);
        public const double deg2rad = (Math.PI / 180);

        public static bool InsideOf(this PointLatLng p, PointLatLng[] pn)
        {
            bool r = false;
            double lng = p.Lng, lat = p.Lat;
            for (int i = pn.Length, j = 0; --i >= 0; j = i)
            {
                if (((pn[i].Lng > lng) != (pn[j].Lng > lng)) &&
                    (lat < (pn[j].Lat - pn[i].Lat) * (lng - pn[i].Lng) /
                        (pn[j].Lng - pn[i].Lng) + pn[i].Lat))
                    r = !r;
            }
            return r;
        }

        public static bool InsideOf(this PointLatLng p, System.Collections.Generic.List<PointLatLng> pn)
            => p.InsideOf(pn.ToArray());

        public static double DistanceBetween(double lat1, double lng1, double lat2, double lng2)
        {
            double d = lat1 * deg2rad;
            double num2 = lng1 * deg2rad;
            double num3 = lat2 * deg2rad;
            double num4 = lng2 * deg2rad;
            double num5 = num4 - num2;
            double num6 = num3 - d;
            double num7 = Math.Pow(Math.Sin(num6 / 2.0), 2.0) + ((Math.Cos(d) * Math.Cos(num3)) * Math.Pow(Math.Sin(num5 / 2.0), 2.0));
            double num8 = 2.0 * Math.Atan2(Math.Sqrt(num7), Math.Sqrt(1.0 - num7));
            return (6371 * num8) * 1000.0; // M
        }

        public static double BearingOf(double lat1, double lng1, double lat2, double lng2)
        {
            var lngdiff = (lng2 - lng1) * deg2rad;
            lat1 *= deg2rad;
            lat2 *= deg2rad;

            var y = Math.Sin(lngdiff) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lngdiff);

            return (rad2deg * (Math.Atan2(y, x)) + 360) % 360;
        }

        public static double DistanceTo(this PointLatLng from, PointLatLng to)
            => DistanceBetween(from.Lat, from.Lng, to.Lat, to.Lng);

        public static double BearingTo(this PointLatLng from, PointLatLng to)
            => BearingOf(from.Lat, from.Lng, to.Lat, to.Lng);

        public static PointLatLng OffsetAngleDistance(this PointLatLng from, double bearing, double distance)
        {
            // '''extrapolate latitude/longitude given a heading and distance 
            //   thanks to http://www.movable-type.co.uk/scripts/latlong.html
            //  '''
            // from math import sin, asin, cos, atan2, radians, degrees
            double radius_of_earth = 6378100.0;//# in meters

            double lat1 = deg2rad * (from.Lat);
            double lon1 = deg2rad * (from.Lng);
            double brng = deg2rad * (bearing);
            double dr = distance / radius_of_earth;

            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(dr) +
                        Math.Cos(lat1) * Math.Sin(dr) * Math.Cos(brng));
            double lon2 = lon1 + Math.Atan2(Math.Sin(brng) * Math.Sin(dr) * Math.Cos(lat1),
                                Math.Cos(dr) - Math.Sin(lat1) * Math.Sin(lat2));

            double latout = rad2deg * (lat2);
            double lngout = rad2deg * (lon2);

            return new PointLatLng(latout, lngout);
        }
    }

    public static class ResourceHelper
    {
        // strings with format paramaters
        public static string FormatWith(this string s, object o)
            => string.Format(s, o);
        public static string FormatWith(this string s, params object[] objs)
            => string.Format(s, objs);

        // user control update
        // only apply on create if no language neutral resource defined
        public static void UpdateLocale(this UserControl userControl)
        {
            System.Resources.ResourceManager resMgr = Properties.Strings.ResourceManager;
            string ucTypeName = userControl.GetType().Name;
            string textResName(Control c) => $"{ucTypeName}.{c.Name}.Text";
            string getString(string name)
            {
                string res = null;
                try
                {
                    res = resMgr.GetString(name);
                }
                catch { }
                return res;
            }
            string locFontName = getString(ucTypeName + ".FontFamily");
            if ((locFontName ?? "").Length > 0)
            {
                if (!float.TryParse(getString(ucTypeName + ".FontSizeAdjust"), out var locFontSizeAdjust))
                    locFontSizeAdjust = 0f;
                void processControls(Control cc)
                {
                    foreach (Control c in cc.Controls)
                    {
                        string s = getString(textResName(c));
                        if (s != null)
                        {
                            c.Text = s;
                            c.Font = new Font(locFontName, c.Font.Size + locFontSizeAdjust);
                            var prop = c.GetType().GetProperties().SingleOrDefault(p => p.Name == "TitleFont");
                            if (prop != null)
                                prop.SetValue(c, new Font(c.Font, FontStyle.Bold));
                        }
                        processControls(c);
                    }
                }
                processControls(userControl);
            }
        }
    }
}
