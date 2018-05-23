using System.Windows.Forms;
using Diva.Controls;

namespace Diva
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Planner));
			this.cmMap = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miClearMission = new System.Windows.Forms.ToolStripMenuItem();
			this.miSetHomeHere = new System.Windows.Forms.ToolStripMenuItem();
			this.miClearAllMissions = new System.Windows.Forms.ToolStripMenuItem();
			this.panelDroneInfo = new System.Windows.Forms.Panel();
			this.BtnAirMode = new System.Windows.Forms.Button();
			this.BtnAltitude = new System.Windows.Forms.Button();
			this.BtnHomeLand = new System.Windows.Forms.Button();
			this.tboxDroneMode = new System.Windows.Forms.TextBox();
			this.tboxAltitudeValue = new System.Windows.Forms.TextBox();
			this.tboxHomeAltitude = new System.Windows.Forms.TextBox();
			this.tboxHomeLongitude = new System.Windows.Forms.TextBox();
			this.tboxHomeLatitude = new System.Windows.Forms.TextBox();
			this.dgvWayPoints = new System.Windows.Forms.DataGridView();
			this.colCommand = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.colParam1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colParam2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colParam3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colParam4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colLatitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colLongitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAltitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.colTagData = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.timerMapItemUpdate = new System.Windows.Forms.Timer(this.components);
			this.TSMainPanel = new System.Windows.Forms.ToolStrip();
			this.TSBtnConnect = new System.Windows.Forms.ToolStripButton();
			this.TSBtnRotation = new System.Windows.Forms.ToolStripButton();
			this.TSBtnConfigure = new System.Windows.Forms.ToolStripButton();
			this.TSBtnTagging = new System.Windows.Forms.ToolStripButton();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.BtnRTL = new System.Windows.Forms.Button();
			this.BtnLand = new System.Windows.Forms.Button();
			this.BtnVideo = new System.Windows.Forms.Button();
			this.BtnAuto = new System.Windows.Forms.Button();
			this.BtnArm = new System.Windows.Forms.Button();
			this.BtnReadWPs = new System.Windows.Forms.Button();
			this.BtnTakeOff = new System.Windows.Forms.Button();
			this.BtnWriteWPs = new System.Windows.Forms.Button();
			this.ImgListBatteryHealth = new System.Windows.Forms.ImageList(this.components);
			this.DroneInfo3 = new Diva.Controls.DroneInfoPanel();
			this.DroneInfo2 = new Diva.Controls.DroneInfoPanel();
			this.DroneInfo1 = new Diva.Controls.DroneInfoPanel();
			this.myMap = new Diva.Controls.MyGMap();
			this.cmMap.SuspendLayout();
			this.panelDroneInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvWayPoints)).BeginInit();
			this.TSMainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmMap
			// 
			this.cmMap.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.cmMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClearMission,
            this.miSetHomeHere,
            this.miClearAllMissions});
			this.cmMap.Name = "contextMenuStrip1";
			this.cmMap.Size = new System.Drawing.Size(174, 70);
			// 
			// miClearMission
			// 
			this.miClearMission.Name = "miClearMission";
			this.miClearMission.Size = new System.Drawing.Size(173, 22);
			this.miClearMission.Text = "Takeoff to Here";
			this.miClearMission.Click += new System.EventHandler(this.goHereToolStripMenuItem_Click);
			// 
			// miSetHomeHere
			// 
			this.miSetHomeHere.Name = "miSetHomeHere";
			this.miSetHomeHere.Size = new System.Drawing.Size(173, 22);
			this.miSetHomeHere.Text = "Set Home Here";
			this.miSetHomeHere.Click += new System.EventHandler(this.setHomeHereToolStripMenuItem_Click);
			// 
			// miClearAllMissions
			// 
			this.miClearAllMissions.Name = "miClearAllMissions";
			this.miClearAllMissions.Size = new System.Drawing.Size(173, 22);
			this.miClearAllMissions.Text = "Clear All Missions";
			this.miClearAllMissions.Click += new System.EventHandler(this.clearMissionToolStripMenuItem_Click);
			// 
			// panelDroneInfo
			// 
			this.panelDroneInfo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.panelDroneInfo.Controls.Add(this.BtnAirMode);
			this.panelDroneInfo.Controls.Add(this.BtnAltitude);
			this.panelDroneInfo.Controls.Add(this.BtnHomeLand);
			this.panelDroneInfo.Controls.Add(this.tboxDroneMode);
			this.panelDroneInfo.Controls.Add(this.tboxAltitudeValue);
			this.panelDroneInfo.Controls.Add(this.tboxHomeAltitude);
			this.panelDroneInfo.Controls.Add(this.tboxHomeLongitude);
			this.panelDroneInfo.Controls.Add(this.tboxHomeLatitude);
			this.panelDroneInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelDroneInfo.Location = new System.Drawing.Point(0, 0);
			this.panelDroneInfo.Name = "panelDroneInfo";
			this.panelDroneInfo.Size = new System.Drawing.Size(948, 51);
			this.panelDroneInfo.TabIndex = 1;
			// 
			// BtnAirMode
			// 
			this.BtnAirMode.AutoSize = true;
			this.BtnAirMode.BackColor = System.Drawing.Color.White;
			this.BtnAirMode.Enabled = false;
			this.BtnAirMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnAirMode.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnAirMode.Image = global::Diva.Properties.Resources.news_32;
			this.BtnAirMode.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.BtnAirMode.Location = new System.Drawing.Point(667, 5);
			this.BtnAirMode.Name = "BtnAirMode";
			this.BtnAirMode.Size = new System.Drawing.Size(117, 40);
			this.BtnAirMode.TabIndex = 20;
			this.BtnAirMode.Text = "SetMode";
			this.BtnAirMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.BtnAirMode.UseVisualStyleBackColor = false;
			// 
			// BtnAltitude
			// 
			this.BtnAltitude.AutoSize = true;
			this.BtnAltitude.BackColor = System.Drawing.Color.White;
			this.BtnAltitude.Enabled = false;
			this.BtnAltitude.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnAltitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnAltitude.Image = global::Diva.Properties.Resources.size_height_edit_32;
			this.BtnAltitude.Location = new System.Drawing.Point(434, 5);
			this.BtnAltitude.Name = "BtnAltitude";
			this.BtnAltitude.Size = new System.Drawing.Size(117, 40);
			this.BtnAltitude.TabIndex = 19;
			this.BtnAltitude.Text = "SetAltitude";
			this.BtnAltitude.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.BtnAltitude.UseVisualStyleBackColor = false;
			// 
			// BtnHomeLand
			// 
			this.BtnHomeLand.AutoSize = true;
			this.BtnHomeLand.BackColor = System.Drawing.Color.White;
			this.BtnHomeLand.Enabled = false;
			this.BtnHomeLand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnHomeLand.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnHomeLand.Image = global::Diva.Properties.Resources.home_32;
			this.BtnHomeLand.Location = new System.Drawing.Point(5, 5);
			this.BtnHomeLand.Name = "BtnHomeLand";
			this.BtnHomeLand.Size = new System.Drawing.Size(114, 40);
			this.BtnHomeLand.TabIndex = 16;
			this.BtnHomeLand.Text = "HomeLand";
			this.BtnHomeLand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.BtnHomeLand.UseVisualStyleBackColor = false;
			// 
			// tboxDroneMode
			// 
			this.tboxDroneMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tboxDroneMode.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tboxDroneMode.Location = new System.Drawing.Point(790, 5);
			this.tboxDroneMode.Multiline = true;
			this.tboxDroneMode.Name = "tboxDroneMode";
			this.tboxDroneMode.Size = new System.Drawing.Size(80, 40);
			this.tboxDroneMode.TabIndex = 18;
			this.tboxDroneMode.Text = "Suspend";
			// 
			// tboxAltitudeValue
			// 
			this.tboxAltitudeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tboxAltitudeValue.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tboxAltitudeValue.Location = new System.Drawing.Point(557, 5);
			this.tboxAltitudeValue.Multiline = true;
			this.tboxAltitudeValue.Name = "tboxAltitudeValue";
			this.tboxAltitudeValue.Size = new System.Drawing.Size(80, 40);
			this.tboxAltitudeValue.TabIndex = 12;
			this.tboxAltitudeValue.Text = "100";
			// 
			// tboxHomeAltitude
			// 
			this.tboxHomeAltitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tboxHomeAltitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tboxHomeAltitude.Location = new System.Drawing.Point(297, 5);
			this.tboxHomeAltitude.Multiline = true;
			this.tboxHomeAltitude.Name = "tboxHomeAltitude";
			this.tboxHomeAltitude.Size = new System.Drawing.Size(80, 40);
			this.tboxHomeAltitude.TabIndex = 10;
			// 
			// tboxHomeLongitude
			// 
			this.tboxHomeLongitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tboxHomeLongitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tboxHomeLongitude.Location = new System.Drawing.Point(211, 5);
			this.tboxHomeLongitude.Multiline = true;
			this.tboxHomeLongitude.Name = "tboxHomeLongitude";
			this.tboxHomeLongitude.Size = new System.Drawing.Size(80, 40);
			this.tboxHomeLongitude.TabIndex = 9;
			// 
			// tboxHomeLatitude
			// 
			this.tboxHomeLatitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tboxHomeLatitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tboxHomeLatitude.Location = new System.Drawing.Point(125, 5);
			this.tboxHomeLatitude.Multiline = true;
			this.tboxHomeLatitude.Name = "tboxHomeLatitude";
			this.tboxHomeLatitude.Size = new System.Drawing.Size(80, 40);
			this.tboxHomeLatitude.TabIndex = 8;
			// 
			// dgvWayPoints
			// 
			this.dgvWayPoints.AllowUserToAddRows = false;
			this.dgvWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvWayPoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvWayPoints.ColumnHeadersHeight = 30;
			this.dgvWayPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCommand,
            this.colParam1,
            this.colParam2,
            this.colParam3,
            this.colParam4,
            this.colLatitude,
            this.colLongitude,
            this.colAltitude,
            this.colAngle,
            this.colDelete,
            this.colTagData});
			this.dgvWayPoints.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvWayPoints.EnableHeadersVisualStyles = false;
			this.dgvWayPoints.GridColor = System.Drawing.Color.White;
			this.dgvWayPoints.Location = new System.Drawing.Point(0, 51);
			this.dgvWayPoints.Margin = new System.Windows.Forms.Padding(4);
			this.dgvWayPoints.Name = "dgvWayPoints";
			this.dgvWayPoints.RowHeadersWidth = 50;
			this.dgvWayPoints.RowTemplate.Height = 24;
			this.dgvWayPoints.Size = new System.Drawing.Size(948, 69);
			this.dgvWayPoints.TabIndex = 6;
			this.dgvWayPoints.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_CellContentClick);
			this.dgvWayPoints.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_RowEnter);
			this.dgvWayPoints.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Commands_RowsAdded);
			this.dgvWayPoints.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Commands_RowValidating);
			// 
			// colCommand
			// 
			this.colCommand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colCommand.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
			this.colCommand.HeaderText = "Command";
			this.colCommand.MinimumWidth = 60;
			this.colCommand.Name = "colCommand";
			this.colCommand.ToolTipText = "APM Command";
			this.colCommand.Width = 60;
			// 
			// colParam1
			// 
			this.colParam1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colParam1.HeaderText = "Param1";
			this.colParam1.MinimumWidth = 50;
			this.colParam1.Name = "colParam1";
			this.colParam1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colParam1.Width = 50;
			// 
			// colParam2
			// 
			this.colParam2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colParam2.HeaderText = "Param2";
			this.colParam2.MinimumWidth = 50;
			this.colParam2.Name = "colParam2";
			this.colParam2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colParam2.Width = 50;
			// 
			// colParam3
			// 
			this.colParam3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colParam3.HeaderText = "Param3";
			this.colParam3.MinimumWidth = 50;
			this.colParam3.Name = "colParam3";
			this.colParam3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colParam3.Width = 50;
			// 
			// colParam4
			// 
			this.colParam4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colParam4.HeaderText = "Param4";
			this.colParam4.MinimumWidth = 50;
			this.colParam4.Name = "colParam4";
			this.colParam4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colParam4.Width = 50;
			// 
			// colLatitude
			// 
			this.colLatitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colLatitude.HeaderText = "Latitude";
			this.colLatitude.MinimumWidth = 60;
			this.colLatitude.Name = "colLatitude";
			this.colLatitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colLatitude.Width = 60;
			// 
			// colLongitude
			// 
			this.colLongitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colLongitude.HeaderText = "Longitude";
			this.colLongitude.MinimumWidth = 60;
			this.colLongitude.Name = "colLongitude";
			this.colLongitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colLongitude.Width = 60;
			// 
			// colAltitude
			// 
			this.colAltitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colAltitude.HeaderText = "Altitude";
			this.colAltitude.MinimumWidth = 60;
			this.colAltitude.Name = "colAltitude";
			this.colAltitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colAltitude.Width = 60;
			// 
			// colAngle
			// 
			this.colAngle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colAngle.HeaderText = "Angle";
			this.colAngle.MinimumWidth = 60;
			this.colAngle.Name = "colAngle";
			this.colAngle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colAngle.Width = 60;
			// 
			// colDelete
			// 
			this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colDelete.HeaderText = "Delete";
			this.colDelete.MinimumWidth = 50;
			this.colDelete.Name = "colDelete";
			this.colDelete.Text = "X";
			this.colDelete.ToolTipText = "Delete the row";
			this.colDelete.Width = 50;
			// 
			// colTagData
			// 
			this.colTagData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colTagData.HeaderText = "TagData";
			this.colTagData.MinimumWidth = 50;
			this.colTagData.Name = "colTagData";
			this.colTagData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colTagData.Visible = false;
			this.colTagData.Width = 50;
			// 
			// timerMapItemUpdate
			// 
			this.timerMapItemUpdate.Interval = 1200;
			this.timerMapItemUpdate.Tick += new System.EventHandler(this.timerMapItemUpdate_Tick);
			// 
			// TSMainPanel
			// 
			this.TSMainPanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.TSMainPanel.Dock = System.Windows.Forms.DockStyle.None;
			this.TSMainPanel.GripMargin = new System.Windows.Forms.Padding(0);
			this.TSMainPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.TSMainPanel.ImageScalingSize = new System.Drawing.Size(0, 0);
			this.TSMainPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSBtnConnect,
            this.TSBtnRotation,
            this.TSBtnConfigure,
            this.TSBtnTagging});
			this.TSMainPanel.Location = new System.Drawing.Point(9, 9);
			this.TSMainPanel.Name = "TSMainPanel";
			this.TSMainPanel.Padding = new System.Windows.Forms.Padding(0);
			this.TSMainPanel.Size = new System.Drawing.Size(306, 79);
			this.TSMainPanel.TabIndex = 13;
			this.TSMainPanel.Text = "toolStrip2";
			// 
			// TSBtnConnect
			// 
			this.TSBtnConnect.AutoSize = false;
			this.TSBtnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSBtnConnect.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TSBtnConnect.Image = global::Diva.Properties.Resources.icon_arm;
			this.TSBtnConnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TSBtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSBtnConnect.Margin = new System.Windows.Forms.Padding(0);
			this.TSBtnConnect.Name = "TSBtnConnect";
			this.TSBtnConnect.Size = new System.Drawing.Size(76, 76);
			this.TSBtnConnect.Text = "Connect";
			this.TSBtnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.TSBtnConnect.Click += new System.EventHandler(this.BUT_Connect_Click);
			this.TSBtnConnect.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
			this.TSBtnConnect.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
			// 
			// TSBtnRotation
			// 
			this.TSBtnRotation.AutoSize = false;
			this.TSBtnRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSBtnRotation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TSBtnRotation.Image = global::Diva.Properties.Resources.icon_rotation;
			this.TSBtnRotation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TSBtnRotation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSBtnRotation.Name = "TSBtnRotation";
			this.TSBtnRotation.Size = new System.Drawing.Size(76, 76);
			this.TSBtnRotation.Text = "Rotation";
			this.TSBtnRotation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.TSBtnRotation.Click += new System.EventHandler(this.BUT_Rotation_Click);
			this.TSBtnRotation.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
			this.TSBtnRotation.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
			// 
			// TSBtnConfigure
			// 
			this.TSBtnConfigure.AutoSize = false;
			this.TSBtnConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSBtnConfigure.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TSBtnConfigure.Image = global::Diva.Properties.Resources.icon_configure;
			this.TSBtnConfigure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TSBtnConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSBtnConfigure.Name = "TSBtnConfigure";
			this.TSBtnConfigure.Size = new System.Drawing.Size(76, 76);
			this.TSBtnConfigure.Text = "Configure";
			this.TSBtnConfigure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.TSBtnConfigure.Click += new System.EventHandler(this.BUT_Configure_Click);
			this.TSBtnConfigure.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
			this.TSBtnConfigure.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
			// 
			// TSBtnTagging
			// 
			this.TSBtnTagging.AutoSize = false;
			this.TSBtnTagging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSBtnTagging.Font = new System.Drawing.Font("Tahoma", 9F);
			this.TSBtnTagging.Image = global::Diva.Properties.Resources.icon_tagging;
			this.TSBtnTagging.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.TSBtnTagging.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSBtnTagging.Name = "TSBtnTagging";
			this.TSBtnTagging.Size = new System.Drawing.Size(76, 76);
			this.TSBtnTagging.Text = "Tagging";
			this.TSBtnTagging.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.TSBtnTagging.Click += new System.EventHandler(this.BUT_Tagging_Click);
			this.TSBtnTagging.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
			this.TSBtnTagging.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Margin = new System.Windows.Forms.Padding(2);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.DroneInfo3);
			this.splitContainer.Panel1.Controls.Add(this.DroneInfo2);
			this.splitContainer.Panel1.Controls.Add(this.DroneInfo1);
			this.splitContainer.Panel1.Controls.Add(this.BtnRTL);
			this.splitContainer.Panel1.Controls.Add(this.BtnLand);
			this.splitContainer.Panel1.Controls.Add(this.BtnVideo);
			this.splitContainer.Panel1.Controls.Add(this.BtnAuto);
			this.splitContainer.Panel1.Controls.Add(this.BtnArm);
			this.splitContainer.Panel1.Controls.Add(this.BtnReadWPs);
			this.splitContainer.Panel1.Controls.Add(this.BtnTakeOff);
			this.splitContainer.Panel1.Controls.Add(this.BtnWriteWPs);
			this.splitContainer.Panel1.Controls.Add(this.TSMainPanel);
			this.splitContainer.Panel1.Controls.Add(this.myMap);
			this.splitContainer.Panel1MinSize = 240;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.dgvWayPoints);
			this.splitContainer.Panel2.Controls.Add(this.panelDroneInfo);
			this.splitContainer.Panel2MinSize = 120;
			this.splitContainer.Size = new System.Drawing.Size(948, 610);
			this.splitContainer.SplitterDistance = 487;
			this.splitContainer.SplitterWidth = 3;
			this.splitContainer.TabIndex = 15;
			// 
			// BtnRTL
			// 
			this.BtnRTL.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnRTL.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnRTL.FlatAppearance.BorderSize = 0;
			this.BtnRTL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnRTL.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnRTL.ForeColor = System.Drawing.Color.White;
			this.BtnRTL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnRTL.Location = new System.Drawing.Point(9, 384);
			this.BtnRTL.Margin = new System.Windows.Forms.Padding(4);
			this.BtnRTL.Name = "BtnRTL";
			this.BtnRTL.Size = new System.Drawing.Size(90, 87);
			this.BtnRTL.TabIndex = 15;
			this.BtnRTL.Text = "RTL";
			this.BtnRTL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnRTL.UseVisualStyleBackColor = false;
			this.BtnRTL.Click += new System.EventHandler(this.BUT_RTL_Click);
			// 
			// BtnLand
			// 
			this.BtnLand.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnLand.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnLand.FlatAppearance.BorderSize = 0;
			this.BtnLand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnLand.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnLand.ForeColor = System.Drawing.Color.White;
			this.BtnLand.Image = global::Diva.Properties.Resources.icon_land;
			this.BtnLand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnLand.Location = new System.Drawing.Point(9, 198);
			this.BtnLand.Name = "BtnLand";
			this.BtnLand.Size = new System.Drawing.Size(90, 87);
			this.BtnLand.TabIndex = 1;
			this.BtnLand.Text = "LAND";
			this.BtnLand.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnLand.UseVisualStyleBackColor = false;
			this.BtnLand.Click += new System.EventHandler(this.BUT_Land_Click);
			this.BtnLand.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
			this.BtnLand.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
			// 
			// BtnVideo
			// 
			this.BtnVideo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnVideo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnVideo.FlatAppearance.BorderSize = 0;
			this.BtnVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnVideo.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnVideo.ForeColor = System.Drawing.Color.White;
			this.BtnVideo.Image = global::Diva.Properties.Resources.icon_add;
			this.BtnVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnVideo.Location = new System.Drawing.Point(105, 384);
			this.BtnVideo.Margin = new System.Windows.Forms.Padding(4);
			this.BtnVideo.Name = "BtnVideo";
			this.BtnVideo.Size = new System.Drawing.Size(90, 87);
			this.BtnVideo.TabIndex = 7;
			this.BtnVideo.Text = "VIDEO";
			this.BtnVideo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnVideo.UseVisualStyleBackColor = false;
			this.BtnVideo.Click += new System.EventHandler(this.VideoDemo_Click);
			this.BtnVideo.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
			this.BtnVideo.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
			// 
			// BtnAuto
			// 
			this.BtnAuto.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnAuto.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnAuto.FlatAppearance.BorderSize = 0;
			this.BtnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnAuto.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnAuto.ForeColor = System.Drawing.Color.White;
			this.BtnAuto.Image = global::Diva.Properties.Resources.icon_auto;
			this.BtnAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnAuto.Location = new System.Drawing.Point(105, 198);
			this.BtnAuto.Name = "BtnAuto";
			this.BtnAuto.Size = new System.Drawing.Size(90, 87);
			this.BtnAuto.TabIndex = 3;
			this.BtnAuto.Text = "AUTO";
			this.BtnAuto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnAuto.UseVisualStyleBackColor = false;
			this.BtnAuto.Click += new System.EventHandler(this.BUT_Auto_Click);
			this.BtnAuto.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
			this.BtnAuto.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
			// 
			// BtnArm
			// 
			this.BtnArm.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnArm.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnArm.FlatAppearance.BorderSize = 0;
			this.BtnArm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnArm.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnArm.ForeColor = System.Drawing.Color.White;
			this.BtnArm.Image = global::Diva.Properties.Resources.icon_connect;
			this.BtnArm.Location = new System.Drawing.Point(9, 105);
			this.BtnArm.Name = "BtnArm";
			this.BtnArm.Size = new System.Drawing.Size(90, 87);
			this.BtnArm.TabIndex = 0;
			this.BtnArm.Text = "ARM";
			this.BtnArm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnArm.UseVisualStyleBackColor = false;
			this.BtnArm.Click += new System.EventHandler(this.BUT_Arm_Click);
			this.BtnArm.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
			this.BtnArm.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
			// 
			// BtnReadWPs
			// 
			this.BtnReadWPs.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnReadWPs.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnReadWPs.FlatAppearance.BorderSize = 0;
			this.BtnReadWPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnReadWPs.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnReadWPs.ForeColor = System.Drawing.Color.White;
			this.BtnReadWPs.Image = global::Diva.Properties.Resources.icon_readwps;
			this.BtnReadWPs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnReadWPs.Location = new System.Drawing.Point(105, 291);
			this.BtnReadWPs.Name = "BtnReadWPs";
			this.BtnReadWPs.Size = new System.Drawing.Size(90, 87);
			this.BtnReadWPs.TabIndex = 5;
			this.BtnReadWPs.Text = "READWPS";
			this.BtnReadWPs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnReadWPs.UseVisualStyleBackColor = false;
			this.BtnReadWPs.Click += new System.EventHandler(this.BUT_read_Click);
			this.BtnReadWPs.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
			this.BtnReadWPs.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
			// 
			// BtnTakeOff
			// 
			this.BtnTakeOff.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnTakeOff.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnTakeOff.FlatAppearance.BorderSize = 0;
			this.BtnTakeOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnTakeOff.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnTakeOff.ForeColor = System.Drawing.Color.White;
			this.BtnTakeOff.Image = global::Diva.Properties.Resources.icon_takeoff;
			this.BtnTakeOff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnTakeOff.Location = new System.Drawing.Point(105, 105);
			this.BtnTakeOff.Name = "BtnTakeOff";
			this.BtnTakeOff.Size = new System.Drawing.Size(90, 87);
			this.BtnTakeOff.TabIndex = 2;
			this.BtnTakeOff.Text = "TAKEOFF";
			this.BtnTakeOff.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnTakeOff.UseVisualStyleBackColor = false;
			this.BtnTakeOff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
			this.BtnTakeOff.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
			this.BtnTakeOff.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
			// 
			// BtnWriteWPs
			// 
			this.BtnWriteWPs.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BtnWriteWPs.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnWriteWPs.FlatAppearance.BorderSize = 0;
			this.BtnWriteWPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnWriteWPs.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnWriteWPs.ForeColor = System.Drawing.Color.White;
			this.BtnWriteWPs.Image = global::Diva.Properties.Resources.icon_writewps;
			this.BtnWriteWPs.Location = new System.Drawing.Point(9, 291);
			this.BtnWriteWPs.Name = "BtnWriteWPs";
			this.BtnWriteWPs.Size = new System.Drawing.Size(90, 87);
			this.BtnWriteWPs.TabIndex = 4;
			this.BtnWriteWPs.Text = "WRITEWPS";
			this.BtnWriteWPs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.BtnWriteWPs.UseVisualStyleBackColor = false;
			this.BtnWriteWPs.Click += new System.EventHandler(this.BUT_write_Click);
			this.BtnWriteWPs.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
			this.BtnWriteWPs.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
			// 
			// ImgListBatteryHealth
			// 
			this.ImgListBatteryHealth.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgListBatteryHealth.ImageStream")));
			this.ImgListBatteryHealth.TransparentColor = System.Drawing.Color.Transparent;
			this.ImgListBatteryHealth.Images.SetKeyName(0, "icon-battery-0.png");
			this.ImgListBatteryHealth.Images.SetKeyName(1, "icon-battery-20.png");
			this.ImgListBatteryHealth.Images.SetKeyName(2, "icon-battery-40.png");
			this.ImgListBatteryHealth.Images.SetKeyName(3, "icon-battery-60.png");
			this.ImgListBatteryHealth.Images.SetKeyName(4, "icon-battery-80.png");
			this.ImgListBatteryHealth.Images.SetKeyName(5, "icon-battery-100.png");
			// 
			// DroneInfo3
			// 
			this.DroneInfo3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DroneInfo3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.DroneInfo3.Enabled = false;
			this.DroneInfo3.Location = new System.Drawing.Point(592, 178);
			this.DroneInfo3.Name = "DroneInfo3";
			this.DroneInfo3.Size = new System.Drawing.Size(344, 78);
			this.DroneInfo3.TabIndex = 18;
			this.DroneInfo3.Tag = "2";
			// 
			// DroneInfo2
			// 
			this.DroneInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DroneInfo2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.DroneInfo2.Enabled = false;
			this.DroneInfo2.Location = new System.Drawing.Point(592, 94);
			this.DroneInfo2.Name = "DroneInfo2";
			this.DroneInfo2.Size = new System.Drawing.Size(344, 78);
			this.DroneInfo2.TabIndex = 17;
			this.DroneInfo2.Tag = "1";
			// 
			// DroneInfo1
			// 
			this.DroneInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DroneInfo1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.DroneInfo1.Enabled = false;
			this.DroneInfo1.Location = new System.Drawing.Point(592, 10);
			this.DroneInfo1.Name = "DroneInfo1";
			this.DroneInfo1.Size = new System.Drawing.Size(344, 78);
			this.DroneInfo1.TabIndex = 16;
			this.DroneInfo1.Tag = "0";
			// 
			// myMap
			// 
			this.myMap.BackColor = System.Drawing.SystemColors.Control;
			this.myMap.Bearing = 0F;
			this.myMap.CanDragMap = true;
			this.myMap.ContextMenuStrip = this.cmMap;
			this.myMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.myMap.EmptyTileColor = System.Drawing.Color.Navy;
			this.myMap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.myMap.GrayScaleMode = false;
			this.myMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.myMap.LevelsKeepInMemmory = 5;
			this.myMap.Location = new System.Drawing.Point(0, 0);
			this.myMap.MarkersEnabled = true;
			this.myMap.MaxZoom = 2;
			this.myMap.MinZoom = 2;
			this.myMap.MouseWheelZoomEnabled = true;
			this.myMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.myMap.Name = "myMap";
			this.myMap.NegativeMode = false;
			this.myMap.PolygonsEnabled = true;
			this.myMap.RetryLoadTile = 0;
			this.myMap.RoutesEnabled = true;
			this.myMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.myMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.myMap.ShowTileGridLines = false;
			this.myMap.Size = new System.Drawing.Size(948, 487);
			this.myMap.TabIndex = 14;
			this.myMap.Zoom = 0D;
			this.myMap.Load += new System.EventHandler(this.myMap_Load);
			// 
			// Planner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(948, 610);
			this.Controls.Add(this.splitContainer);
			this.MinimumSize = new System.Drawing.Size(964, 648);
			this.Name = "Planner";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Planner";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Planner_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Planner_FormClosed);
			this.Load += new System.EventHandler(this.Planner_Load);
			this.cmMap.ResumeLayout(false);
			this.panelDroneInfo.ResumeLayout(false);
			this.panelDroneInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvWayPoints)).EndInit();
			this.TSMainPanel.ResumeLayout(false);
			this.TSMainPanel.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelDroneInfo;
		private System.Windows.Forms.DataGridView dgvWayPoints;
		private System.Windows.Forms.TextBox tboxHomeAltitude;
		private System.Windows.Forms.TextBox tboxHomeLongitude;
		private System.Windows.Forms.TextBox tboxHomeLatitude;
		private System.Windows.Forms.TextBox tboxAltitudeValue;
		private System.Windows.Forms.TextBox tboxDroneMode;
		private System.Windows.Forms.ContextMenuStrip cmMap;
		private System.Windows.Forms.ToolStripMenuItem miClearMission;
		private System.Windows.Forms.ToolStripMenuItem miSetHomeHere;
		private System.Windows.Forms.ToolStripMenuItem miClearAllMissions;
		private System.Windows.Forms.Timer timerMapItemUpdate;
		private System.Windows.Forms.ToolStrip TSMainPanel;
		private System.Windows.Forms.ToolStripButton TSBtnConnect;
		private System.Windows.Forms.ToolStripButton TSBtnRotation;
		private System.Windows.Forms.ToolStripButton TSBtnConfigure;
		private MyGMap myMap;
		private Button BtnArm;
		private Button BtnReadWPs;
		private Button BtnWriteWPs;
		private Button BtnAuto;
		private Button BtnTakeOff;
		private Button BtnLand;
		private ToolStripButton TSBtnTagging;
        private Button BtnVideo;
        private DataGridViewComboBoxColumn colCommand;
        private DataGridViewTextBoxColumn colParam1;
        private DataGridViewTextBoxColumn colParam2;
        private DataGridViewTextBoxColumn colParam3;
        private DataGridViewTextBoxColumn colParam4;
        private DataGridViewTextBoxColumn colLatitude;
        private DataGridViewTextBoxColumn colLongitude;
        private DataGridViewTextBoxColumn colAltitude;
        private DataGridViewTextBoxColumn colAngle;
        private DataGridViewButtonColumn colDelete;
        private DataGridViewTextBoxColumn colTagData;
        private SplitContainer splitContainer;
		private Button BtnHomeLand;
		private Button BtnAltitude;
		private Button BtnAirMode;
		private ImageList ImgListBatteryHealth;
		private Button BtnRTL;
		private DroneInfoPanel DroneInfo1;
		private DroneInfoPanel DroneInfo2;
		private DroneInfoPanel DroneInfo3;
	}
}