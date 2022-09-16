namespace Diva.Controls
{
    partial class DroneInfoPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ThePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TelemetryData = new Diva.Controls.TelemetryDataPanel();
            this.DroneInfoTip = new System.Windows.Forms.ToolTip(this.components);
            this.ThePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ThePanel
            // 
            this.ThePanel.AutoSize = true;
            this.ThePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ThePanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ThePanel.Controls.Add(this.TelemetryData);
            this.ThePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ThePanel.Location = new System.Drawing.Point(0, 0);
            this.ThePanel.Margin = new System.Windows.Forms.Padding(0);
            this.ThePanel.Name = "ThePanel";
            this.ThePanel.Size = new System.Drawing.Size(0, 402);
            this.ThePanel.TabIndex = 0;
            // 
            // TelemetryData
            // 
            this.TelemetryData.BackColor = System.Drawing.Color.Black;
            this.TelemetryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TelemetryData.Location = new System.Drawing.Point(0, 0);
            this.TelemetryData.Margin = new System.Windows.Forms.Padding(0);
            this.TelemetryData.Name = "TelemetryData";
            this.TelemetryData.Size = new System.Drawing.Size(0, 402);
            this.TelemetryData.TabIndex = 0;
            // 
            // DroneInfoTip
            // 
            this.DroneInfoTip.UseAnimation = false;
            this.DroneInfoTip.UseFading = false;
            // 
            // DroneInfoPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.ThePanel);
            this.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DroneInfoPanel";
            this.Size = new System.Drawing.Size(0, 402);
            this.ThePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ThePanel;
        private TelemetryDataPanel TelemetryData;
        private System.Windows.Forms.ToolTip DroneInfoTip;
    }
}
