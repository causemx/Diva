namespace Diva.Controls
{
    partial class CacheZoomSelectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CacheZoomSelectDialog));
            this.LblDescription = new System.Windows.Forms.Label();
            this.LblZoomMin = new System.Windows.Forms.Label();
            this.LblZoomMax = new System.Windows.Forms.Label();
            this.NumZoomMin = new System.Windows.Forms.NumericUpDown();
            this.NumZoomMax = new System.Windows.Forms.NumericUpDown();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumZoomMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumZoomMax)).BeginInit();
            this.SuspendLayout();
            // 
            // LblDescription
            // 
            resources.ApplyResources(this.LblDescription, "LblDescription");
            this.LblDescription.Name = "LblDescription";
            // 
            // LblZoomMin
            // 
            resources.ApplyResources(this.LblZoomMin, "LblZoomMin");
            this.LblZoomMin.Name = "LblZoomMin";
            // 
            // LblZoomMax
            // 
            resources.ApplyResources(this.LblZoomMax, "LblZoomMax");
            this.LblZoomMax.Name = "LblZoomMax";
            // 
            // NumZoomMin
            // 
            resources.ApplyResources(this.NumZoomMin, "NumZoomMin");
            this.NumZoomMin.Name = "NumZoomMin";
            this.NumZoomMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumZoomMin.ValueChanged += new System.EventHandler(this.NumZoomMin_ValueChanged);
            // 
            // NumZoomMax
            // 
            resources.ApplyResources(this.NumZoomMax, "NumZoomMax");
            this.NumZoomMax.Name = "NumZoomMax";
            this.NumZoomMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumZoomMax.ValueChanged += new System.EventHandler(this.NumZoomMax_ValueChanged);
            // 
            // BtnOk
            // 
            resources.ApplyResources(this.BtnOk, "BtnOk");
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // CacheZoomSelectDialog
            // 
            this.AcceptButton = this.BtnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.NumZoomMax);
            this.Controls.Add(this.NumZoomMin);
            this.Controls.Add(this.LblZoomMax);
            this.Controls.Add(this.LblZoomMin);
            this.Controls.Add(this.LblDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CacheZoomSelectDialog";
            ((System.ComponentModel.ISupportInitialize)(this.NumZoomMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumZoomMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblDescription;
        private System.Windows.Forms.Label LblZoomMin;
        private System.Windows.Forms.Label LblZoomMax;
        private System.Windows.Forms.NumericUpDown NumZoomMin;
        private System.Windows.Forms.NumericUpDown NumZoomMax;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
    }
}