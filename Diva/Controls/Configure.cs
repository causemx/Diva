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
			configGeoFence1.Enabled = false;
			configGeoFence1.Visible = false;
			configTuning1.Enabled = false;
			configTuning1.Visible = false;
		}

		private void MenuButton_Click(object sender, EventArgs e)
		{
			SidePanel.Height = ((Button)sender).Height;
			SidePanel.Top = ((Button)sender).Top;

			switch (((Button)sender).Name)
			{
				case "BtnVehicle":
					break;
				case "BtnGeoFence":
					break;
				case "BtnTuning":
					break;
				case "BtnAbout":
					break;
			}
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

		private void BtnVehicle_Click(object sender, EventArgs e)
		{
			SidePanel.Height = BtnVehicle.Height;
			SidePanel.Top = BtnVehicle.Top;
		}
	}
}
