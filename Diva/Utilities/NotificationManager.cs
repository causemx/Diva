using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Controls;
using Diva.Mavlink;

namespace Diva.Utilities
{
	public class NotificationManager
	{

		public interface INotification
		{
			void Notify();
		}

		public class BatteryNotification : INotification
		{
			bool isShowing = false;
			DroneInfo dinfo;
			MavDrone activeDrone = null;

			public BatteryNotification(DroneInfo info)
			{
				this.dinfo = info;
				this.activeDrone = info.Drone;
			}

			public void Notify()
			{

				if (activeDrone.Status.battery_voltage == 0.0d) return;

				try
				{
				
					if (!isShowing && activeDrone.Status.battery_voltage < activeDrone.Status.low_voltage)
					{
						Task.Factory.StartNew(() =>
						{
							dinfo.LowVoltageWarning(true);
							MessageBox.Show(Diva.Properties.Strings.MsgLowVoltageWarning, Diva.Properties.Strings.DialogTitleWarning, MessageBoxButtons.OK);
						});
						isShowing = true;
					}
					else if (activeDrone.Status.battery_voltage > activeDrone.Status.low_voltage)
					{
						dinfo.LowVoltageWarning(false);
						isShowing = false;
					}
					

				}
				catch (Exception e)
				{
					Planner.log.Error(e.ToString());
				}
			}
		}
	}
}
