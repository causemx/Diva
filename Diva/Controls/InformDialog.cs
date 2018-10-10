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
	}
}
