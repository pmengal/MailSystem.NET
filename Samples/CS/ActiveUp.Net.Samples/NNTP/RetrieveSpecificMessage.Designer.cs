namespace ActiveUp.Net.Samples.NNTP
{
    partial class RetrieveSpecificMessage
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
            this._tbNewsgroup = new System.Windows.Forms.TextBox();
            this._lNewsgroup = new System.Windows.Forms.Label();
            this._tbNntpServer = new System.Windows.Forms.TextBox();
            this._lNntpServer = new System.Windows.Forms.Label();
            this._bRetrieveSpecificMessage = new System.Windows.Forms.Button();
            this._lvMessages = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._lvMessages);
            this.splitContainer1.Panel1.Controls.Add(this._tbNewsgroup);
            this.splitContainer1.Panel1.Controls.Add(this._lNewsgroup);
            this.splitContainer1.Panel1.Controls.Add(this._bRetrieveSpecificMessage);
            this.splitContainer1.Panel1.Controls.Add(this._tbNntpServer);
            this.splitContainer1.Panel1.Controls.Add(this._lNntpServer);
            this.splitContainer1.TabIndex = 1;
            // 
            // _tbNewsgroup
            // 
            this._tbNewsgroup.Location = new System.Drawing.Point(15, 27);
            this._tbNewsgroup.Name = "_tbNewsgroup";
            this._tbNewsgroup.Size = new System.Drawing.Size(270, 20);
            this._tbNewsgroup.TabIndex = 1;
            // 
            // _lNewsgroup
            // 
            this._lNewsgroup.AutoSize = true;
            this._lNewsgroup.Location = new System.Drawing.Point(12, 11);
            this._lNewsgroup.Name = "_lNewsgroup";
            this._lNewsgroup.Size = new System.Drawing.Size(64, 13);
            this._lNewsgroup.TabIndex = 0;
            this._lNewsgroup.Text = "Newsgroup:";
            // 
            // _tbNntpServer
            // 
            this._tbNntpServer.Location = new System.Drawing.Point(15, 68);
            this._tbNntpServer.Name = "_tbNntpServer";
            this._tbNntpServer.Size = new System.Drawing.Size(270, 20);
            this._tbNntpServer.TabIndex = 3;
            // 
            // _lNntpServer
            // 
            this._lNntpServer.AutoSize = true;
            this._lNntpServer.Location = new System.Drawing.Point(12, 52);
            this._lNntpServer.Name = "_lNntpServer";
            this._lNntpServer.Size = new System.Drawing.Size(246, 13);
            this._lNntpServer.TabIndex = 2;
            this._lNntpServer.Text = "NNTP server address (will use 119 as default port):";
            // 
            // _bRetrieveSpecificMessage
            // 
            this._bRetrieveSpecificMessage.Location = new System.Drawing.Point(15, 94);
            this._bRetrieveSpecificMessage.Name = "_bRetrieveSpecificMessage";
            this._bRetrieveSpecificMessage.Size = new System.Drawing.Size(270, 36);
            this._bRetrieveSpecificMessage.TabIndex = 4;
            this._bRetrieveSpecificMessage.Text = "Retrieve specific message";
            this._bRetrieveSpecificMessage.UseVisualStyleBackColor = true;
            this._bRetrieveSpecificMessage.Click += new System.EventHandler(this._bRetrieveSpecificMessage_Click);
            // 
            // _lvMessages
            // 
            this._lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this._lvMessages.FullRowSelect = true;
            this._lvMessages.GridLines = true;
            this._lvMessages.HideSelection = false;
            this._lvMessages.Location = new System.Drawing.Point(16, 136);
            this._lvMessages.MultiSelect = false;
            this._lvMessages.Name = "_lvMessages";
            this._lvMessages.Size = new System.Drawing.Size(269, 108);
            this._lvMessages.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._lvMessages.TabIndex = 9;
            this._lvMessages.UseCompatibleStateImageBehavior = false;
            this._lvMessages.View = System.Windows.Forms.View.Details;
            this._lvMessages.DoubleClick += new System.EventHandler(this._lvMessages_DoubleClick);
            this._lvMessages.KeyDown += new System.Windows.Forms.KeyEventHandler(this._lvMessages_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 34;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Subject";
            this.columnHeader2.Width = 226;
            // 
            // RetrieveSpecificMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "RetrieveSpecificMessage";
            this.Text = "Retrieve a specific message";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox _tbNewsgroup;
        private System.Windows.Forms.Label _lNewsgroup;
        private System.Windows.Forms.TextBox _tbNntpServer;
        private System.Windows.Forms.Label _lNntpServer;
        private System.Windows.Forms.Button _bRetrieveSpecificMessage;
        private System.Windows.Forms.ListView _lvMessages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
