using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class DroneSettingInput : UserControl
	{
        private List<DroneSetting> DroneList =>
            ((Parent?.Parent?.Parent) as ConfigureForm)?.EditingDroneList;
        private bool NoTrigger = true;
        private static void SetEnabled(Button btn, bool enabled)
            => ConfigureForm.SetEnabled(btn, enabled);

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
                BtnDeaction.Visible = value != Mode.Empty;
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

        private void StartEdit(Mode m)
        {
            // freeze others
            foreach (var d in Parent.Controls.OfType<DroneSettingInput>())
            {
                if (d != this)
                {
                    SetEnabled(d.BtnAction, false);
                    SetEnabled(d.BtnDeaction, false);
                }
            }
            // freeze control panels
            foreach (var p in Parent.Parent.Controls.OfType<Panel>().Where(p => p != Parent))
            {
                p.Enabled = false;
                foreach (var b in p.Controls.OfType<Button>())
                    b.BackColor = Color.Gray;
            }
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
                foreach (var d in Parent.Controls.OfType<DroneSettingInput>())
                {
                    if (d != this)
                    {
                        SetEnabled(d.BtnAction, true);
                        SetEnabled(d.BtnDeaction, true);
                    }
                }
                foreach (var p in Parent.Parent.Controls.OfType<Panel>().Where(p => p != Parent))
                {
                    p.Enabled = true;
                    foreach (var b in p.Controls.OfType<Button>().Where(b => b.Enabled))
                        b.BackColor = Color.Black;
                }
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
                MessageBox.Show(Properties.Strings.DroneSetting_MsgDuplicatedName);
                TBoxComNo.Focus();
            } else if (!int.TryParse(TBoxPortValue.Text, out int value))
            {
                MessageBox.Show(Properties.Strings.DroneSetting_MsgDuplicatedName);
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
                if (CurMode == Mode.New)
                    DroneList?.Add(d);
                EditPanel.Visible = false;
                ok = true;
            }
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
			set { LabelDoneNameText.Text = value; }
			get { return LabelDoneNameText.Text; }
		}
        public string PortName
		{
			set { LabelPortNameText.Text = value; }
            get { return LabelPortNameText.Text; }
		}
		public string PortNumber
		{
			set { LabelPortNumberText.Text = value; }
			get { return LabelPortNumberText.Text; }
		}
		public string Baudrate
		{
			set { LabelBaudrateText.Text = value; }
			get { return LabelBaudrateText.Text; }
		}
		public string StreamURI
		{
			set { LabelStreamURIText.Text = value; }
			get { return LabelStreamURIText.Text; }
		}
        public DroneSetting Setting => new DroneSetting()
            {
                Name = DroneName ?? "",
                PortName = PortName ?? "",
                PortNumber = PortNumber ?? "",
                Baudrate = Baudrate ?? "",
                StreamURI = StreamURI ?? ""
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
        }

        public static DroneSettingInput FromSetting(DroneSetting s)
        {
            DroneSettingInput input = new DroneSettingInput
            {
                DroneName = s.Name,
                PortName = s.PortName,
                PortNumber = s.PortNumber,
                Baudrate = s.Baudrate,
                StreamURI = s.StreamURI
            };
            input.CurMode = s.Name == "" ? Mode.Empty : Mode.Normal;
            bool isUdp = s.PortName.Equals("udp", StringComparison.InvariantCultureIgnoreCase);
            input.LabelPortNumber.Visible = input.LabelPortNumberText.Visible = isUdp;
            input.LabelBaudrate.Visible = input.LabelBaudrateText.Visible = !isUdp;
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
                        Modified?.Invoke(this, null);
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
        #endregion
    }
}
