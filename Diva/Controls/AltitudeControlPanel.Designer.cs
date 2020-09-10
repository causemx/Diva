namespace Diva.Controls
{
    partial class AltitudeControlPanel
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
            this.SuspendLayout();
            // 
            // AltitudeControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.MinimumSize = new System.Drawing.Size(40, 100);
            this.Name = "AltitudeControlPanel";
            this.Size = new System.Drawing.Size(40, 100);
            this.MouseLeave += new System.EventHandler(this.AltitudeControlPanel_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AltitudeControlPanel_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
