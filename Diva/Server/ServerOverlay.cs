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
using System.Runtime.CompilerServices;
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

        public static ServerOverlay GetServerOverlay(MavDrone drone)
        {
            if (!Overlays.ContainsKey(drone))
            {
                new ServerOverlay(drone);
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
        
        public static GMapMarker GetGMapMarker(MarkerType type, string tag, double lng, double lat, double radius=10)
        {
            try
            {
                IBitmapRender bitmapRender = null;
                if (type == MarkerType.GPS)
                    bitmapRender = new GpsBitmapRender();
                else if (type == MarkerType.Forecast)
                    bitmapRender = new ForecastBitmapRender();
                else
                    bitmapRender = new DefalutBitmapRender();
                

                PointLatLng point = new PointLatLng(lat, lng);

                GMapTaggedMarker m = new GMapTaggedMarker(point, tag, bitmapRender)
                {
                    ToolTipMode = MarkerTooltipMode.OnMouseOver,
                    ToolTipText = "Type: " + Enum.GetName(typeof(MarkerType), type)
                };

                return m;
            }
            catch (ServerOverlayException se)
            {
                Debug.WriteLine(se.ToString());
                return null;
            }
        }
    }
}
