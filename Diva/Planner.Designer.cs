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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Planner));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.TSMainPanel = new System.Windows.Forms.ToolStrip();
            this.TSBtnConnect = new System.Windows.Forms.ToolStripButton();
            this.TSBtnRotation = new System.Windows.Forms.ToolStripButton();
            this.TSBtnConfigure = new System.Windows.Forms.ToolStripButton();
            this.TSBtnTagging = new System.Windows.Forms.ToolStripButton();
            this.BtnSaveMission = new System.Windows.Forms.ToolStripButton();
            this.BtnReadMission = new System.Windows.Forms.ToolStripButton();
            this.myMap = new Diva.Controls.MyGMap();
            this.cmMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miClearMission = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetHomeHere = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearAllMissions = new System.Windows.Forms.ToolStripMenuItem();
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
            this.panelDroneInfo = new System.Windows.Forms.Panel();
            this.TxtHomeLongitude = new System.Windows.Forms.TextBox();
            this.TxtHomeLatitude = new System.Windows.Forms.TextBox();
            this.TxtHomeAltitude = new System.Windows.Forms.TextBox();
            this.LabelLongitude = new System.Windows.Forms.Label();
            this.LabelLatitude = new System.Windows.Forms.Label();
            this.LabelAltitude = new System.Windows.Forms.Label();
            this.TxtDroneMode = new System.Windows.Forms.Label();
            this.BtnDroneMode = new System.Windows.Forms.Button();
            this.BtnAltitude = new System.Windows.Forms.Button();
            this.BtnHomeLand = new System.Windows.Forms.Button();
            this.TxtAltitudeValue = new System.Windows.Forms.TextBox();
            this.timerMapItemUpdate = new System.Windows.Forms.Timer(this.components);
            this.ImgListBatteryHealth = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.TSMainPanel.SuspendLayout();
            this.cmMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWayPoints)).BeginInit();
            this.panelDroneInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
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
            // 
            // splitContainer.Panel2
            // 
            resources.ApplyResources(this.splitContainer.Panel2, "splitContainer.Panel2");
            this.splitContainer.Panel2.Controls.Add(this.dgvWayPoints);
            this.splitContainer.Panel2.Controls.Add(this.panelDroneInfo);
            // 
            // CollectionTelemetryData
            // 
            resources.ApplyResources(this.CollectionTelemetryData, "CollectionTelemetryData");
            this.CollectionTelemetryData.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.CollectionTelemetryData.Name = "CollectionTelemetryData";
            // 
            // DroneInfo3
            // 
            resources.ApplyResources(this.DroneInfo3, "DroneInfo3");
            this.DroneInfo3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DroneInfo3.DroneName = "APM-3";
            this.DroneInfo3.IsActivate = false;
            this.DroneInfo3.Name = "DroneInfo3";
            this.DroneInfo3.Tag = "2";
            this.DroneInfo3.DoubleClick += new System.EventHandler(this.DroneInfo_DoubleClick);
            // 
            // DroneInfo2
            // 
            resources.ApplyResources(this.DroneInfo2, "DroneInfo2");
            this.DroneInfo2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DroneInfo2.DroneName = "APM-2";
            this.DroneInfo2.IsActivate = false;
            this.DroneInfo2.Name = "DroneInfo2";
            this.DroneInfo2.Tag = "1";
            this.DroneInfo2.DoubleClick += new System.EventHandler(this.DroneInfo_DoubleClick);
            // 
            // DroneInfo1
            // 
            resources.ApplyResources(this.DroneInfo1, "DroneInfo1");
            this.DroneInfo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.DroneInfo1.DroneName = "APM-1";
            this.DroneInfo1.IsActivate = false;
            this.DroneInfo1.Name = "DroneInfo1";
            this.DroneInfo1.Tag = "0";
            this.DroneInfo1.DoubleClick += new System.EventHandler(this.DroneInfo_DoubleClick);
            // 
            // BtnRTL
            // 
            resources.ApplyResources(this.BtnRTL, "BtnRTL");
            this.BtnRTL.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnRTL.FlatAppearance.BorderSize = 0;
            this.BtnRTL.ForeColor = System.Drawing.Color.White;
            this.BtnRTL.Name = "BtnRTL";
            this.BtnRTL.UseVisualStyleBackColor = false;
            this.BtnRTL.Click += new System.EventHandler(this.BUT_RTL_Click);
            // 
            // BtnLand
            // 
            resources.ApplyResources(this.BtnLand, "BtnLand");
            this.BtnLand.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnLand.FlatAppearance.BorderSize = 0;
            this.BtnLand.ForeColor = System.Drawing.Color.White;
            this.BtnLand.Image = global::Diva.Properties.Resources.icon_land;
            this.BtnLand.Name = "BtnLand";
            this.BtnLand.UseVisualStyleBackColor = false;
            this.BtnLand.Click += new System.EventHandler(this.BUT_Land_Click);
            this.BtnLand.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
            this.BtnLand.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
            // 
            // BtnVideo
            // 
            resources.ApplyResources(this.BtnVideo, "BtnVideo");
            this.BtnVideo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnVideo.FlatAppearance.BorderSize = 0;
            this.BtnVideo.ForeColor = System.Drawing.Color.White;
            this.BtnVideo.Image = global::Diva.Properties.Resources.icon_add;
            this.BtnVideo.Name = "BtnVideo";
            this.BtnVideo.UseVisualStyleBackColor = false;
            this.BtnVideo.Click += new System.EventHandler(this.VideoDemo_Click);
            this.BtnVideo.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
            this.BtnVideo.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
            // 
            // BtnAuto
            // 
            resources.ApplyResources(this.BtnAuto, "BtnAuto");
            this.BtnAuto.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnAuto.FlatAppearance.BorderSize = 0;
            this.BtnAuto.ForeColor = System.Drawing.Color.White;
            this.BtnAuto.Image = global::Diva.Properties.Resources.icon_auto;
            this.BtnAuto.Name = "BtnAuto";
            this.BtnAuto.UseVisualStyleBackColor = false;
            this.BtnAuto.Click += new System.EventHandler(this.BUT_Auto_Click);
            this.BtnAuto.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
            this.BtnAuto.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
            // 
            // BtnArm
            // 
            resources.ApplyResources(this.BtnArm, "BtnArm");
            this.BtnArm.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnArm.FlatAppearance.BorderSize = 0;
            this.BtnArm.ForeColor = System.Drawing.Color.White;
            this.BtnArm.Image = global::Diva.Properties.Resources.icon_connect;
            this.BtnArm.Name = "BtnArm";
            this.BtnArm.UseVisualStyleBackColor = false;
            this.BtnArm.Click += new System.EventHandler(this.BUT_Arm_Click);
            this.BtnArm.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
            this.BtnArm.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
            // 
            // BtnReadWPs
            // 
            resources.ApplyResources(this.BtnReadWPs, "BtnReadWPs");
            this.BtnReadWPs.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnReadWPs.FlatAppearance.BorderSize = 0;
            this.BtnReadWPs.ForeColor = System.Drawing.Color.White;
            this.BtnReadWPs.Image = global::Diva.Properties.Resources.icon_readwps;
            this.BtnReadWPs.Name = "BtnReadWPs";
            this.BtnReadWPs.UseVisualStyleBackColor = false;
            this.BtnReadWPs.Click += new System.EventHandler(this.BUT_read_Click);
            this.BtnReadWPs.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
            this.BtnReadWPs.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
            // 
            // BtnTakeOff
            // 
            resources.ApplyResources(this.BtnTakeOff, "BtnTakeOff");
            this.BtnTakeOff.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnTakeOff.FlatAppearance.BorderSize = 0;
            this.BtnTakeOff.ForeColor = System.Drawing.Color.White;
            this.BtnTakeOff.Image = global::Diva.Properties.Resources.icon_takeoff;
            this.BtnTakeOff.Name = "BtnTakeOff";
            this.BtnTakeOff.UseVisualStyleBackColor = false;
            this.BtnTakeOff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
            this.BtnTakeOff.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
            this.BtnTakeOff.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
            // 
            // BtnWriteWPs
            // 
            resources.ApplyResources(this.BtnWriteWPs, "BtnWriteWPs");
            this.BtnWriteWPs.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnWriteWPs.FlatAppearance.BorderSize = 0;
            this.BtnWriteWPs.ForeColor = System.Drawing.Color.White;
            this.BtnWriteWPs.Image = global::Diva.Properties.Resources.icon_writewps;
            this.BtnWriteWPs.Name = "BtnWriteWPs";
            this.BtnWriteWPs.UseVisualStyleBackColor = false;
            this.BtnWriteWPs.Click += new System.EventHandler(this.BUT_write_Click);
            this.BtnWriteWPs.MouseLeave += new System.EventHandler(this.BUT_Mouse_Leave);
            this.BtnWriteWPs.MouseHover += new System.EventHandler(this.BUT_Mouse_Hover);
            // 
            // TSMainPanel
            // 
            resources.ApplyResources(this.TSMainPanel, "TSMainPanel");
            this.TSMainPanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TSMainPanel.GripMargin = new System.Windows.Forms.Padding(0);
            this.TSMainPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TSMainPanel.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.TSMainPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSBtnConnect,
            this.TSBtnRotation,
            this.TSBtnConfigure,
            this.TSBtnTagging,
            this.BtnSaveMission,
            this.BtnReadMission});
            this.TSMainPanel.Name = "TSMainPanel";
            // 
            // TSBtnConnect
            // 
            resources.ApplyResources(this.TSBtnConnect, "TSBtnConnect");
            this.TSBtnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConnect.Image = global::Diva.Properties.Resources.icon_arm;
            this.TSBtnConnect.Margin = new System.Windows.Forms.Padding(0);
            this.TSBtnConnect.Name = "TSBtnConnect";
            this.TSBtnConnect.Click += new System.EventHandler(this.BUT_Connect_Click);
            this.TSBtnConnect.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
            this.TSBtnConnect.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
            // 
            // TSBtnRotation
            // 
            resources.ApplyResources(this.TSBtnRotation, "TSBtnRotation");
            this.TSBtnRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnRotation.Image = global::Diva.Properties.Resources.icon_rotation;
            this.TSBtnRotation.Name = "TSBtnRotation";
            this.TSBtnRotation.Click += new System.EventHandler(this.BUT_Rotation2_Click);
            this.TSBtnRotation.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
            this.TSBtnRotation.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
            // 
            // TSBtnConfigure
            // 
            resources.ApplyResources(this.TSBtnConfigure, "TSBtnConfigure");
            this.TSBtnConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConfigure.Image = global::Diva.Properties.Resources.icon_configure;
            this.TSBtnConfigure.Name = "TSBtnConfigure";
            this.TSBtnConfigure.Click += new System.EventHandler(this.BUT_Configure_Click);
            this.TSBtnConfigure.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
            this.TSBtnConfigure.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
            // 
            // TSBtnTagging
            // 
            resources.ApplyResources(this.TSBtnTagging, "TSBtnTagging");
            this.TSBtnTagging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnTagging.Image = global::Diva.Properties.Resources.icon_tagging;
            this.TSBtnTagging.Name = "TSBtnTagging";
            this.TSBtnTagging.Click += new System.EventHandler(this.BUT_Tagging_Click);
            this.TSBtnTagging.MouseLeave += new System.EventHandler(this.TSBUT_Mouse_Leave);
            this.TSBtnTagging.MouseHover += new System.EventHandler(this.TSBUT_Mouse_Hover);
            // 
            // BtnSaveMission
            // 
            resources.ApplyResources(this.BtnSaveMission, "BtnSaveMission");
            this.BtnSaveMission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnSaveMission.ForeColor = System.Drawing.Color.White;
            this.BtnSaveMission.Name = "BtnSaveMission";
            this.BtnSaveMission.Click += new System.EventHandler(this.BtnSaveMission_Click);
            // 
            // BtnReadMission
            // 
            resources.ApplyResources(this.BtnReadMission, "BtnReadMission");
            this.BtnReadMission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnReadMission.ForeColor = System.Drawing.Color.White;
            this.BtnReadMission.Name = "BtnReadMission";
            this.BtnReadMission.Click += new System.EventHandler(this.BtnReadMission_Click);
            // 
            // myMap
            // 
            resources.ApplyResources(this.myMap, "myMap");
            this.myMap.BackColor = System.Drawing.SystemColors.Control;
            this.myMap.Bearing = 0F;
            this.myMap.CanDragMap = true;
            this.myMap.ContextMenuStrip = this.cmMap;
            this.myMap.DebugMapLocation = true;
            this.myMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.myMap.GrayScaleMode = false;
            this.myMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.myMap.LevelsKeepInMemmory = 5;
            this.myMap.MarkersEnabled = true;
            this.myMap.MaxZoom = 24;
            this.myMap.MinZoom = 0;
            this.myMap.MouseWheelZoomEnabled = true;
            this.myMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.myMap.Name = "myMap";
            this.myMap.NegativeMode = false;
            this.myMap.PolygonsEnabled = true;
            this.myMap.RetryLoadTile = 0;
            this.myMap.RoutesEnabled = true;
            this.myMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.myMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.myMap.ShowTileGridLines = false;
            this.myMap.Zoom = 15D;
            // 
            // cmMap
            // 
            resources.ApplyResources(this.cmMap, "cmMap");
            this.cmMap.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClearMission,
            this.miSetHomeHere,
            this.miClearAllMissions});
            this.cmMap.Name = "contextMenuStrip1";
            // 
            // miClearMission
            // 
            resources.ApplyResources(this.miClearMission, "miClearMission");
            this.miClearMission.Name = "miClearMission";
            this.miClearMission.Click += new System.EventHandler(this.goHereToolStripMenuItem_Click);
            // 
            // miSetHomeHere
            // 
            resources.ApplyResources(this.miSetHomeHere, "miSetHomeHere");
            this.miSetHomeHere.Name = "miSetHomeHere";
            this.miSetHomeHere.Click += new System.EventHandler(this.setHomeHereToolStripMenuItem_Click);
            // 
            // miClearAllMissions
            // 
            resources.ApplyResources(this.miClearAllMissions, "miClearAllMissions");
            this.miClearAllMissions.Name = "miClearAllMissions";
            this.miClearAllMissions.Click += new System.EventHandler(this.clearMissionToolStripMenuItem_Click);
            // 
            // dgvWayPoints
            // 
            resources.ApplyResources(this.dgvWayPoints, "dgvWayPoints");
            this.dgvWayPoints.AllowUserToAddRows = false;
            this.dgvWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvWayPoints.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvWayPoints.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.dgvWayPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvWayPoints.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvWayPoints.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWayPoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWayPoints.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWayPoints.EnableHeadersVisualStyles = false;
            this.dgvWayPoints.GridColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgvWayPoints.Name = "dgvWayPoints";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWayPoints.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvWayPoints.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWayPoints.RowTemplate.Height = 24;
            this.dgvWayPoints.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_CellContentClick);
            this.dgvWayPoints.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_RowEnter);
            this.dgvWayPoints.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Commands_RowsAdded);
            this.dgvWayPoints.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Commands_RowValidating);
            // 
            // colCommand
            // 
            this.colCommand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colCommand.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            resources.ApplyResources(this.colCommand, "colCommand");
            this.colCommand.Name = "colCommand";
            // 
            // colParam1
            // 
            this.colParam1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colParam1, "colParam1");
            this.colParam1.Name = "colParam1";
            this.colParam1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colParam2
            // 
            this.colParam2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colParam2, "colParam2");
            this.colParam2.Name = "colParam2";
            this.colParam2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colParam3
            // 
            this.colParam3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colParam3, "colParam3");
            this.colParam3.Name = "colParam3";
            this.colParam3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colParam4
            // 
            this.colParam4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colParam4, "colParam4");
            this.colParam4.Name = "colParam4";
            this.colParam4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLatitude
            // 
            this.colLatitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colLatitude, "colLatitude");
            this.colLatitude.Name = "colLatitude";
            this.colLatitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLongitude
            // 
            this.colLongitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colLongitude, "colLongitude");
            this.colLongitude.Name = "colLongitude";
            this.colLongitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAltitude
            // 
            this.colAltitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colAltitude, "colAltitude");
            this.colAltitude.Name = "colAltitude";
            this.colAltitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAngle
            // 
            this.colAngle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colAngle, "colAngle");
            this.colAngle.Name = "colAngle";
            this.colAngle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDelete
            // 
            this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colDelete, "colDelete");
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "X";
            // 
            // colTagData
            // 
            this.colTagData.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            resources.ApplyResources(this.colTagData, "colTagData");
            this.colTagData.Name = "colTagData";
            this.colTagData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelDroneInfo
            // 
            resources.ApplyResources(this.panelDroneInfo, "panelDroneInfo");
            this.panelDroneInfo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.panelDroneInfo.Controls.Add(this.TxtHomeLongitude);
            this.panelDroneInfo.Controls.Add(this.TxtHomeLatitude);
            this.panelDroneInfo.Controls.Add(this.TxtHomeAltitude);
            this.panelDroneInfo.Controls.Add(this.LabelLongitude);
            this.panelDroneInfo.Controls.Add(this.LabelLatitude);
            this.panelDroneInfo.Controls.Add(this.LabelAltitude);
            this.panelDroneInfo.Controls.Add(this.TxtDroneMode);
            this.panelDroneInfo.Controls.Add(this.BtnDroneMode);
            this.panelDroneInfo.Controls.Add(this.BtnAltitude);
            this.panelDroneInfo.Controls.Add(this.BtnHomeLand);
            this.panelDroneInfo.Controls.Add(this.TxtAltitudeValue);
            this.panelDroneInfo.Name = "panelDroneInfo";
            // 
            // TxtHomeLongitude
            // 
            resources.ApplyResources(this.TxtHomeLongitude, "TxtHomeLongitude");
            this.TxtHomeLongitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHomeLongitude.Name = "TxtHomeLongitude";
            // 
            // TxtHomeLatitude
            // 
            resources.ApplyResources(this.TxtHomeLatitude, "TxtHomeLatitude");
            this.TxtHomeLatitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHomeLatitude.Name = "TxtHomeLatitude";
            // 
            // TxtHomeAltitude
            // 
            resources.ApplyResources(this.TxtHomeAltitude, "TxtHomeAltitude");
            this.TxtHomeAltitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHomeAltitude.Name = "TxtHomeAltitude";
            // 
            // LabelLongitude
            // 
            resources.ApplyResources(this.LabelLongitude, "LabelLongitude");
            this.LabelLongitude.ForeColor = System.Drawing.Color.White;
            this.LabelLongitude.Name = "LabelLongitude";
            // 
            // LabelLatitude
            // 
            resources.ApplyResources(this.LabelLatitude, "LabelLatitude");
            this.LabelLatitude.ForeColor = System.Drawing.Color.White;
            this.LabelLatitude.Name = "LabelLatitude";
            // 
            // LabelAltitude
            // 
            resources.ApplyResources(this.LabelAltitude, "LabelAltitude");
            this.LabelAltitude.ForeColor = System.Drawing.Color.White;
            this.LabelAltitude.Name = "LabelAltitude";
            // 
            // TxtDroneMode
            // 
            resources.ApplyResources(this.TxtDroneMode, "TxtDroneMode");
            this.TxtDroneMode.ForeColor = System.Drawing.Color.White;
            this.TxtDroneMode.Name = "TxtDroneMode";
            // 
            // BtnDroneMode
            // 
            resources.ApplyResources(this.BtnDroneMode, "BtnDroneMode");
            this.BtnDroneMode.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnDroneMode.FlatAppearance.BorderSize = 0;
            this.BtnDroneMode.ForeColor = System.Drawing.Color.White;
            this.BtnDroneMode.Image = global::Diva.Properties.Resources.icon_airplane_32;
            this.BtnDroneMode.Name = "BtnDroneMode";
            this.BtnDroneMode.UseVisualStyleBackColor = false;
            // 
            // BtnAltitude
            // 
            resources.ApplyResources(this.BtnAltitude, "BtnAltitude");
            this.BtnAltitude.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnAltitude.FlatAppearance.BorderSize = 0;
            this.BtnAltitude.ForeColor = System.Drawing.Color.White;
            this.BtnAltitude.Image = global::Diva.Properties.Resources.icon_edit_32;
            this.BtnAltitude.Name = "BtnAltitude";
            this.BtnAltitude.UseVisualStyleBackColor = false;
            // 
            // BtnHomeLand
            // 
            resources.ApplyResources(this.BtnHomeLand, "BtnHomeLand");
            this.BtnHomeLand.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnHomeLand.FlatAppearance.BorderSize = 0;
            this.BtnHomeLand.ForeColor = System.Drawing.Color.White;
            this.BtnHomeLand.Image = global::Diva.Properties.Resources.icon_house_32;
            this.BtnHomeLand.Name = "BtnHomeLand";
            this.BtnHomeLand.UseVisualStyleBackColor = false;
            // 
            // TxtAltitudeValue
            // 
            resources.ApplyResources(this.TxtAltitudeValue, "TxtAltitudeValue");
            this.TxtAltitudeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltitudeValue.Name = "TxtAltitudeValue";
            // 
            // timerMapItemUpdate
            // 
            this.timerMapItemUpdate.Interval = 1200;
            this.timerMapItemUpdate.Tick += new System.EventHandler(this.timerMapItemUpdate_Tick);
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
            // Planner
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "Planner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Planner_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Planner_FormClosed);
            this.Load += new System.EventHandler(this.Planner_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.TSMainPanel.ResumeLayout(false);
            this.TSMainPanel.PerformLayout();
            this.cmMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWayPoints)).EndInit();
            this.panelDroneInfo.ResumeLayout(false);
            this.panelDroneInfo.PerformLayout();
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
        private SplitContainer splitContainer;
		private Button BtnHomeLand;
		private ImageList ImgListBatteryHealth;
		private Button BtnRTL;
		private DroneInfoPanel DroneInfo1;
		private DroneInfoPanel DroneInfo2;
		private DroneInfoPanel DroneInfo3;
		private TelemetryDataPanel CollectionTelemetryData;
		private Button BtnAltitude;
		private Button BtnDroneMode;
		private Label TxtDroneMode;
		private Label LabelLongitude;
		private Label LabelLatitude;
		private Label LabelAltitude;
		private TextBox TxtHomeAltitude;
		private TextBox TxtHomeLatitude;
		private TextBox TxtHomeLongitude;
		private ToolStripButton BtnSaveMission;
		private ToolStripButton BtnReadMission;
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
    }
}