using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.ToolTips;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public static readonly Color DefaultLineColor = Color.White;
        public static readonly Color WarningColor = Color.DarkOrange;
        public static readonly Color DefaultLoiterCircleColor = Color.Violet;
        public static readonly Color AlertLoiterCircleColor = Color.Red;
        public const float DefaultLineWidth = 5.0f;
        public const float TrackTargetRadius = 15.0f;
        public const float LoiterCircleWidth = 3.0f;

        public static Pen LineStroke { get; set; } = new Pen(DefaultLineColor, DefaultLineWidth);
        public static Pen NormalLoiterCirclePen { get; set; } = new Pen(DefaultLoiterCircleColor, LoiterCircleWidth);
        public static Pen WarningLoiterCircleColorPen { get; set; } = new Pen(WarningColor, LoiterCircleWidth);
        public static Pen AlertLoiterCircleColorPen { get; set; } = new Pen(AlertLoiterCircleColor, LoiterCircleWidth);

        public static Brush NormalTextBrush { get; set; } = Brushes.White;
        public static Brush WarningTextBrush { get; set; } = Brushes.DarkOrange;
        public static Brush FillBrush { get; set; } = Brushes.Navy;

        private PointLatLng from;
        private PointLatLng[] EndPoints => new PointLatLng[] { from, Position };
        private GMapRoute route;
        public int LoiterRadius { get; set; }
        public Pen LoiterCirclePen { get; set; } = NormalLoiterCirclePen;

        public PointLatLng To
        {
            get => Position;
            set
            {
                Position = value;
                if (route == null)
                {
                    route = new GMapRoute(EndPoints, null) { Stroke = LineStroke };
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
        public PointLatLng From
        {
            get => from;
            set
            {
                from = value;
                if (route == null)
                {
                    route = new GMapRoute(EndPoints, null) { Stroke = LineStroke };
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

        private bool Reached;
        public void SetReached()
        {
            ToolTip.Foreground = WarningTextBrush;
            LoiterCirclePen = WarningLoiterCircleColorPen;
            Reached = true;
        }

        private static double ToleranceRatio = 0.0012;
        public static double LoiterRadiusToleranceRatio
        {
            get => ToleranceRatio * 1000;
            set => ToleranceRatio = value / 1000;
        }
        public double DistanceKm => Projection.GetDistance(From, To);
        public bool BeyondLoiterRadius => DistanceKm > LoiterRadius * ToleranceRatio;
        public bool CloseAreaAlert { get; set; }

        public bool ValidDestination(PointLatLng dest)
            => Projection.GetDistance(From, dest) > LoiterRadius * ToleranceRatio;

        private string GetDescriptionText()
        {
            string toFixed(double d, int digits = 1) { return d.ToString($"N{digits}"); }
            string Lat(double lat) { return toFixed(System.Math.Abs(lat), 4) + (lat < 0 ? " S" : " N");  }
            string Lng(double lng) { return toFixed(System.Math.Abs(lng), 4) + (lng < 0 ? " W" : " E");  }
            if (Overlay == null) return "";
            if (Reached) return $"To: ({Lat(To.Lat)},{Lng(To.Lng)})\nReached";
            return $"To: ({Lat(To.Lat)},{Lng(To.Lng)})\nDistance: " +
                (DistanceKm > 10 ? $"{toFixed(DistanceKm, 3)}km" : $"{toFixed(DistanceKm * 1000)}m");
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
                    Foreground = NormalTextBrush,
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
                var center = route.LocalPoints[1];
                float radius = (float)(LoiterRadius / Projection.GetGroundResolution(zoom, To.Lat));
                g.DrawArc(LoiterCirclePen, center.X - radius,
                     center.Y - radius, 2 * radius, 2 * radius, 0, 360);
                if (CloseAreaAlert)
                {
                    radius = (float)(radius * LoiterRadiusToleranceRatio);
                    center = route.LocalPoints[0];
                    using (Brush brush = new HatchBrush(HatchStyle.ForwardDiagonal,
                        AlertLoiterCircleColor, Color.FromArgb(0)))
                    {
                        g.FillEllipse(brush, center.X - radius,
                            center.Y - radius, 2 * radius, 2 * radius);
                    }
                }

            }
            base.OnRender(g);
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
