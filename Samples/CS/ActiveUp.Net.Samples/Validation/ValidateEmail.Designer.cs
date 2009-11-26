namespace ActiveUp.Net.Samples.Validation
{
    partial class ValidateEmail
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
            this._tbEmail = new System.Windows.Forms.TextBox();
            this._lEmail = new System.Windows.Forms.Label();
            this._bValidateEmail = new System.Windows.Forms.Button();
            this._cbUseDnsToValidateEmail = new System.Windows.Forms.CheckBox();
            this._tbDnsServer = new System.Windows.Forms.TextBox();
            this._dnsServerLabel = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._cbUseDnsToValidateEmail);
            this.splitContainer1.Panel1.Controls.Add(this._tbDnsServer);
            this.splitContainer1.Panel1.Controls.Add(this._dnsServerLabel);
            this.splitContainer1.Panel1.Controls.Add(this._tbEmail);
            this.splitContainer1.Panel1.Controls.Add(this._lEmail);
            this.splitContainer1.Panel1.Controls.Add(this._bValidateEmail);
            // 
            // _tbEmail
            // 
            this._tbEmail.Location = new System.Drawing.Point(12, 29);
            this._tbEmail.Name = "_tbEmail";
            this._tbEmail.Size = new System.Drawing.Size(270, 20);
            this._tbEmail.TabIndex = 39;
            // 
            // _lEmail
            // 
            this._lEmail.AutoSize = true;
            this._lEmail.Location = new System.Drawing.Point(9, 13);
            this._lEmail.Name = "_lEmail";
            this._lEmail.Size = new System.Drawing.Size(130, 13);
            this._lEmail.TabIndex = 38;
            this._lEmail.Text = "Email address to validate :";
            // 
            // _bValidateEmail
            // 
            this._bValidateEmail.Location = new System.Drawing.Point(12, 117);
            this._bValidateEmail.Name = "_bValidateEmail";
            this._bValidateEmail.Size = new System.Drawing.Size(270, 36);
            this._bValidateEmail.TabIndex = 37;
            this._bValidateEmail.Text = "Validate";
            this._bValidateEmail.UseVisualStyleBackColor = true;
            this._bValidateEmail.Click += new System.EventHandler(this._bValidateEmail_Click);
            // 
            // _cbUseDnsToValidateEmail
            // 
            this._cbUseDnsToValidateEmail.AutoSize = true;
            this._cbUseDnsToValidateEmail.Location = new System.Drawing.Point(12, 55);
            this._cbUseDnsToValidateEmail.Name = "_cbUseDnsToValidateEmail";
            this._cbUseDnsToValidateEmail.Size = new System.Drawing.Size(212, 17);
            this._cbUseDnsToValidateEmail.TabIndex = 40;
            this._cbUseDnsToValidateEmail.Text = "Use the dns server to validate the email";
            this._cbUseDnsToValidateEmail.UseVisualStyleBackColor = true;
            // 
            // _tbDnsServer
            // 
            this._tbDnsServer.Location = new System.Drawing.Point(12, 91);
            this._tbDnsServer.Name = "_tbDnsServer";
            this._tbDnsServer.Size = new System.Drawing.Size(273, 20);
            this._tbDnsServer.TabIndex = 42;
            // 
            // _dnsServerLabel
            // 
            this._dnsServerLabel.AutoSize = true;
            this._dnsServerLabel.Location = new System.Drawing.Point(12, 75);
            this._dnsServerLabel.Name = "_dnsServerLabel";
            this._dnsServerLabel.Size = new System.Drawing.Size(127, 13);
            this._dnsServerLabel.TabIndex = 41;
            this._dnsServerLabel.Text = "Dsn server (MX records) :";
            // 
            // ValidateEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "ValidateEmail";
            this.Text = "Validate an email";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _tbEmail;
        private System.Windows.Forms.Label _lEmail;
        private System.Windows.Forms.Button _bValidateEmail;
        private System.Windows.Forms.CheckBox _cbUseDnsToValidateEmail;
        private System.Windows.Forms.TextBox _tbDnsServer;
        private System.Windows.Forms.Label _dnsServerLabel;
    }
}
