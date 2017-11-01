using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooApplication.Controls
{
	class MyButton : Control
	{
		private SolidBrush borderBrush, textBrush;
		private Rectangle borderRectangle;
		private bool active = false;
		private StringFormat sf = new StringFormat();

		public override Cursor Cursor { get; set; } = Cursors.Hand;
		public float BorderThickness { get; set; } = 2;

		public MyButton()
		{
			borderBrush = new SolidBrush(ColorTranslator.FromHtml("#31302b"));
			textBrush = new SolidBrush(ColorTranslator.FromHtml("#FFF"));

			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;

			this.Paint += MyButton_Paint;
		}

		private void MyButton_Paint(object sender, PaintEventArgs e)
		{
			borderRectangle = new Rectangle(0, 0, Width, Height);
			e.Graphics.DrawRectangle(new Pen(borderBrush, BorderThickness), borderRectangle);
			e.Graphics.DrawString(this.Text, this.Font, (active) ? textBrush : borderBrush, borderRectangle, sf);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			base.BackColor = ColorTranslator.FromHtml("#31302b");
			active = true;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			base.BackColor = ColorTranslator.FromHtml("#FFF");
			active = false;
		}

	}
}
