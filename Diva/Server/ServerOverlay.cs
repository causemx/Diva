using Diva.Mavlink;
using Diva.Utilities;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Diva.Server
{
    public class ServerOverlay
    {
        [Serializable]
        class ServerOverlayException : Exception
        {
            public override string ToString() => 
                "ServerOverlay_" + base.ToString();
        }

        public readonly MavDrone Drone;
        public readonly GMapOverlay Overlay;

        public static ServerOverlay GetServerOverlay(MavDrone drone) => new ServerOverlay(drone);

        public static GMapOverlay GetOverlay(MavDrone drone) => GetServerOverlay(drone).Overlay;

        private ServerOverlay(MavDrone drone)
        {
            Drone = drone;
            Overlay = new GMapOverlay("Server_" + drone.Name + Guid.NewGuid().ToString());
            Overlay.IsVisibile = true;
            Planner.GetPlannerInstance().GMapControl?.Overlays.Add(Overlay);
        }

        public void AddPolygonMarker(Bitmap src, double lng, double lat, double radius, string tooltip)
        {
            try
            {
                PointLatLng point = new PointLatLng(lat, lng);
          
                GMarkerGoogle marker = new GMarkerGoogle(point, src)
                {
                    ToolTipMode = MarkerTooltipMode.OnMouseOver,
                    ToolTipText = tooltip,
                };

                GMapRectMarker mBorders = new GMapRectMarker(point)
                {
                    InnerMarker = marker,
                    wprad = (int)radius
                };

                Overlay.Markers.Add(marker);
                Overlay.Markers.Add(mBorders);
            }
            catch (ServerOverlayException se)
            {
                Debug.WriteLine(se.ToString());
            }
        }

        public void ClearMarker() => Overlay.Clear();
    }
}
