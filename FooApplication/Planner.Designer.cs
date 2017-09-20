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
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ClearMissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setHomeHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllMissionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BUT_Rotation = new System.Windows.Forms.Button();
			this.BUT_Read_WP = new System.Windows.Forms.Button();
			this.BUT_Land = new System.Windows.Forms.Button();
			this.BUT_Disarm = new System.Windows.Forms.Button();
			this.btn_takeoff = new System.Windows.Forms.Button();
			this.TXT_Mode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.BUT_Write_wp = new System.Windows.Forms.Button();
			this.BUT_Auto = new System.Windows.Forms.Button();
			this.BUT_Arm = new System.Windows.Forms.Button();
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
			this.but_connection = new System.Windows.Forms.Button();
			this.toolStrip_dronelist = new System.Windows.Forms.ToolStrip();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Commands)).BeginInit();
			this.toolStrip1.SuspendLayout();
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
			this.panel1.Controls.Add(this.BUT_Rotation);
			this.panel1.Controls.Add(this.BUT_Read_WP);
			this.panel1.Controls.Add(this.BUT_Land);
			this.panel1.Controls.Add(this.BUT_Disarm);
			this.panel1.Controls.Add(this.btn_takeoff);
			this.panel1.Controls.Add(this.TXT_Mode);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.BUT_Write_wp);
			this.panel1.Controls.Add(this.BUT_Auto);
			this.panel1.Controls.Add(this.BUT_Arm);
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
			// BUT_Rotation
			// 
			this.BUT_Rotation.Location = new System.Drawing.Point(750, 17);
			this.BUT_Rotation.Name = "BUT_Rotation";
			this.BUT_Rotation.Size = new System.Drawing.Size(75, 23);
			this.BUT_Rotation.TabIndex = 23;
			this.BUT_Rotation.Text = "Rotation";
			this.BUT_Rotation.UseVisualStyleBackColor = true;
			this.BUT_Rotation.Click += new System.EventHandler(this.BUT_Rotation_Click);
			// 
			// BUT_Read_WP
			// 
			this.BUT_Read_WP.BackColor = System.Drawing.SystemColors.HotTrack;
			this.BUT_Read_WP.Location = new System.Drawing.Point(750, 73);
			this.BUT_Read_WP.Name = "BUT_Read_WP";
			this.BUT_Read_WP.Size = new System.Drawing.Size(75, 23);
			this.BUT_Read_WP.TabIndex = 22;
			this.BUT_Read_WP.Text = "Read_WP";
			this.BUT_Read_WP.UseVisualStyleBackColor = false;
			this.BUT_Read_WP.Click += new System.EventHandler(this.BUT_read_Click);
			// 
			// BUT_Land
			// 
			this.BUT_Land.Location = new System.Drawing.Point(832, 45);
			this.BUT_Land.Name = "BUT_Land";
			this.BUT_Land.Size = new System.Drawing.Size(75, 23);
			this.BUT_Land.TabIndex = 21;
			this.BUT_Land.Text = "Land";
			this.BUT_Land.UseVisualStyleBackColor = true;
			this.BUT_Land.Click += new System.EventHandler(this.BUT_Land_Click);
			// 
			// BUT_Disarm
			// 
			this.BUT_Disarm.Location = new System.Drawing.Point(912, 73);
			this.BUT_Disarm.Name = "BUT_Disarm";
			this.BUT_Disarm.Size = new System.Drawing.Size(75, 23);
			this.BUT_Disarm.TabIndex = 20;
			this.BUT_Disarm.Text = "Disarm";
			this.BUT_Disarm.UseVisualStyleBackColor = true;
			this.BUT_Disarm.Click += new System.EventHandler(this.BUT_Disarm_Click);
			// 
			// btn_takeoff
			// 
			this.btn_takeoff.Location = new System.Drawing.Point(831, 17);
			this.btn_takeoff.Name = "btn_takeoff";
			this.btn_takeoff.Size = new System.Drawing.Size(75, 23);
			this.btn_takeoff.TabIndex = 19;
			this.btn_takeoff.Text = "Takeoff";
			this.btn_takeoff.UseVisualStyleBackColor = true;
			this.btn_takeoff.Click += new System.EventHandler(this.btn_takeoff_Click);
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
			this.BUT_Write_wp.BackColor = System.Drawing.SystemColors.HotTrack;
			this.BUT_Write_wp.Location = new System.Drawing.Point(751, 44);
			this.BUT_Write_wp.Name = "BUT_Write_wp";
			this.BUT_Write_wp.Size = new System.Drawing.Size(75, 23);
			this.BUT_Write_wp.TabIndex = 16;
			this.BUT_Write_wp.Text = "Write_WP";
			this.BUT_Write_wp.UseVisualStyleBackColor = false;
			this.BUT_Write_wp.Click += new System.EventHandler(this.BUT_write_Click);
			// 
			// BUT_Auto
			// 
			this.BUT_Auto.Location = new System.Drawing.Point(831, 73);
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
			this.Commands.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
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
			// but_connection
			// 
			this.but_connection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.but_connection.BackColor = System.Drawing.SystemColors.Menu;
			this.but_connection.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_connection.Image = global::FooApplication.Properties.Resources.if_paper_plane_32;
			this.but_connection.Location = new System.Drawing.Point(470, 0);
			this.but_connection.Name = "but_connection";
			this.but_connection.Size = new System.Drawing.Size(75, 62);
			this.but_connection.TabIndex = 11;
			this.but_connection.Text = "Connect";
			this.but_connection.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.but_connection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.but_connection.UseVisualStyleBackColor = false;
			this.but_connection.Click += new System.EventHandler(this.BUT_Connect_Click);
			// 
			// toolStrip_dronelist
			// 
			this.toolStrip_dronelist.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.toolStrip_dronelist.AutoSize = false;
			this.toolStrip_dronelist.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip_dronelist.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip_dronelist.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.toolStrip_dronelist.Location = new System.Drawing.Point(943, 157);
			this.toolStrip_dronelist.Name = "toolStrip_dronelist";
			this.toolStrip_dronelist.Size = new System.Drawing.Size(56, 188);
			this.toolStrip_dronelist.TabIndex = 12;
			this.toolStrip_dronelist.Text = "toolStrip2";
			// 
			// timer1
			// 
			this.timer1.Interval = 1200;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Planner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 537);
			this.Controls.Add(this.toolStrip_dronelist);
			this.Controls.Add(this.but_connection);
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
			((System.ComponentModel.ISupportInitialize)(this.Commands)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
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
		private System.Windows.Forms.Button BUT_Write_wp;
		private System.Windows.Forms.Button BUT_Auto;
		private System.Windows.Forms.Button BUT_Arm;
		private System.Windows.Forms.TextBox TXT_Mode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ClearMissionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setHomeHereToolStripMenuItem;
		private System.Windows.Forms.Button btn_takeoff;
		private System.Windows.Forms.Button BUT_Disarm;
		private System.Windows.Forms.Button BUT_Land;
		private System.Windows.Forms.Button BUT_Read_WP;
		private System.Windows.Forms.Button BUT_Rotation;
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
		private System.Windows.Forms.Button but_connection;
		private System.Windows.Forms.ToolStrip toolStrip_dronelist;
		private System.Windows.Forms.Timer timer1;
	}
}