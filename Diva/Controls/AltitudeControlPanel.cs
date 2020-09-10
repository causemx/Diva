using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
    public partial class AltitudeControlPanel : UserControl
    {
        private Brush AboveBrush = Brushes.SkyBlue;
        private Brush BelowBrush = Brushes.Navy;
        private Brush PointingBrush = Brushes.Red;
        private Pen PointingPen = Pens.Red;
        private Pen TargetPen = Pens.Yellow;
        private Brush TextBrush = SystemBrushes.ControlText;
        private Size normalSize, hoverSize;

        [Browsable(true)]
        public Color AboveColor { get; set; } = Color.SkyBlue;
        [Browsable(true)]
        public Color BelowColor { get; set; } = Color.Navy;
        [Browsable(true)]
        public Color TargetColor { get; set; } = Color.Yellow;
        [Browsable(true)]
        public Color PointingColor { get; set; } = Color.Red;
        [Browsable(true)]
        public float Minimum { get; set; } = 10.0f;
        [Browsable(true)]
        public float Maximum { get; set; } = 120.0f;
        [Browsable(true)]
        public new Size Size
        {
            get => normalSize;
            set
            {
                normalSize = value;
                if (!IsMouseOver)
                    base.Size = value;
            }
        }
        [Browsable(true)]
        public Size HoverSize
        {
            get => hoverSize;
            set
            {
                hoverSize = value;
                if (IsMouseOver)
                    base.Size = value;
            }
        }
        [Browsable(true)]
        public ContentAlignment HoverGrow { get; set; } = ContentAlignment.TopRight;

        private float target, value;
        private bool targeting;
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                InvokeInvalidate();
            }
        }
        public bool Targeting
        {
            get => targeting;
            set
            {
                targeting = value;
                InvokeInvalidate();
            }
        }
        public float Target
        {
            get => target;
            set
            {
                target = value;
                InvokeInvalidate();
            }
        }

        public Point MousePos => PointToClient(Cursor.Position);
        public bool IsMouseOver
        {
            get
            {
                var pos = MousePos;
                return pos.X >= 0 && pos.X <= Width && pos.Y >= 0 && pos.Y <= Height;
            }
        }

        public float PointValue
        {
            get
            {
                var pos = MousePos;
                if (pos.X < 0 && pos.X > Width && pos.Y < 0 && pos.Y > Height)
                    return 0;
                int cw = ClientSize.Width - Padding.Left - Padding.Right;
                int ch = ClientSize.Height - Padding.Top - Padding.Bottom;
                float y = pos.Y;
                if (y < Padding.Top)
                    y = Padding.Top;
                if (y > ClientSize.Height - Padding.Bottom - 1)
                    y = ClientSize.Height - Padding.Bottom - 1;
                return (Maximum + (Minimum - Maximum) * (y - Padding.Top) / (ch - 1));
            }
        }

        public AltitudeControlPanel()
        {
            InitializeComponent();
            Visible = false;
        }

        public void SetSource(Diva.Mavlink.MavDrone drone)
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                Value = drone?.Status.Altitude ?? 0;
                Target = Diva.Mission.AltitudeControl.TargetAltitudes[drone];
                Targeting = Diva.Mission.AltitudeControl.Has(drone);
                Visible = Value != 0;
            }));
        }

        public void ClearSource()
        {
            BeginInvoke((MethodInvoker)(() =>
            {
                Targeting = false;
                Value = 0;
                Visible = false;
            }));
        }

        private void InvokeInvalidate()
        {
            if (IsHandleCreated)
                BeginInvoke((MethodInvoker)Invalidate);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Value == 0) return; // no active drone

            var ptr = PointToClient(Cursor.Position);
            bool mouseover = ptr.X >= 0 && ptr.X <= ClientSize.Width
                && ptr.Y >= 0 && ptr.Y <= ClientSize.Height;

            int cw = ClientSize.Width - Padding.Left - Padding.Right;
            int ch = ClientSize.Height - Padding.Top - Padding.Bottom;

            var g = e.Graphics;
            float scale = (ch - 1) / (Minimum - Maximum);
            g.SetClip(new Rectangle(Padding.Left, Padding.Top, cw, ch));
            g.TranslateTransform(Padding.Left, Padding.Top);
            g.ScaleTransform(1, scale);
            g.TranslateTransform(0, -Maximum);

            if (((SolidBrush)AboveBrush).Color != AboveColor)
                AboveBrush = new SolidBrush(AboveColor);
            g.FillRectangle(AboveBrush, 0, Value, cw, Maximum - Value);
            if (((SolidBrush)BelowBrush).Color != BelowColor)
                BelowBrush = new SolidBrush(BelowColor);
            g.FillRectangle(BelowBrush, 0, 0, cw, Value);
            if (Targeting)
            {
                if (TargetPen.Color != TargetColor)
                    TargetPen = new Pen(TargetColor);
                g.DrawLine(TargetPen, 0, Target, cw, Target);
            }
            g.ResetTransform();

            var str = Value.ToString("F1");
            Brush brush = null;
            if (mouseover)
            {
                if (PointingPen.Color != PointingColor)
                {
                    PointingPen = new Pen(PointingColor);
                    PointingBrush = new SolidBrush(PointingColor);
                }
                float y = ptr.Y;
                if (y < Padding.Top)
                    y = Padding.Top;
                if (y > ClientSize.Height - Padding.Bottom - 1)
                    y = ClientSize.Height - Padding.Bottom - 1;
                g.DrawLine(PointingPen, Padding.Left, y, cw, y);
                str = (Maximum + (y - Padding.Top) / scale).ToString("F1");
                brush = PointingBrush;
            }
            else
            {
                if (((SolidBrush)TextBrush).Color != ForeColor)
                    TextBrush = new SolidBrush(ForeColor);
                brush = TextBrush;
            }
            float sz = Font.Size;
            SizeF ssize;
            Font f = null;
            do
            {
                f?.Dispose();
                f = new Font(Font.FontFamily, sz, Font.Style);
                ssize = g.MeasureString(str, f);
                sz -= sz > 10 ? 1 : 0.5f;
            } while (ssize.Width > Width);

            g.ResetClip();
            g.DrawString(str, f, brush,
                (ClientSize.Width - ssize.Width) / 2,
                ClientSize.Height - ssize.Height - Padding.Bottom);
            f.Dispose();
        }

        private void HoverResize(Size size)
        {
            if (base.Size != size)
            {
                var diff = size - base.Size;
                switch (HoverGrow)
                {
                    case ContentAlignment.BottomCenter:
                        Left -= diff.Width / 2;
                        break;
                    case ContentAlignment.BottomLeft:
                        Left -= diff.Width;
                        break;
                    case ContentAlignment.BottomRight:
                        break;
                    case ContentAlignment.MiddleCenter:
                        Top -= diff.Height / 2;
                        Left -= diff.Width / 2;
                        break;
                    case ContentAlignment.MiddleLeft:
                        Top -= diff.Height / 2;
                        Left -= diff.Width;
                        break;
                    case ContentAlignment.MiddleRight:
                        Top -= diff.Height / 2;
                        break;
                    case ContentAlignment.TopCenter:
                        Top -= diff.Height;
                        Left -= diff.Width / 2;
                        break;
                    case ContentAlignment.TopLeft:
                        Top -= diff.Height;
                        Left -= diff.Width;
                        break;
                    case ContentAlignment.TopRight:
                        Top -= diff.Height;
                        break;
                }
                base.Size = size;
            }
        }

        private void AltitudeControlPanel_MouseLeave(object sender, EventArgs e)
        {
            HoverResize(Size);
            Invalidate();
        }

        private void AltitudeControlPanel_MouseMove(object sender, MouseEventArgs e)
        {
            HoverResize(HoverSize);
            Invalidate();
        }
    }
}
