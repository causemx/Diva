using System.Windows.Forms;
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
            this.IconGPSLostWarning = new System.Windows.Forms.Button();
            this.IconModeWarning = new System.Windows.Forms.Button();
            this.BtnBreakAction = new Diva.Controls.Components.MyButton();
            this.AltitudeControlPanel = new Diva.Controls.AltitudeControlPanel();
            this.LblModeDesc = new System.Windows.Forms.Label();
            this.BtnZoomOut = new Diva.Controls.Components.MyButton();
            this.BtnZoomIn = new Diva.Controls.Components.MyButton();
            this.BtnMapFocus = new Diva.Controls.Components.MyButton();
            this.LblMode = new System.Windows.Forms.Label();
            this.RotationInfoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DroneInfoPanel = new Diva.Controls.DroneInfoPanel();
            this.BtnRTL = new Diva.Controls.Components.MyButton();
            this.BtnLand = new Diva.Controls.Components.MyButton();
            this.BtnAuto = new Diva.Controls.Components.MyButton();
            this.BtnArm = new Diva.Controls.Components.MyButton();
            this.BtnReadWPs = new Diva.Controls.Components.MyButton();
            this.BtnTakeOff = new Diva.Controls.Components.MyButton();
            this.BtnWriteWPs = new Diva.Controls.Components.MyButton();
            this.TSMainPanel = new System.Windows.Forms.ToolStrip();
            this.TSBtnConnect = new Diva.Controls.Components.MyTSButton();
            this.TSBtnConfigure = new Diva.Controls.Components.MyTSButton();
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
            this.TTButtonDescription = new System.Windows.Forms.ToolTip(this.components);
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
            resources.ApplyResources(this.SplitContainer.Panel1, "SplitContainer.Panel1");
            this.SplitContainer.Panel1.Controls.Add(this.IconGPSLostWarning);
            this.SplitContainer.Panel1.Controls.Add(this.IconModeWarning);
            this.SplitContainer.Panel1.Controls.Add(this.BtnBreakAction);
            this.SplitContainer.Panel1.Controls.Add(this.AltitudeControlPanel);
            this.SplitContainer.Panel1.Controls.Add(this.LblModeDesc);
            this.SplitContainer.Panel1.Controls.Add(this.BtnZoomOut);
            this.SplitContainer.Panel1.Controls.Add(this.BtnZoomIn);
            this.SplitContainer.Panel1.Controls.Add(this.BtnMapFocus);
            this.SplitContainer.Panel1.Controls.Add(this.LblMode);
            this.SplitContainer.Panel1.Controls.Add(this.RotationInfoPanel);
            this.SplitContainer.Panel1.Controls.Add(this.DroneInfoPanel);
            this.SplitContainer.Panel1.Controls.Add(this.BtnRTL);
            this.SplitContainer.Panel1.Controls.Add(this.BtnLand);
            this.SplitContainer.Panel1.Controls.Add(this.BtnAuto);
            this.SplitContainer.Panel1.Controls.Add(this.BtnArm);
            this.SplitContainer.Panel1.Controls.Add(this.BtnReadWPs);
            this.SplitContainer.Panel1.Controls.Add(this.BtnTakeOff);
            this.SplitContainer.Panel1.Controls.Add(this.BtnWriteWPs);
            this.SplitContainer.Panel1.Controls.Add(this.TSMainPanel);
            this.SplitContainer.Panel1.Controls.Add(this.Map);
            this.TTButtonDescription.SetToolTip(this.SplitContainer.Panel1, resources.GetString("SplitContainer.Panel1.ToolTip"));
            // 
            // SplitContainer.Panel2
            // 
            resources.ApplyResources(this.SplitContainer.Panel2, "SplitContainer.Panel2");
            this.SplitContainer.Panel2.Controls.Add(this.DGVWayPoints);
            this.SplitContainer.Panel2.Controls.Add(this.ActiveDroneInfoPanel);
            this.TTButtonDescription.SetToolTip(this.SplitContainer.Panel2, resources.GetString("SplitContainer.Panel2.ToolTip"));
            this.TTButtonDescription.SetToolTip(this.SplitContainer, resources.GetString("SplitContainer.ToolTip"));
            // 
            // IconGPSLostWarning
            // 
            resources.ApplyResources(this.IconGPSLostWarning, "IconGPSLostWarning");
            this.IconGPSLostWarning.BackColor = System.Drawing.Color.Transparent;
            this.IconGPSLostWarning.BackgroundImage = global::Diva.Properties.Resources.waiting_gps;
            this.IconGPSLostWarning.FlatAppearance.BorderSize = 0;
            this.IconGPSLostWarning.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.IconGPSLostWarning.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.IconGPSLostWarning.ForeColor = System.Drawing.Color.Gold;
            this.IconGPSLostWarning.Name = "IconGPSLostWarning";
            this.TTButtonDescription.SetToolTip(this.IconGPSLostWarning, resources.GetString("IconGPSLostWarning.ToolTip"));
            this.IconGPSLostWarning.UseVisualStyleBackColor = false;
            // 
            // IconModeWarning
            // 
            resources.ApplyResources(this.IconModeWarning, "IconModeWarning");
            this.IconModeWarning.BackColor = System.Drawing.Color.Transparent;
            this.IconModeWarning.FlatAppearance.BorderSize = 0;
            this.IconModeWarning.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.IconModeWarning.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.IconModeWarning.ForeColor = System.Drawing.Color.Red;
            this.IconModeWarning.Name = "IconModeWarning";
            this.TTButtonDescription.SetToolTip(this.IconModeWarning, resources.GetString("IconModeWarning.ToolTip"));
            this.IconModeWarning.UseVisualStyleBackColor = false;
            // 
            // BtnBreakAction
            // 
            resources.ApplyResources(this.BtnBreakAction, "BtnBreakAction");
            this.BtnBreakAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BtnBreakAction.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnBreakAction.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnBreakAction.ClickImage = null;
            this.BtnBreakAction.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.BtnBreakAction.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnBreakAction.HoverImage = null;
            this.BtnBreakAction.Image = global::Diva.Properties.Resources.red_octagon_with_hand;
            this.BtnBreakAction.Name = "BtnBreakAction";
            this.TTButtonDescription.SetToolTip(this.BtnBreakAction, resources.GetString("BtnBreakAction.ToolTip"));
            this.BtnBreakAction.UseVisualStyleBackColor = false;
            this.BtnBreakAction.Click += new System.EventHandler(this.BtnBreakAction_Click);
            // 
            // AltitudeControlPanel
            // 
            this.AltitudeControlPanel.AboveColor = System.Drawing.Color.SkyBlue;
            resources.ApplyResources(this.AltitudeControlPanel, "AltitudeControlPanel");
            this.AltitudeControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.AltitudeControlPanel.BelowColor = System.Drawing.Color.Navy;
            this.AltitudeControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AltitudeControlPanel.ForeColor = System.Drawing.Color.White;
            this.AltitudeControlPanel.HoverGrow = System.Drawing.ContentAlignment.TopRight;
            this.AltitudeControlPanel.HoverSize = new System.Drawing.Size(60, 200);
            this.AltitudeControlPanel.Maximum = 120F;
            this.AltitudeControlPanel.Minimum = 10F;
            this.AltitudeControlPanel.Name = "AltitudeControlPanel";
            this.AltitudeControlPanel.PointingColor = System.Drawing.Color.Red;
            this.AltitudeControlPanel.Target = 0F;
            this.AltitudeControlPanel.TargetColor = System.Drawing.Color.Yellow;
            this.AltitudeControlPanel.Targeting = false;
            this.TTButtonDescription.SetToolTip(this.AltitudeControlPanel, resources.GetString("AltitudeControlPanel.ToolTip"));
            this.AltitudeControlPanel.Value = 0F;
            this.AltitudeControlPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AltitudeControlPanel_MouseClick);
            // 
            // LblModeDesc
            // 
            resources.ApplyResources(this.LblModeDesc, "LblModeDesc");
            this.LblModeDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.LblModeDesc.ForeColor = System.Drawing.Color.White;
            this.LblModeDesc.Name = "LblModeDesc";
            this.TTButtonDescription.SetToolTip(this.LblModeDesc, resources.GetString("LblModeDesc.ToolTip"));
            // 
            // BtnZoomOut
            // 
            resources.ApplyResources(this.BtnZoomOut, "BtnZoomOut");
            this.BtnZoomOut.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.ClickImage = null;
            this.BtnZoomOut.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.HoverImage = global::Diva.Properties.Resources.icon_zoom_out_active;
            this.BtnZoomOut.Image = global::Diva.Properties.Resources.icon_zoom_out;
            this.BtnZoomOut.Name = "BtnZoomOut";
            this.TTButtonDescription.SetToolTip(this.BtnZoomOut, resources.GetString("BtnZoomOut.ToolTip"));
            this.BtnZoomOut.UseVisualStyleBackColor = true;
            this.BtnZoomOut.Click += new System.EventHandler(this.But_ZoomOut_Click);
            // 
            // BtnZoomIn
            // 
            resources.ApplyResources(this.BtnZoomIn, "BtnZoomIn");
            this.BtnZoomIn.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.ClickImage = null;
            this.BtnZoomIn.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.HoverImage = global::Diva.Properties.Resources.icon_zoom_in_active;
            this.BtnZoomIn.Image = global::Diva.Properties.Resources.icon_zoom_in;
            this.BtnZoomIn.Name = "BtnZoomIn";
            this.TTButtonDescription.SetToolTip(this.BtnZoomIn, resources.GetString("BtnZoomIn.ToolTip"));
            this.BtnZoomIn.UseVisualStyleBackColor = true;
            this.BtnZoomIn.Click += new System.EventHandler(this.But_ZoomIn_Click);
            // 
            // BtnMapFocus
            // 
            resources.ApplyResources(this.BtnMapFocus, "BtnMapFocus");
            this.BtnMapFocus.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.ClickImage = null;
            this.BtnMapFocus.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.HoverImage = global::Diva.Properties.Resources.icon_zoom_focus_active;
            this.BtnMapFocus.Image = global::Diva.Properties.Resources.icon_zoom_focus;
            this.BtnMapFocus.Name = "BtnMapFocus";
            this.TTButtonDescription.SetToolTip(this.BtnMapFocus, resources.GetString("BtnMapFocus.ToolTip"));
            this.BtnMapFocus.UseVisualStyleBackColor = true;
            this.BtnMapFocus.Click += new System.EventHandler(this.But_MapFocus_Click);
            this.BtnMapFocus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnMapFocus_MouseUp);
            // 
            // LblMode
            // 
            resources.ApplyResources(this.LblMode, "LblMode");
            this.LblMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.LblMode.ForeColor = System.Drawing.Color.White;
            this.LblMode.Name = "LblMode";
            this.TTButtonDescription.SetToolTip(this.LblMode, resources.GetString("LblMode.ToolTip"));
            // 
            // RotationInfoPanel
            // 
            resources.ApplyResources(this.RotationInfoPanel, "RotationInfoPanel");
            this.RotationInfoPanel.Name = "RotationInfoPanel";
            this.TTButtonDescription.SetToolTip(this.RotationInfoPanel, resources.GetString("RotationInfoPanel.ToolTip"));
            this.RotationInfoPanel.SizeChanged += new System.EventHandler(this.RotationInfoPanel_SizeChanged);
            // 
            // DroneInfoPanel
            // 
            resources.ApplyResources(this.DroneInfoPanel, "DroneInfoPanel");
            this.DroneInfoPanel.ActiveDroneInfo = null;
            this.DroneInfoPanel.ForeColor = System.Drawing.Color.White;
            this.DroneInfoPanel.Name = "DroneInfoPanel";
            this.TTButtonDescription.SetToolTip(this.DroneInfoPanel, resources.GetString("DroneInfoPanel.ToolTip"));
            this.DroneInfoPanel.ActiveDroneChanged += new System.EventHandler(this.DroneInfoPanel_ActiveDroneChanged);
            this.DroneInfoPanel.DroneClosed += new System.EventHandler(this.DroneInfoPanel_DroneClosed);
            // 
            // BtnRTL
            // 
            resources.ApplyResources(this.BtnRTL, "BtnRTL");
            this.BtnRTL.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnRTL.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnRTL.ClickImage = null;
            this.BtnRTL.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnRTL.ForeColor = System.Drawing.Color.White;
            this.BtnRTL.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnRTL.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnRTL.HoverImage = global::Diva.Properties.Resources.icon_left_home_active;
            this.BtnRTL.Image = global::Diva.Properties.Resources.icon_left_home;
            this.BtnRTL.Name = "BtnRTL";
            this.TTButtonDescription.SetToolTip(this.BtnRTL, resources.GetString("BtnRTL.ToolTip"));
            this.BtnRTL.UseVisualStyleBackColor = false;
            this.BtnRTL.Click += new System.EventHandler(this.BUT_RTL_Click);
            // 
            // BtnLand
            // 
            resources.ApplyResources(this.BtnLand, "BtnLand");
            this.BtnLand.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnLand.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnLand.ClickImage = null;
            this.BtnLand.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnLand.ForeColor = System.Drawing.Color.White;
            this.BtnLand.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnLand.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnLand.HoverImage = global::Diva.Properties.Resources.icon_left_landing_active;
            this.BtnLand.Image = global::Diva.Properties.Resources.icon_left_landing;
            this.BtnLand.Name = "BtnLand";
            this.TTButtonDescription.SetToolTip(this.BtnLand, resources.GetString("BtnLand.ToolTip"));
            this.BtnLand.UseVisualStyleBackColor = false;
            this.BtnLand.Click += new System.EventHandler(this.BUT_Land_Click);
            // 
            // BtnAuto
            // 
            resources.ApplyResources(this.BtnAuto, "BtnAuto");
            this.BtnAuto.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnAuto.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnAuto.ClickImage = null;
            this.BtnAuto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnAuto.ForeColor = System.Drawing.Color.White;
            this.BtnAuto.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnAuto.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnAuto.HoverImage = global::Diva.Properties.Resources.icon_left_auto_active;
            this.BtnAuto.Image = global::Diva.Properties.Resources.icon_left_auto;
            this.BtnAuto.Name = "BtnAuto";
            this.TTButtonDescription.SetToolTip(this.BtnAuto, resources.GetString("BtnAuto.ToolTip"));
            this.BtnAuto.UseVisualStyleBackColor = false;
            this.BtnAuto.Click += new System.EventHandler(this.BUT_Auto_Click);
            // 
            // BtnArm
            // 
            resources.ApplyResources(this.BtnArm, "BtnArm");
            this.BtnArm.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnArm.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnArm.ClickImage = null;
            this.BtnArm.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnArm.ForeColor = System.Drawing.Color.White;
            this.BtnArm.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnArm.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnArm.HoverImage = global::Diva.Properties.Resources.icon_left_startup_active;
            this.BtnArm.Image = global::Diva.Properties.Resources.icon_left_startup;
            this.BtnArm.Name = "BtnArm";
            this.TTButtonDescription.SetToolTip(this.BtnArm, resources.GetString("BtnArm.ToolTip"));
            this.BtnArm.UseVisualStyleBackColor = false;
            this.BtnArm.Click += new System.EventHandler(this.BUT_Arm_Click);
            // 
            // BtnReadWPs
            // 
            resources.ApplyResources(this.BtnReadWPs, "BtnReadWPs");
            this.BtnReadWPs.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.ClickImage = null;
            this.BtnReadWPs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnReadWPs.ForeColor = System.Drawing.Color.White;
            this.BtnReadWPs.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnReadWPs.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnReadWPs.HoverImage = global::Diva.Properties.Resources.icon_left_output_active;
            this.BtnReadWPs.Image = global::Diva.Properties.Resources.icon_left_output;
            this.BtnReadWPs.Name = "BtnReadWPs";
            this.TTButtonDescription.SetToolTip(this.BtnReadWPs, resources.GetString("BtnReadWPs.ToolTip"));
            this.BtnReadWPs.UseVisualStyleBackColor = false;
            this.BtnReadWPs.Click += new System.EventHandler(this.BUT_read_Click);
            // 
            // BtnTakeOff
            // 
            resources.ApplyResources(this.BtnTakeOff, "BtnTakeOff");
            this.BtnTakeOff.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.ClickImage = null;
            this.BtnTakeOff.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnTakeOff.ForeColor = System.Drawing.Color.White;
            this.BtnTakeOff.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnTakeOff.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnTakeOff.HoverImage = global::Diva.Properties.Resources.icon_left_takeoff_active;
            this.BtnTakeOff.Image = global::Diva.Properties.Resources.icon_left_takeoff;
            this.BtnTakeOff.Name = "BtnTakeOff";
            this.TTButtonDescription.SetToolTip(this.BtnTakeOff, resources.GetString("BtnTakeOff.ToolTip"));
            this.BtnTakeOff.UseVisualStyleBackColor = false;
            this.BtnTakeOff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
            // 
            // BtnWriteWPs
            // 
            resources.ApplyResources(this.BtnWriteWPs, "BtnWriteWPs");
            this.BtnWriteWPs.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.ClickImage = null;
            this.BtnWriteWPs.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnWriteWPs.ForeColor = System.Drawing.Color.White;
            this.BtnWriteWPs.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnWriteWPs.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.BtnWriteWPs.HoverImage = global::Diva.Properties.Resources.icon_left_input_active;
            this.BtnWriteWPs.Image = global::Diva.Properties.Resources.icon_left_input;
            this.BtnWriteWPs.Name = "BtnWriteWPs";
            this.TTButtonDescription.SetToolTip(this.BtnWriteWPs, resources.GetString("BtnWriteWPs.ToolTip"));
            this.BtnWriteWPs.UseVisualStyleBackColor = false;
            this.BtnWriteWPs.Click += new System.EventHandler(this.BUT_write_Click);
            // 
            // TSMainPanel
            // 
            resources.ApplyResources(this.TSMainPanel, "TSMainPanel");
            this.TSMainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.TSMainPanel.GripMargin = new System.Windows.Forms.Padding(0);
            this.TSMainPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TSMainPanel.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.TSMainPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSBtnConnect,
            this.TSBtnConfigure});
            this.TSMainPanel.Name = "TSMainPanel";
            this.TTButtonDescription.SetToolTip(this.TSMainPanel, resources.GetString("TSMainPanel.ToolTip"));
            // 
            // TSBtnConnect
            // 
            resources.ApplyResources(this.TSBtnConnect, "TSBtnConnect");
            this.TSBtnConnect.CheckedBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.CheckedForeColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.CheckedImage = global::Diva.Properties.Resources.top_link;
            this.TSBtnConnect.CheckOnClick = true;
            this.TSBtnConnect.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.ClickImage = null;
            this.TSBtnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConnect.ForeColor = System.Drawing.Color.White;
            this.TSBtnConnect.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnConnect.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnConnect.HoverImage = null;
            this.TSBtnConnect.Image = global::Diva.Properties.Resources.top_unlink;
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
            this.TSBtnConfigure.ClickBackColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.ClickForeColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.ClickImage = null;
            this.TSBtnConfigure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSBtnConfigure.ForeColor = System.Drawing.Color.White;
            this.TSBtnConfigure.HoverBackColor = System.Drawing.Color.Empty;
            this.TSBtnConfigure.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.TSBtnConfigure.HoverImage = null;
            this.TSBtnConfigure.Image = global::Diva.Properties.Resources.icon_top_setting;
            this.TSBtnConfigure.Name = "TSBtnConfigure";
            this.TSBtnConfigure.Click += new System.EventHandler(this.BUT_Configure_Click);
            // 
            // Map
            // 
            resources.ApplyResources(this.Map, "Map");
            this.Map.BackColor = System.Drawing.SystemColors.Control;
            this.Map.Bearing = 0F;
            this.Map.CanDragMap = true;
            this.Map.ContextMenuStrip = this.cmMap;
            this.Map.DebugMode = true;
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
            this.Map.ScaleSize = new System.Drawing.Size(100, 10);
            this.Map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.Map.ShowTileGridLines = false;
            this.TTButtonDescription.SetToolTip(this.Map, resources.GetString("Map.ToolTip"));
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
            this.TTButtonDescription.SetToolTip(this.cmMap, resources.GetString("cmMap.ToolTip"));
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
            // DGVWayPoints
            // 
            resources.ApplyResources(this.DGVWayPoints, "DGVWayPoints");
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
            this.TTButtonDescription.SetToolTip(this.DGVWayPoints, resources.GetString("DGVWayPoints.ToolTip"));
            this.DGVWayPoints.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_CellContentClick);
            this.DGVWayPoints.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DGVWayPoints_DataError);
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
            this.TTButtonDescription.SetToolTip(this.ActiveDroneInfoPanel, resources.GetString("ActiveDroneInfoPanel.ToolTip"));
            // 
            // ComBoxModeSwitch
            // 
            resources.ApplyResources(this.ComBoxModeSwitch, "ComBoxModeSwitch");
            this.ComBoxModeSwitch.FormattingEnabled = true;
            this.ComBoxModeSwitch.Name = "ComBoxModeSwitch";
            this.TTButtonDescription.SetToolTip(this.ComBoxModeSwitch, resources.GetString("ComBoxModeSwitch.ToolTip"));
            this.ComBoxModeSwitch.SelectedIndexChanged += new System.EventHandler(this.ComBoxModeSwitch_SelectedIndexChanged);
            this.ComBoxModeSwitch.SelectionChangeCommitted += new System.EventHandler(this.ComBoxModeSwitch_SelectionChangeCommitted);
            // 
            // TxtHomeLongitude
            // 
            resources.ApplyResources(this.TxtHomeLongitude, "TxtHomeLongitude");
            this.TxtHomeLongitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHomeLongitude.Name = "TxtHomeLongitude";
            this.TTButtonDescription.SetToolTip(this.TxtHomeLongitude, resources.GetString("TxtHomeLongitude.ToolTip"));
            // 
            // TxtHomeLatitude
            // 
            resources.ApplyResources(this.TxtHomeLatitude, "TxtHomeLatitude");
            this.TxtHomeLatitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHomeLatitude.Name = "TxtHomeLatitude";
            this.TTButtonDescription.SetToolTip(this.TxtHomeLatitude, resources.GetString("TxtHomeLatitude.ToolTip"));
            // 
            // TxtHomeAltitude
            // 
            resources.ApplyResources(this.TxtHomeAltitude, "TxtHomeAltitude");
            this.TxtHomeAltitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHomeAltitude.Name = "TxtHomeAltitude";
            this.TTButtonDescription.SetToolTip(this.TxtHomeAltitude, resources.GetString("TxtHomeAltitude.ToolTip"));
            // 
            // LabelLongitude
            // 
            resources.ApplyResources(this.LabelLongitude, "LabelLongitude");
            this.LabelLongitude.ForeColor = System.Drawing.Color.White;
            this.LabelLongitude.Name = "LabelLongitude";
            this.TTButtonDescription.SetToolTip(this.LabelLongitude, resources.GetString("LabelLongitude.ToolTip"));
            // 
            // LabelLatitude
            // 
            resources.ApplyResources(this.LabelLatitude, "LabelLatitude");
            this.LabelLatitude.ForeColor = System.Drawing.Color.White;
            this.LabelLatitude.Name = "LabelLatitude";
            this.TTButtonDescription.SetToolTip(this.LabelLatitude, resources.GetString("LabelLatitude.ToolTip"));
            // 
            // LabelAltitude
            // 
            resources.ApplyResources(this.LabelAltitude, "LabelAltitude");
            this.LabelAltitude.ForeColor = System.Drawing.Color.White;
            this.LabelAltitude.Name = "LabelAltitude";
            this.TTButtonDescription.SetToolTip(this.LabelAltitude, resources.GetString("LabelAltitude.ToolTip"));
            // 
            // BtnDroneMode
            // 
            resources.ApplyResources(this.BtnDroneMode, "BtnDroneMode");
            this.BtnDroneMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.BtnDroneMode.FlatAppearance.BorderSize = 0;
            this.BtnDroneMode.ForeColor = System.Drawing.Color.White;
            this.BtnDroneMode.Image = global::Diva.Properties.Resources.icon_airplane_32;
            this.BtnDroneMode.Name = "BtnDroneMode";
            this.TTButtonDescription.SetToolTip(this.BtnDroneMode, resources.GetString("BtnDroneMode.ToolTip"));
            this.BtnDroneMode.UseVisualStyleBackColor = false;
            // 
            // BtnAltitude
            // 
            resources.ApplyResources(this.BtnAltitude, "BtnAltitude");
            this.BtnAltitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.BtnAltitude.FlatAppearance.BorderSize = 0;
            this.BtnAltitude.ForeColor = System.Drawing.Color.White;
            this.BtnAltitude.Image = global::Diva.Properties.Resources.icon_edit_32;
            this.BtnAltitude.Name = "BtnAltitude";
            this.TTButtonDescription.SetToolTip(this.BtnAltitude, resources.GetString("BtnAltitude.ToolTip"));
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
            this.TTButtonDescription.SetToolTip(this.BtnHome, resources.GetString("BtnHome.ToolTip"));
            this.BtnHome.UseVisualStyleBackColor = false;
            // 
            // TxtAltitudeValue
            // 
            resources.ApplyResources(this.TxtAltitudeValue, "TxtAltitudeValue");
            this.TxtAltitudeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtAltitudeValue.Name = "TxtAltitudeValue";
            this.TTButtonDescription.SetToolTip(this.TxtAltitudeValue, resources.GetString("TxtAltitudeValue.ToolTip"));
            // 
            // Planner
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.SplitContainer);
            this.KeyPreview = true;
            this.Name = "Planner";
            this.TTButtonDescription.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Planner_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Planner_FormClosed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Planner_KeyUp);
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
		private MyTSButton TSBtnConfigure;
		private MyGMap Map;
		private MyButton BtnArm;
		private MyButton BtnReadWPs;
		private MyButton BtnWriteWPs;
		private MyButton BtnAuto;
		private MyButton BtnTakeOff;
		private MyButton BtnLand;
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
        private DroneInfoPanel DroneInfoPanel;
		private FlowLayoutPanel RotationInfoPanel;
		private Label LblMode;
		private ComboBox ComBoxModeSwitch;
		private MyButton BtnZoomOut;
		private MyButton BtnZoomIn;
		private MyButton BtnMapFocus;
		private Label LblModeDesc;
        private AltitudeControlPanel AltitudeControlPanel;
        private ToolTip TTButtonDescription;
        private MyButton BtnBreakAction;
        private Button IconModeWarning;
        private Button IconGPSLostWarning;
    }
}