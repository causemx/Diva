﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public class MyGroupBox : GroupBox
	{

		public Color TitleBackColor { get; set; }
		public HatchStyle TitleHatchStyle { get; set; }
		public Font TitleFont { get; set; }
		public Color TitleForeColor { get; set; }
		public int Radious { get; set; }
		public int TitlePadding { get; set; }

		public MyGroupBox()
		{
			this.DoubleBuffered = true;
			this.TitleBackColor = Color.Gray;
			this.TitleForeColor = Color.White;
			this.TitleFont = new Font(this.Font.FontFamily, Font.Size, FontStyle.Bold);
			this.BackColor = Color.Transparent;
			this.Radious = 1;
			this.TitleHatchStyle = HatchStyle.Percent60;
			this.TitlePadding = 5;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			GroupBoxRenderer.DrawParentBackground(e.Graphics, this.ClientRectangle, this);
			var rect = ClientRectangle;
			using (var path = GetRoundRectagle(this.ClientRectangle, Radious))
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				rect = new Rectangle(0, 0,
					rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
				if (this.BackColor != Color.Transparent)
					using (var brush = new SolidBrush(BackColor))
						e.Graphics.FillPath(brush, path);
				var clip = e.Graphics.ClipBounds;
				e.Graphics.SetClip(rect);
				using (var brush = new HatchBrush(TitleHatchStyle,
					TitleBackColor, ControlPaint.Light(TitleBackColor)))
					e.Graphics.FillPath(brush, path);
				using (var pen = new Pen(TitleBackColor, 1))
					e.Graphics.DrawPath(pen, path);
				
				TextRenderer.DrawText(e.Graphics, Text, TitleFont, new Point(TitlePadding, TitlePadding), TitleForeColor);
				e.Graphics.SetClip(clip);
				using (var pen = new Pen(TitleBackColor, 1))
					e.Graphics.DrawPath(pen, path);
			}

		}
		
		private GraphicsPath GetRoundRectagle(Rectangle b, int r)
		{
			GraphicsPath path = new GraphicsPath();
			path.AddArc(b.X, b.Y, r, r, 180, 90);
			path.AddArc(b.X + b.Width - r - 1, b.Y, r, r, 270, 90);
			path.AddArc(b.X + b.Width - r - 1, b.Y + b.Height - r - 1, r, r, 0, 90);
			path.AddArc(b.X, b.Y + b.Height - r - 1, r, r, 90, 90);
			path.CloseAllFigures();
			return path;
		}
	}
}
