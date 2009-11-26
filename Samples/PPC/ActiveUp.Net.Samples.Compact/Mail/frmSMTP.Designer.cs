namespace ActiveUp.Net.Samples.Compact
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
            this.pnlServer = new System.Windows.Forms.Panel();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEmail = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.pnlServer.SuspendLayout();
            this.pnlEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkDirectSend
            // 
            this.chkDirectSend.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkDirectSend.Location = new System.Drawing.Point(0, 0);
            this.chkDirectSend.Name = "chkDirectSend";
            this.chkDirectSend.Size = new System.Drawing.Size(238, 26);
            this.chkDirectSend.TabIndex = 0;
            this.chkDirectSend.Text = "Use Direct Send ?";
            this.chkDirectSend.CheckStateChanged += new System.EventHandler(this.chkDirectSend_CheckStateChanged);
            // 
            // pnlServer
            // 
            this.pnlServer.Controls.Add(this.txtPort);
            this.pnlServer.Controls.Add(this.label2);
            this.pnlServer.Controls.Add(this.txtServer);
            this.pnlServer.Controls.Add(this.label1);
            this.pnlServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlServer.Location = new System.Drawing.Point(0, 26);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new System.Drawing.Size(238, 269);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(83, 31);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(112, 23);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "25";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.Text = "Port";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(83, 4);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(112, 23);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "smtp.1and1.com";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.Text = "Server";
            // 
            // pnlEmail
            // 
            this.pnlEmail.Controls.Add(this.btnSend);
            this.pnlEmail.Controls.Add(this.btnAttach);
            this.pnlEmail.Controls.Add(this.txtMessage);
            this.pnlEmail.Controls.Add(this.label6);
            this.pnlEmail.Controls.Add(this.txtSubject);
            this.pnlEmail.Controls.Add(this.label5);
            this.pnlEmail.Controls.Add(this.txtTo);
            this.pnlEmail.Controls.Add(this.label4);
            this.pnlEmail.Controls.Add(this.txtFrom);
            this.pnlEmail.Controls.Add(this.label3);
            this.pnlEmail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEmail.Location = new System.Drawing.Point(0, 86);
            this.pnlEmail.Name = "pnlEmail";
            this.pnlEmail.Size = new System.Drawing.Size(238, 209);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(83, 161);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(128, 30);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send Email";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(3, 133);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(53, 22);
            this.btnAttach.TabIndex = 11;
            this.btnAttach.Text = "Attach";
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(83, 98);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(152, 57);
            this.txtMessage.TabIndex = 9;
            this.txtMessage.Text = "Hi,\r\n\r\ntest email from PPC. How are you doing ?\r\n\r\nContact me :).\r\n\r\n";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.Text = "Message";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(83, 69);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(152, 23);
            this.txtSubject.TabIndex = 6;
            this.txtSubject.Text = "PocketPC SMTP Test";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.Text = "Subject";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(83, 40);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(152, 23);
            this.txtTo.TabIndex = 3;
            this.txtTo.Text = "coresoftindia@gmail.com";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.Text = "To Email";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(83, 11);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(152, 23);
            this.txtFrom.TabIndex = 1;
            this.txtFrom.Text = "testman@mydbonline.com";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.Text = "From Email";
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Word Files|*.doc";
            this.dlgOpen.InitialDirectory = "\\Storage Card\\";
            // 
            // frmSMTP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.pnlEmail);
            this.Controls.Add(this.pnlServer);
            this.Controls.Add(this.chkDirectSend);
            this.Name = "frmSMTP";
            this.Text = "SMTP";
            this.Load += new System.EventHandler(this.frmSMTP_Load);
            this.pnlServer.ResumeLayout(false);
            this.pnlEmail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDirectSend;
        private System.Windows.Forms.Panel pnlServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Panel pnlEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAttach;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.OpenFileDialog dlgOpen;

    }
}