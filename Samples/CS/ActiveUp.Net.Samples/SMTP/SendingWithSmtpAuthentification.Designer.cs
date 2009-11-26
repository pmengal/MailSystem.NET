namespace ActiveUp.Net.Samples.SMTP
{
    partial class SendingWithSmtpAuthentification
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
            this.bodyTextTextbox = new System.Windows.Forms.TextBox();
            this.bodyTextLabel = new System.Windows.Forms.Label();
            this.subjectTextbox = new System.Windows.Forms.TextBox();
            this.subjectLabel = new System.Windows.Forms.Label();
            this.fromEmailTextbox = new System.Windows.Forms.TextBox();
            this.fromEmailLabel = new System.Windows.Forms.Label();
            this.toEmailTextbox = new System.Windows.Forms.TextBox();
            this.toEmailLabel = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.loginTextbox = new System.Windows.Forms.TextBox();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.portTextbox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.portLabel);
            this.splitContainer1.Panel1.Controls.Add(this.portTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.passwordLabel);
            this.splitContainer1.Panel1.Controls.Add(this.passwordTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.loginTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.loginLabel);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressLabel);
            this.splitContainer1.Panel1.Controls.Add(this.sendMessageButton);
            this.splitContainer1.Panel1.Controls.Add(this.bodyTextTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.bodyTextLabel);
            this.splitContainer1.Panel1.Controls.Add(this.subjectTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.subjectLabel);
            this.splitContainer1.Panel1.Controls.Add(this.fromEmailTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.fromEmailLabel);
            this.splitContainer1.Panel1.Controls.Add(this.toEmailTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.toEmailLabel);
            this.splitContainer1.TabIndex = 1;
            // 
            // smtpServerAddressTextbox
            // 
            this.smtpServerAddressTextbox.Location = new System.Drawing.Point(9, 350);
            this.smtpServerAddressTextbox.Name = "smtpServerAddressTextbox";
            this.smtpServerAddressTextbox.Size = new System.Drawing.Size(203, 20);
            this.smtpServerAddressTextbox.TabIndex = 9;
            // 
            // smtpServerAddressLabel
            // 
            this.smtpServerAddressLabel.AutoSize = true;
            this.smtpServerAddressLabel.Location = new System.Drawing.Point(9, 334);
            this.smtpServerAddressLabel.Name = "smtpServerAddressLabel";
            this.smtpServerAddressLabel.Size = new System.Drawing.Size(112, 13);
            this.smtpServerAddressLabel.TabIndex = 8;
            this.smtpServerAddressLabel.Text = "SMTP server address:";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(9, 426);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(270, 36);
            this.sendMessageButton.TabIndex = 16;
            this.sendMessageButton.Text = "Send message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // bodyTextTextbox
            // 
            this.bodyTextTextbox.Location = new System.Drawing.Point(12, 142);
            this.bodyTextTextbox.Multiline = true;
            this.bodyTextTextbox.Name = "bodyTextTextbox";
            this.bodyTextTextbox.Size = new System.Drawing.Size(270, 189);
            this.bodyTextTextbox.TabIndex = 7;
            // 
            // bodyTextLabel
            // 
            this.bodyTextLabel.AutoSize = true;
            this.bodyTextLabel.Location = new System.Drawing.Point(9, 126);
            this.bodyTextLabel.Name = "bodyTextLabel";
            this.bodyTextLabel.Size = new System.Drawing.Size(105, 13);
            this.bodyTextLabel.TabIndex = 6;
            this.bodyTextLabel.Text = "Body text (plain text):";
            // 
            // subjectTextbox
            // 
            this.subjectTextbox.Location = new System.Drawing.Point(12, 103);
            this.subjectTextbox.Name = "subjectTextbox";
            this.subjectTextbox.Size = new System.Drawing.Size(270, 20);
            this.subjectTextbox.TabIndex = 5;
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Location = new System.Drawing.Point(9, 87);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(46, 13);
            this.subjectLabel.TabIndex = 4;
            this.subjectLabel.Text = "Subject:";
            // 
            // fromEmailTextbox
            // 
            this.fromEmailTextbox.Location = new System.Drawing.Point(12, 25);
            this.fromEmailTextbox.Name = "fromEmailTextbox";
            this.fromEmailTextbox.Size = new System.Drawing.Size(206, 20);
            this.fromEmailTextbox.TabIndex = 1;
            // 
            // fromEmailLabel
            // 
            this.fromEmailLabel.AutoSize = true;
            this.fromEmailLabel.Location = new System.Drawing.Point(9, 9);
            this.fromEmailLabel.Name = "fromEmailLabel";
            this.fromEmailLabel.Size = new System.Drawing.Size(60, 13);
            this.fromEmailLabel.TabIndex = 0;
            this.fromEmailLabel.Text = "From email:";
            // 
            // toEmailTextbox
            // 
            this.toEmailTextbox.Location = new System.Drawing.Point(12, 64);
            this.toEmailTextbox.Name = "toEmailTextbox";
            this.toEmailTextbox.Size = new System.Drawing.Size(206, 20);
            this.toEmailTextbox.TabIndex = 3;
            // 
            // toEmailLabel
            // 
            this.toEmailLabel.AutoSize = true;
            this.toEmailLabel.Location = new System.Drawing.Point(9, 48);
            this.toEmailLabel.Name = "toEmailLabel";
            this.toEmailLabel.Size = new System.Drawing.Size(50, 13);
            this.toEmailLabel.TabIndex = 2;
            this.toEmailLabel.Text = "To email:";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(9, 374);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(36, 13);
            this.loginLabel.TabIndex = 12;
            this.loginLabel.Text = "Login:";
            // 
            // loginTextbox
            // 
            this.loginTextbox.Location = new System.Drawing.Point(12, 390);
            this.loginTextbox.Name = "loginTextbox";
            this.loginTextbox.Size = new System.Drawing.Size(131, 20);
            this.loginTextbox.TabIndex = 13;
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(149, 390);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(133, 20);
            this.passwordTextbox.TabIndex = 15;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(146, 373);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 14;
            this.passwordLabel.Text = "Password:";
            // 
            // portTextbox
            // 
            this.portTextbox.Location = new System.Drawing.Point(218, 350);
            this.portTextbox.Name = "portTextbox";
            this.portTextbox.Size = new System.Drawing.Size(64, 20);
            this.portTextbox.TabIndex = 11;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(215, 335);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 10;
            this.portLabel.Text = "Port:";
            // 
            // SendingWithSmtpAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SendingWithSmtpAuthentification";
            this.Text = "Sending with smtp authentification";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox smtpServerAddressTextbox;
        private System.Windows.Forms.Label smtpServerAddressLabel;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.TextBox bodyTextTextbox;
        private System.Windows.Forms.Label bodyTextLabel;
        private System.Windows.Forms.TextBox subjectTextbox;
        private System.Windows.Forms.Label subjectLabel;
        private System.Windows.Forms.TextBox fromEmailTextbox;
        private System.Windows.Forms.Label fromEmailLabel;
        private System.Windows.Forms.TextBox toEmailTextbox;
        private System.Windows.Forms.Label toEmailLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.TextBox loginTextbox;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextbox;
    }
}