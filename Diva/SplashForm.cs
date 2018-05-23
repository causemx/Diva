﻿using System;
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

        private void SplashForm_Load(object sender, EventArgs e)
        {
           if (ConfigData.Ready)
            {
                timer = new Timer();
                timer.Tick += (o, ex) => {
                    timer.Stop();
                    FormClosing -= SplashForm_FormClosing;
                    Close();
                };
                timer.Interval = 5000;
                timer.Start();
            }
            else
            {
                MessageBox.Show("Cannot load config file.", "Error");
                Close();
            }
        }
    }
}
