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
			this.drone_mode_value = new System.Windows.Forms.Label();
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
			this.drone_status_altitude_value = new System.Windows.Forms.Label();
			this.drone_status_gspeed_title = new System.Windows.Forms.Label();
			this.drone_status_battery_value = new System.Windows.Forms.Label();
			this.drone_status_yaw_title = new System.Windows.Forms.Label();
			this.btn_connect = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.opertaion_table_title = new System.Windows.Forms.Label();
			this.gmapControl = new GMap.NET.WindowsForms.GMapControl();
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
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1068, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
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
			this.tabControl1.Location = new System.Drawing.Point(0, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1068, 631);
			this.tabControl1.TabIndex = 2;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.splitContainer1);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(1060, 603);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Overview";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
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
			this.splitContainer1.Size = new System.Drawing.Size(1054, 597);
			this.splitContainer1.SplitterDistance = 351;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
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
			this.splitContainer2.Size = new System.Drawing.Size(351, 597);
			this.splitContainer2.SplitterDistance = 278;
			this.splitContainer2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Charge_station_status";
			// 
			// drone_ack_panel
			// 
			this.drone_ack_panel.Controls.Add(this.drone_ack_value);
			this.drone_ack_panel.Controls.Add(this.drone_mode_value);
			this.drone_ack_panel.Controls.Add(this.drone_ack_title);
			this.drone_ack_panel.Controls.Add(this.drone_mode_title);
			this.drone_ack_panel.Location = new System.Drawing.Point(18, 12);
			this.drone_ack_panel.Name = "drone_ack_panel";
			this.drone_ack_panel.Size = new System.Drawing.Size(286, 59);
			this.drone_ack_panel.TabIndex = 15;
			// 
			// drone_ack_value
			// 
			this.drone_ack_value.AutoSize = true;
			this.drone_ack_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_ack_value.Location = new System.Drawing.Point(172, 35);
			this.drone_ack_value.Name = "drone_ack_value";
			this.drone_ack_value.Size = new System.Drawing.Size(35, 15);
			this.drone_ack_value.TabIndex = 17;
			this.drone_ack_value.Text = "None";
			// 
			// drone_mode_value
			// 
			this.drone_mode_value.AutoSize = true;
			this.drone_mode_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_mode_value.Location = new System.Drawing.Point(172, 9);
			this.drone_mode_value.Name = "drone_mode_value";
			this.drone_mode_value.Size = new System.Drawing.Size(35, 15);
			this.drone_mode_value.TabIndex = 16;
			this.drone_mode_value.Text = "None";
			// 
			// drone_ack_title
			// 
			this.drone_ack_title.AutoSize = true;
			this.drone_ack_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_ack_title.Location = new System.Drawing.Point(13, 35);
			this.drone_ack_title.Name = "drone_ack_title";
			this.drone_ack_title.Size = new System.Drawing.Size(70, 15);
			this.drone_ack_title.TabIndex = 15;
			this.drone_ack_title.Text = "Drone_Ack";
			// 
			// drone_mode_title
			// 
			this.drone_mode_title.AutoSize = true;
			this.drone_mode_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_mode_title.Location = new System.Drawing.Point(13, 9);
			this.drone_mode_title.Name = "drone_mode_title";
			this.drone_mode_title.Size = new System.Drawing.Size(77, 15);
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
			this.drone_status_panel.Controls.Add(this.drone_status_altitude_value);
			this.drone_status_panel.Controls.Add(this.drone_status_gspeed_title);
			this.drone_status_panel.Controls.Add(this.drone_status_battery_value);
			this.drone_status_panel.Controls.Add(this.drone_status_yaw_title);
			this.drone_status_panel.Location = new System.Drawing.Point(18, 87);
			this.drone_status_panel.Name = "drone_status_panel";
			this.drone_status_panel.Size = new System.Drawing.Size(286, 213);
			this.drone_status_panel.TabIndex = 14;
			// 
			// drone_status_vspeed_value
			// 
			this.drone_status_vspeed_value.AutoSize = true;
			this.drone_status_vspeed_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_vspeed_value.Location = new System.Drawing.Point(172, 185);
			this.drone_status_vspeed_value.Name = "drone_status_vspeed_value";
			this.drone_status_vspeed_value.Size = new System.Drawing.Size(35, 15);
			this.drone_status_vspeed_value.TabIndex = 13;
			this.drone_status_vspeed_value.Text = "0000";
			// 
			// drone_status_title
			// 
			this.drone_status_title.AutoSize = true;
			this.drone_status_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_title.Location = new System.Drawing.Point(13, 9);
			this.drone_status_title.Name = "drone_status_title";
			this.drone_status_title.Size = new System.Drawing.Size(91, 15);
			this.drone_status_title.TabIndex = 1;
			this.drone_status_title.Text = "DRONE_STATUS";
			// 
			// drone_status_battery_title
			// 
			this.drone_status_battery_title.AutoSize = true;
			this.drone_status_battery_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_battery_title.Location = new System.Drawing.Point(13, 43);
			this.drone_status_battery_title.Name = "drone_status_battery_title";
			this.drone_status_battery_title.Size = new System.Drawing.Size(56, 15);
			this.drone_status_battery_title.TabIndex = 6;
			this.drone_status_battery_title.Text = "Battery";
			// 
			// drone_status_gspeed_value
			// 
			this.drone_status_gspeed_value.AutoSize = true;
			this.drone_status_gspeed_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gspeed_value.Location = new System.Drawing.Point(172, 154);
			this.drone_status_gspeed_value.Name = "drone_status_gspeed_value";
			this.drone_status_gspeed_value.Size = new System.Drawing.Size(35, 15);
			this.drone_status_gspeed_value.TabIndex = 12;
			this.drone_status_gspeed_value.Text = "0000";
			// 
			// drone_Status_altitude_title
			// 
			this.drone_Status_altitude_title.AutoSize = true;
			this.drone_Status_altitude_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_Status_altitude_title.Location = new System.Drawing.Point(13, 72);
			this.drone_Status_altitude_title.Name = "drone_Status_altitude_title";
			this.drone_Status_altitude_title.Size = new System.Drawing.Size(63, 15);
			this.drone_Status_altitude_title.TabIndex = 2;
			this.drone_Status_altitude_title.Text = "Altitude";
			// 
			// drone_status_yaw_value
			// 
			this.drone_status_yaw_value.AutoSize = true;
			this.drone_status_yaw_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_yaw_value.Location = new System.Drawing.Point(172, 125);
			this.drone_status_yaw_value.Name = "drone_status_yaw_value";
			this.drone_status_yaw_value.Size = new System.Drawing.Size(35, 15);
			this.drone_status_yaw_value.TabIndex = 11;
			this.drone_status_yaw_value.Text = "0000";
			// 
			// drone_status_gps_title
			// 
			this.drone_status_gps_title.AutoSize = true;
			this.drone_status_gps_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gps_title.Location = new System.Drawing.Point(13, 99);
			this.drone_status_gps_title.Name = "drone_status_gps_title";
			this.drone_status_gps_title.Size = new System.Drawing.Size(28, 15);
			this.drone_status_gps_title.TabIndex = 3;
			this.drone_status_gps_title.Text = "GPS";
			// 
			// drone_status_gps_value
			// 
			this.drone_status_gps_value.AutoSize = true;
			this.drone_status_gps_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gps_value.Location = new System.Drawing.Point(172, 99);
			this.drone_status_gps_value.Name = "drone_status_gps_value";
			this.drone_status_gps_value.Size = new System.Drawing.Size(35, 15);
			this.drone_status_gps_value.TabIndex = 10;
			this.drone_status_gps_value.Text = "0000";
			// 
			// drone_status_vspeed_title
			// 
			this.drone_status_vspeed_title.AutoSize = true;
			this.drone_status_vspeed_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_vspeed_title.Location = new System.Drawing.Point(13, 185);
			this.drone_status_vspeed_title.Name = "drone_status_vspeed_title";
			this.drone_status_vspeed_title.Size = new System.Drawing.Size(105, 15);
			this.drone_status_vspeed_title.TabIndex = 4;
			this.drone_status_vspeed_title.Text = "Vertical_Speed";
			// 
			// drone_status_altitude_value
			// 
			this.drone_status_altitude_value.AutoSize = true;
			this.drone_status_altitude_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_altitude_value.Location = new System.Drawing.Point(172, 72);
			this.drone_status_altitude_value.Name = "drone_status_altitude_value";
			this.drone_status_altitude_value.Size = new System.Drawing.Size(35, 15);
			this.drone_status_altitude_value.TabIndex = 9;
			this.drone_status_altitude_value.Text = "0000";
			// 
			// drone_status_gspeed_title
			// 
			this.drone_status_gspeed_title.AutoSize = true;
			this.drone_status_gspeed_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_gspeed_title.Location = new System.Drawing.Point(13, 154);
			this.drone_status_gspeed_title.Name = "drone_status_gspeed_title";
			this.drone_status_gspeed_title.Size = new System.Drawing.Size(84, 15);
			this.drone_status_gspeed_title.TabIndex = 5;
			this.drone_status_gspeed_title.Text = "Groud_Speed";
			// 
			// drone_status_battery_value
			// 
			this.drone_status_battery_value.AutoSize = true;
			this.drone_status_battery_value.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_battery_value.Location = new System.Drawing.Point(172, 43);
			this.drone_status_battery_value.Name = "drone_status_battery_value";
			this.drone_status_battery_value.Size = new System.Drawing.Size(35, 15);
			this.drone_status_battery_value.TabIndex = 8;
			this.drone_status_battery_value.Text = "0000";
			// 
			// drone_status_yaw_title
			// 
			this.drone_status_yaw_title.AutoSize = true;
			this.drone_status_yaw_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.drone_status_yaw_title.Location = new System.Drawing.Point(13, 125);
			this.drone_status_yaw_title.Name = "drone_status_yaw_title";
			this.drone_status_yaw_title.Size = new System.Drawing.Size(28, 15);
			this.drone_status_yaw_title.TabIndex = 7;
			this.drone_status_yaw_title.Text = "Yaw";
			// 
			// btn_connect
			// 
			this.btn_connect.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_connect.Location = new System.Drawing.Point(477, 11);
			this.btn_connect.Name = "btn_connect";
			this.btn_connect.Size = new System.Drawing.Size(200, 33);
			this.btn_connect.TabIndex = 2;
			this.btn_connect.Text = "Connect";
			this.btn_connect.UseVisualStyleBackColor = true;
			this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button4);
			this.panel1.Controls.Add(this.button3);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.opertaion_table_title);
			this.panel1.Location = new System.Drawing.Point(477, 51);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 173);
			this.panel1.TabIndex = 1;
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button4.Location = new System.Drawing.Point(26, 132);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(155, 25);
			this.button4.TabIndex = 4;
			this.button4.Text = "Land";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button3.Location = new System.Drawing.Point(26, 101);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(155, 25);
			this.button3.TabIndex = 3;
			this.button3.Text = "Takeoff";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(26, 70);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(155, 25);
			this.button2.TabIndex = 2;
			this.button2.Text = "Disarm";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(26, 39);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(155, 25);
			this.button1.TabIndex = 1;
			this.button1.Text = "Arm";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// opertaion_table_title
			// 
			this.opertaion_table_title.AutoSize = true;
			this.opertaion_table_title.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.opertaion_table_title.Location = new System.Drawing.Point(14, 11);
			this.opertaion_table_title.Name = "opertaion_table_title";
			this.opertaion_table_title.Size = new System.Drawing.Size(112, 15);
			this.opertaion_table_title.TabIndex = 0;
			this.opertaion_table_title.Text = "operation_table";
			// 
			// gmapControl
			// 
			this.gmapControl.Bearing = 0F;
			this.gmapControl.CanDragMap = true;
			this.gmapControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gmapControl.EmptyTileColor = System.Drawing.Color.Navy;
			this.gmapControl.GrayScaleMode = false;
			this.gmapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.gmapControl.LevelsKeepInMemmory = 5;
			this.gmapControl.Location = new System.Drawing.Point(0, 0);
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
			this.gmapControl.Size = new System.Drawing.Size(697, 595);
			this.gmapControl.TabIndex = 0;
			this.gmapControl.Zoom = 0D;
			this.gmapControl.Load += new System.EventHandler(this.gMapControl1_Load);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.gMapControl1);
			this.tabPage3.Location = new System.Drawing.Point(4, 24);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(1060, 603);
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
			this.gMapControl1.Location = new System.Drawing.Point(3, 3);
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
			this.gMapControl1.Size = new System.Drawing.Size(1054, 597);
			this.gMapControl1.TabIndex = 0;
			this.gMapControl1.Zoom = 0D;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 24);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(1060, 603);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Rotation";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1060, 603);
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
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1068, 655);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
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
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label drone_status_title;
		private System.Windows.Forms.Label drone_status_vspeed_value;
		private System.Windows.Forms.Label drone_status_gspeed_value;
		private System.Windows.Forms.Label drone_status_yaw_value;
		private System.Windows.Forms.Label drone_status_gps_value;
		private System.Windows.Forms.Label drone_status_altitude_value;
		private System.Windows.Forms.Label drone_status_battery_value;
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
		private System.Windows.Forms.Label drone_mode_value;
		private GMap.NET.WindowsForms.GMapControl gMapControl1;
		private System.Windows.Forms.Button btn_connect;
		private System.Windows.Forms.PrintDialog printDialog1;
	}
}

