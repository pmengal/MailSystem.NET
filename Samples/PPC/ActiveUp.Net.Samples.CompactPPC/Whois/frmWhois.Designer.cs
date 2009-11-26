namespace ActiveUp.Net.Samples.CompactPPC
{
    partial class frmWhois
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
            this.txtDomainName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtresult = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 26);
            this.label1.Text = "Domain Name";
            // 
            // txtDomainName
            // 
            this.txtDomainName.Location = new System.Drawing.Point(3, 34);
            this.txtDomainName.Name = "txtDomainName";
            this.txtDomainName.Size = new System.Drawing.Size(234, 21);
            this.txtDomainName.TabIndex = 1;
            this.txtDomainName.Text = "networksolutions.com";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Fetch details";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtresult
            // 
            this.txtresult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtresult.Location = new System.Drawing.Point(0, 141);
            this.txtresult.Multiline = true;
            this.txtresult.Name = "txtresult";
            this.txtresult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtresult.Size = new System.Drawing.Size(240, 127);
            this.txtresult.TabIndex = 5;
            this.txtresult.Text = "Result";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(234, 21);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "whois.networksolutions.com";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 18);
            this.label2.Text = "Server";
            // 
            // frmWhois
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtresult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDomainName);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.Name = "frmWhois";
            this.Text = "WhoIS Query";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDomainName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtresult;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}