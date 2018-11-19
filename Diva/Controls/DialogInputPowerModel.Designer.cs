﻿namespace Diva.Controls
{
	partial class DialogInputPowerModel
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.LBLDroneID = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.TXTBatteryCapacity = new System.Windows.Forms.TextBox();
			this.TXTAvailableCapacity = new System.Windows.Forms.TextBox();
			this.BTNConfirm = new System.Windows.Forms.Button();
			this.BTNCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.74122F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.25878F));
			this.tableLayoutPanel1.Controls.Add(this.label6, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.LBLDroneID, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.TXTBatteryCapacity, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.TXTAvailableCapacity, 1, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(24, 32);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(541, 203);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.label6.ForeColor = System.Drawing.Color.White;
			this.label6.Location = new System.Drawing.Point(217, 51);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(242, 22);
			this.label6.TabIndex = 7;
			this.label6.Text = "c://users/Project/Diva/*.model";
			// 
			// LBLDroneID
			// 
			this.LBLDroneID.AutoSize = true;
			this.LBLDroneID.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.LBLDroneID.ForeColor = System.Drawing.Color.White;
			this.LBLDroneID.Location = new System.Drawing.Point(217, 0);
			this.LBLDroneID.Name = "LBLDroneID";
			this.LBLDroneID.Size = new System.Drawing.Size(38, 22);
			this.LBLDroneID.TabIndex = 4;
			this.LBLDroneID.Text = "N/A";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 22);
			this.label1.TabIndex = 0;
			this.label1.Text = "Drone ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(3, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(165, 22);
			this.label2.TabIndex = 1;
			this.label2.Text = "Consumption Model";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(3, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(182, 22);
			this.label3.TabIndex = 2;
			this.label3.Text = "Battery Capacity (mAh)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(3, 157);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(172, 22);
			this.label4.TabIndex = 3;
			this.label4.Text = "Avaliable Capacity (%)";
			// 
			// TXTBatteryCapacity
			// 
			this.TXTBatteryCapacity.Location = new System.Drawing.Point(217, 105);
			this.TXTBatteryCapacity.Name = "TXTBatteryCapacity";
			this.TXTBatteryCapacity.Size = new System.Drawing.Size(242, 22);
			this.TXTBatteryCapacity.TabIndex = 5;
			// 
			// TXTAvailableCapacity
			// 
			this.TXTAvailableCapacity.Location = new System.Drawing.Point(217, 160);
			this.TXTAvailableCapacity.Name = "TXTAvailableCapacity";
			this.TXTAvailableCapacity.Size = new System.Drawing.Size(242, 22);
			this.TXTAvailableCapacity.TabIndex = 6;
			// 
			// BTNConfirm
			// 
			this.BTNConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BTNConfirm.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.BTNConfirm.ForeColor = System.Drawing.Color.White;
			this.BTNConfirm.Location = new System.Drawing.Point(481, 251);
			this.BTNConfirm.Name = "BTNConfirm";
			this.BTNConfirm.Size = new System.Drawing.Size(84, 34);
			this.BTNConfirm.TabIndex = 1;
			this.BTNConfirm.Text = "Confirm";
			this.BTNConfirm.UseVisualStyleBackColor = true;
			this.BTNConfirm.Click += new System.EventHandler(this.BTNConfirm_Click);
			// 
			// BTNCancel
			// 
			this.BTNCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BTNCancel.Font = new System.Drawing.Font("Noto Sans", 12F);
			this.BTNCancel.ForeColor = System.Drawing.Color.White;
			this.BTNCancel.Location = new System.Drawing.Point(381, 251);
			this.BTNCancel.Name = "BTNCancel";
			this.BTNCancel.Size = new System.Drawing.Size(84, 34);
			this.BTNCancel.TabIndex = 2;
			this.BTNCancel.Text = "Cancel";
			this.BTNCancel.UseVisualStyleBackColor = true;
			this.BTNCancel.Click += new System.EventHandler(this.BTNCancel_Click);
			// 
			// DialogInputPowerModel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(588, 310);
			this.Controls.Add(this.BTNCancel);
			this.Controls.Add(this.BTNConfirm);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "DialogInputPowerModel";
			this.Text = "DialogInputPowerModel";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label LBLDroneID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox TXTBatteryCapacity;
		private System.Windows.Forms.TextBox TXTAvailableCapacity;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button BTNConfirm;
		private System.Windows.Forms.Button BTNCancel;
	}
}