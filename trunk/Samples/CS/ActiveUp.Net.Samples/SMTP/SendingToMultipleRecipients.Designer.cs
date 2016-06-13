namespace ActiveUp.Net.Samples.SMTP
{
    partial class SendingToMultipleRecipients
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
            this._lToEmail = new System.Windows.Forms.Label();
            this._bRemove = new System.Windows.Forms.Button();
            this._bAdd = new System.Windows.Forms.Button();
            this._tbToAddress = new System.Windows.Forms.TextBox();
            this._lvToEmail = new System.Windows.Forms.ListView();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._lvToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbToAddress);
            this.splitContainer1.Panel1.Controls.Add(this._bAdd);
            this.splitContainer1.Panel1.Controls.Add(this._bRemove);
            this.splitContainer1.Panel1.Controls.Add(this._tbSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lSmtpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lToEmail);
            this.splitContainer1.Panel1.Controls.Add(this._bSendMessage);
            this.splitContainer1.Panel1.Controls.Add(this._tbBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._tbFromEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._lSubject);
            this.splitContainer1.Panel1.Controls.Add(this._tbSubject);
            this.splitContainer1.TabIndex = 1;
            // 
            // _tbSmtpServer
            // 
            this._tbSmtpServer.Location = new System.Drawing.Point(12, 354);
            this._tbSmtpServer.Name = "_tbSmtpServer";
            this._tbSmtpServer.Size = new System.Drawing.Size(270, 20);
            this._tbSmtpServer.TabIndex = 12;
            // 
            // _lSmtpServer
            // 
            this._lSmtpServer.AutoSize = true;
            this._lSmtpServer.Location = new System.Drawing.Point(12, 338);
            this._lSmtpServer.Name = "_lSmtpServer";
            this._lSmtpServer.Size = new System.Drawing.Size(240, 13);
            this._lSmtpServer.TabIndex = 11;
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
            this._tbBodyText.Location = new System.Drawing.Point(12, 228);
            this._tbBodyText.Multiline = true;
            this._tbBodyText.Name = "_tbBodyText";
            this._tbBodyText.Size = new System.Drawing.Size(270, 103);
            this._tbBodyText.TabIndex = 10;
            // 
            // _lBodyText
            // 
            this._lBodyText.AutoSize = true;
            this._lBodyText.Location = new System.Drawing.Point(9, 212);
            this._lBodyText.Name = "_lBodyText";
            this._lBodyText.Size = new System.Drawing.Size(105, 13);
            this._lBodyText.TabIndex = 9;
            this._lBodyText.Text = "Body text (plain text):";
            // 
            // _tbSubject
            // 
            this._tbSubject.Location = new System.Drawing.Point(12, 189);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(270, 20);
            this._tbSubject.TabIndex = 8;
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(9, 173);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(46, 13);
            this._lSubject.TabIndex = 7;
            this._lSubject.Text = "Subject:";
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Location = new System.Drawing.Point(12, 26);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.Size = new System.Drawing.Size(270, 20);
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
            // _lToEmail
            // 
            this._lToEmail.AutoSize = true;
            this._lToEmail.Location = new System.Drawing.Point(9, 49);
            this._lToEmail.Name = "_lToEmail";
            this._lToEmail.Size = new System.Drawing.Size(50, 13);
            this._lToEmail.TabIndex = 2;
            this._lToEmail.Text = "To email:";
            // 
            // _bRemove
            // 
            this._bRemove.Enabled = false;
            this._bRemove.Location = new System.Drawing.Point(220, 147);
            this._bRemove.Name = "_bRemove";
            this._bRemove.Size = new System.Drawing.Size(61, 23);
            this._bRemove.TabIndex = 6;
            this._bRemove.Text = "Remove";
            this._bRemove.UseVisualStyleBackColor = true;
            this._bRemove.Click += new System.EventHandler(this._bRemove_Click);
            // 
            // _bAdd
            // 
            this._bAdd.Enabled = false;
            this._bAdd.Location = new System.Drawing.Point(155, 147);
            this._bAdd.Name = "_bAdd";
            this._bAdd.Size = new System.Drawing.Size(59, 23);
            this._bAdd.TabIndex = 5;
            this._bAdd.Text = "Add";
            this._bAdd.UseVisualStyleBackColor = true;
            this._bAdd.Click += new System.EventHandler(this._bAdd_Click);
            // 
            // _tbToAddress
            // 
            this._tbToAddress.Location = new System.Drawing.Point(12, 149);
            this._tbToAddress.Name = "_tbToAddress";
            this._tbToAddress.Size = new System.Drawing.Size(140, 20);
            this._tbToAddress.TabIndex = 4;
            this._tbToAddress.TextChanged += new System.EventHandler(this._tbToAddress_TextChanged);
            this._tbToAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this._tbToAddress_KeyDown);
            // 
            // _lvToEmail
            // 
            this._lvToEmail.FullRowSelect = true;
            this._lvToEmail.HideSelection = false;
            this._lvToEmail.Location = new System.Drawing.Point(12, 65);
            this._lvToEmail.Name = "_lvToEmail";
            this._lvToEmail.Size = new System.Drawing.Size(269, 78);
            this._lvToEmail.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._lvToEmail.TabIndex = 3;
            this._lvToEmail.UseCompatibleStateImageBehavior = false;
            this._lvToEmail.View = System.Windows.Forms.View.List;
            this._lvToEmail.SelectedIndexChanged += new System.EventHandler(this._lvToEmail_SelectedIndexChanged);
            // 
            // SendingToMultipleRecipients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SendingToMultipleRecipients";
            this.Text = "Sending to multiple recipients";
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
        private System.Windows.Forms.Label _lToEmail;
        private System.Windows.Forms.TextBox _tbToAddress;
        private System.Windows.Forms.Button _bAdd;
        private System.Windows.Forms.Button _bRemove;
        private System.Windows.Forms.ListView _lvToEmail;
    }
}
