using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Net;

namespace Diva.Controls
{
	public class MyGMap : GMapControl
	{
        public readonly GMapProvider GlobalMapProvider = BingSatelliteMapProvider.Instance;
        public bool inOnPaint = false;
        public bool DebugMapLocation { get; set; }
        public bool IndoorMode { get; private set; }
		string otherthread = "";
		int lastx = 0;
		int lasty = 0;
		public MyGMap() : base()
		{
            MapScaleInfoEnabled = false;
            DisableFocusOnMouseEnter = true;
            //RoutesEnabled = true; // set by designer
            ForceDoubleBuffer = false;
            DebugMapLocation = true;

            /*OnSelectionChange += (s, z) =>
            {
                if (!IndoorMode && !s.IsEmpty)
                {
                    for (int i = 0; i < MapProvider.MaxZoom; i++)
                    {
                        using (TilePrefetcher pf = new TilePrefetcher())
                        {
                            pf.ShowCompleteMessage = false;
                            pf.Shuffle = true;
                            pf.Start(s, i, MapProvider, 100, 3);
                        }
                    }
                }
                SelectedArea = new RectLatLng();
            };*/
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

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			var start = DateTime.Now;

			if (inOnPaint)
			{
				Console.WriteLine("Was in onpaint Gmap th:" + System.Threading.Thread.CurrentThread.Name + " in " + otherthread);
				return;
			}

			otherthread = System.Threading.Thread.CurrentThread.Name;

			inOnPaint = true;

			try
			{
				base.OnPaint(e);
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }

			inOnPaint = false;

			var end = DateTime.Now;

			System.Diagnostics.Debug.WriteLine("map draw time " + (end - start).TotalMilliseconds);
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

        protected override void OnPaintOverlays(Graphics g)
        {
            Font f = SystemFonts.SmallCaptionFont;
            base.OnPaintOverlays(g);
            if (DebugMapLocation && IndoorMode)
            {
                g.DrawString($"Zoom level: {Zoom}, Center: {Position.Lat}, {Position.Lng}",
                    f, Brushes.Blue, 20, Height - 20);
            }
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
