using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Controls.Icons
{
    public class Badges
    {
        const int Padding = 10;
        const int DefaultWidth = 32;
        const int DefaultHeight = 32;

        private int _isSelected = -1;
        private Point _location = new Point(0, 0);
        private Bitmap[] _bmps = null;
        private Rectangle[] _rects = null;
        
        public enum Type
        {
            NONE = -1,
            POLYGON = 0,
            EKF = 1,
            FISH_STAMP = 2,
        }

        public int IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public Badges(params Bitmap[] bmps)
        {
            this._bmps = bmps;
            _rects = new Rectangle[bmps.Length];
            for (int i = 0; i<bmps.Length; i++)
            {
                _rects[i] = new Rectangle(0, (32+Padding)*i, _bmps[i].Width, _bmps[i].Height);
            }
        }

        public Rectangle[] Rectangle
        {
            get
            {
                Rectangle[] rs =
                {
                    new Rectangle(Location.X, Location.Y, DefaultWidth, DefaultHeight),
                    new Rectangle(Location.X, Location.Y+DefaultHeight+Padding, DefaultWidth, DefaultHeight),
                    new Rectangle(Location.X, Location.Y+DefaultHeight*2+Padding*2, DefaultWidth, DefaultHeight)
                };
                return rs;
            }
        }

        public void Paint(Graphics g)
        {
            // move 0,0 to out start location - no clipping is used, so we can draw anywhere on the parent control
            g.TranslateTransform(Location.X, Location.Y);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 0; i < _bmps.Length; i++)
            {
                using(var _brush = new SolidBrush(Color.Black))
                using (var _brushSelected = new SolidBrush(Color.DarkSlateGray))
                using (var _pen = new Pen(Color.WhiteSmoke, 1))
                {
                    if (IsSelected == i)
                        g.FillPie(_brushSelected, _rects[i], 0, 360);
                    else
                        g.FillPie(_brush, _rects[i], 0, 360);
                    g.DrawArc(_pen, _rects[i], 0, 360);
                    g.DrawImage(_bmps[i], _rects[i]);
                }
            }
        }
    }
}
