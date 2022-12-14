using Diva.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls.Dialogs
{
    class DialogGrid : DialogOrigin
    {
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox comboBoxStartfrom;
        private NumericUpDown numericUpDownAlt;
        private NumericUpDown numericUpDownAngle;
        private NumericUpDown numericUpDownDistance;
        private NumericUpDown numericUpDownSpace;
        

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxStartfrom = new System.Windows.Forms.ComboBox();
            this.numericUpDownAlt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDistance = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSpace = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpace)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxStartfrom, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownAlt, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownAngle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownDistance, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownSpace, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.22223F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(332, 262);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 16);
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
            this.label2.Location = new System.Drawing.Point(57, 68);
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
            this.label3.Location = new System.Drawing.Point(33, 122);
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
            this.label4.Location = new System.Drawing.Point(30, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Spacing Inline [m]";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(51, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Start From";
            // 
            // comboBoxStartfrom
            // 
            this.comboBoxStartfrom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxStartfrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartfrom.FormattingEnabled = true;
            this.comboBoxStartfrom.Items.AddRange(new object[] {
            "Home"});
            this.comboBoxStartfrom.Location = new System.Drawing.Point(188, 223);
            this.comboBoxStartfrom.Name = "comboBoxStartfrom";
            this.comboBoxStartfrom.Size = new System.Drawing.Size(121, 23);
            this.comboBoxStartfrom.TabIndex = 9;
            this.comboBoxStartfrom.Text = "Home";
            // 
            // numericUpDownAlt
            // 
            this.numericUpDownAlt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.numericUpDownAlt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAlt.Location = new System.Drawing.Point(189, 13);
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
            this.numericUpDownAngle.Location = new System.Drawing.Point(189, 65);
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
            this.numericUpDownDistance.Location = new System.Drawing.Point(189, 119);
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
            this.numericUpDownSpace.Location = new System.Drawing.Point(189, 171);
            this.numericUpDownSpace.Name = "numericUpDownSpace";
            this.numericUpDownSpace.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownSpace.TabIndex = 13;
            // 
            // DialogGrid
            // 
            this.ClientSize = new System.Drawing.Size(336, 353);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DialogGrid";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpace)).EndInit();
            this.ResumeLayout(false);

        }

        public DialogGrid(string c, MessageBoxButtons mb) : base(c, mb)
        {
            InitializeComponent();
        }

        protected override void SetFormSize()
        {
            // int widht = tableLayoutPanel1?.Width ?? 600;
            this.Size = new Size(Width, Height);
        }

        protected async override void Button_Click(object sender, EventArgs e)
        {
            var drone = Planner.GetActiveDrone();
            var home = Planner.GetPlannerInstance().HomeLocation;
            Grid g = new Grid(drone, home);
            await g.Accept(
                Grid.ScanMode.Survey,
                (double)numericUpDownAlt.Value,
                (double)numericUpDownDistance.Value,
                (double)numericUpDownSpace.Value,
                (double)numericUpDownAngle.Value,
                Grid.StartPosition.Home,
                home);
        }

        #region -> Drag Form
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion
    }
}
