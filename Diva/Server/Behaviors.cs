using Diva.Mavlink;
using Diva.Properties;
using Diva.Utilities;
using GMap.NET;
using GMap.NET.ObjectModel;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace Diva.Server
{
    internal class Behaviors
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public class Echo : WebSocketBehavior
        {

            public const string OVERLAY_ID_GPS = "_GPS";
            public const int FORECASTE_DISTANCE = 100; //meter 
            // public const string DUMMY_GPS_DATA = "(24.84668179965741,121.00870604721274)";

            public static Planner planner = Planner.GetPlannerInstance();
            public static MavDrone drone = Planner.GetActiveDrone();
            public static GMapControl mapControl = planner?.GMapControl;
            public static ObservableCollectionThreadSafe<GMapOverlay> overlays = mapControl?.Overlays;
            public static GMapOverlay gpsOverlay = new GMapOverlay(id: OVERLAY_ID_GPS);
            public static bool isContainOverlay = false;
            // public GMapMarker gpsMarker = new GMarkerGoogle(new PointLatLng(0f, 0f), GMarkerGoogleType.blue_dot);
            public static GMapMarker gpsMarker = new GMarkerGoogle(
                new PointLatLngAlt(0f, 0f),
                new Bitmap(Resources.icon_gps_32));

            public static GMapMarker baseMarker = new GMarkerGoogle(
                new PointLatLngAlt(0f, 0f),
                GMarkerGoogleType.arrow);

            public static GMapMarker forecastMarser = new GMarkerGoogle(
                new PointLatLngAlt(0f, 0f),
                new Bitmap(Resources.icon_forecast_32));

            public static PointLatLng dummyBaseLocation = new PointLatLng(24.773306, 121.045633);

          
            /// <summary>
            /// Uer OnOpen to instead of constructor, Instance class and variable
            /// </summary>
            protected override void OnOpen()
            {
                base.OnOpen();

                if (!isContainOverlay)
                {
                    overlays.Add(gpsOverlay);
                    isContainOverlay = true;
                    gpsOverlay.Markers.Add(gpsMarker);
                }
#if DEBUG
                //  Enable dummy data marker
                Console.WriteLine("DEBUG");
                baseMarker.Position = dummyBaseLocation;
                gpsOverlay.Markers.Add(baseMarker);
#endif

            }

            protected override void OnMessage(MessageEventArgs e)
            {

                var _data = Encoding.UTF8.GetString(e.RawData);
                log.Info($"recv: {_data}");
                Send(_data);

                var _coord = Parse(_data);
                var _point = new PointLatLng(_coord[0], _coord[1]);


                // Update/Add gps marker.
                gpsMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                gpsMarker.ToolTip = new GMapToolTip(gpsMarker);
                bool _markerShown = gpsOverlay.Markers.Contains(gpsMarker);

                // Generate base, gps_dongle and forecast position.
                gpsMarker.Position = _point;
                drone.Status.GpsDonglePosition = _point;
                
#if DEBUG
                var _bearing = MathHelper.BearingOf(
                    dummyBaseLocation.Lat, dummyBaseLocation.Lng, _point.Lat, _point.Lng);
                var _derivedPoint = MathHelper.GetNewGeoPoint(_point, _bearing, FORECASTE_DISTANCE);
                forecastMarser.Position = _derivedPoint;
                drone.Status.ForecastPosition = _derivedPoint;
                bool _isEmptyBase = (dummyBaseLocation.Lat == 0d && dummyBaseLocation.Lng == 0d);
#else
                var _bearing = MathHelper.BearingOf(
                    BaseLocation.Location.Lat, BaseLocation.Location.Lng, _point.Lat, _point.Lng);
                var _derivedPoint = MathHelper.GetNewGeoPoint(_point, _bearing, FORECASTE_DISTANCE);
                forecastMarser.Position = _derivedPoint;
                drone.Status.ForecastPosition = _derivedPoint;
                bool _isEmptyBase = (BaseLocation.Location.Lat == 0d && BaseLocation.Location.Lng == 0d);
#endif

                gpsOverlay.Markers.Clear();

                gpsOverlay.Markers.Add(gpsMarker);
                if (!_isEmptyBase) gpsOverlay.Markers.Add(forecastMarser);
                
                
            }


            private static double[] Parse(string input)
            {
                return Array.ConvertAll(input.Split(','), Double.Parse);
            }
        }
    }
}
