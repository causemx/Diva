using Diva.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Diva.Cooper
{
	public class Rotation
	{
		private int cursor;
		private ProgressDialog myDialog;

		public Rotation()
		{
			cursor = 0;
			myDialog = new ProgressDialog();
			myDialog.IsActive = true;
		}


		public void SingleRotation()
		{
			myDialog.Show();

			myDialog.DoWork += delegate (object dialog, DoWorkEventArgs dwe)
			{
				while (myDialog.IsActive)
				{

					// TODO think about mavlinkinterface dispose issue.

					var _comport = Planner.comPorts[cursor];

					try
					{
						if (!_comport.BaseStream.IsOpen) continue;
						while (_comport.MAV.mode != (uint)4)
						{
							Thread.Sleep(3000);
							myDialog.ReportProgress(-1, "Waiting for switching mode to GUIDED");
							_comport.setMode(_comport.MAV.sysid, _comport.MAV.compid, "GUIDED");
						}

						myDialog.ReportProgress(-1, "Mode has switching to GUIDED");

						// do command - arm throttle

						while (!_comport.MAV.armed)
						{
							Thread.Sleep(1000);
							myDialog.ReportProgress(-1, "arming throttle");
							_comport.doARM(true);
							_comport.doCommand(MAVLink.MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, 10);
						}


						myDialog.ReportProgress(-1, "status: takeoff");


						while (_comport.MAV.mode != (uint)3)
						{
							Thread.Sleep(3000);
							myDialog.ReportProgress(-1, "Waiting for switching mode to AUTO");
							// switch mode to AUTO
							_comport.setMode(_comport.MAV.sysid, _comport.MAV.compid, "AUTO");
						}

						myDialog.ReportProgress(-1, "Mode has switching to AUTO");


						while (!_comport.MAV.landed)
						{
							Thread.Sleep(3000);
						}

						myDialog.ReportProgress(-1, "Next drone standby");
						cursor = (cursor + 1) % Planner.comPorts.Count;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
			};

			myDialog.ProgressChanged += delegate (object dialog, ProgressChangedEventArgs pce) {
				myDialog.Message = pce.UserState + ".";
			};

			myDialog.Run();

		}

		public void standByRotation()
		{
			var uav1 = Planner.comPorts[0];
			var uav2 = Planner.comPorts[1];

			myDialog.Show();
			myDialog.DoWork += delegate (object dialog, DoWorkEventArgs dwe)
			{
				// TODO: think a better way to do co-operative uav.

			};
		}

	}
}
