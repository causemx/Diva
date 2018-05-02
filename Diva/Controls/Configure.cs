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
	public partial class Configure : Form
	{
		public Configure()
		{
			InitializeComponent();
			configGeoFence1.Enabled = true;
			configGeoFence1.Visible = true;
			configTuning1.Enabled = false;
			configTuning1.Visible = false;
		}

		private void BtnGeoFence_Click(object sender, EventArgs e)
		{
			configGeoFence1.Enabled = true;
			configGeoFence1.Visible = true;
			configTuning1.Enabled = false;
			configTuning1.Visible = false;

			configGeoFence1.Activate();
		}

		private void BtnTuning_Click(object sender, EventArgs e)
		{
			configGeoFence1.Enabled = false;
			configGeoFence1.Visible = false;
			configTuning1.Enabled = true;
			configTuning1.Visible = true;

			configTuning1.Activate();
		}
	}
}
