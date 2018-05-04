using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Vlc.DotNet.Forms;

namespace Diva
{
    class VideoPlayer
    {
        public VlcControl Player { get; }
        public string UriStr { get; set; }
        public static implicit operator Control(VideoPlayer player) { return player.Player; }

        public VideoPlayer(string src, bool debug = false)
        {
            UriStr = src;
            Player = new VlcControl();
            Player.Top = Player.Left = 0;
            Player.VlcMediaplayerOptions = debug ?
                new string[] { "-vvv", "--extraintf=logger", "--verbose=2", "--logfile=vlclogs.log" } : null;
            Player.VlcLibDirectory = new DirectoryInfo(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc"));
            Player.VlcLibDirectoryNeeded += (o, ex) =>
            {
                // error handling
                ex.VlcLibDirectory = new DirectoryInfo("");
            };
            Player.EncounteredError += (o, ex) =>
            {
                Console.WriteLine("VlcControl encountered error: " + ex.ToString());
            };
            Player.Dock = DockStyle.Fill;
            ((System.ComponentModel.ISupportInitialize)(Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(Player)).EndInit();
        }

        public void Start() { Player.Play(new Uri(UriStr)); }
        public void Start(string[] opts) { Player.Play(new Uri(UriStr), opts); }
        public void Stop() { Player.Stop(); }
    }
}
