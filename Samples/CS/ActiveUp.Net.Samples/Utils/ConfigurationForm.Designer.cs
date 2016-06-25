namespace ActiveUp.Net.Samples.Utils
{
    partial class ConfigurationForm
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
            this._gbMessage = new System.Windows.Forms.GroupBox();
            this._tbBodyText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this._tbBodyHtml = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._tbSubject = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._tbReturnReceipt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._tbReplyTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._tbConfirmRead = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._tbToEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._tbToName = new System.Windows.Forms.TextBox();
            this._lToName = new System.Windows.Forms.Label();
            this._tbFromEmail = new System.Windows.Forms.TextBox();
            this._lFromEmail = new System.Windows.Forms.Label();
            this._tbFromName = new System.Windows.Forms.TextBox();
            this._lFromName = new System.Windows.Forms.Label();
            this._gbPop3 = new System.Windows.Forms.GroupBox();
            this._tbPop3Server = new System.Windows.Forms.TextBox();
            this._lPop3Server = new System.Windows.Forms.Label();
            this._tbPop3Password = new System.Windows.Forms.TextBox();
            this._lPop3Password = new System.Windows.Forms.Label();
            this._tbPop3Username = new System.Windows.Forms.TextBox();
            this._lPop3Username = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._tbImap4SearchPattern = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._tbImap4Server = new System.Windows.Forms.TextBox();
            this._lImap4Server = new System.Windows.Forms.Label();
            this._tbImap4Password = new System.Windows.Forms.TextBox();
            this._lImap4Password = new System.Windows.Forms.Label();
            this._tbImap4Username = new System.Windows.Forms.TextBox();
            this._lImap4Username = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._tbSmtpBackupServer = new System.Windows.Forms.TextBox();
            this._lBackupServer = new System.Windows.Forms.Label();
            this._tbSmtpServer = new System.Windows.Forms.TextBox();
            this._lSmtpServer = new System.Windows.Forms.Label();
            this._gbNntp = new System.Windows.Forms.GroupBox();
            this._tbNntpServer = new System.Windows.Forms.TextBox();
            this._lNntpServer = new System.Windows.Forms.Label();
            this._tbNewsgroup = new System.Windows.Forms.TextBox();
            this._lNntpNewsgroup = new System.Windows.Forms.Label();
            this._bCancel = new System.Windows.Forms.Button();
            this._bSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._tbDnsServer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pleaseLabel = new System.Windows.Forms.Label();
            this._gbMessage.SuspendLayout();
            this._gbPop3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this._gbNntp.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gbMessage
            // 
            this._gbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._gbMessage.Controls.Add(this._tbBodyText);
            this._gbMessage.Controls.Add(this.label8);
            this._gbMessage.Controls.Add(this._tbBodyHtml);
            this._gbMessage.Controls.Add(this.label7);
            this._gbMessage.Controls.Add(this._tbSubject);
            this._gbMessage.Controls.Add(this.label6);
            this._gbMessage.Controls.Add(this._tbReturnReceipt);
            this._gbMessage.Controls.Add(this.label4);
            this._gbMessage.Controls.Add(this._tbReplyTo);
            this._gbMessage.Controls.Add(this.label2);
            this._gbMessage.Controls.Add(this._tbConfirmRead);
            this._gbMessage.Controls.Add(this.label1);
            this._gbMessage.Controls.Add(this._tbToEmail);
            this._gbMessage.Controls.Add(this.label3);
            this._gbMessage.Controls.Add(this._tbToName);
            this._gbMessage.Controls.Add(this._lToName);
            this._gbMessage.Controls.Add(this._tbFromEmail);
            this._gbMessage.Controls.Add(this._lFromEmail);
            this._gbMessage.Controls.Add(this._tbFromName);
            this._gbMessage.Controls.Add(this._lFromName);
            this._gbMessage.Location = new System.Drawing.Point(6, 6);
            this._gbMessage.Name = "_gbMessage";
            this._gbMessage.Size = new System.Drawing.Size(542, 275);
            this._gbMessage.TabIndex = 0;
            this._gbMessage.TabStop = false;
            this._gbMessage.Text = "Message";
            // 
            // _tbBodyText
            // 
            this._tbBodyText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbBodyText.Location = new System.Drawing.Point(139, 170);
            this._tbBodyText.Name = "_tbBodyText";
            this._tbBodyText.Size = new System.Drawing.Size(397, 20);
            this._tbBodyText.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 170);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(126, 19);
            this.label8.TabIndex = 12;
            this.label8.Text = "Body text :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbBodyHtml
            // 
            this._tbBodyHtml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbBodyHtml.Location = new System.Drawing.Point(139, 144);
            this._tbBodyHtml.Name = "_tbBodyHtml";
            this._tbBodyHtml.Size = new System.Drawing.Size(397, 20);
            this._tbBodyHtml.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 144);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(126, 19);
            this.label7.TabIndex = 10;
            this.label7.Text = "Body html :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbSubject
            // 
            this._tbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbSubject.Location = new System.Drawing.Point(139, 118);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(397, 20);
            this._tbSubject.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 118);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(126, 19);
            this.label6.TabIndex = 8;
            this.label6.Text = "Subject :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbReturnReceipt
            // 
            this._tbReturnReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbReturnReceipt.Location = new System.Drawing.Point(139, 248);
            this._tbReturnReceipt.Name = "_tbReturnReceipt";
            this._tbReturnReceipt.Size = new System.Drawing.Size(397, 20);
            this._tbReturnReceipt.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 248);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(126, 19);
            this.label4.TabIndex = 18;
            this.label4.Text = "Return receipt :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbReplyTo
            // 
            this._tbReplyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbReplyTo.Location = new System.Drawing.Point(139, 222);
            this._tbReplyTo.Name = "_tbReplyTo";
            this._tbReplyTo.Size = new System.Drawing.Size(397, 20);
            this._tbReplyTo.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 222);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(126, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "Reply-to email :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbConfirmRead
            // 
            this._tbConfirmRead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbConfirmRead.Location = new System.Drawing.Point(139, 196);
            this._tbConfirmRead.Name = "_tbConfirmRead";
            this._tbConfirmRead.Size = new System.Drawing.Size(397, 20);
            this._tbConfirmRead.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 196);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(126, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "Confirm read email :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbToEmail
            // 
            this._tbToEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbToEmail.Location = new System.Drawing.Point(139, 92);
            this._tbToEmail.Name = "_tbToEmail";
            this._tbToEmail.Size = new System.Drawing.Size(397, 20);
            this._tbToEmail.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 92);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(126, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "To email :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbToName
            // 
            this._tbToName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbToName.Location = new System.Drawing.Point(139, 66);
            this._tbToName.Name = "_tbToName";
            this._tbToName.Size = new System.Drawing.Size(397, 20);
            this._tbToName.TabIndex = 5;
            // 
            // _lToName
            // 
            this._lToName.Location = new System.Drawing.Point(6, 66);
            this._lToName.Name = "_lToName";
            this._lToName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lToName.Size = new System.Drawing.Size(126, 19);
            this._lToName.TabIndex = 4;
            this._lToName.Text = "To name :";
            this._lToName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbFromEmail.Location = new System.Drawing.Point(138, 42);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.Size = new System.Drawing.Size(397, 20);
            this._tbFromEmail.TabIndex = 3;
            // 
            // _lFromEmail
            // 
            this._lFromEmail.Location = new System.Drawing.Point(6, 40);
            this._lFromEmail.Name = "_lFromEmail";
            this._lFromEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lFromEmail.Size = new System.Drawing.Size(126, 19);
            this._lFromEmail.TabIndex = 2;
            this._lFromEmail.Text = "From email :";
            this._lFromEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbFromName
            // 
            this._tbFromName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbFromName.Location = new System.Drawing.Point(139, 16);
            this._tbFromName.Name = "_tbFromName";
            this._tbFromName.Size = new System.Drawing.Size(397, 20);
            this._tbFromName.TabIndex = 1;
            // 
            // _lFromName
            // 
            this._lFromName.Location = new System.Drawing.Point(6, 16);
            this._lFromName.Name = "_lFromName";
            this._lFromName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lFromName.Size = new System.Drawing.Size(126, 19);
            this._lFromName.TabIndex = 0;
            this._lFromName.Text = "From name :";
            this._lFromName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _gbPop3
            // 
            this._gbPop3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._gbPop3.Controls.Add(this._tbPop3Server);
            this._gbPop3.Controls.Add(this._lPop3Server);
            this._gbPop3.Controls.Add(this._tbPop3Password);
            this._gbPop3.Controls.Add(this._lPop3Password);
            this._gbPop3.Controls.Add(this._tbPop3Username);
            this._gbPop3.Controls.Add(this._lPop3Username);
            this._gbPop3.Location = new System.Drawing.Point(6, 6);
            this._gbPop3.Name = "_gbPop3";
            this._gbPop3.Size = new System.Drawing.Size(542, 100);
            this._gbPop3.TabIndex = 0;
            this._gbPop3.TabStop = false;
            this._gbPop3.Text = "Pop3";
            // 
            // _tbPop3Server
            // 
            this._tbPop3Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbPop3Server.Location = new System.Drawing.Point(142, 71);
            this._tbPop3Server.Name = "_tbPop3Server";
            this._tbPop3Server.Size = new System.Drawing.Size(397, 20);
            this._tbPop3Server.TabIndex = 5;
            // 
            // _lPop3Server
            // 
            this._lPop3Server.Location = new System.Drawing.Point(9, 71);
            this._lPop3Server.Name = "_lPop3Server";
            this._lPop3Server.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lPop3Server.Size = new System.Drawing.Size(126, 19);
            this._lPop3Server.TabIndex = 4;
            this._lPop3Server.Text = "Server :";
            this._lPop3Server.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbPop3Password
            // 
            this._tbPop3Password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbPop3Password.Location = new System.Drawing.Point(142, 45);
            this._tbPop3Password.Name = "_tbPop3Password";
            this._tbPop3Password.Size = new System.Drawing.Size(397, 20);
            this._tbPop3Password.TabIndex = 3;
            // 
            // _lPop3Password
            // 
            this._lPop3Password.Location = new System.Drawing.Point(9, 45);
            this._lPop3Password.Name = "_lPop3Password";
            this._lPop3Password.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lPop3Password.Size = new System.Drawing.Size(126, 19);
            this._lPop3Password.TabIndex = 2;
            this._lPop3Password.Text = "Password :";
            this._lPop3Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbPop3Username
            // 
            this._tbPop3Username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbPop3Username.Location = new System.Drawing.Point(142, 19);
            this._tbPop3Username.Name = "_tbPop3Username";
            this._tbPop3Username.Size = new System.Drawing.Size(397, 20);
            this._tbPop3Username.TabIndex = 1;
            // 
            // _lPop3Username
            // 
            this._lPop3Username.Location = new System.Drawing.Point(9, 19);
            this._lPop3Username.Name = "_lPop3Username";
            this._lPop3Username.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lPop3Username.Size = new System.Drawing.Size(126, 19);
            this._lPop3Username.TabIndex = 0;
            this._lPop3Username.Text = "User name :";
            this._lPop3Username.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._tbImap4SearchPattern);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this._tbImap4Server);
            this.groupBox1.Controls.Add(this._lImap4Server);
            this.groupBox1.Controls.Add(this._tbImap4Password);
            this.groupBox1.Controls.Add(this._lImap4Password);
            this.groupBox1.Controls.Add(this._tbImap4Username);
            this.groupBox1.Controls.Add(this._lImap4Username);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imap4";
            // 
            // _tbImap4SearchPattern
            // 
            this._tbImap4SearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbImap4SearchPattern.Location = new System.Drawing.Point(142, 97);
            this._tbImap4SearchPattern.Name = "_tbImap4SearchPattern";
            this._tbImap4SearchPattern.Size = new System.Drawing.Size(397, 20);
            this._tbImap4SearchPattern.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 97);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(126, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Search pattern :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbImap4Server
            // 
            this._tbImap4Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbImap4Server.Location = new System.Drawing.Point(142, 71);
            this._tbImap4Server.Name = "_tbImap4Server";
            this._tbImap4Server.Size = new System.Drawing.Size(397, 20);
            this._tbImap4Server.TabIndex = 5;
            // 
            // _lImap4Server
            // 
            this._lImap4Server.Location = new System.Drawing.Point(9, 71);
            this._lImap4Server.Name = "_lImap4Server";
            this._lImap4Server.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lImap4Server.Size = new System.Drawing.Size(126, 19);
            this._lImap4Server.TabIndex = 4;
            this._lImap4Server.Text = "Server :";
            this._lImap4Server.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbImap4Password
            // 
            this._tbImap4Password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbImap4Password.Location = new System.Drawing.Point(142, 45);
            this._tbImap4Password.Name = "_tbImap4Password";
            this._tbImap4Password.Size = new System.Drawing.Size(397, 20);
            this._tbImap4Password.TabIndex = 3;
            // 
            // _lImap4Password
            // 
            this._lImap4Password.Location = new System.Drawing.Point(9, 45);
            this._lImap4Password.Name = "_lImap4Password";
            this._lImap4Password.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lImap4Password.Size = new System.Drawing.Size(126, 19);
            this._lImap4Password.TabIndex = 2;
            this._lImap4Password.Text = "Password :";
            this._lImap4Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbImap4Username
            // 
            this._tbImap4Username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbImap4Username.Location = new System.Drawing.Point(142, 19);
            this._tbImap4Username.Name = "_tbImap4Username";
            this._tbImap4Username.Size = new System.Drawing.Size(397, 20);
            this._tbImap4Username.TabIndex = 1;
            // 
            // _lImap4Username
            // 
            this._lImap4Username.Location = new System.Drawing.Point(9, 19);
            this._lImap4Username.Name = "_lImap4Username";
            this._lImap4Username.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lImap4Username.Size = new System.Drawing.Size(126, 19);
            this._lImap4Username.TabIndex = 0;
            this._lImap4Username.Text = "User name :";
            this._lImap4Username.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._tbSmtpBackupServer);
            this.groupBox2.Controls.Add(this._lBackupServer);
            this.groupBox2.Controls.Add(this._tbSmtpServer);
            this.groupBox2.Controls.Add(this._lSmtpServer);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(542, 75);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Smtp";
            // 
            // _tbSmtpBackupServer
            // 
            this._tbSmtpBackupServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbSmtpBackupServer.Location = new System.Drawing.Point(142, 45);
            this._tbSmtpBackupServer.Name = "_tbSmtpBackupServer";
            this._tbSmtpBackupServer.Size = new System.Drawing.Size(397, 20);
            this._tbSmtpBackupServer.TabIndex = 3;
            // 
            // _lBackupServer
            // 
            this._lBackupServer.Location = new System.Drawing.Point(9, 45);
            this._lBackupServer.Name = "_lBackupServer";
            this._lBackupServer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lBackupServer.Size = new System.Drawing.Size(126, 19);
            this._lBackupServer.TabIndex = 2;
            this._lBackupServer.Text = "Backup Server:";
            this._lBackupServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbSmtpServer
            // 
            this._tbSmtpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbSmtpServer.Location = new System.Drawing.Point(142, 19);
            this._tbSmtpServer.Name = "_tbSmtpServer";
            this._tbSmtpServer.Size = new System.Drawing.Size(397, 20);
            this._tbSmtpServer.TabIndex = 1;
            // 
            // _lSmtpServer
            // 
            this._lSmtpServer.Location = new System.Drawing.Point(9, 19);
            this._lSmtpServer.Name = "_lSmtpServer";
            this._lSmtpServer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lSmtpServer.Size = new System.Drawing.Size(126, 19);
            this._lSmtpServer.TabIndex = 0;
            this._lSmtpServer.Text = "Server :";
            this._lSmtpServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _gbNntp
            // 
            this._gbNntp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._gbNntp.Controls.Add(this._tbNntpServer);
            this._gbNntp.Controls.Add(this._lNntpServer);
            this._gbNntp.Controls.Add(this._tbNewsgroup);
            this._gbNntp.Controls.Add(this._lNntpNewsgroup);
            this._gbNntp.Location = new System.Drawing.Point(6, 6);
            this._gbNntp.Name = "_gbNntp";
            this._gbNntp.Size = new System.Drawing.Size(542, 75);
            this._gbNntp.TabIndex = 0;
            this._gbNntp.TabStop = false;
            this._gbNntp.Text = "Nntp";
            // 
            // _tbNntpServer
            // 
            this._tbNntpServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbNntpServer.Location = new System.Drawing.Point(142, 45);
            this._tbNntpServer.Name = "_tbNntpServer";
            this._tbNntpServer.Size = new System.Drawing.Size(397, 20);
            this._tbNntpServer.TabIndex = 3;
            // 
            // _lNntpServer
            // 
            this._lNntpServer.Location = new System.Drawing.Point(9, 45);
            this._lNntpServer.Name = "_lNntpServer";
            this._lNntpServer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lNntpServer.Size = new System.Drawing.Size(126, 19);
            this._lNntpServer.TabIndex = 2;
            this._lNntpServer.Text = "Server:";
            this._lNntpServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbNewsgroup
            // 
            this._tbNewsgroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbNewsgroup.Location = new System.Drawing.Point(142, 19);
            this._tbNewsgroup.Name = "_tbNewsgroup";
            this._tbNewsgroup.Size = new System.Drawing.Size(397, 20);
            this._tbNewsgroup.TabIndex = 1;
            // 
            // _lNntpNewsgroup
            // 
            this._lNntpNewsgroup.Location = new System.Drawing.Point(9, 19);
            this._lNntpNewsgroup.Name = "_lNntpNewsgroup";
            this._lNntpNewsgroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._lNntpNewsgroup.Size = new System.Drawing.Size(126, 19);
            this._lNntpNewsgroup.TabIndex = 0;
            this._lNntpNewsgroup.Text = "Newsgroup :";
            this._lNntpNewsgroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _bCancel
            // 
            this._bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._bCancel.Location = new System.Drawing.Point(496, 349);
            this._bCancel.Name = "_bCancel";
            this._bCancel.Size = new System.Drawing.Size(75, 23);
            this._bCancel.TabIndex = 23;
            this._bCancel.Text = "Cancel";
            this._bCancel.UseVisualStyleBackColor = true;
            this._bCancel.Click += new System.EventHandler(this._bCancel_Click);
            // 
            // _bSave
            // 
            this._bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._bSave.Location = new System.Drawing.Point(415, 349);
            this._bSave.Name = "_bSave";
            this._bSave.Size = new System.Drawing.Size(75, 23);
            this._bSave.TabIndex = 24;
            this._bSave.Text = "Save";
            this._bSave.UseVisualStyleBackColor = true;
            this._bSave.Click += new System.EventHandler(this._bSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(12, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(559, 314);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._gbMessage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(551, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Message";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._gbPop3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(551, 288);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pop3";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(551, 288);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Imap4";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(551, 288);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Smtp";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this._gbNntp);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(551, 288);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Nntp";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox3);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(551, 288);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Validation";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this._tbDnsServer);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(542, 50);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Nntp";
            // 
            // _tbDnsServer
            // 
            this._tbDnsServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbDnsServer.Location = new System.Drawing.Point(142, 19);
            this._tbDnsServer.Name = "_tbDnsServer";
            this._tbDnsServer.Size = new System.Drawing.Size(397, 20);
            this._tbDnsServer.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(9, 19);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label12.Size = new System.Drawing.Size(126, 19);
            this.label12.TabIndex = 0;
            this.label12.Text = "Dns server :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(142, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(397, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 45);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(126, 19);
            this.label9.TabIndex = 2;
            this.label9.Text = "Server:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(142, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(397, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(9, 19);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(126, 19);
            this.label10.TabIndex = 0;
            this.label10.Text = "Newsgroup :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pleaseLabel
            // 
            this.pleaseLabel.AutoSize = true;
            this.pleaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pleaseLabel.Location = new System.Drawing.Point(12, 9);
            this.pleaseLabel.Name = "pleaseLabel";
            this.pleaseLabel.Size = new System.Drawing.Size(371, 13);
            this.pleaseLabel.TabIndex = 25;
            this.pleaseLabel.Text = "Please define default parameters to use with the samples below:";
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this._bSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._bCancel;
            this.ClientSize = new System.Drawing.Size(576, 379);
            this.Controls.Add(this.pleaseLabel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this._bSave);
            this.Controls.Add(this._bCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration";
            this._gbMessage.ResumeLayout(false);
            this._gbMessage.PerformLayout();
            this._gbPop3.ResumeLayout(false);
            this._gbPop3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this._gbNntp.ResumeLayout(false);
            this._gbNntp.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _gbMessage;
        private System.Windows.Forms.GroupBox _gbPop3;
        private System.Windows.Forms.TextBox _tbPop3Server;
        private System.Windows.Forms.Label _lPop3Server;
        private System.Windows.Forms.TextBox _tbPop3Password;
        private System.Windows.Forms.Label _lPop3Password;
        private System.Windows.Forms.TextBox _tbPop3Username;
        private System.Windows.Forms.Label _lPop3Username;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _tbImap4Server;
        private System.Windows.Forms.Label _lImap4Server;
        private System.Windows.Forms.TextBox _tbImap4Password;
        private System.Windows.Forms.Label _lImap4Password;
        private System.Windows.Forms.TextBox _tbImap4Username;
        private System.Windows.Forms.Label _lImap4Username;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox _tbSmtpBackupServer;
        private System.Windows.Forms.Label _lBackupServer;
        private System.Windows.Forms.TextBox _tbSmtpServer;
        private System.Windows.Forms.Label _lSmtpServer;
        private System.Windows.Forms.GroupBox _gbNntp;
        private System.Windows.Forms.TextBox _tbNntpServer;
        private System.Windows.Forms.Label _lNntpServer;
        private System.Windows.Forms.TextBox _tbNewsgroup;
        private System.Windows.Forms.Label _lNntpNewsgroup;
        private System.Windows.Forms.TextBox _tbImap4SearchPattern;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button _bCancel;
        private System.Windows.Forms.Button _bSave;
        private System.Windows.Forms.TextBox _tbReturnReceipt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _tbReplyTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _tbConfirmRead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _tbToEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _tbToName;
        private System.Windows.Forms.Label _lToName;
        private System.Windows.Forms.TextBox _tbFromEmail;
        private System.Windows.Forms.Label _lFromEmail;
        private System.Windows.Forms.TextBox _tbFromName;
        private System.Windows.Forms.Label _lFromName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox _tbBodyText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _tbBodyHtml;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox _tbSubject;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox _tbDnsServer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label pleaseLabel;
    }
}