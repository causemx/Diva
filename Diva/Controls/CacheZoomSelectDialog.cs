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
    public partial class CacheZoomSelectDialog : Form
    {
        public int Min { get => (int)NumZoomMin.Value; set => NumZoomMin.Value = value; }

        public int Max { get => (int)NumZoomMax.Value; set => NumZoomMax.Value = value; }

        public CacheZoomSelectDialog(int max)
        {
            InitializeComponent();
            NumZoomMin.Maximum = max;
            NumZoomMax.Value = NumZoomMax.Maximum = max;
        }

        private void NumZoomMin_ValueChanged(object sender, EventArgs e)
        {
            if (NumZoomMax.Value < NumZoomMin.Value)
                NumZoomMax.Value = NumZoomMin.Value;
        }

        private void NumZoomMax_ValueChanged(object sender, EventArgs e)
        {
            if (NumZoomMax.Value < NumZoomMin.Value)
                NumZoomMin.Value = NumZoomMax.Value;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
