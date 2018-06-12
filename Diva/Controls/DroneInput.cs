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
	public partial class DroneInput : UserControl
	{

		public event EventHandler ButtonRemoveClick;

		public DroneInput()
		{
			InitializeComponent();
		}

		public string Name
		{
			set { TxtName.Text = value; }
			get { return TxtName.Text; }
		}

		public string PortName
		{
			set { TxtPortName.Text = value; }
			get { return TxtPortName.Text; }
		}

		public string PortNumber
		{
			set { TxtPortNumber.Text = value; }
			get { return TxtPortNumber.Text; }
		}

		public string Baudrate
		{
			set { TxtBaudrate.Text = value; }
			get { return TxtBaudrate.Text; }
		}

		public string Streaming
		{
			set { TxtStreaming.Text = value; }
			get { return TxtStreaming.Text; }
		}

		private void BtnEdit_Click(object sender, EventArgs e)
		{
			ProgressInputDialog dialog = new ProgressInputDialog()
			{
				Text = "Parameters"
			};

			dialog.DoConfirm_Click += delegate (object obj, EventArgs e2)
			{

				TxtPortName.Text = dialog.port_name;
				TxtPortNumber.Text = dialog.port_number;
				TxtBaudrate.Text = dialog.baudrate;
				TxtStreaming.Text = dialog.streaming;

				dialog.Close();
			};

			dialog.Show();
		}

		private void BtnRemove_Click(object sender, EventArgs e)
		{
			if (this.ButtonRemoveClick != null)
				this.ButtonRemoveClick(this, e);
		}
	}
}
