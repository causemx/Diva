using Diva.Controls;
using Diva.Mavlink;
using Diva.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;


namespace Diva.Mission
{
	class Rotation
	{

		public static readonly int DRONE_NUMBER_CONSTRAIN = 2;
		public bool isActive = false;
		public RotationInfo infoDialog;
		public List<MavDrone> drones;
		public ManualResetEvent manualResetEvent;

		public Rotation(List<MavDrone> _drones, RotationInfo _infoDialog)
		{
			drones = _drones;
			infoDialog = _infoDialog;
			infoDialog.DoCancelHandler += (s, e) => Stop();
			manualResetEvent = new ManualResetEvent(false);	

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
			infoDialog.Show(true);
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
			infoDialog.Show(false);
			worker.CancelAsync();
			manualResetEvent.Dispose();

			
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
				MessageBox.Show(Strings.MsgDroneNumberRequest, Strings.DialogTitleWarning, MessageBoxButtons.OK);
				return;
			}
					   
			while (isActive)
			{
				LockDrone(index);
				MavDrone drone = drones[index];

				if (!drone.IsRotationStandby && drone.Status.State == MAVLink.MAV_STATE.STANDBY)
				{
					try
					{
						if (!drone.IsOpen)
							throw new Exception();
					}
					catch (Exception e)
					{
						Planner.log.Error(e.ToString());
						continue;
					}

					infoDialog.Message(String.Format(Strings.MsgDialogRotationSwitch, index));

					// **IMPORTANT**: If using the INF firmware, mark this line.
					// mav.setMode(mav.Status.sysid, mav.Status.compid, "GUIDED");

					while (!drone.Status.IsArmed)
					{
						manualResetEvent.WaitOne(1000);
                        drone.DoArm(true);
                        drone.TakeOff(10);
					}

					while (drone.Status.State != MAVLink.MAV_STATE.ACTIVE)
					{
						manualResetEvent.WaitOne(1000);
					}

                    // switch mode to AUTO
                    drone.StartMission();

					infoDialog.Message(String.Format(Strings.MsgDialogRotationExecute, index));

					while (drone.Status.State == MAVLink.MAV_STATE.ACTIVE)
					{
						manualResetEvent.WaitOne(1000);
					}

					index = (index + 1) % drones.Count;
				}
			}
		}
	}
}
