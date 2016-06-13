namespace ActiveUp.Net.Samples.Compact
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
            this.btnLookUp = new System.Windows.Forms.Button();
            this.cboRecordType = new System.Windows.Forms.ComboBox();
            this.sbrResult = new System.Windows.Forms.StatusBar();
            this.txtDNS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.Text = "Server Name";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(15, 45);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(202, 23);
            this.txtHost.TabIndex = 1;
            this.txtHost.Text = "activeup.com";
            // 
            // btnLookUp
            // 
            this.btnLookUp.Location = new System.Drawing.Point(15, 185);
            this.btnLookUp.Name = "btnLookUp";
            this.btnLookUp.Size = new System.Drawing.Size(202, 68);
            this.btnLookUp.TabIndex = 2;
            this.btnLookUp.Text = "&LookUp Resource\r\nRecord";
            this.btnLookUp.Click += new System.EventHandler(this.btnLookUp_Click);
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
            this.cboRecordType.Location = new System.Drawing.Point(15, 148);
            this.cboRecordType.Name = "cboRecordType";
            this.cboRecordType.Size = new System.Drawing.Size(202, 23);
            this.cboRecordType.TabIndex = 3;
            // 
            // sbrResult
            // 
            this.sbrResult.Location = new System.Drawing.Point(0, 271);
            this.sbrResult.Name = "sbrResult";
            this.sbrResult.Size = new System.Drawing.Size(238, 24);
            this.sbrResult.Text = "Ready";
            // 
            // txtDNS
            // 
            this.txtDNS.Location = new System.Drawing.Point(15, 101);
            this.txtDNS.Name = "txtDNS";
            this.txtDNS.Size = new System.Drawing.Size(202, 23);
            this.txtDNS.TabIndex = 5;
            this.txtDNS.Text = "218.248.240.208";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.Text = "DNS Server";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 18);
            this.label3.Text = "Query Type";
            // 
            // frmDnsCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDNS);
            this.Controls.Add(this.sbrResult);
            this.Controls.Add(this.cboRecordType);
            this.Controls.Add(this.btnLookUp);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label1);
            this.Name = "frmDnsCheck";
            this.Text = "Lookup DNS";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnLookUp;
        private System.Windows.Forms.ComboBox cboRecordType;
        private System.Windows.Forms.StatusBar sbrResult;
        private System.Windows.Forms.TextBox txtDNS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}