using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.EnergyConsumption;

namespace Diva.Controls
{
	public partial class DroneSettingInput : UserControl
	{
        private ConfigureForm confForm => (Parent?.Parent?.Parent) as ConfigureForm;
        private List<DroneSetting> DroneList => confForm?.EditingDroneList;
        private bool NoTrigger = true;
        private static void SetEnabled(Button btn, bool enabled)
            => ConfigureForm.SetEnabled(btn, enabled);
        private void SetSettingsDirty() => Modified?.Invoke(this, null);

        #region Mode switching
        public enum Mode
        {
            Empty,
            New,
            Normal,
            Edit
        }
        private Mode mode;
        private Mode CurMode
        {
            get => mode;
            set
            {
                mode = value;
                ChkDroneNameText.Visible = BtnDeaction.Visible = value != Mode.Empty;
                switch (value)
                {
                    case Mode.Empty:
                        BtnAction.Text = Properties.Strings.DroneSettingInput_NewBtn_Text;
                        EndEdit();
                        break;
                    case Mode.Normal:
                        BtnAction.Text = Properties.Strings.DroneSettingInput_EditBtn_Text;
                        BtnDeaction.Text = Properties.Strings.DroneSettingInput_RemoveBtn_Text;
                        EndEdit();
                        break;
                    case Mode.New:
                    case Mode.Edit:
                        BtnAction.Text = Properties.Strings.DroneSettingInput_ApplyBtn_Text;
                        BtnDeaction.Text = Properties.Strings.DroneSettingInput_DiscardBtn_Text;
                        StartEdit(value);
                        break;
                }
            }
        }

        private void SetActionButtons(bool enable)
        {
            foreach (var d in Parent.Controls.OfType<DroneSettingInput>())
            {
                SetEnabled(d.BtnAction, enable || this == d);
                SetEnabled(d.BtnDeaction, enable || this == d);
            }
        }

        private void StartEdit(Mode m)
        {
            // freeze others
            SetActionButtons(false);
            // freeze control panels
            confForm?.FreezeVConfPanel(true);
            EditPanel.Visible = TBoxDroneName.Visible = true;
            if (m == Mode.Edit)
            {
                NoTrigger = true;
                TBoxDroneName.Text = DroneName;
                if (PortName.Equals("udp", StringComparison.CurrentCultureIgnoreCase))
                {
                    RBUdp.Checked = true;
                    TBoxComNo.Enabled = false;
                    TBoxPortValue.Text = PortNumber;
                } else
                {
                    RBSerial.Checked = true;
                    TBoxComNo.Enabled = true;
                    TBoxComNo.Text = PortName.Substring(3);
                    TBoxPortValue.Text = Baudrate;
                }
                TBoxStreamURI.Text = StreamURI;
                ComboPowerModel.SelectedItem = PowerModelName;
                NoTrigger = false;
            } else
            {
                TBoxDroneName.Text = TBoxComNo.Text = TBoxPortValue.Text = TBoxStreamURI.Text = "";
                RBUdp.Checked = true;
            }
            mode = m;
        }

        private void EndEdit()
        {
            // defreeze controls
            if (Parent != null)
            {
                SetActionButtons(true);
                confForm?.FreezeVConfPanel(false);
            }
            EditPanel.Visible = TBoxDroneName.Visible = false;
        }

        private bool ApplyEdit()
        {
            bool ok = false;
            string newName = TBoxDroneName.Text;
            if (newName == "")
            {
                MessageBox.Show(Properties.Strings.DroneSetting_MsgNoName);
            } else if (newName != DroneName &&
                (DroneList?.Exists(d => d.Name == newName) ?? false))
            {
                MessageBox.Show(Properties.Strings.DroneSetting_MsgDuplicatedName);
            } else if (!int.TryParse(TBoxComNo.Text, out int com) && RBSerial.Checked)
            {
                MessageBox.Show(Properties.Strings.DroneSetting_MsgValueInvalid);
                TBoxComNo.Focus();
            } else if (!int.TryParse(TBoxPortValue.Text, out int value))
            {
                MessageBox.Show(Properties.Strings.DroneSetting_MsgValueInvalid);
                TBoxPortValue.Focus();
            } else
            {
                DroneSetting d = DroneList?.Find(e => e.Name == DroneName) ?? new DroneSetting();
                DroneName = d.Name = newName;
                if (RBUdp.Checked)
                {
                    PortName = "udp";
                    PortNumber = d.PortNumber = value.ToString();
                } else
                {
                    PortName = "COM" + com.ToString();
                    Baudrate = d.Baudrate = value.ToString();
                }
                LabelPortNumber.Visible = LabelPortNumberText.Visible = RBUdp.Checked;
                LabelBaudrate.Visible = LabelBaudrateText.Visible = RBSerial.Checked;
                d.PortName = PortName;
                StreamURI = d.StreamURI = TBoxStreamURI.Text;
                d.PowerModel = PowerModelName = (string)ComboPowerModel.SelectedItem;
                if (CurMode == Mode.New)
                    DroneList?.Add(d);
                EditPanel.Visible = false;
                ok = true;
            }
            SetSettingsDirty();
            return ok;
        }
        #endregion

        #region public property
        public static EventHandler DefaultAdded;
        public static EventHandler DefaultModified;
        public static EventHandler DefaultRemoved;
        public static void SetDefaultHandlers(EventHandler added, EventHandler modified, EventHandler removed)
        { DefaultAdded = added; DefaultModified = modified; DefaultRemoved = removed; }
        public event EventHandler Added;
        public event EventHandler Modified;
        public event EventHandler Removed;

        public string DroneName
		{
            get => ChkDroneNameText.Text;
			set => ChkDroneNameText.Text = value;
		}
        public string PortName
		{
            get => LabelPortNameText.Text;
			set => LabelPortNameText.Text = value;
		}
		public string PortNumber
		{
            get => LabelPortNumberText.Text;
            set => LabelPortNumberText.Text = value;
		}
		public string Baudrate
		{
			get => LabelBaudrateText.Text;
			set => LabelBaudrateText.Text = value;
		}
		public string StreamURI
		{
            get => LabelStreamURIText.Text;
            set => LabelStreamURIText.Text = value;
		}
        public string PowerModelName
        {
            get => PowerModel.GetModel(LabelPowerModelText.Text).ModelName;
            set => LabelPowerModelText.Text = PowerModel.GetModel(value).ModelName;
        }
        public bool Checked
        {
            get => ChkDroneNameText.Checked;
            set => ChkDroneNameText.Checked = value;
		}
        public DroneSetting Setting => new DroneSetting()
        {
            Name = DroneName ?? "",
            PortName = PortName ?? "",
            PortNumber = PortNumber ?? "",
            Baudrate = Baudrate ?? "",
            StreamURI = StreamURI ?? "",
            Checked = Checked
        };
        #endregion

        public DroneSettingInput()
        {
            InitializeComponent();
            this.UpdateLocale();
            Added += DefaultAdded;
            Modified += DefaultModified;
            Removed += DefaultRemoved;
            CurMode = Mode.Empty;
            NoTrigger = false;
            ComboPowerModel.Items.AddRange(PowerModel.GetPowerModelNames().ToArray());
            PowerModelName = PowerModel.PowerModelNone.ModelName;
        }

        public static DroneSettingInput FromSetting(DroneSetting s)
        {
            DroneSettingInput input = new DroneSettingInput
            {
                DroneName = s.Name,
                PortName = s.PortName,
                PortNumber = s.PortNumber,
                Baudrate = s.Baudrate,
                StreamURI = s.StreamURI,
                Checked = s.Checked,
            };
            input.CurMode = s.Name == "" ? Mode.Empty : Mode.Normal;
            bool isUdp = s.PortName.Equals("udp", StringComparison.InvariantCultureIgnoreCase);
            input.LabelPortNumber.Visible = input.LabelPortNumberText.Visible = isUdp;
            input.LabelBaudrate.Visible = input.LabelBaudrateText.Visible = !isUdp;
            input.PowerModelName = s.PowerModel;
            return input;
        }

        #region hanndlers
        private void BtnAction_Click(object sender, EventArgs e)
		{
            switch (CurMode)
            {
                case Mode.Empty:
                    CurMode = Mode.New;
                    break;
                case Mode.New:
                    if (ApplyEdit())
                    {
                        CurMode = Mode.Normal;
                        Added?.Invoke(this, null);
                    }
                    break;
                case Mode.Normal:
                    CurMode = Mode.Edit;
                    break;
                case Mode.Edit:
                    if (ApplyEdit())
                    {
                        CurMode = Mode.Normal;
                        SetSettingsDirty();
                    }
                    break;
            }
		}

		private void BtnDeaction_Click(object sender, EventArgs e)
		{
            switch (CurMode)
            {
                case Mode.Empty:
                    // should not come here
                    break;
                case Mode.New:
                    CurMode = Mode.Empty;
                    break;
                case Mode.Normal:
                    // remove
                    DroneList?.Remove(DroneList.Find(d => d.Name == DroneName));
                    Removed?.Invoke(this, null);
                    break;
                case Mode.Edit:
                    CurMode = Mode.Normal;
                    break;
            }
		}

        private void RBPortType_CheckedChanged(object sender, EventArgs e)
        {
            if (NoTrigger || !(sender as RadioButton).Checked) return;
            bool isSerial = RBSerial.Checked;
            TBoxComNo.Enabled = isSerial;
            LabelBaudrate.Visible = isSerial;
            LabelPortNumber.Visible = !isSerial;
            if (isSerial) TBoxComNo.Focus();
        }

        private void ChkDroneNameText_CheckedChanged(object sender, EventArgs e)
        {
            var d = DroneList?.Find(n => n.Name == DroneName);
            if (d != null) d.Checked = Checked;
            SetSettingsDirty();
        }

        private void BtnNewModel_Click(object sender, EventArgs e)
        {
            var dlg = new NewPowerModelForm();
            if (dlg.ShowDialog() == DialogResult.Yes)
            {
                ComboPowerModel.SelectedItem = dlg.NewPowerModelName;
                foreach (var dsi in Parent.Controls.OfType<DroneSettingInput>())
                {
                    string pm = (string)dsi.ComboPowerModel.SelectedItem;
                    dsi.ComboPowerModel.Items.Clear();
                    dsi.ComboPowerModel.Items.AddRange(PowerModel.GetPowerModelNames().ToArray());
                    dsi.ComboPowerModel.SelectedItem = pm;
                }
            }
        }
        #endregion
    }
}
