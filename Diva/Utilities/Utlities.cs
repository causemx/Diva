using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Diva.Utilities
{
    public static class MathHelper
    {
        public const double rad2deg = (180 / Math.PI);
        public const double deg2rad = (1.0 / rad2deg);

        public static double Degrees(double rad)
        {
            return rad * rad2deg;
        }

        public static double Radians(double deg)
        {
            return deg * deg2rad;
        }

        public static double map(double x, double in_min, double in_max, double out_min, double out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
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
