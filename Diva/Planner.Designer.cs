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
            this.cmMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miClearMission = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetHomeHere = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearAllMissions = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDroneInfo = new System.Windows.Forms.Panel();
            this.tboxDroneMode = new System.Windows.Forms.TextBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.tboxAltitudeValue = new System.Windows.Forms.TextBox();
            this.lblAltitude = new System.Windows.Forms.Label();
            this.tboxHomeAltitude = new System.Windows.Forms.TextBox();
            this.tboxHomeLongitude = new System.Windows.Forms.TextBox();
            this.tboxHomeLatitude = new System.Windows.Forms.TextBox();
            this.lblHome = new System.Windows.Forms.Label();
            this.gboxManualButtons = new System.Windows.Forms.GroupBox();
            this.btnAddwp = new System.Windows.Forms.Button();
            this.btnVideo = new System.Windows.Forms.Button();
            this.btnReadWPs = new System.Windows.Forms.Button();
            this.btnWriteWPs = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.btnTakeOff = new System.Windows.Forms.Button();
            this.btnLand = new System.Windows.Forms.Button();
            this.btnArm = new System.Windows.Forms.Button();
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
            this.gaugeAltitude = new System.Windows.Forms.AGauge();
            this.gaugeSpeed = new System.Windows.Forms.AGauge();
            this.lblGaugeAltitude = new System.Windows.Forms.Label();
            this.labelGaugeSpeed = new System.Windows.Forms.Label();
            this.lblGagueAltitudeValue = new System.Windows.Forms.Label();
            this.lblGagueSpeedValue = new System.Windows.Forms.Label();
            this.lblGaugeAltitudeUnit = new System.Windows.Forms.Label();
            this.lblGagueSpeedUnit = new System.Windows.Forms.Label();
            this.tsDroneStatus = new System.Windows.Forms.ToolStrip();
            this.tsbtnGPSSignalIcon = new System.Windows.Forms.ToolStripButton();
            this.tslblGPS = new System.Windows.Forms.ToolStripLabel();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnBatteryHealthIcon = new System.Windows.Forms.ToolStripButton();
            this.tslblBattery = new System.Windows.Forms.ToolStripLabel();
            this.tsDroneList = new System.Windows.Forms.ToolStrip();
            this.timerMapItemUpdate = new System.Windows.Forms.Timer(this.components);
            this.tsMainFunctions = new System.Windows.Forms.ToolStrip();
            this.tsbtnConnect = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnRotation = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnConfigure = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnTagging = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDeplex = new System.Windows.Forms.ToolStripButton();
            this.myMap = new Diva.Controls.MyGMap();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.cmMap.SuspendLayout();
            this.panelDroneInfo.SuspendLayout();
            this.gboxManualButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWayPoints)).BeginInit();
            this.tsDroneStatus.SuspendLayout();
            this.tsMainFunctions.SuspendLayout();
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
            this.cmMap.Size = new System.Drawing.Size(211, 104);
            // 
            // miClearMission
            // 
            this.miClearMission.Name = "miClearMission";
            this.miClearMission.Size = new System.Drawing.Size(210, 24);
            this.miClearMission.Text = "Takeoff to Here";
            this.miClearMission.Click += new System.EventHandler(this.goHereToolStripMenuItem_Click);
            // 
            // miSetHomeHere
            // 
            this.miSetHomeHere.Name = "miSetHomeHere";
            this.miSetHomeHere.Size = new System.Drawing.Size(210, 24);
            this.miSetHomeHere.Text = "Set Home Here";
            this.miSetHomeHere.Click += new System.EventHandler(this.setHomeHereToolStripMenuItem_Click);
            // 
            // miClearAllMissions
            // 
            this.miClearAllMissions.Name = "miClearAllMissions";
            this.miClearAllMissions.Size = new System.Drawing.Size(210, 24);
            this.miClearAllMissions.Text = "Clear All Missions";
            this.miClearAllMissions.Click += new System.EventHandler(this.clearMissionToolStripMenuItem_Click);
            // 
            // panelDroneInfo
            // 
            this.panelDroneInfo.Controls.Add(this.tboxDroneMode);
            this.panelDroneInfo.Controls.Add(this.lblMode);
            this.panelDroneInfo.Controls.Add(this.tboxAltitudeValue);
            this.panelDroneInfo.Controls.Add(this.lblAltitude);
            this.panelDroneInfo.Controls.Add(this.tboxHomeAltitude);
            this.panelDroneInfo.Controls.Add(this.tboxHomeLongitude);
            this.panelDroneInfo.Controls.Add(this.tboxHomeLatitude);
            this.panelDroneInfo.Controls.Add(this.lblHome);
            this.panelDroneInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDroneInfo.Location = new System.Drawing.Point(0, 0);
            this.panelDroneInfo.Margin = new System.Windows.Forms.Padding(4);
            this.panelDroneInfo.Name = "panelDroneInfo";
            this.panelDroneInfo.Size = new System.Drawing.Size(885, 32);
            this.panelDroneInfo.TabIndex = 1;
            // 
            // tboxDroneMode
            // 
            this.tboxDroneMode.Location = new System.Drawing.Point(707, 4);
            this.tboxDroneMode.Margin = new System.Windows.Forms.Padding(4);
            this.tboxDroneMode.Name = "tboxDroneMode";
            this.tboxDroneMode.Size = new System.Drawing.Size(105, 25);
            this.tboxDroneMode.TabIndex = 18;
            this.tboxDroneMode.Text = "NULL";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Location = new System.Drawing.Point(636, 6);
            this.lblMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(58, 24);
            this.lblMode.TabIndex = 17;
            this.lblMode.Text = "Mode";
            // 
            // tboxAltitudeValue
            // 
            this.tboxAltitudeValue.Location = new System.Drawing.Point(521, 4);
            this.tboxAltitudeValue.Margin = new System.Windows.Forms.Padding(4);
            this.tboxAltitudeValue.Name = "tboxAltitudeValue";
            this.tboxAltitudeValue.Size = new System.Drawing.Size(105, 25);
            this.tboxAltitudeValue.TabIndex = 12;
            this.tboxAltitudeValue.Text = "10";
            // 
            // lblAltitude
            // 
            this.lblAltitude.AutoSize = true;
            this.lblAltitude.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAltitude.Location = new System.Drawing.Point(428, 6);
            this.lblAltitude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAltitude.Name = "lblAltitude";
            this.lblAltitude.Size = new System.Drawing.Size(79, 24);
            this.lblAltitude.TabIndex = 11;
            this.lblAltitude.Text = "Altitude";
            // 
            // tboxHomeAltitude
            // 
            this.tboxHomeAltitude.Location = new System.Drawing.Point(313, 4);
            this.tboxHomeAltitude.Margin = new System.Windows.Forms.Padding(4);
            this.tboxHomeAltitude.Name = "tboxHomeAltitude";
            this.tboxHomeAltitude.Size = new System.Drawing.Size(105, 25);
            this.tboxHomeAltitude.TabIndex = 10;
            // 
            // tboxHomeLongitude
            // 
            this.tboxHomeLongitude.Location = new System.Drawing.Point(199, 4);
            this.tboxHomeLongitude.Margin = new System.Windows.Forms.Padding(4);
            this.tboxHomeLongitude.Name = "tboxHomeLongitude";
            this.tboxHomeLongitude.Size = new System.Drawing.Size(105, 25);
            this.tboxHomeLongitude.TabIndex = 9;
            // 
            // tboxHomeLatitude
            // 
            this.tboxHomeLatitude.Location = new System.Drawing.Point(84, 4);
            this.tboxHomeLatitude.Margin = new System.Windows.Forms.Padding(4);
            this.tboxHomeLatitude.Name = "tboxHomeLatitude";
            this.tboxHomeLatitude.Size = new System.Drawing.Size(105, 25);
            this.tboxHomeLatitude.TabIndex = 8;
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHome.Location = new System.Drawing.Point(4, 6);
            this.lblHome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(63, 24);
            this.lblHome.TabIndex = 7;
            this.lblHome.Text = "Home";
            // 
            // gboxManualButtons
            // 
            this.gboxManualButtons.Controls.Add(this.btnAddwp);
            this.gboxManualButtons.Controls.Add(this.btnVideo);
            this.gboxManualButtons.Controls.Add(this.btnReadWPs);
            this.gboxManualButtons.Controls.Add(this.btnWriteWPs);
            this.gboxManualButtons.Controls.Add(this.btnAuto);
            this.gboxManualButtons.Controls.Add(this.btnTakeOff);
            this.gboxManualButtons.Controls.Add(this.btnLand);
            this.gboxManualButtons.Controls.Add(this.btnArm);
            this.gboxManualButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.gboxManualButtons.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gboxManualButtons.Location = new System.Drawing.Point(885, 0);
            this.gboxManualButtons.Margin = new System.Windows.Forms.Padding(4);
            this.gboxManualButtons.Name = "gboxManualButtons";
            this.gboxManualButtons.Padding = new System.Windows.Forms.Padding(4);
            this.gboxManualButtons.Size = new System.Drawing.Size(459, 120);
            this.gboxManualButtons.TabIndex = 19;
            this.gboxManualButtons.TabStop = false;
            this.gboxManualButtons.Text = "Manual";
            // 
            // btnAddwp
            // 
            this.btnAddwp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddwp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddwp.ForeColor = System.Drawing.Color.White;
            this.btnAddwp.Image = global::Diva.Properties.Resources.if_camera_1055100;
            this.btnAddwp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddwp.Location = new System.Drawing.Point(5, 69);
            this.btnAddwp.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddwp.Name = "btnAddwp";
            this.btnAddwp.Size = new System.Drawing.Size(105, 49);
            this.btnAddwp.TabIndex = 6;
            this.btnAddwp.Text = "Add";
            this.btnAddwp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddwp.UseVisualStyleBackColor = false;
            // 
            // btnVideo
            // 
            this.btnVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVideo.ForeColor = System.Drawing.Color.White;
            this.btnVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVideo.Location = new System.Drawing.Point(8, 15);
            this.btnVideo.Margin = new System.Windows.Forms.Padding(5);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(105, 49);
            this.btnVideo.TabIndex = 7;
            this.btnVideo.Text = "Video";
            this.btnVideo.UseVisualStyleBackColor = false;
            this.btnVideo.Click += new System.EventHandler(this.VideoDemo_Click);
            // 
            // btnReadWPs
            // 
            this.btnReadWPs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnReadWPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadWPs.ForeColor = System.Drawing.Color.White;
            this.btnReadWPs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReadWPs.Location = new System.Drawing.Point(119, 69);
            this.btnReadWPs.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadWPs.Name = "btnReadWPs";
            this.btnReadWPs.Size = new System.Drawing.Size(105, 49);
            this.btnReadWPs.TabIndex = 5;
            this.btnReadWPs.Text = "ReadWPs";
            this.btnReadWPs.UseVisualStyleBackColor = false;
            this.btnReadWPs.Click += new System.EventHandler(this.BUT_read_Click);
            // 
            // btnWriteWPs
            // 
            this.btnWriteWPs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnWriteWPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWriteWPs.ForeColor = System.Drawing.Color.White;
            this.btnWriteWPs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWriteWPs.Location = new System.Drawing.Point(119, 15);
            this.btnWriteWPs.Margin = new System.Windows.Forms.Padding(4);
            this.btnWriteWPs.Name = "btnWriteWPs";
            this.btnWriteWPs.Size = new System.Drawing.Size(105, 49);
            this.btnWriteWPs.TabIndex = 4;
            this.btnWriteWPs.Text = "WriteWPs";
            this.btnWriteWPs.UseVisualStyleBackColor = false;
            this.btnWriteWPs.Click += new System.EventHandler(this.BUT_write_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuto.ForeColor = System.Drawing.Color.White;
            this.btnAuto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAuto.Location = new System.Drawing.Point(232, 69);
            this.btnAuto.Margin = new System.Windows.Forms.Padding(4);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(105, 49);
            this.btnAuto.TabIndex = 3;
            this.btnAuto.Text = "AUTO";
            this.btnAuto.UseVisualStyleBackColor = false;
            this.btnAuto.Click += new System.EventHandler(this.BUT_Auto_Click);
            // 
            // btnTakeOff
            // 
            this.btnTakeOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTakeOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTakeOff.ForeColor = System.Drawing.Color.White;
            this.btnTakeOff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTakeOff.Location = new System.Drawing.Point(232, 15);
            this.btnTakeOff.Margin = new System.Windows.Forms.Padding(4);
            this.btnTakeOff.Name = "btnTakeOff";
            this.btnTakeOff.Size = new System.Drawing.Size(105, 49);
            this.btnTakeOff.TabIndex = 2;
            this.btnTakeOff.Text = "TakeOFF";
            this.btnTakeOff.UseVisualStyleBackColor = false;
            this.btnTakeOff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
            // 
            // btnLand
            // 
            this.btnLand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLand.ForeColor = System.Drawing.Color.White;
            this.btnLand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLand.Location = new System.Drawing.Point(345, 69);
            this.btnLand.Margin = new System.Windows.Forms.Padding(4);
            this.btnLand.Name = "btnLand";
            this.btnLand.Size = new System.Drawing.Size(105, 49);
            this.btnLand.TabIndex = 1;
            this.btnLand.Text = "Land";
            this.btnLand.UseVisualStyleBackColor = false;
            this.btnLand.Click += new System.EventHandler(this.BUT_Land_Click);
            // 
            // btnArm
            // 
            this.btnArm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnArm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArm.ForeColor = System.Drawing.Color.White;
            this.btnArm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnArm.Location = new System.Drawing.Point(345, 15);
            this.btnArm.Margin = new System.Windows.Forms.Padding(4);
            this.btnArm.Name = "btnArm";
            this.btnArm.Size = new System.Drawing.Size(105, 49);
            this.btnArm.TabIndex = 0;
            this.btnArm.Text = "Arm";
            this.btnArm.UseVisualStyleBackColor = false;
            this.btnArm.Click += new System.EventHandler(this.BUT_Arm_Click);
            // 
            // dgvWayPoints
            // 
            this.dgvWayPoints.AllowUserToAddRows = false;
            this.dgvWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
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
            this.dgvWayPoints.Location = new System.Drawing.Point(0, 32);
            this.dgvWayPoints.Margin = new System.Windows.Forms.Padding(5);
            this.dgvWayPoints.Name = "dgvWayPoints";
            this.dgvWayPoints.RowHeadersWidth = 50;
            this.dgvWayPoints.RowTemplate.Height = 24;
            this.dgvWayPoints.Size = new System.Drawing.Size(885, 88);
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
            this.colCommand.Width = 71;
            // 
            // colParam1
            // 
            this.colParam1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colParam1.HeaderText = "Param1";
            this.colParam1.MinimumWidth = 50;
            this.colParam1.Name = "colParam1";
            this.colParam1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colParam1.Width = 56;
            // 
            // colParam2
            // 
            this.colParam2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colParam2.HeaderText = "Param2";
            this.colParam2.MinimumWidth = 50;
            this.colParam2.Name = "colParam2";
            this.colParam2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colParam2.Width = 56;
            // 
            // colParam3
            // 
            this.colParam3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colParam3.HeaderText = "Param3";
            this.colParam3.MinimumWidth = 50;
            this.colParam3.Name = "colParam3";
            this.colParam3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colParam3.Width = 56;
            // 
            // colParam4
            // 
            this.colParam4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colParam4.HeaderText = "Param4";
            this.colParam4.MinimumWidth = 50;
            this.colParam4.Name = "colParam4";
            this.colParam4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colParam4.Width = 56;
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
            this.colLongitude.Width = 71;
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
            this.colTagData.Width = 61;
            // 
            // gaugeAltitude
            // 
            this.gaugeAltitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.gaugeAltitude.BackColor = System.Drawing.SystemColors.Menu;
            this.gaugeAltitude.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeAltitude.BaseArcRadius = 50;
            this.gaugeAltitude.BaseArcStart = 135;
            this.gaugeAltitude.BaseArcSweep = 270;
            this.gaugeAltitude.BaseArcWidth = 2;
            this.gaugeAltitude.Center = new System.Drawing.Point(100, 100);
            this.gaugeAltitude.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeAltitude.Location = new System.Drawing.Point(0, 111);
            this.gaugeAltitude.Margin = new System.Windows.Forms.Padding(5);
            this.gaugeAltitude.MaxValue = 100F;
            this.gaugeAltitude.MinValue = 0F;
            this.gaugeAltitude.Name = "gaugeAltitude";
            this.gaugeAltitude.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.gaugeAltitude.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeAltitude.NeedleRadius = 30;
            this.gaugeAltitude.NeedleType = System.Windows.Forms.NeedleType.Advance;
            this.gaugeAltitude.NeedleWidth = 2;
            this.gaugeAltitude.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeAltitude.ScaleLinesInterInnerRadius = 45;
            this.gaugeAltitude.ScaleLinesInterOuterRadius = 50;
            this.gaugeAltitude.ScaleLinesInterWidth = 1;
            this.gaugeAltitude.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeAltitude.ScaleLinesMajorInnerRadius = 40;
            this.gaugeAltitude.ScaleLinesMajorOuterRadius = 50;
            this.gaugeAltitude.ScaleLinesMajorStepValue = 10F;
            this.gaugeAltitude.ScaleLinesMajorWidth = 2;
            this.gaugeAltitude.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gaugeAltitude.ScaleLinesMinorInnerRadius = 45;
            this.gaugeAltitude.ScaleLinesMinorOuterRadius = 50;
            this.gaugeAltitude.ScaleLinesMinorTicks = 9;
            this.gaugeAltitude.ScaleLinesMinorWidth = 1;
            this.gaugeAltitude.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gaugeAltitude.ScaleNumbersFormat = null;
            this.gaugeAltitude.ScaleNumbersRadius = 60;
            this.gaugeAltitude.ScaleNumbersRotation = 0;
            this.gaugeAltitude.ScaleNumbersStartScaleLine = 0;
            this.gaugeAltitude.ScaleNumbersStepScaleLines = 1;
            this.gaugeAltitude.Size = new System.Drawing.Size(271, 230);
            this.gaugeAltitude.TabIndex = 2;
            this.gaugeAltitude.Text = "aGauge1";
            this.gaugeAltitude.Value = 0F;
            // 
            // gaugeSpeed
            // 
            this.gaugeSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.gaugeSpeed.BackColor = System.Drawing.SystemColors.Menu;
            this.gaugeSpeed.BaseArcColor = System.Drawing.Color.Gray;
            this.gaugeSpeed.BaseArcRadius = 50;
            this.gaugeSpeed.BaseArcStart = 135;
            this.gaugeSpeed.BaseArcSweep = 270;
            this.gaugeSpeed.BaseArcWidth = 2;
            this.gaugeSpeed.Center = new System.Drawing.Point(100, 100);
            this.gaugeSpeed.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gaugeSpeed.Location = new System.Drawing.Point(0, 341);
            this.gaugeSpeed.Margin = new System.Windows.Forms.Padding(5);
            this.gaugeSpeed.MaxValue = 30F;
            this.gaugeSpeed.MinValue = 0F;
            this.gaugeSpeed.Name = "gaugeSpeed";
            this.gaugeSpeed.NeedleColor1 = System.Windows.Forms.AGaugeNeedleColor.Gray;
            this.gaugeSpeed.NeedleColor2 = System.Drawing.Color.DimGray;
            this.gaugeSpeed.NeedleRadius = 30;
            this.gaugeSpeed.NeedleType = System.Windows.Forms.NeedleType.Advance;
            this.gaugeSpeed.NeedleWidth = 2;
            this.gaugeSpeed.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.gaugeSpeed.ScaleLinesInterInnerRadius = 45;
            this.gaugeSpeed.ScaleLinesInterOuterRadius = 50;
            this.gaugeSpeed.ScaleLinesInterWidth = 1;
            this.gaugeSpeed.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.gaugeSpeed.ScaleLinesMajorInnerRadius = 40;
            this.gaugeSpeed.ScaleLinesMajorOuterRadius = 50;
            this.gaugeSpeed.ScaleLinesMajorStepValue = 5F;
            this.gaugeSpeed.ScaleLinesMajorWidth = 2;
            this.gaugeSpeed.ScaleLinesMinorColor = System.Drawing.Color.Gray;
            this.gaugeSpeed.ScaleLinesMinorInnerRadius = 45;
            this.gaugeSpeed.ScaleLinesMinorOuterRadius = 50;
            this.gaugeSpeed.ScaleLinesMinorTicks = 9;
            this.gaugeSpeed.ScaleLinesMinorWidth = 1;
            this.gaugeSpeed.ScaleNumbersColor = System.Drawing.Color.Black;
            this.gaugeSpeed.ScaleNumbersFormat = null;
            this.gaugeSpeed.ScaleNumbersRadius = 60;
            this.gaugeSpeed.ScaleNumbersRotation = 0;
            this.gaugeSpeed.ScaleNumbersStartScaleLine = 0;
            this.gaugeSpeed.ScaleNumbersStepScaleLines = 1;
            this.gaugeSpeed.Size = new System.Drawing.Size(271, 230);
            this.gaugeSpeed.TabIndex = 3;
            this.gaugeSpeed.Text = "aGauge1";
            this.gaugeSpeed.Value = 0F;
            // 
            // lblGaugeAltitude
            // 
            this.lblGaugeAltitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGaugeAltitude.AutoSize = true;
            this.lblGaugeAltitude.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGaugeAltitude.Location = new System.Drawing.Point(90, 119);
            this.lblGaugeAltitude.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblGaugeAltitude.Name = "lblGaugeAltitude";
            this.lblGaugeAltitude.Size = new System.Drawing.Size(82, 24);
            this.lblGaugeAltitude.TabIndex = 4;
            this.lblGaugeAltitude.Text = "Altitude";
            // 
            // labelGaugeSpeed
            // 
            this.labelGaugeSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelGaugeSpeed.AutoSize = true;
            this.labelGaugeSpeed.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGaugeSpeed.Location = new System.Drawing.Point(90, 339);
            this.labelGaugeSpeed.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelGaugeSpeed.Name = "labelGaugeSpeed";
            this.labelGaugeSpeed.Size = new System.Drawing.Size(69, 24);
            this.labelGaugeSpeed.TabIndex = 5;
            this.labelGaugeSpeed.Text = "Speed";
            // 
            // lblGagueAltitudeValue
            // 
            this.lblGagueAltitudeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGagueAltitudeValue.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGagueAltitudeValue.Location = new System.Drawing.Point(85, 295);
            this.lblGagueAltitudeValue.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblGagueAltitudeValue.Name = "lblGagueAltitudeValue";
            this.lblGagueAltitudeValue.Size = new System.Drawing.Size(67, 25);
            this.lblGagueAltitudeValue.TabIndex = 6;
            this.lblGagueAltitudeValue.Text = "00";
            this.lblGagueAltitudeValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGagueSpeedValue
            // 
            this.lblGagueSpeedValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGagueSpeedValue.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGagueSpeedValue.Location = new System.Drawing.Point(79, 524);
            this.lblGagueSpeedValue.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblGagueSpeedValue.Name = "lblGagueSpeedValue";
            this.lblGagueSpeedValue.Size = new System.Drawing.Size(67, 25);
            this.lblGagueSpeedValue.TabIndex = 7;
            this.lblGagueSpeedValue.Text = "00";
            this.lblGagueSpeedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGaugeAltitudeUnit
            // 
            this.lblGaugeAltitudeUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGaugeAltitudeUnit.AutoSize = true;
            this.lblGaugeAltitudeUnit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGaugeAltitudeUnit.Location = new System.Drawing.Point(160, 295);
            this.lblGaugeAltitudeUnit.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblGaugeAltitudeUnit.Name = "lblGaugeAltitudeUnit";
            this.lblGaugeAltitudeUnit.Size = new System.Drawing.Size(27, 24);
            this.lblGaugeAltitudeUnit.TabIndex = 8;
            this.lblGaugeAltitudeUnit.Text = "m";
            // 
            // lblGagueSpeedUnit
            // 
            this.lblGagueSpeedUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGagueSpeedUnit.AutoSize = true;
            this.lblGagueSpeedUnit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGagueSpeedUnit.Location = new System.Drawing.Point(148, 525);
            this.lblGagueSpeedUnit.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblGagueSpeedUnit.Name = "lblGagueSpeedUnit";
            this.lblGagueSpeedUnit.Size = new System.Drawing.Size(38, 24);
            this.lblGagueSpeedUnit.TabIndex = 9;
            this.lblGagueSpeedUnit.Text = "ms";
            // 
            // tsDroneStatus
            // 
            this.tsDroneStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tsDroneStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDroneStatus.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDroneStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsDroneStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnGPSSignalIcon,
            this.tslblGPS,
            this.tsSeparator1,
            this.tsbtnBatteryHealthIcon,
            this.tslblBattery});
            this.tsDroneStatus.Location = new System.Drawing.Point(1194, 0);
            this.tsDroneStatus.Name = "tsDroneStatus";
            this.tsDroneStatus.Size = new System.Drawing.Size(138, 27);
            this.tsDroneStatus.TabIndex = 10;
            this.tsDroneStatus.Text = "Drone Status";
            // 
            // tsbtnGPSSignalIcon
            // 
            this.tsbtnGPSSignalIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnGPSSignalIcon.Image = global::Diva.Properties.Resources.if_50_111142;
            this.tsbtnGPSSignalIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnGPSSignalIcon.Name = "tsbtnGPSSignalIcon";
            this.tsbtnGPSSignalIcon.Size = new System.Drawing.Size(24, 24);
            this.tsbtnGPSSignalIcon.Text = "GPS";
            this.tsbtnGPSSignalIcon.ToolTipText = "GPS Signal Strength";
            // 
            // tslblGPS
            // 
            this.tslblGPS.Name = "tslblGPS";
            this.tslblGPS.Size = new System.Drawing.Size(55, 24);
            this.tslblGPS.Text = "strong";
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbtnBatteryHealthIcon
            // 
            this.tsbtnBatteryHealthIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnBatteryHealthIcon.Image = global::Diva.Properties.Resources.if_battery_reduce_energy_charge_2203543;
            this.tsbtnBatteryHealthIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnBatteryHealthIcon.Name = "tsbtnBatteryHealthIcon";
            this.tsbtnBatteryHealthIcon.Size = new System.Drawing.Size(24, 24);
            this.tsbtnBatteryHealthIcon.Text = "Battery";
            this.tsbtnBatteryHealthIcon.ToolTipText = "Battery Health";
            // 
            // tslblBattery
            // 
            this.tslblBattery.Name = "tslblBattery";
            this.tslblBattery.Size = new System.Drawing.Size(26, 24);
            this.tslblBattery.Text = "0v";
            // 
            // tsDroneList
            // 
            this.tsDroneList.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tsDroneList.AutoSize = false;
            this.tsDroneList.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDroneList.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDroneList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsDroneList.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsDroneList.Location = new System.Drawing.Point(1257, 259);
            this.tsDroneList.Name = "tsDroneList";
            this.tsDroneList.Padding = new System.Windows.Forms.Padding(1);
            this.tsDroneList.Size = new System.Drawing.Size(75, 235);
            this.tsDroneList.TabIndex = 12;
            this.tsDroneList.Text = "Drone List";
            // 
            // timerMapItemUpdate
            // 
            this.timerMapItemUpdate.Interval = 1200;
            this.timerMapItemUpdate.Tick += new System.EventHandler(this.timerMapItemUpdate_Tick);
            // 
            // tsMainFunctions
            // 
            this.tsMainFunctions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tsMainFunctions.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMainFunctions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMainFunctions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsMainFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnConnect,
            this.tsSeparator2,
            this.tsbtnRotation,
            this.tsSeparator3,
            this.tsbtnConfigure,
            this.tsSeparator4,
            this.tsbtnTagging,
            this.tsSeparator5,
            this.tsbtnDeplex});
            this.tsMainFunctions.Location = new System.Drawing.Point(509, 11);
            this.tsMainFunctions.Name = "tsMainFunctions";
            this.tsMainFunctions.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tsMainFunctions.Size = new System.Drawing.Size(332, 58);
            this.tsMainFunctions.TabIndex = 13;
            this.tsMainFunctions.Text = "toolStrip2";
            // 
            // tsbtnConnect
            // 
            this.tsbtnConnect.AutoSize = false;
            this.tsbtnConnect.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnConnect.Image = global::Diva.Properties.Resources.if_paper_plane_32;
            this.tsbtnConnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnConnect.Name = "tsbtnConnect";
            this.tsbtnConnect.Size = new System.Drawing.Size(60, 51);
            this.tsbtnConnect.Text = "Connect";
            this.tsbtnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnConnect.Click += new System.EventHandler(this.BUT_Connect_Click);
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnRotation
            // 
            this.tsbtnRotation.AutoSize = false;
            this.tsbtnRotation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnRotation.Image = global::Diva.Properties.Resources.if_rotation_32;
            this.tsbtnRotation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRotation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRotation.Name = "tsbtnRotation";
            this.tsbtnRotation.Size = new System.Drawing.Size(60, 51);
            this.tsbtnRotation.Text = "Rotation";
            this.tsbtnRotation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRotation.Click += new System.EventHandler(this.BUT_Rotation_Click);
            // 
            // tsSeparator3
            // 
            this.tsSeparator3.Name = "tsSeparator3";
            this.tsSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnConfigure
            // 
            this.tsbtnConfigure.AutoSize = false;
            this.tsbtnConfigure.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnConfigure.Image = global::Diva.Properties.Resources.if_settings_32;
            this.tsbtnConfigure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnConfigure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnConfigure.Name = "tsbtnConfigure";
            this.tsbtnConfigure.Size = new System.Drawing.Size(60, 51);
            this.tsbtnConfigure.Text = "Configure";
            this.tsbtnConfigure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnConfigure.Click += new System.EventHandler(this.BUT_Configure_Click);
            // 
            // tsSeparator4
            // 
            this.tsSeparator4.Name = "tsSeparator4";
            this.tsSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnTagging
            // 
            this.tsbtnTagging.AutoSize = false;
            this.tsbtnTagging.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tsbtnTagging.Image = global::Diva.Properties.Resources.if_price_tag_2639892;
            this.tsbtnTagging.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnTagging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTagging.Name = "tsbtnTagging";
            this.tsbtnTagging.Size = new System.Drawing.Size(60, 51);
            this.tsbtnTagging.Text = "Tagging";
            this.tsbtnTagging.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnTagging.Click += new System.EventHandler(this.BUT_Tagging_Click);
            // 
            // tsSeparator5
            // 
            this.tsSeparator5.Name = "tsSeparator5";
            this.tsSeparator5.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnDeplex
            // 
            this.tsbtnDeplex.AutoSize = false;
            this.tsbtnDeplex.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnDeplex.Image = global::Diva.Properties.Resources.if_code_fork_1608638;
            this.tsbtnDeplex.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnDeplex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeplex.Name = "tsbtnDeplex";
            this.tsbtnDeplex.Size = new System.Drawing.Size(60, 51);
            this.tsbtnDeplex.Text = "Deplex";
            this.tsbtnDeplex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnDeplex.Click += new System.EventHandler(this.BUT_Deplex_Click);
            // 
            // myMap
            // 
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
            this.myMap.Margin = new System.Windows.Forms.Padding(4);
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
            this.myMap.Size = new System.Drawing.Size(1344, 629);
            this.myMap.TabIndex = 14;
            this.myMap.Zoom = 0D;
            this.myMap.Load += new System.EventHandler(this.myMap_Load);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lblGaugeAltitude);
            this.splitContainer.Panel1.Controls.Add(this.lblGagueAltitudeValue);
            this.splitContainer.Panel1.Controls.Add(this.lblGaugeAltitudeUnit);
            this.splitContainer.Panel1.Controls.Add(this.gaugeAltitude);
            this.splitContainer.Panel1.Controls.Add(this.labelGaugeSpeed);
            this.splitContainer.Panel1.Controls.Add(this.lblGagueSpeedValue);
            this.splitContainer.Panel1.Controls.Add(this.lblGagueSpeedUnit);
            this.splitContainer.Panel1.Controls.Add(this.gaugeSpeed);
            this.splitContainer.Panel1.Controls.Add(this.tsDroneList);
            this.splitContainer.Panel1.Controls.Add(this.tsDroneStatus);
            this.splitContainer.Panel1.Controls.Add(this.tsMainFunctions);
            this.splitContainer.Panel1.Controls.Add(this.myMap);
            this.splitContainer.Panel1MinSize = 320;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvWayPoints);
            this.splitContainer.Panel2.Controls.Add(this.panelDroneInfo);
            this.splitContainer.Panel2.Controls.Add(this.gboxManualButtons);
            this.splitContainer.Panel2MinSize = 110;
            this.splitContainer.Size = new System.Drawing.Size(1344, 753);
            this.splitContainer.SplitterDistance = 629;
            this.splitContainer.TabIndex = 15;
            // 
            // Planner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 753);
            this.Controls.Add(this.splitContainer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1280, 800);
            this.Name = "Planner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Planner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Planner_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Planner_FormClosed);
            this.Load += new System.EventHandler(this.Planner_Load);
            this.cmMap.ResumeLayout(false);
            this.panelDroneInfo.ResumeLayout(false);
            this.panelDroneInfo.PerformLayout();
            this.gboxManualButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWayPoints)).EndInit();
            this.tsDroneStatus.ResumeLayout(false);
            this.tsDroneStatus.PerformLayout();
            this.tsMainFunctions.ResumeLayout(false);
            this.tsMainFunctions.PerformLayout();
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
		private System.Windows.Forms.Label lblHome;
		private System.Windows.Forms.TextBox tboxAltitudeValue;
		private System.Windows.Forms.Label lblAltitude;
		private System.Windows.Forms.TextBox tboxDroneMode;
		private System.Windows.Forms.Label lblMode;
		private System.Windows.Forms.ContextMenuStrip cmMap;
		private System.Windows.Forms.ToolStripMenuItem miClearMission;
		private System.Windows.Forms.ToolStripMenuItem miSetHomeHere;
		private System.Windows.Forms.AGauge gaugeAltitude;
		private System.Windows.Forms.AGauge gaugeSpeed;
		private System.Windows.Forms.Label lblGaugeAltitude;
		private System.Windows.Forms.Label labelGaugeSpeed;
		private System.Windows.Forms.Label lblGagueAltitudeValue;
		private System.Windows.Forms.Label lblGagueSpeedValue;
		private System.Windows.Forms.Label lblGaugeAltitudeUnit;
		private System.Windows.Forms.Label lblGagueSpeedUnit;
		private System.Windows.Forms.ToolStripMenuItem miClearAllMissions;
		private System.Windows.Forms.ToolStrip tsDroneStatus;
		private System.Windows.Forms.ToolStripButton tsbtnGPSSignalIcon;
		private System.Windows.Forms.ToolStripLabel tslblGPS;
		private System.Windows.Forms.ToolStripSeparator tsSeparator1;
		private System.Windows.Forms.ToolStripButton tsbtnBatteryHealthIcon;
		private System.Windows.Forms.ToolStripLabel tslblBattery;
		private System.Windows.Forms.ToolStrip tsDroneList;
		private System.Windows.Forms.Timer timerMapItemUpdate;
		private System.Windows.Forms.ToolStrip tsMainFunctions;
		private System.Windows.Forms.ToolStripButton tsbtnConnect;
		private System.Windows.Forms.ToolStripButton tsbtnRotation;
		private System.Windows.Forms.ToolStripButton tsbtnConfigure;
		private System.Windows.Forms.ToolStripSeparator tsSeparator2;
		private System.Windows.Forms.ToolStripSeparator tsSeparator3;
		private GroupBox gboxManualButtons;
		private MyGMap myMap;
		private ToolStripButton tsbtnDeplex;
		private ToolStripSeparator tsSeparator4;
		private Button btnArm;
		private Button btnAddwp;
		private Button btnReadWPs;
		private Button btnWriteWPs;
		private Button btnAuto;
		private Button btnTakeOff;
		private Button btnLand;
		private ToolStripButton tsbtnTagging;
		private ToolStripSeparator tsSeparator5;
        private Button btnVideo;
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
    }
}