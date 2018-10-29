using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class InformDialog : Form, IDialog
	{

		public event EventHandler DoCancelHandler;

		public InformDialog()
		{
			InitializeComponent();
		}

		public void Message(string message)
		{
			this.LBL_Message.Text = message;
		}

		public void Title(string title)
		{
			this.Text = title;
		}

		public void BUT_Cancel_Click(object s, EventArgs e)
		{
			if (DoCancelHandler != null)
				DoCancelHandler(s, e);
		}


/*
Constants in Windows API
0x84 = WM_NCHITTEST - Mouse Capture Test
0x1 = HTCLIENT - Application Client Area
0x2 = HTCAPTION - Application Title Bar

This function intercepts all the commands sent to the application. 
It checks to see of the message is a mouse click in the application. 
It passes the action to the base action by default. It reassigns 
the action to the title bar if it occured in the client area
to allow the drag and move behavior.
*/
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x84:
					base.WndProc(ref m);
					if ((int)m.Result == 0x1)
						m.Result = (IntPtr)0x2;
					return;
			}

			base.WndProc(ref m);
		}
	}
}
