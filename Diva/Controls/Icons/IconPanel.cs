using Diva.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Controls.Icons
{
    public class IconPanel
    {
        private int _width;
        private int _height;
        private Point _location = new Point(0, 0);


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
            SolidBrush bgBrush = new SolidBrush(Color.Black);
            var rect = new Rectangle(0, 0, 50, 100);
            g.FillRectangle(bgBrush, rect);

            /*
            Bitmap bmp = new Bitmap(Resources.icon_airplane_32);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                using (SolidBrush myBrush = new SolidBrush(Color.Red))
                {
                    graphics.FillRectangle(myBrush, new Rectangle(0, 0, 40, 40));

                }
            }*/
        }
    }
}
