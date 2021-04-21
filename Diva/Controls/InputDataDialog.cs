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
		public string Value { get => Tbox_Value.Text; set => Tbox_Value.Text = value; }
		public string Hint { get => Lbl_Hint.Text; set => Lbl_Hint.Text = value; }
		public string Unit { get => Lbl_Unit.Text; set => Lbl_Unit.Text = value; }
        public bool Required { get => !Btn_Cancel.Visible; set => Btn_Cancel.Visible = !value; }

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
			Close();
		}

		private void Btn_Cancel_Click(object sender, EventArgs e)
		{
            Close();
		}

        private void Tbox_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoClick(sender, e);
                Close();
            }
        }
    }
}
