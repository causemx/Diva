﻿using System.Windows.Forms;
using Diva.Controls;
using Diva.Controls.Components;

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
            this.BtnRTL = new Diva.Controls.Components.MyButton();
            this.BtnLand = new Diva.Controls.Components.MyButton();
            this.BtnVideo = new Diva.Controls.Components.MyButton();
            this.BtnAuto = new Diva.Controls.Components.MyButton();
            this.BtnArm = new Diva.Controls.Components.MyButton();
            this.BtnReadWPs = new Diva.Controls.Components.MyButton();
            this.BtnTakeOff = new Diva.Controls.Components.MyButton();
            this.BtnWriteWPs = new Diva.Controls.Components.MyButton();
            this.TSMainPanel = new System.Windows.Forms.ToolStrip();
            this.TSBtnConnect = new Diva.Controls.Components.MyTSButton();
            this.TSBtnRotation = new Diva.Controls.Components.MyTSButton();
            this.TSBtnConfigure = new Diva.Controls.Components.MyTSButton();
            this.TSBtnTagging = new Diva.Controls.Components.MyTSButton();
            this.TSBtnSaveMission = new Diva.Controls.Components.MyTSButton();
            this.TSBtnReadMission = new Diva.Controls.Components.MyTSButton();
            this.myMap = new Diva.Controls.MyGMap();
            this.cmMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.drawPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPolygonPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noFlyZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setReturnLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
            this.BtnRTL.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnRTL.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnRTL.ClickImage = null;
            this.BtnRTL.FlatAppearance.BorderSize = 0;
            this.BtnRTL.ForeColor = System.Drawing.Color.White;
            this.BtnRTL.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnRTL.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnRTL.HoverImage = null;
            this.BtnRTL.Name = "BtnRTL";
            this.BtnRTL.UseVisualStyleBackColor = false;
            this.BtnRTL.Click += new System.EventHandler(this.BUT_RTL_Click);
            // 
            // BtnLand
            // 
            resources.ApplyResources(this.BtnLand, "BtnLand");
            this.BtnLand.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnLand.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnLand.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnLand.ClickImage = null;
            this.BtnLand.FlatAppearance.BorderSize = 0;
            this.BtnLand.ForeColor = System.Drawing.Color.White;
            this.BtnLand.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnLand.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnLand.HoverImage = global::Diva.Properties.Resources.icon_land_active;
            this.BtnLand.Image = global::Diva.Properties.Resources.icon_land;
            this.BtnLand.Name = "BtnLand";
            this.BtnLand.UseVisualStyleBackColor = false;
            this.BtnLand.Click += new System.EventHandler(this.BUT_Land_Click);
            // 
            // BtnVideo
            // 
            resources.ApplyResources(this.BtnVideo, "BtnVideo");
            this.BtnVideo.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnVideo.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnVideo.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnVideo.ClickImage = null;
            this.BtnVideo.FlatAppearance.BorderSize = 0;
            this.BtnVideo.ForeColor = System.Drawing.Color.White;
            this.BtnVideo.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnVideo.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnVideo.HoverImage = global::Diva.Properties.Resources.icon_add_active;
            this.BtnVideo.Image = global::Diva.Properties.Resources.icon_add;
            this.BtnVideo.Name = "BtnVideo";
            this.BtnVideo.UseVisualStyleBackColor = false;
            this.BtnVideo.Click += new System.EventHandler(this.VideoDemo_Click);
            // 
            // BtnAuto
            // 
            resources.ApplyResources(this.BtnAuto, "BtnAuto");
            this.BtnAuto.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnAuto.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnAuto.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnAuto.ClickImage = null;
            this.BtnAuto.FlatAppearance.BorderSize = 0;
            this.BtnAuto.ForeColor = System.Drawing.Color.White;
            this.BtnAuto.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnAuto.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnAuto.HoverImage = global::Diva.Properties.Resources.icon_auto_active;
            this.BtnAuto.Image = global::Diva.Properties.Resources.icon_auto;
            this.BtnAuto.Name = "BtnAuto";
            this.BtnAuto.UseVisualStyleBackColor = false;
            this.BtnAuto.Click += new System.EventHandler(this.BUT_Auto_Click);
            // 
            // BtnArm
            // 
            resources.ApplyResources(this.BtnArm, "BtnArm");
            this.BtnArm.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnArm.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnArm.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnArm.ClickImage = null;
            this.BtnArm.FlatAppearance.BorderSize = 0;
            this.BtnArm.ForeColor = System.Drawing.Color.White;
            this.BtnArm.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnArm.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnArm.HoverImage = global::Diva.Properties.Resources.icon_connect_active;
            this.BtnArm.Image = global::Diva.Properties.Resources.icon_connect;
            this.BtnArm.Name = "BtnArm";
            this.BtnArm.UseVisualStyleBackColor = false;
            this.BtnArm.Click += new System.EventHandler(this.BUT_Arm_Click);
            // 
            // BtnReadWPs
            // 
            resources.ApplyResources(this.BtnReadWPs, "BtnReadWPs");
            this.BtnReadWPs.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnReadWPs.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.ClickImage = null;
            this.BtnReadWPs.FlatAppearance.BorderSize = 0;
            this.BtnReadWPs.ForeColor = System.Drawing.Color.White;
            this.BtnReadWPs.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnReadWPs.HoverImage = global::Diva.Properties.Resources.icon_readwps_active;
            this.BtnReadWPs.Image = global::Diva.Properties.Resources.icon_readwps;
            this.BtnReadWPs.Name = "BtnReadWPs";
            this.BtnReadWPs.UseVisualStyleBackColor = false;
            this.BtnReadWPs.Click += new System.EventHandler(this.BUT_read_Click);
            // 
            // BtnTakeOff
            // 
            resources.ApplyResources(this.BtnTakeOff, "BtnTakeOff");
            this.BtnTakeOff.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnTakeOff.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.ClickImage = null;
            this.BtnTakeOff.FlatAppearance.BorderSize = 0;
            this.BtnTakeOff.ForeColor = System.Drawing.Color.White;
            this.BtnTakeOff.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnTakeOff.HoverImage = global::Diva.Properties.Resources.icon_takeoff_active;
            this.BtnTakeOff.Image = global::Diva.Properties.Resources.icon_takeoff;
            this.BtnTakeOff.Name = "BtnTakeOff";
            this.BtnTakeOff.UseVisualStyleBackColor = false;
            this.BtnTakeOff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
            // 
            // BtnWriteWPs
            // 
            resources.ApplyResources(this.BtnWriteWPs, "BtnWriteWPs");
            this.BtnWriteWPs.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BtnWriteWPs.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.ClickImage = null;
            this.BtnWriteWPs.FlatAppearance.BorderSize = 0;
            this.BtnWriteWPs.ForeColor = System.Drawing.Color.White;
            this.BtnWriteWPs.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnWriteWPs.HoverImage = global::Diva.Properties.Resources.icon_writewps_active;
            this.BtnWriteWPs.Image = global::Diva.Properties.Resources.icon_writewps;
            this.BtnWriteWPs.Name = "BtnWriteWPs";
            this.BtnWriteWPs.UseVisualStyleBackColor = false;
            this.BtnWriteWPs.Click += new System.EventHandler(this.BUT_write_Click);
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
            this.TSBtnSaveMission,
            this.TSBtnReadMission});
            this.TSMainPanel.Name = "TSMainPanel";
            // 
            // TSBtnConnect
            // 
            resources.ApplyResources(this.TSBtnConnect, "TSBtnConnect");
            this.TSBtnConnect.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.ClickImage = null;
            this.TSBtnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConnect.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnConnect.HoverImage = global::Diva.Properties.Resources.icon_arm_active;
            this.TSBtnConnect.Image = global::Diva.Properties.Resources.icon_arm;
            this.TSBtnConnect.Margin = new System.Windows.Forms.Padding(0);
            this.TSBtnConnect.Name = "TSBtnConnect";
            this.TSBtnConnect.Click += new System.EventHandler(this.BUT_Connect_Click);
            // 
            // TSBtnRotation
            // 
            resources.ApplyResources(this.TSBtnRotation, "TSBtnRotation");
            this.TSBtnRotation.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnRotation.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnRotation.ClickImage = null;
            this.TSBtnRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnRotation.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnRotation.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnRotation.HoverImage = global::Diva.Properties.Resources.icon_rotation_active;
            this.TSBtnRotation.Image = global::Diva.Properties.Resources.icon_rotation;
            this.TSBtnRotation.Name = "TSBtnRotation";
            this.TSBtnRotation.Click += new System.EventHandler(this.BUT_Rotation2_Click);
            // 
            // TSBtnConfigure
            // 
            resources.ApplyResources(this.TSBtnConfigure, "TSBtnConfigure");
            this.TSBtnConfigure.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.ClickImage = null;
            this.TSBtnConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConfigure.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnConfigure.HoverImage = global::Diva.Properties.Resources.icon_configure_active;
            this.TSBtnConfigure.Image = global::Diva.Properties.Resources.icon_configure;
            this.TSBtnConfigure.Name = "TSBtnConfigure";
            this.TSBtnConfigure.Click += new System.EventHandler(this.BUT_Configure_Click);
            // 
            // TSBtnTagging
            // 
            resources.ApplyResources(this.TSBtnTagging, "TSBtnTagging");
            this.TSBtnTagging.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.ClickImage = null;
            this.TSBtnTagging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnTagging.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnTagging.HoverImage = global::Diva.Properties.Resources.icon_tagging_active;
            this.TSBtnTagging.Image = global::Diva.Properties.Resources.icon_tagging;
            this.TSBtnTagging.Name = "TSBtnTagging";
            this.TSBtnTagging.Click += new System.EventHandler(this.BUT_Tagging_Click);
            // 
            // TSBtnSaveMission
            // 
            resources.ApplyResources(this.TSBtnSaveMission, "TSBtnSaveMission");
            this.TSBtnSaveMission.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.ClickImage = null;
            this.TSBtnSaveMission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSBtnSaveMission.ForeColor = System.Drawing.Color.White;
            this.TSBtnSaveMission.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnSaveMission.HoverImage = null;
            this.TSBtnSaveMission.Name = "TSBtnSaveMission";
            this.TSBtnSaveMission.Click += new System.EventHandler(this.BtnSaveMission_Click);
            // 
            // TSBtnReadMission
            // 
            resources.ApplyResources(this.TSBtnReadMission, "TSBtnReadMission");
            this.TSBtnReadMission.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.ClickImage = null;
            this.TSBtnReadMission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSBtnReadMission.ForeColor = System.Drawing.Color.White;
            this.TSBtnReadMission.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnReadMission.HoverImage = null;
            this.TSBtnReadMission.Name = "TSBtnReadMission";
            this.TSBtnReadMission.Click += new System.EventHandler(this.BtnReadMission_Click);
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
            this.myMap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.MainMap_OnMarkerClick);
            this.myMap.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.MainMap_OnMarkerEnter);
            this.myMap.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.MainMap_OnMarkerLeave);
            this.myMap.OnPositionChanged += new GMap.NET.PositionChanged(this.MainMap_OnCurrentPositionChanged);
            this.myMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseDown);
            this.myMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseMove);
            this.myMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseUp);
            // 
            // cmMap
            // 
            resources.ApplyResources(this.cmMap, "cmMap");
            this.cmMap.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawPolygonToolStripMenuItem,
            this.noFlyZoneToolStripMenuItem,
            this.toolStripSeparator1,
            this.miClearMission,
            this.miSetHomeHere,
            this.miClearAllMissions});
            this.cmMap.Name = "contextMenuStrip1";
            // 
            // drawPolygonToolStripMenuItem
            // 
            resources.ApplyResources(this.drawPolygonToolStripMenuItem, "drawPolygonToolStripMenuItem");
            this.drawPolygonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPolygonPointToolStripMenuItem,
            this.clearPolygonToolStripMenuItem,
            this.savePolygonToolStripMenuItem,
            this.loadPolygonToolStripMenuItem,
            this.areaToolStripMenuItem});
            this.drawPolygonToolStripMenuItem.Name = "drawPolygonToolStripMenuItem";
            // 
            // addPolygonPointToolStripMenuItem
            // 
            resources.ApplyResources(this.addPolygonPointToolStripMenuItem, "addPolygonPointToolStripMenuItem");
            this.addPolygonPointToolStripMenuItem.Name = "addPolygonPointToolStripMenuItem";
            this.addPolygonPointToolStripMenuItem.Click += new System.EventHandler(this.addPolygonPointToolStripMenuItem_Click);
            // 
            // clearPolygonToolStripMenuItem
            // 
            resources.ApplyResources(this.clearPolygonToolStripMenuItem, "clearPolygonToolStripMenuItem");
            this.clearPolygonToolStripMenuItem.Name = "clearPolygonToolStripMenuItem";
            this.clearPolygonToolStripMenuItem.Click += new System.EventHandler(this.clearPolygonToolStripMenuItem_Click);
            // 
            // savePolygonToolStripMenuItem
            // 
            resources.ApplyResources(this.savePolygonToolStripMenuItem, "savePolygonToolStripMenuItem");
            this.savePolygonToolStripMenuItem.Name = "savePolygonToolStripMenuItem";
            this.savePolygonToolStripMenuItem.Click += new System.EventHandler(this.savePolygonToolStripMenuItem_Click);
            // 
            // loadPolygonToolStripMenuItem
            // 
            resources.ApplyResources(this.loadPolygonToolStripMenuItem, "loadPolygonToolStripMenuItem");
            this.loadPolygonToolStripMenuItem.Name = "loadPolygonToolStripMenuItem";
            this.loadPolygonToolStripMenuItem.Click += new System.EventHandler(this.loadPolygonToolStripMenuItem_Click);
            // 
            // areaToolStripMenuItem
            // 
            resources.ApplyResources(this.areaToolStripMenuItem, "areaToolStripMenuItem");
            this.areaToolStripMenuItem.Name = "areaToolStripMenuItem";
            // 
            // noFlyZoneToolStripMenuItem
            // 
            resources.ApplyResources(this.noFlyZoneToolStripMenuItem, "noFlyZoneToolStripMenuItem");
            this.noFlyZoneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.setReturnLocationToolStripMenuItem,
            this.loadFromFileToolStripMenuItem,
            this.saveToFileToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.noFlyZoneToolStripMenuItem.Name = "noFlyZoneToolStripMenuItem";
            // 
            // uploadToolStripMenuItem
            // 
            resources.ApplyResources(this.uploadToolStripMenuItem, "uploadToolStripMenuItem");
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.GeoFenceuploadToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            resources.ApplyResources(this.downloadToolStripMenuItem, "downloadToolStripMenuItem");
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.GeoFencedownloadToolStripMenuItem_Click);
            // 
            // setReturnLocationToolStripMenuItem
            // 
            resources.ApplyResources(this.setReturnLocationToolStripMenuItem, "setReturnLocationToolStripMenuItem");
            this.setReturnLocationToolStripMenuItem.Name = "setReturnLocationToolStripMenuItem";
            this.setReturnLocationToolStripMenuItem.Click += new System.EventHandler(this.setReturnLocationToolStripMenuItem_Click);
            // 
            // loadFromFileToolStripMenuItem
            // 
            resources.ApplyResources(this.loadFromFileToolStripMenuItem, "loadFromFileToolStripMenuItem");
            this.loadFromFileToolStripMenuItem.Name = "loadFromFileToolStripMenuItem";
            this.loadFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadFromFileToolStripMenuItem_Click);
            // 
            // saveToFileToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToFileToolStripMenuItem, "saveToFileToolStripMenuItem");
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
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
		private MyTSButton TSBtnConnect;
		private MyTSButton TSBtnRotation;
		private MyTSButton TSBtnConfigure;
		private MyGMap myMap;
		private MyButton BtnArm;
		private MyButton BtnReadWPs;
		private MyButton BtnWriteWPs;
		private MyButton BtnAuto;
		private MyButton BtnTakeOff;
		private MyButton BtnLand;
		private MyTSButton TSBtnTagging;
        private MyButton BtnVideo;
        private SplitContainer splitContainer;
		private Button BtnHomeLand;
		private ImageList ImgListBatteryHealth;
		private MyButton BtnRTL;
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
		private MyTSButton TSBtnSaveMission;
		private MyTSButton TSBtnReadMission;
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
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem drawPolygonToolStripMenuItem;
		private ToolStripMenuItem addPolygonPointToolStripMenuItem;
		private ToolStripMenuItem clearPolygonToolStripMenuItem;
		private ToolStripMenuItem savePolygonToolStripMenuItem;
		private ToolStripMenuItem loadPolygonToolStripMenuItem;
		private ToolStripMenuItem areaToolStripMenuItem;
		private ToolStripMenuItem noFlyZoneToolStripMenuItem;
		private ToolStripMenuItem uploadToolStripMenuItem;
		private ToolStripMenuItem downloadToolStripMenuItem;
		private ToolStripMenuItem setReturnLocationToolStripMenuItem;
		private ToolStripMenuItem loadFromFileToolStripMenuItem;
		private ToolStripMenuItem saveToFileToolStripMenuItem;
		private ToolStripMenuItem clearToolStripMenuItem;
	}
}