using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Mavlink;

namespace Diva.Utilities
{
	public class NotificationManager
	{

		public interface INotification
		{
			void Register(string paramName);
			void Notify();
		}

		public class BatteryNotification : INotification
		{
			MavlinkInterface activeDrone = null;
			float cacheValue = 0.1f;
			bool isShown = false;

			public BatteryNotification(MavlinkInterface mav)
			{
				this.activeDrone = mav;
				
			}

			public void Register(string paramName)
			{
				cacheValue = activeDrone.GetParam(paramName);
			}

			public void Notify()
			{
				try
				{
					if (!isShown && activeDrone.Status.battery_voltage < activeDrone.GetParam("FS_BATT_VOLTAGE"))
						MessageBox.Show("Low voltage level, Start to Return...", "Warning", MessageBoxButtons.OK);

					isShown = (cacheValue == activeDrone.GetParam("FS_BATT_VOLTAGE")) ? true : false;

				}
				catch (Exception e)
				{
					if (activeDrone.Status.battery_voltage < activeDrone.GetParam("LOW_VOLT"))
						MessageBox.Show("Low voltage level", "Warning", MessageBoxButtons.OK);
				}
			}

			
		}

	}
}
