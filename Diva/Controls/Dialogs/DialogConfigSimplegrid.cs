using Diva.Utilities;
using GMap.NET;
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
    public partial class DialogConfigSimplegrid : Form
    {
        
        private PointLatLng _hl;
        private Grid _grid;

        public DialogConfigSimplegrid(Grid grid, PointLatLng homeLocation)
        {
            InitializeComponent();
            this._hl = homeLocation;
            this._grid = grid;
        }

        public void buttonAccept_Click(object sender, EventArgs e)
        {
            AddGridWPsToMap(_grid, Grid.ScanMode.Survey);
            Dispose();
        }

        public async void AddGridWPsToMap(Grid grid, Grid.ScanMode scanMode)
        {
            await grid.Accept(
                scanMode,
                (double)altitudeNumericUpDown.Value,
                (double)distanceNumericUpDown.Value,
                (double)spacingNumericUpDown.Value,
                (double)angleNumericUpDown.Value,
                Grid.StartPosition.Home,
                _hl
            );
        }
    }
}
