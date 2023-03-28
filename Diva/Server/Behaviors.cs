using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using WebSocketSharp;
using System.Drawing;
using Diva.Mavlink;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.ObjectModel;
using log4net;
using System.Reflection;
using SharpKml.Dom;
using GMap.NET;
using Diva.Utilities;
using GMap.NET.WindowsForms.Markers;
using System.Reflection.Emit;
using Newtonsoft.Json.Linq;
using Diva.Properties;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Printing;
using System.Device.Location;

namespace Diva.Server
{
    internal class Behaviors
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public class Echo : WebSocketBehavior
        {
            public const string OVERLAY_ID_GPS = "_GPS";
            public const string DUMMY_GPS_DATA = "(24.84668179965741,121.00870604721274)";

            public Planner planner;
            public GMapControl mapControl;
            public ObservableCollectionThreadSafe<GMapOverlay> overlays;
            public GMapOverlay gpsOverlay;
            // public GMapMarker gpsMarker = new GMarkerGoogle(new PointLatLng(0f, 0f), GMarkerGoogleType.blue_dot);
            public GMapMarker gpsMarker = new GMarkerGoogle(
                new PointLatLngAlt(0f, 0f),
                new Bitmap(Resources.icon_gps_32));

            public GMapMarker dummyBaseMarker = new GMarkerGoogle(
                new PointLatLngAlt(0f, 0f),
                GMarkerGoogleType.arrow);

            public GMapMarker forecastMarser = new GMarkerGoogle(
                new PointLatLngAlt(0f, 0f),
                GMarkerGoogleType.green_big_go);

            public PointLatLng dummyBaseLocation = (BaseLocation.Location == null) ? 
                BaseLocation.Location : new PointLatLng(24.773306, 121.045633);


            /// <summary>
            /// Uer OnOpen to instead of constructor, Instance class and variable
            /// </summary>
            protected override void OnOpen()
            {
                base.OnOpen();

                log.Info("ws is open");

                planner = Planner.GetPlannerInstance();
                mapControl = planner?.GMapControl;
                overlays = mapControl.Overlays;
                gpsOverlay = new GMapOverlay(id: OVERLAY_ID_GPS);
                overlays.Add(gpsOverlay);
                gpsOverlay.Markers.Add(gpsMarker);

                //  Enable dummy data marker
                // dummyBaseMarker.Position = dummyBaseLocation;
                // gpsOverlay.Markers.Add(dummyBaseMarker);

            }

            protected override void OnMessage(MessageEventArgs e)
            {

                var _data = Encoding.UTF8.GetString(e.RawData);
                log.Info($"raw: {_data}");

                var _coord = Parse(_data);
                var _point = new PointLatLng(_coord[0], _coord[1]);


                // Update/Add gps marker.
                gpsMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                gpsMarker.ToolTip = new GMapToolTip(gpsMarker);
                bool _markerShown = gpsOverlay.Markers.Contains(gpsMarker);
                var _overlay = gpsOverlay;

                // Generate base, gps_dongle and forecast position.
                gpsMarker.Position = _point;
                var _bearing = MathHelper.BearingOf(
                    BaseLocation.Location.Lat, BaseLocation.Location.Lng, _point.Lat, _point.Lng);
                var _derivedPoint = MathHelper.GetNewGeoPoint(_point, _bearing, 1000);
                forecastMarser.Position = _derivedPoint;

                if (!_markerShown)
                {
                    _overlay.Markers.Add(gpsMarker);
                    _overlay.Markers.Add(forecastMarser);
                }    
                else if (_markerShown)
                {
                    _overlay.Markers.Remove(gpsMarker);
                    _overlay.Markers.Remove(forecastMarser);
                }
                    
                Parse(_data);
                // log.Info($"parsed: {Parse(_data)}");
                
                Send(_data);
            }


            private double[] Parse(string input)
            {
                return Array.ConvertAll(input.Split(','), Double.Parse);
            }
        }
    }
}
