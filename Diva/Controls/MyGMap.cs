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

            OnSelectionChange += (s, z) =>
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
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            ResetMapProvider();

            base.OnLoad(e);
        }

        public void ResetMapProvider()
        {
            IndoorMode = false;
            if (ConfigData.GetBoolOption(ConfigData.OptionName.UseImageMap)) try
            {
                var p = new ImageMapProvider(ConfigData.GetOption(ConfigData.OptionName.ImageMapSource));
                if (p != null)
                {
                    MapProvider = p;
                    IndoorMode = true;
                    MaxZoom = p.MaxZoom ?? MaxZoom;
                    MinZoom = p.MinZoom;
                    if (Zoom > MaxZoom) Zoom = MaxZoom;
                    if (Zoom < MinZoom) Zoom = MinZoom;
                    if (p.Area != null)
                        Position = ((RectLatLng)p.Area).LocationMiddle;
                    GMaps.Instance.Mode = AccessMode.ServerOnly;
                    /*if (DebugMapLocation)
                        ShowTileGridLines = true;*/
                    return;
                }
                ConfigData.SetBoolOption(ConfigData.OptionName.UseImageMap, false);
            }
            catch { }
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            string proxy = ConfigData.GetOption(ConfigData.OptionName.MapProxy);
            string[] prox = proxy.Split(':');
            if (prox.Length == 2)
            {
                int.TryParse(prox[1], out int port);
                GMapProvider.WebProxy = new WebProxy(prox[0], port);
            }
            else
                GMapProvider.WebProxy = WebRequest.DefaultWebProxy;
            MapProvider = GoogleSatelliteMapProvider.Instance;
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

				base.OnMouseMove(e);
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

    }
}
