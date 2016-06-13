namespace ActiveUp.Net.Samples.Compact
{
    partial class frmvCard
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
            this.btnReadCard = new System.Windows.Forms.Button();
            this.pnlInterface = new System.Windows.Forms.Panel();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtBirth = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.pnlInterface.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadCard
            // 
            this.btnReadCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReadCard.Location = new System.Drawing.Point(0, 0);
            this.btnReadCard.Name = "btnReadCard";
            this.btnReadCard.Size = new System.Drawing.Size(238, 33);
            this.btnReadCard.TabIndex = 0;
            this.btnReadCard.Text = "Read Card";
            this.btnReadCard.Click += new System.EventHandler(this.btnReadCard_Click);
            // 
            // pnlInterface
            // 
            this.pnlInterface.Controls.Add(this.label4);
            this.pnlInterface.Controls.Add(this.dtBirth);
            this.pnlInterface.Controls.Add(this.txtEmail);
            this.pnlInterface.Controls.Add(this.label3);
            this.pnlInterface.Controls.Add(this.txtLastName);
            this.pnlInterface.Controls.Add(this.label2);
            this.pnlInterface.Controls.Add(this.txtFirstName);
            this.pnlInterface.Controls.Add(this.label1);
            this.pnlInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInterface.Location = new System.Drawing.Point(0, 33);
            this.pnlInterface.Name = "pnlInterface";
            this.pnlInterface.Size = new System.Drawing.Size(238, 262);
            // 
            // btnWrite
            // 
            this.btnWrite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnWrite.Location = new System.Drawing.Point(0, 262);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(238, 33);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "Write Card";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.Text = "Full Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(82, 17);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(156, 23);
            this.txtFirstName.TabIndex = 1;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(82, 46);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(156, 23);
            this.txtLastName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.Text = "Nick Name";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(82, 75);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(156, 23);
            this.txtEmail.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.Text = "Email";
            // 
            // dtBirth
            // 
            this.dtBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtBirth.Location = new System.Drawing.Point(82, 105);
            this.dtBirth.Name = "dtBirth";
            this.dtBirth.ShowUpDown = true;
            this.dtBirth.Size = new System.Drawing.Size(96, 24);
            this.dtBirth.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.Text = "Birth Day";
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "vCard Files|*.vcf";
            this.dlgOpen.InitialDirectory = "\\Storage Card\\";
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "vCard Files|*.vcf";
            this.dlgSave.InitialDirectory = "\\Storage Card\\";
            // 
            // frmvCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.pnlInterface);
            this.Controls.Add(this.btnReadCard);
            this.Name = "frmvCard";
            this.Text = "Test vCard";
            this.pnlInterface.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadCard;
        private System.Windows.Forms.Panel pnlInterface;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtBirth;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
    }
}