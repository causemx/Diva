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
            this.LabelPowerModel = new System.Windows.Forms.Label();
            this.LabelStreamURIText = new System.Windows.Forms.Label();
            this.LabelBaudrateText = new System.Windows.Forms.Label();
            this.LabelPortNumberText = new System.Windows.Forms.Label();
            this.LabelPortNameText = new System.Windows.Forms.Label();
            this.LabelStreamURI = new System.Windows.Forms.Label();
            this.LabelBaudrate = new System.Windows.Forms.Label();
            this.LabelPortNumber = new System.Windows.Forms.Label();
            this.LabelPortName = new System.Windows.Forms.Label();
            this.PBDronePhoto = new System.Windows.Forms.PictureBox();
            this.LabelPowerModelText = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.TBoxDroneName = new System.Windows.Forms.TextBox();
            this.BtnDeaction = new System.Windows.Forms.Button();
            this.BtnAction = new System.Windows.Forms.Button();
            this.ChkDroneNameText = new System.Windows.Forms.CheckBox();
            this.EditPanel = new System.Windows.Forms.Panel();
            this.BtnNewModel = new System.Windows.Forms.Button();
            this.ComboPowerModel = new System.Windows.Forms.ComboBox();
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
            this.BottomPanel.Controls.Add(this.LabelPowerModel);
            this.BottomPanel.Controls.Add(this.LabelStreamURIText);
            this.BottomPanel.Controls.Add(this.LabelBaudrateText);
            this.BottomPanel.Controls.Add(this.LabelPortNumberText);
            this.BottomPanel.Controls.Add(this.LabelPortNameText);
            this.BottomPanel.Controls.Add(this.LabelStreamURI);
            this.BottomPanel.Controls.Add(this.LabelBaudrate);
            this.BottomPanel.Controls.Add(this.LabelPortNumber);
            this.BottomPanel.Controls.Add(this.LabelPortName);
            this.BottomPanel.Controls.Add(this.PBDronePhoto);
            this.BottomPanel.Controls.Add(this.LabelPowerModelText);
            this.BottomPanel.Location = new System.Drawing.Point(2, 51);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(358, 108);
            this.BottomPanel.TabIndex = 3;
            // 
            // LabelPowerModel
            // 
            this.LabelPowerModel.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPowerModel.ForeColor = System.Drawing.Color.White;
            this.LabelPowerModel.Location = new System.Drawing.Point(100, 79);
            this.LabelPowerModel.Name = "LabelPowerModel";
            this.LabelPowerModel.Size = new System.Drawing.Size(90, 14);
            this.LabelPowerModel.TabIndex = 8;
            this.LabelPowerModel.Text = "Power Model";
            this.LabelPowerModel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelStreamURIText
            // 
            this.LabelStreamURIText.AutoSize = true;
            this.LabelStreamURIText.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelStreamURIText.ForeColor = System.Drawing.Color.White;
            this.LabelStreamURIText.Location = new System.Drawing.Point(200, 56);
            this.LabelStreamURIText.Name = "LabelStreamURIText";
            this.LabelStreamURIText.Size = new System.Drawing.Size(0, 15);
            this.LabelStreamURIText.TabIndex = 7;
            // 
            // LabelBaudrateText
            // 
            this.LabelBaudrateText.AutoEllipsis = true;
            this.LabelBaudrateText.AutoSize = true;
            this.LabelBaudrateText.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBaudrateText.ForeColor = System.Drawing.Color.White;
            this.LabelBaudrateText.Location = new System.Drawing.Point(200, 33);
            this.LabelBaudrateText.Name = "LabelBaudrateText";
            this.LabelBaudrateText.Size = new System.Drawing.Size(0, 15);
            this.LabelBaudrateText.TabIndex = 6;
            this.LabelBaudrateText.Visible = false;
            // 
            // LabelPortNumberText
            // 
            this.LabelPortNumberText.AutoEllipsis = true;
            this.LabelPortNumberText.AutoSize = true;
            this.LabelPortNumberText.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPortNumberText.ForeColor = System.Drawing.Color.White;
            this.LabelPortNumberText.Location = new System.Drawing.Point(200, 33);
            this.LabelPortNumberText.Name = "LabelPortNumberText";
            this.LabelPortNumberText.Size = new System.Drawing.Size(0, 15);
            this.LabelPortNumberText.TabIndex = 5;
            // 
            // LabelPortNameText
            // 
            this.LabelPortNameText.AutoSize = true;
            this.LabelPortNameText.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPortNameText.ForeColor = System.Drawing.Color.White;
            this.LabelPortNameText.Location = new System.Drawing.Point(200, 10);
            this.LabelPortNameText.Name = "LabelPortNameText";
            this.LabelPortNameText.Size = new System.Drawing.Size(0, 15);
            this.LabelPortNameText.TabIndex = 5;
            // 
            // LabelStreamURI
            // 
            this.LabelStreamURI.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelStreamURI.ForeColor = System.Drawing.Color.White;
            this.LabelStreamURI.Location = new System.Drawing.Point(100, 56);
            this.LabelStreamURI.Name = "LabelStreamURI";
            this.LabelStreamURI.Size = new System.Drawing.Size(90, 14);
            this.LabelStreamURI.TabIndex = 4;
            this.LabelStreamURI.Text = "Video stream";
            this.LabelStreamURI.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelBaudrate
            // 
            this.LabelBaudrate.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBaudrate.ForeColor = System.Drawing.Color.White;
            this.LabelBaudrate.Location = new System.Drawing.Point(100, 33);
            this.LabelBaudrate.Name = "LabelBaudrate";
            this.LabelBaudrate.Size = new System.Drawing.Size(90, 14);
            this.LabelBaudrate.TabIndex = 3;
            this.LabelBaudrate.Text = "Baudrate";
            this.LabelBaudrate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.LabelBaudrate.Visible = false;
            // 
            // LabelPortNumber
            // 
            this.LabelPortNumber.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPortNumber.ForeColor = System.Drawing.Color.White;
            this.LabelPortNumber.Location = new System.Drawing.Point(100, 33);
            this.LabelPortNumber.Name = "LabelPortNumber";
            this.LabelPortNumber.Size = new System.Drawing.Size(90, 14);
            this.LabelPortNumber.TabIndex = 2;
            this.LabelPortNumber.Text = "Port number";
            this.LabelPortNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelPortName
            // 
            this.LabelPortName.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPortName.ForeColor = System.Drawing.Color.White;
            this.LabelPortName.Location = new System.Drawing.Point(100, 10);
            this.LabelPortName.Name = "LabelPortName";
            this.LabelPortName.Size = new System.Drawing.Size(90, 14);
            this.LabelPortName.TabIndex = 1;
            this.LabelPortName.Text = "Port name";
            this.LabelPortName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PBDronePhoto
            // 
            this.PBDronePhoto.BackColor = System.Drawing.Color.Transparent;
            this.PBDronePhoto.Image = ((System.Drawing.Image)(resources.GetObject("PBDronePhoto.Image")));
            this.PBDronePhoto.Location = new System.Drawing.Point(21, 14);
            this.PBDronePhoto.Name = "PBDronePhoto";
            this.PBDronePhoto.Size = new System.Drawing.Size(78, 78);
            this.PBDronePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PBDronePhoto.TabIndex = 0;
            this.PBDronePhoto.TabStop = false;
            // 
            // LabelPowerModelText
            // 
            this.LabelPowerModelText.AutoEllipsis = true;
            this.LabelPowerModelText.AutoSize = true;
            this.LabelPowerModelText.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPowerModelText.ForeColor = System.Drawing.Color.White;
            this.LabelPowerModelText.Location = new System.Drawing.Point(200, 79);
            this.LabelPowerModelText.Name = "LabelPowerModelText";
            this.LabelPowerModelText.Size = new System.Drawing.Size(47, 15);
            this.LabelPowerModelText.TabIndex = 9;
            this.LabelPowerModelText.Text = "(None)";
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Black;
            this.TopPanel.Controls.Add(this.TBoxDroneName);
            this.TopPanel.Controls.Add(this.BtnDeaction);
            this.TopPanel.Controls.Add(this.BtnAction);
            this.TopPanel.Controls.Add(this.ChkDroneNameText);
            this.TopPanel.Location = new System.Drawing.Point(2, 2);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(357, 48);
            this.TopPanel.TabIndex = 2;
            // 
            // TBoxDroneName
            // 
            this.TBoxDroneName.Font = new System.Drawing.Font("Georgia", 9F);
            this.TBoxDroneName.Location = new System.Drawing.Point(18, 14);
            this.TBoxDroneName.Margin = new System.Windows.Forms.Padding(2);
            this.TBoxDroneName.Name = "TBoxDroneName";
            this.TBoxDroneName.Size = new System.Drawing.Size(121, 21);
            this.TBoxDroneName.TabIndex = 3;
            this.TBoxDroneName.Visible = false;
            // 
            // BtnDeaction
            // 
            this.BtnDeaction.BackColor = System.Drawing.Color.Black;
            this.BtnDeaction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeaction.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeaction.ForeColor = System.Drawing.Color.White;
            this.BtnDeaction.Location = new System.Drawing.Point(276, 12);
            this.BtnDeaction.Name = "BtnDeaction";
            this.BtnDeaction.Size = new System.Drawing.Size(75, 23);
            this.BtnDeaction.TabIndex = 2;
            this.BtnDeaction.Text = "Remove";
            this.BtnDeaction.UseVisualStyleBackColor = false;
            this.BtnDeaction.Click += new System.EventHandler(this.BtnDeaction_Click);
            // 
            // BtnAction
            // 
            this.BtnAction.BackColor = System.Drawing.Color.Black;
            this.BtnAction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAction.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAction.ForeColor = System.Drawing.Color.White;
            this.BtnAction.Location = new System.Drawing.Point(195, 12);
            this.BtnAction.Name = "BtnAction";
            this.BtnAction.Size = new System.Drawing.Size(75, 23);
            this.BtnAction.TabIndex = 1;
            this.BtnAction.Text = "Edit";
            this.BtnAction.UseVisualStyleBackColor = false;
            this.BtnAction.Click += new System.EventHandler(this.BtnAction_Click);
            // 
            // ChkDroneNameText
            // 
            this.ChkDroneNameText.Checked = true;
            this.ChkDroneNameText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkDroneNameText.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDroneNameText.ForeColor = System.Drawing.Color.White;
            this.ChkDroneNameText.Location = new System.Drawing.Point(18, 16);
            this.ChkDroneNameText.Name = "ChkDroneNameText";
            this.ChkDroneNameText.Size = new System.Drawing.Size(120, 16);
            this.ChkDroneNameText.TabIndex = 0;
            this.ChkDroneNameText.UseVisualStyleBackColor = true;
            this.ChkDroneNameText.CheckedChanged += new System.EventHandler(this.ChkDroneNameText_CheckedChanged);
            // 
            // EditPanel
            // 
            this.EditPanel.BackColor = System.Drawing.Color.Black;
            this.EditPanel.Controls.Add(this.BtnNewModel);
            this.EditPanel.Controls.Add(this.ComboPowerModel);
            this.EditPanel.Controls.Add(this.TBoxComNo);
            this.EditPanel.Controls.Add(this.TBoxStreamURI);
            this.EditPanel.Controls.Add(this.TBoxPortValue);
            this.EditPanel.Controls.Add(this.RBSerial);
            this.EditPanel.Controls.Add(this.RBUdp);
            this.EditPanel.ForeColor = System.Drawing.Color.White;
            this.EditPanel.Location = new System.Drawing.Point(195, 51);
            this.EditPanel.Margin = new System.Windows.Forms.Padding(2);
            this.EditPanel.Name = "EditPanel";
            this.EditPanel.Size = new System.Drawing.Size(164, 107);
            this.EditPanel.TabIndex = 8;
            this.EditPanel.Visible = false;
            // 
            // BtnNewModel
            // 
            this.BtnNewModel.BackColor = System.Drawing.Color.Black;
            this.BtnNewModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNewModel.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNewModel.ForeColor = System.Drawing.Color.White;
            this.BtnNewModel.Location = new System.Drawing.Point(110, 75);
            this.BtnNewModel.Name = "BtnNewModel";
            this.BtnNewModel.Size = new System.Drawing.Size(40, 23);
            this.BtnNewModel.TabIndex = 6;
            this.BtnNewModel.Text = "New";
            this.BtnNewModel.UseVisualStyleBackColor = false;
            this.BtnNewModel.Click += new System.EventHandler(this.BtnNewModel_Click);
            // 
            // ComboPowerModel
            // 
            this.ComboPowerModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboPowerModel.Font = new System.Drawing.Font("Georgia", 9F);
            this.ComboPowerModel.FormattingEnabled = true;
            this.ComboPowerModel.Location = new System.Drawing.Point(10, 77);
            this.ComboPowerModel.Name = "ComboPowerModel";
            this.ComboPowerModel.Size = new System.Drawing.Size(90, 23);
            this.ComboPowerModel.TabIndex = 5;
            // 
            // TBoxComNo
            // 
            this.TBoxComNo.Enabled = false;
            this.TBoxComNo.Font = new System.Drawing.Font("Georgia", 9F);
            this.TBoxComNo.Location = new System.Drawing.Point(96, 8);
            this.TBoxComNo.Margin = new System.Windows.Forms.Padding(2);
            this.TBoxComNo.Name = "TBoxComNo";
            this.TBoxComNo.Size = new System.Drawing.Size(46, 21);
            this.TBoxComNo.TabIndex = 4;
            // 
            // TBoxStreamURI
            // 
            this.TBoxStreamURI.Font = new System.Drawing.Font("Georgia", 9F);
            this.TBoxStreamURI.Location = new System.Drawing.Point(10, 54);
            this.TBoxStreamURI.Margin = new System.Windows.Forms.Padding(2);
            this.TBoxStreamURI.Name = "TBoxStreamURI";
            this.TBoxStreamURI.Size = new System.Drawing.Size(91, 21);
            this.TBoxStreamURI.TabIndex = 3;
            // 
            // TBoxPortValue
            // 
            this.TBoxPortValue.Font = new System.Drawing.Font("Georgia", 9F);
            this.TBoxPortValue.Location = new System.Drawing.Point(10, 31);
            this.TBoxPortValue.Margin = new System.Windows.Forms.Padding(2);
            this.TBoxPortValue.Name = "TBoxPortValue";
            this.TBoxPortValue.Size = new System.Drawing.Size(91, 21);
            this.TBoxPortValue.TabIndex = 2;
            // 
            // RBSerial
            // 
            this.RBSerial.AutoSize = true;
            this.RBSerial.Font = new System.Drawing.Font("Georgia", 9F);
            this.RBSerial.Location = new System.Drawing.Point(45, 10);
            this.RBSerial.Margin = new System.Windows.Forms.Padding(2);
            this.RBSerial.Name = "RBSerial";
            this.RBSerial.Size = new System.Drawing.Size(53, 19);
            this.RBSerial.TabIndex = 1;
            this.RBSerial.TabStop = true;
            this.RBSerial.Text = "COM";
            this.RBSerial.UseVisualStyleBackColor = true;
            this.RBSerial.CheckedChanged += new System.EventHandler(this.RBPortType_CheckedChanged);
            // 
            // RBUdp
            // 
            this.RBUdp.AutoSize = true;
            this.RBUdp.Font = new System.Drawing.Font("Georgia", 9F);
            this.RBUdp.Location = new System.Drawing.Point(0, 10);
            this.RBUdp.Margin = new System.Windows.Forms.Padding(2);
            this.RBUdp.Name = "RBUdp";
            this.RBUdp.Size = new System.Drawing.Size(50, 19);
            this.RBUdp.TabIndex = 0;
            this.RBUdp.TabStop = true;
            this.RBUdp.Text = "UDP";
            this.RBUdp.UseVisualStyleBackColor = true;
            this.RBUdp.CheckedChanged += new System.EventHandler(this.RBPortType_CheckedChanged);
            // 
            // DroneSettingInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EditPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TopPanel);
            this.Name = "DroneSettingInput";
            this.Size = new System.Drawing.Size(360, 160);
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
        private System.Windows.Forms.Label LabelPowerModel;
        private System.Windows.Forms.Label LabelPowerModelText;
        private System.Windows.Forms.ComboBox ComboPowerModel;
        private System.Windows.Forms.Button BtnNewModel;
    }
}
