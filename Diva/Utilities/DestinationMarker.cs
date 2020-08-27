using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Utilities
{
    class DestinationMarker : GMapMarker
    {
        public static GMapOverlay overlay;
        public static GMapOverlay DistanceOverlay
            => overlay ?? (overlay = Planner.GetPlannerInstance()?.GMapControl?.Overlays.First(o => o.Id == "Commons"));
        public static Font Font = new Font(FontFamily.GenericMonospace, SystemFonts.DefaultFont.Size, FontStyle.Bold);

        public static readonly Color NormalColor = Color.White;
        public static readonly Color BrakeColor = Color.Red;
        public const float DefaultLineWidth = 5.0f;

        public Color LineColor { get; set; } = NormalColor;
        public Brush TextBrush { get; set; } = Brushes.DarkBlue;
        public Brush FillBrush { get; set; } = Brushes.CornflowerBlue;
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
                ToolTipText = GetDescriptionText();
            }
        }

        private string GetDescriptionText()
        {
            string toFixed(double d, int digits = 5) { return d.ToString($"N{digits}"); }
            if (Overlay == null) return "";
            return $"To: ({toFixed(To.Lat)},{toFixed(To.Lng)})\nDistance: " +
                $"{toFixed(Overlay.Control.MapProvider.Projection.GetDistance(From, To) * 1000)}m";
        }

        public DestinationMarker(PointLatLng dest) : base(dest)
        {
            ToolTip = new GMapBaloonToolTip(this)
            {
                Fill = FillBrush,
                Font = Font,
                Foreground = TextBrush,
                Offset = new Point(-10, -20)
            };
            DistanceOverlay.Markers.Add(this);
            From = dest;
            ToolTipMode = MarkerTooltipMode.Always;
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
