namespace ActiveUp.MailSystem.DesktopClient
{
    partial class AddAccountWizardForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelScreen1 = new System.Windows.Forms.Panel();
            this.chkManualConfiguration = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkRememberPassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelScreen2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtOutgoingPort = new System.Windows.Forms.TextBox();
            this.chkOutPort = new System.Windows.Forms.CheckBox();
            this.chkOutgoingNeedsAuth = new System.Windows.Forms.CheckBox();
            this.chkOutgoingNeedsSSL = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtOutgoingServer = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIncomingPortNumber = new System.Windows.Forms.TextBox();
            this.chkInPort = new System.Windows.Forms.CheckBox();
            this.txtLogInID = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ddlAuthenticationType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.txtIncomingServer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ddlServerType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panelScreen3 = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panelScreen1.SuspendLayout();
            this.panelScreen2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelScreen3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(13, 292);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 2);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(397, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(316, 305);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 15;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(15, 305);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 16;
            this.btnBack.Text = "&Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelScreen1
            // 
            this.panelScreen1.Controls.Add(this.chkManualConfiguration);
            this.panelScreen1.Controls.Add(this.label7);
            this.panelScreen1.Controls.Add(this.txtName);
            this.panelScreen1.Controls.Add(this.label6);
            this.panelScreen1.Controls.Add(this.label5);
            this.panelScreen1.Controls.Add(this.chkRememberPassword);
            this.panelScreen1.Controls.Add(this.txtPassword);
            this.panelScreen1.Controls.Add(this.label4);
            this.panelScreen1.Controls.Add(this.linkLabel1);
            this.panelScreen1.Controls.Add(this.label3);
            this.panelScreen1.Controls.Add(this.txtEmailAddress);
            this.panelScreen1.Controls.Add(this.label2);
            this.panelScreen1.Controls.Add(this.label1);
            this.panelScreen1.Location = new System.Drawing.Point(35, 12);
            this.panelScreen1.Name = "panelScreen1";
            this.panelScreen1.Size = new System.Drawing.Size(383, 274);
            this.panelScreen1.TabIndex = 17;
            // 
            // chkManualConfiguration
            // 
            this.chkManualConfiguration.AutoSize = true;
            this.chkManualConfiguration.Location = new System.Drawing.Point(27, 253);
            this.chkManualConfiguration.Name = "chkManualConfiguration";
            this.chkManualConfiguration.Size = new System.Drawing.Size(283, 17);
            this.chkManualConfiguration.TabIndex = 25;
            this.chkManualConfiguration.Text = "Manually &configure the server settings for this account.";
            this.chkManualConfiguration.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(113, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "For example: John Smith";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 193);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(248, 20);
            this.txtName.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Display Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(329, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "How should your name appear on the emails sent from this account?";
            // 
            // chkRememberPassword
            // 
            this.chkRememberPassword.AutoSize = true;
            this.chkRememberPassword.Location = new System.Drawing.Point(113, 110);
            this.chkRememberPassword.Name = "chkRememberPassword";
            this.chkRememberPassword.Size = new System.Drawing.Size(125, 17);
            this.chkRememberPassword.TabIndex = 20;
            this.chkRememberPassword.Text = "Remember password";
            this.chkRememberPassword.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(110, 83);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(248, 20);
            this.txtPassword.TabIndex = 19;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Password:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(222, 53);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(123, 13);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Get a free email account";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "abc@xyz.com";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(110, 26);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(248, 20);
            this.txtEmailAddress.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Email Address:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Please enter your email account information below:";
            // 
            // panelScreen2
            // 
            this.panelScreen2.Controls.Add(this.groupBox3);
            this.panelScreen2.Controls.Add(this.groupBox2);
            this.panelScreen2.Location = new System.Drawing.Point(35, 12);
            this.panelScreen2.Name = "panelScreen2";
            this.panelScreen2.Size = new System.Drawing.Size(383, 274);
            this.panelScreen2.TabIndex = 18;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtOutgoingPort);
            this.groupBox3.Controls.Add(this.chkOutPort);
            this.groupBox3.Controls.Add(this.chkOutgoingNeedsAuth);
            this.groupBox3.Controls.Add(this.chkOutgoingNeedsSSL);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtOutgoingServer);
            this.groupBox3.Location = new System.Drawing.Point(4, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(376, 81);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Outgoing Server Information ";
            // 
            // txtOutgoingPort
            // 
            this.txtOutgoingPort.Location = new System.Drawing.Point(320, 16);
            this.txtOutgoingPort.Name = "txtOutgoingPort";
            this.txtOutgoingPort.Size = new System.Drawing.Size(50, 20);
            this.txtOutgoingPort.TabIndex = 15;
            // 
            // chkOutPort
            // 
            this.chkOutPort.AutoSize = true;
            this.chkOutPort.Checked = true;
            this.chkOutPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOutPort.Location = new System.Drawing.Point(272, 19);
            this.chkOutPort.Name = "chkOutPort";
            this.chkOutPort.Size = new System.Drawing.Size(48, 17);
            this.chkOutPort.TabIndex = 18;
            this.chkOutPort.Text = "Port:";
            this.chkOutPort.UseVisualStyleBackColor = true;
            this.chkOutPort.CheckedChanged += new System.EventHandler(this.chkOutPort_CheckedChanged);
            // 
            // chkOutgoingNeedsAuth
            // 
            this.chkOutgoingNeedsAuth.AutoSize = true;
            this.chkOutgoingNeedsAuth.Location = new System.Drawing.Point(29, 58);
            this.chkOutgoingNeedsAuth.Name = "chkOutgoingNeedsAuth";
            this.chkOutgoingNeedsAuth.Size = new System.Drawing.Size(221, 17);
            this.chkOutgoingNeedsAuth.TabIndex = 17;
            this.chkOutgoingNeedsAuth.Text = "My outgoing server needs authentication.";
            this.chkOutgoingNeedsAuth.UseVisualStyleBackColor = true;
            // 
            // chkOutgoingNeedsSSL
            // 
            this.chkOutgoingNeedsSSL.AutoSize = true;
            this.chkOutgoingNeedsSSL.Location = new System.Drawing.Point(29, 40);
            this.chkOutgoingNeedsSSL.Name = "chkOutgoingNeedsSSL";
            this.chkOutgoingNeedsSSL.Size = new System.Drawing.Size(271, 17);
            this.chkOutgoingNeedsSSL.TabIndex = 16;
            this.chkOutgoingNeedsSSL.Text = "My outgoing server needs secure connection (SSL).";
            this.chkOutgoingNeedsSSL.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(26, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Outgoing server:";
            // 
            // txtOutgoingServer
            // 
            this.txtOutgoingServer.Location = new System.Drawing.Point(120, 15);
            this.txtOutgoingServer.Name = "txtOutgoingServer";
            this.txtOutgoingServer.Size = new System.Drawing.Size(146, 20);
            this.txtOutgoingServer.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIncomingPortNumber);
            this.groupBox2.Controls.Add(this.chkInPort);
            this.groupBox2.Controls.Add(this.txtLogInID);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.ddlAuthenticationType);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.chkSSL);
            this.groupBox2.Controls.Add(this.txtIncomingServer);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.ddlServerType);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(376, 189);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Incoming Server Information";
            // 
            // txtIncomingPortNumber
            // 
            this.txtIncomingPortNumber.Location = new System.Drawing.Point(320, 51);
            this.txtIncomingPortNumber.Name = "txtIncomingPortNumber";
            this.txtIncomingPortNumber.Size = new System.Drawing.Size(49, 20);
            this.txtIncomingPortNumber.TabIndex = 6;
            // 
            // chkInPort
            // 
            this.chkInPort.AutoSize = true;
            this.chkInPort.Checked = true;
            this.chkInPort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInPort.Location = new System.Drawing.Point(272, 53);
            this.chkInPort.Name = "chkInPort";
            this.chkInPort.Size = new System.Drawing.Size(48, 17);
            this.chkInPort.TabIndex = 12;
            this.chkInPort.Text = "Port:";
            this.chkInPort.UseVisualStyleBackColor = true;
            this.chkInPort.CheckedChanged += new System.EventHandler(this.chkInPort_CheckedChanged);
            // 
            // txtLogInID
            // 
            this.txtLogInID.Location = new System.Drawing.Point(29, 156);
            this.txtLogInID.Name = "txtLogInID";
            this.txtLogInID.Size = new System.Drawing.Size(340, 20);
            this.txtLogInID.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(179, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Login ID (if other than email address)";
            // 
            // ddlAuthenticationType
            // 
            this.ddlAuthenticationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAuthenticationType.FormattingEnabled = true;
            this.ddlAuthenticationType.Location = new System.Drawing.Point(100, 103);
            this.ddlAuthenticationType.Name = "ddlAuthenticationType";
            this.ddlAuthenticationType.Size = new System.Drawing.Size(269, 21);
            this.ddlAuthenticationType.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Log on using ";
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(26, 79);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(221, 17);
            this.chkSSL.TabIndex = 7;
            this.chkSSL.Text = "This server requires a secure connection.";
            this.chkSSL.UseVisualStyleBackColor = true;
            // 
            // txtIncomingServer
            // 
            this.txtIncomingServer.Location = new System.Drawing.Point(117, 51);
            this.txtIncomingServer.Name = "txtIncomingServer";
            this.txtIncomingServer.Size = new System.Drawing.Size(149, 20);
            this.txtIncomingServer.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Incoming server:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(255, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "server.";
            // 
            // ddlServerType
            // 
            this.ddlServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlServerType.FormattingEnabled = true;
            this.ddlServerType.Items.AddRange(new object[] {
            "POP3",
            "IMAP"});
            this.ddlServerType.Location = new System.Drawing.Point(171, 25);
            this.ddlServerType.Name = "ddlServerType";
            this.ddlServerType.Size = new System.Drawing.Size(78, 21);
            this.ddlServerType.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "My incoming mail server is a ";
            // 
            // panelScreen3
            // 
            this.panelScreen3.Controls.Add(this.progressBar);
            this.panelScreen3.Controls.Add(this.labelStatus);
            this.panelScreen3.Location = new System.Drawing.Point(35, 11);
            this.panelScreen3.Name = "panelScreen3";
            this.panelScreen3.Size = new System.Drawing.Size(383, 273);
            this.panelScreen3.TabIndex = 12;
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(3, 52);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(377, 35);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Verifying email configurations...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 127);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(363, 14);
            this.progressBar.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // AddAccountWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 340);
            this.Controls.Add(this.panelScreen3);
            this.Controls.Add(this.panelScreen2);
            this.Controls.Add(this.panelScreen1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAccountWizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add an Email Account";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddAccountWizardForm_FormClosing);
            this.Load += new System.EventHandler(this.AddAccountWizardForm_Load);
            this.panelScreen1.ResumeLayout(false);
            this.panelScreen1.PerformLayout();
            this.panelScreen2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelScreen3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelScreen1;
        private System.Windows.Forms.CheckBox chkManualConfiguration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkRememberPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelScreen2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIncomingServer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddlServerType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLogInID;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox ddlAuthenticationType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.TextBox txtIncomingPortNumber;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtOutgoingPort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtOutgoingServer;
        private System.Windows.Forms.CheckBox chkOutgoingNeedsAuth;
        private System.Windows.Forms.CheckBox chkOutgoingNeedsSSL;
        private System.Windows.Forms.Panel panelScreen3;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox chkOutPort;
        private System.Windows.Forms.CheckBox chkInPort;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer timer;
    }
}