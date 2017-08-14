namespace FooApplication
{
	partial class Main
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.drone_ack_panel = new System.Windows.Forms.Panel();
			this.drone_ack_value = new System.Windows.Forms.Label();
			this.TXT_Mode = new System.Windows.Forms.Label();
			this.drone_ack_title = new System.Windows.Forms.Label();
			this.drone_mode_title = new System.Windows.Forms.Label();
			this.drone_status_panel = new System.Windows.Forms.Panel();
			this.drone_status_vspeed_value = new System.Windows.Forms.Label();
			this.drone_status_title = new System.Windows.Forms.Label();
			this.drone_status_battery_title = new System.Windows.Forms.Label();
			this.drone_status_gspeed_value = new System.Windows.Forms.Label();
			this.drone_Status_altitude_title = new System.Windows.Forms.Label();
			this.drone_status_yaw_value = new System.Windows.Forms.Label();
			this.drone_status_gps_title = new System.Windows.Forms.Label();
			this.drone_status_gps_value = new System.Windows.Forms.Label();
			this.drone_status_vspeed_title = new System.Windows.Forms.Label();
			this.TXT_Alt = new System.Windows.Forms.Label();
			this.drone_status_gspeed_title = new System.Windows.Forms.Label();
			this.TXT_Battery = new System.Windows.Forms.Label();
			this.drone_status_yaw_title = new System.Windows.Forms.Label();
			this.btn_connect = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btn_land = new System.Windows.Forms.Button();
			this.btn_takeoff = new System.Windows.Forms.Button();
			this.btn_disarm = new System.Windows.Forms.Button();
			this.btn_arm = new System.Windows.Forms.Button();
			this.opertaion_table_title = new System.Windows.Forms.Label();
			this.gmapControl = new GMap.NET.WindowsForms.GMapControl();
			this.contextMenuStripMap = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.flyToHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.flyToHereAltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.takeOFfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.drone_ack_panel.SuspendLayout();
			this.drone_status_panel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.contextMenuStripMap.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(1288, 33);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(51, 27);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(55, 27);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(63, 27);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(62, 27);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabControl1.Location = new System.Drawing.Point(0, 33);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1288, 949);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.splitContainer1);
			this.tabPage2.Location = new System.Drawing.Point(4, 32);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage2.Size = new System.Drawing.Size(1280, 913);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Overview";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(4, 4);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btn_connect);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Panel2.Controls.Add(this.gmapControl);
			this.splitContainer1.Size = new System.Drawing.Size(1272, 905);
			this.splitContainer1.SplitterDistance = 423;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.drone_ack_panel);
			this.splitContainer2.Panel2.Controls.Add(this.drone_status_panel);
			this.splitContainer2.Size = new System.Drawing.Size(423, 905);
			this.splitContainer2.SplitterDistance = 401;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(22, 32);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(241, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Charge_station_status";
			// 
			// drone_ack_panel
			// 
			this.drone_ack_panel.Controls.Add(this.drone_ack_value);
			this.drone_ack_panel.Controls.Add(this.TXT_Mode);
			this.drone_ack_panel.Controls.Add(this.drone_ack_title);
			this.drone_ack_panel.Controls.Add(this.drone_mode_title);
			this.drone_ack_panel.Location = new System.Drawing.Point(27, 18);
			this.drone_ack_panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.drone_ack_panel.Name = "drone_ack_panel";
			this.drone_ack_panel.Size = new System.Drawing.Size(338, 88);
			this.drone_ack_panel.TabIndex = 15;
			// 
			// drone_ack_value
			// 
			this.drone_ack_value.AutoSize = true;
			this.drone_ack_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_ack_value.Location = new System.Drawing.Point(258, 52);
			this.drone_ack_value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_ack_value.Name = "drone_ack_value";
			this.drone_ack_value.Size = new System.Drawing.Size(54, 23);
			this.drone_ack_value.TabIndex = 17;
			this.drone_ack_value.Text = "None";
			// 
			// TXT_Mode
			// 
			this.TXT_Mode.AutoSize = true;
			this.TXT_Mode.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TXT_Mode.Location = new System.Drawing.Point(258, 14);
			this.TXT_Mode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TXT_Mode.Name = "TXT_Mode";
			this.TXT_Mode.Size = new System.Drawing.Size(54, 23);
			this.TXT_Mode.TabIndex = 16;
			this.TXT_Mode.Text = "None";
			// 
			// drone_ack_title
			// 
			this.drone_ack_title.AutoSize = true;
			this.drone_ack_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_ack_title.Location = new System.Drawing.Point(20, 52);
			this.drone_ack_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_ack_title.Name = "drone_ack_title";
			this.drone_ack_title.Size = new System.Drawing.Size(109, 23);
			this.drone_ack_title.TabIndex = 15;
			this.drone_ack_title.Text = "Drone_Ack";
			// 
			// drone_mode_title
			// 
			this.drone_mode_title.AutoSize = true;
			this.drone_mode_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_mode_title.Location = new System.Drawing.Point(20, 14);
			this.drone_mode_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_mode_title.Name = "drone_mode_title";
			this.drone_mode_title.Size = new System.Drawing.Size(120, 23);
			this.drone_mode_title.TabIndex = 14;
			this.drone_mode_title.Text = "Drone_Mode";
			// 
			// drone_status_panel
			// 
			this.drone_status_panel.Controls.Add(this.drone_status_vspeed_value);
			this.drone_status_panel.Controls.Add(this.drone_status_title);
			this.drone_status_panel.Controls.Add(this.drone_status_battery_title);
			this.drone_status_panel.Controls.Add(this.drone_status_gspeed_value);
			this.drone_status_panel.Controls.Add(this.drone_Status_altitude_title);
			this.drone_status_panel.Controls.Add(this.drone_status_yaw_value);
			this.drone_status_panel.Controls.Add(this.drone_status_gps_title);
			this.drone_status_panel.Controls.Add(this.drone_status_gps_value);
			this.drone_status_panel.Controls.Add(this.drone_status_vspeed_title);
			this.drone_status_panel.Controls.Add(this.TXT_Alt);
			this.drone_status_panel.Controls.Add(this.drone_status_gspeed_title);
			this.drone_status_panel.Controls.Add(this.TXT_Battery);
			this.drone_status_panel.Controls.Add(this.drone_status_yaw_title);
			this.drone_status_panel.Location = new System.Drawing.Point(27, 130);
			this.drone_status_panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.drone_status_panel.Name = "drone_status_panel";
			this.drone_status_panel.Size = new System.Drawing.Size(338, 320);
			this.drone_status_panel.TabIndex = 14;
			// 
			// drone_status_vspeed_value
			// 
			this.drone_status_vspeed_value.AutoSize = true;
			this.drone_status_vspeed_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_vspeed_value.Location = new System.Drawing.Point(258, 278);
			this.drone_status_vspeed_value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_vspeed_value.Name = "drone_status_vspeed_value";
			this.drone_status_vspeed_value.Size = new System.Drawing.Size(54, 23);
			this.drone_status_vspeed_value.TabIndex = 13;
			this.drone_status_vspeed_value.Text = "0000";
			// 
			// drone_status_title
			// 
			this.drone_status_title.AutoSize = true;
			this.drone_status_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_title.Location = new System.Drawing.Point(20, 14);
			this.drone_status_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_title.Name = "drone_status_title";
			this.drone_status_title.Size = new System.Drawing.Size(142, 23);
			this.drone_status_title.TabIndex = 1;
			this.drone_status_title.Text = "DRONE_STATUS";
			// 
			// drone_status_battery_title
			// 
			this.drone_status_battery_title.AutoSize = true;
			this.drone_status_battery_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_battery_title.Location = new System.Drawing.Point(20, 64);
			this.drone_status_battery_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_battery_title.Name = "drone_status_battery_title";
			this.drone_status_battery_title.Size = new System.Drawing.Size(87, 23);
			this.drone_status_battery_title.TabIndex = 6;
			this.drone_status_battery_title.Text = "Battery";
			// 
			// drone_status_gspeed_value
			// 
			this.drone_status_gspeed_value.AutoSize = true;
			this.drone_status_gspeed_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gspeed_value.Location = new System.Drawing.Point(258, 231);
			this.drone_status_gspeed_value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_gspeed_value.Name = "drone_status_gspeed_value";
			this.drone_status_gspeed_value.Size = new System.Drawing.Size(54, 23);
			this.drone_status_gspeed_value.TabIndex = 12;
			this.drone_status_gspeed_value.Text = "0000";
			// 
			// drone_Status_altitude_title
			// 
			this.drone_Status_altitude_title.AutoSize = true;
			this.drone_Status_altitude_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_Status_altitude_title.Location = new System.Drawing.Point(20, 108);
			this.drone_Status_altitude_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_Status_altitude_title.Name = "drone_Status_altitude_title";
			this.drone_Status_altitude_title.Size = new System.Drawing.Size(98, 23);
			this.drone_Status_altitude_title.TabIndex = 2;
			this.drone_Status_altitude_title.Text = "Altitude";
			// 
			// drone_status_yaw_value
			// 
			this.drone_status_yaw_value.AutoSize = true;
			this.drone_status_yaw_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_yaw_value.Location = new System.Drawing.Point(258, 188);
			this.drone_status_yaw_value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_yaw_value.Name = "drone_status_yaw_value";
			this.drone_status_yaw_value.Size = new System.Drawing.Size(54, 23);
			this.drone_status_yaw_value.TabIndex = 11;
			this.drone_status_yaw_value.Text = "0000";
			// 
			// drone_status_gps_title
			// 
			this.drone_status_gps_title.AutoSize = true;
			this.drone_status_gps_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gps_title.Location = new System.Drawing.Point(20, 148);
			this.drone_status_gps_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_gps_title.Name = "drone_status_gps_title";
			this.drone_status_gps_title.Size = new System.Drawing.Size(43, 23);
			this.drone_status_gps_title.TabIndex = 3;
			this.drone_status_gps_title.Text = "GPS";
			// 
			// drone_status_gps_value
			// 
			this.drone_status_gps_value.AutoSize = true;
			this.drone_status_gps_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gps_value.Location = new System.Drawing.Point(258, 148);
			this.drone_status_gps_value.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_gps_value.Name = "drone_status_gps_value";
			this.drone_status_gps_value.Size = new System.Drawing.Size(54, 23);
			this.drone_status_gps_value.TabIndex = 10;
			this.drone_status_gps_value.Text = "0000";
			// 
			// drone_status_vspeed_title
			// 
			this.drone_status_vspeed_title.AutoSize = true;
			this.drone_status_vspeed_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_vspeed_title.Location = new System.Drawing.Point(20, 278);
			this.drone_status_vspeed_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_vspeed_title.Name = "drone_status_vspeed_title";
			this.drone_status_vspeed_title.Size = new System.Drawing.Size(164, 23);
			this.drone_status_vspeed_title.TabIndex = 4;
			this.drone_status_vspeed_title.Text = "Vertical_Speed";
			// 
			// TXT_Alt
			// 
			this.TXT_Alt.AutoSize = true;
			this.TXT_Alt.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TXT_Alt.Location = new System.Drawing.Point(258, 108);
			this.TXT_Alt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TXT_Alt.Name = "TXT_Alt";
			this.TXT_Alt.Size = new System.Drawing.Size(54, 23);
			this.TXT_Alt.TabIndex = 9;
			this.TXT_Alt.Text = "0000";
			// 
			// drone_status_gspeed_title
			// 
			this.drone_status_gspeed_title.AutoSize = true;
			this.drone_status_gspeed_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gspeed_title.Location = new System.Drawing.Point(20, 231);
			this.drone_status_gspeed_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_gspeed_title.Name = "drone_status_gspeed_title";
			this.drone_status_gspeed_title.Size = new System.Drawing.Size(131, 23);
			this.drone_status_gspeed_title.TabIndex = 5;
			this.drone_status_gspeed_title.Text = "Groud_Speed";
			// 
			// TXT_Battery
			// 
			this.TXT_Battery.AutoSize = true;
			this.TXT_Battery.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TXT_Battery.Location = new System.Drawing.Point(258, 64);
			this.TXT_Battery.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.TXT_Battery.Name = "TXT_Battery";
			this.TXT_Battery.Size = new System.Drawing.Size(54, 23);
			this.TXT_Battery.TabIndex = 8;
			this.TXT_Battery.Text = "0000";
			// 
			// drone_status_yaw_title
			// 
			this.drone_status_yaw_title.AutoSize = true;
			this.drone_status_yaw_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_yaw_title.Location = new System.Drawing.Point(20, 188);
			this.drone_status_yaw_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.drone_status_yaw_title.Name = "drone_status_yaw_title";
			this.drone_status_yaw_title.Size = new System.Drawing.Size(43, 23);
			this.drone_status_yaw_title.TabIndex = 7;
			this.drone_status_yaw_title.Text = "Yaw";
			// 
			// btn_connect
			// 
			this.btn_connect.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_connect.Location = new System.Drawing.Point(517, 15);
			this.btn_connect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btn_connect.Name = "btn_connect";
			this.btn_connect.Size = new System.Drawing.Size(300, 50);
			this.btn_connect.TabIndex = 2;
			this.btn_connect.Text = "Connect";
			this.btn_connect.UseVisualStyleBackColor = true;
			this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btn_land);
			this.panel1.Controls.Add(this.btn_takeoff);
			this.panel1.Controls.Add(this.btn_disarm);
			this.panel1.Controls.Add(this.btn_arm);
			this.panel1.Controls.Add(this.opertaion_table_title);
			this.panel1.Location = new System.Drawing.Point(517, 73);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(300, 260);
			this.panel1.TabIndex = 1;
			// 
			// btn_land
			// 
			this.btn_land.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_land.Location = new System.Drawing.Point(39, 198);
			this.btn_land.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btn_land.Name = "btn_land";
			this.btn_land.Size = new System.Drawing.Size(232, 38);
			this.btn_land.TabIndex = 4;
			this.btn_land.Text = "Land";
			this.btn_land.UseVisualStyleBackColor = true;
			this.btn_land.Click += new System.EventHandler(this.btn_land_Click);
			// 
			// btn_takeoff
			// 
			this.btn_takeoff.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_takeoff.Location = new System.Drawing.Point(39, 152);
			this.btn_takeoff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btn_takeoff.Name = "btn_takeoff";
			this.btn_takeoff.Size = new System.Drawing.Size(232, 38);
			this.btn_takeoff.TabIndex = 3;
			this.btn_takeoff.Text = "Takeoff";
			this.btn_takeoff.UseVisualStyleBackColor = true;
			this.btn_takeoff.Click += new System.EventHandler(this.btn_takeoff_Click);
			// 
			// btn_disarm
			// 
			this.btn_disarm.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_disarm.Location = new System.Drawing.Point(39, 105);
			this.btn_disarm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btn_disarm.Name = "btn_disarm";
			this.btn_disarm.Size = new System.Drawing.Size(232, 38);
			this.btn_disarm.TabIndex = 2;
			this.btn_disarm.Text = "Disarm";
			this.btn_disarm.UseVisualStyleBackColor = true;
			// 
			// btn_arm
			// 
			this.btn_arm.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_arm.Location = new System.Drawing.Point(39, 58);
			this.btn_arm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btn_arm.Name = "btn_arm";
			this.btn_arm.Size = new System.Drawing.Size(232, 38);
			this.btn_arm.TabIndex = 1;
			this.btn_arm.Text = "Arm";
			this.btn_arm.UseVisualStyleBackColor = true;
			this.btn_arm.Click += new System.EventHandler(this.btn_arm_Click);
			// 
			// opertaion_table_title
			// 
			this.opertaion_table_title.AutoSize = true;
			this.opertaion_table_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.opertaion_table_title.Location = new System.Drawing.Point(21, 16);
			this.opertaion_table_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.opertaion_table_title.Name = "opertaion_table_title";
			this.opertaion_table_title.Size = new System.Drawing.Size(175, 23);
			this.opertaion_table_title.TabIndex = 0;
			this.opertaion_table_title.Text = "operation_table";
			// 
			// gmapControl
			// 
			this.gmapControl.Bearing = 0F;
			this.gmapControl.CanDragMap = true;
			this.gmapControl.ContextMenuStrip = this.contextMenuStripMap;
			this.gmapControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gmapControl.EmptyTileColor = System.Drawing.Color.Navy;
			this.gmapControl.GrayScaleMode = false;
			this.gmapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.gmapControl.LevelsKeepInMemmory = 5;
			this.gmapControl.Location = new System.Drawing.Point(0, 0);
			this.gmapControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gmapControl.MarkersEnabled = true;
			this.gmapControl.MaxZoom = 2;
			this.gmapControl.MinZoom = 2;
			this.gmapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.gmapControl.Name = "gmapControl";
			this.gmapControl.NegativeMode = false;
			this.gmapControl.PolygonsEnabled = true;
			this.gmapControl.RetryLoadTile = 0;
			this.gmapControl.RoutesEnabled = true;
			this.gmapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.gmapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.gmapControl.ShowTileGridLines = false;
			this.gmapControl.Size = new System.Drawing.Size(841, 903);
			this.gmapControl.TabIndex = 0;
			this.gmapControl.Zoom = 0D;
			this.gmapControl.Load += new System.EventHandler(this.gMapControl1_Load);
			this.gmapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseDown);
			// 
			// contextMenuStripMap
			// 
			this.contextMenuStripMap.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextMenuStripMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flyToHereToolStripMenuItem,
            this.flyToHereAltToolStripMenuItem,
            this.takeOFfToolStripMenuItem});
			this.contextMenuStripMap.Name = "contextMenuStripMap";
			this.contextMenuStripMap.Size = new System.Drawing.Size(218, 94);
			// 
			// flyToHereToolStripMenuItem
			// 
			this.flyToHereToolStripMenuItem.Name = "flyToHereToolStripMenuItem";
			this.flyToHereToolStripMenuItem.Size = new System.Drawing.Size(217, 30);
			this.flyToHereToolStripMenuItem.Text = "Fly To Here";
			this.flyToHereToolStripMenuItem.Click += new System.EventHandler(this.goHereToolStripMenuItem_Click);
			// 
			// flyToHereAltToolStripMenuItem
			// 
			this.flyToHereAltToolStripMenuItem.Name = "flyToHereAltToolStripMenuItem";
			this.flyToHereAltToolStripMenuItem.Size = new System.Drawing.Size(217, 30);
			this.flyToHereAltToolStripMenuItem.Text = "Fly To Here Alt";
			// 
			// takeOFfToolStripMenuItem
			// 
			this.takeOFfToolStripMenuItem.Name = "takeOFfToolStripMenuItem";
			this.takeOFfToolStripMenuItem.Size = new System.Drawing.Size(217, 30);
			this.takeOFfToolStripMenuItem.Text = "TakeOff";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.gMapControl1);
			this.tabPage3.Location = new System.Drawing.Point(4, 32);
			this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage3.Size = new System.Drawing.Size(1594, 910);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Planning";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// gMapControl1
			// 
			this.gMapControl1.Bearing = 0F;
			this.gMapControl1.CanDragMap = true;
			this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
			this.gMapControl1.GrayScaleMode = false;
			this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.gMapControl1.LevelsKeepInMemmory = 5;
			this.gMapControl1.Location = new System.Drawing.Point(4, 4);
			this.gMapControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gMapControl1.MarkersEnabled = true;
			this.gMapControl1.MaxZoom = 2;
			this.gMapControl1.MinZoom = 2;
			this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.gMapControl1.Name = "gMapControl1";
			this.gMapControl1.NegativeMode = false;
			this.gMapControl1.PolygonsEnabled = true;
			this.gMapControl1.RetryLoadTile = 0;
			this.gMapControl1.RoutesEnabled = true;
			this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.gMapControl1.ShowTileGridLines = false;
			this.gMapControl1.Size = new System.Drawing.Size(1586, 902);
			this.gMapControl1.TabIndex = 0;
			this.gMapControl1.Zoom = 0D;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 32);
			this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage4.Size = new System.Drawing.Size(1594, 910);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Rotation";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 32);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabPage1.Size = new System.Drawing.Size(1594, 910);
			this.tabPage1.TabIndex = 4;
			this.tabPage1.Text = "Option";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// printDialog1
			// 
			this.printDialog1.UseEXDialog = true;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1288, 982);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Main";
			this.Text = "MainPage";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.drone_ack_panel.ResumeLayout(false);
			this.drone_ack_panel.PerformLayout();
			this.drone_status_panel.ResumeLayout(false);
			this.drone_status_panel.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contextMenuStripMap.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TabPage tabPage1;
		private GMap.NET.WindowsForms.GMapControl gmapControl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label opertaion_table_title;
		private System.Windows.Forms.Button btn_land;
		private System.Windows.Forms.Button btn_takeoff;
		private System.Windows.Forms.Button btn_disarm;
		private System.Windows.Forms.Button btn_arm;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label drone_status_title;
		private System.Windows.Forms.Label drone_status_vspeed_value;
		private System.Windows.Forms.Label drone_status_gspeed_value;
		private System.Windows.Forms.Label drone_status_yaw_value;
		private System.Windows.Forms.Label drone_status_gps_value;
		private System.Windows.Forms.Label TXT_Alt;
		private System.Windows.Forms.Label TXT_Battery;
		private System.Windows.Forms.Label drone_status_yaw_title;
		private System.Windows.Forms.Label drone_status_battery_title;
		private System.Windows.Forms.Label drone_status_gspeed_title;
		private System.Windows.Forms.Label drone_status_vspeed_title;
		private System.Windows.Forms.Label drone_status_gps_title;
		private System.Windows.Forms.Label drone_Status_altitude_title;
		private System.Windows.Forms.Panel drone_ack_panel;
		private System.Windows.Forms.Label drone_ack_title;
		private System.Windows.Forms.Label drone_mode_title;
		private System.Windows.Forms.Panel drone_status_panel;
		private System.Windows.Forms.Label drone_ack_value;
		private System.Windows.Forms.Label TXT_Mode;
		private GMap.NET.WindowsForms.GMapControl gMapControl1;
		private System.Windows.Forms.Button btn_connect;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripMap;
		private System.Windows.Forms.ToolStripMenuItem flyToHereToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem flyToHereAltToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem takeOFfToolStripMenuItem;
	}
}

