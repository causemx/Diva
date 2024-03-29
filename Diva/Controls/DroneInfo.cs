﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Diva.Mavlink;
using Diva.Utilities;

namespace Diva.Controls
{
    public partial class DroneInfo : UserControl
    {
        public static readonly Color InactiveBGColor = Color.FromArgb(128, 128, 128);
        public static readonly Color ActiveBGColor = Color.FromArgb(37, 54, 98);
        public static readonly Color InactiveBGTOColor = Color.FromArgb(192, 128, 128);
        public static readonly Color ActiveBGTOColor = Color.FromArgb(165, 54, 98);
        public const int DroneInfoTimeout = 4;
        private Color BGColor => isActive ? ActiveBGColor : InactiveBGColor;

        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            private set
            {
                isActive = value;
                BackColor = BGColor;
                Parent?.Invalidate(Bounds, true);
            }
        }
        public MavDrone Drone { get; private set; }
        private bool IsLowVoltage => Drone.Status.BatteryVoltage != 0 && Drone.Status.BatteryLowVoltage > Drone.Status.BatteryVoltage;

        public DroneInfo(MavDrone m, string name)
        {
            InitializeComponent();
            TxtDroneName.Text = name;
            TxtSystemID.Text = (m.Status.FlightMode).ToString();
            Margin = new Padding(0);
            m.Status.GuidedMode.z = (float)DefaultValues.TakeoffHeight;
            Drone = m;
            if (MIRDCHelper.IsShip(name))
                PBDroneView.Image = Properties.Resources.boat_side;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void IsArming(bool arming) => labelIsArm.Text = arming ? "ARM" : "DisARM";
      

        public void UpdateTelemetryData(ToolTip toolTip)
        {
            if (Drone.Status.BatteryVoltage == 0)
            {
                TxtBatteryHealth.Text = "N/A";
                TxtBatteryHealth.ForeColor = Color.White;
                IconBattery.Image = Properties.Resources.icon_battery_normal;
            }
            else
            {
                TxtBatteryHealth.Text = Drone.Status.BatteryVoltage.ToString("F2") + 'V';
                if (IsLowVoltage)
                {
                    TxtBatteryHealth.ForeColor = Color.White;
                    IconBattery.Image = Properties.Resources.icon_battery_normal;
                }
                else
                {
                    TxtBatteryHealth.ForeColor = Color.Red;
                    IconBattery.Image = Properties.Resources.icon_battery_nopower;
                }
            }
            TxtCurrent.Text = Drone.Status.current.ToString("F2");
            TxtGPS.Text = Drone.Status.SatteliteCount.ToString() + "|" + Drone.Status.gpshdop.ToString();
            TxRcRSSI.Text = Drone.Status.rxrssi.ToString();

            if (Drone.Status.ArmedSince != null)
            {
                var elapsed = DateTime.Now - Drone.Status.ArmedSince.Value;
                if (elapsed < new TimeSpan(0, 1, 0))
                    TxtCurrent.Text = elapsed.Seconds + "s";
                else if (elapsed < new TimeSpan(1, 0, 0))
                    TxtCurrent.Text = elapsed.Minutes + "m" + elapsed.Seconds + "s";
                else
                {
                    int m = (int)elapsed.TotalMinutes;
                    TxtCurrent.Text = (m / 60) + "h" + (m % 60) + "m";
                }
            }
            else
                TxtCurrent.Text = "-";

            var now = DateTime.Now;
            var to = (int)(now - Drone.Status.LastPacket).TotalSeconds;
            var tts = toolTip.GetToolTip(this).Split('\n');
            Color c = BGColor;
            if (to > DroneInfoTimeout)
            {
                if (isActive)
                {
                    if (now.Second % 2 == 0)
                        c = ActiveBGTOColor;
                    toolTip.SetToolTip(this, toolTip.GetToolTip(this) + Properties.Strings.PacketLostForSeconds.FormatWith(to));
                }
                else
                {
                    if (now.Second % 2 == 1)
                        c = InactiveBGTOColor;
                    toolTip.SetToolTip(this, tts[0] + Properties.Strings.PacketLostForSeconds.FormatWith(to));
                }
            }
            else if (!isActive && tts.Length > 1)
                toolTip.SetToolTip(this, tts[0]);

            if (c != BackColor)
            {
                BackColor = c;
                Parent?.Invalidate(Bounds, true);
            }
        }

        private readonly Color EC_ColorNormal = Color.White;
        private readonly Color EC_ColorWarning = Color.Orange;
        private readonly Color EC_ColorError = Color.Red;

        public void NotifyMissionChanged() {}

        [Browsable(true)]
		public event EventHandler ToggleTelemetryInfo;
		private void BtnExpand_Click(object sender, EventArgs e)
		{
			ToggleTelemetryInfo?.Invoke(this, e);
		}

        public void SetExpanded(bool expanded) {
            BtnExpand.Image = expanded ? Properties.Resources.icon_btn_collapse
                : Properties.Resources.icon_btn_expand;
        }


        private void Pb_MouseHover(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;
            ToolTip tt = new ToolTip();
            
            switch (pb.Name)
            {
                case "IconTime":
                    tt.SetToolTip(pb, "Telemetry RSSI");
                    break;
                case "IconBattery":
                    tt.SetToolTip(pb, "Battery");
                    break;
                case "IconGPS":
                    tt.SetToolTip(pb, "GPS Status");
                    break;
                case "iconRC":
                    tt.SetToolTip(pb, "RC RSSI");
                    break;
            }
        }
    }
}
