namespace FooApplication
{
	partial class Planner
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
			this.myMap = new FooApplication.Controls.MyGMap();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TXT_Mode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.BUT_Write_wp = new System.Windows.Forms.Button();
			this.BUT_Auto = new System.Windows.Forms.Button();
			this.BUT_Arm = new System.Windows.Forms.Button();
			this.BUT_Connect = new System.Windows.Forms.Button();
			this.TXT_DefaultAlt = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.TXT_homealt = new System.Windows.Forms.TextBox();
			this.TXT_homelng = new System.Windows.Forms.TextBox();
			this.TXT_homelat = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Commands = new System.Windows.Forms.DataGridView();
			this.Command = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Param1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Param2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Param3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Param4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Lat = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Lon = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Alt = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.TagData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.setHomeHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteWPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Commands)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// myMap
			// 
			this.myMap.Bearing = 0F;
			this.myMap.CanDragMap = true;
			this.myMap.ContextMenuStrip = this.contextMenuStrip1;
			this.myMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.myMap.EmptyTileColor = System.Drawing.Color.Navy;
			this.myMap.GrayScaleMode = false;
			this.myMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.myMap.LevelsKeepInMemmory = 5;
			this.myMap.Location = new System.Drawing.Point(0, 0);
			this.myMap.MarkersEnabled = true;
			this.myMap.MaxZoom = 2;
			this.myMap.MinZoom = 2;
			this.myMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.myMap.Name = "myMap";
			this.myMap.NegativeMode = false;
			this.myMap.PolygonsEnabled = true;
			this.myMap.RetryLoadTile = 0;
			this.myMap.RoutesEnabled = true;
			this.myMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.myMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.myMap.ShowTileGridLines = false;
			this.myMap.Size = new System.Drawing.Size(1008, 537);
			this.myMap.TabIndex = 0;
			this.myMap.Zoom = 0D;
			this.myMap.Load += new System.EventHandler(this.myMap_Load);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.TXT_Mode);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.BUT_Write_wp);
			this.panel1.Controls.Add(this.BUT_Auto);
			this.panel1.Controls.Add(this.BUT_Arm);
			this.panel1.Controls.Add(this.BUT_Connect);
			this.panel1.Controls.Add(this.TXT_DefaultAlt);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.TXT_homealt);
			this.panel1.Controls.Add(this.TXT_homelng);
			this.panel1.Controls.Add(this.TXT_homelat);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.Commands);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 428);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1008, 109);
			this.panel1.TabIndex = 1;
			// 
			// TXT_Mode
			// 
			this.TXT_Mode.Location = new System.Drawing.Point(646, 5);
			this.TXT_Mode.Name = "TXT_Mode";
			this.TXT_Mode.Size = new System.Drawing.Size(100, 22);
			this.TXT_Mode.TabIndex = 18;
			this.TXT_Mode.Text = "NULL";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(585, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 23);
			this.label3.TabIndex = 17;
			this.label3.Text = "Mode";
			// 
			// BUT_Write_wp
			// 
			this.BUT_Write_wp.Location = new System.Drawing.Point(831, 45);
			this.BUT_Write_wp.Name = "BUT_Write_wp";
			this.BUT_Write_wp.Size = new System.Drawing.Size(75, 23);
			this.BUT_Write_wp.TabIndex = 16;
			this.BUT_Write_wp.Text = "Write_WP";
			this.BUT_Write_wp.UseVisualStyleBackColor = true;
			this.BUT_Write_wp.Click += new System.EventHandler(this.BUT_write_Click);
			// 
			// BUT_Auto
			// 
			this.BUT_Auto.Location = new System.Drawing.Point(912, 74);
			this.BUT_Auto.Name = "BUT_Auto";
			this.BUT_Auto.Size = new System.Drawing.Size(75, 23);
			this.BUT_Auto.TabIndex = 15;
			this.BUT_Auto.Text = "Auto";
			this.BUT_Auto.UseVisualStyleBackColor = true;
			this.BUT_Auto.Click += new System.EventHandler(this.BUT_Auto_Click);
			// 
			// BUT_Arm
			// 
			this.BUT_Arm.Location = new System.Drawing.Point(912, 45);
			this.BUT_Arm.Name = "BUT_Arm";
			this.BUT_Arm.Size = new System.Drawing.Size(75, 23);
			this.BUT_Arm.TabIndex = 14;
			this.BUT_Arm.Text = "Arm";
			this.BUT_Arm.UseVisualStyleBackColor = true;
			this.BUT_Arm.Click += new System.EventHandler(this.BUT_Arm_Click);
			// 
			// BUT_Connect
			// 
			this.BUT_Connect.Location = new System.Drawing.Point(912, 16);
			this.BUT_Connect.Name = "BUT_Connect";
			this.BUT_Connect.Size = new System.Drawing.Size(75, 23);
			this.BUT_Connect.TabIndex = 13;
			this.BUT_Connect.Text = "Connect";
			this.BUT_Connect.UseVisualStyleBackColor = true;
			this.BUT_Connect.Click += new System.EventHandler(this.BUT_Connect_Click);
			// 
			// TXT_DefaultAlt
			// 
			this.TXT_DefaultAlt.Location = new System.Drawing.Point(469, 3);
			this.TXT_DefaultAlt.Name = "TXT_DefaultAlt";
			this.TXT_DefaultAlt.Size = new System.Drawing.Size(100, 22);
			this.TXT_DefaultAlt.TabIndex = 12;
			this.TXT_DefaultAlt.Text = "10";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(392, 4);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 23);
			this.label2.TabIndex = 11;
			this.label2.Text = "Altitude";
			// 
			// TXT_homealt
			// 
			this.TXT_homealt.Location = new System.Drawing.Point(275, 4);
			this.TXT_homealt.Name = "TXT_homealt";
			this.TXT_homealt.Size = new System.Drawing.Size(100, 22);
			this.TXT_homealt.TabIndex = 10;
			// 
			// TXT_homelng
			// 
			this.TXT_homelng.Location = new System.Drawing.Point(169, 3);
			this.TXT_homelng.Name = "TXT_homelng";
			this.TXT_homelng.Size = new System.Drawing.Size(100, 22);
			this.TXT_homelng.TabIndex = 9;
			// 
			// TXT_homelat
			// 
			this.TXT_homelat.Location = new System.Drawing.Point(63, 3);
			this.TXT_homelat.Name = "TXT_homelat";
			this.TXT_homelat.Size = new System.Drawing.Size(100, 22);
			this.TXT_homelat.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 7;
			this.label1.Text = "Home";
			// 
			// Commands
			// 
			this.Commands.AllowUserToAddRows = false;
			this.Commands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Commands.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
			this.Commands.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
			this.Commands.ColumnHeadersHeight = 30;
			this.Commands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Command,
            this.Param1,
            this.Param2,
            this.Param3,
            this.Param4,
            this.Lat,
            this.Lon,
            this.Alt,
            this.Column5,
            this.Delete,
            this.TagData});
			this.Commands.Location = new System.Drawing.Point(0, 29);
			this.Commands.Name = "Commands";
			this.Commands.RowHeadersWidth = 50;
			this.Commands.RowTemplate.Height = 24;
			this.Commands.Size = new System.Drawing.Size(707, 80);
			this.Commands.TabIndex = 6;
			this.Commands.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_CellContentClick);
			this.Commands.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_RowEnter);
			this.Commands.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Commands_RowsAdded);
			this.Commands.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Commands_RowValidating);
			// 
			// Command
			// 
			this.Command.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Command.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
			this.Command.HeaderText = "Command";
			this.Command.MinimumWidth = 60;
			this.Command.Name = "Command";
			this.Command.ToolTipText = "APM Command";
			this.Command.Width = 60;
			// 
			// Param1
			// 
			this.Param1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Param1.HeaderText = "Param1";
			this.Param1.MinimumWidth = 50;
			this.Param1.Name = "Param1";
			this.Param1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Param1.Width = 50;
			// 
			// Param2
			// 
			this.Param2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Param2.HeaderText = "Param2";
			this.Param2.MinimumWidth = 50;
			this.Param2.Name = "Param2";
			this.Param2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Param2.Width = 50;
			// 
			// Param3
			// 
			this.Param3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Param3.HeaderText = "Param3";
			this.Param3.MinimumWidth = 50;
			this.Param3.Name = "Param3";
			this.Param3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Param3.Width = 50;
			// 
			// Param4
			// 
			this.Param4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Param4.HeaderText = "Param4";
			this.Param4.MinimumWidth = 50;
			this.Param4.Name = "Param4";
			this.Param4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Param4.Width = 50;
			// 
			// Lat
			// 
			this.Lat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Lat.HeaderText = "Latitude";
			this.Lat.MinimumWidth = 60;
			this.Lat.Name = "Lat";
			this.Lat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Lat.Width = 60;
			// 
			// Lon
			// 
			this.Lon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Lon.HeaderText = "Longitude";
			this.Lon.MinimumWidth = 60;
			this.Lon.Name = "Lon";
			this.Lon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Lon.Width = 60;
			// 
			// Alt
			// 
			this.Alt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Alt.HeaderText = "Altitude";
			this.Alt.MinimumWidth = 60;
			this.Alt.Name = "Alt";
			this.Alt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Alt.Width = 60;
			// 
			// Column5
			// 
			this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Column5.HeaderText = "Angle";
			this.Column5.MinimumWidth = 60;
			this.Column5.Name = "Column5";
			this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column5.Width = 60;
			// 
			// Delete
			// 
			this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.Delete.HeaderText = "Delete";
			this.Delete.MinimumWidth = 50;
			this.Delete.Name = "Delete";
			this.Delete.Text = "X";
			this.Delete.ToolTipText = "Delete the row";
			this.Delete.Width = 50;
			// 
			// TagData
			// 
			this.TagData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
			this.TagData.HeaderText = "TagData";
			this.TagData.MinimumWidth = 50;
			this.TagData.Name = "TagData";
			this.TagData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.TagData.Visible = false;
			this.TagData.Width = 50;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteWPToolStripMenuItem,
            this.setHomeHereToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(163, 70);
			// 
			// setHomeHereToolStripMenuItem
			// 
			this.setHomeHereToolStripMenuItem.Name = "setHomeHereToolStripMenuItem";
			this.setHomeHereToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.setHomeHereToolStripMenuItem.Text = "Set Home Here";
			this.setHomeHereToolStripMenuItem.Click += new System.EventHandler(this.setHomeHereToolStripMenuItem_Click);
			// 
			// deleteWPToolStripMenuItem
			// 
			this.deleteWPToolStripMenuItem.Name = "deleteWPToolStripMenuItem";
			this.deleteWPToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.deleteWPToolStripMenuItem.Text = "Delete WP";
			this.deleteWPToolStripMenuItem.Click += new System.EventHandler(this.deleteWPToolStripMenuItem_Click);
			// 
			// Planner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 537);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.myMap);
			this.Name = "Planner";
			this.Text = "Planner";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Commands)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.MyGMap myMap;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView Commands;
		private System.Windows.Forms.TextBox TXT_homealt;
		private System.Windows.Forms.TextBox TXT_homelng;
		private System.Windows.Forms.TextBox TXT_homelat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TXT_DefaultAlt;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridViewComboBoxColumn Command;
		private System.Windows.Forms.DataGridViewTextBoxColumn Param1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Param2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Param3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Param4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Lat;
		private System.Windows.Forms.DataGridViewTextBoxColumn Lon;
		private System.Windows.Forms.DataGridViewTextBoxColumn Alt;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewButtonColumn Delete;
		private System.Windows.Forms.DataGridViewTextBoxColumn TagData;
		private System.Windows.Forms.Button BUT_Write_wp;
		private System.Windows.Forms.Button BUT_Auto;
		private System.Windows.Forms.Button BUT_Arm;
		private System.Windows.Forms.Button BUT_Connect;
		private System.Windows.Forms.TextBox TXT_Mode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem deleteWPToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setHomeHereToolStripMenuItem;
	}
}