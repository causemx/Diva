﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Diva.Controls.Components
{
    class MyButton : Button
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

    class MyTSRenderer : ToolStripRenderer
    {
        public MyTSRenderer() { }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
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

        [Browsable(true)]
        public Image CheckedImage { get; set; }
        [Browsable(true)]
        public Color CheckedForeColor { get; set; }
        [Browsable(true)]
        public Color CheckedBackColor { get; set; }
        [Browsable(true)]
        public String CheckedText { get; set; }

        private string _Text;
        public new string Text
        {
            get => _Text;
            set { _Text = base.Text = value; }
        }

        public Color CheckStateColor => Checked ? CheckedForeColor : _ForeColor;
        public Color CheckStateBackColor => Checked ? CheckedBackColor : _BackColor;
        public Image CheckStateImage => Checked ? CheckedImage : _NormalImage;
        public String CheckStateText => Checked ? CheckedText : _Text;

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
