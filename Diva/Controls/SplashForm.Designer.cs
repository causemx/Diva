namespace Diva
{
    partial class SplashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            this.lblProgress = new System.Windows.Forms.Label();
            this.BorderPanel = new System.Windows.Forms.Panel();
            this.BorderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.BackColor = System.Drawing.Color.Black;
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Name = "lblProgress";
            // 
            // BorderPanel
            // 
            resources.ApplyResources(this.BorderPanel, "BorderPanel");
            this.BorderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BorderPanel.Controls.Add(this.lblProgress);
            this.BorderPanel.Name = "BorderPanel";
            // 
            // SplashForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ControlBox = false;
            this.Controls.Add(this.BorderPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SplashForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SplashForm_FormClosed);
            this.Load += new System.EventHandler(this.SplashForm_Load);
            this.BorderPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel BorderPanel;
    }
}