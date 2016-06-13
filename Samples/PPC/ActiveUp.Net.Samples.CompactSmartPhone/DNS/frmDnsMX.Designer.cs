namespace ActiveUp.Net.Samples.CompactSP
{
    partial class frmDnsMX
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtDNS = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.sbrResult = new System.Windows.Forms.Label();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.Text = "DNS Server";
            // 
            // txtDNS
            // 
            this.txtDNS.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtDNS.Location = new System.Drawing.Point(7, 77);
            this.txtDNS.Name = "txtDNS";
            this.txtDNS.Size = new System.Drawing.Size(230, 28);
            this.txtDNS.TabIndex = 13;
            this.txtDNS.Text = "218.248.240.208";
            // 
            // txtHost
            // 
            this.txtHost.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtHost.Location = new System.Drawing.Point(7, 25);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(230, 28);
            this.txtHost.TabIndex = 9;
            this.txtHost.Text = "activeup.com";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.Text = "Server Name";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // sbrResult
            // 
            this.sbrResult.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.sbrResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sbrResult.Location = new System.Drawing.Point(0, 240);
            this.sbrResult.Name = "sbrResult";
            this.sbrResult.Size = new System.Drawing.Size(240, 26);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Look Up";
            this.menuItem1.Click += new System.EventHandler(this.btnLookUp_Click_1);
            // 
            // frmDnsMX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.sbrResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDNS);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.Menu = this.mainMenu1;
            this.Name = "frmDnsMX";
            this.Text = "MX Query";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDNS;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.Label sbrResult;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}