namespace ActiveUp.Net.Samples.IMAP4
{
    partial class NewMessageNotification
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
            this.sendTestMessageButton = new System.Windows.Forms.Button();
            this.imap4ServerTextbox = new System.Windows.Forms.TextBox();
            this._lUserName = new System.Windows.Forms.Label();
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this._lPassword = new System.Windows.Forms.Label();
            this._lImap4Server = new System.Windows.Forms.Label();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.emailAddressTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connectNotificationReceiver = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.connectNotificationReceiver);
            this.splitContainer1.Panel1.Controls.Add(this.emailAddressTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.imap4ServerTextbox);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this.usernameTextbox);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._lImap4Server);
            this.splitContainer1.Panel1.Controls.Add(this.passwordTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.sendTestMessageButton);
            // 
            // sendTestMessageButton
            // 
            this.sendTestMessageButton.Location = new System.Drawing.Point(79, 267);
            this.sendTestMessageButton.Name = "sendTestMessageButton";
            this.sendTestMessageButton.Size = new System.Drawing.Size(135, 23);
            this.sendTestMessageButton.TabIndex = 0;
            this.sendTestMessageButton.Text = "Send Test Message";
            this.sendTestMessageButton.UseVisualStyleBackColor = true;
            this.sendTestMessageButton.Click += new System.EventHandler(this.sendTestMessageButton_Click);
            // 
            // imap4ServerTextbox
            // 
            this.imap4ServerTextbox.Location = new System.Drawing.Point(12, 110);
            this.imap4ServerTextbox.Name = "imap4ServerTextbox";
            this.imap4ServerTextbox.Size = new System.Drawing.Size(270, 20);
            this.imap4ServerTextbox.TabIndex = 11;
            // 
            // _lUserName
            // 
            this._lUserName.AutoSize = true;
            this._lUserName.Location = new System.Drawing.Point(9, 16);
            this._lUserName.Name = "_lUserName";
            this._lUserName.Size = new System.Drawing.Size(61, 13);
            this._lUserName.TabIndex = 6;
            this._lUserName.Text = "User name:";
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(12, 32);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(270, 20);
            this.usernameTextbox.TabIndex = 7;
            // 
            // _lPassword
            // 
            this._lPassword.AutoSize = true;
            this._lPassword.Location = new System.Drawing.Point(9, 55);
            this._lPassword.Name = "_lPassword";
            this._lPassword.Size = new System.Drawing.Size(56, 13);
            this._lPassword.TabIndex = 8;
            this._lPassword.Text = "Password:";
            // 
            // _lImap4Server
            // 
            this._lImap4Server.AutoSize = true;
            this._lImap4Server.Location = new System.Drawing.Point(9, 94);
            this._lImap4Server.Name = "_lImap4Server";
            this._lImap4Server.Size = new System.Drawing.Size(248, 13);
            this._lImap4Server.TabIndex = 10;
            this._lImap4Server.Text = "IMAP4 server address (will use 143 as default port):";
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(12, 71);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(270, 20);
            this.passwordTextbox.TabIndex = 9;
            // 
            // emailAddressTextbox
            // 
            this.emailAddressTextbox.Location = new System.Drawing.Point(12, 151);
            this.emailAddressTextbox.Name = "emailAddressTextbox";
            this.emailAddressTextbox.Size = new System.Drawing.Size(270, 20);
            this.emailAddressTextbox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Email address:";
            // 
            // connectNotificationReceiver
            // 
            this.connectNotificationReceiver.Location = new System.Drawing.Point(66, 177);
            this.connectNotificationReceiver.Name = "connectNotificationReceiver";
            this.connectNotificationReceiver.Size = new System.Drawing.Size(161, 23);
            this.connectNotificationReceiver.TabIndex = 14;
            this.connectNotificationReceiver.Text = "Connect Notifion Receiver";
            this.connectNotificationReceiver.UseVisualStyleBackColor = true;
            this.connectNotificationReceiver.Click += new System.EventHandler(this.connectNotificationReceiver_Click);
            // 
            // NewMessageNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "NewMessageNotification";
            this.Text = "NewMessageNotification";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button sendTestMessageButton;
        private System.Windows.Forms.TextBox emailAddressTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox imap4ServerTextbox;
        private System.Windows.Forms.Label _lUserName;
        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.Label _lPassword;
        private System.Windows.Forms.Label _lImap4Server;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Button connectNotificationReceiver;
    }
}