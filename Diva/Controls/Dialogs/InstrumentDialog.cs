using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls.Dialogs
{
    public class InstrumentDialog : Panel
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(Color.Red, 5) { Alignment = PenAlignment.Inset },new Rectangle(0, 0, Width, 50));

            e.Graphics.DrawRectangle(new Pen(Color.Black, 5) { Alignment = PenAlignment.Inset }, new Rectangle(0, 0, Width/3, 50));

        }

    }
}
