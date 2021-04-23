namespace Diva.Controls
{
    partial class ConfigSettingsPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigSettingsPage));
            this.grpGuidedWPAltitude = new System.Windows.Forms.GroupBox();
            this.lblAltitudeUnit = new System.Windows.Forms.Label();
            this.numWPAltitude = new System.Windows.Forms.NumericUpDown();
            this.rbAlwaysPrompt = new System.Windows.Forms.RadioButton();
            this.rbDefaultAltitude = new System.Windows.Forms.RadioButton();
            this.rbMinimumAltitude = new System.Windows.Forms.RadioButton();
            this.rbDroneCurrent = new System.Windows.Forms.RadioButton();
            this.cbCreateTLog = new System.Windows.Forms.CheckBox();
            this.cbDisplayDroneRoute = new System.Windows.Forms.CheckBox();
            this.lblMaxEntriesDescription = new System.Windows.Forms.Label();
            this.numRouteEntriesMax = new System.Windows.Forms.NumericUpDown();
            this.grpGuidedWPAltitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWPAltitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRouteEntriesMax)).BeginInit();
            this.SuspendLayout();
            // 
            // grpGuidedWPAltitude
            // 
            resources.ApplyResources(this.grpGuidedWPAltitude, "grpGuidedWPAltitude");
            this.grpGuidedWPAltitude.Controls.Add(this.lblAltitudeUnit);
            this.grpGuidedWPAltitude.Controls.Add(this.numWPAltitude);
            this.grpGuidedWPAltitude.Controls.Add(this.rbAlwaysPrompt);
            this.grpGuidedWPAltitude.Controls.Add(this.rbDefaultAltitude);
            this.grpGuidedWPAltitude.Controls.Add(this.rbMinimumAltitude);
            this.grpGuidedWPAltitude.Controls.Add(this.rbDroneCurrent);
            this.grpGuidedWPAltitude.ForeColor = System.Drawing.Color.White;
            this.grpGuidedWPAltitude.Name = "grpGuidedWPAltitude";
            this.grpGuidedWPAltitude.TabStop = false;
            // 
            // lblAltitudeUnit
            // 
            resources.ApplyResources(this.lblAltitudeUnit, "lblAltitudeUnit");
            this.lblAltitudeUnit.Name = "lblAltitudeUnit";
            // 
            // numWPAltitude
            // 
            resources.ApplyResources(this.numWPAltitude, "numWPAltitude");
            this.numWPAltitude.DecimalPlaces = 1;
            this.numWPAltitude.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numWPAltitude.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numWPAltitude.Name = "numWPAltitude";
            this.numWPAltitude.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // rbAlwaysPrompt
            // 
            resources.ApplyResources(this.rbAlwaysPrompt, "rbAlwaysPrompt");
            this.rbAlwaysPrompt.Name = "rbAlwaysPrompt";
            this.rbAlwaysPrompt.TabStop = true;
            this.rbAlwaysPrompt.UseVisualStyleBackColor = true;
            // 
            // rbDefaultAltitude
            // 
            resources.ApplyResources(this.rbDefaultAltitude, "rbDefaultAltitude");
            this.rbDefaultAltitude.Name = "rbDefaultAltitude";
            this.rbDefaultAltitude.TabStop = true;
            this.rbDefaultAltitude.UseVisualStyleBackColor = true;
            // 
            // rbMinimumAltitude
            // 
            resources.ApplyResources(this.rbMinimumAltitude, "rbMinimumAltitude");
            this.rbMinimumAltitude.Name = "rbMinimumAltitude";
            this.rbMinimumAltitude.TabStop = true;
            this.rbMinimumAltitude.UseVisualStyleBackColor = true;
            // 
            // rbDroneCurrent
            // 
            resources.ApplyResources(this.rbDroneCurrent, "rbDroneCurrent");
            this.rbDroneCurrent.Name = "rbDroneCurrent";
            this.rbDroneCurrent.TabStop = true;
            this.rbDroneCurrent.UseVisualStyleBackColor = true;
            // 
            // cbCreateTLog
            // 
            resources.ApplyResources(this.cbCreateTLog, "cbCreateTLog");
            this.cbCreateTLog.ForeColor = System.Drawing.Color.White;
            this.cbCreateTLog.Name = "cbCreateTLog";
            this.cbCreateTLog.UseVisualStyleBackColor = true;
            // 
            // cbDisplayDroneRoute
            // 
            resources.ApplyResources(this.cbDisplayDroneRoute, "cbDisplayDroneRoute");
            this.cbDisplayDroneRoute.ForeColor = System.Drawing.Color.White;
            this.cbDisplayDroneRoute.Name = "cbDisplayDroneRoute";
            this.cbDisplayDroneRoute.UseVisualStyleBackColor = true;
            // 
            // lblMaxEntriesDescription
            // 
            resources.ApplyResources(this.lblMaxEntriesDescription, "lblMaxEntriesDescription");
            this.lblMaxEntriesDescription.ForeColor = System.Drawing.Color.White;
            this.lblMaxEntriesDescription.Name = "lblMaxEntriesDescription";
            // 
            // numRouteEntriesMax
            // 
            resources.ApplyResources(this.numRouteEntriesMax, "numRouteEntriesMax");
            this.numRouteEntriesMax.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numRouteEntriesMax.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRouteEntriesMax.Name = "numRouteEntriesMax";
            this.numRouteEntriesMax.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // ConfigSettingsPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lblMaxEntriesDescription);
            this.Controls.Add(this.numRouteEntriesMax);
            this.Controls.Add(this.cbDisplayDroneRoute);
            this.Controls.Add(this.cbCreateTLog);
            this.Controls.Add(this.grpGuidedWPAltitude);
            this.Name = "ConfigSettingsPage";
            this.grpGuidedWPAltitude.ResumeLayout(false);
            this.grpGuidedWPAltitude.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWPAltitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRouteEntriesMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpGuidedWPAltitude;
        private System.Windows.Forms.Label lblAltitudeUnit;
        private System.Windows.Forms.NumericUpDown numWPAltitude;
        private System.Windows.Forms.RadioButton rbAlwaysPrompt;
        private System.Windows.Forms.RadioButton rbDefaultAltitude;
        private System.Windows.Forms.RadioButton rbMinimumAltitude;
        private System.Windows.Forms.RadioButton rbDroneCurrent;
        private System.Windows.Forms.CheckBox cbCreateTLog;
        private System.Windows.Forms.CheckBox cbDisplayDroneRoute;
        private System.Windows.Forms.Label lblMaxEntriesDescription;
        private System.Windows.Forms.NumericUpDown numRouteEntriesMax;
    }
}
