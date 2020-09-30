using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static TimeSpan GPSTimeout { get; set; } = new TimeSpan(0, 3, 0);
        public static DateTime LastPositionTime { get; private set; }
        public static EventHandler LocationChanged = null;

        private static GeoCoordinate geoCoordinate;

        private static void LocationWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (e.Position.Location.IsUnknown ||
                e.Position.Location.HorizontalAccuracy > AccuracyLimit)
                // possibly not gps result, drop
                return;
            LastPositionTime = DateTime.Now;
            geoCoordinate = e.Position.Location;
            LocationChanged?.Invoke(null, e);
        }

        public static bool Ready
            => DateTime.Now - LastPositionTime <= GPSTimeout
            && LocationWatcher.Status == GeoPositionStatus.Ready;

        public static GPoint Location => new GPoint(geoCoordinate.Latitude, geoCoordinate.Longitude);
    }
}
