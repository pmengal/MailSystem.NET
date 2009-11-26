namespace ActiveUp.Net.Samples.CompactSP.Mail.POP3
{
    partial class Pop3Result
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
            this.lvwMails = new System.Windows.Forms.ListView();
            this.colDate = new System.Windows.Forms.ColumnHeader();
            this.colSubject = new System.Windows.Forms.ColumnHeader();
            this.colIP = new System.Windows.Forms.ColumnHeader();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // lvwMails
            // 
            this.lvwMails.Columns.Add(this.colDate);
            this.lvwMails.Columns.Add(this.colSubject);
            this.lvwMails.Columns.Add(this.colIP);
            this.lvwMails.Columns.Add(this.colName);
            this.lvwMails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwMails.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.lvwMails.FullRowSelect = true;
            this.lvwMails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwMails.Location = new System.Drawing.Point(0, 0);
            this.lvwMails.Name = "lvwMails";
            this.lvwMails.Size = new System.Drawing.Size(240, 266);
            this.lvwMails.TabIndex = 0;
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
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = " ";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Done";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // Pop3Result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.lvwMails);
            this.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.Menu = this.mainMenu1;
            this.Name = "Pop3Result";
            this.Text = "Pop3 Mails";
            this.Load += new System.EventHandler(this.Pop3Result_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwMails;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colSubject;
        private System.Windows.Forms.ColumnHeader colIP;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}