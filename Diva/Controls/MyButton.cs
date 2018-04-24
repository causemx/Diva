using System.Drawing;
using System.Windows.Forms;

namespace FooApplication.Controls
{
	public class MyButton : Control
	{
		public SolidBrush BorderBrush, TextBrush;
		public Rectangle BorderRectangle;
		private bool _isActive;
		public StringFormat MyStringFormat = new StringFormat();

		public override Cursor Cursor { get; set; } = Cursors.Hand;
		public float BorderThickness { get; set; } = 2;

		public MyButton()
		{
			BorderBrush = new SolidBrush(ColorTranslator.FromHtml("#31302b"));
			TextBrush = new SolidBrush(ColorTranslator.FromHtml("#FFF"));

			MyStringFormat.Alignment = StringAlignment.Center;
			MyStringFormat.LineAlignment = StringAlignment.Center;

			Paint += MyButton_Paint;
		}

		private void MyButton_Paint(object sender, PaintEventArgs e)
		{
			BorderRectangle = new Rectangle(0, 0, Width, Height);
			e.Graphics.DrawRectangle(new Pen(BorderBrush, BorderThickness), BorderRectangle);
			e.Graphics.DrawString(Text, Font, (_isActive) ? TextBrush : BorderBrush, BorderRectangle, MyStringFormat);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			BackColor = ColorTranslator.FromHtml("#31302b");
			_isActive = true;
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// MyButton
			// 
			this.Name = "MyButton";
			this.Size = new System.Drawing.Size(100, 50);
			this.ResumeLayout(false);

		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			BackColor = ColorTranslator.FromHtml("#FFF");
			_isActive = false;
		}

	}
}
