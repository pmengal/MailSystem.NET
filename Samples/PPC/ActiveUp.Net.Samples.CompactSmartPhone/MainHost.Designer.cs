namespace ActiveUp.Net.Samples.CompactSP
{
    partial class MainHost
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainHost));
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuMain = new System.Windows.Forms.MenuItem();
            this.mnuItemDNS = new System.Windows.Forms.MenuItem();
            this.mnuItemDNS_DNSAny = new System.Windows.Forms.MenuItem();
            this.mnuItemDNS_DNSMX = new System.Windows.Forms.MenuItem();
            this.mnuItemMail = new System.Windows.Forms.MenuItem();
            this.mnuItemMailNNTP = new System.Windows.Forms.MenuItem();
            this.mnuItemMailPOP3 = new System.Windows.Forms.MenuItem();
            this.mnuItemMailIMAP4 = new System.Windows.Forms.MenuItem();
            this.mnuItemMailSMTP = new System.Windows.Forms.MenuItem();
            this.mnuItemWhoIs = new System.Windows.Forms.MenuItem();
            this.mnuItemExit = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuMain);
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.Add(this.mnuItemDNS);
            this.mnuMain.MenuItems.Add(this.mnuItemMail);
            this.mnuMain.MenuItems.Add(this.mnuItemWhoIs);
            this.mnuMain.MenuItems.Add(this.mnuItemExit);
            this.mnuMain.Text = "Menu";
            // 
            // mnuItemDNS
            // 
            this.mnuItemDNS.MenuItems.Add(this.mnuItemDNS_DNSAny);
            this.mnuItemDNS.MenuItems.Add(this.mnuItemDNS_DNSMX);
            this.mnuItemDNS.Text = "DNS";
            // 
            // mnuItemDNS_DNSAny
            // 
            this.mnuItemDNS_DNSAny.Text = "DNS Any";
            this.mnuItemDNS_DNSAny.Click += new System.EventHandler(this.mnuItemDNS_DNSAny_Click);
            // 
            // mnuItemDNS_DNSMX
            // 
            this.mnuItemDNS_DNSMX.Text = "DNS MX";
            this.mnuItemDNS_DNSMX.Click += new System.EventHandler(this.mnuItemDNS_DNSMX_Click);
            // 
            // mnuItemMail
            // 
            this.mnuItemMail.MenuItems.Add(this.mnuItemMailNNTP);
            this.mnuItemMail.MenuItems.Add(this.mnuItemMailPOP3);
            this.mnuItemMail.MenuItems.Add(this.mnuItemMailIMAP4);
            this.mnuItemMail.MenuItems.Add(this.mnuItemMailSMTP);
            this.mnuItemMail.Text = "Mail";
            // 
            // mnuItemMailNNTP
            // 
            this.mnuItemMailNNTP.Text = "NNTP";
            this.mnuItemMailNNTP.Click += new System.EventHandler(this.mnuItemMailNNTP_Click);
            // 
            // mnuItemMailPOP3
            // 
            this.mnuItemMailPOP3.Text = "POP3";
            this.mnuItemMailPOP3.Click += new System.EventHandler(this.mnuItemMailPOP3_Click);
            // 
            // mnuItemMailIMAP4
            // 
            this.mnuItemMailIMAP4.Text = "IMAP4";
            this.mnuItemMailIMAP4.Click += new System.EventHandler(this.mnuItemMailIMAP4_Click);
            // 
            // mnuItemMailSMTP
            // 
            this.mnuItemMailSMTP.Text = "SMTP";
            this.mnuItemMailSMTP.Click += new System.EventHandler(this.mnuItemMailSMTP_Click);
            // 
            // mnuItemWhoIs
            // 
            this.mnuItemWhoIs.Text = "Whois";
            this.mnuItemWhoIs.Click += new System.EventHandler(this.mnuItemWhoIs_Click);
            // 
            // mnuItemExit
            // 
            this.mnuItemExit.Text = "E&xit";
            this.mnuItemExit.Click += new System.EventHandler(this.mnuItemExit_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(56, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 22);
            this.label1.Text = "ActiveUp.Net PPC Samples";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 50);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(0, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 60);
            this.label2.Text = "Use the menu to explore the various options available through the library";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.Menu = this.mainMenu;
            this.Name = "MainHost";
            this.Text = "ActiveUp.Net.Samples";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuMain;
        private System.Windows.Forms.MenuItem mnuItemDNS;
        private System.Windows.Forms.MenuItem mnuItemMail;
        private System.Windows.Forms.MenuItem mnuItemWhoIs;
        private System.Windows.Forms.MenuItem mnuItemMailNNTP;
        private System.Windows.Forms.MenuItem mnuItemMailPOP3;
        private System.Windows.Forms.MenuItem mnuItemDNS_DNSAny;
        private System.Windows.Forms.MenuItem mnuItemDNS_DNSMX;
        private System.Windows.Forms.MenuItem mnuItemMailIMAP4;
        private System.Windows.Forms.MenuItem mnuItemMailSMTP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuItem mnuItemExit;
        private System.Windows.Forms.Label label2;
    }
}