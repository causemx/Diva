namespace Diva.Controls
{
	partial class InputDataDialog
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
			this.Tbox_Value = new System.Windows.Forms.TextBox();
			this.Lbl_Unit = new System.Windows.Forms.Label();
			this.Btn_Confirm = new System.Windows.Forms.Button();
			this.Btn_Cancel = new System.Windows.Forms.Button();
			this.Lbl_Hint = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Tbox_Value
			// 
			this.Tbox_Value.Location = new System.Drawing.Point(21, 54);
			this.Tbox_Value.Name = "Tbox_Value";
			this.Tbox_Value.Size = new System.Drawing.Size(156, 22);
			this.Tbox_Value.TabIndex = 0;
			// 
			// Lbl_Unit
			// 
			this.Lbl_Unit.AutoSize = true;
			this.Lbl_Unit.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Lbl_Unit.Location = new System.Drawing.Point(183, 57);
			this.Lbl_Unit.Name = "Lbl_Unit";
			this.Lbl_Unit.Size = new System.Drawing.Size(32, 15);
			this.Lbl_Unit.TabIndex = 1;
			this.Lbl_Unit.Text = "unit";
			// 
			// Btn_Confirm
			// 
			this.Btn_Confirm.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Btn_Confirm.Location = new System.Drawing.Point(33, 87);
			this.Btn_Confirm.Name = "Btn_Confirm";
			this.Btn_Confirm.Size = new System.Drawing.Size(75, 23);
			this.Btn_Confirm.TabIndex = 2;
			this.Btn_Confirm.Text = "Enter";
			this.Btn_Confirm.UseVisualStyleBackColor = true;
			this.Btn_Confirm.Click += new System.EventHandler(this.Btn_Confirm_Click);
			// 
			// Btn_Cancel
			// 
			this.Btn_Cancel.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Btn_Cancel.Location = new System.Drawing.Point(114, 87);
			this.Btn_Cancel.Name = "Btn_Cancel";
			this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.Btn_Cancel.TabIndex = 3;
			this.Btn_Cancel.Text = "Cancel";
			this.Btn_Cancel.UseVisualStyleBackColor = true;
			this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// Lbl_Hint
			// 
			this.Lbl_Hint.AutoSize = true;
			this.Lbl_Hint.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Lbl_Hint.Location = new System.Drawing.Point(24, 26);
			this.Lbl_Hint.Name = "Lbl_Hint";
			this.Lbl_Hint.Size = new System.Drawing.Size(84, 15);
			this.Lbl_Hint.TabIndex = 4;
			this.Lbl_Hint.Text = "do something";
			// 
			// InputDataDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(244, 133);
			this.Controls.Add(this.Lbl_Hint);
			this.Controls.Add(this.Btn_Cancel);
			this.Controls.Add(this.Btn_Confirm);
			this.Controls.Add(this.Lbl_Unit);
			this.Controls.Add(this.Tbox_Value);
			this.Name = "InputDataDialog";
			this.Text = "InputDataDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Tbox_Value;
		private System.Windows.Forms.Label Lbl_Unit;
		private System.Windows.Forms.Button Btn_Confirm;
		private System.Windows.Forms.Button Btn_Cancel;
		private System.Windows.Forms.Label Lbl_Hint;
	}
}