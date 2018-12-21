using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Properties;

namespace Diva.Controls.Components
{
	public class MyCheckBox : CheckBox
	{

		public new event EventHandler CheckedChanged;

		[System.ComponentModel.Browsable(true)]
		public double OnValue { get; set; }

		[System.ComponentModel.Browsable(true)]
		public double OffValue { get; set; }

		[System.ComponentModel.Browsable(true)]
		public string ParamName { get; set; }

		Control _control;


		public MyCheckBox()
		{
			OnValue = 1;
			OffValue = 0;
			this.Enabled = false;

			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			Padding = new Padding(3);
		}

		public void setup(double[] OnValue, double[] OffValue, string[] paramname, MAVLink.MAVLinkParamList paramlist,
			Control enabledisable = null)
		{
			int idx = 0;
			foreach (var s in paramname)
			{
				if (paramlist.ContainsKey(s))
				{
					setup(OnValue[idx], OffValue[idx], s, paramlist, enabledisable);
					return;
				}
				idx++;
			}
		}

		public void setup(double OnValue, double OffValue, string[] paramname, MAVLink.MAVLinkParamList paramlist,
			Control enabledisable = null)
		{
			foreach (var s in paramname)
			{
				if (paramlist.ContainsKey(s))
				{
					setup(OnValue, OffValue, s, paramlist, enabledisable);
					return;
				}
			}
		}

		public void setup(double OnValue, double OffValue, string paramname, MAVLink.MAVLinkParamList paramlist,
			Control enabledisable = null)
		{
			base.CheckedChanged -= MyCheckBox_CheckedChanged;

			this.OnValue = OnValue;
			this.OffValue = OffValue;
			this.ParamName = paramname;
			this._control = enabledisable;

			if (paramlist.ContainsKey(paramname))
			{
				this.Enabled = true;
				this.Visible = true;

				if (paramlist[paramname].Value == OnValue)
				{
					this.Checked = true;
					enableControl(true);
				}
				else if (paramlist[paramname].Value == OffValue)
				{
					this.Checked = false;
					enableControl(false);
				}
				else
				{
					this.CheckState = System.Windows.Forms.CheckState.Indeterminate;
					enableControl(false);
				}
			}
			else
			{
				this.Enabled = false;
			}

			base.CheckedChanged += new EventHandler(MyCheckBox_CheckedChanged);
		}

		public void enableControl(bool enable)
		{
			if (_control != null)
				_control.Enabled = enable;
		}

		public void MyCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (this.CheckedChanged != null)
				this.CheckedChanged(sender, e);

			if (this.Checked)
			{
				enableControl(true);
				try
				{
					bool ans = Planner.GetActiveDrone().SetParam(ParamName, OnValue);
					if (ans == false)
						MessageBox.Show(String.Format(Strings.MsgInvalidEntry, ParamName), Strings.DialogTitleError);
				}
				catch
				{
					MessageBox.Show(String.Format(Strings.MsgInvalidEntry, ParamName), Strings.DialogTitleError);
				}
			}
			else
			{
				enableControl(false);
				try
				{
					bool ans = Planner.GetActiveDrone().SetParam(ParamName, OffValue);
					if (ans == false)
						MessageBox.Show(String.Format(Strings.MsgInvalidEntry, ParamName), Strings.DialogTitleError);
				}
				catch
				{
					MessageBox.Show(String.Format(Strings.MsgInvalidEntry, ParamName), Strings.DialogTitleError);
				}
			}

		}


		protected override void OnPaint(PaintEventArgs e)
		{
			this.OnPaintBackground(e);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (var path = new GraphicsPath())
			{
				var d = Padding.All;
				var r = this.Height - 2 * d;
				path.AddArc(d, d, r, r, 90, 180);
				path.AddArc(this.Width - r - d, d, r, r, -90, 180);
				path.CloseFigure();
				e.Graphics.FillPath(Checked ? Brushes.DarkGray : Brushes.LightGray, path);
				r = Height - 1;
				var rect = Checked ? new Rectangle(Width - r - 1, 0, r, r)
								   : new Rectangle(0, 0, r, r);
				e.Graphics.FillEllipse(Checked ? Brushes.Green : Brushes.WhiteSmoke, rect);
			}
		}
	}
}
