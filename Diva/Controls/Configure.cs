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
	
		}

		private void BtnTuning_Click(object sender, EventArgs e)
		{
			
		}

		private void BtnVehicle_Click(object sender, EventArgs e)
		{
			
		}
	}
}
