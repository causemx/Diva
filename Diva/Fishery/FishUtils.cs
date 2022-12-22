using Diva.Properties;
using Diva.Utilities;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Fishery
{
    class FishUtils
    {
        public static void AddFishMarkerGrid(string tag, string context, PointLatLng p)
        {
			try
			{
				var overlays = Planner.GetPlannerInstance().GMapControl?.Overlays;
				var overlay = new GMapOverlay("Fish");
				

				GMarkerGoogle m = new GMarkerGoogle(p, GMarkerGoogleType.orange_dot)
				{
					ToolTipMode = MarkerTooltipMode.Never,
					ToolTipText = context,
					Tag = "fish_"+ tag
				};

				//MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
				GMapRectMarker mBorders = new GMapRectMarker(p) { InnerMarker = m };

				overlay.Markers.Add(m);
				overlay.Markers.Add(mBorders);
				overlays.Add(overlay);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
    }
}
