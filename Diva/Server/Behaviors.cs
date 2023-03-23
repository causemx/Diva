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
            private const string OVERLAY_ID_GPS = "_GPS";
            private const string DUMMY_GPS_DATA = "(24.84668179965741,121.00870604721274)";

            private Planner _planner;
            private GMapControl _mapControl;
            private ObservableCollectionThreadSafe<GMapOverlay> _overlays;
            private GMapOverlay _gpsOverlay;
            private GMapMarker _gpsMarker;

            /// <summary>
            /// Uer OnOpen to instead of constructor, Instance class and variable
            /// </summary>
            protected override void OnOpen()
            {
                base.OnOpen();

                log.Info("ws is open");

                _planner = Planner.GetPlannerInstance();
                _mapControl = _planner?.GMapControl;
                _overlays = _mapControl.Overlays;
                _gpsOverlay = new GMapOverlay(id: OVERLAY_ID_GPS);
                _overlays.Add(_gpsOverlay);


                // var _dummys = Parse(DUMMY_GPS_DATA);
                // var _point = new PointLatLng(_dummys[0], _dummys[1]);
                // var _point = new PointLatLng(24.776439, 121.042456);
                // _gpsMarker = new GMarkerGoogle(_point, GMarkerGoogleType.arrow);
                // _gpsOverlay.Markers.Add(_gpsMarker);
                
            }

            protected override void OnMessage(MessageEventArgs e)
            {

                var _data = Encoding.UTF8.GetString(e.RawData);
                log.Info($"raw: {_data}");

                var _coord = Parse(_data);
                var _point = new PointLatLng(_coord[0], _coord[1]);
                _gpsMarker = new GMarkerGoogle(_point, GMarkerGoogleType.arrow);
                _gpsOverlay.Markers.Clear();
                _gpsOverlay.Markers.Add(_gpsMarker);

                Parse(_data);
                // log.Info($"parsed: {Parse(_data)}");
                
                Send(e.Data);
            }

            private double[] Parse(string input)
            {
                return Array.ConvertAll(input.Split(','), Double.Parse);
            }
        }
    }
}
