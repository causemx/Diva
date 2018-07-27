namespace Diva.Controls
{
    partial class ConfigMapPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnMapConfigReset = new System.Windows.Forms.Button();
            this.BtnMapConfigApply = new System.Windows.Forms.Button();
            this.PanelIndoorMapControls = new System.Windows.Forms.Panel();
            this.LabelImageMapLocation = new System.Windows.Forms.Label();
            this.TBoxIndoorMapLocation = new System.Windows.Forms.TextBox();
            this.BtnBrowseIndoorMap = new System.Windows.Forms.Button();
            this.RBtnIndoorMap = new System.Windows.Forms.RadioButton();
            this.PanelGlobalMapControls = new System.Windows.Forms.Panel();
            this.LabelInitialPosition = new System.Windows.Forms.Label();
            this.TBoxInitialZoom = new System.Windows.Forms.TextBox();
            this.LabelInitialZoom = new System.Windows.Forms.Label();
            this.LabelIPLongtitude = new System.Windows.Forms.Label();
            this.TBoxIPLongitude = new System.Windows.Forms.TextBox();
            this.LabelIPLatitude = new System.Windows.Forms.Label();
            this.TBoxIPLatitude = new System.Windows.Forms.TextBox();
            this.LabelMapCacheLocation = new System.Windows.Forms.Label();
            this.TBoxMapCacheLocation = new System.Windows.Forms.TextBox();
            this.BtnBrowseMapLocation = new System.Windows.Forms.Button();
            this.RBtnGlobalMap = new System.Windows.Forms.RadioButton();
            this.PanelIndoorMapControls.SuspendLayout();
            this.PanelGlobalMapControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnMapConfigReset
            // 
            this.BtnMapConfigReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMapConfigReset.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMapConfigReset.ForeColor = System.Drawing.Color.White;
            this.BtnMapConfigReset.Location = new System.Drawing.Point(650, 420);
            this.BtnMapConfigReset.Name = "BtnMapConfigReset";
            this.BtnMapConfigReset.Size = new System.Drawing.Size(120, 40);
            this.BtnMapConfigReset.TabIndex = 22;
            this.BtnMapConfigReset.Text = "Reset";
            this.BtnMapConfigReset.UseVisualStyleBackColor = true;
            this.BtnMapConfigReset.Click += new System.EventHandler(this.BtnMapConfigReset_Click);
            // 
            // BtnMapConfigApply
            // 
            this.BtnMapConfigApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMapConfigApply.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMapConfigApply.ForeColor = System.Drawing.Color.White;
            this.BtnMapConfigApply.Location = new System.Drawing.Point(500, 420);
            this.BtnMapConfigApply.Name = "BtnMapConfigApply";
            this.BtnMapConfigApply.Size = new System.Drawing.Size(120, 40);
            this.BtnMapConfigApply.TabIndex = 21;
            this.BtnMapConfigApply.Text = "Apply";
            this.BtnMapConfigApply.UseVisualStyleBackColor = true;
            this.BtnMapConfigApply.Click += new System.EventHandler(this.BtnMapConfigApply_Click);
            // 
            // PanelIndoorMapControls
            // 
            this.PanelIndoorMapControls.Controls.Add(this.LabelImageMapLocation);
            this.PanelIndoorMapControls.Controls.Add(this.TBoxIndoorMapLocation);
            this.PanelIndoorMapControls.Controls.Add(this.BtnBrowseIndoorMap);
            this.PanelIndoorMapControls.Location = new System.Drawing.Point(60, 320);
            this.PanelIndoorMapControls.Name = "PanelIndoorMapControls";
            this.PanelIndoorMapControls.Size = new System.Drawing.Size(760, 60);
            this.PanelIndoorMapControls.TabIndex = 20;
            // 
            // LabelImageMapLocation
            // 
            this.LabelImageMapLocation.AutoSize = true;
            this.LabelImageMapLocation.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelImageMapLocation.ForeColor = System.Drawing.Color.White;
            this.LabelImageMapLocation.Location = new System.Drawing.Point(0, 0);
            this.LabelImageMapLocation.Name = "LabelImageMapLocation";
            this.LabelImageMapLocation.Size = new System.Drawing.Size(204, 23);
            this.LabelImageMapLocation.TabIndex = 8;
            this.LabelImageMapLocation.Text = "Indoor map image file:";
            // 
            // TBoxIndoorMapLocation
            // 
            this.TBoxIndoorMapLocation.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxIndoorMapLocation.Location = new System.Drawing.Point(0, 30);
            this.TBoxIndoorMapLocation.Name = "TBoxIndoorMapLocation";
            this.TBoxIndoorMapLocation.Size = new System.Drawing.Size(600, 27);
            this.TBoxIndoorMapLocation.TabIndex = 9;
            this.TBoxIndoorMapLocation.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // BtnBrowseIndoorMap
            // 
            this.BtnBrowseIndoorMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrowseIndoorMap.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrowseIndoorMap.ForeColor = System.Drawing.Color.White;
            this.BtnBrowseIndoorMap.Location = new System.Drawing.Point(620, 20);
            this.BtnBrowseIndoorMap.Name = "BtnBrowseIndoorMap";
            this.BtnBrowseIndoorMap.Size = new System.Drawing.Size(120, 40);
            this.BtnBrowseIndoorMap.TabIndex = 10;
            this.BtnBrowseIndoorMap.Text = "Browse";
            this.BtnBrowseIndoorMap.UseVisualStyleBackColor = true;
            this.BtnBrowseIndoorMap.Click += new System.EventHandler(this.BtnBrowseIndoorMap_Click);
            // 
            // RBtnIndoorMap
            // 
            this.RBtnIndoorMap.AutoSize = true;
            this.RBtnIndoorMap.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBtnIndoorMap.ForeColor = System.Drawing.Color.White;
            this.RBtnIndoorMap.Location = new System.Drawing.Point(40, 280);
            this.RBtnIndoorMap.Name = "RBtnIndoorMap";
            this.RBtnIndoorMap.Size = new System.Drawing.Size(132, 27);
            this.RBtnIndoorMap.TabIndex = 19;
            this.RBtnIndoorMap.Text = "Indoor Map";
            this.RBtnIndoorMap.UseVisualStyleBackColor = true;
            this.RBtnIndoorMap.CheckedChanged += new System.EventHandler(this.MapControl_RadioCheckedChanged);
            // 
            // PanelGlobalMapControls
            // 
            this.PanelGlobalMapControls.Controls.Add(this.LabelInitialPosition);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxInitialZoom);
            this.PanelGlobalMapControls.Controls.Add(this.LabelInitialZoom);
            this.PanelGlobalMapControls.Controls.Add(this.LabelIPLongtitude);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxIPLongitude);
            this.PanelGlobalMapControls.Controls.Add(this.LabelIPLatitude);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxIPLatitude);
            this.PanelGlobalMapControls.Controls.Add(this.LabelMapCacheLocation);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxMapCacheLocation);
            this.PanelGlobalMapControls.Controls.Add(this.BtnBrowseMapLocation);
            this.PanelGlobalMapControls.ForeColor = System.Drawing.Color.Transparent;
            this.PanelGlobalMapControls.Location = new System.Drawing.Point(60, 80);
            this.PanelGlobalMapControls.Name = "PanelGlobalMapControls";
            this.PanelGlobalMapControls.Size = new System.Drawing.Size(760, 160);
            this.PanelGlobalMapControls.TabIndex = 18;
            // 
            // LabelInitialPosition
            // 
            this.LabelInitialPosition.AutoSize = true;
            this.LabelInitialPosition.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInitialPosition.ForeColor = System.Drawing.Color.White;
            this.LabelInitialPosition.Location = new System.Drawing.Point(0, 90);
            this.LabelInitialPosition.Name = "LabelInitialPosition";
            this.LabelInitialPosition.Size = new System.Drawing.Size(179, 23);
            this.LabelInitialPosition.TabIndex = 11;
            this.LabelInitialPosition.Text = "Initial map position";
            // 
            // TBoxInitialZoom
            // 
            this.TBoxInitialZoom.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxInitialZoom.Location = new System.Drawing.Point(310, 130);
            this.TBoxInitialZoom.Name = "TBoxInitialZoom";
            this.TBoxInitialZoom.Size = new System.Drawing.Size(80, 27);
            this.TBoxInitialZoom.TabIndex = 17;
            this.TBoxInitialZoom.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelInitialZoom
            // 
            this.LabelInitialZoom.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInitialZoom.ForeColor = System.Drawing.Color.White;
            this.LabelInitialZoom.Location = new System.Drawing.Point(100, 130);
            this.LabelInitialZoom.Name = "LabelInitialZoom";
            this.LabelInitialZoom.Size = new System.Drawing.Size(200, 23);
            this.LabelInitialZoom.TabIndex = 16;
            this.LabelInitialZoom.Text = "Initial zoom level:";
            this.LabelInitialZoom.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelIPLongtitude
            // 
            this.LabelIPLongtitude.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPLongtitude.ForeColor = System.Drawing.Color.White;
            this.LabelIPLongtitude.Location = new System.Drawing.Point(450, 90);
            this.LabelIPLongtitude.Name = "LabelIPLongtitude";
            this.LabelIPLongtitude.Size = new System.Drawing.Size(140, 23);
            this.LabelIPLongtitude.TabIndex = 15;
            this.LabelIPLongtitude.Text = "Longitude:";
            this.LabelIPLongtitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBoxIPLongitude
            // 
            this.TBoxIPLongitude.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxIPLongitude.Location = new System.Drawing.Point(600, 90);
            this.TBoxIPLongitude.Name = "TBoxIPLongitude";
            this.TBoxIPLongitude.Size = new System.Drawing.Size(150, 27);
            this.TBoxIPLongitude.TabIndex = 14;
            this.TBoxIPLongitude.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelIPLatitude
            // 
            this.LabelIPLatitude.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPLatitude.ForeColor = System.Drawing.Color.White;
            this.LabelIPLatitude.Location = new System.Drawing.Point(160, 90);
            this.LabelIPLatitude.Name = "LabelIPLatitude";
            this.LabelIPLatitude.Size = new System.Drawing.Size(140, 23);
            this.LabelIPLatitude.TabIndex = 13;
            this.LabelIPLatitude.Text = "Latitude:";
            this.LabelIPLatitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBoxIPLatitude
            // 
            this.TBoxIPLatitude.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxIPLatitude.Location = new System.Drawing.Point(310, 90);
            this.TBoxIPLatitude.Name = "TBoxIPLatitude";
            this.TBoxIPLatitude.Size = new System.Drawing.Size(150, 27);
            this.TBoxIPLatitude.TabIndex = 12;
            this.TBoxIPLatitude.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelMapCacheLocation
            // 
            this.LabelMapCacheLocation.AutoSize = true;
            this.LabelMapCacheLocation.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelMapCacheLocation.ForeColor = System.Drawing.Color.White;
            this.LabelMapCacheLocation.Location = new System.Drawing.Point(0, 0);
            this.LabelMapCacheLocation.Name = "LabelMapCacheLocation";
            this.LabelMapCacheLocation.Size = new System.Drawing.Size(628, 23);
            this.LabelMapCacheLocation.TabIndex = 8;
            this.LabelMapCacheLocation.Text = "Offline map cache file: (will be created under \'TileDBv5\\en\' subdirectory)";
            // 
            // TBoxMapCacheLocation
            // 
            this.TBoxMapCacheLocation.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxMapCacheLocation.Location = new System.Drawing.Point(0, 40);
            this.TBoxMapCacheLocation.Name = "TBoxMapCacheLocation";
            this.TBoxMapCacheLocation.Size = new System.Drawing.Size(600, 27);
            this.TBoxMapCacheLocation.TabIndex = 9;
            this.TBoxMapCacheLocation.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // BtnBrowseMapLocation
            // 
            this.BtnBrowseMapLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrowseMapLocation.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrowseMapLocation.ForeColor = System.Drawing.Color.White;
            this.BtnBrowseMapLocation.Location = new System.Drawing.Point(620, 30);
            this.BtnBrowseMapLocation.Name = "BtnBrowseMapLocation";
            this.BtnBrowseMapLocation.Size = new System.Drawing.Size(120, 40);
            this.BtnBrowseMapLocation.TabIndex = 10;
            this.BtnBrowseMapLocation.Text = "Browse";
            this.BtnBrowseMapLocation.UseVisualStyleBackColor = true;
            this.BtnBrowseMapLocation.Click += new System.EventHandler(this.BtnBrowseMapLocation_Click);
            // 
            // RBtnGlobalMap
            // 
            this.RBtnGlobalMap.AutoSize = true;
            this.RBtnGlobalMap.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBtnGlobalMap.ForeColor = System.Drawing.Color.White;
            this.RBtnGlobalMap.Location = new System.Drawing.Point(40, 40);
            this.RBtnGlobalMap.Name = "RBtnGlobalMap";
            this.RBtnGlobalMap.Size = new System.Drawing.Size(130, 27);
            this.RBtnGlobalMap.TabIndex = 17;
            this.RBtnGlobalMap.Text = "Global Map";
            this.RBtnGlobalMap.UseVisualStyleBackColor = true;
            this.RBtnGlobalMap.CheckedChanged += new System.EventHandler(this.MapControl_RadioCheckedChanged);
            // 
            // ConfigMapPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.BtnMapConfigReset);
            this.Controls.Add(this.BtnMapConfigApply);
            this.Controls.Add(this.PanelIndoorMapControls);
            this.Controls.Add(this.RBtnIndoorMap);
            this.Controls.Add(this.PanelGlobalMapControls);
            this.Controls.Add(this.RBtnGlobalMap);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ConfigMapPage";
            this.Size = new System.Drawing.Size(840, 480);
            this.PanelIndoorMapControls.ResumeLayout(false);
            this.PanelIndoorMapControls.PerformLayout();
            this.PanelGlobalMapControls.ResumeLayout(false);
            this.PanelGlobalMapControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnMapConfigReset;
        private System.Windows.Forms.Button BtnMapConfigApply;
        private System.Windows.Forms.Panel PanelIndoorMapControls;
        private System.Windows.Forms.Label LabelImageMapLocation;
        private System.Windows.Forms.TextBox TBoxIndoorMapLocation;
        private System.Windows.Forms.Button BtnBrowseIndoorMap;
        private System.Windows.Forms.RadioButton RBtnIndoorMap;
        private System.Windows.Forms.Panel PanelGlobalMapControls;
        private System.Windows.Forms.Label LabelInitialPosition;
        private System.Windows.Forms.TextBox TBoxInitialZoom;
        private System.Windows.Forms.Label LabelInitialZoom;
        private System.Windows.Forms.Label LabelIPLongtitude;
        private System.Windows.Forms.TextBox TBoxIPLongitude;
        private System.Windows.Forms.Label LabelIPLatitude;
        private System.Windows.Forms.TextBox TBoxIPLatitude;
        private System.Windows.Forms.Label LabelMapCacheLocation;
        private System.Windows.Forms.TextBox TBoxMapCacheLocation;
        private System.Windows.Forms.Button BtnBrowseMapLocation;
        private System.Windows.Forms.RadioButton RBtnGlobalMap;
    }
}
