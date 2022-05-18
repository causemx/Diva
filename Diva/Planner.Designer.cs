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
            this.flyToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnArm = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.BtnTakeOff = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.BtnLand = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.BtnAuto = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.BtnWriteWPs = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.BtnReadWPs = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.BtnRTL = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.configGridPanel = new System.Windows.Forms.Panel();
            this.shrinkButton = new System.Windows.Forms.Button();
            this.gridAcceptbutton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.spacingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.distanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.angleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.altitudeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.startFromComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.planToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surveyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.corridorScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.flyToolStrip.SuspendLayout();
            this.configGridPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spacingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.altitudeNumericUpDown)).BeginInit();
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
            this.SplitContainer.Panel1.Controls.Add(this.flyToolStrip);
            this.SplitContainer.Panel1.Controls.Add(this.configGridPanel);
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
            this.SplitContainer.Panel1.Controls.Add(this.TSMainPanel);
            this.SplitContainer.Panel1.Controls.Add(this.Map);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.DGVWayPoints);
            this.SplitContainer.Panel2.Controls.Add(this.ActiveDroneInfoPanel);
            // 
            // flyToolStrip
            // 
            resources.ApplyResources(this.flyToolStrip, "flyToolStrip");
            this.flyToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(32)))), ((int)(((byte)(35)))));
            this.flyToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.flyToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator3,
            this.BtnArm,
            this.toolStripLabel8,
            this.BtnTakeOff,
            this.toolStripLabel2,
            this.BtnLand,
            this.toolStripLabel3,
            this.BtnAuto,
            this.toolStripLabel4,
            this.BtnWriteWPs,
            this.toolStripLabel6,
            this.BtnReadWPs,
            this.toolStripLabel7,
            this.BtnRTL,
            this.toolStripLabel5});
            this.flyToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.flyToolStrip.Name = "flyToolStrip";
            this.flyToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // BtnArm
            // 
            this.BtnArm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BtnArm, "BtnArm");
            this.BtnArm.Name = "BtnArm";
            this.BtnArm.Click += new System.EventHandler(this.BUT_Arm_Click);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel8.Name = "toolStripLabel8";
            resources.ApplyResources(this.toolStripLabel8, "toolStripLabel8");
            // 
            // BtnTakeOff
            // 
            this.BtnTakeOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BtnTakeOff, "BtnTakeOff");
            this.BtnTakeOff.Name = "BtnTakeOff";
            this.BtnTakeOff.Click += new System.EventHandler(this.BUT_Takeoff_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel2.Name = "toolStripLabel2";
            resources.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
            // 
            // BtnLand
            // 
            this.BtnLand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BtnLand, "BtnLand");
            this.BtnLand.Name = "BtnLand";
            this.BtnLand.Click += new System.EventHandler(this.BUT_Land_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel3.Name = "toolStripLabel3";
            resources.ApplyResources(this.toolStripLabel3, "toolStripLabel3");
            // 
            // BtnAuto
            // 
            this.BtnAuto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BtnAuto, "BtnAuto");
            this.BtnAuto.Name = "BtnAuto";
            this.BtnAuto.Click += new System.EventHandler(this.BUT_Auto_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel4.Name = "toolStripLabel4";
            resources.ApplyResources(this.toolStripLabel4, "toolStripLabel4");
            // 
            // BtnWriteWPs
            // 
            this.BtnWriteWPs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BtnWriteWPs, "BtnWriteWPs");
            this.BtnWriteWPs.Name = "BtnWriteWPs";
            this.BtnWriteWPs.Click += new System.EventHandler(this.BUT_write_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel6.Name = "toolStripLabel6";
            resources.ApplyResources(this.toolStripLabel6, "toolStripLabel6");
            // 
            // BtnReadWPs
            // 
            this.BtnReadWPs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BtnReadWPs, "BtnReadWPs");
            this.BtnReadWPs.Name = "BtnReadWPs";
            this.BtnReadWPs.Click += new System.EventHandler(this.BUT_read_Click);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel7.Name = "toolStripLabel7";
            resources.ApplyResources(this.toolStripLabel7, "toolStripLabel7");
            // 
            // BtnRTL
            // 
            this.BtnRTL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BtnRTL, "BtnRTL");
            this.BtnRTL.Name = "BtnRTL";
            this.BtnRTL.Click += new System.EventHandler(this.BUT_RTL_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel5.Name = "toolStripLabel5";
            resources.ApplyResources(this.toolStripLabel5, "toolStripLabel5");
            // 
            // configGridPanel
            // 
            resources.ApplyResources(this.configGridPanel, "configGridPanel");
            this.configGridPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.configGridPanel.Controls.Add(this.shrinkButton);
            this.configGridPanel.Controls.Add(this.gridAcceptbutton);
            this.configGridPanel.Controls.Add(this.tableLayoutPanel1);
            this.configGridPanel.Name = "configGridPanel";
            // 
            // shrinkButton
            // 
            resources.ApplyResources(this.shrinkButton, "shrinkButton");
            this.shrinkButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.shrinkButton.FlatAppearance.BorderSize = 0;
            this.shrinkButton.Name = "shrinkButton";
            this.shrinkButton.UseVisualStyleBackColor = true;
            this.shrinkButton.Click += new System.EventHandler(this.config_grid_button_click);
            // 
            // gridAcceptbutton
            // 
            resources.ApplyResources(this.gridAcceptbutton, "gridAcceptbutton");
            this.gridAcceptbutton.ForeColor = System.Drawing.Color.White;
            this.gridAcceptbutton.Name = "gridAcceptbutton";
            this.gridAcceptbutton.UseVisualStyleBackColor = true;
            this.gridAcceptbutton.Click += new System.EventHandler(this.gridAcceptbutton_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.spacingNumericUpDown, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.distanceNumericUpDown, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.angleNumericUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.altitudeNumericUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.startFromComboBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Name = "label6";
            // 
            // spacingNumericUpDown
            // 
            resources.ApplyResources(this.spacingNumericUpDown, "spacingNumericUpDown");
            this.spacingNumericUpDown.Name = "spacingNumericUpDown";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // distanceNumericUpDown
            // 
            resources.ApplyResources(this.distanceNumericUpDown, "distanceNumericUpDown");
            this.distanceNumericUpDown.Name = "distanceNumericUpDown";
            this.distanceNumericUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // angleNumericUpDown
            // 
            resources.ApplyResources(this.angleNumericUpDown, "angleNumericUpDown");
            this.angleNumericUpDown.Name = "angleNumericUpDown";
            this.angleNumericUpDown.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // altitudeNumericUpDown
            // 
            resources.ApplyResources(this.altitudeNumericUpDown, "altitudeNumericUpDown");
            this.altitudeNumericUpDown.Name = "altitudeNumericUpDown";
            this.altitudeNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // startFromComboBox
            // 
            this.startFromComboBox.FormattingEnabled = true;
            this.startFromComboBox.Items.AddRange(new object[] {
            resources.GetString("startFromComboBox.Items")});
            resources.ApplyResources(this.startFromComboBox, "startFromComboBox");
            this.startFromComboBox.Name = "startFromComboBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
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
            this.TTButtonDescription.SetToolTip(this.BtnZoomOut, resources.GetString("BtnZoomOut.ToolTip"));
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
            this.TTButtonDescription.SetToolTip(this.BtnZoomIn, resources.GetString("BtnZoomIn.ToolTip"));
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
            // TSMainPanel
            // 
            this.TSMainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            resources.ApplyResources(this.TSMainPanel, "TSMainPanel");
            this.TSMainPanel.GripMargin = new System.Windows.Forms.Padding(0);
            this.TSMainPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TSMainPanel.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.TSMainPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSBtnConnect,
            this.TSBtnConfigure});
            this.TSMainPanel.Name = "TSMainPanel";
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
            this.Map.ScaleSize = new System.Drawing.Size(100, 10);
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
            // 
            // corridorScanToolStripMenuItem
            // 
            this.corridorScanToolStripMenuItem.Name = "corridorScanToolStripMenuItem";
            resources.ApplyResources(this.corridorScanToolStripMenuItem, "corridorScanToolStripMenuItem");
            // 
            // Planner
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.SplitContainer);
            this.KeyPreview = true;
            this.Name = "Planner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Planner_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Planner_FormClosed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Planner_KeyUp);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.flyToolStrip.ResumeLayout(false);
            this.flyToolStrip.PerformLayout();
            this.configGridPanel.ResumeLayout(false);
            this.configGridPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spacingNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.altitudeNumericUpDown)).EndInit();
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
        private ToolTip TTButtonDescription;
        private MyButton BtnBreakAction;
        private Button IconModeWarning;
        private Button IconGPSLostWarning;
        internal MyGMap Map;
        internal AltitudeControlPanel AltitudeControlPanel;
        private ToolStripSeparator toolStripSeparator2;
        private Panel configGridPanel;
        private Button gridAcceptbutton;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label6;
        private NumericUpDown spacingNumericUpDown;
        private Label label4;
        private NumericUpDown distanceNumericUpDown;
        private Label label3;
        private NumericUpDown angleNumericUpDown;
        private Label label2;
        private Label label1;
        private NumericUpDown altitudeNumericUpDown;
        private ComboBox startFromComboBox;
        private Button shrinkButton;
        private ToolStrip flyToolStrip;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton BtnTakeOff;
        private ToolStripButton BtnLand;
        private ToolStripButton BtnAuto;
        private ToolStripButton BtnWriteWPs;
        private ToolStripButton BtnReadWPs;
        private ToolStripButton BtnRTL;
        private ToolStripLabel toolStripLabel2;
        private ToolStripLabel toolStripLabel3;
        private ToolStripLabel toolStripLabel4;
        private ToolStripLabel toolStripLabel6;
        private ToolStripLabel toolStripLabel7;
        private ToolStripLabel toolStripLabel5;
        private ToolStripButton BtnArm;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel toolStripLabel8;
        private ToolStripMenuItem planToolStripMenuItem;
        private ToolStripMenuItem surveyToolStripMenuItem;
        private ToolStripMenuItem corridorScanToolStripMenuItem;
    }
}