using System;
using System.Threading.Tasks;
using System.Device.Location;
using GPoint = GMap.NET.PointLatLng;
using Diva.Mavlink;

namespace Diva.Utilities
{
    static class BaseLocation
    {
        private class BaseLocationStatus : DroneStatus
        {
            public override GPoint Location => BaseLocation.Location;
        }

        public readonly static MavDrone AsDrone
            = new MavDrone(new DroneSetting { Name = Properties.Strings.StrBase }) { Status = new BaseLocationStatus() };

        private readonly static GeoCoordinateWatcher LocationWatcher = new GeoCoordinateWatcher();

        static BaseLocation()
        {
            LocationWatcher.StatusChanged += LocationWatcher_StatusChanged;
            LocationWatcher.PositionChanged += LocationWatcher_PositionChanged;
            LocationWatcher.Start();
        }

        public static bool Initialized { get; private set; }
        private static void LocationWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Initialized |= e.Status == GeoPositionStatus.Ready;
        }

        public static double AccuracyLimit { get; set; } = 30;
        public static TimeSpan GPSTimeout { get; set; } = new TimeSpan(0, 0, 10);
        public static DateTime LastPositionTime { get; private set; }
        public static EventHandler LocationChanged;

        private static GeoCoordinate geoCoordinate;
        private static DateTime sentinelTimestamp = DateTime.Now;
        private static GeoCoordinateWatcher sentinelWatcher;
        private static readonly TimeSpan SentinelTimeout = TimeSpan.FromSeconds(45);

        private static void SentinelCheck()
        {
            if (DateTime.Now - sentinelTimestamp > SentinelTimeout)
            {
                sentinelWatcher?.Dispose();
                Task.Run(() => {
                    try
                    {
                        sentinelWatcher = new GeoCoordinateWatcher();
                        sentinelWatcher.Start();

                    } catch (Exception _e)
                    {
                        Console.WriteLine(_e.ToString());
                    }
                });
                sentinelTimestamp = DateTime.Now;
            }
        }

        private static void LocationWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (e.Position.Location.IsUnknown ||
                e.Position.Location.HorizontalAccuracy > AccuracyLimit)
            {
                SentinelCheck();
                return;
            }
            LastPositionTime = sentinelTimestamp = DateTime.Now;
            geoCoordinate = e.Position.Location;
            LocationChanged?.Invoke(null, e);
        }

        public static bool Ready
        {
            get
            {
                SentinelCheck();
                return DateTime.Now - LastPositionTime <= GPSTimeout
                    && LocationWatcher.Status == GeoPositionStatus.Ready;
            }
        }

        public static GPoint Location
        {
            get
            {
                SentinelCheck();
                return geoCoordinate == null ? GPoint.Empty :
                    new GPoint(geoCoordinate.Latitude, geoCoordinate.Longitude);
            }
        }
    }
}
