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
	public partial class ProgressInputDialog : Form
	{
		public event EventHandler DoConfirm_Click;

		public string port_name
		{
			get { return txt_target.Text; }
		}

		public string port
		{
			get { return txt_port.Text; }
		}

		public string baudrate
		{
			get { return cb_baud.Text; }
		}


		public ProgressInputDialog()
		{
			InitializeComponent();
		}

		private void but_confirm_click(object sender, EventArgs e)
		{
			if (DoConfirm_Click != null)
			{
				DoConfirm_Click(this, e);
			}
		}

		private void but_cancel_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}
