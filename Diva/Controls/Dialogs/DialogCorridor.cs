using Diva.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls.Dialogs
{
    class DialogCorridor : DialogOrigin
    {
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private NumericUpDown numericUpDownAlt;
        private NumericUpDown numericUpDownAngle;
        private NumericUpDown numericUpDownDistance;
        private NumericUpDown numericUpDownSpace;
        private NumericUpDown numericUpDownWidth;
        private Label label6;
        private Label label7;
        private NumericUpDown numericUpDownVertex;
        private NumericUpDown numericUpDownLayered;
        private Panel panel1;

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownAlt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDistance = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSpace = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownVertex = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLayered = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLayered)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 340);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownAlt, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownAngle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownDistance, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownSpace, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownWidth, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownVertex, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownLayered, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(356, 340);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Altitude [m]";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Angle [°]";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Line Spacing [m]";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Spacing Inline [m]";
            // 
            // numericUpDownAlt
            // 
            this.numericUpDownAlt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownAlt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAlt.Location = new System.Drawing.Point(207, 13);
            this.numericUpDownAlt.Name = "numericUpDownAlt";
            this.numericUpDownAlt.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownAlt.TabIndex = 10;
            this.numericUpDownAlt.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownAngle
            // 
            this.numericUpDownAngle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAngle.Location = new System.Drawing.Point(207, 61);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownAngle.TabIndex = 11;
            this.numericUpDownAngle.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // numericUpDownDistance
            // 
            this.numericUpDownDistance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownDistance.Location = new System.Drawing.Point(207, 109);
            this.numericUpDownDistance.Name = "numericUpDownDistance";
            this.numericUpDownDistance.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownDistance.TabIndex = 12;
            this.numericUpDownDistance.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numericUpDownSpace
            // 
            this.numericUpDownSpace.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSpace.Location = new System.Drawing.Point(207, 157);
            this.numericUpDownSpace.Name = "numericUpDownSpace";
            this.numericUpDownSpace.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownSpace.TabIndex = 13;
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownWidth.Location = new System.Drawing.Point(207, 205);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownWidth.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(60, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Width [m]";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(58, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Vertex [m]";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(63, 306);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "Layered";
            // 
            // numericUpDownVertex
            // 
            this.numericUpDownVertex.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownVertex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownVertex.Location = new System.Drawing.Point(207, 253);
            this.numericUpDownVertex.Name = "numericUpDownVertex";
            this.numericUpDownVertex.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownVertex.TabIndex = 17;
            this.numericUpDownVertex.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // numericUpDownLayered
            // 
            this.numericUpDownLayered.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownLayered.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownLayered.Location = new System.Drawing.Point(207, 303);
            this.numericUpDownLayered.Name = "numericUpDownLayered";
            this.numericUpDownLayered.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownLayered.TabIndex = 18;
            this.numericUpDownLayered.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // DialogCorridor
            // 
            this.ClientSize = new System.Drawing.Size(360, 435);
            this.Controls.Add(this.panel1);
            this.Name = "DialogCorridor";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVertex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLayered)).EndInit();
            this.ResumeLayout(false);

        }

        public DialogCorridor(string c, MessageBoxButtons mb, MessageBoxIcon mi) : base(c, mb, mi)
        {
            InitializeComponent();
        }

        protected override void SetFormSize()
        {
            // int widht = tableLayoutPanel1?.Width ?? 600;
            this.Size = new Size(Width, Height);
        }

        protected async override void OK_Click(object sender, EventArgs e)
        {
            var drone = Planner.GetActiveDrone();
            var home = Planner.GetPlannerInstance().HomeLocation;
            Grid g = new Grid(drone, home);
            await g.Accept(
                Grid.ScanMode.Corridor,
                (double)numericUpDownAlt.Value,
                (double)numericUpDownDistance.Value,
                (double)numericUpDownSpace.Value,
                (double)numericUpDownAngle.Value,
                (double)numericUpDownVertex.Value,
                (double)numericUpDownLayered.Value,
                Grid.StartPosition.Home,
                decimal.ToInt32(numericUpDownWidth.Value));
        }
    }
}
