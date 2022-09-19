using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls.Dialogs
{
    public partial class DialogEKF : Form
    {
        Mavlink.MavDrone drone = Planner.GetActiveDrone();

        public DialogEKF()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ekfvel.Value = (int)(drone.Status.ekfvelv * 100);
            ekfposh.Value = (int)(drone.Status.ekfposhor * 100);
            ekfposv.Value = (int)(drone.Status.ekfposvert * 100);
            ekfcompass.Value = (int)(drone.Status.ekfcompv * 100);
        }
    }
}
