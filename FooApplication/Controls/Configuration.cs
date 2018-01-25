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
	public partial class Configuration : Form
	{
		public Configuration()
		{
			InitializeComponent();
			SidePanel.Height = button1.Height;
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SidePanel.Height = button1.Height;
			SidePanel.Top = button1.Top;
			controlTuning1.BringToFront();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			SidePanel.Height = button2.Height;
			SidePanel.Top = button2.Top;
			controlVersion1.BringToFront();
		}
	}
}
