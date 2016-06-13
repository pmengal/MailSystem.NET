namespace ActiveUp.Net.Samples.CompactSP
{
    partial class frmDnsCheck
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
            this.cboRecordType = new System.Windows.Forms.ComboBox();
            this.txtDNS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.sbrResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.Text = "Server Name";
            // 
            // txtHost
            // 
            this.txtHost.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtHost.Location = new System.Drawing.Point(7, 25);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(228, 28);
            this.txtHost.TabIndex = 1;
            this.txtHost.Text = "activeup.com";
            // 
            // cboRecordType
            // 
            this.cboRecordType.DisplayMember = "Value";
            this.cboRecordType.Items.Add("A");
            this.cboRecordType.Items.Add("NS");
            this.cboRecordType.Items.Add("CNAME");
            this.cboRecordType.Items.Add("SOA");
            this.cboRecordType.Items.Add("MB");
            this.cboRecordType.Items.Add("MG");
            this.cboRecordType.Items.Add("MR");
            this.cboRecordType.Items.Add("PTR");
            this.cboRecordType.Items.Add("MX");
            this.cboRecordType.Items.Add("TXT");
            this.cboRecordType.Items.Add("All");
            this.cboRecordType.Location = new System.Drawing.Point(7, 129);
            this.cboRecordType.Name = "cboRecordType";
            this.cboRecordType.Size = new System.Drawing.Size(228, 22);
            this.cboRecordType.TabIndex = 3;
            // 
            // txtDNS
            // 
            this.txtDNS.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtDNS.Location = new System.Drawing.Point(7, 77);
            this.txtDNS.Name = "txtDNS";
            this.txtDNS.Size = new System.Drawing.Size(228, 28);
            this.txtDNS.TabIndex = 5;
            this.txtDNS.Text = "218.248.240.208";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.Text = "DNS Server";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(7, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 18);
            this.label3.Text = "Query Type";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Look Up";
            this.menuItem1.Click += new System.EventHandler(this.btnLookUp_Click);
            // 
            // sbrResult
            // 
            this.sbrResult.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.sbrResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sbrResult.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.sbrResult.Location = new System.Drawing.Point(0, 237);
            this.sbrResult.Name = "sbrResult";
            this.sbrResult.Size = new System.Drawing.Size(240, 29);
            // 
            // frmDnsCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.sbrResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDNS);
            this.Controls.Add(this.cboRecordType);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.Menu = this.mainMenu1;
            this.Name = "frmDnsCheck";
            this.Text = "Lookup DNS";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.ComboBox cboRecordType;
        private System.Windows.Forms.TextBox txtDNS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Label sbrResult;
    }
}