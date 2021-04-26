using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.ToolTips;
using System.Drawing;
using System.Linq;

namespace Diva.Utilities
{
    internal class DestinationMarker : GMapMarker
    {
        private static GMapControl mapControl;
        private static PureProjection projection;
        private static GMapOverlay overlay;
        private static GMapControl MapControl => mapControl ??
            (mapControl = Planner.GetPlannerInstance()?.GMapControl);
        private static PureProjection Projection => projection ??
            (projection = MapControl?.MapProvider.Projection);
        public static GMapOverlay DistanceOverlay => overlay ??
            (overlay = MapControl?.Overlays.First(o => o.Id == "Commons"));

        public static Font Font = new Font(FontFamily.GenericMonospace, SystemFonts.DefaultFont.Size, FontStyle.Bold);
        public static Size MarkerSize = new Size(60, 60);

        public static readonly Color NormalColor = Color.White;
        public static readonly Color BrakeColor = Color.Red;
        public const float DefaultLineWidth = 5.0f;
        public const float TrackTargetRadius = 15.0f;
        public const float LoiterCircleWidth = 3.0f;

        public Color LineColor { get; set; } = NormalColor;
        public Brush TextBrush { get; set; } = Brushes.White;
        public Brush FillBrush { get; set; } = Brushes.Navy;
        public Pen LoiterCirclePen { get; set; } = new Pen(Color.Violet, LoiterCircleWidth);
        public float Width = DefaultLineWidth;
        public PointLatLng To
        {
            get => Position;
            set
            {
                Position = value;
                if (route == null)
                {
                    route = new GMapRoute(EndPoints, null)
                    { Stroke = new Pen(LineColor, Width) };
                    DistanceOverlay.Routes.Add(route);
                }
                else
                    route.Points[1] = value;
                if (DistanceOverlay != null)
                    lock (DistanceOverlay)
                        DistanceOverlay.ForceUpdate();
                if (ToolTipMode == MarkerTooltipMode.Always)
                    ToolTipText = GetDescriptionText();
            }
        }
        private PointLatLng from;
        private PointLatLng[] EndPoints => new PointLatLng[] { from, Position };
        private GMapRoute route;
        public PointLatLng From
        {
            get => from;
            set
            {
                from = value;
                if (route == null)
                {
                    route = new GMapRoute(EndPoints, null)
                    { Stroke = new Pen(LineColor, Width) };
                    DistanceOverlay.Routes.Add(route);
                }
                else
                    route.Points[0] = value;
                if (DistanceOverlay != null)
                    lock (DistanceOverlay)
                        DistanceOverlay.ForceUpdate();
                if (ToolTipMode == MarkerTooltipMode.Always)
                    ToolTipText = GetDescriptionText();
            }
        }
        public int LoiterRadius;

        private bool Reached = false;
        public void SetReached() { Reached = true; }

        private string GetDescriptionText()
        {
            string toFixed(double d, int digits = 1) { return d.ToString($"N{digits}"); }
            string Lat(double lat) { return toFixed(System.Math.Abs(lat), 4) + (lat < 0 ? " S" : " N");  }
            string Lng(double lng) { return toFixed(System.Math.Abs(lng), 4) + (lng < 0 ? " W" : " E");  }
            if (Overlay == null) return "";
            if (Reached) return $"To: ({Lat(To.Lat)},{Lng(To.Lng)})\nReached";
            var distkm = Projection.GetDistance(From, To);
            return $"To: ({Lat(To.Lat)},{Lng(To.Lng)})\nDistance: " +
                (distkm > 10 ? $"{toFixed(distkm, 3)}km" : $"{toFixed(distkm * 1000)}m");
        }

        public DestinationMarker(PointLatLng dest, bool trackmode = false) : base(dest)
        {
            if (trackmode)
            {
                ToolTipMode = MarkerTooltipMode.Never;
            }
            else
            {
                ToolTip = new GMapBaloonToolTip(this)
                {
                    Fill = FillBrush,
                    Font = Font,
                    Foreground = TextBrush,
                    Offset = new Point(-10, -20)
                };
                ToolTipMode = MarkerTooltipMode.Always;
            }
            DistanceOverlay.Markers.Add(this);
            From = dest;
            Size = MarkerSize;
        }

        public override void OnRender(Graphics g)
        {
            if (LoiterRadius > 0)
            {
                int zoom = (int)MapControl.Zoom;
                var end = route.LocalPoints[1];
                float radius = (float)(LoiterRadius / Projection.GetGroundResolution(zoom, To.Lat));
                g.DrawArc(LoiterCirclePen, end.X - radius,
                     end.Y - radius, 2 * radius, 2 * radius, 0, 360);
            }
            base.OnRender(g);
        }

        public void SetBrakeMode(bool brake)
        {
            LineColor = brake ? NormalColor : BrakeColor;
            if (route != null)
            {
                var s = route.Stroke;
                route.Stroke = new Pen(LineColor, Width);
                s.Dispose();
            }      
        }

        private bool disposing = false;
        public override void Dispose()
        {
            if (!disposing)
            {
                if (route != null)
                {
                    lock (DistanceOverlay)
                        DistanceOverlay.Routes.Remove(route);
                    route.Dispose();
                }
                route = null;
                lock (DistanceOverlay)
                    DistanceOverlay.Markers.Remove(this);
                disposing = true;
            }
            base.Dispose();
        }
    }
}
