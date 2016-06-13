namespace ActiveUp.Net.Samples.CompactSP
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
            this.chkAPOP = new System.Windows.Forms.CheckBox();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuItemRetrieve = new System.Windows.Forms.MenuItem();
            this.sbrStatus = new System.Windows.Forms.Label();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 23);
            this.label1.Text = "Server";
            // 
            // txtHost
            // 
            this.txtHost.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtHost.Location = new System.Drawing.Point(7, 33);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(226, 26);
            this.txtHost.TabIndex = 1;
            this.txtHost.Text = "pop.1and1.com";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtPort.Location = new System.Drawing.Point(56, 67);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(44, 26);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "110";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 18);
            this.label3.Text = "Username";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtUser.Location = new System.Drawing.Point(7, 120);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(226, 26);
            this.txtUser.TabIndex = 7;
            this.txtUser.Text = "testman@mydbonline.com";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtPassword.Location = new System.Drawing.Point(7, 172);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(226, 26);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.Text = "testtest";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(7, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 18);
            this.label4.Text = "Password";
            // 
            // chkAPOP
            // 
            this.chkAPOP.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.chkAPOP.Location = new System.Drawing.Point(133, 72);
            this.chkAPOP.Name = "chkAPOP";
            this.chkAPOP.Size = new System.Drawing.Size(100, 20);
            this.chkAPOP.TabIndex = 18;
            this.chkAPOP.Text = "Use APOP ?";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuItemRetrieve);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // mnuItemRetrieve
            // 
            this.mnuItemRetrieve.Text = "Retrieve";
            this.mnuItemRetrieve.Click += new System.EventHandler(this.btnClick_Click);
            // 
            // sbrStatus
            // 
            this.sbrStatus.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.sbrStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sbrStatus.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.sbrStatus.Location = new System.Drawing.Point(0, 236);
            this.sbrStatus.Name = "sbrStatus";
            this.sbrStatus.Size = new System.Drawing.Size(240, 30);
            this.sbrStatus.Text = "Ready";
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Done";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // frmPOP3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.chkAPOP);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.Menu = this.mainMenu1;
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
        private System.Windows.Forms.CheckBox chkAPOP;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuItemRetrieve;
        private System.Windows.Forms.Label sbrStatus;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}