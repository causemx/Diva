﻿using Diva.Mavlink;
using Diva.Utilities;
using GMap.NET;
using log4net;
using SharpKml.Base;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Diva.Mission
{
    public class FlytoPipe: IDisposable
    {
        public enum State
        {
            Ready,
            Active,
            Pause,
            Destory,
        }

        public static readonly ILog log = 
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public MavDrone drone;
        public Planner planner = Planner.GetPlannerInstance();
        private PointLatLng[] destinations;
        private State state;
        private FlyTo flyTo;

        public FlytoPipe(MavDrone _drone)
        {
            drone = _drone;
        }


        public bool SetDestinations(PointLatLng[] _destinations)
        {
            destinations = _destinations;
            return destinations != null;
        }

        public bool Ready(string specificMode)
        {
            if (drone.Status.State != MAVLink.MAV_STATE.ACTIVE
                || !drone.IsMode(specificMode))
            {
                return false;
            }
            state = State.Ready;
            return true;
        }


        public void StartPipe(int round=0)
        {
            // flyTo = FlyTo.GetFlyToFrom(drone);
            flyTo = new FlyTo(drone);
            planner.BackgroundTimer -= ModeWatcher;
            planner.BackgroundTimer += ModeWatcher;
            try
            {
                if (flyTo.SetDestination(destinations[round]))
                {
                    drone.SetMode("GUIDED");

                    if (flyTo.Start())
                    {
                        state = State.Active;
                        flyTo.DestinationReached += (o, r) =>
                        {
                            FloatMessage.NewMessage(
                                drone.Name,
                                (int)MAVLink.MAV_SEVERITY.ALERT,
                                $"Reached FlyTo destination[{round}]");
                            StartPipe(round + 1);

                            var f = (FlyTo)o;
                            f.Dispose();
                        };
                    }
                }
            }
            catch (Exception)
            {
                flyTo.Stop();
                flyTo.Dispose();
                planner.BackgroundTimer -= ModeWatcher;
                FloatMessage.NewMessage(
                    drone.Name,
                    (int)MAVLink.MAV_SEVERITY.INFO,
                    "Fly to derive point completed");
            }
        }

        private DateTime nextTime;
        private bool Delay(int _timeSpan)
        {
            if (DateTime.Now < nextTime)
                return false;
                
            nextTime = DateTime.Now.AddMilliseconds(_timeSpan);
            return true;
        }

        private void ModeWatcher(Object sender, EventArgs e)
        {
            if (!drone.IsMode("GUIDED"))
            {
                if (Delay(2000))
                {
                    Console.WriteLine($"Drone is Guided mode? {drone.IsMode("GUIDED")}");
                    drone.SetMode("GUIDED");
                }
            }
            
        }

        public void Stop()
        {
            flyTo.Dispose();
        }

        public void Dispose()
        {
            state = State.Destory;
        }
    }
}
