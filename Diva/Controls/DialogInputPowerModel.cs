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
	public partial class DialogInputPowerModel : Form
	{

		private string droneID;
		private double batteryCapacity;
		private double availableCapacity;

		public event EventHandler DoClickEvent;

		public string DroneID { get => LBLDroneID.Text; set => droneID = value; }
		public double BatteryCapacity {
			get => batteryCapacity;
			set
			{
				batteryCapacity = value;
				TXTBatteryCapacity.Text = batteryCapacity.ToString();
			}
		}
		public double AvailableCapacity {
			get => availableCapacity;
			set
			{
				availableCapacity = value;
				TXTAvailableCapacity.Text = AvailableCapacity.ToString();
			}
		}

		public DialogInputPowerModel()
		{
			InitializeComponent();
		}

		private void BTNConfirm_Click(object sender, EventArgs e)
		{
			DoClickEvent?.Invoke(sender, e);
		}

		private void BTNCancel_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}
