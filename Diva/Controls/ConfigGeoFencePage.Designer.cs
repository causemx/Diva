namespace Diva.Controls
{
	partial class ConfigGeoFencePage
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.myCheckBox1 = new Diva.Controls.Components.MyCheckBox();
			this.myComboBox1 = new Diva.Controls.Components.MyComboBox();
			this.myComboBox2 = new Diva.Controls.Components.MyComboBox();
			this.myNumericUpDown1 = new Diva.Controls.Components.MyNumericUpDown();
			this.myNumericUpDown2 = new Diva.Controls.Components.MyNumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.myNumericUpDown3 = new Diva.Controls.Components.MyNumericUpDown();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown3)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.577F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.423F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.myCheckBox1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.myComboBox1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.myComboBox2, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown1, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown2, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.myNumericUpDown3, 1, 5);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(29, 18);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.47059F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.52941F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 241);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enable";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Type";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "Action";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 113);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(61, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "Max Radius";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 156);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 12);
			this.label5.TabIndex = 4;
			this.label5.Text = "Max Altitude";
			// 
			// myCheckBox1
			// 
			this.myCheckBox1.Enabled = false;
			this.myCheckBox1.Location = new System.Drawing.Point(134, 3);
			this.myCheckBox1.Name = "myCheckBox1";
			this.myCheckBox1.OffValue = 0D;
			this.myCheckBox1.OnValue = 1D;
			this.myCheckBox1.Padding = new System.Windows.Forms.Padding(3);
			this.myCheckBox1.ParamName = null;
			this.myCheckBox1.Size = new System.Drawing.Size(63, 24);
			this.myCheckBox1.TabIndex = 5;
			this.myCheckBox1.Text = "myCheckBox1";
			this.myCheckBox1.UseVisualStyleBackColor = true;
			// 
			// myComboBox1
			// 
			this.myComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.myComboBox1.Enabled = false;
			this.myComboBox1.FormattingEnabled = true;
			this.myComboBox1.Location = new System.Drawing.Point(134, 41);
			this.myComboBox1.Name = "myComboBox1";
			this.myComboBox1.ParamName = null;
			this.myComboBox1.Size = new System.Drawing.Size(121, 20);
			this.myComboBox1.SubControl = null;
			this.myComboBox1.TabIndex = 6;
			// 
			// myComboBox2
			// 
			this.myComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.myComboBox2.Enabled = false;
			this.myComboBox2.FormattingEnabled = true;
			this.myComboBox2.Location = new System.Drawing.Point(134, 77);
			this.myComboBox2.Name = "myComboBox2";
			this.myComboBox2.ParamName = null;
			this.myComboBox2.Size = new System.Drawing.Size(121, 20);
			this.myComboBox2.SubControl = null;
			this.myComboBox2.TabIndex = 7;
			// 
			// myNumericUpDown1
			// 
			this.myNumericUpDown1.Enabled = false;
			this.myNumericUpDown1.Location = new System.Drawing.Point(134, 116);
			this.myNumericUpDown1.Max = 1F;
			this.myNumericUpDown1.Min = 0F;
			this.myNumericUpDown1.Name = "myNumericUpDown1";
			this.myNumericUpDown1.ParamName = null;
			this.myNumericUpDown1.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown1.TabIndex = 8;
			// 
			// myNumericUpDown2
			// 
			this.myNumericUpDown2.Enabled = false;
			this.myNumericUpDown2.Location = new System.Drawing.Point(134, 159);
			this.myNumericUpDown2.Max = 1F;
			this.myNumericUpDown2.Min = 0F;
			this.myNumericUpDown2.Name = "myNumericUpDown2";
			this.myNumericUpDown2.ParamName = null;
			this.myNumericUpDown2.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown2.TabIndex = 9;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 197);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(67, 12);
			this.label6.TabIndex = 10;
			this.label6.Text = "RTL Altitude";
			// 
			// myNumericUpDown3
			// 
			this.myNumericUpDown3.Enabled = false;
			this.myNumericUpDown3.Location = new System.Drawing.Point(134, 200);
			this.myNumericUpDown3.Max = 1F;
			this.myNumericUpDown3.Min = 0F;
			this.myNumericUpDown3.Name = "myNumericUpDown3";
			this.myNumericUpDown3.ParamName = null;
			this.myNumericUpDown3.Size = new System.Drawing.Size(120, 22);
			this.myNumericUpDown3.TabIndex = 11;
			// 
			// ConfigGeoFence
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ConfigGeoFence";
			this.Size = new System.Drawing.Size(433, 297);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.myNumericUpDown3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private Components.MyCheckBox myCheckBox1;
		private Components.MyComboBox myComboBox1;
		private Components.MyComboBox myComboBox2;
		private Components.MyNumericUpDown myNumericUpDown1;
		private Components.MyNumericUpDown myNumericUpDown2;
		private System.Windows.Forms.Label label6;
		private Components.MyNumericUpDown myNumericUpDown3;
	}
}
