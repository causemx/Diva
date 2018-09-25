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
	public partial class InputDataDialog : Form
	{

		private string value;
		private string hint;
		private string unit;


		public string Value { get => Tbox_Value.Text; set => this.value = value; }
		public string Hint { get => hint; set { hint = value; Lbl_Hint.Text = value; } }
		public string Unit { get => unit; set { unit = value; Lbl_Unit.Text = value; } }
		

		public EventHandler DoClick;
		// public delegate void UserControlClickHandler(object sender, EventArgs e);


		public InputDataDialog()
		{
			InitializeComponent();
			StartPosition = FormStartPosition.CenterScreen;
		}
		
		private void Btn_Confirm_Click(object sender, EventArgs e)
		{
			this.DoClick(sender, e);
			Dispose();
		}

		private void Btn_Cancel_Click(object sender, EventArgs e)
		{
			Dispose();
		}
	}
}
