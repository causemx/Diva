using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooApplication.Controls
{
	public partial class ProgressInputDialog : Form
	{
		Planner mPlanner;

		public ProgressInputDialog(Planner parent)
		{
			InitializeComponent();
			this.mPlanner = parent;
		}

		private void but_confirm_Click(object sender, EventArgs e)
		{
			if (txt_target.Text != "" && txt_baud.Text != "")
			{
				mPlanner.AddItemtoConnectPannel(txt_target.Text, txt_baud.Text);
			}
			this.Dispose();
		}

		private void but_cancel_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}
