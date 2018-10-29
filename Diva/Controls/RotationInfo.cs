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
	public partial class RotationInfo : UserControl
	{

		public event EventHandler DoCancelHandler;

		public RotationInfo()
		{
			InitializeComponent();
		}

		public void Show(bool isShow)
		{
			this.Visible = isShow ? true : false;
		}

		public void Message(string text)
		{
			this.LBL_Message.Text = text;
		}

		private void BUT_Cancel_Click(object sender, EventArgs e)
		{
			DoCancelHandler?.Invoke(sender, e);
		}
	}
}
