namespace ActiveUp.Net.Samples.SMTP
{
    partial class SendingWithFileAttachments
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
            this._lSmtpServer = new System.Windows.Forms.Label();
            this._bSendMessage = new System.Windows.Forms.Button();
            this._tbBodyText = new System.Windows.Forms.TextBox();
            this._lBodyText = new System.Windows.Forms.Label();
            this._tbSubject = new System.Windows.Forms.TextBox();
            this._lSubject = new System.Windows.Forms.Label();
            this._tbFromEmail = new System.Windows.Forms.TextBox();
            this._lFromEmail = new System.Windows.Forms.Label();
            this._tbToEmail = new System.Windows.Forms.TextBox();
            this._lToEmail = new System.Windows.Forms.Label();
            this._lvAttachments = new System.Windows.Forms.ListBox();
            this._lAttachments = new System.Windows.Forms.Label();
            this._bAddAttachment = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._bAddAttachment);
            this.splitContainer1.Panel1.Controls.Add(this._lAttachments);
            this.splitContainer1.Panel1.Controls.Add(this._lvAttachments);
            this.splitContainer1.Panel1.Controls.Add(this._tbSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lSmtpServer);
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
            // _tbSmtpServer
            // 
            this._tbSmtpServer.Location = new System.Drawing.Point(12, 269);
            this._tbSmtpServer.Name = "_tbSmtpServer";
            this._tbSmtpServer.Size = new System.Drawing.Size(273, 20);
            this._tbSmtpServer.TabIndex = 9;
            // 
            // _lSmtpServer
            // 
            this._lSmtpServer.AutoSize = true;
            this._lSmtpServer.Location = new System.Drawing.Point(12, 253);
            this._lSmtpServer.Name = "_lSmtpServer";
            this._lSmtpServer.Size = new System.Drawing.Size(240, 13);
            this._lSmtpServer.TabIndex = 8;
            this._lSmtpServer.Text = "SMTP server address (will use 25 as default port):";
            // 
            // _bSendMessage
            // 
            this._bSendMessage.Location = new System.Drawing.Point(12, 430);
            this._bSendMessage.Name = "_bSendMessage";
            this._bSendMessage.Size = new System.Drawing.Size(270, 36);
            this._bSendMessage.TabIndex = 13;
            this._bSendMessage.Text = "Send message";
            this._bSendMessage.UseVisualStyleBackColor = true;
            this._bSendMessage.Click += new System.EventHandler(this._bSendMessage_Click);
            // 
            // _tbBodyText
            // 
            this._tbBodyText.Location = new System.Drawing.Point(12, 143);
            this._tbBodyText.Multiline = true;
            this._tbBodyText.Name = "_tbBodyText";
            this._tbBodyText.Size = new System.Drawing.Size(270, 103);
            this._tbBodyText.TabIndex = 7;
            // 
            // _lBodyText
            // 
            this._lBodyText.AutoSize = true;
            this._lBodyText.Location = new System.Drawing.Point(9, 127);
            this._lBodyText.Name = "_lBodyText";
            this._lBodyText.Size = new System.Drawing.Size(105, 13);
            this._lBodyText.TabIndex = 6;
            this._lBodyText.Text = "Body text (plain text):";
            // 
            // _tbSubject
            // 
            this._tbSubject.Location = new System.Drawing.Point(12, 104);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(270, 20);
            this._tbSubject.TabIndex = 5;
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(9, 88);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(46, 13);
            this._lSubject.TabIndex = 4;
            this._lSubject.Text = "Subject:";
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Location = new System.Drawing.Point(12, 26);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.Size = new System.Drawing.Size(206, 20);
            this._tbFromEmail.TabIndex = 1;
            // 
            // _lFromEmail
            // 
            this._lFromEmail.AutoSize = true;
            this._lFromEmail.Location = new System.Drawing.Point(9, 10);
            this._lFromEmail.Name = "_lFromEmail";
            this._lFromEmail.Size = new System.Drawing.Size(60, 13);
            this._lFromEmail.TabIndex = 0;
            this._lFromEmail.Text = "From email:";
            // 
            // _tbToEmail
            // 
            this._tbToEmail.Location = new System.Drawing.Point(12, 65);
            this._tbToEmail.Name = "_tbToEmail";
            this._tbToEmail.Size = new System.Drawing.Size(206, 20);
            this._tbToEmail.TabIndex = 3;
            // 
            // _lToEmail
            // 
            this._lToEmail.AutoSize = true;
            this._lToEmail.Location = new System.Drawing.Point(9, 49);
            this._lToEmail.Name = "_lToEmail";
            this._lToEmail.Size = new System.Drawing.Size(50, 13);
            this._lToEmail.TabIndex = 2;
            this._lToEmail.Text = "To email:";
            // 
            // _lvAttachments
            // 
            this._lvAttachments.FormattingEnabled = true;
            this._lvAttachments.HorizontalScrollbar = true;
            this._lvAttachments.Location = new System.Drawing.Point(12, 308);
            this._lvAttachments.Name = "_lvAttachments";
            this._lvAttachments.Size = new System.Drawing.Size(273, 69);
            this._lvAttachments.TabIndex = 11;
            // 
            // _lAttachments
            // 
            this._lAttachments.AutoSize = true;
            this._lAttachments.Location = new System.Drawing.Point(12, 292);
            this._lAttachments.Name = "_lAttachments";
            this._lAttachments.Size = new System.Drawing.Size(69, 13);
            this._lAttachments.TabIndex = 10;
            this._lAttachments.Text = "Attachments:";
            // 
            // _bAddAttachment
            // 
            this._bAddAttachment.Location = new System.Drawing.Point(182, 383);
            this._bAddAttachment.Name = "_bAddAttachment";
            this._bAddAttachment.Size = new System.Drawing.Size(103, 23);
            this._bAddAttachment.TabIndex = 12;
            this._bAddAttachment.Text = "Add attachment";
            this._bAddAttachment.UseVisualStyleBackColor = true;
            this._bAddAttachment.Click += new System.EventHandler(this.addAttachment_Click);
            // 
            // SendingWithFileAttachments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SendingWithFileAttachments";
            this.Text = "Sending with file attachments";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _tbSmtpServer;
        private System.Windows.Forms.Label _lSmtpServer;
        private System.Windows.Forms.Button _bSendMessage;
        private System.Windows.Forms.TextBox _tbBodyText;
        private System.Windows.Forms.Label _lBodyText;
        private System.Windows.Forms.TextBox _tbSubject;
        private System.Windows.Forms.Label _lSubject;
        private System.Windows.Forms.TextBox _tbFromEmail;
        private System.Windows.Forms.Label _lFromEmail;
        private System.Windows.Forms.TextBox _tbToEmail;
        private System.Windows.Forms.Label _lToEmail;
        private System.Windows.Forms.Button _bAddAttachment;
        private System.Windows.Forms.Label _lAttachments;
        private System.Windows.Forms.ListBox _lvAttachments;
    }
}