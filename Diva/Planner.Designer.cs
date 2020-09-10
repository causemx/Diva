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
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.BtnAltitudeLower = new Diva.Controls.Components.MyButton();
            this.BtnAltitudeHigher = new Diva.Controls.Components.MyButton();
            this.BtnAltitudeHighest = new Diva.Controls.Components.MyButton();
            this.LblModeDesc = new System.Windows.Forms.Label();
            this.BtnZoomOut = new Diva.Controls.Components.MyButton();
            this.BtnZoomIn = new Diva.Controls.Components.MyButton();
            this.BtnMapFocus = new Diva.Controls.Components.MyButton();
            this.LblMode = new System.Windows.Forms.Label();
            this.RotationInfoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DroneInfoPanel = new Diva.Controls.DroneInfoPanel();
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
            this.TSBtnConfigure = new Diva.Controls.Components.MyTSButton();
            this.Btn_Rotation = new Diva.Controls.Components.MyTSButton();
            this.TSBtnTagging = new Diva.Controls.Components.MyTSButton();
            this.TSBtnSaveMission = new Diva.Controls.Components.MyTSButton();
            this.TSBtnReadMission = new Diva.Controls.Components.MyTSButton();
            this.TSBtnCusOverlay = new Diva.Controls.Components.MyTSButton();
            this.Map = new Diva.Controls.MyGMap();
            this.cmMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.drawPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPolygonPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPolygonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.areaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noFlyZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setReturnLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miClearMission = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetHomeHere = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearAllMissions = new System.Windows.Forms.ToolStripMenuItem();
            this.DGVWayPoints = new System.Windows.Forms.DataGridView();
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
            this.ActiveDroneInfoPanel = new System.Windows.Forms.Panel();
            this.ComBoxModeSwitch = new System.Windows.Forms.ComboBox();
            this.TxtHomeLongitude = new System.Windows.Forms.TextBox();
            this.TxtHomeLatitude = new System.Windows.Forms.TextBox();
            this.TxtHomeAltitude = new System.Windows.Forms.TextBox();
            this.LabelLongitude = new System.Windows.Forms.Label();
            this.LabelLatitude = new System.Windows.Forms.Label();
            this.LabelAltitude = new System.Windows.Forms.Label();
            this.BtnDroneMode = new System.Windows.Forms.Button();
            this.BtnAltitude = new System.Windows.Forms.Button();
            this.BtnHome = new System.Windows.Forms.Button();
            this.TxtAltitudeValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.TSMainPanel.SuspendLayout();
            this.cmMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVWayPoints)).BeginInit();
            this.ActiveDroneInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            resources.ApplyResources(this.SplitContainer, "SplitContainer");
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.BtnAltitudeLower);
            this.SplitContainer.Panel1.Controls.Add(this.BtnAltitudeHigher);
            this.SplitContainer.Panel1.Controls.Add(this.BtnAltitudeHighest);
            this.SplitContainer.Panel1.Controls.Add(this.LblModeDesc);
            this.SplitContainer.Panel1.Controls.Add(this.BtnZoomOut);
            this.SplitContainer.Panel1.Controls.Add(this.BtnZoomIn);
            this.SplitContainer.Panel1.Controls.Add(this.BtnMapFocus);
            this.SplitContainer.Panel1.Controls.Add(this.LblMode);
            this.SplitContainer.Panel1.Controls.Add(this.RotationInfoPanel);
            this.SplitContainer.Panel1.Controls.Add(this.DroneInfoPanel);
            this.SplitContainer.Panel1.Controls.Add(this.BtnRTL);
            this.SplitContainer.Panel1.Controls.Add(this.BtnLand);
            this.SplitContainer.Panel1.Controls.Add(this.BtnVideo);
            this.SplitContainer.Panel1.Controls.Add(this.BtnAuto);
            this.SplitContainer.Panel1.Controls.Add(this.BtnArm);
            this.SplitContainer.Panel1.Controls.Add(this.BtnReadWPs);
            this.SplitContainer.Panel1.Controls.Add(this.BtnTakeOff);
            this.SplitContainer.Panel1.Controls.Add(this.BtnWriteWPs);
            this.SplitContainer.Panel1.Controls.Add(this.TSMainPanel);
            this.SplitContainer.Panel1.Controls.Add(this.Map);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.DGVWayPoints);
            this.SplitContainer.Panel2.Controls.Add(this.ActiveDroneInfoPanel);
            // 
            // BtnAltitudeLower
            // 
            this.BtnAltitudeLower.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnAltitudeLower.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnAltitudeLower.ClickImage = null;
            resources.ApplyResources(this.BtnAltitudeLower, "BtnAltitudeLower");
            this.BtnAltitudeLower.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnAltitudeLower.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnAltitudeLower.HoverImage = null;
            this.BtnAltitudeLower.Name = "BtnAltitudeLower";
            this.BtnAltitudeLower.UseVisualStyleBackColor = true;
            this.BtnAltitudeLower.Click += new System.EventHandler(this.BtnAltitudeLower_Click);
            this.BtnAltitudeLower.MouseHover += new System.EventHandler(this.AltitudeButtons_MouseHover);
            // 
            // BtnAltitudeHigher
            // 
            this.BtnAltitudeHigher.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHigher.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHigher.ClickImage = null;
            resources.ApplyResources(this.BtnAltitudeHigher, "BtnAltitudeHigher");
            this.BtnAltitudeHigher.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHigher.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHigher.HoverImage = null;
            this.BtnAltitudeHigher.Name = "BtnAltitudeHigher";
            this.BtnAltitudeHigher.UseVisualStyleBackColor = true;
            this.BtnAltitudeHigher.Click += new System.EventHandler(this.BtnAltitudeHigher_Click);
            this.BtnAltitudeHigher.MouseHover += new System.EventHandler(this.AltitudeButtons_MouseHover);
            // 
            // BtnAltitudeHighest
            // 
            this.BtnAltitudeHighest.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHighest.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHighest.ClickImage = null;
            resources.ApplyResources(this.BtnAltitudeHighest, "BtnAltitudeHighest");
            this.BtnAltitudeHighest.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHighest.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnAltitudeHighest.HoverImage = null;
            this.BtnAltitudeHighest.Name = "BtnAltitudeHighest";
            this.BtnAltitudeHighest.UseVisualStyleBackColor = true;
            this.BtnAltitudeHighest.Click += new System.EventHandler(this.BtnAltitudeHighest_Click);
            this.BtnAltitudeHighest.MouseHover += new System.EventHandler(this.AltitudeButtons_MouseHover);
            // 
            // LblModeDesc
            // 
            resources.ApplyResources(this.LblModeDesc, "LblModeDesc");
            this.LblModeDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.LblModeDesc.ForeColor = System.Drawing.Color.White;
            this.LblModeDesc.Name = "LblModeDesc";
            // 
            // BtnZoomOut
            // 
            this.BtnZoomOut.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.ClickImage = null;
            resources.ApplyResources(this.BtnZoomOut, "BtnZoomOut");
            this.BtnZoomOut.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.HoverImage = global::Diva.Properties.Resources.icon_zoom_out_active;
            this.BtnZoomOut.Image = global::Diva.Properties.Resources.icon_zoom_out;
            this.BtnZoomOut.Name = "BtnZoomOut";
            this.BtnZoomOut.UseVisualStyleBackColor = true;
            this.BtnZoomOut.Click += new System.EventHandler(this.But_ZoomOut_Click);
            // 
            // BtnZoomIn
            // 
            this.BtnZoomIn.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.ClickImage = null;
            resources.ApplyResources(this.BtnZoomIn, "BtnZoomIn");
            this.BtnZoomIn.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.HoverImage = global::Diva.Properties.Resources.icon_zoom_in_active;
            this.BtnZoomIn.Image = global::Diva.Properties.Resources.icon_zoom_in;
            this.BtnZoomIn.Name = "BtnZoomIn";
            this.BtnZoomIn.UseVisualStyleBackColor = true;
            this.BtnZoomIn.Click += new System.EventHandler(this.But_ZoomIn_Click);
            // 
            // BtnMapFocus
            // 
            this.BtnMapFocus.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.ClickImage = null;
            resources.ApplyResources(this.BtnMapFocus, "BtnMapFocus");
            this.BtnMapFocus.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.HoverImage = global::Diva.Properties.Resources.icon_zoom_focus_active;
            this.BtnMapFocus.Image = global::Diva.Properties.Resources.icon_zoom_focus;
            this.BtnMapFocus.Name = "BtnMapFocus";
            this.BtnMapFocus.UseVisualStyleBackColor = true;
            this.BtnMapFocus.Click += new System.EventHandler(this.But_MapFocus_Click);
            // 
            // LblMode
            // 
            resources.ApplyResources(this.LblMode, "LblMode");
            this.LblMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.LblMode.ForeColor = System.Drawing.Color.White;
            this.LblMode.Name = "LblMode";
            // 
            // RotationInfoPanel
            // 
            resources.ApplyResources(this.RotationInfoPanel, "RotationInfoPanel");
            this.RotationInfoPanel.Name = "RotationInfoPanel";
            this.RotationInfoPanel.SizeChanged += new System.EventHandler(this.RotationInfoPanel_SizeChanged);
            // 
            // DroneInfoPanel
            // 
            this.DroneInfoPanel.ActiveDroneInfo = null;
            resources.ApplyResources(this.DroneInfoPanel, "DroneInfoPanel");
            this.DroneInfoPanel.ForeColor = System.Drawing.Color.White;
            this.DroneInfoPanel.Name = "DroneInfoPanel";
            this.DroneInfoPanel.ActiveDroneChanged += new System.EventHandler(this.DroneInfoPanel_ActiveDroneChanged);
            this.DroneInfoPanel.DroneClosed += new System.EventHandler(this.DroneInfoPanel_DroneClosed);
            // 
            // BtnRTL
            // 
            this.BtnRTL.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnRTL.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnRTL.ClickImage = null;
            this.BtnRTL.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnRTL, "BtnRTL");
            this.BtnRTL.ForeColor = System.Drawing.Color.White;
            this.BtnRTL.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnRTL.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnRTL.HoverImage = global::Diva.Properties.Resources.icon_left_home_active;
            this.BtnRTL.Image = global::Diva.Properties.Resources.icon_left_home;
            this.BtnRTL.Name = "BtnRTL";
            this.BtnRTL.UseVisualStyleBackColor = false;
            this.BtnRTL.Click += new System.EventHandler(this.BUT_RTL_Click);
            // 
            // BtnLand
            // 
            this.BtnLand.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnLand.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnLand.ClickImage = null;
            this.BtnLand.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnLand, "BtnLand");
            this.BtnLand.ForeColor = System.Drawing.Color.White;
            this.BtnLand.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnLand.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnLand.HoverImage = global::Diva.Properties.Resources.icon_left_landing_active;
            this.BtnLand.Image = global::Diva.Properties.Resources.icon_left_landing;
            this.BtnLand.Name = "BtnLand";
            this.BtnLand.UseVisualStyleBackColor = false;
            this.BtnLand.Click += new System.EventHandler(this.BUT_Land_Click);
            // 
            // BtnVideo
            // 
            this.BtnVideo.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnVideo.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnVideo.ClickImage = null;
            this.BtnVideo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnVideo, "BtnVideo");
            this.BtnVideo.ForeColor = System.Drawing.Color.White;
            this.BtnVideo.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnVideo.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnVideo.HoverImage = global::Diva.Properties.Resources.icon_left_video_active;
            this.BtnVideo.Image = global::Diva.Properties.Resources.icon_left_video;
            this.BtnVideo.Name = "BtnVideo";
            this.BtnVideo.UseVisualStyleBackColor = false;
            this.BtnVideo.Click += new System.EventHandler(this.VideoDemo_Click);
            // 
            // BtnAuto
            // 
            this.BtnAuto.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnAuto.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnAuto.ClickImage = null;
            this.BtnAuto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnAuto, "BtnAuto");
            this.BtnAuto.ForeColor = System.Drawing.Color.White;
            this.BtnAuto.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnAuto.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnAuto.HoverImage = global::Diva.Properties.Resources.icon_left_auto_active;
            this.BtnAuto.Image = global::Diva.Properties.Resources.icon_left_auto;
            this.BtnAuto.Name = "BtnAuto";
            this.BtnAuto.UseVisualStyleBackColor = false;
            this.BtnAuto.Click += new System.EventHandler(this.BUT_Auto_Click);
            // 
            // BtnArm
            // 
            this.BtnArm.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnArm.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnArm.ClickImage = null;
            this.BtnArm.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnArm, "BtnArm");
            this.BtnArm.ForeColor = System.Drawing.Color.White;
            this.BtnArm.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnArm.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnArm.HoverImage = global::Diva.Properties.Resources.icon_left_startup_active;
            this.BtnArm.Image = global::Diva.Properties.Resources.icon_left_startup;
            this.BtnArm.Name = "BtnArm";
            this.BtnArm.UseVisualStyleBackColor = false;
            this.BtnArm.Click += new System.EventHandler(this.BUT_Arm_Click);
            // 
            // BtnReadWPs
            // 
            this.BtnReadWPs.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.ClickImage = null;
            this.BtnReadWPs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnReadWPs, "BtnReadWPs");
            this.BtnReadWPs.ForeColor = System.Drawing.Color.White;
            this.BtnReadWPs.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnReadWPs.HoverImage = global::Diva.Properties.Resources.icon_left_output_active;
            this.BtnReadWPs.Image = global::Diva.Properties.Resources.icon_left_output;
            this.BtnReadWPs.Name = "BtnReadWPs";
            this.BtnReadWPs.UseVisualStyleBackColor = false;
            this.BtnReadWPs.Click += new System.EventHandler(this.BUT_read_Click);
            // 
            // BtnTakeOff
            // 
            this.BtnTakeOff.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.ClickImage = null;
            this.BtnTakeOff.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnTakeOff, "BtnTakeOff");
            this.BtnTakeOff.ForeColor = System.Drawing.Color.White;
            this.BtnTakeOff.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnTakeOff.HoverImage = global::Diva.Properties.Resources.icon_left_takeoff_active;
            this.BtnTakeOff.Image = global::Diva.Properties.Resources.icon_left_takeoff;
            this.BtnTakeOff.Name = "BtnTakeOff";
            this.BtnTakeOff.UseVisualStyleBackColor = false;
            this.BtnTakeOff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
            // 
            // BtnWriteWPs
            // 
            this.BtnWriteWPs.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.ClickImage = null;
            this.BtnWriteWPs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnWriteWPs, "BtnWriteWPs");
            this.BtnWriteWPs.ForeColor = System.Drawing.Color.White;
            this.BtnWriteWPs.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnWriteWPs.HoverImage = global::Diva.Properties.Resources.icon_left_input_active;
            this.BtnWriteWPs.Image = global::Diva.Properties.Resources.icon_left_input;
            this.BtnWriteWPs.Name = "BtnWriteWPs";
            this.BtnWriteWPs.UseVisualStyleBackColor = false;
            this.BtnWriteWPs.Click += new System.EventHandler(this.BUT_write_Click);
            // 
            // TSMainPanel
            // 
            this.TSMainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            resources.ApplyResources(this.TSMainPanel, "TSMainPanel");
            this.TSMainPanel.GripMargin = new System.Windows.Forms.Padding(0);
            this.TSMainPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TSMainPanel.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.TSMainPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSBtnConnect,
            this.TSBtnConfigure,
            this.Btn_Rotation,
            this.TSBtnTagging,
            this.TSBtnSaveMission,
            this.TSBtnReadMission,
            this.TSBtnCusOverlay});
            this.TSMainPanel.Name = "TSMainPanel";
            // 
            // TSBtnConnect
            // 
            resources.ApplyResources(this.TSBtnConnect, "TSBtnConnect");
            this.TSBtnConnect.CheckedBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.CheckedForeColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.CheckedImage = null;
            this.TSBtnConnect.CheckedText = null;
            this.TSBtnConnect.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.ClickImage = null;
            this.TSBtnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConnect.ForeColor = System.Drawing.Color.White;
            this.TSBtnConnect.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnConnect.HoverImage = global::Diva.Properties.Resources.icon_top_power_active;
            this.TSBtnConnect.Image = global::Diva.Properties.Resources.icon_top_power;
            this.TSBtnConnect.Margin = new System.Windows.Forms.Padding(0);
            this.TSBtnConnect.Name = "TSBtnConnect";
            this.TSBtnConnect.Click += new System.EventHandler(this.BUT_Connect_Click);
            // 
            // TSBtnConfigure
            // 
            resources.ApplyResources(this.TSBtnConfigure, "TSBtnConfigure");
            this.TSBtnConfigure.CheckedBackColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.CheckedForeColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.CheckedImage = null;
            this.TSBtnConfigure.CheckedText = null;
            this.TSBtnConfigure.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.ClickImage = null;
            this.TSBtnConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConfigure.ForeColor = System.Drawing.Color.White;
            this.TSBtnConfigure.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnConfigure.HoverImage = global::Diva.Properties.Resources.icon_top_setting_active;
            this.TSBtnConfigure.Image = global::Diva.Properties.Resources.icon_top_setting;
            this.TSBtnConfigure.Name = "TSBtnConfigure";
            this.TSBtnConfigure.Click += new System.EventHandler(this.BUT_Configure_Click);
            // 
            // Btn_Rotation
            // 
            resources.ApplyResources(this.Btn_Rotation, "Btn_Rotation");
            this.Btn_Rotation.CheckedBackColor = System.Drawing.Color.Empty;
            this.Btn_Rotation.CheckedForeColor = System.Drawing.Color.Empty;
            this.Btn_Rotation.CheckedImage = null;
            this.Btn_Rotation.CheckedText = null;
            this.Btn_Rotation.ClickBackColor = System.Drawing.Color.Empty;
            this.Btn_Rotation.ClickForeColor = System.Drawing.Color.Empty;
            this.Btn_Rotation.ClickImage = null;
            this.Btn_Rotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Rotation.ForeColor = System.Drawing.Color.White;
            this.Btn_Rotation.HoverBackColor = System.Drawing.Color.Empty;
            this.Btn_Rotation.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.Btn_Rotation.HoverImage = global::Diva.Properties.Resources.icon_top_patrol_active;
            this.Btn_Rotation.Image = global::Diva.Properties.Resources.icon_top_patrol;
            this.Btn_Rotation.Name = "Btn_Rotation";
            this.Btn_Rotation.Click += new System.EventHandler(this.Btn_Rotation_Click);
            // 
            // TSBtnTagging
            // 
            resources.ApplyResources(this.TSBtnTagging, "TSBtnTagging");
            this.TSBtnTagging.CheckedBackColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.CheckedForeColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.CheckedImage = null;
            this.TSBtnTagging.CheckedText = null;
            this.TSBtnTagging.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.ClickImage = null;
            this.TSBtnTagging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnTagging.ForeColor = System.Drawing.Color.White;
            this.TSBtnTagging.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnTagging.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnTagging.HoverImage = global::Diva.Properties.Resources.icon_top_mark_active;
            this.TSBtnTagging.Image = global::Diva.Properties.Resources.icon_top_mark;
            this.TSBtnTagging.Name = "TSBtnTagging";
            this.TSBtnTagging.Click += new System.EventHandler(this.BUT_Tagging_Click);
            // 
            // TSBtnSaveMission
            // 
            resources.ApplyResources(this.TSBtnSaveMission, "TSBtnSaveMission");
            this.TSBtnSaveMission.CheckedBackColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.CheckedForeColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.CheckedImage = null;
            this.TSBtnSaveMission.CheckedText = null;
            this.TSBtnSaveMission.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.ClickImage = null;
            this.TSBtnSaveMission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnSaveMission.ForeColor = System.Drawing.Color.White;
            this.TSBtnSaveMission.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnSaveMission.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnSaveMission.HoverImage = global::Diva.Properties.Resources.icon_top_save_active;
            this.TSBtnSaveMission.Image = global::Diva.Properties.Resources.icon_top_save;
            this.TSBtnSaveMission.Name = "TSBtnSaveMission";
            this.TSBtnSaveMission.Click += new System.EventHandler(this.BtnSaveMission_Click);
            // 
            // TSBtnReadMission
            // 
            resources.ApplyResources(this.TSBtnReadMission, "TSBtnReadMission");
            this.TSBtnReadMission.CheckedBackColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.CheckedForeColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.CheckedImage = null;
            this.TSBtnReadMission.CheckedText = null;
            this.TSBtnReadMission.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.ClickImage = null;
            this.TSBtnReadMission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnReadMission.ForeColor = System.Drawing.Color.White;
            this.TSBtnReadMission.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnReadMission.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnReadMission.HoverImage = global::Diva.Properties.Resources.icon_top_read_active;
            this.TSBtnReadMission.Image = global::Diva.Properties.Resources.icon_top_read;
            this.TSBtnReadMission.Name = "TSBtnReadMission";
            this.TSBtnReadMission.Click += new System.EventHandler(this.BtnReadMission_Click);
            // 
            // TSBtnCusOverlay
            // 
            resources.ApplyResources(this.TSBtnCusOverlay, "TSBtnCusOverlay");
            this.TSBtnCusOverlay.CheckedBackColor = System.Drawing.Color.Empty;
            this.TSBtnCusOverlay.CheckedForeColor = System.Drawing.Color.Empty;
            this.TSBtnCusOverlay.CheckedImage = null;
            this.TSBtnCusOverlay.CheckedText = null;
            this.TSBtnCusOverlay.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnCusOverlay.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnCusOverlay.ClickImage = null;
            this.TSBtnCusOverlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnCusOverlay.ForeColor = System.Drawing.Color.White;
            this.TSBtnCusOverlay.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnCusOverlay.HoverForeColor = System.Drawing.Color.Aqua;
            this.TSBtnCusOverlay.HoverImage = global::Diva.Properties.Resources.icon_top_custom_active;
            this.TSBtnCusOverlay.Image = global::Diva.Properties.Resources.icon_top_custom;
            this.TSBtnCusOverlay.Name = "TSBtnCusOverlay";
            this.TSBtnCusOverlay.Click += new System.EventHandler(this.LoadCustomizedOverlay_Click);
            // 
            // Map
            // 
            this.Map.BackColor = System.Drawing.SystemColors.Control;
            this.Map.Bearing = 0F;
            this.Map.CanDragMap = true;
            this.Map.ContextMenuStrip = this.cmMap;
            this.Map.DebugMode = true;
            resources.ApplyResources(this.Map, "Map");
            this.Map.EmptyTileColor = System.Drawing.Color.Navy;
            this.Map.GrayScaleMode = false;
            this.Map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.Map.LevelsKeepInMemmory = 5;
            this.Map.MarkersEnabled = true;
            this.Map.MaxZoom = 24;
            this.Map.MinZoom = 0;
            this.Map.MouseWheelZoomEnabled = true;
            this.Map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            this.Map.MsgWindowOffset = ((System.Drawing.PointF)(resources.GetObject("Map.MsgWindowOffset")));
            this.Map.Name = "Map";
            this.Map.NegativeMode = false;
            this.Map.PolygonsEnabled = true;
            this.Map.RetryLoadTile = 0;
            this.Map.RoutesEnabled = true;
            this.Map.ScaleFont = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.Map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.Map.ScalePosition = new System.Drawing.Point(10, -50);
            this.Map.ScaleSize = new System.Drawing.Size(20, 10);
            this.Map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.Map.ShowTileGridLines = false;
            this.Map.Zoom = 15D;
            this.Map.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.MainMap_OnMarkerClick);
            this.Map.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.MainMap_OnMarkerEnter);
            this.Map.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.MainMap_OnMarkerLeave);
            this.Map.OnPositionChanged += new GMap.NET.PositionChanged(this.MainMap_OnCurrentPositionChanged);
            this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseDown);
            this.Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseMove);
            this.Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseUp);
            // 
            // cmMap
            // 
            this.cmMap.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawPolygonToolStripMenuItem,
            this.noFlyZoneToolStripMenuItem,
            this.toolStripSeparator1,
            this.miClearMission,
            this.miSetHomeHere,
            this.miClearAllMissions});
            this.cmMap.Name = "contextMenuStrip1";
            resources.ApplyResources(this.cmMap, "cmMap");
            // 
            // drawPolygonToolStripMenuItem
            // 
            this.drawPolygonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPolygonPointToolStripMenuItem,
            this.clearPolygonToolStripMenuItem,
            this.savePolygonToolStripMenuItem,
            this.loadPolygonToolStripMenuItem,
            this.areaToolStripMenuItem});
            this.drawPolygonToolStripMenuItem.Name = "drawPolygonToolStripMenuItem";
            resources.ApplyResources(this.drawPolygonToolStripMenuItem, "drawPolygonToolStripMenuItem");
            // 
            // addPolygonPointToolStripMenuItem
            // 
            this.addPolygonPointToolStripMenuItem.Name = "addPolygonPointToolStripMenuItem";
            resources.ApplyResources(this.addPolygonPointToolStripMenuItem, "addPolygonPointToolStripMenuItem");
            this.addPolygonPointToolStripMenuItem.Click += new System.EventHandler(this.addPolygonPointToolStripMenuItem_Click);
            // 
            // clearPolygonToolStripMenuItem
            // 
            this.clearPolygonToolStripMenuItem.Name = "clearPolygonToolStripMenuItem";
            resources.ApplyResources(this.clearPolygonToolStripMenuItem, "clearPolygonToolStripMenuItem");
            this.clearPolygonToolStripMenuItem.Click += new System.EventHandler(this.clearPolygonToolStripMenuItem_Click);
            // 
            // savePolygonToolStripMenuItem
            // 
            this.savePolygonToolStripMenuItem.Name = "savePolygonToolStripMenuItem";
            resources.ApplyResources(this.savePolygonToolStripMenuItem, "savePolygonToolStripMenuItem");
            this.savePolygonToolStripMenuItem.Click += new System.EventHandler(this.savePolygonToolStripMenuItem_Click);
            // 
            // loadPolygonToolStripMenuItem
            // 
            this.loadPolygonToolStripMenuItem.Name = "loadPolygonToolStripMenuItem";
            resources.ApplyResources(this.loadPolygonToolStripMenuItem, "loadPolygonToolStripMenuItem");
            this.loadPolygonToolStripMenuItem.Click += new System.EventHandler(this.loadPolygonToolStripMenuItem_Click);
            // 
            // areaToolStripMenuItem
            // 
            this.areaToolStripMenuItem.Name = "areaToolStripMenuItem";
            resources.ApplyResources(this.areaToolStripMenuItem, "areaToolStripMenuItem");
            // 
            // noFlyZoneToolStripMenuItem
            // 
            this.noFlyZoneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem,
            this.setReturnLocationToolStripMenuItem,
            this.loadFromFileToolStripMenuItem,
            this.saveToFileToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.noFlyZoneToolStripMenuItem.Name = "noFlyZoneToolStripMenuItem";
            resources.ApplyResources(this.noFlyZoneToolStripMenuItem, "noFlyZoneToolStripMenuItem");
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            resources.ApplyResources(this.uploadToolStripMenuItem, "uploadToolStripMenuItem");
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.GeoFenceuploadToolStripMenuItem_Click);
            // 
            // setReturnLocationToolStripMenuItem
            // 
            this.setReturnLocationToolStripMenuItem.Name = "setReturnLocationToolStripMenuItem";
            resources.ApplyResources(this.setReturnLocationToolStripMenuItem, "setReturnLocationToolStripMenuItem");
            this.setReturnLocationToolStripMenuItem.Click += new System.EventHandler(this.setReturnLocationToolStripMenuItem_Click);
            // 
            // loadFromFileToolStripMenuItem
            // 
            this.loadFromFileToolStripMenuItem.Name = "loadFromFileToolStripMenuItem";
            resources.ApplyResources(this.loadFromFileToolStripMenuItem, "loadFromFileToolStripMenuItem");
            this.loadFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadFromFileToolStripMenuItem_Click);
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            resources.ApplyResources(this.saveToFileToolStripMenuItem, "saveToFileToolStripMenuItem");
            this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // miClearMission
            // 
            this.miClearMission.Name = "miClearMission";
            resources.ApplyResources(this.miClearMission, "miClearMission");
            this.miClearMission.Click += new System.EventHandler(this.goHereToolStripMenuItem_Click);
            // 
            // miSetHomeHere
            // 
            this.miSetHomeHere.Name = "miSetHomeHere";
            resources.ApplyResources(this.miSetHomeHere, "miSetHomeHere");
            this.miSetHomeHere.Click += new System.EventHandler(this.setHomeHereToolStripMenuItem_Click);
            // 
            // miClearAllMissions
            // 
            this.miClearAllMissions.Name = "miClearAllMissions";
            resources.ApplyResources(this.miClearAllMissions, "miClearAllMissions");
            this.miClearAllMissions.Click += new System.EventHandler(this.clearMissionToolStripMenuItem_Click);
            // 
            // DGVWayPoints
            // 
            this.DGVWayPoints.AllowUserToAddRows = false;
            this.DGVWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGVWayPoints.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGVWayPoints.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.DGVWayPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGVWayPoints.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DGVWayPoints.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVWayPoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.DGVWayPoints, "DGVWayPoints");
            this.DGVWayPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.DGVWayPoints.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVWayPoints.EnableHeadersVisualStyles = false;
            this.DGVWayPoints.GridColor = System.Drawing.SystemColors.InactiveCaption;
            this.DGVWayPoints.Name = "DGVWayPoints";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVWayPoints.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.DGVWayPoints.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DGVWayPoints.RowTemplate.Height = 24;
            this.DGVWayPoints.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_CellContentClick);
            this.DGVWayPoints.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_RowEnter);
            this.DGVWayPoints.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Commands_RowsAdded);
            this.DGVWayPoints.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Commands_RowValidating);
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
            // ActiveDroneInfoPanel
            // 
            resources.ApplyResources(this.ActiveDroneInfoPanel, "ActiveDroneInfoPanel");
            this.ActiveDroneInfoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.ActiveDroneInfoPanel.Controls.Add(this.ComBoxModeSwitch);
            this.ActiveDroneInfoPanel.Controls.Add(this.TxtHomeLongitude);
            this.ActiveDroneInfoPanel.Controls.Add(this.TxtHomeLatitude);
            this.ActiveDroneInfoPanel.Controls.Add(this.TxtHomeAltitude);
            this.ActiveDroneInfoPanel.Controls.Add(this.LabelLongitude);
            this.ActiveDroneInfoPanel.Controls.Add(this.LabelLatitude);
            this.ActiveDroneInfoPanel.Controls.Add(this.LabelAltitude);
            this.ActiveDroneInfoPanel.Controls.Add(this.BtnDroneMode);
            this.ActiveDroneInfoPanel.Controls.Add(this.BtnAltitude);
            this.ActiveDroneInfoPanel.Controls.Add(this.BtnHome);
            this.ActiveDroneInfoPanel.Controls.Add(this.TxtAltitudeValue);
            this.ActiveDroneInfoPanel.Name = "ActiveDroneInfoPanel";
            // 
            // ComBoxModeSwitch
            // 
            resources.ApplyResources(this.ComBoxModeSwitch, "ComBoxModeSwitch");
            this.ComBoxModeSwitch.FormattingEnabled = true;
            this.ComBoxModeSwitch.Name = "ComBoxModeSwitch";
            this.ComBoxModeSwitch.SelectedIndexChanged += new System.EventHandler(this.ComBoxModeSwitch_SelectedIndexChanged);
            // 
            // TxtHomeLongitude
            // 
            this.TxtHomeLongitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.TxtHomeLongitude, "TxtHomeLongitude");
            this.TxtHomeLongitude.Name = "TxtHomeLongitude";
            // 
            // TxtHomeLatitude
            // 
            this.TxtHomeLatitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.TxtHomeLatitude, "TxtHomeLatitude");
            this.TxtHomeLatitude.Name = "TxtHomeLatitude";
            // 
            // TxtHomeAltitude
            // 
            this.TxtHomeAltitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.TxtHomeAltitude, "TxtHomeAltitude");
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
            // BtnDroneMode
            // 
            resources.ApplyResources(this.BtnDroneMode, "BtnDroneMode");
            this.BtnDroneMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.BtnDroneMode.FlatAppearance.BorderSize = 0;
            this.BtnDroneMode.ForeColor = System.Drawing.Color.White;
            this.BtnDroneMode.Image = global::Diva.Properties.Resources.icon_airplane_32;
            this.BtnDroneMode.Name = "BtnDroneMode";
            this.BtnDroneMode.UseVisualStyleBackColor = false;
            // 
            // BtnAltitude
            // 
            this.BtnAltitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.BtnAltitude.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BtnAltitude, "BtnAltitude");
            this.BtnAltitude.ForeColor = System.Drawing.Color.White;
            this.BtnAltitude.Image = global::Diva.Properties.Resources.icon_edit_32;
            this.BtnAltitude.Name = "BtnAltitude";
            this.BtnAltitude.UseVisualStyleBackColor = false;
            // 
            // BtnHome
            // 
            resources.ApplyResources(this.BtnHome, "BtnHome");
            this.BtnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.BtnHome.FlatAppearance.BorderSize = 0;
            this.BtnHome.ForeColor = System.Drawing.Color.White;
            this.BtnHome.Image = global::Diva.Properties.Resources.icon_house_32;
            this.BtnHome.Name = "BtnHome";
            this.BtnHome.UseVisualStyleBackColor = false;
            // 
            // TxtAltitudeValue
            // 
            this.TxtAltitudeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.TxtAltitudeValue, "TxtAltitudeValue");
            this.TxtAltitudeValue.Name = "TxtAltitudeValue";
            // 
            // Planner
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.SplitContainer);
            this.Name = "Planner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Planner_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Planner_FormClosed);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.TSMainPanel.ResumeLayout(false);
            this.TSMainPanel.PerformLayout();
            this.cmMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVWayPoints)).EndInit();
            this.ActiveDroneInfoPanel.ResumeLayout(false);
            this.ActiveDroneInfoPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel ActiveDroneInfoPanel;
		private System.Windows.Forms.DataGridView DGVWayPoints;
		private System.Windows.Forms.TextBox TxtAltitudeValue;
		private System.Windows.Forms.ContextMenuStrip cmMap;
		private System.Windows.Forms.ToolStripMenuItem miClearMission;
		private System.Windows.Forms.ToolStripMenuItem miSetHomeHere;
		private System.Windows.Forms.ToolStripMenuItem miClearAllMissions;
		private System.Windows.Forms.ToolStrip TSMainPanel;
		private MyTSButton TSBtnConnect;
		private MyTSButton Btn_Rotation;
		private MyTSButton TSBtnConfigure;
		private MyGMap Map;
		private MyButton BtnArm;
		private MyButton BtnReadWPs;
		private MyButton BtnWriteWPs;
		private MyButton BtnAuto;
		private MyButton BtnTakeOff;
		private MyButton BtnLand;
		private MyTSButton TSBtnTagging;
        private MyButton BtnVideo;
        private SplitContainer SplitContainer;
		private Button BtnHome;
		private MyButton BtnRTL;
		private Button BtnAltitude;
		private Button BtnDroneMode;
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
		private ToolStripMenuItem setReturnLocationToolStripMenuItem;
		private ToolStripMenuItem loadFromFileToolStripMenuItem;
		private ToolStripMenuItem saveToFileToolStripMenuItem;
		private ToolStripMenuItem clearToolStripMenuItem;
		private MyTSButton TSBtnCusOverlay;
        private DroneInfoPanel DroneInfoPanel;
		private FlowLayoutPanel RotationInfoPanel;
		private Label LblMode;
		private ComboBox ComBoxModeSwitch;
		private MyButton BtnZoomOut;
		private MyButton BtnZoomIn;
		private MyButton BtnMapFocus;
		private Label LblModeDesc;
        private MyButton BtnAltitudeLower;
        private MyButton BtnAltitudeHigher;
        private MyButton BtnAltitudeHighest;
    }
}