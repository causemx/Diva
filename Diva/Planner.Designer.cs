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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Planner));
			this.cmMap = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miClearMission = new System.Windows.Forms.ToolStripMenuItem();
			this.miSetHomeHere = new System.Windows.Forms.ToolStripMenuItem();
			this.miClearAllMissions = new System.Windows.Forms.ToolStripMenuItem();
			this.panelDroneInfo = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.TxtDroneMode = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.BtnAltitude = new System.Windows.Forms.Button();
			this.BtnHomeLand = new System.Windows.Forms.Button();
			this.TxtAltitudeValue = new System.Windows.Forms.TextBox();
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
			this.CollectionTelemetryData = new Diva.Controls.TelemetryDataPanel();
			this.DroneInfo3 = new Diva.Controls.DroneInfoPanel();
			this.DroneInfo2 = new Diva.Controls.DroneInfoPanel();
			this.DroneInfo1 = new Diva.Controls.DroneInfoPanel();
			this.BtnRTL = new System.Windows.Forms.Button();
			this.BtnLand = new System.Windows.Forms.Button();
			this.BtnVideo = new System.Windows.Forms.Button();
			this.BtnAuto = new System.Windows.Forms.Button();
			this.BtnArm = new System.Windows.Forms.Button();
			this.BtnReadWPs = new System.Windows.Forms.Button();
			this.BtnTakeOff = new System.Windows.Forms.Button();
			this.BtnWriteWPs = new System.Windows.Forms.Button();
			this.myMap = new Diva.Controls.MyGMap();
			this.ImgListBatteryHealth = new System.Windows.Forms.ImageList(this.components);
			this.TxtHomeAltitude = new System.Windows.Forms.TextBox();
			this.TxtHomeLatitude = new System.Windows.Forms.TextBox();
			this.TxtHomeLongitude = new System.Windows.Forms.TextBox();
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
			this.panelDroneInfo.Controls.Add(this.TxtHomeLongitude);
			this.panelDroneInfo.Controls.Add(this.TxtHomeLatitude);
			this.panelDroneInfo.Controls.Add(this.TxtHomeAltitude);
			this.panelDroneInfo.Controls.Add(this.label3);
			this.panelDroneInfo.Controls.Add(this.label2);
			this.panelDroneInfo.Controls.Add(this.label1);
			this.panelDroneInfo.Controls.Add(this.TxtDroneMode);
			this.panelDroneInfo.Controls.Add(this.button1);
			this.panelDroneInfo.Controls.Add(this.BtnAltitude);
			this.panelDroneInfo.Controls.Add(this.BtnHomeLand);
			this.panelDroneInfo.Controls.Add(this.TxtAltitudeValue);
			this.panelDroneInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelDroneInfo.Location = new System.Drawing.Point(0, 0);
			this.panelDroneInfo.Name = "panelDroneInfo";
			this.panelDroneInfo.Size = new System.Drawing.Size(948, 51);
			this.panelDroneInfo.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(347, 18);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 15);
			this.label3.TabIndex = 26;
			this.label3.Text = "lng:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(245, 18);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(27, 15);
			this.label2.TabIndex = 25;
			this.label2.Text = "lat:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(146, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 15);
			this.label1.TabIndex = 24;
			this.label1.Text = "alt:";
			// 
			// TxtDroneMode
			// 
			this.TxtDroneMode.AutoSize = true;
			this.TxtDroneMode.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtDroneMode.ForeColor = System.Drawing.Color.White;
			this.TxtDroneMode.Location = new System.Drawing.Point(864, 18);
			this.TxtDroneMode.Name = "TxtDroneMode";
			this.TxtDroneMode.Size = new System.Drawing.Size(32, 15);
			this.TxtDroneMode.TabIndex = 23;
			this.TxtDroneMode.Text = "N/A";
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Image = global::Diva.Properties.Resources.icon_airplane_32;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(728, 1);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(130, 49);
			this.button1.TabIndex = 22;
			this.button1.Text = "Drone Mode";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button1.UseVisualStyleBackColor = false;
			// 
			// BtnAltitude
			// 
			this.BtnAltitude.AutoSize = true;
			this.BtnAltitude.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnAltitude.FlatAppearance.BorderSize = 0;
			this.BtnAltitude.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnAltitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnAltitude.ForeColor = System.Drawing.Color.White;
			this.BtnAltitude.Image = global::Diva.Properties.Resources.icon_edit_32;
			this.BtnAltitude.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnAltitude.Location = new System.Drawing.Point(472, 1);
			this.BtnAltitude.Name = "BtnAltitude";
			this.BtnAltitude.Size = new System.Drawing.Size(130, 49);
			this.BtnAltitude.TabIndex = 21;
			this.BtnAltitude.Text = "Write alt.";
			this.BtnAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnAltitude.UseVisualStyleBackColor = false;
			// 
			// BtnHomeLand
			// 
			this.BtnHomeLand.AutoSize = true;
			this.BtnHomeLand.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnHomeLand.FlatAppearance.BorderSize = 0;
			this.BtnHomeLand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnHomeLand.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnHomeLand.ForeColor = System.Drawing.Color.White;
			this.BtnHomeLand.Image = global::Diva.Properties.Resources.icon_house_32;
			this.BtnHomeLand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnHomeLand.Location = new System.Drawing.Point(0, 0);
			this.BtnHomeLand.Name = "BtnHomeLand";
			this.BtnHomeLand.Size = new System.Drawing.Size(130, 49);
			this.BtnHomeLand.TabIndex = 16;
			this.BtnHomeLand.Text = "Homeland";
			this.BtnHomeLand.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnHomeLand.UseVisualStyleBackColor = false;
			// 
			// TxtAltitudeValue
			// 
			this.TxtAltitudeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtAltitudeValue.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtAltitudeValue.Location = new System.Drawing.Point(608, 6);
			this.TxtAltitudeValue.Multiline = true;
			this.TxtAltitudeValue.Name = "TxtAltitudeValue";
			this.TxtAltitudeValue.Size = new System.Drawing.Size(62, 41);
			this.TxtAltitudeValue.TabIndex = 12;
			this.TxtAltitudeValue.Text = "30";
			this.TxtAltitudeValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// dgvWayPoints
			// 
			this.dgvWayPoints.AllowUserToAddRows = false;
			this.dgvWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgvWayPoints.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.dgvWayPoints.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.dgvWayPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgvWayPoints.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgvWayPoints.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvWayPoints.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvWayPoints.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvWayPoints.EnableHeadersVisualStyles = false;
			this.dgvWayPoints.GridColor = System.Drawing.SystemColors.InactiveCaption;
			this.dgvWayPoints.Location = new System.Drawing.Point(0, 51);
			this.dgvWayPoints.Margin = new System.Windows.Forms.Padding(4);
			this.dgvWayPoints.Name = "dgvWayPoints";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvWayPoints.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvWayPoints.RowHeadersWidth = 50;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.InfoText;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
			this.dgvWayPoints.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvWayPoints.RowTemplate.Height = 24;
			this.dgvWayPoints.Size = new System.Drawing.Size(948, 80);
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
			this.colParam1.Width = 57;
			// 
			// colParam2
			// 
			this.colParam2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colParam2.HeaderText = "Param2";
			this.colParam2.MinimumWidth = 50;
			this.colParam2.Name = "colParam2";
			this.colParam2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colParam2.Width = 58;
			// 
			// colParam3
			// 
			this.colParam3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colParam3.HeaderText = "Param3";
			this.colParam3.MinimumWidth = 50;
			this.colParam3.Name = "colParam3";
			this.colParam3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colParam3.Width = 58;
			// 
			// colParam4
			// 
			this.colParam4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.colParam4.HeaderText = "Param4";
			this.colParam4.MinimumWidth = 50;
			this.colParam4.Name = "colParam4";
			this.colParam4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colParam4.Width = 58;
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
			this.colLongitude.Width = 69;
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
			this.colTagData.Width = 63;
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
			this.TSBtnRotation.Click += new System.EventHandler(this.BUT_Rotation2_Click);
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
			this.splitContainer.Panel1.Controls.Add(this.CollectionTelemetryData);
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
			this.splitContainer.Size = new System.Drawing.Size(948, 662);
			this.splitContainer.SplitterDistance = 528;
			this.splitContainer.SplitterWidth = 3;
			this.splitContainer.TabIndex = 15;
			// 
			// CollectionTelemetryData
			// 
			this.CollectionTelemetryData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CollectionTelemetryData.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.CollectionTelemetryData.Location = new System.Drawing.Point(595, 262);
			this.CollectionTelemetryData.Name = "CollectionTelemetryData";
			this.CollectionTelemetryData.Size = new System.Drawing.Size(341, 245);
			this.CollectionTelemetryData.TabIndex = 19;
			// 
			// DroneInfo3
			// 
			this.DroneInfo3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DroneInfo3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.DroneInfo3.IsActivate = false;
			this.DroneInfo3.Location = new System.Drawing.Point(592, 178);
			this.DroneInfo3.Name = "DroneInfo3";
			this.DroneInfo3.Size = new System.Drawing.Size(344, 78);
			this.DroneInfo3.TabIndex = 18;
			this.DroneInfo3.Tag = "2";
			this.DroneInfo3.DoubleClick += new System.EventHandler(this.DroneInfo_DoubleClick);
			// 
			// DroneInfo2
			// 
			this.DroneInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DroneInfo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.DroneInfo2.IsActivate = false;
			this.DroneInfo2.Location = new System.Drawing.Point(592, 94);
			this.DroneInfo2.Name = "DroneInfo2";
			this.DroneInfo2.Size = new System.Drawing.Size(344, 78);
			this.DroneInfo2.TabIndex = 17;
			this.DroneInfo2.Tag = "1";
			this.DroneInfo2.DoubleClick += new System.EventHandler(this.DroneInfo_DoubleClick);
			// 
			// DroneInfo1
			// 
			this.DroneInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DroneInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.DroneInfo1.IsActivate = false;
			this.DroneInfo1.Location = new System.Drawing.Point(592, 10);
			this.DroneInfo1.Name = "DroneInfo1";
			this.DroneInfo1.Size = new System.Drawing.Size(344, 78);
			this.DroneInfo1.TabIndex = 16;
			this.DroneInfo1.Tag = "0";
			this.DroneInfo1.DoubleClick += new System.EventHandler(this.DroneInfo_DoubleClick);
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
			this.BtnRTL.Location = new System.Drawing.Point(9, 405);
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
			this.BtnLand.Location = new System.Drawing.Point(9, 219);
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
			this.BtnVideo.Location = new System.Drawing.Point(105, 405);
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
			this.BtnAuto.Location = new System.Drawing.Point(105, 219);
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
			this.BtnArm.Location = new System.Drawing.Point(9, 126);
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
			this.BtnReadWPs.Location = new System.Drawing.Point(105, 312);
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
			this.BtnTakeOff.Location = new System.Drawing.Point(105, 126);
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
			this.BtnWriteWPs.Location = new System.Drawing.Point(9, 312);
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
			this.myMap.Size = new System.Drawing.Size(948, 528);
			this.myMap.TabIndex = 14;
			this.myMap.Zoom = 0D;
			this.myMap.Load += new System.EventHandler(this.myMap_Load);
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
			// TxtHomeAltitude
			// 
			this.TxtHomeAltitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtHomeAltitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtHomeAltitude.Location = new System.Drawing.Point(177, 6);
			this.TxtHomeAltitude.Multiline = true;
			this.TxtHomeAltitude.Name = "TxtHomeAltitude";
			this.TxtHomeAltitude.Size = new System.Drawing.Size(62, 41);
			this.TxtHomeAltitude.TabIndex = 30;
			this.TxtHomeAltitude.Text = "30";
			this.TxtHomeAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TxtHomeLatitude
			// 
			this.TxtHomeLatitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtHomeLatitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtHomeLatitude.Location = new System.Drawing.Point(279, 6);
			this.TxtHomeLatitude.Multiline = true;
			this.TxtHomeLatitude.Name = "TxtHomeLatitude";
			this.TxtHomeLatitude.Size = new System.Drawing.Size(62, 41);
			this.TxtHomeLatitude.TabIndex = 31;
			this.TxtHomeLatitude.Text = "30";
			this.TxtHomeLatitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TxtHomeLongitude
			// 
			this.TxtHomeLongitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.TxtHomeLongitude.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtHomeLongitude.Location = new System.Drawing.Point(383, 6);
			this.TxtHomeLongitude.Multiline = true;
			this.TxtHomeLongitude.Name = "TxtHomeLongitude";
			this.TxtHomeLongitude.Size = new System.Drawing.Size(62, 41);
			this.TxtHomeLongitude.TabIndex = 32;
			this.TxtHomeLongitude.Text = "30";
			this.TxtHomeLongitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// Planner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(948, 662);
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
		private System.Windows.Forms.TextBox TxtAltitudeValue;
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
		private ImageList ImgListBatteryHealth;
		private Button BtnRTL;
		private DroneInfoPanel DroneInfo1;
		private DroneInfoPanel DroneInfo2;
		private DroneInfoPanel DroneInfo3;
		private TelemetryDataPanel CollectionTelemetryData;
		private Button BtnAltitude;
		private Button button1;
		private Label TxtDroneMode;
		private Label label3;
		private Label label2;
		private Label label1;
		private TextBox TxtHomeAltitude;
		private TextBox TxtHomeLatitude;
		private TextBox TxtHomeLongitude;
	}
}