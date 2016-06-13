namespace ActiveUp.Net.Samples.CompactSP
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
            this.pnlInterface = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.dtBirth = new System.Windows.Forms.DateTimePicker();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pnlInterface.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlInterface.Location = new System.Drawing.Point(0, 0);
            this.pnlInterface.Name = "pnlInterface";
            this.pnlInterface.Size = new System.Drawing.Size(240, 266);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(8, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.Text = "Birth Day";
            // 
            // dtBirth
            // 
            this.dtBirth.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Regular);
            this.dtBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtBirth.Location = new System.Drawing.Point(86, 167);
            this.dtBirth.Name = "dtBirth";
            this.dtBirth.Size = new System.Drawing.Size(96, 27);
            this.dtBirth.TabIndex = 8;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtEmail.Location = new System.Drawing.Point(9, 133);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(222, 28);
            this.txtEmail.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(9, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.Text = "Email";
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtLastName.Location = new System.Drawing.Point(9, 79);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(222, 28);
            this.txtLastName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.Text = "Nick Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.txtFirstName.Location = new System.Drawing.Point(9, 29);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(222, 28);
            this.txtFirstName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.Text = "Full Name";
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "vCard Files|*.vcf";
            this.dlgOpen.InitialDirectory = "\\Storage Card\\";
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "vCard Files|*.vcf";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Read Card";
            this.menuItem1.Click += new System.EventHandler(this.btnReadCard_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Write Card";
            this.menuItem2.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // frmvCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.pnlInterface);
            this.Menu = this.mainMenu1;
            this.Name = "frmvCard";
            this.Text = "Test vCard";
            this.pnlInterface.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInterface;
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
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}