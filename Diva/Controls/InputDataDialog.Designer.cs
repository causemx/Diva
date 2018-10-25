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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDataDialog));
			this.Tbox_Value = new System.Windows.Forms.TextBox();
			this.Lbl_Unit = new System.Windows.Forms.Label();
			this.Btn_Confirm = new System.Windows.Forms.Button();
			this.Btn_Cancel = new System.Windows.Forms.Button();
			this.Lbl_Hint = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Tbox_Value
			// 
			resources.ApplyResources(this.Tbox_Value, "Tbox_Value");
			this.Tbox_Value.Name = "Tbox_Value";
			// 
			// Lbl_Unit
			// 
			resources.ApplyResources(this.Lbl_Unit, "Lbl_Unit");
			this.Lbl_Unit.Name = "Lbl_Unit";
			// 
			// Btn_Confirm
			// 
			resources.ApplyResources(this.Btn_Confirm, "Btn_Confirm");
			this.Btn_Confirm.Name = "Btn_Confirm";
			this.Btn_Confirm.UseVisualStyleBackColor = true;
			this.Btn_Confirm.Click += new System.EventHandler(this.Btn_Confirm_Click);
			// 
			// Btn_Cancel
			// 
			resources.ApplyResources(this.Btn_Cancel, "Btn_Cancel");
			this.Btn_Cancel.Name = "Btn_Cancel";
			this.Btn_Cancel.UseVisualStyleBackColor = true;
			this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// Lbl_Hint
			// 
			resources.ApplyResources(this.Lbl_Hint, "Lbl_Hint");
			this.Lbl_Hint.Name = "Lbl_Hint";
			// 
			// InputDataDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Lbl_Hint);
			this.Controls.Add(this.Btn_Cancel);
			this.Controls.Add(this.Btn_Confirm);
			this.Controls.Add(this.Lbl_Unit);
			this.Controls.Add(this.Tbox_Value);
			this.Name = "InputDataDialog";
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