using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Core.Interops.Signatures;
using Vlc.DotNet.Forms;

namespace Diva.Controls
{
    public partial class VideoStreamForm : Form
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public VideoStreamForm()
        {
            InitializeComponent();

            if (vlcControl.Audio != null)
            {
                log.Debug("Audio is ready");
            }
        }

        public void Play(string address)
        {
            try
            {
                vlcControl.Play(new Uri(address));
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }

        private void OnVlcControlNeedLibDirectory(object sender, VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            if (!e.VlcLibDirectory.Exists)
            {
                var folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "Select Vlc libraries folder.";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.ProgramFiles;
                folderBrowserDialog.ShowNewFolderButton = true;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    e.VlcLibDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void OnVlcPlaying(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(() =>
            {
                var mediaInformations = vlcControl.GetCurrentMedia().Tracks;
                foreach (var mediaInformation in mediaInformations)
                {
                    if (mediaInformation.Type == MediaTrackTypes.Audio)
                    {
                        myLblAudioCodec.Text += FourCCConverter.FromFourCC(mediaInformation.CodecFourcc);
                        var audioTrack = mediaInformation.TrackInfo as AudioTrack;
                        myLblAudioChannels.Text += audioTrack?.Channels.ToString() ?? "null";
                        myLblAudioRate.Text += audioTrack?.Rate.ToString() ?? "null";
                    }
                    else if (mediaInformation.Type == MediaTrackTypes.Video)
                    {
                        myLblVideoCodec.Text += FourCCConverter.FromFourCC(mediaInformation.CodecFourcc);
                        var videoTrack = mediaInformation.TrackInfo as VideoTrack;
                        myLblVideoHeight.Text += videoTrack?.Height.ToString() ?? "null";
                        myLblVideoWidth.Text += videoTrack?.Width.ToString() ?? "null";
                    }
                    else if (mediaInformation.Type == MediaTrackTypes.Text)
                    {
                        myLblVideoCodec.Text += FourCCConverter.FromFourCC(mediaInformation.CodecFourcc);
                        var subtitleTrack = mediaInformation.TrackInfo as SubtitleTrack;
                        myLblVideoHeight.Text += subtitleTrack?.Encoding;
                    }
                }
            });
            this.BeginInvoke(mi, null);
        }

        private void myCbxAspectRatio_TextChanged(object sender, EventArgs e)
        {
            vlcControl.Video.AspectRatio = myCbxAspectRatio.Text;
            ResizeVlcControl();
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            ResizeVlcControl();
        }

        void ResizeVlcControl()
        {
            if (!string.IsNullOrEmpty(myCbxAspectRatio.Text))
            {
                var ratio = myCbxAspectRatio.Text.Split(':');
                int width, height;
                if (ratio.Length == 2 && int.TryParse(ratio[0], out width) && int.TryParse(ratio[1], out height))
                    vlcControl.Width = vlcControl.Height * width / height;
            }
        }

        private void vlcControl_Log(object sender, Vlc.DotNet.Core.VlcMediaPlayerLogEventArgs e)
        {
            string message = string.Format("libVlc : {0} {1} @ {2}", e.Level, e.Message, e.Module);
            System.Diagnostics.Debug.WriteLine(message);
        }

    }
}
