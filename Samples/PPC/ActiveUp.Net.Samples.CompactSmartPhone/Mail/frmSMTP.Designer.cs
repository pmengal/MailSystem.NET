namespace ActiveUp.Net.Samples.CompactSP
{
    partial class frmSMTP
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
            this.chkDirectSend = new System.Windows.Forms.CheckBox();
            this.pnlEmail = new System.Windows.Forms.Panel();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pnlEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkDirectSend
            // 
            this.chkDirectSend.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkDirectSend.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.chkDirectSend.Location = new System.Drawing.Point(0, 0);
            this.chkDirectSend.Name = "chkDirectSend";
            this.chkDirectSend.Size = new System.Drawing.Size(240, 26);
            this.chkDirectSend.TabIndex = 0;
            this.chkDirectSend.Text = "Use Direct Send ?";
            this.chkDirectSend.CheckStateChanged += new System.EventHandler(this.chkDirectSend_CheckStateChanged);
            // 
            // pnlEmail
            // 
            this.pnlEmail.Controls.Add(this.txtMessage);
            this.pnlEmail.Controls.Add(this.label6);
            this.pnlEmail.Controls.Add(this.txtSubject);
            this.pnlEmail.Controls.Add(this.label5);
            this.pnlEmail.Controls.Add(this.txtTo);
            this.pnlEmail.Controls.Add(this.label4);
            this.pnlEmail.Controls.Add(this.txtFrom);
            this.pnlEmail.Controls.Add(this.label3);
            this.pnlEmail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEmail.Location = new System.Drawing.Point(0, 82);
            this.pnlEmail.Name = "pnlEmail";
            this.pnlEmail.Size = new System.Drawing.Size(240, 184);
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMessage.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtMessage.Location = new System.Drawing.Point(0, 109);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(240, 75);
            this.txtMessage.TabIndex = 9;
            this.txtMessage.Text = "Hi,\r\n\r\ntest email from PPC. How are you doing ?\r\n\r\nContact me :).\r\n\r\n";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(7, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.Text = "Message";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtSubject.Location = new System.Drawing.Point(83, 59);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(152, 26);
            this.txtSubject.TabIndex = 6;
            this.txtSubject.Text = "PocketPC SMTP Test";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(7, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.Text = "Subject";
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtTo.Location = new System.Drawing.Point(83, 32);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(152, 26);
            this.txtTo.TabIndex = 3;
            this.txtTo.Text = "coresoftindia@gmail.com";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(7, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.Text = "To";
            // 
            // txtFrom
            // 
            this.txtFrom.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtFrom.Location = new System.Drawing.Point(83, 5);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(152, 26);
            this.txtFrom.TabIndex = 1;
            this.txtFrom.Text = "testman@mydbonline.com";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(7, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.Text = "From";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtPort.Location = new System.Drawing.Point(66, 56);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(52, 26);
            this.txtPort.TabIndex = 7;
            this.txtPort.Text = "25";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.Text = "Port";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtServer.Location = new System.Drawing.Point(66, 29);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(171, 26);
            this.txtServer.TabIndex = 6;
            this.txtServer.Text = "smtp.1and1.com";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.Text = "Server";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Send";
            this.menuItem1.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Done";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // frmSMTP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlEmail);
            this.Controls.Add(this.chkDirectSend);
            this.Menu = this.mainMenu1;
            this.Name = "frmSMTP";
            this.Text = "SMTP";
            this.Load += new System.EventHandler(this.frmSMTP_Load);
            this.pnlEmail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDirectSend;
        private System.Windows.Forms.Panel pnlEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;

    }
}