
namespace Diva.Controls.Dialogs
{
    partial class DialogEKF
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ekfvel = new Diva.Controls.CustomProgressBar();
            this.ekfposh = new Diva.Controls.CustomProgressBar();
            this.ekfposv = new Diva.Controls.CustomProgressBar();
            this.ekfcompass = new Diva.Controls.CustomProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.13591F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.13591F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.13591F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.13591F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.72819F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.72819F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ekfvel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ekfposh, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.ekfposv, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.ekfcompass, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 5, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(498, 274);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 4);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "EFK Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 49);
            this.label2.TabIndex = 9;
            this.label2.Text = "Velocity";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(103, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 49);
            this.label3.TabIndex = 10;
            this.label3.Text = "Position (Horiz)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(173, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 49);
            this.label4.TabIndex = 11;
            this.label4.Text = "Position (Verti)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(243, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 49);
            this.label5.TabIndex = 12;
            this.label5.Text = "Compass";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label6, 2);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(313, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 45);
            this.label6.TabIndex = 13;
            this.label6.Text = "Flag";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(313, 93);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(182, 129);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ekfvel
            // 
            this.ekfvel.DrawLabel = true;
            this.ekfvel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ekfvel.Location = new System.Drawing.Point(33, 49);
            this.ekfvel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ekfvel.Maximum = 100;
            this.ekfvel.Minimum = 0;
            this.ekfvel.Name = "ekfvel";
            this.tableLayoutPanel1.SetRowSpan(this.ekfvel, 4);
            this.ekfvel.Size = new System.Drawing.Size(64, 172);
            this.ekfvel.TabIndex = 5;
            this.ekfvel.Value = 10;
            // 
            // ekfposh
            // 
            this.ekfposh.DrawLabel = true;
            this.ekfposh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ekfposh.Location = new System.Drawing.Point(103, 49);
            this.ekfposh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ekfposh.Maximum = 100;
            this.ekfposh.Minimum = 0;
            this.ekfposh.Name = "ekfposh";
            this.tableLayoutPanel1.SetRowSpan(this.ekfposh, 4);
            this.ekfposh.Size = new System.Drawing.Size(64, 172);
            this.ekfposh.TabIndex = 6;
            this.ekfposh.Value = 10;
            // 
            // ekfposv
            // 
            this.ekfposv.DrawLabel = true;
            this.ekfposv.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ekfposv.Location = new System.Drawing.Point(173, 49);
            this.ekfposv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ekfposv.Maximum = 100;
            this.ekfposv.Minimum = 0;
            this.ekfposv.Name = "ekfposv";
            this.tableLayoutPanel1.SetRowSpan(this.ekfposv, 4);
            this.ekfposv.Size = new System.Drawing.Size(64, 172);
            this.ekfposv.TabIndex = 7;
            this.ekfposv.Value = 10;
            // 
            // ekfcompass
            // 
            this.ekfcompass.DrawLabel = true;
            this.ekfcompass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ekfcompass.Location = new System.Drawing.Point(243, 49);
            this.ekfcompass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ekfcompass.Maximum = 100;
            this.ekfcompass.Minimum = 0;
            this.ekfcompass.Name = "ekfcompass";
            this.tableLayoutPanel1.SetRowSpan(this.ekfcompass, 4);
            this.ekfcompass.Size = new System.Drawing.Size(64, 172);
            this.ekfcompass.TabIndex = 8;
            this.ekfcompass.Value = 10;
            // 
            // DialogEKF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 274);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DialogEKF";
            this.Text = "FKFStatus";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private CustomProgressBar ekfvel;
        private CustomProgressBar ekfposh;
        private CustomProgressBar ekfposv;
        private CustomProgressBar ekfcompass;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}