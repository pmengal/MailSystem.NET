namespace ActiveUp.Net.Samples.NNTP
{
    partial class DisplayAllNewsGroup
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
            this._bRetrieveAllNewsgroup = new System.Windows.Forms.Button();
            this._tbNntpServer = new System.Windows.Forms.TextBox();
            this._lNntpServer = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbNntpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lNntpServer);
            this.splitContainer1.Panel1.Controls.Add(this._bRetrieveAllNewsgroup);
            this.splitContainer1.TabIndex = 1;
            // 
            // _bRetrieveAllNewsgroup
            // 
            this._bRetrieveAllNewsgroup.Location = new System.Drawing.Point(12, 93);
            this._bRetrieveAllNewsgroup.Name = "_bRetrieveAllNewsgroup";
            this._bRetrieveAllNewsgroup.Size = new System.Drawing.Size(270, 36);
            this._bRetrieveAllNewsgroup.TabIndex = 2;
            this._bRetrieveAllNewsgroup.Text = "Retrieve all newsgroup";
            this._bRetrieveAllNewsgroup.UseVisualStyleBackColor = true;
            this._bRetrieveAllNewsgroup.Click += new System.EventHandler(this._bRetrieveAllNewsgroup_Click);
            // 
            // _tbNntpServer
            // 
            this._tbNntpServer.Location = new System.Drawing.Point(12, 29);
            this._tbNntpServer.Name = "_tbNntpServer";
            this._tbNntpServer.Size = new System.Drawing.Size(270, 20);
            this._tbNntpServer.TabIndex = 1;
            // 
            // _lNntpServer
            // 
            this._lNntpServer.AutoSize = true;
            this._lNntpServer.Location = new System.Drawing.Point(9, 13);
            this._lNntpServer.Name = "_lNntpServer";
            this._lNntpServer.Size = new System.Drawing.Size(246, 13);
            this._lNntpServer.TabIndex = 0;
            this._lNntpServer.Text = "NNTP server address (will use 119 as default port):";
            // 
            // DisplayAllNewsGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "DisplayAllNewsGroup";
            this.Text = "Display all newsgroup on a server";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _bRetrieveAllNewsgroup;
        private System.Windows.Forms.TextBox _tbNntpServer;
        private System.Windows.Forms.Label _lNntpServer;
    }
}
