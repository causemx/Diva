using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Utilities
{
    interface IOverlayUtility
    {
        void CreateOverlay(PointLatLngAlt home, List<Locationwp> missionitems, double wpradius, double loiterradius);
        void addpolygonmarker(string tag, double lng, double lat, double alt, Color? color, double wpradius);
        void RegenerateWPRoute(List<PointLatLngAlt> fullpointlist, PointLatLngAlt HomeLocation);
    }
}
