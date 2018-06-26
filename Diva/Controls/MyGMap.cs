﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public class MyGMap : GMap.NET.WindowsForms.GMapControl
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

        }

        protected override void OnLoad(EventArgs e)
        {
            MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            MinZoom = 0;
            MaxZoom = 24;
            Zoom = 15;
            base.OnLoad(e);
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
