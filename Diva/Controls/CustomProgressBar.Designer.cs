
namespace Diva.Controls
{
    partial class CustomProgressBar
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picboxPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxPB)).BeginInit();
            this.SuspendLayout();
            // 
            // picboxPB
            // 
            this.picboxPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picboxPB.Location = new System.Drawing.Point(0, 0);
            this.picboxPB.Name = "picboxPB";
            this.picboxPB.Size = new System.Drawing.Size(94, 454);
            this.picboxPB.TabIndex = 0;
            this.picboxPB.TabStop = false;
            // 
            // CustomProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picboxPB);
            this.Name = "CustomProgressBar";
            this.Size = new System.Drawing.Size(94, 454);
            ((System.ComponentModel.ISupportInitialize)(this.picboxPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picboxPB;
    }
}
