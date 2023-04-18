using Diva.Mavlink;
using GMap.NET;
using log4net;
using System;
using System.Reflection;

namespace Diva.Mission
{
    public class FlytoPool: IDisposable
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public MavDrone drone;
        private FlyTo flyTo = null;
        private PointLatLng[] _destinations;

        public PointLatLng[] Destinations
        {
            set => _destinations = value;
            get => _destinations;
        }

        public FlytoPool(MavDrone _drone)
        {
            drone = _drone;
        }

        public void StartRecycle(int round = 0)
        {
            var flyTo = new FlyTo(drone);
            
            try
            {
                flyTo.SetDestination(Destinations[round]);
                
                if (flyTo.Start())
                {
                    flyTo.DestinationReached += (o, r) =>
                    {
                        log.Warn("Destination has reached.");
                        // flyTo.SetDestination(Destinations[1]);
                        StartRecycle(round + 1);

                        var f = (FlyTo)o;
                        f.Dispose();
                    };
                }
            }
            catch (Exception)
            {
                log.Warn("Has reached endpoint.");
            }
            
        }

        public void Dispose()
        {
            flyTo.Dispose();
        }
    }
}
