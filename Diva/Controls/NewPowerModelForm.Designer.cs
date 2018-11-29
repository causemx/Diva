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
            this.PanelCollection = new System.Windows.Forms.Panel();
            this.PanelSeparator = new System.Windows.Forms.Panel();
            this.TBoxDataCollectionDescription = new System.Windows.Forms.TextBox();
            this.TBoxDatCollectionSteps = new System.Windows.Forms.TextBox();
            this.LabelLogFileLocation = new System.Windows.Forms.Label();
            this.TBoxLogFileLocation = new System.Windows.Forms.TextBox();
            this.BtnLogFileBrowse = new System.Windows.Forms.Button();
            this.TBoxModelName = new System.Windows.Forms.TextBox();
            this.LabelModelName = new System.Windows.Forms.Label();
            this.LabelModelNameRules = new System.Windows.Forms.Label();
            this.PanelActionButtons.SuspendLayout();
            this.PanelAnalysis.SuspendLayout();
            this.PanelCollection.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelActionButtons
            // 
            this.PanelActionButtons.BackColor = System.Drawing.Color.Black;
            this.PanelActionButtons.Controls.Add(this.BtnDataAnalysisTab);
            this.PanelActionButtons.Controls.Add(this.BtnDataCollectionTab);
            this.PanelActionButtons.Controls.Add(this.BtnActionCancel);
            this.PanelActionButtons.Controls.Add(this.BtnActionOk);
            resources.ApplyResources(this.PanelActionButtons, "PanelActionButtons");
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
            this.PanelAnalysis.BackColor = System.Drawing.Color.Black;
            this.PanelAnalysis.Controls.Add(this.LabelModelNameRules);
            this.PanelAnalysis.Controls.Add(this.TBoxModelName);
            this.PanelAnalysis.Controls.Add(this.LabelModelName);
            this.PanelAnalysis.Controls.Add(this.BtnLogFileBrowse);
            this.PanelAnalysis.Controls.Add(this.TBoxLogFileLocation);
            this.PanelAnalysis.Controls.Add(this.LabelLogFileLocation);
            resources.ApplyResources(this.PanelAnalysis, "PanelAnalysis");
            this.PanelAnalysis.Name = "PanelAnalysis";
            // 
            // PanelCollection
            // 
            this.PanelCollection.BackColor = System.Drawing.Color.Black;
            this.PanelCollection.Controls.Add(this.TBoxDatCollectionSteps);
            this.PanelCollection.Controls.Add(this.TBoxDataCollectionDescription);
            resources.ApplyResources(this.PanelCollection, "PanelCollection");
            this.PanelCollection.Name = "PanelCollection";
            // 
            // PanelSeparator
            // 
            this.PanelSeparator.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.PanelSeparator, "PanelSeparator");
            this.PanelSeparator.Name = "PanelSeparator";
            // 
            // TBoxDataCollectionDescription
            // 
            this.TBoxDataCollectionDescription.BackColor = System.Drawing.Color.Black;
            this.TBoxDataCollectionDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TBoxDataCollectionDescription, "TBoxDataCollectionDescription");
            this.TBoxDataCollectionDescription.ForeColor = System.Drawing.Color.White;
            this.TBoxDataCollectionDescription.Name = "TBoxDataCollectionDescription";
            this.TBoxDataCollectionDescription.ReadOnly = true;
            // 
            // TBoxDatCollectionSteps
            // 
            this.TBoxDatCollectionSteps.BackColor = System.Drawing.Color.Black;
            this.TBoxDatCollectionSteps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TBoxDatCollectionSteps, "TBoxDatCollectionSteps");
            this.TBoxDatCollectionSteps.ForeColor = System.Drawing.Color.White;
            this.TBoxDatCollectionSteps.Name = "TBoxDatCollectionSteps";
            this.TBoxDatCollectionSteps.ReadOnly = true;
            // 
            // LabelLogFileLocation
            // 
            resources.ApplyResources(this.LabelLogFileLocation, "LabelLogFileLocation");
            this.LabelLogFileLocation.Name = "LabelLogFileLocation";
            // 
            // TBoxLogFileLocation
            // 
            resources.ApplyResources(this.TBoxLogFileLocation, "TBoxLogFileLocation");
            this.TBoxLogFileLocation.Name = "TBoxLogFileLocation";
            // 
            // BtnLogFileBrowse
            // 
            resources.ApplyResources(this.BtnLogFileBrowse, "BtnLogFileBrowse");
            this.BtnLogFileBrowse.Name = "BtnLogFileBrowse";
            this.BtnLogFileBrowse.UseVisualStyleBackColor = true;
            this.BtnLogFileBrowse.Click += new System.EventHandler(this.BtnLogFileBrowse_Click);
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
            // LabelModelNameRules
            // 
            resources.ApplyResources(this.LabelModelNameRules, "LabelModelNameRules");
            this.LabelModelNameRules.Name = "LabelModelNameRules";
            // 
            // NewPowerModelForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PanelAnalysis);
            this.Controls.Add(this.PanelCollection);
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
    }
}