using System;
using System.Text;
using System.Windows.Forms;
using Diva.Properties;

namespace Diva.Controls
{
	public partial class PowerModelInfo : UserControl
	{
		private double estimatedPower;
		private double availableCapacity = 0.0d;

		public PowerModelInfo()
		{
			InitializeComponent();
		}

		public double EstimatedPower
		{
			get => estimatedPower;
			set
			{
	
				LBLConsumption.Text = value.ToString();
				estimatedPower = value;
				
				double tmp = (estimatedPower / availableCapacity)*100;

				if (estimatedPower <= availableCapacity * 0.8)
				{
					PBHint.Image = Resources.icon_power_full_32;
					LBLDescription.Text = "Power Level: Full";
				}
				else if (estimatedPower > availableCapacity * 0.8 && estimatedPower < availableCapacity)
				{
					PBHint.Image = Resources.icon_power_medium_32;
					LBLDescription.Text = "Power Level: Medium";
				}
				else if (estimatedPower > availableCapacity)
				{
					PBHint.Image = Resources.icon_power_low_32;
					LBLDescription.Text = "Power Level: Low";
				}

				LBLDescriptionTip.Text = tmp.ToString("F2") + "% Energy you needed";
			}
		}

		public void UpdateConsumption(double batteryCapacity, int availablePercentage)
		{
			this.availableCapacity = batteryCapacity * availablePercentage / 100;
		}

		private void BTNClose_Click(object sender, EventArgs e)
		{
			this.Visible = !this.Visible;
		}
	}
}
