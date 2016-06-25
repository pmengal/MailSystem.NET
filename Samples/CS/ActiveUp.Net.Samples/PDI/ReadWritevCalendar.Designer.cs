namespace ActiveUp.Net.Samples.PDI
{
    partial class ReadWritevCalendar
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
            this.objectLabel = new System.Windows.Forms.Label();
            this.objectTextbox = new System.Windows.Forms.TextBox();
            this.placeTextbox = new System.Windows.Forms.TextBox();
            this.placeLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.endTime = new System.Windows.Forms.DateTimePicker();
            this.createNewButton = new System.Windows.Forms.Button();
            this.openVcalendarButton = new System.Windows.Forms.Button();
            this.sendByEmailGroupbox = new System.Windows.Forms.GroupBox();
            this.toEmailTextbox = new System.Windows.Forms.TextBox();
            this.sendByEmailButton = new System.Windows.Forms.Button();
            this.smtpServerAddressTextbox = new System.Windows.Forms.TextBox();
            this.smtpServerAddressLabel = new System.Windows.Forms.Label();
            this.toEmailLabel = new System.Windows.Forms.Label();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.rawDataTextbox = new System.Windows.Forms.TextBox();
            this.openvCalendarDialog = new System.Windows.Forms.OpenFileDialog();
            this.savevCalendarDialog = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.sendByEmailGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sendByEmailGroupbox);
            this.splitContainer1.Panel1.Controls.Add(this.saveAsButton);
            this.splitContainer1.Panel1.Controls.Add(this.rawDataTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.createNewButton);
            this.splitContainer1.Panel1.Controls.Add(this.openVcalendarButton);
            this.splitContainer1.Panel1.Controls.Add(this.endDate);
            this.splitContainer1.Panel1.Controls.Add(this.endTime);
            this.splitContainer1.Panel1.Controls.Add(this.startDate);
            this.splitContainer1.Panel1.Controls.Add(this.startTime);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.startLabel);
            this.splitContainer1.Panel1.Controls.Add(this.placeTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.placeLabel);
            this.splitContainer1.Panel1.Controls.Add(this.objectTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.objectLabel);
            // 
            // objectLabel
            // 
            this.objectLabel.AutoSize = true;
            this.objectLabel.Location = new System.Drawing.Point(12, 48);
            this.objectLabel.Name = "objectLabel";
            this.objectLabel.Size = new System.Drawing.Size(38, 13);
            this.objectLabel.TabIndex = 0;
            this.objectLabel.Text = "Object";
            // 
            // objectTextbox
            // 
            this.objectTextbox.Location = new System.Drawing.Point(15, 64);
            this.objectTextbox.Name = "objectTextbox";
            this.objectTextbox.Size = new System.Drawing.Size(270, 20);
            this.objectTextbox.TabIndex = 1;
            // 
            // placeTextbox
            // 
            this.placeTextbox.Location = new System.Drawing.Point(15, 109);
            this.placeTextbox.Name = "placeTextbox";
            this.placeTextbox.Size = new System.Drawing.Size(270, 20);
            this.placeTextbox.TabIndex = 4;
            // 
            // placeLabel
            // 
            this.placeLabel.AutoSize = true;
            this.placeLabel.Location = new System.Drawing.Point(12, 93);
            this.placeLabel.Name = "placeLabel";
            this.placeLabel.Size = new System.Drawing.Size(34, 13);
            this.placeLabel.TabIndex = 3;
            this.placeLabel.Text = "Place";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Location = new System.Drawing.Point(12, 151);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(32, 13);
            this.startLabel.TabIndex = 5;
            this.startLabel.Text = "Start:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "End:";
            // 
            // startTime
            // 
            this.startTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startTime.Location = new System.Drawing.Point(143, 147);
            this.startTime.Name = "startTime";
            this.startTime.Size = new System.Drawing.Size(74, 20);
            this.startTime.TabIndex = 7;
            // 
            // startDate
            // 
            this.startDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDate.Location = new System.Drawing.Point(50, 147);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(87, 20);
            this.startDate.TabIndex = 8;
            // 
            // endDate
            // 
            this.endDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDate.Location = new System.Drawing.Point(50, 178);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(87, 20);
            this.endDate.TabIndex = 10;
            // 
            // endTime
            // 
            this.endTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.endTime.Location = new System.Drawing.Point(143, 178);
            this.endTime.Name = "endTime";
            this.endTime.Size = new System.Drawing.Size(74, 20);
            this.endTime.TabIndex = 9;
            // 
            // createNewButton
            // 
            this.createNewButton.Location = new System.Drawing.Point(122, 12);
            this.createNewButton.Name = "createNewButton";
            this.createNewButton.Size = new System.Drawing.Size(95, 23);
            this.createNewButton.TabIndex = 17;
            this.createNewButton.Text = "Create New";
            this.createNewButton.UseVisualStyleBackColor = true;
            this.createNewButton.Click += new System.EventHandler(this.createNewButton_Click);
            // 
            // openVcalendarButton
            // 
            this.openVcalendarButton.Location = new System.Drawing.Point(15, 12);
            this.openVcalendarButton.Name = "openVcalendarButton";
            this.openVcalendarButton.Size = new System.Drawing.Size(101, 23);
            this.openVcalendarButton.TabIndex = 16;
            this.openVcalendarButton.Text = "Open vCalendar";
            this.openVcalendarButton.UseVisualStyleBackColor = true;
            this.openVcalendarButton.Click += new System.EventHandler(this.openVcalendarButton_Click);
            // 
            // sendByEmailGroupbox
            // 
            this.sendByEmailGroupbox.Controls.Add(this.toEmailTextbox);
            this.sendByEmailGroupbox.Controls.Add(this.sendByEmailButton);
            this.sendByEmailGroupbox.Controls.Add(this.smtpServerAddressTextbox);
            this.sendByEmailGroupbox.Controls.Add(this.smtpServerAddressLabel);
            this.sendByEmailGroupbox.Controls.Add(this.toEmailLabel);
            this.sendByEmailGroupbox.Enabled = false;
            this.sendByEmailGroupbox.Location = new System.Drawing.Point(15, 406);
            this.sendByEmailGroupbox.Name = "sendByEmailGroupbox";
            this.sendByEmailGroupbox.Size = new System.Drawing.Size(275, 100);
            this.sendByEmailGroupbox.TabIndex = 20;
            this.sendByEmailGroupbox.TabStop = false;
            this.sendByEmailGroupbox.Text = "Send By Email";
            this.sendByEmailGroupbox.Visible = false;
            // 
            // toEmailTextbox
            // 
            this.toEmailTextbox.Location = new System.Drawing.Point(6, 34);
            this.toEmailTextbox.Name = "toEmailTextbox";
            this.toEmailTextbox.Size = new System.Drawing.Size(174, 20);
            this.toEmailTextbox.TabIndex = 13;
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
            // toEmailLabel
            // 
            this.toEmailLabel.AutoSize = true;
            this.toEmailLabel.Location = new System.Drawing.Point(3, 18);
            this.toEmailLabel.Name = "toEmailLabel";
            this.toEmailLabel.Size = new System.Drawing.Size(50, 13);
            this.toEmailLabel.TabIndex = 12;
            this.toEmailLabel.Text = "To email:";
            // 
            // saveAsButton
            // 
            this.saveAsButton.Enabled = false;
            this.saveAsButton.Location = new System.Drawing.Point(215, 341);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(75, 54);
            this.saveAsButton.TabIndex = 19;
            this.saveAsButton.Text = "Save As";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.saveAsButton_Click);
            // 
            // rawDataTextbox
            // 
            this.rawDataTextbox.Location = new System.Drawing.Point(15, 341);
            this.rawDataTextbox.Multiline = true;
            this.rawDataTextbox.Name = "rawDataTextbox";
            this.rawDataTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rawDataTextbox.Size = new System.Drawing.Size(194, 54);
            this.rawDataTextbox.TabIndex = 18;
            // 
            // openvCalendarDialog
            // 
            this.openvCalendarDialog.DefaultExt = "ics";
            // 
            // savevCalendarDialog
            // 
            this.savevCalendarDialog.DefaultExt = "ics";
            // 
            // ReadWritevCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "ReadWritevCalendar";
            this.Text = "ReadWritevCalendar";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.sendByEmailGroupbox.ResumeLayout(false);
            this.sendByEmailGroupbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label objectLabel;
        private System.Windows.Forms.TextBox objectTextbox;
        private System.Windows.Forms.TextBox placeTextbox;
        private System.Windows.Forms.Label placeLabel;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.DateTimePicker endTime;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.DateTimePicker startTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Button createNewButton;
        private System.Windows.Forms.Button openVcalendarButton;
        private System.Windows.Forms.GroupBox sendByEmailGroupbox;
        private System.Windows.Forms.TextBox toEmailTextbox;
        private System.Windows.Forms.Button sendByEmailButton;
        private System.Windows.Forms.TextBox smtpServerAddressTextbox;
        private System.Windows.Forms.Label smtpServerAddressLabel;
        private System.Windows.Forms.Label toEmailLabel;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.TextBox rawDataTextbox;
        private System.Windows.Forms.OpenFileDialog openvCalendarDialog;
        private System.Windows.Forms.SaveFileDialog savevCalendarDialog;
    }
}
