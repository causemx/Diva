using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MavDrone = Diva.Mavlink.MavDrone;
using PLL = GMap.NET.PointLatLng;
using Diva.Utilities;

namespace Diva.Controls
{
    public partial class TrackerDialog : Form
    {
        private Bitmap PBoxBackground;
        private MavDrone Active;
        private List<MavDrone> Sources;

        public TrackerDialog(List<MavDrone> drones, MavDrone active)
        {
            InitializeComponent();
            Sources = drones.FindAll(d => d != active);
            Active = active;
        }

        public MavDrone Target => Sources[CBTrackingTarget.SelectedIndex];

        public double BearingAngle
        {
            get
            {
                double.TryParse(TBoxBearingAngle.Text, out double angle);
                return angle;
            }
        }

        public double Distance
        {
            get
            {
                double.TryParse(TBoxDistance.Text, out double distance);
                return distance;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UpdateRelativePosition()
        {
            if (RBtnTrackRelative.Checked)
            {
                PLL src = Sources[CBTrackingTarget.SelectedIndex].Status.Location;
                PLL cur = Active.Status.Location;
                TBoxBearingAngle.Text = src.BearingTo(cur).ToString();
                TBoxDistance.Text = src.DistanceTo(cur).ToString();
            }
        }

        private void LBTrackingTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRelativePosition();
        }

        private void TrackTypeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton == RBtnTrackClose && radioButton.Checked)
                    TBoxDistance.Text = "0";
                else if (radioButton == RBtnTrackRelative)
                    UpdateRelativePosition();
                TBoxBearingAngle.Enabled = RBtnCustomizedTrack.Checked;
                TBoxDistance.Enabled = RBtnCustomizedTrack.Checked;
            }
        }

        private void TrackParameter_TextChanged(object sender, EventArgs e)
        {
            DrawSchematic();
        }

        private void DrawSchematic()
        {
            var bmp = new Bitmap(PBoxBackground);
            float l = bmp.Width * 0.5f, r = l * 0.05f, R = l * 0.1f;
            using (var g = Graphics.FromImage(bmp))
            {
                var angle = BearingAngle * Math.PI / 180;
                bool zero = Distance < 0.001;
                g.TranslateTransform(l, l);
                l *= 0.9f;
                float x = zero ? 0 : (float)(Math.Sin(angle) * l) + 1f;
                float y = zero ? 0 : (float)(-Math.Cos(angle) * l) + 1f;
                using (var brush = new SolidBrush(Color.Red))
                    g.FillEllipse(brush, x - r, y - r, R, R);
            }
            var oldbmp = PBoxSchematic.Image;
            PBoxSchematic.Image = bmp;
            if (oldbmp != null)
                oldbmp.Dispose();
        }

        private void TrackerDialog_Shown(object sender, EventArgs e)
        {
            CBTrackingTarget.Items.Clear();
            Sources.ForEach(s => CBTrackingTarget.Items.Add(s.Name));
            CBTrackingTarget.SelectedIndex = 0;
            var angle = BearingAngle;
            if (PBoxBackground == null)
            {
                int L = PBoxSchematic.Width;
                PBoxBackground = new Bitmap(L, L);
                using (var g = Graphics.FromImage(PBoxBackground))
                {
                    float l = L / 2;
                    g.TranslateTransform(l, l);
                    using (var brush = new SolidBrush(Color.FromArgb(0)))
                        g.FillRectangle(brush, -l, -l, L, L);
                    using (var brush = new SolidBrush(Color.Black))
                        g.FillEllipse(brush, -l, -l, L, L);
                    float r = l * 0.1f;
                    using (var pen = new Pen(Color.White))
                        g.DrawEllipse(pen, -l + r, -l + r, L - 2 * r, L - 2 * r);
                    r = l * 0.075f;
                    using (var brush = new SolidBrush(Color.Yellow))
                        g.FillEllipse(brush, -r, -r, 2 * r, 2 * r);
                }
            }
            RBtnTrackClose.Checked = true;
            DrawSchematic();
        }

        public new void Dispose()
        {
            if (PBoxBackground != null)
            {
                PBoxBackground.Dispose();
                PBoxBackground = null;
            }
            if (PBoxSchematic.Image != null)
            {
                PBoxSchematic.Image.Dispose();
                PBoxSchematic.Image = null;
            }
            base.Dispose();
        }
    }
}
