using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Diva.Controls.Components;

namespace Diva.Controls
{
	
	public partial class ConfigTuningPage : UserControl, IActivate
	{

		// from http://stackoverflow.com/questions/2512781/winforms-big-paragraph-tooltip/2512895#2512895

		private const int maximumSingleLineTooltipLength = 50;
		private static Hashtable tooltips = new Hashtable();
		private readonly Hashtable changes = new Hashtable();
		internal bool startup = true;
        private Mavlink.MavlinkInterface mav = Planner.GetActiveDrone();

        public ConfigTuningPage()
		{
			InitializeComponent();
		}

		public void Activate()
		{
            if (!mav.BaseStream.IsOpen)
			{
				Enabled = false;
				return;
			}

			startup = true;

			changes.Clear();

			myNumericUpDown1.setup(0, 0, 1, 0.001f, "WPNAV_LOIT_SPEED", mav.Status.param);
			myNumericUpDown2.setup(0, 0, 1, 0.001f, "WPNAV_RADIUS", mav.Status.param);
			myNumericUpDown3.setup(0, 0, 1, 0.001f, "WPNAV_SPEED", mav.Status.param);
			myNumericUpDown4.setup(0, 0, 1, 0.001f, "WPNAV_SPEED_DN", mav.Status.param);
			myNumericUpDown5.setup(0, 0, 1, 0.001f, "WPNAV_SPEED_UP", mav.Status.param);
		}

		internal void EEPROM_View_float_TextChanged(object sender, EventArgs e)
		{
			if (startup)
				return;

			float value = 0;
			var name = ((Control)sender).Name;

			// do domainupdown state check
			try
			{
				if (sender.GetType() == typeof(MyNumericUpDown))
				{
					value = ((MAVLinkParamChanged)e).value;
					changes[name] = value;
				}
				
				((Control)sender).BackColor = Color.Green;
			}
			catch (Exception)
			{
				((Control)sender).BackColor = Color.Red;
			}
		}
			private void BUT_writePIDS_Click(object sender, EventArgs e)
		{
			var temp = (Hashtable)changes.Clone();

			foreach (string value in temp.Keys)
			{
				try
				{
					if ((float)changes[value] > (float)mav.Status.param[value] * 2.0f)
						if (
							MessageBox.Show(value + " has more than doubled the last input. Are you sure?",
								"Large Value", MessageBoxButtons.YesNo) == DialogResult.No)
						{
							try
							{
								// set control as well
								var textControls = Controls.Find(value, true);
								if (textControls.Length > 0)
								{
									// restore old value
									textControls[0].Text = mav.Status.param[value].Value.ToString();
									textControls[0].BackColor = Color.FromArgb(0x43, 0x44, 0x45);
								}
							}
							catch
							{
							}
							return;
						}

					mav.setParam(value, (float)changes[value]);

					changes.Remove(value);

					try
					{
						// set control as well
						var textControls = Controls.Find(value, true);
						if (textControls.Length > 0)
						{
							textControls[0].BackColor = Color.FromArgb(0x43, 0x44, 0x45);
						}
					}
					catch
					{
					}
				}
				catch
				{
					MessageBox.Show(string.Format(Strings.ErrorSetValueFailed, value), Strings.ERROR);
				}
			}
		}

		/// <summary>
		///     Handles the Click event of the BUT_rerequestparams control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
		protected void BUT_rerequestparams_Click(object sender, EventArgs e)
		{
			if (!mav.BaseStream.IsOpen)
				return;

			((Control)sender).Enabled = false;

			try
			{
				mav.getParamList();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Errors while receiving parameters" + ex, Strings.ERROR);
			}


			((Control)sender).Enabled = true;


			Activate();
		}

		private void BUT_refreshpart_Click(object sender, EventArgs e)
		{
			if (!mav.BaseStream.IsOpen)
				return;

			((Control)sender).Enabled = false;


			updateparam(this);

			((Control)sender).Enabled = true;


			Activate();
		}

		private void updateparam(Control parentctl)
		{
			foreach (Control ctl in parentctl.Controls)
			{
				if (typeof(MyNumericUpDown) == ctl.GetType() || typeof(ComboBox) == ctl.GetType())
				{
					try
					{
						mav.GetParam(ctl.Name);
					}
					catch
					{
					}
				}

				if (ctl.Controls.Count > 0)
				{
					updateparam(ctl);
				}
			}
		}

		
		private void numeric_ValueUpdated(object sender, EventArgs e)
		{
			EEPROM_View_float_TextChanged(sender, e);
		}
	}
}
