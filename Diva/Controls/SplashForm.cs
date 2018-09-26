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
        }
    }
}
