
namespace Diva
{
    partial class Main
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTitlebar = new System.Windows.Forms.Panel();
            this.thePanel = new System.Windows.Forms.Panel();
            this.configButton = new Diva.Controls.Components.MyButton();
            this.connectButton = new Diva.Controls.Components.MyButton();
            this.iconButton5 = new Diva.Controls.Components.IconButton();
            this.iconButton2 = new Diva.Controls.Components.IconButton();
            this.iconButton1 = new Diva.Controls.Components.IconButton();
            this.dropdownMenuOperation = new Diva.Controls.Menu.DropdownMenu(this.components);
            this.throttleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeoffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.landMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dropdownMenuPlanning = new Diva.Controls.Menu.DropdownMenu(this.components);
            this.writeMissionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readMissionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMissionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMissionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTitlebar.SuspendLayout();
            this.dropdownMenuOperation.SuspendLayout();
            this.dropdownMenuPlanning.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.panelMenu.Controls.Add(this.iconButton5);
            this.panelMenu.Controls.Add(this.iconButton2);
            this.panelMenu.Controls.Add(this.iconButton1);
            this.panelMenu.Controls.Add(this.panel1);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(152, 601);
            this.panelMenu.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(152, 89);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Diva.Properties.Resources.icon_menu_48;
            this.pictureBox1.Location = new System.Drawing.Point(45, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelTitlebar
            // 
            this.panelTitlebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.panelTitlebar.Controls.Add(this.configButton);
            this.panelTitlebar.Controls.Add(this.connectButton);
            this.panelTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitlebar.Location = new System.Drawing.Point(152, 0);
            this.panelTitlebar.Name = "panelTitlebar";
            this.panelTitlebar.Size = new System.Drawing.Size(834, 66);
            this.panelTitlebar.TabIndex = 2;
            // 
            // thePanel
            // 
            this.thePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.thePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.thePanel.Location = new System.Drawing.Point(152, 66);
            this.thePanel.Name = "thePanel";
            this.thePanel.Size = new System.Drawing.Size(834, 535);
            this.thePanel.TabIndex = 3;
            // 
            // configButton
            // 
            this.configButton.Checked = false;
            this.configButton.CheckedImage = null;
            this.configButton.ClickBackColor = System.Drawing.Color.Empty;
            this.configButton.ClickForeColor = System.Drawing.Color.Empty;
            this.configButton.ClickImage = null;
            this.configButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.configButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.configButton.HoverBackColor = System.Drawing.Color.Empty;
            this.configButton.HoverForeColor = System.Drawing.Color.Empty;
            this.configButton.HoverImage = null;
            this.configButton.Image = global::Diva.Properties.Resources.icon_settings_7_32;
            this.configButton.Location = new System.Drawing.Point(75, 0);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(75, 66);
            this.configButton.TabIndex = 1;
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.MenuButtons_Click);
            // 
            // connectButton
            // 
            this.connectButton.Checked = false;
            this.connectButton.CheckedImage = global::Diva.Properties.Resources.icon_connected_32;
            this.connectButton.ClickBackColor = System.Drawing.Color.Empty;
            this.connectButton.ClickForeColor = System.Drawing.Color.Empty;
            this.connectButton.ClickImage = null;
            this.connectButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectButton.HoverBackColor = System.Drawing.Color.Empty;
            this.connectButton.HoverForeColor = System.Drawing.Color.Empty;
            this.connectButton.HoverImage = null;
            this.connectButton.Image = global::Diva.Properties.Resources.icon_connect_32;
            this.connectButton.Location = new System.Drawing.Point(0, 0);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 66);
            this.connectButton.TabIndex = 0;
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.MenuButtons_Click);
            // 
            // iconButton5
            // 
            this.iconButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.iconButton5.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.iconButton5.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.iconButton5.BorderRadius = 0;
            this.iconButton5.BorderSize = 0;
            this.iconButton5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.iconButton5.FlatAppearance.BorderSize = 0;
            this.iconButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton5.ForeColor = System.Drawing.Color.White;
            this.iconButton5.Image = global::Diva.Properties.Resources.icon_switch_2_24;
            this.iconButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton5.Location = new System.Drawing.Point(0, 561);
            this.iconButton5.Name = "iconButton5";
            this.iconButton5.Padding = new System.Windows.Forms.Padding(10, 0, 0, 10);
            this.iconButton5.Size = new System.Drawing.Size(152, 40);
            this.iconButton5.TabIndex = 7;
            this.iconButton5.Text = "Switch View";
            this.iconButton5.TextColor = System.Drawing.Color.White;
            this.iconButton5.UseVisualStyleBackColor = false;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.iconButton2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.iconButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.iconButton2.BorderRadius = 0;
            this.iconButton2.BorderSize = 0;
            this.iconButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton2.ForeColor = System.Drawing.Color.White;
            this.iconButton2.Image = global::Diva.Properties.Resources.icon_chess_knight_24;
            this.iconButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton2.Location = new System.Drawing.Point(0, 129);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButton2.Size = new System.Drawing.Size(152, 40);
            this.iconButton2.TabIndex = 4;
            this.iconButton2.Text = "Planning";
            this.iconButton2.TextColor = System.Drawing.Color.White;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.iconButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.iconButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.iconButton1.BorderRadius = 0;
            this.iconButton1.BorderSize = 0;
            this.iconButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.Image = global::Diva.Properties.Resources.icon_chess_castel_24;
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.Location = new System.Drawing.Point(0, 89);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.iconButton1.Size = new System.Drawing.Size(152, 40);
            this.iconButton1.TabIndex = 3;
            this.iconButton1.Text = "Operation";
            this.iconButton1.TextColor = System.Drawing.Color.White;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // dropdownMenuOperation
            // 
            this.dropdownMenuOperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.dropdownMenuOperation.IsMainMenu = false;
            this.dropdownMenuOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.throttleMenuItem,
            this.takeoffMenuItem,
            this.landMenuItem,
            this.autoMenuItem,
            this.rtlMenuItem});
            this.dropdownMenuOperation.MenuItemHeight = 25;
            this.dropdownMenuOperation.MenuItemTextColor = System.Drawing.Color.Empty;
            this.dropdownMenuOperation.Name = "dropdownMenu1";
            this.dropdownMenuOperation.PrimaryColor = System.Drawing.Color.Transparent;
            this.dropdownMenuOperation.Size = new System.Drawing.Size(116, 114);
            // 
            // throttleMenuItem
            // 
            this.throttleMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.throttleMenuItem.ForeColor = System.Drawing.Color.White;
            this.throttleMenuItem.Name = "throttleMenuItem";
            this.throttleMenuItem.Size = new System.Drawing.Size(115, 22);
            this.throttleMenuItem.Text = "Throttle";
            this.throttleMenuItem.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // takeoffMenuItem
            // 
            this.takeoffMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.takeoffMenuItem.ForeColor = System.Drawing.Color.White;
            this.takeoffMenuItem.Name = "takeoffMenuItem";
            this.takeoffMenuItem.Size = new System.Drawing.Size(115, 22);
            this.takeoffMenuItem.Text = "Takeoff";
            this.takeoffMenuItem.Visible = false;
            this.takeoffMenuItem.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // landMenuItem
            // 
            this.landMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.landMenuItem.ForeColor = System.Drawing.Color.White;
            this.landMenuItem.Name = "landMenuItem";
            this.landMenuItem.Size = new System.Drawing.Size(115, 22);
            this.landMenuItem.Text = "Land";
            this.landMenuItem.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // autoMenuItem
            // 
            this.autoMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoMenuItem.ForeColor = System.Drawing.Color.White;
            this.autoMenuItem.Name = "autoMenuItem";
            this.autoMenuItem.Size = new System.Drawing.Size(115, 22);
            this.autoMenuItem.Text = "Auto";
            this.autoMenuItem.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // rtlMenuItem
            // 
            this.rtlMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtlMenuItem.ForeColor = System.Drawing.Color.White;
            this.rtlMenuItem.Name = "rtlMenuItem";
            this.rtlMenuItem.Size = new System.Drawing.Size(115, 22);
            this.rtlMenuItem.Text = "RTL";
            this.rtlMenuItem.Click += new System.EventHandler(this.MenuItems_Click);
            // 
            // dropdownMenuPlanning
            // 
            this.dropdownMenuPlanning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.dropdownMenuPlanning.IsMainMenu = false;
            this.dropdownMenuPlanning.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.writeMissionMenuItem,
            this.readMissionMenuItem,
            this.importMissionMenuItem,
            this.exportMissionMenuItem});
            this.dropdownMenuPlanning.MenuItemHeight = 25;
            this.dropdownMenuPlanning.MenuItemTextColor = System.Drawing.Color.Empty;
            this.dropdownMenuPlanning.Name = "dropdownMenuPlanning";
            this.dropdownMenuPlanning.PrimaryColor = System.Drawing.Color.Empty;
            this.dropdownMenuPlanning.Size = new System.Drawing.Size(156, 92);
            // 
            // writeMissionMenuItem
            // 
            this.writeMissionMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeMissionMenuItem.ForeColor = System.Drawing.Color.White;
            this.writeMissionMenuItem.Name = "writeMissionMenuItem";
            this.writeMissionMenuItem.Size = new System.Drawing.Size(155, 22);
            this.writeMissionMenuItem.Text = "Write Mission";
            // 
            // readMissionMenuItem
            // 
            this.readMissionMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readMissionMenuItem.ForeColor = System.Drawing.Color.White;
            this.readMissionMenuItem.Name = "readMissionMenuItem";
            this.readMissionMenuItem.Size = new System.Drawing.Size(155, 22);
            this.readMissionMenuItem.Text = "Read Mission";
            // 
            // importMissionMenuItem
            // 
            this.importMissionMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importMissionMenuItem.ForeColor = System.Drawing.Color.White;
            this.importMissionMenuItem.Name = "importMissionMenuItem";
            this.importMissionMenuItem.Size = new System.Drawing.Size(155, 22);
            this.importMissionMenuItem.Text = "Import Mission";
            // 
            // exportMissionMenuItem
            // 
            this.exportMissionMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportMissionMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportMissionMenuItem.Name = "exportMissionMenuItem";
            this.exportMissionMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exportMissionMenuItem.Text = "Export Mission";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 601);
            this.Controls.Add(this.thePanel);
            this.Controls.Add(this.panelTitlebar);
            this.Controls.Add(this.panelMenu);
            this.Name = "Main";
            this.Text = "Main";
            this.panelMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTitlebar.ResumeLayout(false);
            this.dropdownMenuOperation.ResumeLayout(false);
            this.dropdownMenuPlanning.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTitlebar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Controls.Components.IconButton iconButton5;
        private Controls.Components.IconButton iconButton2;
        private System.Windows.Forms.Panel panel1;
        private Controls.Menu.DropdownMenu dropdownMenuOperation;
        private System.Windows.Forms.ToolStripMenuItem throttleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeoffMenuItem;
        private System.Windows.Forms.ToolStripMenuItem landMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rtlMenuItem;
        private Controls.Menu.DropdownMenu dropdownMenuPlanning;
        private System.Windows.Forms.ToolStripMenuItem writeMissionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readMissionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMissionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMissionMenuItem;
        private System.Windows.Forms.Panel thePanel;
        private Controls.Components.IconButton iconButton1;
        private Controls.Components.MyButton configButton;
        private Controls.Components.MyButton connectButton;
    }
}