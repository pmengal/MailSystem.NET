namespace ActiveUp.Net.Samples.SMTP
{
    partial class SendWithFileAttachments
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
            this.attachmentsListbox = new System.Windows.Forms.ListBox();
            this.attachmentsLabel = new System.Windows.Forms.Label();
            this.addAttachment = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addAttachment);
            this.splitContainer1.Panel1.Controls.Add(this.attachmentsLabel);
            this.splitContainer1.Panel1.Controls.Add(this.attachmentsListbox);
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
            // 
            // smtpServerAddressTextbox
            // 
            this.smtpServerAddressTextbox.Location = new System.Drawing.Point(12, 269);
            this.smtpServerAddressTextbox.Name = "smtpServerAddressTextbox";
            this.smtpServerAddressTextbox.Size = new System.Drawing.Size(273, 20);
            this.smtpServerAddressTextbox.TabIndex = 20;
            // 
            // smtpServerAddressLabel
            // 
            this.smtpServerAddressLabel.AutoSize = true;
            this.smtpServerAddressLabel.Location = new System.Drawing.Point(12, 253);
            this.smtpServerAddressLabel.Name = "smtpServerAddressLabel";
            this.smtpServerAddressLabel.Size = new System.Drawing.Size(240, 13);
            this.smtpServerAddressLabel.TabIndex = 19;
            this.smtpServerAddressLabel.Text = "SMTP server address (will use 25 as default port):";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(12, 430);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(270, 36);
            this.sendMessageButton.TabIndex = 21;
            this.sendMessageButton.Text = "Send message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // bodyTextTextbox
            // 
            this.bodyTextTextbox.Location = new System.Drawing.Point(12, 143);
            this.bodyTextTextbox.Multiline = true;
            this.bodyTextTextbox.Name = "bodyTextTextbox";
            this.bodyTextTextbox.Size = new System.Drawing.Size(270, 103);
            this.bodyTextTextbox.TabIndex = 18;
            // 
            // bodyTextLabel
            // 
            this.bodyTextLabel.AutoSize = true;
            this.bodyTextLabel.Location = new System.Drawing.Point(9, 127);
            this.bodyTextLabel.Name = "bodyTextLabel";
            this.bodyTextLabel.Size = new System.Drawing.Size(105, 13);
            this.bodyTextLabel.TabIndex = 17;
            this.bodyTextLabel.Text = "Body text (plain text):";
            // 
            // subjectTextbox
            // 
            this.subjectTextbox.Location = new System.Drawing.Point(12, 104);
            this.subjectTextbox.Name = "subjectTextbox";
            this.subjectTextbox.Size = new System.Drawing.Size(270, 20);
            this.subjectTextbox.TabIndex = 16;
            // 
            // subjectLabel
            // 
            this.subjectLabel.AutoSize = true;
            this.subjectLabel.Location = new System.Drawing.Point(9, 88);
            this.subjectLabel.Name = "subjectLabel";
            this.subjectLabel.Size = new System.Drawing.Size(46, 13);
            this.subjectLabel.TabIndex = 15;
            this.subjectLabel.Text = "Subject:";
            // 
            // fromEmailTextbox
            // 
            this.fromEmailTextbox.Location = new System.Drawing.Point(12, 26);
            this.fromEmailTextbox.Name = "fromEmailTextbox";
            this.fromEmailTextbox.Size = new System.Drawing.Size(206, 20);
            this.fromEmailTextbox.TabIndex = 12;
            // 
            // fromEmailLabel
            // 
            this.fromEmailLabel.AutoSize = true;
            this.fromEmailLabel.Location = new System.Drawing.Point(9, 10);
            this.fromEmailLabel.Name = "fromEmailLabel";
            this.fromEmailLabel.Size = new System.Drawing.Size(60, 13);
            this.fromEmailLabel.TabIndex = 11;
            this.fromEmailLabel.Text = "From email:";
            // 
            // toEmailTextbox
            // 
            this.toEmailTextbox.Location = new System.Drawing.Point(12, 65);
            this.toEmailTextbox.Name = "toEmailTextbox";
            this.toEmailTextbox.Size = new System.Drawing.Size(206, 20);
            this.toEmailTextbox.TabIndex = 14;
            // 
            // toEmailLabel
            // 
            this.toEmailLabel.AutoSize = true;
            this.toEmailLabel.Location = new System.Drawing.Point(9, 49);
            this.toEmailLabel.Name = "toEmailLabel";
            this.toEmailLabel.Size = new System.Drawing.Size(50, 13);
            this.toEmailLabel.TabIndex = 13;
            this.toEmailLabel.Text = "To email:";
            // 
            // attachmentsListbox
            // 
            this.attachmentsListbox.FormattingEnabled = true;
            this.attachmentsListbox.HorizontalScrollbar = true;
            this.attachmentsListbox.Location = new System.Drawing.Point(12, 308);
            this.attachmentsListbox.Name = "attachmentsListbox";
            this.attachmentsListbox.Size = new System.Drawing.Size(273, 69);
            this.attachmentsListbox.TabIndex = 22;
            // 
            // attachmentsLabel
            // 
            this.attachmentsLabel.AutoSize = true;
            this.attachmentsLabel.Location = new System.Drawing.Point(12, 292);
            this.attachmentsLabel.Name = "attachmentsLabel";
            this.attachmentsLabel.Size = new System.Drawing.Size(69, 13);
            this.attachmentsLabel.TabIndex = 23;
            this.attachmentsLabel.Text = "Attachments:";
            // 
            // addAttachment
            // 
            this.addAttachment.Location = new System.Drawing.Point(182, 383);
            this.addAttachment.Name = "addAttachment";
            this.addAttachment.Size = new System.Drawing.Size(103, 23);
            this.addAttachment.TabIndex = 24;
            this.addAttachment.Text = "Add attachment";
            this.addAttachment.UseVisualStyleBackColor = true;
            this.addAttachment.Click += new System.EventHandler(this.addAttachment_Click);
            // 
            // SendWithFileAttachments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SendWithFileAttachments";
            this.Text = "SendWithFileAttachments";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button addAttachment;
        private System.Windows.Forms.Label attachmentsLabel;
        private System.Windows.Forms.ListBox attachmentsListbox;
    }
}