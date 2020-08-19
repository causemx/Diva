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
        public const double deg2rad = (1.0 / rad2deg);

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

        public static double DistanceTo(this PointLatLng from, PointLatLng to)
        {
            double d = from.Lat * 0.017453292519943295;
            double num2 = from.Lng * 0.017453292519943295;
            double num3 = to.Lat * 0.017453292519943295;
            double num4 = to.Lng * 0.017453292519943295;
            double num5 = num4 - num2;
            double num6 = num3 - d;
            double num7 = Math.Pow(Math.Sin(num6 / 2.0), 2.0) + ((Math.Cos(d) * Math.Cos(num3)) * Math.Pow(Math.Sin(num5 / 2.0), 2.0));
            double num8 = 2.0 * Math.Atan2(Math.Sqrt(num7), Math.Sqrt(1.0 - num7));
            return (6371 * num8) * 1000.0; // M
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
