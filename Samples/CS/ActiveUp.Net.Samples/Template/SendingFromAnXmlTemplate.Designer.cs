namespace ActiveUp.Net.Samples.Template
{
    partial class SendingFromAnXmlTemplate
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
            this.smtpServerAddressTextbox = new System.Windows.Forms.TextBox();
            this.smtpServerAddressLabel = new System.Windows.Forms.Label();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this._cbUseSmtpFromTemplate = new System.Windows.Forms.CheckBox();
            this._tbXmlTemplate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._bAddXmlTemplate = new System.Windows.Forms.Button();
            this._ofdXmlTemplate = new System.Windows.Forms.OpenFileDialog();
            this._bShowTemplate = new System.Windows.Forms.Button();
            this._bShowSource = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._bShowSource);
            this.splitContainer1.Panel1.Controls.Add(this._bShowTemplate);
            this.splitContainer1.Panel1.Controls.Add(this._bAddXmlTemplate);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this._tbXmlTemplate);
            this.splitContainer1.Panel1.Controls.Add(this._cbUseSmtpFromTemplate);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressLabel);
            this.splitContainer1.Panel1.Controls.Add(this.sendMessageButton);
            this.splitContainer1.TabIndex = 1;
            // 
            // smtpServerAddressTextbox
            // 
            this.smtpServerAddressTextbox.Location = new System.Drawing.Point(12, 52);
            this.smtpServerAddressTextbox.Name = "smtpServerAddressTextbox";
            this.smtpServerAddressTextbox.Size = new System.Drawing.Size(273, 20);
            this.smtpServerAddressTextbox.TabIndex = 2;
            // 
            // smtpServerAddressLabel
            // 
            this.smtpServerAddressLabel.AutoSize = true;
            this.smtpServerAddressLabel.Location = new System.Drawing.Point(12, 36);
            this.smtpServerAddressLabel.Name = "smtpServerAddressLabel";
            this.smtpServerAddressLabel.Size = new System.Drawing.Size(240, 13);
            this.smtpServerAddressLabel.TabIndex = 1;
            this.smtpServerAddressLabel.Text = "SMTP server address (will use 25 as default port):";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(12, 192);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(270, 36);
            this.sendMessageButton.TabIndex = 3;
            this.sendMessageButton.Text = "Send message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // _cbUseSmtpFromTemplate
            // 
            this._cbUseSmtpFromTemplate.AutoSize = true;
            this._cbUseSmtpFromTemplate.Location = new System.Drawing.Point(12, 16);
            this._cbUseSmtpFromTemplate.Name = "_cbUseSmtpFromTemplate";
            this._cbUseSmtpFromTemplate.Size = new System.Drawing.Size(222, 17);
            this._cbUseSmtpFromTemplate.TabIndex = 0;
            this._cbUseSmtpFromTemplate.Text = "Use the smtp server from the xml template";
            this._cbUseSmtpFromTemplate.UseVisualStyleBackColor = true;
            this._cbUseSmtpFromTemplate.CheckedChanged += new System.EventHandler(this._cbUseSmtpFromTemplate_CheckedChanged);
            // 
            // _tbXmlTemplate
            // 
            this._tbXmlTemplate.Location = new System.Drawing.Point(12, 97);
            this._tbXmlTemplate.Name = "_tbXmlTemplate";
            this._tbXmlTemplate.ReadOnly = true;
            this._tbXmlTemplate.Size = new System.Drawing.Size(273, 20);
            this._tbXmlTemplate.TabIndex = 4;
            this._tbXmlTemplate.Text = "MailFormat.xml";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Xml template";
            // 
            // _bAddXmlTemplate
            // 
            this._bAddXmlTemplate.Location = new System.Drawing.Point(161, 123);
            this._bAddXmlTemplate.Name = "_bAddXmlTemplate";
            this._bAddXmlTemplate.Size = new System.Drawing.Size(124, 23);
            this._bAddXmlTemplate.TabIndex = 6;
            this._bAddXmlTemplate.Text = "Add xml template";
            this._bAddXmlTemplate.UseVisualStyleBackColor = true;
            this._bAddXmlTemplate.Click += new System.EventHandler(this._bAddXmlTemplate_Click);
            // 
            // _ofdXmlTemplate
            // 
            this._ofdXmlTemplate.Filter = "Xml file|*.xml";
            // 
            // _bShowTemplate
            // 
            this._bShowTemplate.Location = new System.Drawing.Point(12, 123);
            this._bShowTemplate.Name = "_bShowTemplate";
            this._bShowTemplate.Size = new System.Drawing.Size(101, 23);
            this._bShowTemplate.TabIndex = 7;
            this._bShowTemplate.Text = "Show template";
            this._bShowTemplate.UseVisualStyleBackColor = true;
            this._bShowTemplate.Click += new System.EventHandler(this._bShowTemplate_Click);
            // 
            // _bShowSource
            // 
            this._bShowSource.Location = new System.Drawing.Point(12, 152);
            this._bShowSource.Name = "_bShowSource";
            this._bShowSource.Size = new System.Drawing.Size(101, 23);
            this._bShowSource.TabIndex = 8;
            this._bShowSource.Text = "Show source";
            this._bShowSource.UseVisualStyleBackColor = true;
            this._bShowSource.Click += new System.EventHandler(this._bShowSource_Click);
            // 
            // SendingFromAnXmlTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SendingFromAnXmlTemplate";
            this.Text = "Sending from an XML template";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox smtpServerAddressTextbox;
        private System.Windows.Forms.Label smtpServerAddressLabel;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.CheckBox _cbUseSmtpFromTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _tbXmlTemplate;
        private System.Windows.Forms.Button _bAddXmlTemplate;
        private System.Windows.Forms.OpenFileDialog _ofdXmlTemplate;
        private System.Windows.Forms.Button _bShowTemplate;
        private System.Windows.Forms.Button _bShowSource;
    }
}
