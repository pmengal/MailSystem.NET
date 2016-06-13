namespace ActiveUp.Net.Samples.CompactPPC.Mail.IMAP4
{
    partial class Imap4Result
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lvwMails = new System.Windows.Forms.ListView();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colSubject = new System.Windows.Forms.ColumnHeader();
            this.colIP = new System.Windows.Forms.ColumnHeader();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvwMails
            // 
            this.lvwMails.Columns.Add(this.colDate);
            this.lvwMails.Columns.Add(this.colSubject);
            this.lvwMails.Columns.Add(this.colIP);
            this.lvwMails.Columns.Add(this.colName);
            this.lvwMails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwMails.FullRowSelect = true;
            this.lvwMails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwMails.Location = new System.Drawing.Point(0, 21);
            this.lvwMails.Name = "lvwMails";
            this.lvwMails.Size = new System.Drawing.Size(240, 247);
            this.lvwMails.TabIndex = 1;
            this.lvwMails.View = System.Windows.Forms.View.Details;
            this.lvwMails.ItemActivate += new System.EventHandler(this.lvwMails_ItemActivate);
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            this.colDate.Width = 66;
            // 
            // colSubject
            // 
            this.colSubject.Text = "Subject";
            this.colSubject.Width = 180;
            // 
            // colIP
            // 
            this.colIP.Text = "IP";
            this.colIP.Width = 90;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 100;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 21);
            this.label1.Text = "Click an item to view details";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Imap4Result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvwMails);
            this.Menu = this.mainMenu1;
            this.Name = "Imap4Result";
            this.Text = "Imap4Result";
            this.Load += new System.EventHandler(this.Imap4Result_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwMails;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colSubject;
        private System.Windows.Forms.ColumnHeader colIP;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.Label label1;
    }
}