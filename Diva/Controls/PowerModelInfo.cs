using System;
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

		public double EstimatedPower { get => estimatedPower;
			set
			{
				LBLConsumption.Text = value.ToString();
				estimatedPower = value;

				if (estimatedPower <= availableCapacity * 0.8)
				{
					PBHint.Image = Resources.icon_power_full_32;
				}
				else if (estimatedPower > availableCapacity * 0.8 && estimatedPower < availableCapacity)
				{
					PBHint.Image = Resources.icon_power_medium_32;
				}
				else if (estimatedPower > availableCapacity)
				{
					PBHint.Image = Resources.icon_power_low_32;
				}
			}
		}

		public void UpdateConsumption(double batteryCapacity, double availablePercentage)
		{
			this.availableCapacity = batteryCapacity * availableCapacity;
		}


		private void BTNClose_Click(object sender, EventArgs e)
		{
			this.Visible = !this.Visible;
		}
	}
}
