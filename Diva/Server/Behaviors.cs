using Diva.Mavlink;
using Diva.Properties;
using Diva.Utilities;
using GMap.NET;
using GMap.NET.ObjectModel;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net;
using SharpKml.Dom;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI;
using WebSocketSharp;
using WebSocketSharp.Server;
using static Diva.Server.Tools;

namespace Diva.Server
{
    internal class Behaviors
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public class Echo : WebSocketBehavior
        {

            public const string OVERLAY_ID_GPS = "_GPS";
            public const int FORECASTE_DISTANCE = 20; //meter 
            public readonly PointLatLng dummyBaseLocation = new PointLatLng(24.7759514, 121.0420257);

            public static Planner planner = Planner.GetPlannerInstance();
            public static MavDrone drone = Planner.GetActiveDrone();
            public static GMapOverlay Overlay = (ServerOverlay.GetServerOverlay(drone)).Overlay;
            public static GMapMarker baseMarker = ServerOverlay.GetGMapMarker(ServerOverlay.MarkerType.Base, "Base", 0d, 0d);
            public static GMapMarker gpsMarker = ServerOverlay.GetGMapMarker(ServerOverlay.MarkerType.GPS, "GPS", 0d, 0d);
            public static GMapMarker forecastMarker = ServerOverlay.GetGMapMarker(ServerOverlay.MarkerType.Forecast, "FORECAST", 0d, 0d);
            public bool isInitialize = false;

            public event EventHandler<ExtendMessageEventArgs> onMessage;
            public event EventHandler<ErrorEventArgs> onError;
            public event EventHandler<CloseEventArgs> onClose;


            /// <summary>
            /// Uer OnOpen to instead of constructor, Instance class and variable
            /// </summary>
            protected override void OnOpen()
            {
                base.OnOpen();

                if (!isInitialize)
                {
                    var mapConteol = planner?.GMapControl;
                    (mapConteol?.Overlays).Add(Overlay);
                    isInitialize = true;
                    Overlay.Markers.Add(baseMarker);
                    Overlay.Markers.Add(gpsMarker);
                    Overlay.Markers.Add(forecastMarker);
                }
            }

            protected override void OnMessage(MessageEventArgs e)
            {
                var _data = Encoding.UTF8.GetString(e.RawData);
                // log.Info($"recv: {_data}");

                Send(_data);

                var _coord = Parse(_data);
                var _gpsPoint = new PointLatLng(_coord[0], _coord[1]);


                // Update/Add gps marker.
                gpsMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                gpsMarker.ToolTip = new GMapToolTip(gpsMarker);
                bool _markerShown = Overlay.Markers.Contains(gpsMarker);

                // Generate base, gps_dongle and forecast position.
                
                
#if DEBUG
                var _bearing = MathHelper.BearingOf(
                    dummyBaseLocation.Lat, dummyBaseLocation.Lng, _gpsPoint.Lat, _gpsPoint.Lng);
                var _derivedPoint = MathHelper.GetNewGeoPoint(_gpsPoint, _bearing, FORECASTE_DISTANCE);
                bool _isEmptyBase = (dummyBaseLocation.Lat == 0d && dummyBaseLocation.Lng == 0d);
#else
                var _bearing = MathHelper.BearingOf(
                    BaseLocation.Location.Lat, BaseLocation.Location.Lng, _point.Lat, _point.Lng);
                var _derivedPoint = MathHelper.GetNewGeoPoint(_point, _bearing, FORECASTE_DISTANCE);
                forecastMarser.Position = _derivedPoint;
                drone.Status.ForecastPosition = _derivedPoint;
                bool _isEmptyBase = (BaseLocation.Location.Lat == 0d && BaseLocation.Location.Lng == 0d);
#endif

                gpsMarker.Position = _gpsPoint;
                forecastMarker.Position = _derivedPoint;
                baseMarker.Position = dummyBaseLocation;

                Overlay.Markers.Clear();

                if (!_isEmptyBase)
                {
                    Overlay.Markers.Add(baseMarker);
                    Overlay.Markers.Add(gpsMarker);
                    Overlay.Markers.Add(forecastMarker);
                }

                ExtendMessageEventArgs eme = new ExtendMessageEventArgs(e);
                eme.GeoData = new PointLatLng[] { _derivedPoint, _gpsPoint , dummyBaseLocation };
                onMessage?.Invoke(this, eme);
            }

            protected override void OnError(ErrorEventArgs e) => onError?.Invoke(this, e);

            protected override void OnClose(CloseEventArgs e) => onClose?.Invoke(this, e);


            private double[] Parse(string input)
            {
                return Array.ConvertAll(input.Split(','), Double.Parse);
            }
        }
    }
}
