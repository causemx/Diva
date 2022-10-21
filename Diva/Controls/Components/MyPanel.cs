using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls.Components
{
    public class MyPanel : Panel
    {

        public event EventHandler SettingClick;
        public event EventHandler ExpandClick;

        Rectangle settingHitZone = new Rectangle();
        Rectangle expandHitZone = new Rectangle();
        public string Title { get; set; } = "title";

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (settingHitZone.IntersectsWith(new Rectangle(e.X, e.Y, 5, 5)))
                SettingClick.Invoke(this, null);
            if (expandHitZone.IntersectsWith(new Rectangle(e.X, e.Y, 5, 5)))
                ExpandClick?.Invoke(this, null);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Bitmap setting = Properties.Resources.icon_panel_setting_24;
            // Bitmap expand = Properties.Resources.icon_panel_expand_24;
            settingHitZone = new Rectangle(0, 0, 24, 24);
            expandHitZone = new Rectangle(Width - 24, 0, 24, 24);
            using (var brush = new SolidBrush(Color.WhiteSmoke))
            {
                using (var p = new Pen(Color.Red))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(0, 0, Width, 24));
                    e.Graphics.DrawRectangle(p, settingHitZone);
                    e.Graphics.DrawRectangle(p, expandHitZone);
                }
            }
            using (var bb = new SolidBrush(Color.Black))
            {
                using (var f = SystemFonts.MessageBoxFont)
                    e.Graphics.DrawString(Title, f, bb, Width / 2-1, 0);
            }
        }
    }
}
