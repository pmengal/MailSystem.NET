namespace ActiveUp.Net.Samples.Common
{
    partial class WorkingWithMessageSettings
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
            this._tbComments = new System.Windows.Forms.TextBox();
            this._lComments = new System.Windows.Forms.Label();
            this._tbConfirmReadEmail = new System.Windows.Forms.TextBox();
            this._lConfirmReadEmail = new System.Windows.Forms.Label();
            this._tbReplyTo = new System.Windows.Forms.TextBox();
            this._lReplyToEmail = new System.Windows.Forms.Label();
            this._tbReturnReceipt = new System.Windows.Forms.TextBox();
            this._lReturnReceipt = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbReturnReceipt);
            this.splitContainer1.Panel1.Controls.Add(this._lReturnReceipt);
            this.splitContainer1.Panel1.Controls.Add(this._tbReplyTo);
            this.splitContainer1.Panel1.Controls.Add(this._lReplyToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbConfirmReadEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lConfirmReadEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbComments);
            this.splitContainer1.Panel1.Controls.Add(this._lComments);
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
            // 
            // _tbSmtpServer
            // 
            this._tbSmtpServer.Location = new System.Drawing.Point(9, 410);
            this._tbSmtpServer.Name = "_tbSmtpServer";
            this._tbSmtpServer.Size = new System.Drawing.Size(273, 20);
            this._tbSmtpServer.TabIndex = 20;
            // 
            // _lSmtpServer
            // 
            this._lSmtpServer.AutoSize = true;
            this._lSmtpServer.Location = new System.Drawing.Point(9, 394);
            this._lSmtpServer.Name = "_lSmtpServer";
            this._lSmtpServer.Size = new System.Drawing.Size(240, 13);
            this._lSmtpServer.TabIndex = 19;
            this._lSmtpServer.Text = "SMTP server address (will use 25 as default port):";
            // 
            // _bSendMessage
            // 
            this._bSendMessage.Location = new System.Drawing.Point(9, 448);
            this._bSendMessage.Name = "_bSendMessage";
            this._bSendMessage.Size = new System.Drawing.Size(270, 36);
            this._bSendMessage.TabIndex = 21;
            this._bSendMessage.Text = "Send message";
            this._bSendMessage.UseVisualStyleBackColor = true;
            this._bSendMessage.Click += new System.EventHandler(this._bSendMessage_Click);
            // 
            // _tbBodyText
            // 
            this._tbBodyText.Location = new System.Drawing.Point(12, 260);
            this._tbBodyText.Multiline = true;
            this._tbBodyText.Name = "_tbBodyText";
            this._tbBodyText.Size = new System.Drawing.Size(270, 68);
            this._tbBodyText.TabIndex = 18;
            // 
            // _lBodyText
            // 
            this._lBodyText.AutoSize = true;
            this._lBodyText.Location = new System.Drawing.Point(9, 244);
            this._lBodyText.Name = "_lBodyText";
            this._lBodyText.Size = new System.Drawing.Size(105, 13);
            this._lBodyText.TabIndex = 17;
            this._lBodyText.Text = "Body text (plain text):";
            // 
            // _tbSubject
            // 
            this._tbSubject.Location = new System.Drawing.Point(12, 221);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(270, 20);
            this._tbSubject.TabIndex = 16;
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(9, 205);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(46, 13);
            this._lSubject.TabIndex = 15;
            this._lSubject.Text = "Subject:";
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Location = new System.Drawing.Point(15, 26);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.Size = new System.Drawing.Size(267, 20);
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
            this._tbToEmail.Size = new System.Drawing.Size(267, 20);
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
            // _tbComments
            // 
            this._tbComments.Location = new System.Drawing.Point(12, 346);
            this._tbComments.Multiline = true;
            this._tbComments.Name = "_tbComments";
            this._tbComments.Size = new System.Drawing.Size(270, 44);
            this._tbComments.TabIndex = 23;
            // 
            // _lComments
            // 
            this._lComments.AutoSize = true;
            this._lComments.Location = new System.Drawing.Point(9, 330);
            this._lComments.Name = "_lComments";
            this._lComments.Size = new System.Drawing.Size(59, 13);
            this._lComments.TabIndex = 22;
            this._lComments.Text = "Comments:";
            // 
            // _tbConfirmReadEmail
            // 
            this._tbConfirmReadEmail.Location = new System.Drawing.Point(15, 104);
            this._tbConfirmReadEmail.Name = "_tbConfirmReadEmail";
            this._tbConfirmReadEmail.Size = new System.Drawing.Size(267, 20);
            this._tbConfirmReadEmail.TabIndex = 27;
            // 
            // _lConfirmReadEmail
            // 
            this._lConfirmReadEmail.AutoSize = true;
            this._lConfirmReadEmail.Location = new System.Drawing.Point(12, 88);
            this._lConfirmReadEmail.Name = "_lConfirmReadEmail";
            this._lConfirmReadEmail.Size = new System.Drawing.Size(96, 13);
            this._lConfirmReadEmail.TabIndex = 26;
            this._lConfirmReadEmail.Text = "Confirm read email:";
            // 
            // _tbReplyTo
            // 
            this._tbReplyTo.Location = new System.Drawing.Point(15, 143);
            this._tbReplyTo.Name = "_tbReplyTo";
            this._tbReplyTo.Size = new System.Drawing.Size(267, 20);
            this._tbReplyTo.TabIndex = 29;
            // 
            // _lReplyToEmail
            // 
            this._lReplyToEmail.AutoSize = true;
            this._lReplyToEmail.Location = new System.Drawing.Point(12, 127);
            this._lReplyToEmail.Name = "_lReplyToEmail";
            this._lReplyToEmail.Size = new System.Drawing.Size(76, 13);
            this._lReplyToEmail.TabIndex = 28;
            this._lReplyToEmail.Text = "Reply to email:";
            // 
            // _tbReturnReceipt
            // 
            this._tbReturnReceipt.Location = new System.Drawing.Point(15, 182);
            this._tbReturnReceipt.Name = "_tbReturnReceipt";
            this._tbReturnReceipt.Size = new System.Drawing.Size(267, 20);
            this._tbReturnReceipt.TabIndex = 31;
            // 
            // _lReturnReceipt
            // 
            this._lReturnReceipt.AutoSize = true;
            this._lReturnReceipt.Location = new System.Drawing.Point(12, 166);
            this._lReturnReceipt.Name = "_lReturnReceipt";
            this._lReturnReceipt.Size = new System.Drawing.Size(77, 13);
            this._lReturnReceipt.TabIndex = 30;
            this._lReturnReceipt.Text = "Return receipt:";
            // 
            // WorkingWithMessageSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "WorkingWithMessageSettings";
            this.Text = "Working with message settings";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox _tbComments;
        private System.Windows.Forms.Label _lComments;
        private System.Windows.Forms.TextBox _tbConfirmReadEmail;
        private System.Windows.Forms.Label _lConfirmReadEmail;
        private System.Windows.Forms.TextBox _tbReturnReceipt;
        private System.Windows.Forms.Label _lReturnReceipt;
        private System.Windows.Forms.TextBox _tbReplyTo;
        private System.Windows.Forms.Label _lReplyToEmail;
    }
}
