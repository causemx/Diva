
namespace Diva.Controls
{
    partial class VideoStreamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vlcControl = new Vlc.DotNet.Forms.VlcControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.myLblAudioRate = new System.Windows.Forms.Label();
            this.myLblAudioChannels = new System.Windows.Forms.Label();
            this.myLblAudioCodec = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.myLblVideoWidth = new System.Windows.Forms.Label();
            this.myLblVideoHeight = new System.Windows.Forms.Label();
            this.myLblVideoCodec = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.myCbxAspectRatio = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // vlcControl
            // 
            this.vlcControl.BackColor = System.Drawing.Color.Black;
            this.vlcControl.Location = new System.Drawing.Point(12, 12);
            this.vlcControl.Name = "vlcControl";
            this.vlcControl.Size = new System.Drawing.Size(650, 390);
            this.vlcControl.Spu = -1;
            this.vlcControl.TabIndex = 0;
            this.vlcControl.Text = "vlcControl1";
            this.vlcControl.VlcLibDirectory = null;
            this.vlcControl.VlcMediaplayerOptions = null;
            this.vlcControl.VlcLibDirectoryNeeded += new System.EventHandler<Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs>(this.OnVlcControlNeedLibDirectory);
            this.vlcControl.Log += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerLogEventArgs>(this.vlcControl_Log);
            this.vlcControl.Playing += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(this.OnVlcPlaying);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.myLblAudioRate);
            this.groupBox1.Controls.Add(this.myLblAudioChannels);
            this.groupBox1.Controls.Add(this.myLblAudioCodec);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(682, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Audio informations";
            // 
            // myLblAudioRate
            // 
            this.myLblAudioRate.AutoSize = true;
            this.myLblAudioRate.Location = new System.Drawing.Point(20, 72);
            this.myLblAudioRate.Name = "myLblAudioRate";
            this.myLblAudioRate.Size = new System.Drawing.Size(36, 15);
            this.myLblAudioRate.TabIndex = 2;
            this.myLblAudioRate.Text = "Rate:";
            // 
            // myLblAudioChannels
            // 
            this.myLblAudioChannels.AutoSize = true;
            this.myLblAudioChannels.Location = new System.Drawing.Point(20, 49);
            this.myLblAudioChannels.Name = "myLblAudioChannels";
            this.myLblAudioChannels.Size = new System.Drawing.Size(62, 15);
            this.myLblAudioChannels.TabIndex = 1;
            this.myLblAudioChannels.Text = "Channels:";
            // 
            // myLblAudioCodec
            // 
            this.myLblAudioCodec.AutoSize = true;
            this.myLblAudioCodec.Location = new System.Drawing.Point(20, 26);
            this.myLblAudioCodec.Name = "myLblAudioCodec";
            this.myLblAudioCodec.Size = new System.Drawing.Size(48, 15);
            this.myLblAudioCodec.TabIndex = 0;
            this.myLblAudioCodec.Text = "Codec: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.myLblVideoWidth);
            this.groupBox2.Controls.Add(this.myLblVideoHeight);
            this.groupBox2.Controls.Add(this.myLblVideoCodec);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(682, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Video informations";
            // 
            // myLblVideoWidth
            // 
            this.myLblVideoWidth.AutoSize = true;
            this.myLblVideoWidth.Location = new System.Drawing.Point(20, 72);
            this.myLblVideoWidth.Name = "myLblVideoWidth";
            this.myLblVideoWidth.Size = new System.Drawing.Size(41, 15);
            this.myLblVideoWidth.TabIndex = 5;
            this.myLblVideoWidth.Text = "Width:";
            // 
            // myLblVideoHeight
            // 
            this.myLblVideoHeight.AutoSize = true;
            this.myLblVideoHeight.Location = new System.Drawing.Point(20, 49);
            this.myLblVideoHeight.Name = "myLblVideoHeight";
            this.myLblVideoHeight.Size = new System.Drawing.Size(46, 15);
            this.myLblVideoHeight.TabIndex = 4;
            this.myLblVideoHeight.Text = "Height:";
            // 
            // myLblVideoCodec
            // 
            this.myLblVideoCodec.AutoSize = true;
            this.myLblVideoCodec.Location = new System.Drawing.Point(20, 26);
            this.myLblVideoCodec.Name = "myLblVideoCodec";
            this.myLblVideoCodec.Size = new System.Drawing.Size(48, 15);
            this.myLblVideoCodec.TabIndex = 3;
            this.myLblVideoCodec.Text = "Codec: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(682, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 100);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Extra";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(682, 335);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Video Aspect Ratio: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(682, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Message:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(682, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "State:";
            // 
            // myCbxAspectRatio
            // 
            this.myCbxAspectRatio.FormattingEnabled = true;
            this.myCbxAspectRatio.Items.AddRange(new object[] {
            "16:9",
            "16:10",
            "4:3"});
            this.myCbxAspectRatio.Location = new System.Drawing.Point(803, 332);
            this.myCbxAspectRatio.Name = "myCbxAspectRatio";
            this.myCbxAspectRatio.Size = new System.Drawing.Size(121, 20);
            this.myCbxAspectRatio.TabIndex = 7;
            this.myCbxAspectRatio.TextChanged += new System.EventHandler(this.myCbxAspectRatio_TextChanged);
            // 
            // VideoStreamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 417);
            this.Controls.Add(this.myCbxAspectRatio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.vlcControl);
            this.Name = "VideoStreamForm";
            this.Text = "VideoStreamForm";
            this.SizeChanged += new System.EventHandler(this.Form_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Vlc.DotNet.Forms.VlcControl vlcControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox myCbxAspectRatio;
        private System.Windows.Forms.Label myLblAudioRate;
        private System.Windows.Forms.Label myLblAudioChannels;
        private System.Windows.Forms.Label myLblAudioCodec;
        private System.Windows.Forms.Label myLblVideoWidth;
        private System.Windows.Forms.Label myLblVideoHeight;
        private System.Windows.Forms.Label myLblVideoCodec;
    }
}