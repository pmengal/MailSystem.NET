namespace ActiveUp.Net.Samples.PDI
{
    partial class ReadWritevCard
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
            this.openVcardButton = new System.Windows.Forms.Button();
            this.openvCardDialog = new System.Windows.Forms.OpenFileDialog();
            this.rawDataTextbox = new System.Windows.Forms.TextBox();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.savevCardDialog = new System.Windows.Forms.SaveFileDialog();
            this.sendByEmailButton = new System.Windows.Forms.Button();
            this.smtpServerAddressTextbox = new System.Windows.Forms.TextBox();
            this.smtpServerAddressLabel = new System.Windows.Forms.Label();
            this.toEmailTextbox = new System.Windows.Forms.TextBox();
            this.toEmailLabel = new System.Windows.Forms.Label();
            this.sendByEmailGroupbox = new System.Windows.Forms.GroupBox();
            this.createNewButton = new System.Windows.Forms.Button();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.firstNameTextbox = new System.Windows.Forms.TextBox();
            this.lastNameTextbox = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.companyTextbox = new System.Windows.Forms.TextBox();
            this.companyLabel = new System.Windows.Forms.Label();
            this.officePhoneTextbox = new System.Windows.Forms.TextBox();
            this.officePhoneLabel = new System.Windows.Forms.Label();
            this.homePhoneTextbox = new System.Windows.Forms.TextBox();
            this.homePhoneLabel = new System.Windows.Forms.Label();
            this.mobilePhoneTextbox = new System.Windows.Forms.TextBox();
            this.mobilePhoneLabel = new System.Windows.Forms.Label();
            this.emailTextbox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.birthdayDatePicker = new System.Windows.Forms.DateTimePicker();
            this.birthdayLabel = new System.Windows.Forms.Label();
            this.photoPictureBox = new System.Windows.Forms.PictureBox();
            this.photoLabel = new System.Windows.Forms.Label();
            this.selectPhotoButton = new System.Windows.Forms.Button();
            this.selectPhotoDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.sendByEmailGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.selectPhotoButton);
            this.splitContainer1.Panel1.Controls.Add(this.photoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.photoPictureBox);
            this.splitContainer1.Panel1.Controls.Add(this.birthdayLabel);
            this.splitContainer1.Panel1.Controls.Add(this.birthdayDatePicker);
            this.splitContainer1.Panel1.Controls.Add(this.emailTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.emailLabel);
            this.splitContainer1.Panel1.Controls.Add(this.mobilePhoneTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.mobilePhoneLabel);
            this.splitContainer1.Panel1.Controls.Add(this.homePhoneTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.homePhoneLabel);
            this.splitContainer1.Panel1.Controls.Add(this.officePhoneTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.officePhoneLabel);
            this.splitContainer1.Panel1.Controls.Add(this.companyTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.companyLabel);
            this.splitContainer1.Panel1.Controls.Add(this.lastNameTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.lastNameLabel);
            this.splitContainer1.Panel1.Controls.Add(this.firstNameTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.firstNameLabel);
            this.splitContainer1.Panel1.Controls.Add(this.createNewButton);
            this.splitContainer1.Panel1.Controls.Add(this.sendByEmailGroupbox);
            this.splitContainer1.Panel1.Controls.Add(this.saveAsButton);
            this.splitContainer1.Panel1.Controls.Add(this.rawDataTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.openVcardButton);
            // 
            // openVcardButton
            // 
            this.openVcardButton.Location = new System.Drawing.Point(12, 12);
            this.openVcardButton.Name = "openVcardButton";
            this.openVcardButton.Size = new System.Drawing.Size(75, 23);
            this.openVcardButton.TabIndex = 0;
            this.openVcardButton.Text = "Open vCard";
            this.openVcardButton.UseVisualStyleBackColor = true;
            this.openVcardButton.Click += new System.EventHandler(this.openVcardButton_Click);
            // 
            // openvCardDialog
            // 
            this.openvCardDialog.DefaultExt = "vcf";
            this.openvCardDialog.FileName = "openFileDialog1";
            // 
            // rawDataTextbox
            // 
            this.rawDataTextbox.Location = new System.Drawing.Point(11, 338);
            this.rawDataTextbox.Multiline = true;
            this.rawDataTextbox.Name = "rawDataTextbox";
            this.rawDataTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rawDataTextbox.Size = new System.Drawing.Size(194, 54);
            this.rawDataTextbox.TabIndex = 1;
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(211, 338);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(75, 54);
            this.saveAsButton.TabIndex = 2;
            this.saveAsButton.Text = "Save As";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // savevCardDialog
            // 
            this.savevCardDialog.DefaultExt = "vcf";
            // 
            // sendByEmailButton
            // 
            this.sendByEmailButton.Location = new System.Drawing.Point(205, 34);
            this.sendByEmailButton.Name = "sendByEmailButton";
            this.sendByEmailButton.Size = new System.Drawing.Size(63, 59);
            this.sendByEmailButton.TabIndex = 3;
            this.sendByEmailButton.Text = "Send by email";
            this.sendByEmailButton.UseVisualStyleBackColor = true;
            // 
            // smtpServerAddressTextbox
            // 
            this.smtpServerAddressTextbox.Location = new System.Drawing.Point(7, 74);
            this.smtpServerAddressTextbox.Name = "smtpServerAddressTextbox";
            this.smtpServerAddressTextbox.Size = new System.Drawing.Size(173, 20);
            this.smtpServerAddressTextbox.TabIndex = 11;
            // 
            // smtpServerAddressLabel
            // 
            this.smtpServerAddressLabel.AutoSize = true;
            this.smtpServerAddressLabel.Location = new System.Drawing.Point(7, 58);
            this.smtpServerAddressLabel.Name = "smtpServerAddressLabel";
            this.smtpServerAddressLabel.Size = new System.Drawing.Size(194, 13);
            this.smtpServerAddressLabel.TabIndex = 10;
            this.smtpServerAddressLabel.Text = "SMTP server address (will use 25  port):";
            // 
            // toEmailTextbox
            // 
            this.toEmailTextbox.Location = new System.Drawing.Point(6, 34);
            this.toEmailTextbox.Name = "toEmailTextbox";
            this.toEmailTextbox.Size = new System.Drawing.Size(174, 20);
            this.toEmailTextbox.TabIndex = 13;
            // 
            // toEmailLabel
            // 
            this.toEmailLabel.AutoSize = true;
            this.toEmailLabel.Location = new System.Drawing.Point(3, 18);
            this.toEmailLabel.Name = "toEmailLabel";
            this.toEmailLabel.Size = new System.Drawing.Size(50, 13);
            this.toEmailLabel.TabIndex = 12;
            this.toEmailLabel.Text = "To email:";
            // 
            // sendByEmailGroupbox
            // 
            this.sendByEmailGroupbox.Controls.Add(this.toEmailTextbox);
            this.sendByEmailGroupbox.Controls.Add(this.sendByEmailButton);
            this.sendByEmailGroupbox.Controls.Add(this.smtpServerAddressTextbox);
            this.sendByEmailGroupbox.Controls.Add(this.smtpServerAddressLabel);
            this.sendByEmailGroupbox.Controls.Add(this.toEmailLabel);
            this.sendByEmailGroupbox.Location = new System.Drawing.Point(11, 403);
            this.sendByEmailGroupbox.Name = "sendByEmailGroupbox";
            this.sendByEmailGroupbox.Size = new System.Drawing.Size(275, 100);
            this.sendByEmailGroupbox.TabIndex = 14;
            this.sendByEmailGroupbox.TabStop = false;
            this.sendByEmailGroupbox.Text = "Send By Email";
            // 
            // createNewButton
            // 
            this.createNewButton.Location = new System.Drawing.Point(93, 12);
            this.createNewButton.Name = "createNewButton";
            this.createNewButton.Size = new System.Drawing.Size(75, 23);
            this.createNewButton.TabIndex = 15;
            this.createNewButton.Text = "Create New";
            this.createNewButton.UseVisualStyleBackColor = true;
            this.createNewButton.Click += new System.EventHandler(this.createNewButton_Click);
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(9, 55);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(55, 13);
            this.firstNameLabel.TabIndex = 16;
            this.firstNameLabel.Text = "Firstname:";
            // 
            // firstNameTextbox
            // 
            this.firstNameTextbox.Location = new System.Drawing.Point(87, 52);
            this.firstNameTextbox.Name = "firstNameTextbox";
            this.firstNameTextbox.Size = new System.Drawing.Size(199, 20);
            this.firstNameTextbox.TabIndex = 17;
            // 
            // lastNameTextbox
            // 
            this.lastNameTextbox.Location = new System.Drawing.Point(87, 78);
            this.lastNameTextbox.Name = "lastNameTextbox";
            this.lastNameTextbox.Size = new System.Drawing.Size(199, 20);
            this.lastNameTextbox.TabIndex = 19;
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(9, 81);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(56, 13);
            this.lastNameLabel.TabIndex = 18;
            this.lastNameLabel.Text = "Lastname:";
            // 
            // companyTextbox
            // 
            this.companyTextbox.Location = new System.Drawing.Point(87, 104);
            this.companyTextbox.Name = "companyTextbox";
            this.companyTextbox.Size = new System.Drawing.Size(199, 20);
            this.companyTextbox.TabIndex = 21;
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Location = new System.Drawing.Point(9, 107);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(54, 13);
            this.companyLabel.TabIndex = 20;
            this.companyLabel.Text = "Company:";
            // 
            // officePhoneTextbox
            // 
            this.officePhoneTextbox.Location = new System.Drawing.Point(87, 130);
            this.officePhoneTextbox.Name = "officePhoneTextbox";
            this.officePhoneTextbox.Size = new System.Drawing.Size(199, 20);
            this.officePhoneTextbox.TabIndex = 23;
            // 
            // officePhoneLabel
            // 
            this.officePhoneLabel.AutoSize = true;
            this.officePhoneLabel.Location = new System.Drawing.Point(9, 133);
            this.officePhoneLabel.Name = "officePhoneLabel";
            this.officePhoneLabel.Size = new System.Drawing.Size(72, 13);
            this.officePhoneLabel.TabIndex = 22;
            this.officePhoneLabel.Text = "Office Phone:";
            // 
            // homePhoneTextbox
            // 
            this.homePhoneTextbox.Location = new System.Drawing.Point(87, 156);
            this.homePhoneTextbox.Name = "homePhoneTextbox";
            this.homePhoneTextbox.Size = new System.Drawing.Size(199, 20);
            this.homePhoneTextbox.TabIndex = 25;
            // 
            // homePhoneLabel
            // 
            this.homePhoneLabel.AutoSize = true;
            this.homePhoneLabel.Location = new System.Drawing.Point(9, 159);
            this.homePhoneLabel.Name = "homePhoneLabel";
            this.homePhoneLabel.Size = new System.Drawing.Size(72, 13);
            this.homePhoneLabel.TabIndex = 24;
            this.homePhoneLabel.Text = "Home Phone:";
            // 
            // mobilePhoneTextbox
            // 
            this.mobilePhoneTextbox.Location = new System.Drawing.Point(87, 182);
            this.mobilePhoneTextbox.Name = "mobilePhoneTextbox";
            this.mobilePhoneTextbox.Size = new System.Drawing.Size(199, 20);
            this.mobilePhoneTextbox.TabIndex = 27;
            // 
            // mobilePhoneLabel
            // 
            this.mobilePhoneLabel.AutoSize = true;
            this.mobilePhoneLabel.Location = new System.Drawing.Point(9, 185);
            this.mobilePhoneLabel.Name = "mobilePhoneLabel";
            this.mobilePhoneLabel.Size = new System.Drawing.Size(75, 13);
            this.mobilePhoneLabel.TabIndex = 26;
            this.mobilePhoneLabel.Text = "Mobile Phone:";
            // 
            // emailTextbox
            // 
            this.emailTextbox.Location = new System.Drawing.Point(87, 208);
            this.emailTextbox.Name = "emailTextbox";
            this.emailTextbox.Size = new System.Drawing.Size(199, 20);
            this.emailTextbox.TabIndex = 29;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(9, 211);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(35, 13);
            this.emailLabel.TabIndex = 28;
            this.emailLabel.Text = "Email:";
            // 
            // birthdayDatePicker
            // 
            this.birthdayDatePicker.Location = new System.Drawing.Point(87, 234);
            this.birthdayDatePicker.Name = "birthdayDatePicker";
            this.birthdayDatePicker.Size = new System.Drawing.Size(200, 20);
            this.birthdayDatePicker.TabIndex = 30;
            // 
            // birthdayLabel
            // 
            this.birthdayLabel.AutoSize = true;
            this.birthdayLabel.Location = new System.Drawing.Point(9, 238);
            this.birthdayLabel.Name = "birthdayLabel";
            this.birthdayLabel.Size = new System.Drawing.Size(48, 13);
            this.birthdayLabel.TabIndex = 31;
            this.birthdayLabel.Text = "Birthday:";
            // 
            // photoPictureBox
            // 
            this.photoPictureBox.Location = new System.Drawing.Point(87, 260);
            this.photoPictureBox.Name = "photoPictureBox";
            this.photoPictureBox.Size = new System.Drawing.Size(64, 67);
            this.photoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.photoPictureBox.TabIndex = 32;
            this.photoPictureBox.TabStop = false;
            // 
            // photoLabel
            // 
            this.photoLabel.AutoSize = true;
            this.photoLabel.Location = new System.Drawing.Point(9, 260);
            this.photoLabel.Name = "photoLabel";
            this.photoLabel.Size = new System.Drawing.Size(38, 13);
            this.photoLabel.TabIndex = 33;
            this.photoLabel.Text = "Photo:";
            // 
            // selectPhotoButton
            // 
            this.selectPhotoButton.Location = new System.Drawing.Point(157, 260);
            this.selectPhotoButton.Name = "selectPhotoButton";
            this.selectPhotoButton.Size = new System.Drawing.Size(55, 35);
            this.selectPhotoButton.TabIndex = 34;
            this.selectPhotoButton.Text = "Select Photo";
            this.selectPhotoButton.UseVisualStyleBackColor = true;
            this.selectPhotoButton.Click += new System.EventHandler(this.selectPhotoButton_Click);
            // 
            // selectPhotoDialog
            // 
            this.selectPhotoDialog.DefaultExt = "jpg";
            // 
            // ReadWritevCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "ReadWritevCard";
            this.Text = "ReadWritevCard";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.sendByEmailGroupbox.ResumeLayout(false);
            this.sendByEmailGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openVcardButton;
        private System.Windows.Forms.OpenFileDialog openvCardDialog;
        private System.Windows.Forms.TextBox rawDataTextbox;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.SaveFileDialog savevCardDialog;
        private System.Windows.Forms.Button sendByEmailButton;
        private System.Windows.Forms.TextBox smtpServerAddressTextbox;
        private System.Windows.Forms.Label smtpServerAddressLabel;
        private System.Windows.Forms.TextBox toEmailTextbox;
        private System.Windows.Forms.Label toEmailLabel;
        private System.Windows.Forms.GroupBox sendByEmailGroupbox;
        private System.Windows.Forms.Button createNewButton;
        private System.Windows.Forms.TextBox firstNameTextbox;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.TextBox officePhoneTextbox;
        private System.Windows.Forms.Label officePhoneLabel;
        private System.Windows.Forms.TextBox companyTextbox;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.TextBox lastNameTextbox;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.DateTimePicker birthdayDatePicker;
        private System.Windows.Forms.TextBox emailTextbox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox mobilePhoneTextbox;
        private System.Windows.Forms.Label mobilePhoneLabel;
        private System.Windows.Forms.TextBox homePhoneTextbox;
        private System.Windows.Forms.Label homePhoneLabel;
        private System.Windows.Forms.Label birthdayLabel;
        private System.Windows.Forms.PictureBox photoPictureBox;
        private System.Windows.Forms.Label photoLabel;
        private System.Windows.Forms.Button selectPhotoButton;
        private System.Windows.Forms.OpenFileDialog selectPhotoDialog;
    }
}