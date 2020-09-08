using System;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Net;
using System.Threading;
using Timer = System.Timers.Timer;
using FltMsg = Diva.Utilities.FloatMessage;


namespace Diva.Controls
{
	public class MyGMap : GMapControl
	{
        public readonly GMapProvider GlobalMapProvider = BingSatelliteMapProvider.Instance;
        public bool DebugMode { get; set; }
        public bool IndoorMode { get; private set; }
        Thread onPaintThread = null;
		private int lastMouseX = 0;
		private int lastMouseY = 0;

        public MyGMap() : base()
		{
            MapScaleInfoEnabled = false;
            DisableFocusOnMouseEnter = true;
            //RoutesEnabled = true; // set by designer
            ForceDoubleBuffer = false;
            DebugMode = true;
            FltMsg.NewMessageNotice += InvalidateMessage;
            msgRefreshTimer.Elapsed += InvalidateMessage;
            msgRefreshTimer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            ResetMapProvider();
            base.OnLoad(e);
        }

        public void ResetMapProvider()
        {
            (MapProvider as ImageMapProvider)?.Dispose();
            IndoorMode = ConfigData.GetBoolOption(ConfigData.OptionName.UseImageMap);
            if (IndoorMode)
            {
                var p = new ImageMapProvider(ConfigData.GetOption(ConfigData.OptionName.ImageMapSource));
                if (p != null)
                {
                    GMaps.Instance.Mode = AccessMode.ServerOnly;
                    GMaps.Instance.MemoryCache.Clear();
                    MapProvider = p;
                    MaxZoom = p.MaxZoom ?? MaxZoom;
                    MinZoom = p.MinZoom;
                    if (Zoom > MaxZoom) Zoom = MaxZoom;
                    if (Zoom < MinZoom) Zoom = MinZoom;
                    if (p.Area != null)
                        Position = ((RectLatLng)p.Area).LocationMiddle;
                    return;
                }
                ConfigData.SetOption(ConfigData.OptionName.UseImageMap, false.ToString());
            }
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            string proxy = ConfigData.GetOption(ConfigData.OptionName.MapProxy);
            string[] prox = proxy.Split(':');
            var webproxy = WebRequest.DefaultWebProxy;
            if (prox.Length == 2)
            {
                int.TryParse(prox[1], out int port);
                try
                {
                    webproxy = new WebProxy(prox[0], port);
                } catch (Exception e)
                {
                    Console.WriteLine($"Error creating proxy {prox[0]}:{prox[1]} ({e.Message}), use system default instead.");
                }
            }
            GMapProvider.WebProxy = webproxy;
            MapProvider = GlobalMapProvider;
            MinZoom = 0;
            MaxZoom = 24;
            Zoom = 15;

            try
            {
                var loc = new System.IO.DirectoryInfo(ConfigData.GetOption(ConfigData.OptionName.MapCacheLocation));
                if (loc.Exists)
                    CacheLocation = loc.FullName;
            }
            catch { }
        }

        protected override void OnPaint(PaintEventArgs e)
		{
			if (onPaintThread != null)
			{
				Console.WriteLine("Was in onpaint Gmap th:" + Thread.CurrentThread.Name + " in " + onPaintThread.Name);
				return;
			}

            var start = DateTime.Now;
            onPaintThread = Thread.CurrentThread;

			try
            {
                base.OnPaint(e);
                // default map scale is blocked by toolstrip
                if (!IndoorMode) DrawCustomMapScale(e.Graphics);
                Planner.GetPlannerInstance().HUD.Draw(e.Graphics);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            onPaintThread = null;

			System.Diagnostics.Debug.WriteLine("map draw time " + (DateTime.Now - start).TotalMilliseconds);
		}

        public Brush ScaleForeBrush { get; set; } = Brushes.Black;
        public Brush ScaleBackBrush = Brushes.White;
        public Font ScaleFont { get; set; } = SystemFonts.SmallCaptionFont;
        public Point ScalePosition { get; set; } = new Point(10, -50);
        public Size ScaleSize { get; set; } = new Size(20, 10);

        private void DrawScale(Graphics g, int x, int y, int scale, int rezs, bool fg)
        {
            var brush = fg ? ScaleForeBrush : ScaleBackBrush;
            g.FillRectangle(brush, x, y, rezs, ScaleSize.Height);
            g.DrawString(scale > 1000 ? $"{scale / 1000}km" : $"{scale}m",
                ScaleFont, ScaleForeBrush, x + rezs - 5, y + ScaleSize.Height + 1);
        }

        private void DrawCustomMapScale(Graphics g)
        {
            double rez = MapProvider.Projection.GetGroundResolution((int)Zoom, Position.Lat);
            int maxw = Width / 3, minw = ScaleSize.Width;
            int scale = 10000000, rezs;
            bool minproof = true;
            int x = ScalePosition.X, y = ScalePosition.Y;
            if (x < 0) x += Width;
            if (y < 0) y += Height;

            do
            {
                scale /= 2;
                rezs = (int)(scale / rez);
                if (minw > rezs) break;
                if (maxw > rezs) DrawScale(g, x, y, scale, rezs, false);
                minproof = false;
                scale /= 5;
                rezs = (int)(scale / rez);
                if (minw > rezs) break;
                if (maxw > rezs) DrawScale(g, x, y, scale, rezs, true);
                minproof = false;
            } while (scale > 1);
            if (minproof) DrawScale(g, x, y, scale, rezs, true);
            g.DrawString(scale > 500 ? "0km" : "0m",
                ScaleFont, ScaleForeBrush, x - 5, y + ScaleSize.Height + 1);
        }

        protected override void OnMouseMove(MouseEventArgs e)
		{
			try
			{
				var buffer = 1;
				if (e.X >= lastMouseX - buffer && e.X <= lastMouseX + buffer && e.Y >= lastMouseY - buffer && e.Y <= lastMouseY + buffer)
					return;

				if (HoldInvalidation)
					return;

				lastMouseX = e.X;
				lastMouseY = e.Y;

                lock (Overlays)
                {
                    base.OnMouseMove(e);
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

		}

        private Rectangle msgRect;
        private readonly Timer msgRefreshTimer = new Timer { Interval = 500 };
        public PointF MsgWindowOffset { get; set; } = new PointF();

        private void InvalidateMessage(object sender, EventArgs e)
        {
            if (msgRect.Height > 0 || sender is FltMsg)
                Invalidate(msgRect);
        }

        private static void DrawText(Graphics g, string s, Font f, Brush b,
            ref float x, float y, bool rmost = false)
        {
            if (string.IsNullOrEmpty(s)) return;
            var sz = g.MeasureString(s + "-", f);
            if (rmost)
            {
                x -= sz.Width;
                g.DrawString(s, f, b, x, y);
            }
            else
            {
                g.DrawString(s, f, b, x, y);
                x += sz.Width;
            }
        }

        private void DrawMessage(Graphics g)
        {
            var msgs = FltMsg.GetMessages(DebugMode ? 7 : 6);
            if (msgs.Length > 0)
            {
                Font f = FltMsg.MsgFont;
                SizeF fsize = g.MeasureString("W", f);
                float lh = fsize.Height + 2;
                float cw = fsize.Width;
                float width = cw * 40;
                if (width > Width / 2 - 8)
                    width = Width / 2 - 8;
                float top = Height - lh * msgs.Length - 6;
                float left = Width - width - 8;
                msgRect = Rectangle.Round(new RectangleF(left, top, width + 4, lh * msgs.Length + 2));
                g.FillRectangle(FltMsg.BgBrush, msgRect);

                float lt = top + 4 + MsgWindowOffset.Y;
                bool blinking = DateTime.Now.Second % 2 == 0;
                foreach (var m in msgs)
                {
                    float ll = left + 4 + MsgWindowOffset.X;
                    DrawText(g, m.Source, FltMsg.SrcFont, m.SrcBrush, ref ll, lt);
                    Brush b = FltMsg.MsgBlink[m.Severity] && blinking ?
                        FltMsg.BlinkBrush : FltMsg.MsgBrushes[m.Severity];
                    DrawText(g, m.Message, FltMsg.MsgFont, b, ref ll, lt);
                    float r = Width;
                    DrawText(g, m.TimeStr, FltMsg.TimeFont, FltMsg.TimeBrush, ref r, lt, true);
                    lt += lh;
                }
            }
            else
                msgRect.Height = 0;
        }

        protected override void OnPaintOverlays(Graphics g)
        {
            base.OnPaintOverlays(g);
            if (DebugMode && IndoorMode)
            {
                g.DrawString($"Zoom level: {Zoom}, Center: {Position.Lat}, {Position.Lng}",
                    SystemFonts.SmallCaptionFont, Brushes.Blue, 20, Height - 20);
            }
            DrawMessage(g);
        }

        private bool markingCache = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            markingCache = (ModifierKeys == Keys.Alt);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (markingCache)
            {
                markingCache = false;
                CacheSelection();
            }
            base.OnMouseUp(e);
        }

        private void CacheSelection()
        {
            if (!(GMaps.Instance.PrimaryCache is
                GMap.NET.CacheProviders.SQLitePureImageCache cache))
                return;
            RectLatLng sel = SelectedArea;
            if (sel.IsEmpty) return;
            //int zmax = MapProvider.MaxZoom ?? 20;
            //for (var z = MapProvider.MinZoom; z < zmax; z++)
            int z = 19;
            {
                using (TilePrefetcher pf = new TilePrefetcher())
                {
                    pf.ShowCompleteMessage = false;
                    pf.Shuffle = true;
                    pf.Start(sel, z, MapProvider, 100, 3);
                }
            }
            GMaps.Instance.ExportToGMDB(cache.CacheLocation);
        }
    }
}
