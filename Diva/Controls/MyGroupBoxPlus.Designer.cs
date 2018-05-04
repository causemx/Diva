namespace Diva.Controls
{
	partial class MyGroupBoxPlus
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
			this.myGroupBox1 = new Diva.Controls.MyGroupBox();
			this.ButRemove = new System.Windows.Forms.Button();
			this.ButEdit = new System.Windows.Forms.Button();
			this.myGroupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// myGroupBox1
			// 
			this.myGroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.myGroupBox1.Controls.Add(this.ButRemove);
			this.myGroupBox1.Controls.Add(this.ButEdit);
			this.myGroupBox1.Location = new System.Drawing.Point(15, 15);
			this.myGroupBox1.Name = "myGroupBox1";
			this.myGroupBox1.Radious = 5;
			this.myGroupBox1.Size = new System.Drawing.Size(372, 310);
			this.myGroupBox1.TabIndex = 0;
			this.myGroupBox1.TabStop = false;
			this.myGroupBox1.Text = "myGroupBox1";
			this.myGroupBox1.TitleBackColor = System.Drawing.Color.Gray;
			this.myGroupBox1.TitleFont = new System.Drawing.Font("新細明體", 17F, System.Drawing.FontStyle.Bold);
			this.myGroupBox1.TitleForeColor = System.Drawing.Color.White;
			this.myGroupBox1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.myGroupBox1.TitlePadding = 5;
			// 
			// ButRemove
			// 
			this.ButRemove.Location = new System.Drawing.Point(283, 6);
			this.ButRemove.Name = "ButRemove";
			this.ButRemove.Size = new System.Drawing.Size(75, 23);
			this.ButRemove.TabIndex = 1;
			this.ButRemove.Text = "Remove";
			this.ButRemove.UseVisualStyleBackColor = true;
			// 
			// ButEdit
			// 
			this.ButEdit.Location = new System.Drawing.Point(202, 6);
			this.ButEdit.Name = "ButEdit";
			this.ButEdit.Size = new System.Drawing.Size(75, 23);
			this.ButEdit.TabIndex = 0;
			this.ButEdit.Text = "Edit";
			this.ButEdit.UseVisualStyleBackColor = true;
			// 
			// MyGroupBoxPlus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.myGroupBox1);
			this.Name = "MyGroupBoxPlus";
			this.Size = new System.Drawing.Size(409, 350);
			this.myGroupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MyGroupBox myGroupBox1;
		private System.Windows.Forms.Button ButRemove;
		private System.Windows.Forms.Button ButEdit;
	}
}
