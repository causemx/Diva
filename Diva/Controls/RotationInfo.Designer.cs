namespace Diva.Controls
{
	partial class RotationInfo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RotationInfo));
			this.BUT_Cancel = new System.Windows.Forms.Button();
			this.LBL_Message = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BUT_Cancel
			// 
			resources.ApplyResources(this.BUT_Cancel, "BUT_Cancel");
			this.BUT_Cancel.ForeColor = System.Drawing.Color.White;
			this.BUT_Cancel.Name = "BUT_Cancel";
			this.BUT_Cancel.UseVisualStyleBackColor = true;
			this.BUT_Cancel.Click += new System.EventHandler(this.BUT_Cancel_Click);
			// 
			// LBL_Message
			// 
			resources.ApplyResources(this.LBL_Message, "LBL_Message");
			this.LBL_Message.ForeColor = System.Drawing.Color.White;
			this.LBL_Message.Name = "LBL_Message";
			// 
			// RotationInfo
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.LBL_Message);
			this.Controls.Add(this.BUT_Cancel);
			this.Name = "RotationInfo";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BUT_Cancel;
		private System.Windows.Forms.Label LBL_Message;
	}
}
