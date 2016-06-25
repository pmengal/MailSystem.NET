namespace ActiveUp.Net.Samples.Common
{
    partial class WorkingWithEncodingAndCharset
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
            this._tbSmtpServer = new System.Windows.Forms.TextBox();
            this.smtpServerAddressLabel = new System.Windows.Forms.Label();
            this._tbSendMessage = new System.Windows.Forms.Button();
            this._tbBodyText = new System.Windows.Forms.TextBox();
            this._lBodyText = new System.Windows.Forms.Label();
            this._tbSubject = new System.Windows.Forms.TextBox();
            this._lSubject = new System.Windows.Forms.Label();
            this._tbFromEmail = new System.Windows.Forms.TextBox();
            this._lFromEmail = new System.Windows.Forms.Label();
            this._tbToEmail = new System.Windows.Forms.TextBox();
            this._lToEmail = new System.Windows.Forms.Label();
            this._lContentTransfertEncoding = new System.Windows.Forms.Label();
            this._comboTransfertEncoding = new System.Windows.Forms.ComboBox();
            this._comboCharset = new System.Windows.Forms.ComboBox();
            this._lCharset = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._comboCharset);
            this.splitContainer1.Panel1.Controls.Add(this._lCharset);
            this.splitContainer1.Panel1.Controls.Add(this._comboTransfertEncoding);
            this.splitContainer1.Panel1.Controls.Add(this._lContentTransfertEncoding);
            this.splitContainer1.Panel1.Controls.Add(this._tbSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressLabel);
            this.splitContainer1.Panel1.Controls.Add(this._lToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbSendMessage);
            this.splitContainer1.Panel1.Controls.Add(this._tbToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._tbFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._lSubject);
            this.splitContainer1.Panel1.Controls.Add(this._tbSubject);
            // 
            // _tbSmtpServer
            // 
            this._tbSmtpServer.Location = new System.Drawing.Point(18, 355);
            this._tbSmtpServer.Name = "_tbSmtpServer";
            this._tbSmtpServer.Size = new System.Drawing.Size(267, 20);
            this._tbSmtpServer.TabIndex = 20;
            // 
            // smtpServerAddressLabel
            // 
            this.smtpServerAddressLabel.AutoSize = true;
            this.smtpServerAddressLabel.Location = new System.Drawing.Point(18, 339);
            this.smtpServerAddressLabel.Name = "smtpServerAddressLabel";
            this.smtpServerAddressLabel.Size = new System.Drawing.Size(240, 13);
            this.smtpServerAddressLabel.TabIndex = 19;
            this.smtpServerAddressLabel.Text = "SMTP server address (will use 25 as default port):";
            // 
            // _tbSendMessage
            // 
            this._tbSendMessage.Location = new System.Drawing.Point(18, 411);
            this._tbSendMessage.Name = "_tbSendMessage";
            this._tbSendMessage.Size = new System.Drawing.Size(267, 36);
            this._tbSendMessage.TabIndex = 21;
            this._tbSendMessage.Text = "Send message";
            this._tbSendMessage.UseVisualStyleBackColor = true;
            this._tbSendMessage.Click += new System.EventHandler(this._tbSendMessage_Click);
            // 
            // _tbBodyText
            // 
            this._tbBodyText.Location = new System.Drawing.Point(15, 143);
            this._tbBodyText.Multiline = true;
            this._tbBodyText.Name = "_tbBodyText";
            this._tbBodyText.Size = new System.Drawing.Size(270, 110);
            this._tbBodyText.TabIndex = 18;
            // 
            // _lBodyText
            // 
            this._lBodyText.AutoSize = true;
            this._lBodyText.Location = new System.Drawing.Point(12, 127);
            this._lBodyText.Name = "_lBodyText";
            this._lBodyText.Size = new System.Drawing.Size(105, 13);
            this._lBodyText.TabIndex = 17;
            this._lBodyText.Text = "Body text (plain text):";
            // 
            // _tbSubject
            // 
            this._tbSubject.Location = new System.Drawing.Point(15, 104);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(270, 20);
            this._tbSubject.TabIndex = 16;
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(12, 88);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(46, 13);
            this._lSubject.TabIndex = 15;
            this._lSubject.Text = "Subject:";
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Location = new System.Drawing.Point(15, 26);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.Size = new System.Drawing.Size(206, 20);
            this._tbFromEmail.TabIndex = 12;
            // 
            // _lFromEmail
            // 
            this._lFromEmail.AutoSize = true;
            this._lFromEmail.Location = new System.Drawing.Point(12, 10);
            this._lFromEmail.Name = "_lFromEmail";
            this._lFromEmail.Size = new System.Drawing.Size(60, 13);
            this._lFromEmail.TabIndex = 11;
            this._lFromEmail.Text = "From email:";
            // 
            // _tbToEmail
            // 
            this._tbToEmail.Location = new System.Drawing.Point(15, 65);
            this._tbToEmail.Name = "_tbToEmail";
            this._tbToEmail.Size = new System.Drawing.Size(206, 20);
            this._tbToEmail.TabIndex = 14;
            // 
            // _lToEmail
            // 
            this._lToEmail.AutoSize = true;
            this._lToEmail.Location = new System.Drawing.Point(12, 49);
            this._lToEmail.Name = "_lToEmail";
            this._lToEmail.Size = new System.Drawing.Size(50, 13);
            this._lToEmail.TabIndex = 13;
            this._lToEmail.Text = "To email:";
            // 
            // _lContentTransfertEncoding
            // 
            this._lContentTransfertEncoding.AutoSize = true;
            this._lContentTransfertEncoding.Location = new System.Drawing.Point(15, 256);
            this._lContentTransfertEncoding.Name = "_lContentTransfertEncoding";
            this._lContentTransfertEncoding.Size = new System.Drawing.Size(138, 13);
            this._lContentTransfertEncoding.TabIndex = 22;
            this._lContentTransfertEncoding.Text = "Content transfert encoding :";
            // 
            // _comboTransfertEncoding
            // 
            this._comboTransfertEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboTransfertEncoding.FormattingEnabled = true;
            this._comboTransfertEncoding.Items.AddRange(new object[] {
            "Base64",
            "QuotedPrintable",
            "SevenBits",
            "EightBits",
            "Binary"});
            this._comboTransfertEncoding.Location = new System.Drawing.Point(18, 272);
            this._comboTransfertEncoding.Name = "_comboTransfertEncoding";
            this._comboTransfertEncoding.Size = new System.Drawing.Size(267, 21);
            this._comboTransfertEncoding.TabIndex = 23;
            // 
            // _comboCharset
            // 
            this._comboCharset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboCharset.FormattingEnabled = true;
            this._comboCharset.Location = new System.Drawing.Point(18, 315);
            this._comboCharset.Name = "_comboCharset";
            this._comboCharset.Size = new System.Drawing.Size(267, 21);
            this._comboCharset.TabIndex = 25;
            // 
            // _lCharset
            // 
            this._lCharset.AutoSize = true;
            this._lCharset.Location = new System.Drawing.Point(15, 299);
            this._lCharset.Name = "_lCharset";
            this._lCharset.Size = new System.Drawing.Size(43, 13);
            this._lCharset.TabIndex = 24;
            this._lCharset.Text = "Charset";
            // 
            // WorkingWithEncodingAndCharset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "WorkingWithEncodingAndCharset";
            this.Text = "Working with encoding and charset";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _tbSmtpServer;
        private System.Windows.Forms.Label smtpServerAddressLabel;
        private System.Windows.Forms.Button _tbSendMessage;
        private System.Windows.Forms.TextBox _tbBodyText;
        private System.Windows.Forms.Label _lBodyText;
        private System.Windows.Forms.TextBox _tbSubject;
        private System.Windows.Forms.Label _lSubject;
        private System.Windows.Forms.TextBox _tbFromEmail;
        private System.Windows.Forms.Label _lFromEmail;
        private System.Windows.Forms.TextBox _tbToEmail;
        private System.Windows.Forms.Label _lToEmail;
        private System.Windows.Forms.Label _lContentTransfertEncoding;
        private System.Windows.Forms.ComboBox _comboTransfertEncoding;
        private System.Windows.Forms.ComboBox _comboCharset;
        private System.Windows.Forms.Label _lCharset;
    }
}
