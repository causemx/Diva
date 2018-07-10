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
    public partial class ConfigAccountPage : UserControl
    {
        private bool disableSelectionUpate;
        private string loginAccount;

        public ConfigAccountPage()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                loginAccount = AccountManager.GetLoginAccount();
        }

        private void InitAccountPage()
        {
            LabelLoginAccount.Text +=
                loginAccount == "" ? "(Anonymous)" : loginAccount;
            LoadAccount();
        }

        private void LoadAccount()
        {
            string acc = CBoxAccounts.Text;
            disableSelectionUpate = true;
            if (CBoxAccounts.Items.Count > 0)
                CBoxAccounts.Items.Clear();
            else
                acc = loginAccount;
            var accs = AccountManager.GetAccounts();
            foreach (string s in accs)
                CBoxAccounts.Items.Add(s);
            disableSelectionUpate = false;
            CBoxAccounts.Text = acc;
            UpdateAccountPanelControls();
        }

        private void UpdateAccountPanelControls()
        {
            bool accoutExist = CBoxAccounts.FindStringExact(CBoxAccounts.Text) >= 0;
            bool passwordConfirmed = TBoxPassword.Text != "" &&
                TBoxPassword.Text == TBoxConfirmPassword.Text &&
                AccountManager.IsValidPassword(TBoxPassword.Text);
            PanelExistingAccountControls.Visible = accoutExist;
            if (accoutExist)
            {
                ConfigureForm.SetEnabled(BtnDeleteAccount, CBoxAccounts.Text != AccountManager.GetLoginAccount());
                ConfigureForm.SetEnabled(BtnChangePassword, passwordConfirmed);
            }
            else
                ConfigureForm.SetEnabled(BtnCreateAccount, 
                    CBoxAccounts.Text != "" && passwordConfirmed);
        }

        private void PasswordInputUpdate(object o, EventArgs e) => UpdateAccountPanelControls();

        private void CBoxAccounts_TextUpdate(object sender, EventArgs e)
        {
            if (disableSelectionUpate) return;

            int i = CBoxAccounts.SelectedIndex;
            int n = CBoxAccounts.FindStringExact(CBoxAccounts.Text);
            if (i != n)
                CBoxAccounts.SelectedItem = CBoxAccounts.Text;
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            UpdateAccountPanelControls();
        }

        private void CBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            UpdateAccountPanelControls();
        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Create account '{CBoxAccounts.Text}'?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.CreateAccount(CBoxAccounts.Text, TBoxPassword.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            LoadAccount();
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Really delete account '{CBoxAccounts.Text}'?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.DeleteAccount(CBoxAccounts.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            LoadAccount();
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Chage password for account '{CBoxAccounts.Text}'?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.ChangePassword(CBoxAccounts.Text, TBoxPassword.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            UpdateAccountPanelControls();
        }
    }
}
