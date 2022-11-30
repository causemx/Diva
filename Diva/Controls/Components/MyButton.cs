using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Diva.Controls.Components
{
    public class MyButton : Button
    {
        private Image _NormalImage;
        public new Image Image
        {
            get => _NormalImage;
            set { _NormalImage = base.Image = value; }
        }

        private Color _ForeColor;
        public new Color ForeColor
        {
            get => _ForeColor;
            set { _ForeColor = base.ForeColor = value; }
        }

        private Color _BackColor;
        public new Color BackColor
        {
            get => _BackColor;
            set { _BackColor = base.BackColor = value; }
        }

        private Image _CheckedImage;
        public bool Checked { get; set; }
        [Browsable(true)]
        public Image CheckedImage
        {
            get => _CheckedImage;
            set
            {
                _CheckedImage = value;
                if (Checked)
                    base.Image = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Image CheckStateImage => Checked && CheckedImage != null ? CheckedImage : _NormalImage;

        [Browsable(true)]
        public Image HoverImage { get; set; }
        [Browsable(true)]
        public Color HoverForeColor { get; set; }
        [Browsable(true)]
        public Color HoverBackColor { get; set; }

        [Browsable(true)]
        public Image ClickImage { get; set; }
        [Browsable(true)]
        public Color ClickForeColor { get; set; }
        [Browsable(true)]
        public Color ClickBackColor { get; set; }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            if (HoverImage != null)
                base.Image = HoverImage;
            if (HoverBackColor.A != 0)
                base.BackColor = HoverBackColor;
            if (HoverForeColor.A != 0)
                base.ForeColor = HoverForeColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            base.Image = _NormalImage;
            base.ForeColor = _ForeColor;
            base.BackColor = _BackColor;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (ClickImage != null)
                base.Image = ClickImage;
            if (ClickBackColor.A != 0)
                base.BackColor = HoverBackColor;
            if (ClickForeColor.A != 0)
                base.ForeColor = HoverForeColor;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            base.Image = _NormalImage;
            base.ForeColor = _ForeColor;
            base.BackColor = _BackColor;
        }
    }

    public class IconButton : Button
    {
        //Fields
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color borderColor = Color.PaleVioletRed;

        //Properties
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        public Color TextColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }

        //Constructor
        public IconButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
        }

        //Methods
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);


            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) //Rounded button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    this.Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    //Button border                    
                    if (borderSize >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Normal button
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }
    }

    class MyTSRenderer : ToolStripProfessionalRenderer
    {
        public MyTSRenderer() { }
        
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            int d = 10;
            ToolStrip t = e.ToolStrip;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle bounds = e.AffectedBounds;
            LinearGradientBrush lgbrush = new LinearGradientBrush(
                new Point(0, 0), new Point(0, t.Height),
                Color.FromArgb(200, Color.FromArgb(48, 61, 69)), Color.FromArgb(200, Color.FromArgb(48, 61, 69)));
            
            GraphicsPath p = new GraphicsPath();
            Rectangle rect = new Rectangle(Point.Empty, t.Size);
            Rectangle arcRect = new Rectangle(rect.Location, new Size(d, d));

            p.AddLine(0, 0, 10, 0);
            arcRect.X = rect.Right - d;
            p.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - d;
            p.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;
            p.AddArc(arcRect, 90, 90);
            p.CloseFigure();
            t.Region = new Region(p);
            g.FillPath(lgbrush, p);

            // base.OnRenderToolStripBackground(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected)
            {
                base.OnRenderButtonBackground(e);
            }
            else
            {
                Brush brush = new SolidBrush(Color.FromArgb(64,255,255,255));
                Rectangle rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                e.Graphics.FillRectangle(brush, rectangle);
            }
        }

    }

    public class MyTSButton : ToolStripButton
    {
        private Image _NormalImage;
        public new Image Image
        {
            get => _NormalImage;
            set { _NormalImage = base.Image = value; }
        }

        private Color _ForeColor;
        public new Color ForeColor
        {
            get => _ForeColor;
            set { _ForeColor = base.ForeColor = value; }
        }

        private Color _BackColor;
        public new Color BackColor
        {
            get => _BackColor;
            set { _BackColor = base.BackColor = value; }
        }

        [Browsable(true)]
        public Image HoverImage { get; set; }
        [Browsable(true)]
        public Color HoverForeColor { get; set; }
        [Browsable(true)]
        public Color HoverBackColor { get; set; }

        [Browsable(true)]
        public Image ClickImage { get; set; }
        [Browsable(true)]
        public Color ClickForeColor { get; set; }
        [Browsable(true)]
        public Color ClickBackColor { get; set; }

        private Image _CheckedImage;
        private Color _CheckedForeColor;
        private Color _CheckedBackColor;
        private string _CheckedText;
        [Browsable(true)]
        public Image CheckedImage
        {
            get => _CheckedImage;
            set
            {
                _CheckedImage = value;
                if (Checked)
                    base.Image = value;
            }
        }
        [Browsable(true)]
        public Color CheckedForeColor
        {
            get => _CheckedForeColor;
            set
            {
                _CheckedForeColor = value;
                if (Checked)
                    base.ForeColor = value;
            }
        }
        [Browsable(true)]
        public Color CheckedBackColor
        {
            get => _CheckedBackColor;
            set
            {
                _CheckedBackColor = value;
                if (Checked)
                    base.BackColor = value;
            }
        }
        [Browsable(true), Localizable(true)]
        public string CheckedText
        {
            get => _CheckedText;
            set
            {
                _CheckedText = value;
                if (Checked)
                    base.Text = value;
            }
        }

        private string _Text;
        public new string Text
        {
            get => _Text;
            set { _Text = base.Text = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color CheckStateColor => Checked && CheckedForeColor.A != 0 ? CheckedForeColor : _ForeColor;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Color CheckStateBackColor => Checked && CheckedBackColor.A != 0 ? CheckedBackColor : _BackColor;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Image CheckStateImage => Checked && CheckedImage != null ? CheckedImage : _NormalImage;
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public String CheckStateText => Checked && !string.IsNullOrEmpty(CheckedText) ? CheckedText : _Text;

        private void SetCheckedState()
        {
            base.Image = CheckStateImage;
            base.ForeColor = CheckStateColor;
            base.BackColor = CheckStateBackColor;
            base.Text = CheckStateText;
        }

        public MyTSButton() : base()
        {
            SetCheckedState();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            if (HoverImage != null)
                base.Image = HoverImage;
            if (HoverBackColor.A != 0)
                base.BackColor = HoverBackColor;
            if (HoverForeColor.A != 0)
                base.ForeColor = HoverForeColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetCheckedState();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (ClickImage != null)
                base.Image = ClickImage;
            if (ClickBackColor.A != 0)
                base.BackColor = HoverBackColor;
            if (ClickForeColor.A != 0)
                base.ForeColor = HoverForeColor;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            SetCheckedState();
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            SetCheckedState();
        }
    }
}
