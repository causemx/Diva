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
	public partial class ConfigVehicle : UserControl
	{
		public ConfigVehicle()
		{
			InitializeComponent();

			MyHandler1 handler = new MyHandler1(OnHandler1);

			myGroupBoxPlus1.Event1 += handler;
		}

		public void OnHandler1(object sender, EventArgs e)
		{
			Console.WriteLine("???");
		}
	}
}
