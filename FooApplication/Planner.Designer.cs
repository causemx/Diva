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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.myMap = new FooApplication.Controls.MyGMap();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ClearMissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setHomeHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllMissionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this.but_read_wp = new FooApplication.Controls.MyButton();
			this.but_write_wp = new FooApplication.Controls.MyButton();
			this.but_land = new FooApplication.Controls.MyButton();
			this.but_takeoff = new FooApplication.Controls.MyButton();
			this.but_disarm = new FooApplication.Controls.MyButton();
			this.but_arm_throttle = new FooApplication.Controls.MyButton();
			this.TXT_Mode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
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
			this.Gauge_alt = new System.Windows.Forms.AGauge();
			this.Gauge_speed = new System.Windows.Forms.AGauge();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbl_alt = new System.Windows.Forms.Label();
			this.lbl_speed = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.ts_lbl_gps = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.ts_lbl_battery = new System.Windows.Forms.ToolStripLabel();
			this.TS_drones = new System.Windows.Forms.ToolStrip();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.BUT_Connect = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.BUT_Rotation = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.BUT_Configure = new System.Windows.Forms.ToolStripButton();
			this.contextMenuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Commands)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// myMap
			// 
			this.myMap.BackColor = System.Drawing.Color.Transparent;
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
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearMissionToolStripMenuItem,
            this.setHomeHereToolStripMenuItem,
            this.clearAllMissionsToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(174, 70);
			// 
			// ClearMissionToolStripMenuItem
			// 
			this.ClearMissionToolStripMenuItem.Name = "ClearMissionToolStripMenuItem";
			this.ClearMissionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.ClearMissionToolStripMenuItem.Text = "Takeoff to Here";
			this.ClearMissionToolStripMenuItem.Click += new System.EventHandler(this.goHereToolStripMenuItem_Click);
			// 
			// setHomeHereToolStripMenuItem
			// 
			this.setHomeHereToolStripMenuItem.Name = "setHomeHereToolStripMenuItem";
			this.setHomeHereToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.setHomeHereToolStripMenuItem.Text = "Set Home Here";
			this.setHomeHereToolStripMenuItem.Click += new System.EventHandler(this.setHomeHereToolStripMenuItem_Click);
			// 
			// clearAllMissionsToolStripMenuItem
			// 
			this.clearAllMissionsToolStripMenuItem.Name = "clearAllMissionsToolStripMenuItem";
			this.clearAllMissionsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.clearAllMissionsToolStripMenuItem.Text = "Clear All Missions";
			this.clearAllMissionsToolStripMenuItem.Click += new System.EventHandler(this.clearMissionToolStripMenuItem_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.TXT_Mode);
			this.panel1.Controls.Add(this.label3);
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
			// panel2
			// 
			this.panel2.Controls.Add(this.label8);
			this.panel2.Controls.Add(this.but_read_wp);
			this.panel2.Controls.Add(this.but_write_wp);
			this.panel2.Controls.Add(this.but_land);
			this.panel2.Controls.Add(this.but_takeoff);
			this.panel2.Controls.Add(this.but_disarm);
			this.panel2.Controls.Add(this.but_arm_throttle);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel2.Location = new System.Drawing.Point(761, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(247, 109);
			this.panel2.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(3, 5);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(68, 23);
			this.label8.TabIndex = 19;
			this.label8.Text = "Maunal";
			// 
			// but_read_wp
			// 
			this.but_read_wp.BorderThickness = 2F;
			this.but_read_wp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_read_wp.Location = new System.Drawing.Point(5, 68);
			this.but_read_wp.Name = "but_read_wp";
			this.but_read_wp.Size = new System.Drawing.Size(75, 23);
			this.but_read_wp.TabIndex = 28;
			this.but_read_wp.Text = "read_WPs";
			this.but_read_wp.Click += new System.EventHandler(this.BUT_read_Click);
			// 
			// but_write_wp
			// 
			this.but_write_wp.BorderThickness = 2F;
			this.but_write_wp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_write_wp.Location = new System.Drawing.Point(4, 40);
			this.but_write_wp.Name = "but_write_wp";
			this.but_write_wp.Size = new System.Drawing.Size(75, 23);
			this.but_write_wp.TabIndex = 27;
			this.but_write_wp.Text = "write_WPs";
			this.but_write_wp.Click += new System.EventHandler(this.BUT_write_Click);
			// 
			// but_land
			// 
			this.but_land.BorderThickness = 2F;
			this.but_land.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_land.Location = new System.Drawing.Point(86, 68);
			this.but_land.Name = "but_land";
			this.but_land.Size = new System.Drawing.Size(75, 23);
			this.but_land.TabIndex = 26;
			this.but_land.Text = "land";
			this.but_land.Click += new System.EventHandler(this.BUT_Land_Click);
			// 
			// but_takeoff
			// 
			this.but_takeoff.BorderThickness = 2F;
			this.but_takeoff.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_takeoff.Location = new System.Drawing.Point(86, 40);
			this.but_takeoff.Name = "but_takeoff";
			this.but_takeoff.Size = new System.Drawing.Size(75, 23);
			this.but_takeoff.TabIndex = 25;
			this.but_takeoff.Text = "takeoff";
			this.but_takeoff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
			// 
			// but_disarm
			// 
			this.but_disarm.BorderThickness = 2F;
			this.but_disarm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_disarm.Location = new System.Drawing.Point(166, 69);
			this.but_disarm.Name = "but_disarm";
			this.but_disarm.Size = new System.Drawing.Size(75, 23);
			this.but_disarm.TabIndex = 24;
			this.but_disarm.Text = "disarm";
			this.but_disarm.Click += new System.EventHandler(this.BUT_Disarm_Click);
			// 
			// but_arm_throttle
			// 
			this.but_arm_throttle.BorderThickness = 2F;
			this.but_arm_throttle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_arm_throttle.Location = new System.Drawing.Point(166, 40);
			this.but_arm_throttle.Name = "but_arm_throttle";
			this.but_arm_throttle.Size = new System.Drawing.Size(75, 23);
			this.but_arm_throttle.TabIndex = 23;
			this.but_arm_throttle.Text = "arm";
			this.but_arm_throttle.Click += new System.EventHandler(this.BUT_Arm_Click);
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
			// TXT_DefaultAlt
			// 
			this.TXT_DefaultAlt.Location = new System.Drawing.Point(470, 5);
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
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.Commands.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
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
			this.Commands.EnableHeadersVisualStyles = false;
			this.Commands.GridColor = System.Drawing.Color.White;
			this.Commands.Location = new System.Drawing.Point(0, 29);
			this.Commands.Name = "Commands";
			this.Commands.RowHeadersWidth = 50;
			this.Commands.RowTemplate.Height = 24;
			this.Commands.Size = new System.Drawing.Size(745, 80);
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
			// Gauge_alt
			// 
			this.Gauge_alt.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.Gauge_alt.BackColor = System.Drawing.SystemColors.Menu;
			this.Gauge_alt.BaseArcColor = System.Drawing.Color.Gray;
			this.Gauge_alt.BaseArcRadius = 50;
			this.Gauge_alt.BaseArcStart = 135;
			this.Gauge_alt.BaseArcSweep = 270;
			this.Gauge_alt.BaseArcWidth = 2;
			this.Gauge_alt.Center = new System.Drawing.Point(100, 100);
			this.Gauge_alt.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Gauge_alt.Location = new System.Drawing.Point(0, 38);
			this.Gauge_alt.MaxValue = 100F;
			this.Gauge_alt.MinValue = 0F;
			this.Gauge_alt.Name = "Gauge_alt";
			this.Gauge_alt.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
			this.Gauge_alt.NeedleColor2 = System.Drawing.Color.DimGray;
			this.Gauge_alt.NeedleRadius = 30;
			this.Gauge_alt.NeedleType = System.Windows.Forms.NeedleType.Advance;
			this.Gauge_alt.NeedleWidth = 2;
			this.Gauge_alt.ScaleLinesInterColor = System.Drawing.Color.Black;
			this.Gauge_alt.ScaleLinesInterInnerRadius = 45;
			this.Gauge_alt.ScaleLinesInterOuterRadius = 50;
			this.Gauge_alt.ScaleLinesInterWidth = 1;
			this.Gauge_alt.ScaleLinesMajorColor = System.Drawing.Color.Black;
			this.Gauge_alt.ScaleLinesMajorInnerRadius = 40;
			this.Gauge_alt.ScaleLinesMajorOuterRadius = 50;
			this.Gauge_alt.ScaleLinesMajorStepValue = 10F;
			this.Gauge_alt.ScaleLinesMajorWidth = 2;
			this.Gauge_alt.ScaleLinesMinorColor = System.Drawing.Color.Gray;
			this.Gauge_alt.ScaleLinesMinorInnerRadius = 45;
			this.Gauge_alt.ScaleLinesMinorOuterRadius = 50;
			this.Gauge_alt.ScaleLinesMinorTicks = 9;
			this.Gauge_alt.ScaleLinesMinorWidth = 1;
			this.Gauge_alt.ScaleNumbersColor = System.Drawing.Color.Black;
			this.Gauge_alt.ScaleNumbersFormat = null;
			this.Gauge_alt.ScaleNumbersRadius = 60;
			this.Gauge_alt.ScaleNumbersRotation = 0;
			this.Gauge_alt.ScaleNumbersStartScaleLine = 0;
			this.Gauge_alt.ScaleNumbersStepScaleLines = 1;
			this.Gauge_alt.Size = new System.Drawing.Size(203, 184);
			this.Gauge_alt.TabIndex = 2;
			this.Gauge_alt.Text = "aGauge1";
			this.Gauge_alt.Value = 0F;
			// 
			// Gauge_speed
			// 
			this.Gauge_speed.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.Gauge_speed.BackColor = System.Drawing.SystemColors.Menu;
			this.Gauge_speed.BaseArcColor = System.Drawing.Color.Gray;
			this.Gauge_speed.BaseArcRadius = 50;
			this.Gauge_speed.BaseArcStart = 135;
			this.Gauge_speed.BaseArcSweep = 270;
			this.Gauge_speed.BaseArcWidth = 2;
			this.Gauge_speed.Center = new System.Drawing.Point(100, 100);
			this.Gauge_speed.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Gauge_speed.Location = new System.Drawing.Point(0, 222);
			this.Gauge_speed.MaxValue = 30F;
			this.Gauge_speed.MinValue = 0F;
			this.Gauge_speed.Name = "Gauge_speed";
			this.Gauge_speed.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
			this.Gauge_speed.NeedleColor2 = System.Drawing.Color.DimGray;
			this.Gauge_speed.NeedleRadius = 30;
			this.Gauge_speed.NeedleType = System.Windows.Forms.NeedleType.Advance;
			this.Gauge_speed.NeedleWidth = 2;
			this.Gauge_speed.ScaleLinesInterColor = System.Drawing.Color.Black;
			this.Gauge_speed.ScaleLinesInterInnerRadius = 45;
			this.Gauge_speed.ScaleLinesInterOuterRadius = 50;
			this.Gauge_speed.ScaleLinesInterWidth = 1;
			this.Gauge_speed.ScaleLinesMajorColor = System.Drawing.Color.Black;
			this.Gauge_speed.ScaleLinesMajorInnerRadius = 40;
			this.Gauge_speed.ScaleLinesMajorOuterRadius = 50;
			this.Gauge_speed.ScaleLinesMajorStepValue = 5F;
			this.Gauge_speed.ScaleLinesMajorWidth = 2;
			this.Gauge_speed.ScaleLinesMinorColor = System.Drawing.Color.Gray;
			this.Gauge_speed.ScaleLinesMinorInnerRadius = 45;
			this.Gauge_speed.ScaleLinesMinorOuterRadius = 50;
			this.Gauge_speed.ScaleLinesMinorTicks = 9;
			this.Gauge_speed.ScaleLinesMinorWidth = 1;
			this.Gauge_speed.ScaleNumbersColor = System.Drawing.Color.Black;
			this.Gauge_speed.ScaleNumbersFormat = null;
			this.Gauge_speed.ScaleNumbersRadius = 60;
			this.Gauge_speed.ScaleNumbersRotation = 0;
			this.Gauge_speed.ScaleNumbersStartScaleLine = 0;
			this.Gauge_speed.ScaleNumbersStepScaleLines = 1;
			this.Gauge_speed.Size = new System.Drawing.Size(203, 184);
			this.Gauge_speed.TabIndex = 3;
			this.Gauge_speed.Text = "aGauge1";
			this.Gauge_speed.Value = 0F;
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(68, 51);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 19);
			this.label4.TabIndex = 4;
			this.label4.Text = "Altitude";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(71, 232);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 19);
			this.label5.TabIndex = 5;
			this.label5.Text = "Speed";
			// 
			// lbl_alt
			// 
			this.lbl_alt.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbl_alt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_alt.Location = new System.Drawing.Point(64, 186);
			this.lbl_alt.Name = "lbl_alt";
			this.lbl_alt.Size = new System.Drawing.Size(50, 20);
			this.lbl_alt.TabIndex = 6;
			this.lbl_alt.Text = "00";
			this.lbl_alt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_speed
			// 
			this.lbl_speed.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbl_speed.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_speed.Location = new System.Drawing.Point(59, 369);
			this.lbl_speed.Name = "lbl_speed";
			this.lbl_speed.Size = new System.Drawing.Size(50, 20);
			this.lbl_speed.TabIndex = 7;
			this.lbl_speed.Text = "00";
			this.lbl_speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(120, 186);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(23, 19);
			this.label6.TabIndex = 8;
			this.label6.Text = "m";
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(111, 370);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 19);
			this.label7.TabIndex = 9;
			this.label7.Text = "ms";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.ts_lbl_gps,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.ts_lbl_battery});
			this.toolStrip1.Location = new System.Drawing.Point(874, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(125, 25);
			this.toolStrip1.TabIndex = 10;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::FooApplication.Properties.Resources.if_50_111142;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			// 
			// ts_lbl_gps
			// 
			this.ts_lbl_gps.Name = "ts_lbl_gps";
			this.ts_lbl_gps.Size = new System.Drawing.Size(44, 22);
			this.ts_lbl_gps.Text = "strong";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = global::FooApplication.Properties.Resources.if_battery_reduce_energy_charge_2203543;
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "toolStripButton2";
			// 
			// ts_lbl_battery
			// 
			this.ts_lbl_battery.Name = "ts_lbl_battery";
			this.ts_lbl_battery.Size = new System.Drawing.Size(26, 22);
			this.ts_lbl_battery.Text = "0%";
			// 
			// TS_drones
			// 
			this.TS_drones.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.TS_drones.AutoSize = false;
			this.TS_drones.Dock = System.Windows.Forms.DockStyle.None;
			this.TS_drones.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.TS_drones.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.TS_drones.Location = new System.Drawing.Point(943, 157);
			this.TS_drones.Name = "TS_drones";
			this.TS_drones.Padding = new System.Windows.Forms.Padding(1);
			this.TS_drones.Size = new System.Drawing.Size(56, 188);
			this.TS_drones.TabIndex = 12;
			this.TS_drones.Text = "toolStrip2";
			// 
			// timer1
			// 
			this.timer1.Interval = 1200;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// toolStrip2
			// 
			this.toolStrip2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BUT_Connect,
            this.toolStripSeparator2,
            this.BUT_Rotation,
            this.toolStripSeparator3,
            this.BUT_Configure});
			this.toolStrip2.Location = new System.Drawing.Point(417, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Padding = new System.Windows.Forms.Padding(2);
			this.toolStrip2.Size = new System.Drawing.Size(197, 58);
			this.toolStrip2.TabIndex = 13;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// BUT_Connect
			// 
			this.BUT_Connect.Font = new System.Drawing.Font("Arial", 9F);
			this.BUT_Connect.Image = global::FooApplication.Properties.Resources.if_paper_plane_32;
			this.BUT_Connect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.BUT_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BUT_Connect.Name = "BUT_Connect";
			this.BUT_Connect.Size = new System.Drawing.Size(57, 51);
			this.BUT_Connect.Text = "Connect";
			this.BUT_Connect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BUT_Connect.Click += new System.EventHandler(this.BUT_Connect_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
			// 
			// BUT_Rotation
			// 
			this.BUT_Rotation.Font = new System.Drawing.Font("Arial", 9F);
			this.BUT_Rotation.Image = global::FooApplication.Properties.Resources.if_rotation_32;
			this.BUT_Rotation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.BUT_Rotation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BUT_Rotation.Name = "BUT_Rotation";
			this.BUT_Rotation.Size = new System.Drawing.Size(57, 51);
			this.BUT_Rotation.Text = "Rotation";
			this.BUT_Rotation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BUT_Rotation.Click += new System.EventHandler(this.BUT_Rotation_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
			// 
			// BUT_Configure
			// 
			this.BUT_Configure.Font = new System.Drawing.Font("Arial", 9F);
			this.BUT_Configure.Image = global::FooApplication.Properties.Resources.if_settings_32;
			this.BUT_Configure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.BUT_Configure.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BUT_Configure.Name = "BUT_Configure";
			this.BUT_Configure.Size = new System.Drawing.Size(65, 51);
			this.BUT_Configure.Text = "Configure";
			this.BUT_Configure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			// 
			// Planner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 537);
			this.Controls.Add(this.toolStrip2);
			this.Controls.Add(this.TS_drones);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.lbl_speed);
			this.Controls.Add(this.lbl_alt);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Gauge_speed);
			this.Controls.Add(this.Gauge_alt);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.myMap);
			this.Name = "Planner";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Planner";
			this.contextMenuStrip1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Commands)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.TextBox TXT_Mode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ClearMissionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setHomeHereToolStripMenuItem;
		private System.Windows.Forms.AGauge Gauge_alt;
		private System.Windows.Forms.AGauge Gauge_speed;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lbl_alt;
		private System.Windows.Forms.Label lbl_speed;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ToolStripMenuItem clearAllMissionsToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripLabel ts_lbl_gps;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripLabel ts_lbl_battery;
		private System.Windows.Forms.ToolStrip TS_drones;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton BUT_Connect;
		private System.Windows.Forms.ToolStripButton BUT_Rotation;
		private System.Windows.Forms.ToolStripButton BUT_Configure;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.Panel panel2;
		private Controls.MyButton but_arm_throttle;
		private Controls.MyButton but_write_wp;
		private Controls.MyButton but_land;
		private Controls.MyButton but_takeoff;
		private Controls.MyButton but_disarm;
		private System.Windows.Forms.Label label8;
		private Controls.MyButton but_read_wp;
	}
}