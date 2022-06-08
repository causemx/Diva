using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Controls.Icons
{
    public abstract class Icon
    {
        private int _width = 32;
        private int _height = 32;
        private bool _isSelected = false;
        private Point _location = new Point(0, 0);

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        public Point Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public Rectangle Rectangle
        {
            get { return new Rectangle(Location.X, Location.Y, Width, Height); }
        }

        public void Paint(Graphics g)
        {
            // move 0,0 to out start location - no clipping is used, so we can draw anywhere on the parent control
            g.TranslateTransform(Location.X, Location.Y);
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, _width, _height);
             
            if (IsSelected)
                g.FillPie(new SolidBrush(Color.DarkSlateGray), rect, 0, 360);
            else
                g.FillPie(new SolidBrush(Color.Black), rect, 0, 360);
            g.DrawArc(new Pen(Color.WhiteSmoke, 1), rect, 0, 360);

            doPaint(g);
        }

        internal abstract void doPaint(Graphics g);
    }
}
