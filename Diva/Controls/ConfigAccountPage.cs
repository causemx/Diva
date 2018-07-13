using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Diva.Controls
{
    public partial class ConfigAccountPage : UserControl
    {
        private bool disableSelectionUpate;
        private string loginAccount;

        // can be implemented with dynamic or dictionary but this should do
        private class UIStrings
        {
            public string anonymousAccount;
            public string confirmation;
            public string createAccountMessage;
            public string deleteAccountMessage;
            public string changePasswordMessage;
        }
        private UIStrings uiStrings = new UIStrings
        {
            anonymousAccount = "(Anonymous)",
            confirmation = "Confirmation",
            createAccountMessage = "Create account '{0}'?",
            deleteAccountMessage = "Delete account '{0}'?",
            changePasswordMessage = "Change password for account '{0}'?",
        };

        public ConfigAccountPage()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                LocaleUpdate();
        }

        public void LocaleUpdate()
        {
            // User control has no ide supported resx management
            ComponentResourceManager rm = new ComponentResourceManager(typeof(ConfigureForm));
            string resName(Control c) => $"ConfigAccountPage.{c.Name}.Text";
            string rmGetString(string name)
            {
                string res = null;
                try
                {
                    res = rm.GetString(name);
                }
                catch { }
                return res;
            }
            string locFontName = rmGetString("ConfigAccountPage.FontFamily");
            if (!float.TryParse(rmGetString("ConfigAccountPage.FontSizeAdjust"), out var locFontSizeAdjust))
                locFontSizeAdjust = 0f;
            if (locFontName != null)
            {
                foreach (Control c in Controls)
                {
                    string s = rmGetString(resName(c));
                    if (s != null)
                    {
                        c.Text = s;
                        c.Font = new Font(locFontName, c.Font.Size + locFontSizeAdjust);
                    }
                }
                var uistrs = typeof(UIStrings).GetFields();
                foreach (var f in uistrs)
                {
                    string s = rmGetString("ConfigAccountPage.UIStrings." + f.Name);
                    if (s != null)
                        f.SetValue(uiStrings, s);
                }
            }
            disableSelectionUpate = true;
            if (InvokeRequired)
                Invoke((MethodInvoker)InitAccountPage);
            else
                InitAccountPage();
            disableSelectionUpate = false;
        }

        private void InitAccountPage()
        {
            loginAccount = AccountManager.GetLoginAccount();
            LabelLoginAccount.Text +=
                loginAccount == "" ? uiStrings.anonymousAccount : loginAccount;
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
            if (MessageBox.Show(
                String.Format(uiStrings.createAccountMessage, CBoxAccounts.Text),
                uiStrings.confirmation, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.CreateAccount(CBoxAccounts.Text, TBoxPassword.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            LoadAccount();
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                String.Format(uiStrings.deleteAccountMessage, CBoxAccounts.Text),
                uiStrings.confirmation, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.DeleteAccount(CBoxAccounts.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            LoadAccount();
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                String.Format(uiStrings.changePasswordMessage, CBoxAccounts.Text),
                uiStrings.confirmation, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.ChangePassword(CBoxAccounts.Text, TBoxPassword.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            UpdateAccountPanelControls();
        }
    }
}
