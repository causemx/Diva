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
	public partial class Configure : Form
	{
		public Configure()
		{
			InitializeComponent();

            string acc = AccountManager.GetLoginAccount();
            var accs = AccountManager.GetAccounts();
            if (acc == "" || accs.Count() == 0)
            {
                BtnAccount.Visible = false;
                BtnAbout.Top = BtnAccount.Top;
            } else
            {
                foreach (string s in accs)
                    CBoxAccounts.Items.Add(s);
                CBoxAccounts.Text = acc;
            }
        }

		private void MenuButton_Click(object sender, EventArgs e)
		{
            var btn = sender as Button;

            if (btn == null || btn.Top == IndicatorPanel.Top)
                return;

			IndicatorPanel.Height = btn.Height;
			IndicatorPanel.Top = btn.Top;
            PanelAccountConfig.Visible = false;

			switch (((Button)sender).Name)
			{
				case "BtnVehicle":
					break;
				case "BtnGeoFence":
					break;
				case "BtnTuning":
					break;
                case "BtnAccount":
                    PanelAccountConfig.Visible = true;
                    break;
                case "BtnAbout":
					break;
			}
		}

        private void cBoxAccounts_TextUpdate(object sender, EventArgs e)
        {
            int i = CBoxAccounts.SelectedIndex;
            int n = CBoxAccounts.FindStringExact(CBoxAccounts.Text);
            BtnCreateAccount.Visible = n >= 0;
            if (i != n)
            {
                CBoxAccounts.SelectedItem = CBoxAccounts.Text;
            }
        }

        private void cBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBoxAccounts.SelectedIndex >= 0)
            {
                CBoxAccounts.Text = CBoxAccounts.SelectedItem as string;
                BtnCreateAccount.Enabled = CBoxAccounts.Text != AccountManager.GetLoginAccount();
                BtnCreateAccount.BackColor = BtnCreateAccount.Enabled ? SystemColors.InactiveCaptionText : Color.Gray;
            }
        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {

        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {

        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {

        }
    }
}
