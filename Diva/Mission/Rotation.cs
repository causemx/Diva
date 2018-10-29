using Diva.Controls;
using Diva.Mavlink;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Diva.Mission
{
	class Rotation
	{

		public static readonly int DRONE_NUMBER_CONSTRAIN = 0;
		public bool isActive = false;
		
		public InformDialog dialog;
		public List<MavDrone> drones;
		public ManualResetEvent manualResetEvent;

		public Rotation(List<MavDrone> _drones)
		{
			drones = _drones;
			manualResetEvent = new ManualResetEvent(false);

			dialog = new InformDialog() {
				StartPosition = FormStartPosition.CenterScreen,
			};
			dialog.Title("Rotation");
			dialog.Message("Initialize....^o^");

			dialog.DoCancelHandler += (o, e) =>
			{
				Planner.log.Info("Rotation Canceling...");
				Stop();
			};

		}

		public void InitializeState()
		{
			drones.ForEach(d =>
			{
				d.IsRotationStandby = true;
			});
		}

		public void ShowDialog()
		{
			dialog.Show();
		}

		private BackgroundWorker worker = null;


		/// <summary>
		/// start a new backgroundw worker, which own independency life cycle.
		/// </summary>
		public void Start()
		{
			isActive = true;

			worker = new BackgroundWorker();
			worker.WorkerSupportsCancellation = true;
			worker.DoWork += new DoWorkEventHandler(DoRotation);
			worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);

			
			if (worker.IsBusy)
			{
				worker.CancelAsync();
				Thread.Sleep(1000);
			}
			worker.RunWorkerAsync();
		}

		public bool IsBusy()
		{
			return worker.IsBusy;
		}

		public bool IsActived()
		{
			return isActive;
			
		}

		public void Stop()
		{
			isActive = false;
			dialog.Hide();
			worker.CancelAsync();
			manualResetEvent.Dispose();

			
		}

		public void ProgressChanged(object sender, ProgressChangedEventArgs pe)
		{
			((InformDialog)sender).Message("chenge");
		}

		public void LockDrone(int index)
		{
			drones.ForEach(d => d.IsRotationStandby = true);
			drones[index].IsRotationStandby = false;
		}


		internal int index = 0;

		public void DoRotation(object sender, DoWorkEventArgs de)
		{
			if (drones.Count < DRONE_NUMBER_CONSTRAIN)
			{
				MessageBox.Show("number invalid", "warning", MessageBoxButtons.OK);
				return;
			}
					   
			while (isActive)
			{
				LockDrone(index);
				MavDrone drone = drones[index];
				var mav = (MavlinkInterface)drone;

				if (!drone.IsRotationStandby && mav.Status.sys_status == (byte)MAVLink.MAV_STATE.STANDBY)
				{
					try
					{
						if (!mav.BaseStream.IsOpen)
							throw new Exception();
					}
					catch (Exception e)
					{
						Planner.log.Error(e.ToString());
						continue;
					}

					dialog.Message(String.Format("switch next drone, index: {0}.", index));

					// If using the INF firmware, mark this line.
					mav.setMode(mav.Status.sysid, mav.Status.compid, "GUIDED");

					while (!mav.Status.armed)
					{
						manualResetEvent.WaitOne(1000);
						mav.doARM(true);
						mav.doCommand(MAVLink.MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, 10);
					}

					while (mav.Status.sys_status != (byte)MAVLink.MAV_STATE.ACTIVE)
					{
						manualResetEvent.WaitOne(1000);
					}

					dialog.Message(String.Format("drone takeoff to 10m, index: {0}.", index));

					// switch mode to AUTO
					mav.doCommand(MAVLink.MAV_CMD.MISSION_START, 0, 0, 0, 0, 0, 0, 0);

					dialog.Message(String.Format("execute the mission, index: {0}.", index));

					while (mav.Status.sys_status == (byte)MAVLink.MAV_STATE.ACTIVE)
					{
						manualResetEvent.WaitOne(1000);
					}

					dialog.Message(String.Format("drone landed, index: {0}.", index));

					index = (index + 1) % drones.Count;
				}
				
			}
		}
	}
}
