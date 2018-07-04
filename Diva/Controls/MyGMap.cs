using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace Diva.Controls
{
	public class MyGMap : GMapControl
	{
		public bool inOnPaint = false;
		string otherthread = "";
		int lastx = 0;
		int lasty = 0;
		public MyGMap() : base()
		{
            MapScaleInfoEnabled = false;
            DisableFocusOnMouseEnter = true;
            //RoutesEnabled = true; // set by designer
            ForceDoubleBuffer = false;

            OnSelectionChange += (s, z) =>
            {
                if (MapProvider == GoogleSatelliteMapProvider.Instance && !s.IsEmpty)
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

            MinZoom = 0;
            MaxZoom = 24;
            Zoom = 15;

            base.OnLoad(e);
        }

        public void ResetMapProvider()
        {
            if (ConfigData.GetOption(ConfigData.OptionName.UseImageMap) == true.ToString()) try
            {
                var p = new ImageMapProvider(ConfigData.GetOption(ConfigData.OptionName.ImageMapSource));
                if (p != null)
                {
                    MapProvider = p;
                    return;
                }
                ConfigData.SetOption(ConfigData.OptionName.UseImageMap, false.ToString());
            }
            catch { }
            MapProvider = GoogleSatelliteMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

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

	}
}
