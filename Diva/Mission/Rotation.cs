using Diva.Controls;
using Diva.Mavlink;
using Diva.Properties;
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
				MessageBox.Show(Diva.Properties.Strings.MsgDroneNumberRequest, Diva.Properties.Strings.DialogTitleWarning, MessageBoxButtons.OK);
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

					infoDialog.Message(String.Format(Diva.Properties.Strings.MsgDialogRotationSwitch, index));

					// **IMPORTANT**: If using the INF firmware, mark this line.
					// mav.setMode(mav.Status.sysid, mav.Status.compid, "GUIDED");

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

					// switch mode to AUTO
					mav.doCommand(MAVLink.MAV_CMD.MISSION_START, 0, 0, 0, 0, 0, 0, 0);

					infoDialog.Message(String.Format(Diva.Properties.Strings.MsgDialogRotationExecute, index));

					while (mav.Status.sys_status == (byte)MAVLink.MAV_STATE.ACTIVE)
					{
						manualResetEvent.WaitOne(1000);
					}

					index = (index + 1) % drones.Count;
				}
				
			}
		}
	}
}
