using Diva.Mavlink;
using Diva.Mission;
using Diva.Properties;
using Diva.Utilities;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net.ObjectRenderer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Web.Routing;
using System.Windows.Forms;

namespace Diva.Server
{
    public class ServerOverlay
    {
        #region --Constructor Injection--
        public class BaseBitmapRender : IBitmapRender
        {
            public Bitmap OnRender()
            {
                return new Bitmap(Resources.icon_house_32);
            }
        }

        public class GpsBitmapRender : IBitmapRender
        {
            public Bitmap OnRender()
            {
                return new Bitmap(Resources.icon_gps_32);
            }
        }

        public class ForecastBitmapRender : IBitmapRender
        {
            public Bitmap OnRender()
            {
                return new Bitmap(Resources.icon_forecast_32);
            }
        }
        #endregion

        [Serializable]
        class ServerOverlayException : Exception
        {
            public override string ToString() => 
                "ServerOverlay_" + base.ToString();
        }

        public enum MarkerType
        {
            Base,
            GPS,
            Forecast,
        }

        public readonly MavDrone Drone;
        public readonly GMapOverlay Overlay;
  
        private readonly static Dictionary<MavDrone, ServerOverlay> Overlays = new Dictionary<MavDrone, ServerOverlay>();
        private static ServerOverlay serverOverlay;

        public static ServerOverlay GetServerOverlay(MavDrone drone)
        {
            if (!Overlays.ContainsKey(drone))
            {
                serverOverlay = new ServerOverlay(drone);
                Planner.GetPlannerInstance().GMapControl?.Overlays.Add(Overlays[drone].Overlay);
            }
            return Overlays[drone];
        }

        private ServerOverlay(MavDrone drone)
        {
            Drone = drone;
            Overlay = new GMapOverlay("Server_" + drone.Name + Guid.NewGuid().ToString())
            {
                IsVisibile = true,
            };
            Overlays.Add(drone, this);
        }
        
        private void AddMarker(MarkerType type, string tag, double lng, double lat)
        {
            try
            {
                IBitmapRender bitmapRender = null;
                if (type == MarkerType.GPS)
                    bitmapRender = new GpsBitmapRender();
                if (type == MarkerType.Forecast)
                    bitmapRender = new ForecastBitmapRender();
                if (type == MarkerType.Base)
                    bitmapRender = new BaseBitmapRender();
                

                PointLatLng point = new PointLatLng(lat, lng);

                GMapTaggedMarker m = new GMapTaggedMarker(point, tag, bitmapRender)
                {
                    ToolTipMode = MarkerTooltipMode.OnMouseOver,
                    ToolTipText = "Type: " + Enum.GetName(typeof(MarkerType), type)
                };

                Overlay.Markers.Add(m);
            }
            catch (ServerOverlayException se)
            {
                Debug.WriteLine(se.ToString());
            }
        }

        private void DrawRoute(PointLatLng[] pts)
        {
            var pen = new Pen(Color.Yellow, 4) { DashStyle = DashStyle.Custom };
            GMapRoute route = new GMapRoute("_server_route");
            route.IsHitTestVisible = true;
            route.Stroke = pen;
            route.Points.AddRange(pts);
            Overlay.Routes.Add(route);
        }

        public void Draw(PointLatLng[] pts)
        {
            Overlay.Clear();
            AddMarker(MarkerType.Forecast, "Forecast", pts[0].Lng, pts[0].Lat);
            AddMarker(MarkerType.GPS, "Gps", pts[1].Lng, pts[1].Lat);
            AddMarker(MarkerType.Base, "Base", pts[2].Lng, pts[2].Lat);


            DrawRoute(pts);
        }
    }
}
