using Diva.Mavlink;
using GMap.NET;
using log4net;
using System;
using System.Reflection;

namespace Diva.Mission
{
    public class FlytoPipe: IDisposable
    {
        public static readonly ILog log = 
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MavDrone drone;
        private PointLatLng[] destinations;

        public FlytoPipe(MavDrone _drone)
        {
            drone = _drone;
        }


        public bool SetDestinations(PointLatLng[] _destinations)
        {
            destinations = _destinations;
            return destinations != null;
        }

        public void Start(int round=0)
        {
            FlyTo flyTo = new FlyTo(drone);
            
            try
            {
                if (flyTo.SetDestination(destinations[round]))
                {
                    if (flyTo.Start())
                    {
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
                log.Warn("Has reached endpoint.");
            }
            
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
