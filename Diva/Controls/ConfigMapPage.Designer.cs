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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigMapPage));
            this.BtnMapConfigReset = new System.Windows.Forms.Button();
            this.BtnMapConfigApply = new System.Windows.Forms.Button();
            this.PanelIndoorMapControls = new System.Windows.Forms.Panel();
            this.LabelOriginGeolocation = new System.Windows.Forms.Label();
            this.LabelOGLongitude = new System.Windows.Forms.Label();
            this.TBoxOGLongitude = new System.Windows.Forms.TextBox();
            this.LabelOGLatitude = new System.Windows.Forms.Label();
            this.TBoxOGLatitude = new System.Windows.Forms.TextBox();
            this.LabelImageMapLocation = new System.Windows.Forms.Label();
            this.TBoxIndoorMapLocation = new System.Windows.Forms.TextBox();
            this.BtnBrowseIndoorMap = new System.Windows.Forms.Button();
            this.RBtnIndoorMap = new System.Windows.Forms.RadioButton();
            this.PanelGlobalMapControls = new System.Windows.Forms.Panel();
            this.TBoxProxyPort = new System.Windows.Forms.TextBox();
            this.LabelProxyPort = new System.Windows.Forms.Label();
            this.TBoxProxyHost = new System.Windows.Forms.TextBox();
            this.RBtnProxyCustom = new System.Windows.Forms.RadioButton();
            this.RBtnProxySystem = new System.Windows.Forms.RadioButton();
            this.LabelProxySetting = new System.Windows.Forms.Label();
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
            resources.ApplyResources(this.BtnMapConfigReset, "BtnMapConfigReset");
            this.BtnMapConfigReset.ForeColor = System.Drawing.Color.White;
            this.BtnMapConfigReset.Name = "BtnMapConfigReset";
            this.BtnMapConfigReset.UseVisualStyleBackColor = true;
            this.BtnMapConfigReset.Click += new System.EventHandler(this.BtnMapConfigReset_Click);
            // 
            // BtnMapConfigApply
            // 
            resources.ApplyResources(this.BtnMapConfigApply, "BtnMapConfigApply");
            this.BtnMapConfigApply.ForeColor = System.Drawing.Color.White;
            this.BtnMapConfigApply.Name = "BtnMapConfigApply";
            this.BtnMapConfigApply.UseVisualStyleBackColor = true;
            this.BtnMapConfigApply.Click += new System.EventHandler(this.BtnMapConfigApply_Click);
            // 
            // PanelIndoorMapControls
            // 
            resources.ApplyResources(this.PanelIndoorMapControls, "PanelIndoorMapControls");
            this.PanelIndoorMapControls.Controls.Add(this.LabelOriginGeolocation);
            this.PanelIndoorMapControls.Controls.Add(this.LabelOGLongitude);
            this.PanelIndoorMapControls.Controls.Add(this.TBoxOGLongitude);
            this.PanelIndoorMapControls.Controls.Add(this.LabelOGLatitude);
            this.PanelIndoorMapControls.Controls.Add(this.TBoxOGLatitude);
            this.PanelIndoorMapControls.Controls.Add(this.LabelImageMapLocation);
            this.PanelIndoorMapControls.Controls.Add(this.TBoxIndoorMapLocation);
            this.PanelIndoorMapControls.Controls.Add(this.BtnBrowseIndoorMap);
            this.PanelIndoorMapControls.Name = "PanelIndoorMapControls";
            // 
            // LabelOriginGeolocation
            // 
            resources.ApplyResources(this.LabelOriginGeolocation, "LabelOriginGeolocation");
            this.LabelOriginGeolocation.ForeColor = System.Drawing.Color.White;
            this.LabelOriginGeolocation.Name = "LabelOriginGeolocation";
            // 
            // LabelOGLongitude
            // 
            resources.ApplyResources(this.LabelOGLongitude, "LabelOGLongitude");
            this.LabelOGLongitude.ForeColor = System.Drawing.Color.White;
            this.LabelOGLongitude.Name = "LabelOGLongitude";
            // 
            // TBoxOGLongitude
            // 
            resources.ApplyResources(this.TBoxOGLongitude, "TBoxOGLongitude");
            this.TBoxOGLongitude.Name = "TBoxOGLongitude";
            // 
            // LabelOGLatitude
            // 
            resources.ApplyResources(this.LabelOGLatitude, "LabelOGLatitude");
            this.LabelOGLatitude.ForeColor = System.Drawing.Color.White;
            this.LabelOGLatitude.Name = "LabelOGLatitude";
            // 
            // TBoxOGLatitude
            // 
            resources.ApplyResources(this.TBoxOGLatitude, "TBoxOGLatitude");
            this.TBoxOGLatitude.Name = "TBoxOGLatitude";
            // 
            // LabelImageMapLocation
            // 
            resources.ApplyResources(this.LabelImageMapLocation, "LabelImageMapLocation");
            this.LabelImageMapLocation.ForeColor = System.Drawing.Color.White;
            this.LabelImageMapLocation.Name = "LabelImageMapLocation";
            // 
            // TBoxIndoorMapLocation
            // 
            resources.ApplyResources(this.TBoxIndoorMapLocation, "TBoxIndoorMapLocation");
            this.TBoxIndoorMapLocation.Name = "TBoxIndoorMapLocation";
            this.TBoxIndoorMapLocation.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // BtnBrowseIndoorMap
            // 
            resources.ApplyResources(this.BtnBrowseIndoorMap, "BtnBrowseIndoorMap");
            this.BtnBrowseIndoorMap.ForeColor = System.Drawing.Color.White;
            this.BtnBrowseIndoorMap.Name = "BtnBrowseIndoorMap";
            this.BtnBrowseIndoorMap.UseVisualStyleBackColor = true;
            this.BtnBrowseIndoorMap.Click += new System.EventHandler(this.BtnBrowseIndoorMap_Click);
            // 
            // RBtnIndoorMap
            // 
            resources.ApplyResources(this.RBtnIndoorMap, "RBtnIndoorMap");
            this.RBtnIndoorMap.ForeColor = System.Drawing.Color.White;
            this.RBtnIndoorMap.Name = "RBtnIndoorMap";
            this.RBtnIndoorMap.UseVisualStyleBackColor = true;
            this.RBtnIndoorMap.CheckedChanged += new System.EventHandler(this.MapControl_RadioCheckedChanged);
            // 
            // PanelGlobalMapControls
            // 
            resources.ApplyResources(this.PanelGlobalMapControls, "PanelGlobalMapControls");
            this.PanelGlobalMapControls.Controls.Add(this.TBoxProxyPort);
            this.PanelGlobalMapControls.Controls.Add(this.LabelProxyPort);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxProxyHost);
            this.PanelGlobalMapControls.Controls.Add(this.RBtnProxyCustom);
            this.PanelGlobalMapControls.Controls.Add(this.RBtnProxySystem);
            this.PanelGlobalMapControls.Controls.Add(this.LabelProxySetting);
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
            this.PanelGlobalMapControls.Name = "PanelGlobalMapControls";
            // 
            // TBoxProxyPort
            // 
            resources.ApplyResources(this.TBoxProxyPort, "TBoxProxyPort");
            this.TBoxProxyPort.Name = "TBoxProxyPort";
            this.TBoxProxyPort.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelProxyPort
            // 
            resources.ApplyResources(this.LabelProxyPort, "LabelProxyPort");
            this.LabelProxyPort.ForeColor = System.Drawing.Color.White;
            this.LabelProxyPort.Name = "LabelProxyPort";
            // 
            // TBoxProxyHost
            // 
            resources.ApplyResources(this.TBoxProxyHost, "TBoxProxyHost");
            this.TBoxProxyHost.Name = "TBoxProxyHost";
            this.TBoxProxyHost.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // RBtnProxyCustom
            // 
            resources.ApplyResources(this.RBtnProxyCustom, "RBtnProxyCustom");
            this.RBtnProxyCustom.ForeColor = System.Drawing.Color.White;
            this.RBtnProxyCustom.Name = "RBtnProxyCustom";
            this.RBtnProxyCustom.UseVisualStyleBackColor = true;
            this.RBtnProxyCustom.CheckedChanged += new System.EventHandler(this.ProxySetting_RadioCheckedChanged);
            // 
            // RBtnProxySystem
            // 
            resources.ApplyResources(this.RBtnProxySystem, "RBtnProxySystem");
            this.RBtnProxySystem.ForeColor = System.Drawing.Color.White;
            this.RBtnProxySystem.Name = "RBtnProxySystem";
            this.RBtnProxySystem.UseVisualStyleBackColor = true;
            this.RBtnProxySystem.CheckedChanged += new System.EventHandler(this.ProxySetting_RadioCheckedChanged);
            // 
            // LabelProxySetting
            // 
            resources.ApplyResources(this.LabelProxySetting, "LabelProxySetting");
            this.LabelProxySetting.ForeColor = System.Drawing.Color.White;
            this.LabelProxySetting.Name = "LabelProxySetting";
            // 
            // LabelInitialPosition
            // 
            resources.ApplyResources(this.LabelInitialPosition, "LabelInitialPosition");
            this.LabelInitialPosition.ForeColor = System.Drawing.Color.White;
            this.LabelInitialPosition.Name = "LabelInitialPosition";
            // 
            // TBoxInitialZoom
            // 
            resources.ApplyResources(this.TBoxInitialZoom, "TBoxInitialZoom");
            this.TBoxInitialZoom.Name = "TBoxInitialZoom";
            this.TBoxInitialZoom.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelInitialZoom
            // 
            resources.ApplyResources(this.LabelInitialZoom, "LabelInitialZoom");
            this.LabelInitialZoom.ForeColor = System.Drawing.Color.White;
            this.LabelInitialZoom.Name = "LabelInitialZoom";
            // 
            // LabelIPLongtitude
            // 
            resources.ApplyResources(this.LabelIPLongtitude, "LabelIPLongtitude");
            this.LabelIPLongtitude.ForeColor = System.Drawing.Color.White;
            this.LabelIPLongtitude.Name = "LabelIPLongtitude";
            // 
            // TBoxIPLongitude
            // 
            resources.ApplyResources(this.TBoxIPLongitude, "TBoxIPLongitude");
            this.TBoxIPLongitude.Name = "TBoxIPLongitude";
            this.TBoxIPLongitude.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelIPLatitude
            // 
            resources.ApplyResources(this.LabelIPLatitude, "LabelIPLatitude");
            this.LabelIPLatitude.ForeColor = System.Drawing.Color.White;
            this.LabelIPLatitude.Name = "LabelIPLatitude";
            // 
            // TBoxIPLatitude
            // 
            resources.ApplyResources(this.TBoxIPLatitude, "TBoxIPLatitude");
            this.TBoxIPLatitude.Name = "TBoxIPLatitude";
            this.TBoxIPLatitude.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelMapCacheLocation
            // 
            resources.ApplyResources(this.LabelMapCacheLocation, "LabelMapCacheLocation");
            this.LabelMapCacheLocation.ForeColor = System.Drawing.Color.White;
            this.LabelMapCacheLocation.Name = "LabelMapCacheLocation";
            // 
            // TBoxMapCacheLocation
            // 
            resources.ApplyResources(this.TBoxMapCacheLocation, "TBoxMapCacheLocation");
            this.TBoxMapCacheLocation.Name = "TBoxMapCacheLocation";
            this.TBoxMapCacheLocation.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // BtnBrowseMapLocation
            // 
            resources.ApplyResources(this.BtnBrowseMapLocation, "BtnBrowseMapLocation");
            this.BtnBrowseMapLocation.ForeColor = System.Drawing.Color.White;
            this.BtnBrowseMapLocation.Name = "BtnBrowseMapLocation";
            this.BtnBrowseMapLocation.UseVisualStyleBackColor = true;
            this.BtnBrowseMapLocation.Click += new System.EventHandler(this.BtnBrowseMapLocation_Click);
            // 
            // RBtnGlobalMap
            // 
            resources.ApplyResources(this.RBtnGlobalMap, "RBtnGlobalMap");
            this.RBtnGlobalMap.Checked = true;
            this.RBtnGlobalMap.ForeColor = System.Drawing.Color.White;
            this.RBtnGlobalMap.Name = "RBtnGlobalMap";
            this.RBtnGlobalMap.TabStop = true;
            this.RBtnGlobalMap.UseVisualStyleBackColor = true;
            this.RBtnGlobalMap.CheckedChanged += new System.EventHandler(this.MapControl_RadioCheckedChanged);
            // 
            // ConfigMapPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.BtnMapConfigReset);
            this.Controls.Add(this.BtnMapConfigApply);
            this.Controls.Add(this.PanelIndoorMapControls);
            this.Controls.Add(this.RBtnIndoorMap);
            this.Controls.Add(this.PanelGlobalMapControls);
            this.Controls.Add(this.RBtnGlobalMap);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ConfigMapPage";
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
        private System.Windows.Forms.TextBox TBoxProxyPort;
        private System.Windows.Forms.Label LabelProxyPort;
        private System.Windows.Forms.TextBox TBoxProxyHost;
        private System.Windows.Forms.RadioButton RBtnProxyCustom;
        private System.Windows.Forms.RadioButton RBtnProxySystem;
        private System.Windows.Forms.Label LabelProxySetting;
        private System.Windows.Forms.Label LabelOriginGeolocation;
        private System.Windows.Forms.Label LabelOGLongitude;
        private System.Windows.Forms.TextBox TBoxOGLongitude;
        private System.Windows.Forms.Label LabelOGLatitude;
        private System.Windows.Forms.TextBox TBoxOGLatitude;
    }
}
