using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooApplication.Controls
{
	public partial class MyProgressBar : UserControl
	{
		public int progress { get { return _progress; } set { _progress = value; }  }
		public string text { get { return _text; } set { _text = value; } }
		private String _text = "loading";
		private int _progress = 50;

		public MyProgressBar()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			e.Graphics.RotateTransform(-90);
			Pen pen1 = new Pen(Color.Red);
			Rectangle rect1 = new Rectangle(0 - this.Width / 2+20, 0 - this.Height / 2+20, this.Width-40, this.Height-40);
			e.Graphics.DrawPie(pen1, rect1, 0, (int)(progress*3.6)); // 0-100
			e.Graphics.FillPie(new SolidBrush(Color.Red), rect1, 0, (int)(progress*3.6));

			Pen pen2 = new Pen(Color.White);
			Rectangle rect2 = new Rectangle(0 - this.Width / 2 + 30, 0 - this.Height / 2 + 30, this.Width - 60, this.Height - 60);
			e.Graphics.DrawPie(pen2, rect2, 0, 360);
			e.Graphics.FillPie(new SolidBrush(Color.White), rect2, 0, 360);

			e.Graphics.RotateTransform(90);
			StringFormat ft = new StringFormat();
			ft.LineAlignment = StringAlignment.Center;
			ft.Alignment = StringAlignment.Center;
			e.Graphics.DrawString(text, new Font("Arial", 12), new SolidBrush(Color.Red), rect2, ft);

			this.Invalidate();
			
		}


	}
}
