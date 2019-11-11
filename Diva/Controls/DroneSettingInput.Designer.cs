namespace Diva.Controls
{
    partial class DroneSettingInput
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DroneSettingInput));
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.LabelStreamURIText = new System.Windows.Forms.Label();
            this.LabelBaudrateText = new System.Windows.Forms.Label();
            this.LabelPortNumberText = new System.Windows.Forms.Label();
            this.LabelPortNameText = new System.Windows.Forms.Label();
            this.LabelStreamURI = new System.Windows.Forms.Label();
            this.LabelBaudrate = new System.Windows.Forms.Label();
            this.LabelPortNumber = new System.Windows.Forms.Label();
            this.LabelPortName = new System.Windows.Forms.Label();
            this.PBDronePhoto = new System.Windows.Forms.PictureBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.TBoxDroneName = new System.Windows.Forms.TextBox();
            this.BtnDeaction = new System.Windows.Forms.Button();
            this.BtnAction = new System.Windows.Forms.Button();
            this.ChkDroneNameText = new System.Windows.Forms.CheckBox();
            this.EditPanel = new System.Windows.Forms.Panel();
            this.TBoxComNo = new System.Windows.Forms.TextBox();
            this.TBoxStreamURI = new System.Windows.Forms.TextBox();
            this.TBoxPortValue = new System.Windows.Forms.TextBox();
            this.RBSerial = new System.Windows.Forms.RadioButton();
            this.RBUdp = new System.Windows.Forms.RadioButton();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBDronePhoto)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.EditPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackColor = System.Drawing.Color.Black;
            this.BottomPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BottomPanel.Controls.Add(this.LabelStreamURIText);
            this.BottomPanel.Controls.Add(this.LabelBaudrateText);
            this.BottomPanel.Controls.Add(this.LabelPortNumberText);
            this.BottomPanel.Controls.Add(this.LabelPortNameText);
            this.BottomPanel.Controls.Add(this.LabelStreamURI);
            this.BottomPanel.Controls.Add(this.LabelBaudrate);
            this.BottomPanel.Controls.Add(this.LabelPortNumber);
            this.BottomPanel.Controls.Add(this.LabelPortName);
            this.BottomPanel.Controls.Add(this.PBDronePhoto);
            resources.ApplyResources(this.BottomPanel, "BottomPanel");
            this.BottomPanel.Name = "BottomPanel";
            // 
            // LabelStreamURIText
            // 
            resources.ApplyResources(this.LabelStreamURIText, "LabelStreamURIText");
            this.LabelStreamURIText.ForeColor = System.Drawing.Color.White;
            this.LabelStreamURIText.Name = "LabelStreamURIText";
            // 
            // LabelBaudrateText
            // 
            this.LabelBaudrateText.AutoEllipsis = true;
            resources.ApplyResources(this.LabelBaudrateText, "LabelBaudrateText");
            this.LabelBaudrateText.ForeColor = System.Drawing.Color.White;
            this.LabelBaudrateText.Name = "LabelBaudrateText";
            // 
            // LabelPortNumberText
            // 
            this.LabelPortNumberText.AutoEllipsis = true;
            resources.ApplyResources(this.LabelPortNumberText, "LabelPortNumberText");
            this.LabelPortNumberText.ForeColor = System.Drawing.Color.White;
            this.LabelPortNumberText.Name = "LabelPortNumberText";
            // 
            // LabelPortNameText
            // 
            resources.ApplyResources(this.LabelPortNameText, "LabelPortNameText");
            this.LabelPortNameText.ForeColor = System.Drawing.Color.White;
            this.LabelPortNameText.Name = "LabelPortNameText";
            // 
            // LabelStreamURI
            // 
            resources.ApplyResources(this.LabelStreamURI, "LabelStreamURI");
            this.LabelStreamURI.ForeColor = System.Drawing.Color.White;
            this.LabelStreamURI.Name = "LabelStreamURI";
            // 
            // LabelBaudrate
            // 
            resources.ApplyResources(this.LabelBaudrate, "LabelBaudrate");
            this.LabelBaudrate.ForeColor = System.Drawing.Color.White;
            this.LabelBaudrate.Name = "LabelBaudrate";
            // 
            // LabelPortNumber
            // 
            resources.ApplyResources(this.LabelPortNumber, "LabelPortNumber");
            this.LabelPortNumber.ForeColor = System.Drawing.Color.White;
            this.LabelPortNumber.Name = "LabelPortNumber";
            // 
            // LabelPortName
            // 
            resources.ApplyResources(this.LabelPortName, "LabelPortName");
            this.LabelPortName.ForeColor = System.Drawing.Color.White;
            this.LabelPortName.Name = "LabelPortName";
            // 
            // PBDronePhoto
            // 
            this.PBDronePhoto.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.PBDronePhoto, "PBDronePhoto");
            this.PBDronePhoto.Name = "PBDronePhoto";
            this.PBDronePhoto.TabStop = false;
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Black;
            this.TopPanel.Controls.Add(this.TBoxDroneName);
            this.TopPanel.Controls.Add(this.BtnDeaction);
            this.TopPanel.Controls.Add(this.BtnAction);
            this.TopPanel.Controls.Add(this.ChkDroneNameText);
            resources.ApplyResources(this.TopPanel, "TopPanel");
            this.TopPanel.Name = "TopPanel";
            // 
            // TBoxDroneName
            // 
            resources.ApplyResources(this.TBoxDroneName, "TBoxDroneName");
            this.TBoxDroneName.Name = "TBoxDroneName";
            // 
            // BtnDeaction
            // 
            this.BtnDeaction.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnDeaction, "BtnDeaction");
            this.BtnDeaction.ForeColor = System.Drawing.Color.White;
            this.BtnDeaction.Name = "BtnDeaction";
            this.BtnDeaction.UseVisualStyleBackColor = false;
            this.BtnDeaction.Click += new System.EventHandler(this.BtnDeaction_Click);
            // 
            // BtnAction
            // 
            this.BtnAction.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.BtnAction, "BtnAction");
            this.BtnAction.ForeColor = System.Drawing.Color.White;
            this.BtnAction.Name = "BtnAction";
            this.BtnAction.UseVisualStyleBackColor = false;
            this.BtnAction.Click += new System.EventHandler(this.BtnAction_Click);
            // 
            // ChkDroneNameText
            // 
            this.ChkDroneNameText.Checked = true;
            this.ChkDroneNameText.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.ChkDroneNameText, "ChkDroneNameText");
            this.ChkDroneNameText.ForeColor = System.Drawing.Color.White;
            this.ChkDroneNameText.Name = "ChkDroneNameText";
            this.ChkDroneNameText.UseVisualStyleBackColor = true;
            this.ChkDroneNameText.CheckedChanged += new System.EventHandler(this.ChkDroneNameText_CheckedChanged);
            // 
            // EditPanel
            // 
            this.EditPanel.BackColor = System.Drawing.Color.Black;
            this.EditPanel.Controls.Add(this.TBoxComNo);
            this.EditPanel.Controls.Add(this.TBoxStreamURI);
            this.EditPanel.Controls.Add(this.TBoxPortValue);
            this.EditPanel.Controls.Add(this.RBSerial);
            this.EditPanel.Controls.Add(this.RBUdp);
            this.EditPanel.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.EditPanel, "EditPanel");
            this.EditPanel.Name = "EditPanel";
            // 
            // TBoxComNo
            // 
            resources.ApplyResources(this.TBoxComNo, "TBoxComNo");
            this.TBoxComNo.Name = "TBoxComNo";
            // 
            // TBoxStreamURI
            // 
            resources.ApplyResources(this.TBoxStreamURI, "TBoxStreamURI");
            this.TBoxStreamURI.Name = "TBoxStreamURI";
            // 
            // TBoxPortValue
            // 
            resources.ApplyResources(this.TBoxPortValue, "TBoxPortValue");
            this.TBoxPortValue.Name = "TBoxPortValue";
            // 
            // RBSerial
            // 
            resources.ApplyResources(this.RBSerial, "RBSerial");
            this.RBSerial.Name = "RBSerial";
            this.RBSerial.TabStop = true;
            this.RBSerial.UseVisualStyleBackColor = true;
            this.RBSerial.CheckedChanged += new System.EventHandler(this.RBPortType_CheckedChanged);
            // 
            // RBUdp
            // 
            resources.ApplyResources(this.RBUdp, "RBUdp");
            this.RBUdp.Name = "RBUdp";
            this.RBUdp.TabStop = true;
            this.RBUdp.UseVisualStyleBackColor = true;
            this.RBUdp.CheckedChanged += new System.EventHandler(this.RBPortType_CheckedChanged);
            // 
            // DroneSettingInput
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EditPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TopPanel);
            this.Name = "DroneSettingInput";
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBDronePhoto)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.EditPanel.ResumeLayout(false);
            this.EditPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Label LabelStreamURIText;
        private System.Windows.Forms.Label LabelBaudrateText;
        private System.Windows.Forms.Label LabelPortNumberText;
        private System.Windows.Forms.Label LabelPortNameText;
        private System.Windows.Forms.Label LabelStreamURI;
        private System.Windows.Forms.Label LabelBaudrate;
        private System.Windows.Forms.Label LabelPortNumber;
        private System.Windows.Forms.Label LabelPortName;
        private System.Windows.Forms.PictureBox PBDronePhoto;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button BtnAction;
        private System.Windows.Forms.Button BtnDeaction;
        private System.Windows.Forms.TextBox TBoxDroneName;
        private System.Windows.Forms.Panel EditPanel;
        private System.Windows.Forms.TextBox TBoxPortValue;
        private System.Windows.Forms.RadioButton RBSerial;
        private System.Windows.Forms.RadioButton RBUdp;
        private System.Windows.Forms.TextBox TBoxStreamURI;
        private System.Windows.Forms.TextBox TBoxComNo;
        private System.Windows.Forms.CheckBox ChkDroneNameText;
    }
}
