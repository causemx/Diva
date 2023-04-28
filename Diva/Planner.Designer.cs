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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.BtnStrartWsServer = new Diva.Controls.Components.MyButton();
            this.IconGPSLostWarning = new System.Windows.Forms.Button();
            this.IconModeWarning = new System.Windows.Forms.Button();
            this.BtnBreakAction = new Diva.Controls.Components.MyButton();
            this.AltitudeControlPanel = new Diva.Controls.AltitudeControlPanel();
            this.BtnZoomOut = new Diva.Controls.Components.MyButton();
            this.BtnZoomIn = new Diva.Controls.Components.MyButton();
            this.BtnMapFocus = new Diva.Controls.Components.MyButton();
            this.RotationInfoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DroneInfoPanel = new Diva.Controls.DroneInfoPanel();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.planToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surveyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.corridorScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.colPrev = new System.Windows.Forms.DataGridViewImageColumn();
            this.colNext = new System.Windows.Forms.DataGridViewImageColumn();
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
            this.NumericUpDownDerived = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.cmMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVWayPoints)).BeginInit();
            this.ActiveDroneInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownDerived)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            resources.ApplyResources(this.SplitContainer, "SplitContainer");
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.NumericUpDownDerived);
            this.SplitContainer.Panel1.Controls.Add(this.BtnStrartWsServer);
            this.SplitContainer.Panel1.Controls.Add(this.IconGPSLostWarning);
            this.SplitContainer.Panel1.Controls.Add(this.IconModeWarning);
            this.SplitContainer.Panel1.Controls.Add(this.BtnBreakAction);
            this.SplitContainer.Panel1.Controls.Add(this.AltitudeControlPanel);
            this.SplitContainer.Panel1.Controls.Add(this.BtnZoomOut);
            this.SplitContainer.Panel1.Controls.Add(this.BtnZoomIn);
            this.SplitContainer.Panel1.Controls.Add(this.BtnMapFocus);
            this.SplitContainer.Panel1.Controls.Add(this.RotationInfoPanel);
            this.SplitContainer.Panel1.Controls.Add(this.DroneInfoPanel);
            this.SplitContainer.Panel1.Controls.Add(this.Map);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.DGVWayPoints);
            this.SplitContainer.Panel2.Controls.Add(this.ActiveDroneInfoPanel);
            // 
            // BtnStrartWsServer
            // 
            this.BtnStrartWsServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BtnStrartWsServer.Checked = false;
            this.BtnStrartWsServer.CheckedImage = null;
            this.BtnStrartWsServer.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnStrartWsServer.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnStrartWsServer.ClickImage = null;
            resources.ApplyResources(this.BtnStrartWsServer, "BtnStrartWsServer");
            this.BtnStrartWsServer.HoverBackColor = System.Drawing.Color.Empty;
            this.BtnStrartWsServer.HoverForeColor = System.Drawing.Color.Empty;
            this.BtnStrartWsServer.HoverImage = global::Diva.Properties.Resources.icon_server_24;
            this.BtnStrartWsServer.Image = global::Diva.Properties.Resources.icon_server_24;
            this.BtnStrartWsServer.Name = "BtnStrartWsServer";
            this.TTButtonDescription.SetToolTip(this.BtnStrartWsServer, resources.GetString("BtnStrartWsServer.ToolTip"));
            this.BtnStrartWsServer.UseVisualStyleBackColor = false;
            this.BtnStrartWsServer.Click += new System.EventHandler(this.BtnStrartWsServer_Click);
            // 
            // IconGPSLostWarning
            // 
            this.IconGPSLostWarning.BackColor = System.Drawing.Color.Transparent;
            this.IconGPSLostWarning.BackgroundImage = global::Diva.Properties.Resources.waiting_gps;
            resources.ApplyResources(this.IconGPSLostWarning, "IconGPSLostWarning");
            this.IconGPSLostWarning.FlatAppearance.BorderSize = 0;
            this.IconGPSLostWarning.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.IconGPSLostWarning.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.IconGPSLostWarning.ForeColor = System.Drawing.Color.Gold;
            this.IconGPSLostWarning.Name = "IconGPSLostWarning";
            this.IconGPSLostWarning.UseVisualStyleBackColor = false;
            // 
            // IconModeWarning
            // 
            this.IconModeWarning.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.IconModeWarning, "IconModeWarning");
            this.IconModeWarning.FlatAppearance.BorderSize = 0;
            this.IconModeWarning.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.IconModeWarning.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.IconModeWarning.ForeColor = System.Drawing.Color.Red;
            this.IconModeWarning.Name = "IconModeWarning";
            this.IconModeWarning.UseVisualStyleBackColor = false;
            // 
            // BtnBreakAction
            // 
            this.BtnBreakAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BtnBreakAction.Checked = false;
            this.BtnBreakAction.CheckedImage = null;
            this.BtnBreakAction.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnBreakAction.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnBreakAction.ClickImage = null;
            resources.ApplyResources(this.BtnBreakAction, "BtnBreakAction");
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
            this.AltitudeControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.AltitudeControlPanel.BelowColor = System.Drawing.Color.Navy;
            this.AltitudeControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.AltitudeControlPanel, "AltitudeControlPanel");
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
            this.AltitudeControlPanel.Value = 0F;
            this.AltitudeControlPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AltitudeControlPanel_MouseClick);
            // 
            // BtnZoomOut
            // 
            this.BtnZoomOut.Checked = false;
            this.BtnZoomOut.CheckedImage = null;
            this.BtnZoomOut.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnZoomOut.ClickImage = null;
            resources.ApplyResources(this.BtnZoomOut, "BtnZoomOut");
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
            this.BtnZoomIn.Checked = false;
            this.BtnZoomIn.CheckedImage = null;
            this.BtnZoomIn.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnZoomIn.ClickImage = null;
            resources.ApplyResources(this.BtnZoomIn, "BtnZoomIn");
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
            this.BtnMapFocus.Checked = false;
            this.BtnMapFocus.CheckedImage = null;
            this.BtnMapFocus.ClickBackColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.ClickForeColor = System.Drawing.Color.Empty;
            this.BtnMapFocus.ClickImage = null;
            resources.ApplyResources(this.BtnMapFocus, "BtnMapFocus");
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
            this.Map.Zoom = 15D;
            this.Map.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.MainMap_OnMarkerClick);
            this.Map.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.MainMap_OnMarkerEnter);
            this.Map.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.MainMap_OnMarkerLeave);
            this.Map.OnPositionChanged += new GMap.NET.PositionChanged(this.MainMap_OnCurrentPositionChanged);
            this.Map.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
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
            this.miClearAllMissions,
            this.toolStripSeparator2,
            this.planToolStripMenuItem});
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // planToolStripMenuItem
            // 
            this.planToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.surveyToolStripMenuItem,
            this.corridorScanToolStripMenuItem});
            this.planToolStripMenuItem.Name = "planToolStripMenuItem";
            resources.ApplyResources(this.planToolStripMenuItem, "planToolStripMenuItem");
            // 
            // surveyToolStripMenuItem
            // 
            this.surveyToolStripMenuItem.Name = "surveyToolStripMenuItem";
            resources.ApplyResources(this.surveyToolStripMenuItem, "surveyToolStripMenuItem");
            this.surveyToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // corridorScanToolStripMenuItem
            // 
            this.corridorScanToolStripMenuItem.Name = "corridorScanToolStripMenuItem";
            resources.ApplyResources(this.corridorScanToolStripMenuItem, "corridorScanToolStripMenuItem");
            this.corridorScanToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // DGVWayPoints
            // 
            this.DGVWayPoints.AllowUserToAddRows = false;
            this.DGVWayPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGVWayPoints.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGVWayPoints.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(10)))), ((int)(((byte)(13)))));
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
            this.colPrev,
            this.colNext,
            this.colDelete,
            this.colTagData});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVWayPoints.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGVWayPoints.EnableHeadersVisualStyles = false;
            this.DGVWayPoints.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(0)))), ((int)(((byte)(13)))));
            this.DGVWayPoints.Name = "DGVWayPoints";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVWayPoints.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.DGVWayPoints.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DGVWayPoints.RowTemplate.Height = 24;
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
            // colPrev
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPrev.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.colPrev, "colPrev");
            this.colPrev.Image = ((System.Drawing.Image)(resources.GetObject("colPrev.Image")));
            this.colPrev.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.colPrev.Name = "colPrev";
            // 
            // colNext
            // 
            resources.ApplyResources(this.colNext, "colNext");
            this.colNext.Image = ((System.Drawing.Image)(resources.GetObject("colNext.Image")));
            this.colNext.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.colNext.Name = "colNext";
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
            this.ActiveDroneInfoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
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
            this.ComBoxModeSwitch.SelectionChangeCommitted += new System.EventHandler(this.ComBoxModeSwitch_SelectionChangeCommitted);
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
            this.BtnDroneMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.BtnDroneMode.FlatAppearance.BorderSize = 0;
            this.BtnDroneMode.ForeColor = System.Drawing.Color.White;
            this.BtnDroneMode.Image = global::Diva.Properties.Resources.icon_airplane_32;
            this.BtnDroneMode.Name = "BtnDroneMode";
            this.BtnDroneMode.UseVisualStyleBackColor = false;
            // 
            // BtnAltitude
            // 
            this.BtnAltitude.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
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
            this.BtnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
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
            // NumericUpDownDerived
            // 
            resources.ApplyResources(this.NumericUpDownDerived, "NumericUpDownDerived");
            this.NumericUpDownDerived.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.NumericUpDownDerived.Name = "NumericUpDownDerived";
            this.TTButtonDescription.SetToolTip(this.NumericUpDownDerived, resources.GetString("NumericUpDownDerived.ToolTip"));
            this.NumericUpDownDerived.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NumericUpDownDerived.ValueChanged += new System.EventHandler(this.NumericUpDownDerived_ValueChanged);
            // 
            // Planner
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.SplitContainer);
            resources.ApplyResources(this, "$this");
            this.Name = "Planner";
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.cmMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVWayPoints)).EndInit();
            this.ActiveDroneInfoPanel.ResumeLayout(false);
            this.ActiveDroneInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownDerived)).EndInit();
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
        private SplitContainer SplitContainer;
		private Button BtnHome;
		private Button BtnAltitude;
		private Button BtnDroneMode;
		private Label LabelLongitude;
		private Label LabelLatitude;
		private Label LabelAltitude;
		private TextBox TxtHomeAltitude;
		private TextBox TxtHomeLatitude;
		private TextBox TxtHomeLongitude;
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
		private ComboBox ComBoxModeSwitch;
		private MyButton BtnZoomOut;
		private MyButton BtnZoomIn;
		private MyButton BtnMapFocus;
        private ToolTip TTButtonDescription;
        private MyButton BtnBreakAction;
        private Button IconModeWarning;
        private Button IconGPSLostWarning;
        internal MyGMap Map;
        internal AltitudeControlPanel AltitudeControlPanel;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem planToolStripMenuItem;
        private ToolStripMenuItem surveyToolStripMenuItem;
        private ToolStripMenuItem corridorScanToolStripMenuItem;
        private MyButton BtnStrartWsServer;
        private DataGridViewComboBoxColumn colCommand;
        private DataGridViewTextBoxColumn colParam1;
        private DataGridViewTextBoxColumn colParam2;
        private DataGridViewTextBoxColumn colParam3;
        private DataGridViewTextBoxColumn colParam4;
        private DataGridViewTextBoxColumn colLatitude;
        private DataGridViewTextBoxColumn colLongitude;
        private DataGridViewTextBoxColumn colAltitude;
        private DataGridViewTextBoxColumn colAngle;

        private DataGridViewImageColumn colPrev;
        private DataGridViewImageColumn colNext;
        private DataGridViewButtonColumn colDelete;
        private DataGridViewTextBoxColumn colTagData;
        private NumericUpDown NumericUpDownDerived;
    }
}