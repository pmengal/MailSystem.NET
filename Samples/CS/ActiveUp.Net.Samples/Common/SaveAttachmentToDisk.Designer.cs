namespace ActiveUp.Net.Samples.Common
{
    partial class SaveAttachmentToDisk
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
            this._bSaveAttachment = new System.Windows.Forms.Button();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._tbUserName = new System.Windows.Forms.TextBox();
            this._bRemoveDirectory = new System.Windows.Forms.Button();
            this._bAddDirectory = new System.Windows.Forms.Button();
            this._tbAttachmentDirectory = new System.Windows.Forms.TextBox();
            this._lSaveDirectory = new System.Windows.Forms.Label();
            this._fbdSaveDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._bRemoveDirectory);
            this.splitContainer1.Panel1.Controls.Add(this._bAddDirectory);
            this.splitContainer1.Panel1.Controls.Add(this._tbAttachmentDirectory);
            this.splitContainer1.Panel1.Controls.Add(this._lSaveDirectory);
            this.splitContainer1.Panel1.Controls.Add(this._tbPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this._tbUserName);
            this.splitContainer1.Panel1.Controls.Add(this._lPop3Server);
            this.splitContainer1.Panel1.Controls.Add(this._tbPassword);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._bSaveAttachment);
            // 
            // _tbPop3Server
            // 
            this._tbPop3Server.Location = new System.Drawing.Point(15, 104);
            this._tbPop3Server.Name = "_tbPop3Server";
            this._tbPop3Server.Size = new System.Drawing.Size(270, 20);
            this._tbPop3Server.TabIndex = 23;
            // 
            // _lUserName
            // 
            this._lUserName.AutoSize = true;
            this._lUserName.Location = new System.Drawing.Point(12, 10);
            this._lUserName.Name = "_lUserName";
            this._lUserName.Size = new System.Drawing.Size(61, 13);
            this._lUserName.TabIndex = 18;
            this._lUserName.Text = "User name:";
            // 
            // _lPop3Server
            // 
            this._lPop3Server.AutoSize = true;
            this._lPop3Server.Location = new System.Drawing.Point(12, 88);
            this._lPop3Server.Name = "_lPop3Server";
            this._lPop3Server.Size = new System.Drawing.Size(244, 13);
            this._lPop3Server.TabIndex = 22;
            this._lPop3Server.Text = "POP3 server address (will use 110 as default port):";
            // 
            // _lPassword
            // 
            this._lPassword.AutoSize = true;
            this._lPassword.Location = new System.Drawing.Point(12, 49);
            this._lPassword.Name = "_lPassword";
            this._lPassword.Size = new System.Drawing.Size(56, 13);
            this._lPassword.TabIndex = 20;
            this._lPassword.Text = "Password:";
            // 
            // _bSaveAttachment
            // 
            this._bSaveAttachment.Location = new System.Drawing.Point(15, 198);
            this._bSaveAttachment.Name = "_bSaveAttachment";
            this._bSaveAttachment.Size = new System.Drawing.Size(270, 36);
            this._bSaveAttachment.TabIndex = 24;
            this._bSaveAttachment.Text = "Save attachment";
            this._bSaveAttachment.UseVisualStyleBackColor = true;
            this._bSaveAttachment.Click += new System.EventHandler(this._bSaveAttachment_Click);
            // 
            // _tbPassword
            // 
            this._tbPassword.Location = new System.Drawing.Point(15, 65);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(270, 20);
            this._tbPassword.TabIndex = 21;
            // 
            // _tbUserName
            // 
            this._tbUserName.Location = new System.Drawing.Point(15, 26);
            this._tbUserName.Name = "_tbUserName";
            this._tbUserName.Size = new System.Drawing.Size(270, 20);
            this._tbUserName.TabIndex = 19;
            // 
            // _bRemoveDirectory
            // 
            this._bRemoveDirectory.Enabled = false;
            this._bRemoveDirectory.Location = new System.Drawing.Point(210, 169);
            this._bRemoveDirectory.Name = "_bRemoveDirectory";
            this._bRemoveDirectory.Size = new System.Drawing.Size(75, 23);
            this._bRemoveDirectory.TabIndex = 44;
            this._bRemoveDirectory.Text = "Remove";
            this._bRemoveDirectory.UseVisualStyleBackColor = true;
            this._bRemoveDirectory.Click += new System.EventHandler(this._bRemoveDirectory_Click);
            // 
            // _bAddDirectory
            // 
            this._bAddDirectory.Location = new System.Drawing.Point(129, 169);
            this._bAddDirectory.Name = "_bAddDirectory";
            this._bAddDirectory.Size = new System.Drawing.Size(75, 23);
            this._bAddDirectory.TabIndex = 43;
            this._bAddDirectory.Text = "Add";
            this._bAddDirectory.UseVisualStyleBackColor = true;
            this._bAddDirectory.Click += new System.EventHandler(this._bAddDirectory_Click);
            // 
            // _tbAttachmentDirectory
            // 
            this._tbAttachmentDirectory.Location = new System.Drawing.Point(15, 143);
            this._tbAttachmentDirectory.Name = "_tbAttachmentDirectory";
            this._tbAttachmentDirectory.ReadOnly = true;
            this._tbAttachmentDirectory.Size = new System.Drawing.Size(270, 20);
            this._tbAttachmentDirectory.TabIndex = 42;
            this._tbAttachmentDirectory.TextChanged += new System.EventHandler(this._tbAttachmentDirectory_TextChanged);
            // 
            // _lSaveDirectory
            // 
            this._lSaveDirectory.AutoSize = true;
            this._lSaveDirectory.Location = new System.Drawing.Point(12, 127);
            this._lSaveDirectory.Name = "_lSaveDirectory";
            this._lSaveDirectory.Size = new System.Drawing.Size(87, 13);
            this._lSaveDirectory.TabIndex = 41;
            this._lSaveDirectory.Text = "Saved directory :";
            // 
            // _fbdSaveDirectory
            // 
            this._fbdSaveDirectory.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SaveAttachmentToDisk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "SaveAttachmentToDisk";
            this.Text = "Save attachment to disk";
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
        private System.Windows.Forms.Button _bSaveAttachment;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.TextBox _tbUserName;
        private System.Windows.Forms.Button _bRemoveDirectory;
        private System.Windows.Forms.Button _bAddDirectory;
        private System.Windows.Forms.TextBox _tbAttachmentDirectory;
        private System.Windows.Forms.Label _lSaveDirectory;
        private System.Windows.Forms.FolderBrowserDialog _fbdSaveDirectory;
    }
}
