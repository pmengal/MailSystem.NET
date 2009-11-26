namespace ActiveUp.Net.Samples.Compact
{
    partial class frmNNTP
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
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtnews = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnRetreive = new System.Windows.Forms.Button();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 26);
            this.label1.Text = "Server";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(79, 10);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(156, 23);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "msnews.microsoft.com";
            // 
            // txtnews
            // 
            this.txtnews.Location = new System.Drawing.Point(79, 39);
            this.txtnews.Name = "txtnews";
            this.txtnews.Size = new System.Drawing.Size(156, 23);
            this.txtnews.TabIndex = 3;
            this.txtnews.Text = "microsoft.public.mobileexplorer";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 26);
            this.label2.Text = "Newsgroup";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.Text = "Headers";
            // 
            // listView1
            // 
            this.listView1.Columns.Add(this.columnHeader1);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.Location = new System.Drawing.Point(0, 110);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 185);
            this.listView1.TabIndex = 6;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // btnRetreive
            // 
            this.btnRetreive.Location = new System.Drawing.Point(79, 72);
            this.btnRetreive.Name = "btnRetreive";
            this.btnRetreive.Size = new System.Drawing.Size(86, 32);
            this.btnRetreive.TabIndex = 7;
            this.btnRetreive.Text = "Retrieve";
            this.btnRetreive.Click += new System.EventHandler(this.btnRetreive_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Subject";
            this.columnHeader1.Width = 225;
            // 
            // frmNNTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnRetreive);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtnews);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label1);
            this.Name = "frmNNTP";
            this.Text = "NNTp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtnews;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnRetreive;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}