namespace ActiveUp.Net.Samples.POP3
{
    partial class RetrieveToFile
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
            this._bRetriveToFile = new System.Windows.Forms.Button();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._tbUserName = new System.Windows.Forms.TextBox();
            this._bRemoveMessageFilename = new System.Windows.Forms.Button();
            this._bAddMessageFilename = new System.Windows.Forms.Button();
            this._tbMessageFilename = new System.Windows.Forms.TextBox();
            this._lSavedMessage = new System.Windows.Forms.Label();
            this._sfdMessageFilename = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._bRemoveMessageFilename);
            this.splitContainer1.Panel1.Controls.Add(this._bAddMessageFilename);
            this.splitContainer1.Panel1.Controls.Add(this._tbMessageFilename);
            this.splitContainer1.Panel1.Controls.Add(this._lSavedMessage);
            this.splitContainer1.Panel1.Controls.Add(this._tbPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this._tbUserName);
            this.splitContainer1.Panel1.Controls.Add(this._lPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._tbPassword);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._bRetriveToFile);
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
            // _bRetriveToFile
            // 
            this._bRetriveToFile.Location = new System.Drawing.Point(15, 262);
            this._bRetriveToFile.Name = "_bRetriveToFile";
            this._bRetriveToFile.Size = new System.Drawing.Size(270, 36);
            this._bRetriveToFile.TabIndex = 10;
            this._bRetriveToFile.Text = "Retrive to file";
            this._bRetriveToFile.UseVisualStyleBackColor = true;
            this._bRetriveToFile.Click += new System.EventHandler(this._bRetriveToFile_Click);
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
            // _bRemoveMessageFilename
            // 
            this._bRemoveMessageFilename.Enabled = false;
            this._bRemoveMessageFilename.Location = new System.Drawing.Point(210, 169);
            this._bRemoveMessageFilename.Name = "_bRemoveMessageFilename";
            this._bRemoveMessageFilename.Size = new System.Drawing.Size(75, 23);
            this._bRemoveMessageFilename.TabIndex = 9;
            this._bRemoveMessageFilename.Text = "Remove";
            this._bRemoveMessageFilename.UseVisualStyleBackColor = true;
            this._bRemoveMessageFilename.Click += new System.EventHandler(this._bRemoveMessageFilename_Click);
            // 
            // _bAddMessageFilename
            // 
            this._bAddMessageFilename.Location = new System.Drawing.Point(129, 169);
            this._bAddMessageFilename.Name = "_bAddMessageFilename";
            this._bAddMessageFilename.Size = new System.Drawing.Size(75, 23);
            this._bAddMessageFilename.TabIndex = 8;
            this._bAddMessageFilename.Text = "Add";
            this._bAddMessageFilename.UseVisualStyleBackColor = true;
            this._bAddMessageFilename.Click += new System.EventHandler(this._bAddMessageFilename_Click);
            // 
            // _tbMessageFilename
            // 
            this._tbMessageFilename.Location = new System.Drawing.Point(15, 143);
            this._tbMessageFilename.Name = "_tbMessageFilename";
            this._tbMessageFilename.ReadOnly = true;
            this._tbMessageFilename.Size = new System.Drawing.Size(270, 20);
            this._tbMessageFilename.TabIndex = 7;
            this._tbMessageFilename.TextChanged += new System.EventHandler(this._tbMessageFilename_TextChanged);
            // 
            // _lSavedMessage
            // 
            this._lSavedMessage.AutoSize = true;
            this._lSavedMessage.Location = new System.Drawing.Point(12, 127);
            this._lSavedMessage.Name = "_lSavedMessage";
            this._lSavedMessage.Size = new System.Drawing.Size(131, 13);
            this._lSavedMessage.TabIndex = 6;
            this._lSavedMessage.Text = "Saved message filename :";
            // 
            // _sfdMessageFilename
            // 
            this._sfdMessageFilename.Filter = "Message file|*.eml";
            // 
            // RetrieveToFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "RetrieveToFile";
            this.Text = "Retrieve message to file";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbPop3Server;
        private System.Windows.Forms.Label _lUserName;
        private System.Windows.Forms.Label _lPop3Server;
        private System.Windows.Forms.Label _lPassword;
        private System.Windows.Forms.Button _bRetriveToFile;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.TextBox _tbUserName;
        private System.Windows.Forms.Button _bRemoveMessageFilename;
        private System.Windows.Forms.Button _bAddMessageFilename;
        private System.Windows.Forms.TextBox _tbMessageFilename;
        private System.Windows.Forms.Label _lSavedMessage;
        private System.Windows.Forms.SaveFileDialog _sfdMessageFilename;
    }
}
