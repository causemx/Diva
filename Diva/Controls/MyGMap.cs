using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Net;
using System.Threading;

namespace Diva
{
    public class FloatMessage
    {
        private const int BasicDisplayTime = 10;
        private const int SeverityExtendedTime = 16;
        private static readonly List<FloatMessage> floatMessages = new List<FloatMessage>();
        public readonly static Brush[] MsgBrushes =
        {
            Brushes.Magenta,
            Brushes.Red,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.GreenYellow,
            Brushes.Cyan,
            Brushes.Gray,
            Brushes.DimGray
        };
        public readonly static Brush BgBrushes = new SolidBrush(Color.FromArgb(127, Color.Black));

        public static void NewMessage(int severity, string message, int timeout = 0)
        {
            lock (floatMessages)
                floatMessages.Add(new FloatMessage(severity, message, timeout));
        }

        public static FloatMessage[] GetMessages()
        {
            lock (floatMessages)
            {
                floatMessages.RemoveAll(m => m.Due < DateTime.Now);
                return floatMessages.ToArray();
            }
        }

        public readonly int Severity;
        public readonly string Message;
        public readonly DateTime Due;

        private FloatMessage(int severity, string message, int timeout)
        {
            Severity = severity;
            Message = message;
            Due = DateTime.Now.AddSeconds(timeout > 0 ? timeout :
                BasicDisplayTime + (SeverityExtendedTime / (1 + severity)));
        }
    }
}

namespace Diva.Controls
{
	public class MyGMap : GMapControl
	{
        public readonly GMapProvider GlobalMapProvider = BingSatelliteMapProvider.Instance;
        public bool DebugMode { get; set; }
        public bool IndoorMode { get; private set; }
        Thread onPaintThread = null;
		int lastx = 0;
		int lasty = 0;

        public MyGMap() : base()
		{
            MapScaleInfoEnabled = false;
            DisableFocusOnMouseEnter = true;
            //RoutesEnabled = true; // set by designer
            ForceDoubleBuffer = false;
            DebugMode = true;
            msgRefreshTimer.Interval = 1000;
            msgRefreshTimer.Tick += InvalidateMessage;
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
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            onPaintThread = null;

			System.Diagnostics.Debug.WriteLine("map draw time " + (DateTime.Now - start).TotalMilliseconds);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			try
			{
				var buffer = 1;
				if (e.X >= lastx - buffer && e.X <= lastx + buffer && e.Y >= lasty - buffer && e.Y <= lasty + buffer)
					return;

				if (HoldInvalidation)
					return;

				lastx = e.X;
				lasty = e.Y;

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
        private readonly System.Windows.Forms.Timer msgRefreshTimer =
            new System.Windows.Forms.Timer();

        private void InvalidateMessage(object sender, EventArgs e)
        {
            if (msgRect.Height > 0)
                Invalidate(msgRect);
        }

        private void DrawMessage(Graphics g)
        {
            Font f = SystemFonts.MessageBoxFont;
            var msgs = FloatMessage.GetMessages();
            if (msgs.Length > 0)
            {
                var fsize = g.MeasureString("A", f);
                float lineheight = fsize.Height + 2;
                float width = fsize.Width * 40;
                if (width > Width / 2)
                    width = Width / 2;
                float top = Height - lineheight * msgs.Length;
                float left = Width - width;
                msgRect = Rectangle.Round(new RectangleF(left, top, width, lineheight * msgs.Length));
                g.FillRectangle(FloatMessage.BgBrushes, msgRect);
                for (int i = 0; i < msgs.Length; i++, top += lineheight)
                {
                    int s = msgs[i].Severity;
                    if (s < 7 || DebugMode)
                        g.DrawString(msgs[i].Message, f,
                            FloatMessage.MsgBrushes[s], left, top);
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
            var cache = GMaps.Instance.PrimaryCache as GMap.NET.CacheProviders.SQLitePureImageCache;
            if (cache == null) return;
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
