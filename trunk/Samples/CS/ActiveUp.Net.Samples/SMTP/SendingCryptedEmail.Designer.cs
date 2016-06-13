namespace ActiveUp.Net.Samples.SMTP
{
    partial class SendingCryptedEmail
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
            this._bRemoveRecipientCertificate = new System.Windows.Forms.Button();
            this._bAddRecipientCertificate = new System.Windows.Forms.Button();
            this._tbRecipientCertificate = new System.Windows.Forms.TextBox();
            this._lRecipientCertificate = new System.Windows.Forms.Label();
            this._tbSmtpServer = new System.Windows.Forms.TextBox();
            this._lFromEmail = new System.Windows.Forms.Label();
            this._lSmtpServer = new System.Windows.Forms.Label();
            this._lToEmail = new System.Windows.Forms.Label();
            this._bSendMessage = new System.Windows.Forms.Button();
            this._tbToEmail = new System.Windows.Forms.TextBox();
            this._tbBodyText = new System.Windows.Forms.TextBox();
            this._tbFromEmail = new System.Windows.Forms.TextBox();
            this._lBodyText = new System.Windows.Forms.Label();
            this._lSubject = new System.Windows.Forms.Label();
            this._tbSubject = new System.Windows.Forms.TextBox();
            this._bRemoveSignerCertificate = new System.Windows.Forms.Button();
            this._bAddSignerCertificate = new System.Windows.Forms.Button();
            this._tbSignerCertificate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._ofdRecipientCertificate = new System.Windows.Forms.OpenFileDialog();
            this._ofdSignerCertificate = new System.Windows.Forms.OpenFileDialog();
            this._tbSecureStringPassword = new System.Windows.Forms.TextBox();
            this._lSecureStringPassword = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbSecureStringPassword);
            this.splitContainer1.Panel1.Controls.Add(this._lSecureStringPassword);
            this.splitContainer1.Panel1.Controls.Add(this._bRemoveSignerCertificate);
            this.splitContainer1.Panel1.Controls.Add(this._bAddSignerCertificate);
            this.splitContainer1.Panel1.Controls.Add(this._tbSignerCertificate);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this._bRemoveRecipientCertificate);
            this.splitContainer1.Panel1.Controls.Add(this._bAddRecipientCertificate);
            this.splitContainer1.Panel1.Controls.Add(this._tbRecipientCertificate);
            this.splitContainer1.Panel1.Controls.Add(this._lRecipientCertificate);
            this.splitContainer1.Panel1.Controls.Add(this._tbSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._bSendMessage);
            this.splitContainer1.Panel1.Controls.Add(this._tbToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._tbFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._lSubject);
            this.splitContainer1.Panel1.Controls.Add(this._tbSubject);
            // 
            // _bRemoveRecipientCertificate
            // 
            this._bRemoveRecipientCertificate.Enabled = false;
            this._bRemoveRecipientCertificate.Location = new System.Drawing.Point(210, 280);
            this._bRemoveRecipientCertificate.Name = "_bRemoveRecipientCertificate";
            this._bRemoveRecipientCertificate.Size = new System.Drawing.Size(75, 23);
            this._bRemoveRecipientCertificate.TabIndex = 26;
            this._bRemoveRecipientCertificate.Text = "Remove";
            this._bRemoveRecipientCertificate.UseVisualStyleBackColor = true;
            this._bRemoveRecipientCertificate.Click += new System.EventHandler(this._bRemoveRecipientCertificate_Click);
            // 
            // _bAddRecipientCertificate
            // 
            this._bAddRecipientCertificate.Location = new System.Drawing.Point(129, 280);
            this._bAddRecipientCertificate.Name = "_bAddRecipientCertificate";
            this._bAddRecipientCertificate.Size = new System.Drawing.Size(75, 23);
            this._bAddRecipientCertificate.TabIndex = 25;
            this._bAddRecipientCertificate.Text = "Add";
            this._bAddRecipientCertificate.UseVisualStyleBackColor = true;
            this._bAddRecipientCertificate.Click += new System.EventHandler(this._bAddRecipientCertificate_Click);
            // 
            // _tbRecipientCertificate
            // 
            this._tbRecipientCertificate.Location = new System.Drawing.Point(15, 254);
            this._tbRecipientCertificate.Name = "_tbRecipientCertificate";
            this._tbRecipientCertificate.ReadOnly = true;
            this._tbRecipientCertificate.Size = new System.Drawing.Size(270, 20);
            this._tbRecipientCertificate.TabIndex = 24;
            this._tbRecipientCertificate.TextChanged += new System.EventHandler(this._tbRecipientCertificate_TextChanged);
            // 
            // _lRecipientCertificate
            // 
            this._lRecipientCertificate.AutoSize = true;
            this._lRecipientCertificate.Location = new System.Drawing.Point(12, 238);
            this._lRecipientCertificate.Name = "_lRecipientCertificate";
            this._lRecipientCertificate.Size = new System.Drawing.Size(123, 13);
            this._lRecipientCertificate.TabIndex = 23;
            this._lRecipientCertificate.Text = "Recipient certificate file :";
            // 
            // _tbSmtpServer
            // 
            this._tbSmtpServer.Location = new System.Drawing.Point(12, 441);
            this._tbSmtpServer.Name = "_tbSmtpServer";
            this._tbSmtpServer.Size = new System.Drawing.Size(270, 20);
            this._tbSmtpServer.TabIndex = 28;
            // 
            // _lFromEmail
            // 
            this._lFromEmail.AutoSize = true;
            this._lFromEmail.Location = new System.Drawing.Point(12, 9);
            this._lFromEmail.Name = "_lFromEmail";
            this._lFromEmail.Size = new System.Drawing.Size(60, 13);
            this._lFromEmail.TabIndex = 15;
            this._lFromEmail.Text = "From email:";
            // 
            // _lSmtpServer
            // 
            this._lSmtpServer.AutoSize = true;
            this._lSmtpServer.Location = new System.Drawing.Point(12, 425);
            this._lSmtpServer.Name = "_lSmtpServer";
            this._lSmtpServer.Size = new System.Drawing.Size(240, 13);
            this._lSmtpServer.TabIndex = 27;
            this._lSmtpServer.Text = "SMTP server address (will use 25 as default port):";
            // 
            // _lToEmail
            // 
            this._lToEmail.AutoSize = true;
            this._lToEmail.Location = new System.Drawing.Point(12, 48);
            this._lToEmail.Name = "_lToEmail";
            this._lToEmail.Size = new System.Drawing.Size(50, 13);
            this._lToEmail.TabIndex = 17;
            this._lToEmail.Text = "To email:";
            // 
            // _bSendMessage
            // 
            this._bSendMessage.Location = new System.Drawing.Point(12, 467);
            this._bSendMessage.Name = "_bSendMessage";
            this._bSendMessage.Size = new System.Drawing.Size(270, 36);
            this._bSendMessage.TabIndex = 29;
            this._bSendMessage.Text = "Send message";
            this._bSendMessage.UseVisualStyleBackColor = true;
            this._bSendMessage.Click += new System.EventHandler(this._bSendMessage_Click);
            // 
            // _tbToEmail
            // 
            this._tbToEmail.Location = new System.Drawing.Point(15, 64);
            this._tbToEmail.Name = "_tbToEmail";
            this._tbToEmail.Size = new System.Drawing.Size(206, 20);
            this._tbToEmail.TabIndex = 18;
            // 
            // _tbBodyText
            // 
            this._tbBodyText.Location = new System.Drawing.Point(15, 142);
            this._tbBodyText.Multiline = true;
            this._tbBodyText.Name = "_tbBodyText";
            this._tbBodyText.Size = new System.Drawing.Size(270, 93);
            this._tbBodyText.TabIndex = 22;
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Location = new System.Drawing.Point(15, 25);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.Size = new System.Drawing.Size(206, 20);
            this._tbFromEmail.TabIndex = 16;
            // 
            // _lBodyText
            // 
            this._lBodyText.AutoSize = true;
            this._lBodyText.Location = new System.Drawing.Point(12, 126);
            this._lBodyText.Name = "_lBodyText";
            this._lBodyText.Size = new System.Drawing.Size(105, 13);
            this._lBodyText.TabIndex = 21;
            this._lBodyText.Text = "Body text (plain text):";
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(12, 87);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(46, 13);
            this._lSubject.TabIndex = 19;
            this._lSubject.Text = "Subject:";
            // 
            // _tbSubject
            // 
            this._tbSubject.Location = new System.Drawing.Point(15, 103);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(270, 20);
            this._tbSubject.TabIndex = 20;
            // 
            // _bRemoveSignerCertificate
            // 
            this._bRemoveSignerCertificate.Enabled = false;
            this._bRemoveSignerCertificate.Location = new System.Drawing.Point(210, 345);
            this._bRemoveSignerCertificate.Name = "_bRemoveSignerCertificate";
            this._bRemoveSignerCertificate.Size = new System.Drawing.Size(75, 23);
            this._bRemoveSignerCertificate.TabIndex = 33;
            this._bRemoveSignerCertificate.Text = "Remove";
            this._bRemoveSignerCertificate.UseVisualStyleBackColor = true;
            this._bRemoveSignerCertificate.Click += new System.EventHandler(this._bRemoveSignerCertificate_Click);
            // 
            // _bAddSignerCertificate
            // 
            this._bAddSignerCertificate.Location = new System.Drawing.Point(129, 345);
            this._bAddSignerCertificate.Name = "_bAddSignerCertificate";
            this._bAddSignerCertificate.Size = new System.Drawing.Size(75, 23);
            this._bAddSignerCertificate.TabIndex = 32;
            this._bAddSignerCertificate.Text = "Add";
            this._bAddSignerCertificate.UseVisualStyleBackColor = true;
            this._bAddSignerCertificate.Click += new System.EventHandler(this._bAddSignerCertificate_Click);
            // 
            // _tbSignerCertificate
            // 
            this._tbSignerCertificate.Location = new System.Drawing.Point(15, 319);
            this._tbSignerCertificate.Name = "_tbSignerCertificate";
            this._tbSignerCertificate.ReadOnly = true;
            this._tbSignerCertificate.Size = new System.Drawing.Size(270, 20);
            this._tbSignerCertificate.TabIndex = 31;
            this._tbSignerCertificate.TextChanged += new System.EventHandler(this._tbSignerCertificate_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Signer certificate file :";
            // 
            // _ofdRecipientCertificate
            // 
            this._ofdRecipientCertificate.Filter = "Recipient certificate files|*.cer";
            // 
            // _ofdSignerCertificate
            // 
            this._ofdSignerCertificate.Filter = "Signer certificate files|*.pfx";
            // 
            // _tbSecureStringPassword
            // 
            this._tbSecureStringPassword.Location = new System.Drawing.Point(15, 387);
            this._tbSecureStringPassword.Name = "_tbSecureStringPassword";
            this._tbSecureStringPassword.Size = new System.Drawing.Size(270, 20);
            this._tbSecureStringPassword.TabIndex = 35;
            // 
            // _lSecureStringPassword
            // 
            this._lSecureStringPassword.AutoSize = true;
            this._lSecureStringPassword.Location = new System.Drawing.Point(12, 371);
            this._lSecureStringPassword.Name = "_lSecureStringPassword";
            this._lSecureStringPassword.Size = new System.Drawing.Size(117, 13);
            this._lSecureStringPassword.TabIndex = 34;
            this._lSecureStringPassword.Text = "Secure string password";
            // 
            // SendingCryptedEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SendingCryptedEmail";
            this.Text = "SendingCryptedEmail";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _bRemoveRecipientCertificate;
        private System.Windows.Forms.Button _bAddRecipientCertificate;
        private System.Windows.Forms.TextBox _tbRecipientCertificate;
        private System.Windows.Forms.Label _lRecipientCertificate;
        private System.Windows.Forms.TextBox _tbSmtpServer;
        private System.Windows.Forms.Label _lFromEmail;
        private System.Windows.Forms.Label _lSmtpServer;
        private System.Windows.Forms.Label _lToEmail;
        private System.Windows.Forms.Button _bSendMessage;
        private System.Windows.Forms.TextBox _tbToEmail;
        private System.Windows.Forms.TextBox _tbBodyText;
        private System.Windows.Forms.TextBox _tbFromEmail;
        private System.Windows.Forms.Label _lBodyText;
        private System.Windows.Forms.Label _lSubject;
        private System.Windows.Forms.TextBox _tbSubject;
        private System.Windows.Forms.Button _bRemoveSignerCertificate;
        private System.Windows.Forms.Button _bAddSignerCertificate;
        private System.Windows.Forms.TextBox _tbSignerCertificate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog _ofdRecipientCertificate;
        private System.Windows.Forms.OpenFileDialog _ofdSignerCertificate;
        private System.Windows.Forms.TextBox _tbSecureStringPassword;
        private System.Windows.Forms.Label _lSecureStringPassword;

    }
}