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


		public static readonly int DRONE_NUMBER_CONSTRAIN = 2;
		public bool isActive = false;
		public BackgroundWorker worker;
		public InformDialog dialog;
		public List<MavDrone> drones;
		public ManualResetEvent manualResetEvent;

		public Rotation(List<MavDrone> _drones)
		{
			drones = _drones;

			manualResetEvent = new ManualResetEvent(false);

			worker = new BackgroundWorker();
			worker.WorkerSupportsCancellation = true;
			worker.DoWork += new DoWorkEventHandler(DoRotation);
			worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);

			dialog = new InformDialog();
			dialog.Title("Rotation");
			dialog.Message("Initialize");
			dialog.DoCancelHandler += (o, e) =>
			{
				Console.WriteLine("call cancel");
				Stop();
				dialog.Dispose();
			};
			dialog.Show();
		}

		public void InitializeState()
		{
			drones.ForEach(d =>
			{
				d.IsRotationStandby = true;
			});
		}

		public void Start()
		{
			isActive = true;
			if (worker != null) worker.RunWorkerAsync();

		}

		public bool isStart()
		{
			return worker.IsBusy;
		}

		public void Stop()
		{
			isActive = false;
			worker.CancelAsync();
			dialog.Dispose();
			manualResetEvent.Dispose();

			// force all drones return to launch.
			drones.ForEach(d =>
			{
				manualResetEvent.WaitOne(1000);
				var mav = (MavlinkInterface)d;
				mav.doCommand(MAVLink.MAV_CMD.RETURN_TO_LAUNCH, 0, 0, 0, 0, 0, 0, 0);

			});
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


		public void DoRotation(object sender, DoWorkEventArgs de)
		{
			if (drones.Count < DRONE_NUMBER_CONSTRAIN)
			{
				MessageBox.Show("number invalid", "warning", MessageBoxButtons.OK);
				return;
			}


			int index = 0;

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
						Console.WriteLine("drone not online");
						continue;
					}

					while (mav.Status.mode != (uint)Planner.FlightMode.GUIDED)
					{
						// Task.Delay(1000).Wait();
						manualResetEvent.WaitOne(1000);
						mav.setMode(mav.Status.sysid, mav.Status.compid, "GUIDED");
					}

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


					Console.WriteLine("sys_status: " + mav.Status.sys_status);

					while (mav.Status.sys_status == (byte)MAVLink.MAV_STATE.ACTIVE)
					{
						manualResetEvent.WaitOne(1000);
					}

					index = (index + 1) % drones.Count;
				}
				
			}
		}


		public class StatusChecker
		{
			private int invokeCount;
			private int maxCount;

			public StatusChecker(int count)
			{
				invokeCount = 0;
				maxCount = count;
			}

			// This method is called by the timer delegate.
			public void CheckStatus(Object stateInfo)
			{
				AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
				Console.WriteLine("{0} Checking status {1,2}.",
					DateTime.Now.ToString("h:mm:ss.fff"),
					(++invokeCount).ToString());

				if (invokeCount == maxCount)
				{
					// Reset the counter and signal the waiting thread.
					invokeCount = 0;
					autoEvent.Set();
				}
			}
		}

	}

}
