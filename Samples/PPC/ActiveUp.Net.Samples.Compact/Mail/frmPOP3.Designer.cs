namespace ActiveUp.Net.Samples.Compact
{
    partial class frmPOP3
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sbrStatus = new System.Windows.Forms.StatusBar();
            this.btnClick = new System.Windows.Forms.Button();
            this.chkAPOP = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.Text = "Server";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(90, 23);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(143, 23);
            this.txtHost.TabIndex = 1;
            this.txtHost.Text = "pop.1and1.com";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(90, 50);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(29, 23);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "110";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 18);
            this.label3.Text = "Username";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(90, 76);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(143, 23);
            this.txtUser.TabIndex = 7;
            this.txtUser.Text = "testman@mydbonline.com";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(90, 105);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(143, 23);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.Text = "testtest";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 18);
            this.label4.Text = "Password";
            // 
            // sbrStatus
            // 
            this.sbrStatus.Location = new System.Drawing.Point(0, 271);
            this.sbrStatus.Name = "sbrStatus";
            this.sbrStatus.Size = new System.Drawing.Size(238, 24);
            this.sbrStatus.Text = "Ready";
            // 
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(35, 184);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(164, 72);
            this.btnClick.TabIndex = 12;
            this.btnClick.Text = "Retrieve Mail";
            this.btnClick.Click += new System.EventHandler(this.btnClick_Click);
            // 
            // chkAPOP
            // 
            this.chkAPOP.Location = new System.Drawing.Point(90, 135);
            this.chkAPOP.Name = "chkAPOP";
            this.chkAPOP.Size = new System.Drawing.Size(100, 20);
            this.chkAPOP.TabIndex = 18;
            this.chkAPOP.Text = "Use APOP ?";
            // 
            // frmPOP3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.chkAPOP);
            this.Controls.Add(this.btnClick);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label1);
            this.Name = "frmPOP3";
            this.Text = "POP3 Mail Client";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusBar sbrStatus;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.CheckBox chkAPOP;
    }
}