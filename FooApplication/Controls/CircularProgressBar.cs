using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooApplication.Controls
{
	public partial class CircularProgressBar : UserControl
	{
		int progress;

		public CircularProgressBar()
		{
			progress = 30;
			// InitializeComponent();
		}

		private void CircularProgressBar_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			e.Graphics.RotateTransform(-90);

			Pen pen1 = new Pen(Color.White);
			Rectangle rect1 = new Rectangle(0 - this.Width / 2 + 20, 0 - this.Height / 2 + 20, this.Width - 40, this.Height - 40);

			e.Graphics.DrawPie(pen1, rect1, 0, 360);
			e.Graphics.FillPie(new SolidBrush(Color.Black), rect1, 0, 360);

			e.Graphics.DrawPie(pen1, rect1, 0, (int)(this.progress * 3.6));
			e.Graphics.FillPie(new SolidBrush(Color.Red), rect1, 0, (int)(this.progress * 3.6));

			pen1 = new Pen(Color.White);
			rect1 = new Rectangle(0 - this.Width / 2 + 30, 0 - this.Height / 2 + 30, this.Width - 60, this.Height - 60);

			e.Graphics.DrawPie(pen1, rect1, 0, 360);
			e.Graphics.FillPie(new SolidBrush(Color.White), rect1, 0, 360);

			StringFormat stf = new StringFormat();
			stf.LineAlignment = StringAlignment.Center;
			stf.Alignment = StringAlignment.Center;
			stf.FormatFlags = StringFormatFlags.DirectionVertical;
			e.Graphics.DrawString(this.progress.ToString() + "ms", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), rect1, stf);

		}

		public void UpdateProgress(int progress)
		{
			this.progress = progress;
			this.Invalidate();
		}
	}
}
