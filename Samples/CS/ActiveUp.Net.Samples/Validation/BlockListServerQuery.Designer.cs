namespace ActiveUp.Net.Samples.Validation
{
    partial class BlockListServerQuery
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
            this.queryServerButton = new System.Windows.Forms.Button();
            this.serverIpTextbox = new System.Windows.Forms.TextBox();
            this.rblServersListbox = new System.Windows.Forms.ListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rblServersListbox);
            this.splitContainer1.Panel1.Controls.Add(this.serverIpTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.queryServerButton);
            // 
            // queryServerButton
            // 
            this.queryServerButton.Location = new System.Drawing.Point(27, 184);
            this.queryServerButton.Name = "queryServerButton";
            this.queryServerButton.Size = new System.Drawing.Size(75, 23);
            this.queryServerButton.TabIndex = 0;
            this.queryServerButton.Text = "Query server";
            this.queryServerButton.UseVisualStyleBackColor = true;
            this.queryServerButton.Click += new System.EventHandler(this.queryServerButton_Click);
            // 
            // serverIpTextbox
            // 
            this.serverIpTextbox.Location = new System.Drawing.Point(27, 60);
            this.serverIpTextbox.Name = "serverIpTextbox";
            this.serverIpTextbox.Size = new System.Drawing.Size(168, 20);
            this.serverIpTextbox.TabIndex = 1;
            // 
            // rblServersListbox
            // 
            this.rblServersListbox.FormattingEnabled = true;
            this.rblServersListbox.Items.AddRange(new object[] {
            "sbl.spamhaus.org",
            "spammers.v6net.org",
            "vox.schpider.com",
            "whois.rfc-ignorant.org"});
            this.rblServersListbox.Location = new System.Drawing.Point(27, 86);
            this.rblServersListbox.Name = "rblServersListbox";
            this.rblServersListbox.Size = new System.Drawing.Size(120, 95);
            this.rblServersListbox.TabIndex = 2;
            // 
            // BlockListServerQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "BlockListServerQuery";
            this.Text = "BlockListServerQuery";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button queryServerButton;
        private System.Windows.Forms.TextBox serverIpTextbox;
        private System.Windows.Forms.ListBox rblServersListbox;
    }
}