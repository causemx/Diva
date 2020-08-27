namespace Diva.Controls
{
    partial class TrackerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackerDialog));
            this.LblTrackingTarget = new System.Windows.Forms.Label();
            this.RBtnTrackClose = new System.Windows.Forms.RadioButton();
            this.RBtnTrackRelative = new System.Windows.Forms.RadioButton();
            this.RBtnCustomizedTrack = new System.Windows.Forms.RadioButton();
            this.LblBearingAngle = new System.Windows.Forms.Label();
            this.TBoxBearingAngle = new System.Windows.Forms.TextBox();
            this.LblBearingAngleUnit = new System.Windows.Forms.Label();
            this.LblDistance = new System.Windows.Forms.Label();
            this.TBoxDistance = new System.Windows.Forms.TextBox();
            this.TBoxDistanceUnit = new System.Windows.Forms.Label();
            this.PBoxSchematic = new System.Windows.Forms.PictureBox();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.CBTrackingTarget = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBoxSchematic)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTrackingTarget
            // 
            resources.ApplyResources(this.LblTrackingTarget, "LblTrackingTarget");
            this.LblTrackingTarget.Name = "LblTrackingTarget";
            // 
            // RBtnTrackClose
            // 
            resources.ApplyResources(this.RBtnTrackClose, "RBtnTrackClose");
            this.RBtnTrackClose.Name = "RBtnTrackClose";
            this.RBtnTrackClose.TabStop = true;
            this.RBtnTrackClose.UseVisualStyleBackColor = true;
            this.RBtnTrackClose.CheckedChanged += new System.EventHandler(this.TrackTypeRadio_CheckedChanged);
            // 
            // RBtnTrackRelative
            // 
            resources.ApplyResources(this.RBtnTrackRelative, "RBtnTrackRelative");
            this.RBtnTrackRelative.Name = "RBtnTrackRelative";
            this.RBtnTrackRelative.TabStop = true;
            this.RBtnTrackRelative.UseVisualStyleBackColor = true;
            this.RBtnTrackRelative.CheckedChanged += new System.EventHandler(this.TrackTypeRadio_CheckedChanged);
            // 
            // RBtnCustomizedTrack
            // 
            resources.ApplyResources(this.RBtnCustomizedTrack, "RBtnCustomizedTrack");
            this.RBtnCustomizedTrack.Name = "RBtnCustomizedTrack";
            this.RBtnCustomizedTrack.TabStop = true;
            this.RBtnCustomizedTrack.UseVisualStyleBackColor = true;
            this.RBtnCustomizedTrack.CheckedChanged += new System.EventHandler(this.TrackTypeRadio_CheckedChanged);
            // 
            // LblBearingAngle
            // 
            resources.ApplyResources(this.LblBearingAngle, "LblBearingAngle");
            this.LblBearingAngle.Name = "LblBearingAngle";
            // 
            // TBoxBearingAngle
            // 
            resources.ApplyResources(this.TBoxBearingAngle, "TBoxBearingAngle");
            this.TBoxBearingAngle.Name = "TBoxBearingAngle";
            this.TBoxBearingAngle.TextChanged += new System.EventHandler(this.TrackParameter_TextChanged);
            // 
            // LblBearingAngleUnit
            // 
            resources.ApplyResources(this.LblBearingAngleUnit, "LblBearingAngleUnit");
            this.LblBearingAngleUnit.Name = "LblBearingAngleUnit";
            // 
            // LblDistance
            // 
            resources.ApplyResources(this.LblDistance, "LblDistance");
            this.LblDistance.Name = "LblDistance";
            // 
            // TBoxDistance
            // 
            resources.ApplyResources(this.TBoxDistance, "TBoxDistance");
            this.TBoxDistance.Name = "TBoxDistance";
            this.TBoxDistance.TextChanged += new System.EventHandler(this.TrackParameter_TextChanged);
            // 
            // TBoxDistanceUnit
            // 
            resources.ApplyResources(this.TBoxDistanceUnit, "TBoxDistanceUnit");
            this.TBoxDistanceUnit.Name = "TBoxDistanceUnit";
            // 
            // PBoxSchematic
            // 
            resources.ApplyResources(this.PBoxSchematic, "PBoxSchematic");
            this.PBoxSchematic.Name = "PBoxSchematic";
            this.PBoxSchematic.TabStop = false;
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
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // CBTrackingTarget
            // 
            resources.ApplyResources(this.CBTrackingTarget, "CBTrackingTarget");
            this.CBTrackingTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTrackingTarget.FormattingEnabled = true;
            this.CBTrackingTarget.Name = "CBTrackingTarget";
            // 
            // TrackerDialog
            // 
            this.AcceptButton = this.BtnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ControlBox = false;
            this.Controls.Add(this.CBTrackingTarget);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.PBoxSchematic);
            this.Controls.Add(this.TBoxDistanceUnit);
            this.Controls.Add(this.TBoxDistance);
            this.Controls.Add(this.LblDistance);
            this.Controls.Add(this.LblBearingAngleUnit);
            this.Controls.Add(this.TBoxBearingAngle);
            this.Controls.Add(this.LblBearingAngle);
            this.Controls.Add(this.RBtnCustomizedTrack);
            this.Controls.Add(this.RBtnTrackRelative);
            this.Controls.Add(this.RBtnTrackClose);
            this.Controls.Add(this.LblTrackingTarget);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TrackerDialog";
            this.Shown += new System.EventHandler(this.TrackerDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PBoxSchematic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblTrackingTarget;
        private System.Windows.Forms.RadioButton RBtnTrackClose;
        private System.Windows.Forms.RadioButton RBtnTrackRelative;
        private System.Windows.Forms.RadioButton RBtnCustomizedTrack;
        private System.Windows.Forms.Label LblBearingAngle;
        private System.Windows.Forms.TextBox TBoxBearingAngle;
        private System.Windows.Forms.Label LblBearingAngleUnit;
        private System.Windows.Forms.Label LblDistance;
        private System.Windows.Forms.TextBox TBoxDistance;
        private System.Windows.Forms.Label TBoxDistanceUnit;
        private System.Windows.Forms.PictureBox PBoxSchematic;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.ComboBox CBTrackingTarget;
    }
}