namespace Diva.Controls
{
	partial class ConfigTuning
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.myNumericUpDown1 = new Diva.Controls.Components.MyNumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.myNumericUpDown5 = new Diva.Controls.Components.MyNumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.myNumericUpDown4 = new Diva.Controls.Components.MyNumericUpDown();
			this.myNumericUpDown2 = new Diva.Controls.Components.MyNumericUpDown();
			this.myNumericUpDown3 = new Diva.Controls.Components.MyNumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.ButWriteParams = new System.Windows.Forms.Button();
			this.BtnRefresh = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown3)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown5, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown4, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown3, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(53, 22);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.61905F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.38095F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(312, 203);
			this.tableLayoutPanel1.TabIndex = 10;
			// 
			// myNumericUpDown1
			// 
			this.myNumericUpDown1.Enabled = false;
			this.myNumericUpDown1.Location = new System.Drawing.Point(159, 3);
			this.myNumericUpDown1.Max = 1F;
			this.myNumericUpDown1.Min = 0F;
			this.myNumericUpDown1.Name = "myNumericUpDown1";
			this.myNumericUpDown1.ParamName = null;
			this.myNumericUpDown1.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown1.TabIndex = 0;
			this.myNumericUpDown1.ValueUpdated += new System.EventHandler(this.numeric_ValueUpdated);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 165);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "Loiter Speed";
			// 
			// myNumericUpDown5
			// 
			this.myNumericUpDown5.Enabled = false;
			this.myNumericUpDown5.Location = new System.Drawing.Point(159, 168);
			this.myNumericUpDown5.Max = 1F;
			this.myNumericUpDown5.Min = 0F;
			this.myNumericUpDown5.Name = "myNumericUpDown5";
			this.myNumericUpDown5.ParamName = null;
			this.myNumericUpDown5.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown5.TabIndex = 4;
			this.myNumericUpDown5.ValueUpdated += new System.EventHandler(this.numeric_ValueUpdated);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "Speed";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "Radius";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 117);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 12);
			this.label4.TabIndex = 8;
			this.label4.Text = "Speed Dn";
			// 
			// myNumericUpDown4
			// 
			this.myNumericUpDown4.Enabled = false;
			this.myNumericUpDown4.Location = new System.Drawing.Point(159, 120);
			this.myNumericUpDown4.Max = 1F;
			this.myNumericUpDown4.Min = 0F;
			this.myNumericUpDown4.Name = "myNumericUpDown4";
			this.myNumericUpDown4.ParamName = null;
			this.myNumericUpDown4.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown4.TabIndex = 3;
			this.myNumericUpDown4.ValueUpdated += new System.EventHandler(this.numeric_ValueUpdated);
			// 
			// myNumericUpDown2
			// 
			this.myNumericUpDown2.Enabled = false;
			this.myNumericUpDown2.Location = new System.Drawing.Point(159, 37);
			this.myNumericUpDown2.Max = 1F;
			this.myNumericUpDown2.Min = 0F;
			this.myNumericUpDown2.Name = "myNumericUpDown2";
			this.myNumericUpDown2.ParamName = null;
			this.myNumericUpDown2.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown2.TabIndex = 1;
			this.myNumericUpDown2.ValueUpdated += new System.EventHandler(this.numeric_ValueUpdated);
			// 
			// myNumericUpDown3
			// 
			this.myNumericUpDown3.Enabled = false;
			this.myNumericUpDown3.Location = new System.Drawing.Point(159, 75);
			this.myNumericUpDown3.Max = 1F;
			this.myNumericUpDown3.Min = 0F;
			this.myNumericUpDown3.Name = "myNumericUpDown3";
			this.myNumericUpDown3.ParamName = null;
			this.myNumericUpDown3.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown3.TabIndex = 2;
			this.myNumericUpDown3.ValueUpdated += new System.EventHandler(this.numeric_ValueUpdated);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "Speed Up";
			// 
			// ButWriteParams
			// 
			this.ButWriteParams.Location = new System.Drawing.Point(53, 231);
			this.ButWriteParams.Name = "ButWriteParams";
			this.ButWriteParams.Size = new System.Drawing.Size(153, 23);
			this.ButWriteParams.TabIndex = 11;
			this.ButWriteParams.Text = "Write Params";
			this.ButWriteParams.UseVisualStyleBackColor = true;
			this.ButWriteParams.Click += new System.EventHandler(this.BUT_writePIDS_Click);
			// 
			// BtnRefresh
			// 
			this.BtnRefresh.Location = new System.Drawing.Point(212, 231);
			this.BtnRefresh.Name = "BtnRefresh";
			this.BtnRefresh.Size = new System.Drawing.Size(153, 23);
			this.BtnRefresh.TabIndex = 12;
			this.BtnRefresh.Text = "Refresh Screen";
			this.BtnRefresh.UseVisualStyleBackColor = true;
			this.BtnRefresh.Click += new System.EventHandler(this.BUT_refreshpart_Click);
			// 
			// ConfigTuning
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.BtnRefresh);
			this.Controls.Add(this.ButWriteParams);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ConfigTuning";
			this.Size = new System.Drawing.Size(430, 266);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private Components.MyNumericUpDown myNumericUpDown5;
		private Components.MyNumericUpDown myNumericUpDown4;
		private Components.MyNumericUpDown myNumericUpDown3;
		private Components.MyNumericUpDown myNumericUpDown2;
		private Components.MyNumericUpDown myNumericUpDown1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button ButWriteParams;
		private System.Windows.Forms.Button BtnRefresh;
	}
}
