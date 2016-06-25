namespace ActiveUp.Net.Samples.SMTP
{
    partial class SendingWithMultipleFailoverSmtp
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
            this._tbMainSmtpServer = new System.Windows.Forms.TextBox();
            this._lMainSmtpServer = new System.Windows.Forms.Label();
            this._bSendMessage = new System.Windows.Forms.Button();
            this._tbBodyText = new System.Windows.Forms.TextBox();
            this._lBodyText = new System.Windows.Forms.Label();
            this._tbSubject = new System.Windows.Forms.TextBox();
            this._lSubject = new System.Windows.Forms.Label();
            this._tbFromEmail = new System.Windows.Forms.TextBox();
            this._lFromEmail = new System.Windows.Forms.Label();
            this._tbToEmail = new System.Windows.Forms.TextBox();
            this._lToEmail = new System.Windows.Forms.Label();
            this._tbBackupSmtpServer = new System.Windows.Forms.TextBox();
            this._lBackupSmtpServer = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbBackupSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lBackupSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._tbMainSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lMainSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._bSendMessage);
            this.splitContainer1.Panel1.Controls.Add(this._tbBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._lBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._tbSubject);
            this.splitContainer1.Panel1.Controls.Add(this._lSubject);
            this.splitContainer1.Panel1.Controls.Add(this._tbFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lToEmail);
            this.splitContainer1.TabIndex = 1;
            // 
            // _tbMainSmtpServer
            // 
            this._tbMainSmtpServer.Location = new System.Drawing.Point(9, 350);
            this._tbMainSmtpServer.Name = "_tbMainSmtpServer";
            this._tbMainSmtpServer.Size = new System.Drawing.Size(273, 20);
            this._tbMainSmtpServer.TabIndex = 9;
            // 
            // _lMainSmtpServer
            // 
            this._lMainSmtpServer.AutoSize = true;
            this._lMainSmtpServer.Location = new System.Drawing.Point(9, 334);
            this._lMainSmtpServer.Name = "_lMainSmtpServer";
            this._lMainSmtpServer.Size = new System.Drawing.Size(266, 13);
            this._lMainSmtpServer.TabIndex = 8;
            this._lMainSmtpServer.Text = "Main SMTP server address (will use 25 as default port):";
            // 
            // _bSendMessage
            // 
            this._bSendMessage.Location = new System.Drawing.Point(9, 423);
            this._bSendMessage.Name = "_bSendMessage";
            this._bSendMessage.Size = new System.Drawing.Size(270, 36);
            this._bSendMessage.TabIndex = 12;
            this._bSendMessage.Text = "Send message";
            this._bSendMessage.UseVisualStyleBackColor = true;
            this._bSendMessage.Click += new System.EventHandler(this._bSendMessage_Click);
            // 
            // _tbBodyText
            // 
            this._tbBodyText.Location = new System.Drawing.Point(12, 142);
            this._tbBodyText.Multiline = true;
            this._tbBodyText.Name = "_tbBodyText";
            this._tbBodyText.Size = new System.Drawing.Size(270, 189);
            this._tbBodyText.TabIndex = 7;
            // 
            // _lBodyText
            // 
            this._lBodyText.AutoSize = true;
            this._lBodyText.Location = new System.Drawing.Point(9, 126);
            this._lBodyText.Name = "_lBodyText";
            this._lBodyText.Size = new System.Drawing.Size(105, 13);
            this._lBodyText.TabIndex = 6;
            this._lBodyText.Text = "Body text (plain text):";
            // 
            // _tbSubject
            // 
            this._tbSubject.Location = new System.Drawing.Point(12, 103);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(270, 20);
            this._tbSubject.TabIndex = 5;
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(9, 87);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(46, 13);
            this._lSubject.TabIndex = 4;
            this._lSubject.Text = "Subject:";
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Location = new System.Drawing.Point(12, 25);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.Size = new System.Drawing.Size(206, 20);
            this._tbFromEmail.TabIndex = 1;
            // 
            // _lFromEmail
            // 
            this._lFromEmail.AutoSize = true;
            this._lFromEmail.Location = new System.Drawing.Point(9, 9);
            this._lFromEmail.Name = "_lFromEmail";
            this._lFromEmail.Size = new System.Drawing.Size(60, 13);
            this._lFromEmail.TabIndex = 0;
            this._lFromEmail.Text = "From email:";
            // 
            // _tbToEmail
            // 
            this._tbToEmail.Location = new System.Drawing.Point(12, 64);
            this._tbToEmail.Name = "_tbToEmail";
            this._tbToEmail.Size = new System.Drawing.Size(206, 20);
            this._tbToEmail.TabIndex = 3;
            // 
            // _lToEmail
            // 
            this._lToEmail.AutoSize = true;
            this._lToEmail.Location = new System.Drawing.Point(9, 48);
            this._lToEmail.Name = "_lToEmail";
            this._lToEmail.Size = new System.Drawing.Size(50, 13);
            this._lToEmail.TabIndex = 2;
            this._lToEmail.Text = "To email:";
            // 
            // _tbBackupSmtpServer
            // 
            this._tbBackupSmtpServer.Location = new System.Drawing.Point(9, 392);
            this._tbBackupSmtpServer.Name = "_tbBackupSmtpServer";
            this._tbBackupSmtpServer.Size = new System.Drawing.Size(273, 20);
            this._tbBackupSmtpServer.TabIndex = 11;
            // 
            // _lBackupSmtpServer
            // 
            this._lBackupSmtpServer.AutoSize = true;
            this._lBackupSmtpServer.Location = new System.Drawing.Point(9, 376);
            this._lBackupSmtpServer.Name = "_lBackupSmtpServer";
            this._lBackupSmtpServer.Size = new System.Drawing.Size(280, 13);
            this._lBackupSmtpServer.TabIndex = 10;
            this._lBackupSmtpServer.Text = "Backup SMTP server address (will use 25 as default port):";
            // 
            // SendingWithMultipleFailoverSmtp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SendingWithMultipleFailoverSmtp";
            this.Text = "Sending with multiple failover smtp";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbMainSmtpServer;
        private System.Windows.Forms.Label _lMainSmtpServer;
        private System.Windows.Forms.Button _bSendMessage;
        private System.Windows.Forms.TextBox _tbBodyText;
        private System.Windows.Forms.Label _lBodyText;
        private System.Windows.Forms.TextBox _tbSubject;
        private System.Windows.Forms.Label _lSubject;
        private System.Windows.Forms.TextBox _tbFromEmail;
        private System.Windows.Forms.Label _lFromEmail;
        private System.Windows.Forms.TextBox _tbToEmail;
        private System.Windows.Forms.Label _lToEmail;
        private System.Windows.Forms.TextBox _tbBackupSmtpServer;
        private System.Windows.Forms.Label _lBackupSmtpServer;
    }
}