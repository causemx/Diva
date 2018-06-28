using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva
{
    public partial class SplashForm : Form
    {
        public static bool InitOk { get; private set; } = false;
        Timer timer = null;

        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void SplashForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (timer != null) timer.Dispose();
        }

        #region debug config tests
        private void TestConfig()
        {
            string getAccountNames()
            {
                string names = "";
                foreach (var acc in AccountManager.GetAccounts())
                    names += acc + ", ";
                return names.Length > 0 ? names.Substring(0, names.Length - 2) : "";
            }
            void testAccount(string n, string p)
            {
                Console.WriteLine($"Test account '{n}' with password '{p}': " +
                    (AccountManager.VerifyAccount(n, p) ? "Succeeded." : "Failed."));
            }
            Console.WriteLine("Current account:" + getAccountNames() + ".");
            testAccount("aaa", "a123456");
            testAccount("aaa", "123456");
            bool b = AccountManager.CreateAccount("aaa", "a123456");
            Console.WriteLine("Create account aaa " + (b ? "succeded." : "failed"));
            Console.WriteLine("Current account:" + getAccountNames() + ".");
            ConfigData.Save();
            Console.WriteLine("Config data saved.");
            Console.WriteLine("Unlock: " + AccountManager.RetryUnlockTime);
            testAccount("aaa", "123456");
            testAccount("aaa", "a123456");
        }
        #endregion

        private bool LoginCheck()
        {
            if (AccountManager.GetLoginAccount() == "")
            {
                if (AccountManager.GetAccounts().Count() == 0)
                {
                    bool noAlert = false;
                    Boolean.TryParse(ConfigData.GetOption(ConfigData.OptionName.SkipNoAccountAlert), out noAlert);
                    if (noAlert) return true;
                    panelNewAccount.Visible = true;
                    lblProgress.Text = "Create account to protect your data.";
                } else
                {
                    panelLogin.Visible = true;
                    lblProgress.Text = "Please login to enter system.";
                }
                return false;
            }
            return true;
        }

        private void ActiveClose(bool ok = false)
        {
            FormClosing -= SplashForm_FormClosing;
            InitOk = ok;
            Close();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
           if (ConfigData.Ready)
            {
                //TestConfig();
                if (LoginCheck())
                {
                    if (timer != null) timer.Dispose();
                    lblProgress.Text = "Config loaded, starting...";
                    timer = new Timer();
                    timer.Tick += (o, ex) =>
                    {
                        timer.Stop();
                        ActiveClose(true);
                    };
                    timer.Interval = 2000;
                    timer.Start();
                }
                else
                    return;
            }
            else
            {
                ActiveClose();
            }
        }

        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            Tuple<Func<bool>, string> gen(Func<bool> c, string s) => Tuple.Create<Func<bool>, string>(c, s); 
            List<Tuple<Func<bool>, string>> checks = new List<Tuple<Func<bool>, string>>()
            {
                gen(() => AccountManager.AccountExist(tBoxUsername.Text), "Account already exists."),
                gen(() => !AccountManager.IsValidAccountName(tBoxUsername.Text), "Invalid account name."),
                gen(() => !AccountManager.IsValidPassword(tBoxPassword.Text), "Invalid password."),
                gen(() => tBoxPassword.Text != tBoxConfirm.Text, "Confirm password mismatch."),
            };
            var failed = checks.FirstOrDefault(c => c.Item1());
            if (failed != null)
            {
                lblNewAccountMessage.Text = failed.Item2;
                return;
            }
            bool result = AccountManager.CreateAccount(tBoxUsername.Text, tBoxPassword.Text);
            if (!result || !AccountManager.Login(tBoxUsername.Text, tBoxPassword.Text))
            {
                lblNewAccountMessage.Text = "Account creation failed. Try other name?";
                return;
            }
            ActiveClose(true);
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            if (cBoxDontNotify.Checked)
                ConfigData.SetOption(ConfigData.OptionName.SkipNoAccountAlert, true.ToString());
            ActiveClose(true);
        }

        private void UpdateLoginLock()
        {
            TimeSpan remaining = AccountManager.RetryUnlockTime - DateTime.Now;
            if (remaining.Ticks < 0)
            {
                timer.Stop();
                btnLogin.Enabled = true;
                lblLoginMessage.Text = "";
            } else
            {
                lblLoginMessage.Text = "Too many tries. Please login after " +
                    ((int)remaining.TotalSeconds) + " seconds.";
            }
        }

        private void CheckLoginLock()
        {
            if (AccountManager.IsAuthenticationLocked)
            {
                btnLogin.Enabled = false;
                if (timer != null) timer.Dispose();
                timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += (o, e) => Invoke((Action)UpdateLoginLock);
                timer.Start();
            }
        }

        private void panelLogin_VisibleChanged(object sender, EventArgs e)
        {
            if (panelLogin.Visible)
            {
                CheckLoginLock();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!AccountManager.IsValidAccountName(tBoxLoginUser.Text))
            {
                lblLoginMessage.Text = "Invalid account name.";
                return;
            }
            if (AccountManager.Login(tBoxLoginUser.Text, tBoxLoginPassword.Text))
            {
                ActiveClose(true);
                return;
            }
            lblLoginMessage.Text = "Login failed.";
            CheckLoginLock();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ActiveClose();
        }
    }
}
