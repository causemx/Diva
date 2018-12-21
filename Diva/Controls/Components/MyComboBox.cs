using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Properties;

namespace Diva.Controls.Components
{
	public class MyComboBox : ComboBox
	{
		public new event EventHandler SelectedIndexChanged;

		[System.ComponentModel.Browsable(true)]
		public string ParamName { get; set; }


		Control _control;
		Type _source;
		List<KeyValuePair<int, string>> _source2;
		string paramname2 = "";

		public Control SubControl
		{
			get { return _control; }
			set { _control = value; }
		}

		[System.ComponentModel.Browsable(true)]
		public event EventHandler ValueUpdated;

		public MyComboBox()
		{
			this.Enabled = false;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		public void setup(List<KeyValuePair<int, string>> source, string paramname, MAVLink.MAVLinkParamList paramlist)
		//, string paramname2 = "", Control enabledisable = null)
		{
			base.SelectedIndexChanged -= MyComboBox_SelectedIndexChanged;

			_source2 = source;

			this.DisplayMember = "Value";
			this.ValueMember = "Key";
			this.DataSource = source;

			this.ParamName = paramname;


			if (paramlist.ContainsKey(paramname))
			{
				this.Enabled = true;
				this.Visible = true;

				enableControl(true);

				var item = paramlist[paramname];

				this.SelectedValue = (int)paramlist[paramname].Value;
			}

			base.SelectedIndexChanged += new EventHandler(MyComboBox_SelectedIndexChanged);
		}


		public void setup(Type source, string paramname, MAVLink.MAVLinkParamList paramlist)
		//, string paramname2 = "", Control enabledisable = null)
		{
			base.SelectedIndexChanged -= MyComboBox_SelectedIndexChanged;

			_source = source;

			this.DataSource = Enum.GetNames(source);

			this.ParamName = paramname;

			if (paramlist.ContainsKey(paramname))
			{
				this.Enabled = true;
				this.Visible = true;

				enableControl(true);

				this.Text = Enum.GetName(source, (Int32)paramlist[paramname].Value);
			}

			base.SelectedIndexChanged += new EventHandler(MyComboBox_SelectedIndexChanged);
		}

		void enableControl(bool enable)
		{
			if (_control != null)
				_control.Enabled = enable;
		}

		void MyComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.SelectedIndexChanged != null)
				this.SelectedIndexChanged(sender, e);

			if (_source != null)
			{
				try
				{
					if (ValueUpdated != null)
					{
						ValueUpdated(this,
							new MAVLinkParamChanged(ParamName, (float)(Int32)Enum.Parse(_source, this.Text)));
						return;
					}

					if (!Planner.GetActiveDrone().SetParam(ParamName, (float)(Int32)Enum.Parse(_source, this.Text)))
					{
						MessageBox.Show(String.Format(Strings.MsgInvalidEntry, ParamName), Strings.DialogTitleError);
					}

					if (paramname2 != "")
					{
						if (
							!Planner.GetActiveDrone().SetParam(paramname2,
								(float)(Int32)Enum.Parse(_source, this.Text) > 0 ? 1 : 0))
						{
							MessageBox.Show(String.Format(Strings.MsgInvalidEntry, paramname2), Strings.DialogTitleError);
						}
					}
				}
				catch
				{
					MessageBox.Show(String.Format(Strings.MsgInvalidEntry, ParamName), Strings.DialogTitleError);
				}
			}
			else if (_source2 != null)
			{
				try
				{
					if (ValueUpdated != null)
					{
						ValueUpdated(this,
							new MAVLinkParamChanged(ParamName, (float)(int)((MyComboBox)sender).SelectedValue));
						return;
					}

					if (!Planner.GetActiveDrone().SetParam(ParamName, (float)(int)((MyComboBox)sender).SelectedValue))
					{
						MessageBox.Show("Set " + ParamName + " Failed!", Strings.DialogTitleError);
					}

					if (paramname2 != "")
					{
						if (
							!Planner.GetActiveDrone().SetParam(paramname2,
								(float)(int)((MyComboBox)sender).SelectedValue > 0 ? 1 : 0))
						{
							MessageBox.Show("Set " + paramname2 + " Failed!", Strings.DialogTitleError);
						}
					}
				}
				catch
				{
					MessageBox.Show("Set " + ParamName + " Failed!", Strings.DialogTitleError);
				}
			}
		}
	}

	public class MAVLinkParamChanged : EventArgs
	{
		public string name;
		public float value;

		public MAVLinkParamChanged(string Name, float Value)
		{
			this.name = Name;
			this.value = Value;
		}
	}
}
