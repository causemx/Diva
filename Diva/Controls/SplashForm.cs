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
        private Timer timer = null;
        private ComponentResourceManager FormResx = new ComponentResourceManager(typeof(SplashForm));

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
            if (!Planner.MIRDCMode &&
                string.IsNullOrEmpty(AccountManager.GetLoginAccount()))
            {
                if (AccountManager.GetAccounts().Count() == 0)
                {
                    if (ConfigData.GetBoolOption(ConfigData.OptionName.ForceAccountLogin))
                    {
                        cBoxDontNotify.Visible = false;
                        btnSkip.Text = btnExit.Text;
                        btnSkip.Click -= btnSkip_Click;
                        btnSkip.Click += btnExit_Click;
                    }
                    else if (ConfigData.GetBoolOption(ConfigData.OptionName.SkipNoAccountAlert))
                        return true;
                    panelNewAccount.Visible = true;
                    lblProgress.Text = FormResx.GetString("CreateAccountNotification");
                } else
                {
                    panelLogin.Visible = true;
                    lblProgress.Text = FormResx.GetString("LoginNotification");
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
                    lblProgress.Text = FormResx.GetString("SplashCountDownNotification");
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
            Tuple<Func<bool>, string> gen(Func<bool> c, string s) => Tuple.Create(c, s); 
            List<Tuple<Func<bool>, string>> checks = new List<Tuple<Func<bool>, string>>()
            {
                gen(() => AccountManager.AccountExist(tBoxUsername.Text), FormResx.GetString("AccountAlreadyExistMessage")),
                gen(() => !AccountManager.IsValidAccountName(tBoxUsername.Text), FormResx.GetString("InvalidNameMessage")),
                gen(() => !AccountManager.IsValidPassword(tBoxPassword.Text), FormResx.GetString("InvalidPasswordMessage")),
                gen(() => tBoxPassword.Text != tBoxConfirm.Text, FormResx.GetString("PasswordMismatchMessage")),
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
                lblNewAccountMessage.Text = FormResx.GetString("AccountCreationFailedMessage");
                return;
            }
            ActiveClose(true);
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            if (cBoxDontNotify.Checked)
                ConfigData.SetBoolOption(ConfigData.OptionName.SkipNoAccountAlert, true);
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
            }
            else
            {
                lblLoginMessage.Text = String.Format(
                    FormResx.GetString("TooManyTriesMessageFormat"),
                    (int)remaining.TotalSeconds);
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
            if (AccountManager.Login(tBoxLoginUser.Text, tBoxLoginPassword.Text))
            {
                ActiveClose(true);
                return;
            }
            lblLoginMessage.Text = FormResx.GetString("LoginFailedMessage");
            CheckLoginLock();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ActiveClose();
        }
    }
}
