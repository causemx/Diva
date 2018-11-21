using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class PowerModelInfo : UserControl
	{

		private double powerConsumption;

		public PowerModelInfo()
		{
			InitializeComponent();
		}

		public double PowerConsumption { get => powerConsumption;
			set
			{
				LBLConsumption.Text = value.ToString();
				powerConsumption = value;
			}
		}

		private void BTNClose_Click(object sender, EventArgs e)
		{
			this.Visible = !this.Visible;
		}
	}
}
