using System.Windows.Forms;

namespace Diva
{
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
            if (!float.TryParse(getString(ucTypeName + ".FontSizeAdjust"), out var locFontSizeAdjust))
                locFontSizeAdjust = 0f;
            if (locFontName != null)
            {
                void processControls(Control cc)
                {
                    foreach (Control c in cc.Controls)
                    {
                        string s = getString(textResName(c));
                        if (s != null)
                        {
                            c.Text = s;
                            c.Font = new System.Drawing.Font(locFontName, c.Font.Size + locFontSizeAdjust);
                        }
                        processControls(c);
                    }
                }
                processControls(userControl);
            }

        }
    }
}
