using Diva.Mavlink;
using GMap.NET;
using log4net;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Policy;
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
            Stop,
        }

        public static readonly ILog log = 
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public const int TOLERANCE = 10000;
        
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
                log.Error("Mode error when ready.");
                return false;
            }
            state = State.Ready;
            return true;
        }


        public void Start(int round=0)
        {
            // flyTo = FlyTo.GetFlyToFrom(drone);
            flyTo = new FlyTo(drone);
            try
            {
                if (flyTo.SetDestination(destinations[round]))
                {
                    if (flyTo.Start())
                    {
                        state = State.Active;
                        flyTo.DestinationReached += (o, r) =>
                        {
                            log.Warn("Destination has reached.");
                            // flyTo.SetDestination(Destinations[1]);
                            Start(round + 1);

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
                log.Warn("Has reached endpoint.");
            }
        }
 

        public void Stop()
        {
            flyTo.Dispose();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
