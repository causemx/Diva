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

	public delegate void MyHandler1(object sender, EventArgs e);


	public partial class MyGroupBoxPlus : UserControl
	{


		public event MyHandler1 Event1;

		public MyGroupBoxPlus()
		{
			InitializeComponent();

			ButEdit.Click += innerButton_Click;
			if (Event1 != null)
				ButEdit.Click += new EventHandler(Event1);
		}

		private void innerButton_Click(object sender, EventArgs e)
		{
			if (Event1!= null)
			{
				//Event1(this, e); // or possibly InnerButtonClick(innerButton, e); depending on what you want the sender to be
			}
			else
			{
				Console.WriteLine("failed");
			}
		}
	}
}
