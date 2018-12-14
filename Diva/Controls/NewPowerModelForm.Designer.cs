namespace Diva.Controls
{
    partial class NewPowerModelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPowerModelForm));
            this.PanelActionButtons = new System.Windows.Forms.Panel();
            this.BtnDataAnalysisTab = new System.Windows.Forms.RadioButton();
            this.BtnDataCollectionTab = new System.Windows.Forms.RadioButton();
            this.BtnActionCancel = new System.Windows.Forms.Button();
            this.BtnActionOk = new System.Windows.Forms.Button();
            this.PanelAnalysis = new System.Windows.Forms.Panel();
            this.LabelModelNameRules = new System.Windows.Forms.Label();
            this.TBoxModelName = new System.Windows.Forms.TextBox();
            this.LabelModelName = new System.Windows.Forms.Label();
            this.BtnLogFileBrowse = new System.Windows.Forms.Button();
            this.TBoxLogFileLocation = new System.Windows.Forms.TextBox();
            this.LabelLogFileLocation = new System.Windows.Forms.Label();
            this.PanelCollection = new System.Windows.Forms.Panel();
            this.TBoxMissionAngle = new System.Windows.Forms.TextBox();
            this.LabelMissionAngle = new System.Windows.Forms.Label();
            this.TBoxDatCollectionSteps = new System.Windows.Forms.TextBox();
            this.TBoxDataCollectionDescription = new System.Windows.Forms.TextBox();
            this.PanelSeparator = new System.Windows.Forms.Panel();
            this.PanelActionButtons.SuspendLayout();
            this.PanelAnalysis.SuspendLayout();
            this.PanelCollection.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelActionButtons
            // 
            resources.ApplyResources(this.PanelActionButtons, "PanelActionButtons");
            this.PanelActionButtons.BackColor = System.Drawing.Color.Black;
            this.PanelActionButtons.Controls.Add(this.BtnDataAnalysisTab);
            this.PanelActionButtons.Controls.Add(this.BtnDataCollectionTab);
            this.PanelActionButtons.Controls.Add(this.BtnActionCancel);
            this.PanelActionButtons.Controls.Add(this.BtnActionOk);
            this.PanelActionButtons.Name = "PanelActionButtons";
            // 
            // BtnDataAnalysisTab
            // 
            resources.ApplyResources(this.BtnDataAnalysisTab, "BtnDataAnalysisTab");
            this.BtnDataAnalysisTab.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BtnDataAnalysisTab.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnDataAnalysisTab.Name = "BtnDataAnalysisTab";
            this.BtnDataAnalysisTab.UseVisualStyleBackColor = true;
            // 
            // BtnDataCollectionTab
            // 
            resources.ApplyResources(this.BtnDataCollectionTab, "BtnDataCollectionTab");
            this.BtnDataCollectionTab.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnDataCollectionTab.Name = "BtnDataCollectionTab";
            this.BtnDataCollectionTab.UseVisualStyleBackColor = true;
            this.BtnDataCollectionTab.CheckedChanged += new System.EventHandler(this.TabRadio_CheckChanged);
            // 
            // BtnActionCancel
            // 
            resources.ApplyResources(this.BtnActionCancel, "BtnActionCancel");
            this.BtnActionCancel.Name = "BtnActionCancel";
            this.BtnActionCancel.UseVisualStyleBackColor = true;
            this.BtnActionCancel.Click += new System.EventHandler(this.BtnActions_Click);
            // 
            // BtnActionOk
            // 
            resources.ApplyResources(this.BtnActionOk, "BtnActionOk");
            this.BtnActionOk.Name = "BtnActionOk";
            this.BtnActionOk.UseVisualStyleBackColor = true;
            this.BtnActionOk.Click += new System.EventHandler(this.BtnActions_Click);
            // 
            // PanelAnalysis
            // 
            resources.ApplyResources(this.PanelAnalysis, "PanelAnalysis");
            this.PanelAnalysis.BackColor = System.Drawing.Color.Black;
            this.PanelAnalysis.Controls.Add(this.LabelModelNameRules);
            this.PanelAnalysis.Controls.Add(this.TBoxModelName);
            this.PanelAnalysis.Controls.Add(this.LabelModelName);
            this.PanelAnalysis.Controls.Add(this.BtnLogFileBrowse);
            this.PanelAnalysis.Controls.Add(this.TBoxLogFileLocation);
            this.PanelAnalysis.Controls.Add(this.LabelLogFileLocation);
            this.PanelAnalysis.Name = "PanelAnalysis";
            // 
            // LabelModelNameRules
            // 
            resources.ApplyResources(this.LabelModelNameRules, "LabelModelNameRules");
            this.LabelModelNameRules.Name = "LabelModelNameRules";
            // 
            // TBoxModelName
            // 
            resources.ApplyResources(this.TBoxModelName, "TBoxModelName");
            this.TBoxModelName.Name = "TBoxModelName";
            // 
            // LabelModelName
            // 
            resources.ApplyResources(this.LabelModelName, "LabelModelName");
            this.LabelModelName.Name = "LabelModelName";
            // 
            // BtnLogFileBrowse
            // 
            resources.ApplyResources(this.BtnLogFileBrowse, "BtnLogFileBrowse");
            this.BtnLogFileBrowse.Name = "BtnLogFileBrowse";
            this.BtnLogFileBrowse.UseVisualStyleBackColor = true;
            this.BtnLogFileBrowse.Click += new System.EventHandler(this.BtnLogFileBrowse_Click);
            // 
            // TBoxLogFileLocation
            // 
            resources.ApplyResources(this.TBoxLogFileLocation, "TBoxLogFileLocation");
            this.TBoxLogFileLocation.Name = "TBoxLogFileLocation";
            // 
            // LabelLogFileLocation
            // 
            resources.ApplyResources(this.LabelLogFileLocation, "LabelLogFileLocation");
            this.LabelLogFileLocation.Name = "LabelLogFileLocation";
            // 
            // PanelCollection
            // 
            resources.ApplyResources(this.PanelCollection, "PanelCollection");
            this.PanelCollection.BackColor = System.Drawing.Color.Black;
            this.PanelCollection.Controls.Add(this.TBoxMissionAngle);
            this.PanelCollection.Controls.Add(this.LabelMissionAngle);
            this.PanelCollection.Controls.Add(this.TBoxDatCollectionSteps);
            this.PanelCollection.Controls.Add(this.TBoxDataCollectionDescription);
            this.PanelCollection.Name = "PanelCollection";
            // 
            // TBoxMissionAngle
            // 
            resources.ApplyResources(this.TBoxMissionAngle, "TBoxMissionAngle");
            this.TBoxMissionAngle.Name = "TBoxMissionAngle";
            // 
            // LabelMissionAngle
            // 
            resources.ApplyResources(this.LabelMissionAngle, "LabelMissionAngle");
            this.LabelMissionAngle.Name = "LabelMissionAngle";
            // 
            // TBoxDatCollectionSteps
            // 
            resources.ApplyResources(this.TBoxDatCollectionSteps, "TBoxDatCollectionSteps");
            this.TBoxDatCollectionSteps.BackColor = System.Drawing.Color.Black;
            this.TBoxDatCollectionSteps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBoxDatCollectionSteps.ForeColor = System.Drawing.Color.White;
            this.TBoxDatCollectionSteps.Name = "TBoxDatCollectionSteps";
            this.TBoxDatCollectionSteps.ReadOnly = true;
            // 
            // TBoxDataCollectionDescription
            // 
            resources.ApplyResources(this.TBoxDataCollectionDescription, "TBoxDataCollectionDescription");
            this.TBoxDataCollectionDescription.BackColor = System.Drawing.Color.Black;
            this.TBoxDataCollectionDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBoxDataCollectionDescription.ForeColor = System.Drawing.Color.White;
            this.TBoxDataCollectionDescription.Name = "TBoxDataCollectionDescription";
            this.TBoxDataCollectionDescription.ReadOnly = true;
            // 
            // PanelSeparator
            // 
            resources.ApplyResources(this.PanelSeparator, "PanelSeparator");
            this.PanelSeparator.BackColor = System.Drawing.Color.White;
            this.PanelSeparator.Name = "PanelSeparator";
            // 
            // NewPowerModelForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PanelCollection);
            this.Controls.Add(this.PanelAnalysis);
            this.Controls.Add(this.PanelSeparator);
            this.Controls.Add(this.PanelActionButtons);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewPowerModelForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.PanelActionButtons.ResumeLayout(false);
            this.PanelActionButtons.PerformLayout();
            this.PanelAnalysis.ResumeLayout(false);
            this.PanelAnalysis.PerformLayout();
            this.PanelCollection.ResumeLayout(false);
            this.PanelCollection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelActionButtons;
        private System.Windows.Forms.Panel PanelAnalysis;
        private System.Windows.Forms.Panel PanelCollection;
        private System.Windows.Forms.Button BtnActionCancel;
        private System.Windows.Forms.Button BtnActionOk;
        private System.Windows.Forms.RadioButton BtnDataAnalysisTab;
        private System.Windows.Forms.RadioButton BtnDataCollectionTab;
        private System.Windows.Forms.Panel PanelSeparator;
        private System.Windows.Forms.TextBox TBoxDataCollectionDescription;
        private System.Windows.Forms.TextBox TBoxDatCollectionSteps;
        private System.Windows.Forms.Label LabelLogFileLocation;
        private System.Windows.Forms.Button BtnLogFileBrowse;
        private System.Windows.Forms.TextBox TBoxLogFileLocation;
        private System.Windows.Forms.TextBox TBoxModelName;
        private System.Windows.Forms.Label LabelModelName;
        private System.Windows.Forms.Label LabelModelNameRules;
        private System.Windows.Forms.Label LabelMissionAngle;
        private System.Windows.Forms.TextBox TBoxMissionAngle;
    }
}