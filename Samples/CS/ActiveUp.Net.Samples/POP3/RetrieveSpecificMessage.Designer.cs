namespace ActiveUp.Net.Samples.POP3
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
            this._tbPop3Server = new System.Windows.Forms.TextBox();
            this._lUserName = new System.Windows.Forms.Label();
            this._lPop3Server = new System.Windows.Forms.Label();
            this._lPassword = new System.Windows.Forms.Label();
            this._bRetrieveSpecificMessage = new System.Windows.Forms.Button();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._tbUserName = new System.Windows.Forms.TextBox();
            this._lvMessages = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this._bStoreToFile = new System.Windows.Forms.Button();
            this._sfdStoreMessage = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._bStoreToFile);
            this.splitContainer1.Panel1.Controls.Add(this._lvMessages);
            this.splitContainer1.Panel1.Controls.Add(this._tbPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this._tbUserName);
            this.splitContainer1.Panel1.Controls.Add(this._lPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._tbPassword);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._bRetrieveSpecificMessage);
            this.splitContainer1.TabIndex = 1;
            // 
            // _tbPop3Server
            // 
            this._tbPop3Server.Location = new System.Drawing.Point(15, 104);
            this._tbPop3Server.Name = "_tbPop3Server";
            this._tbPop3Server.Size = new System.Drawing.Size(270, 20);
            this._tbPop3Server.TabIndex = 5;
            // 
            // _lUserName
            // 
            this._lUserName.AutoSize = true;
            this._lUserName.Location = new System.Drawing.Point(12, 10);
            this._lUserName.Name = "_lUserName";
            this._lUserName.Size = new System.Drawing.Size(61, 13);
            this._lUserName.TabIndex = 0;
            this._lUserName.Text = "User name:";
            // 
            // _lPop3Server
            // 
            this._lPop3Server.AutoSize = true;
            this._lPop3Server.Location = new System.Drawing.Point(12, 88);
            this._lPop3Server.Name = "_lPop3Server";
            this._lPop3Server.Size = new System.Drawing.Size(244, 13);
            this._lPop3Server.TabIndex = 4;
            this._lPop3Server.Text = "POP3 server address (will use 110 as default port):";
            // 
            // _lPassword
            // 
            this._lPassword.AutoSize = true;
            this._lPassword.Location = new System.Drawing.Point(12, 49);
            this._lPassword.Name = "_lPassword";
            this._lPassword.Size = new System.Drawing.Size(56, 13);
            this._lPassword.TabIndex = 2;
            this._lPassword.Text = "Password:";
            // 
            // _bRetrieveSpecificMessage
            // 
            this._bRetrieveSpecificMessage.Location = new System.Drawing.Point(15, 130);
            this._bRetrieveSpecificMessage.Name = "_bRetrieveSpecificMessage";
            this._bRetrieveSpecificMessage.Size = new System.Drawing.Size(270, 36);
            this._bRetrieveSpecificMessage.TabIndex = 6;
            this._bRetrieveSpecificMessage.Text = "Retrieve messages";
            this._bRetrieveSpecificMessage.UseVisualStyleBackColor = true;
            this._bRetrieveSpecificMessage.Click += new System.EventHandler(this._bRetrieveSpecificMessage_Click);
            // 
            // _tbPassword
            // 
            this._tbPassword.Location = new System.Drawing.Point(15, 65);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(270, 20);
            this._tbPassword.TabIndex = 3;
            // 
            // _tbUserName
            // 
            this._tbUserName.Location = new System.Drawing.Point(15, 26);
            this._tbUserName.Name = "_tbUserName";
            this._tbUserName.Size = new System.Drawing.Size(270, 20);
            this._tbUserName.TabIndex = 1;
            // 
            // _lvMessages
            // 
            this._lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this._lvMessages.FullRowSelect = true;
            this._lvMessages.GridLines = true;
            this._lvMessages.HideSelection = false;
            this._lvMessages.Location = new System.Drawing.Point(16, 172);
            this._lvMessages.MultiSelect = false;
            this._lvMessages.Name = "_lvMessages";
            this._lvMessages.Size = new System.Drawing.Size(269, 108);
            this._lvMessages.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._lvMessages.TabIndex = 7;
            this._lvMessages.UseCompatibleStateImageBehavior = false;
            this._lvMessages.View = System.Windows.Forms.View.Details;
            this._lvMessages.DoubleClick += new System.EventHandler(this._lvMessages_DoubleClick);
            this._lvMessages.SelectedIndexChanged += new System.EventHandler(this._lvMessages_SelectedIndexChanged);
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
            // _bStoreToFile
            // 
            this._bStoreToFile.Enabled = false;
            this._bStoreToFile.Location = new System.Drawing.Point(210, 286);
            this._bStoreToFile.Name = "_bStoreToFile";
            this._bStoreToFile.Size = new System.Drawing.Size(75, 23);
            this._bStoreToFile.TabIndex = 8;
            this._bStoreToFile.Text = "Store to file";
            this._bStoreToFile.UseVisualStyleBackColor = true;
            this._bStoreToFile.Click += new System.EventHandler(this._bStoreToFile_Click);
            // 
            // _sfdStoreMessage
            // 
            this._sfdStoreMessage.Filter = "Message files|*.eml";
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

        private System.Windows.Forms.TextBox _tbPop3Server;
        private System.Windows.Forms.Label _lUserName;
        private System.Windows.Forms.Label _lPop3Server;
        private System.Windows.Forms.Label _lPassword;
        private System.Windows.Forms.Button _bRetrieveSpecificMessage;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.TextBox _tbUserName;
        private System.Windows.Forms.ListView _lvMessages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button _bStoreToFile;
        private System.Windows.Forms.SaveFileDialog _sfdStoreMessage;
    }
}
