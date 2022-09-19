using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
    public partial class CustomProgressBar : UserControl
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Timer t = new Timer();
        double pbUnit;
        int pbWidth = 0, pbHeight = 0;
        Bitmap bmp;
        Graphics g;

        int offset = 0;
        int _min = 0;
        int _max = 100;
        int _value = 0;
        public bool reverse = false;
        int displayValue = 0;
        bool _drawlabel = true;

        public bool DrawLabel
        {
            get
            {
                return _drawlabel;
            }
            set
            {
                _drawlabel = value;
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;
                _value = value;
                displayValue = _value;

                if (reverse)
                {
                    int dif = _value - Minimum;
                    _value = Maximum - dif;
                }

                int ans = _value + offset;
                if (ans <= _min)
                {
                    ans = _min + 1; // prevent an exception for the -1 hack
                }
                else if (ans >= _max)
                {
                    ans = _max;
                }

                _value = ans;

                Drawbl(_value);
                Invalidate();
            }
        }

        [System.ComponentModel.Browsable(true),
System.ComponentModel.Category("Mine"),
System.ComponentModel.Description("minimum value for display")]
        public int Minimum
        {
            get { return _min; }
            set
            {
                _min = value;
                if (_min < 0)
                {
                    _min = 0; offset = (_max - value) / 2; _max = _max - value;
                }
                else
                {
                    _min = value;
                }
                this.Invalidate();
            }
        }

        [System.ComponentModel.Browsable(true),
System.ComponentModel.Category("Mine"),
System.ComponentModel.Description("maximum value for display")]
        public int Maximum
        {
            get { return _max; }
            set
            {
                _max = value;
                this.Invalidate();
            }

        }

        readonly Font font = new Font("Arial", 12);

        public CustomProgressBar()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            pbWidth = picboxPB.Width;
            pbHeight = picboxPB.Height;
            pbUnit = (double)pbHeight / Maximum;
            bmp = new Bitmap(pbWidth, pbHeight);
        }


        public void Drawbl(int value)
        {
            if (bmp == null)
                return;

            g = Graphics.FromImage(bmp);
            g.Clear(Color.LightSkyBlue);
            // g.FillRectangle(Brushes.CornflowerBlue, new Rectangle(0, Height- (int)(value * pbUnit), pbWidth, (int)(value * pbUnit)));
            g.FillRectangle(Brushes.CornflowerBlue, 0, Height - (float)(value * pbUnit), pbWidth, (float)(value * pbUnit));
            g.DrawString(Value.ToString(), font, Brushes.Black, new PointF(pbWidth / 10, pbHeight / 2));
            picboxPB.Image = bmp;
        }

    }
}
