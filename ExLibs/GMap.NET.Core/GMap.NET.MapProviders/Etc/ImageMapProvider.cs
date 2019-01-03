using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace GMap.NET.MapProviders
{
    public class ImageMapProvider : GMapProvider, IDisposable
    {
        Guid id = Guid.NewGuid();
        string name;
        public int OriginalZoom { get; private set; }
        public PointLatLng Center { get; private set; }
        int tile_left;
        int tile_top;
        int tiles_width;
        int tiles_height;
        Image image;

        public ImageMapProvider(string filename)
        {
            // naming scheme: <mapname>_<tile_x>_<tile_y>_<zoom_level>.ext
            var fn = new FileInfo(filename).Name;
            var match = new System.Text.RegularExpressions.Regex(
                @"^([^_]+)_(\d+)_(\d+)_(\d+)\.([jJ][pP][gG]|[pP][nN][gG]|[bB][mM][pP])$")
                .Match(fn);
            if (match.Success) try
            {
                name = match.Groups[1].ToString();
                tile_left = int.Parse(match.Groups[2].ToString());
                tile_top = int.Parse(match.Groups[3].ToString());
                OriginalZoom = int.Parse(match.Groups[4].ToString());
                image = Image.FromFile(filename);
                tiles_width = (image.Width - 1) / 256 + 1;
                tiles_height = (image.Height - 1) / 256 + 1;
                int shrink = int.MaxValue;
                foreach (var value in new int[] { tile_left, tile_top, tiles_width, tiles_height })
                    shrink = Math.Min(shrink, value & -value);
                shrink = (int)Math.Log(shrink, 2);
                //int shrink = (int)Math.Log(Math.Min(tiles_width, tiles_height), 2);
                MaxZoom = OriginalZoom + 4;
                MinZoom = OriginalZoom - shrink;
                var lt = Projection.FromPixelToLatLng(Projection.FromTileXYToPixel(
                    new GPoint(tile_left, tile_top)), OriginalZoom);
                var rb = Projection.FromPixelToLatLng(Projection.FromTileXYToPixel(
                    new GPoint(tile_left + tiles_width, tile_top + tiles_height)), OriginalZoom);
                Area = RectLatLng.FromLTRB(lt.Lng, lt.Lat, rb.Lng, rb.Lat);
                //Console.WriteLine($"Load Image Map {filename}: {tiles_width}x{tiles_height} tiles, max shrink {shrink} level.");
            }
            catch { }
        }

        #region GMapProvider Members

        public override Guid Id => id;

        public override string Name => name;

        public override PureProjection Projection => Projections.MercatorProjection.Instance;

        GMapProvider[] overlays;
        public override GMapProvider[] Overlays => overlays ?? (overlays = new GMapProvider[] { this });

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            int x = (int)pos.X, y = (int)pos.Y;
            if (zoom < MinZoom || zoom > MaxZoom)
                return TileImageProxy.GenerateDebugTile("Out of bound", x, y, zoom);

            int l, t, w, h, z = zoom - OriginalZoom;
            if (z < 0)
            {
                z = -z;
                l = ((x << z) - tile_left) * 256;
                t = ((y << z) - tile_top) * 256;
                w = h = 256 << z;
            }
            else
            {
                l = (x - (tile_left << z)) * (256 >> z);
                t = (y - (tile_top << z)) * (256 >> z);
                w = h = 256 >> z;
            }
            if (l < 0 || t < 0 || l + w > tiles_width * 256 || t + h > tiles_height * 256)
                return TileImageProxy.GenerateDebugTile("Out of bound", x, y, zoom);

            if (image == null)
                return TileImageProxy.GenerateDebugTile("Error loading image", x, y, zoom);

            PureImage ret;
            Image src;
            lock (image) { src = image.Clone() as Image; }
            if (src == null)
                return TileImageProxy.GenerateDebugTile("Error loading image", x, y, zoom);

            //Console.WriteLine($"Generate tile {x},{y},{zoom} using left top {l},{t} of size {w} in image.");
            using (var dest = new Bitmap(256, 256))
            using (var g = Graphics.FromImage(dest))
            using (var wrap = new System.Drawing.Imaging.ImageAttributes())
            {
                float scale = (float)Math.Pow(2, zoom - OriginalZoom);
                dest.SetResolution(src.HorizontalResolution * scale,
                    src.VerticalResolution * scale);

                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                wrap.SetWrapMode(WrapMode.TileFlipXY);

                g.DrawImage(src, new Rectangle(0, 0, 256, 256), l, t, w, h, GraphicsUnit.Pixel, wrap);
                ret = TileImageProxy.FromImage(dest);
            }
            src.Dispose();
            return ret;
        }
        #endregion

        public void Dispose() { image?.Dispose(); }
    }
}
