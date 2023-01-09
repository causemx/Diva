using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Core.Interops.Signatures;
using Vlc.DotNet.Forms;

namespace Diva.Controls
{
    public partial class LivestreamForm : Form
    {
        private GroupBox myGrpAudioInformations;
        private Label myLblAudioRate;
        private Label myLblAudioChannels;
        private Label myLblAudioCodec;
        private GroupBox myGrpVideoInformations;
        private Label myLblVideoWidth;
        private Label myLblVideoHeight;
        private Label myLblVideoCodec;
        private Label label4;
        private Label label3;
        private ComboBox myCbxAspectRatio;
        private Label label1;
        private VlcControl vlcControl;
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public LivestreamForm()
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
            } catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }

        private void OnVlcControlNeedLibDirectory(object sender, VlcLibDirectoryNeededEventArgs e)
        {
            try
            {
                var currentAssembly = Assembly.GetEntryAssembly();
                var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
                // Default installation path of VideoLAN.LibVLC.Windows
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

                if (!e.VlcLibDirectory.Exists)
                {
                    var folderBrowserDialog = new FolderBrowserDialog();
                    folderBrowserDialog.Description = "Select Vlc libraries folder.";
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    folderBrowserDialog.ShowNewFolderButton = true;
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        e.VlcLibDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                    }
                }
            } catch(Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }

        private void OnVlcPlaying(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(()=>
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

        }

        private void LivestreamForm_SizeChanged(object sender, EventArgs e)
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

        private void InitializeComponent()
        {
            this.myGrpAudioInformations = new System.Windows.Forms.GroupBox();
            this.myLblAudioRate = new System.Windows.Forms.Label();
            this.myLblAudioChannels = new System.Windows.Forms.Label();
            this.myLblAudioCodec = new System.Windows.Forms.Label();
            this.myGrpVideoInformations = new System.Windows.Forms.GroupBox();
            this.myLblVideoWidth = new System.Windows.Forms.Label();
            this.myLblVideoHeight = new System.Windows.Forms.Label();
            this.myLblVideoCodec = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.myCbxAspectRatio = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.vlcControl = new Vlc.DotNet.Forms.VlcControl();
            this.myGrpAudioInformations.SuspendLayout();
            this.myGrpVideoInformations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl)).BeginInit();
            this.SuspendLayout();
            // 
            // myGrpAudioInformations
            // 
            this.myGrpAudioInformations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.myGrpAudioInformations.Controls.Add(this.myLblAudioRate);
            this.myGrpAudioInformations.Controls.Add(this.myLblAudioChannels);
            this.myGrpAudioInformations.Controls.Add(this.myLblAudioCodec);
            this.myGrpAudioInformations.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myGrpAudioInformations.Location = new System.Drawing.Point(687, 12);
            this.myGrpAudioInformations.Name = "myGrpAudioInformations";
            this.myGrpAudioInformations.Size = new System.Drawing.Size(252, 100);
            this.myGrpAudioInformations.TabIndex = 2;
            this.myGrpAudioInformations.TabStop = false;
            this.myGrpAudioInformations.Text = "Audio informations";
            // 
            // myLblAudioRate
            // 
            this.myLblAudioRate.AutoSize = true;
            this.myLblAudioRate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLblAudioRate.Location = new System.Drawing.Point(12, 73);
            this.myLblAudioRate.Name = "myLblAudioRate";
            this.myLblAudioRate.Size = new System.Drawing.Size(39, 15);
            this.myLblAudioRate.TabIndex = 10;
            this.myLblAudioRate.Text = "Rate: ";
            // 
            // myLblAudioChannels
            // 
            this.myLblAudioChannels.AutoSize = true;
            this.myLblAudioChannels.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLblAudioChannels.Location = new System.Drawing.Point(12, 49);
            this.myLblAudioChannels.Name = "myLblAudioChannels";
            this.myLblAudioChannels.Size = new System.Drawing.Size(67, 15);
            this.myLblAudioChannels.TabIndex = 9;
            this.myLblAudioChannels.Text = "Channels: ";
            // 
            // myLblAudioCodec
            // 
            this.myLblAudioCodec.AutoSize = true;
            this.myLblAudioCodec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLblAudioCodec.Location = new System.Drawing.Point(12, 25);
            this.myLblAudioCodec.Name = "myLblAudioCodec";
            this.myLblAudioCodec.Size = new System.Drawing.Size(49, 15);
            this.myLblAudioCodec.TabIndex = 8;
            this.myLblAudioCodec.Text = "Codec: ";
            // 
            // myGrpVideoInformations
            // 
            this.myGrpVideoInformations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.myGrpVideoInformations.Controls.Add(this.myLblVideoWidth);
            this.myGrpVideoInformations.Controls.Add(this.myLblVideoHeight);
            this.myGrpVideoInformations.Controls.Add(this.myLblVideoCodec);
            this.myGrpVideoInformations.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myGrpVideoInformations.Location = new System.Drawing.Point(687, 118);
            this.myGrpVideoInformations.Name = "myGrpVideoInformations";
            this.myGrpVideoInformations.Size = new System.Drawing.Size(252, 100);
            this.myGrpVideoInformations.TabIndex = 3;
            this.myGrpVideoInformations.TabStop = false;
            this.myGrpVideoInformations.Text = "Video informations";
            // 
            // myLblVideoWidth
            // 
            this.myLblVideoWidth.AutoSize = true;
            this.myLblVideoWidth.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLblVideoWidth.Location = new System.Drawing.Point(12, 74);
            this.myLblVideoWidth.Name = "myLblVideoWidth";
            this.myLblVideoWidth.Size = new System.Drawing.Size(44, 15);
            this.myLblVideoWidth.TabIndex = 13;
            this.myLblVideoWidth.Text = "Width: ";
            // 
            // myLblVideoHeight
            // 
            this.myLblVideoHeight.AutoSize = true;
            this.myLblVideoHeight.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLblVideoHeight.Location = new System.Drawing.Point(12, 50);
            this.myLblVideoHeight.Name = "myLblVideoHeight";
            this.myLblVideoHeight.Size = new System.Drawing.Size(49, 15);
            this.myLblVideoHeight.TabIndex = 12;
            this.myLblVideoHeight.Text = "Height: ";
            // 
            // myLblVideoCodec
            // 
            this.myLblVideoCodec.AutoSize = true;
            this.myLblVideoCodec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLblVideoCodec.Location = new System.Drawing.Point(12, 26);
            this.myLblVideoCodec.Name = "myLblVideoCodec";
            this.myLblVideoCodec.Size = new System.Drawing.Size(49, 15);
            this.myLblVideoCodec.TabIndex = 11;
            this.myLblVideoCodec.Text = "Codec: ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(687, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Message: ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(687, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "State: ";
            // 
            // myCbxAspectRatio
            // 
            this.myCbxAspectRatio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.myCbxAspectRatio.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myCbxAspectRatio.FormattingEnabled = true;
            this.myCbxAspectRatio.ItemHeight = 15;
            this.myCbxAspectRatio.Items.AddRange(new object[] {
            "16:9",
            "16:10",
            "4:3"});
            this.myCbxAspectRatio.Location = new System.Drawing.Point(802, 227);
            this.myCbxAspectRatio.Name = "myCbxAspectRatio";
            this.myCbxAspectRatio.Size = new System.Drawing.Size(121, 23);
            this.myCbxAspectRatio.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(687, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Video Aspect Ratio: ";
            // 
            // vlcControl
            // 
            this.vlcControl.BackColor = System.Drawing.Color.Black;
            this.vlcControl.Location = new System.Drawing.Point(12, 12);
            this.vlcControl.Name = "vlcControl";
            this.vlcControl.Size = new System.Drawing.Size(660, 388);
            this.vlcControl.Spu = -1;
            this.vlcControl.TabIndex = 13;
            this.vlcControl.Text = "vlcControl1";
            this.vlcControl.VlcLibDirectory = null;
            this.vlcControl.VlcMediaplayerOptions = null;
            this.vlcControl.VlcLibDirectoryNeeded += new System.EventHandler<Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs>(this.OnVlcControlNeedLibDirectory);
            this.vlcControl.Playing += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(this.OnVlcPlaying);
            this.vlcControl.SizeChanged += new System.EventHandler(this.LivestreamForm_SizeChanged);
            // 
            // LivestreamForm
            // 
            this.ClientSize = new System.Drawing.Size(949, 412);
            this.Controls.Add(this.vlcControl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.myCbxAspectRatio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myGrpVideoInformations);
            this.Controls.Add(this.myGrpAudioInformations);
            this.Name = "LivestreamForm";
            this.Text = "LivestreamForm";
            this.myGrpAudioInformations.ResumeLayout(false);
            this.myGrpAudioInformations.PerformLayout();
            this.myGrpVideoInformations.ResumeLayout(false);
            this.myGrpVideoInformations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
