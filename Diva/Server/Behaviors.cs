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
            public GMapMarker gpsMarker =
                new GMarkerGoogle(new PointLatLng(0f, 0f), GMarkerGoogleType.blue_dot);


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

                gpsMarker.Position = _point;

                if (!_markerShown)
                    _overlay.Markers.Add(gpsMarker);
                else if (_markerShown)
                    _overlay.Markers.Remove(gpsMarker);

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
