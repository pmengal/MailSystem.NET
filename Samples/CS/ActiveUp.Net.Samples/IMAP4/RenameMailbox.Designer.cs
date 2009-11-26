namespace ActiveUp.Net.Samples.IMAP4
{
    partial class RenameMailbox
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
            this._tbOldMailbox = new System.Windows.Forms.TextBox();
            this._lUserName = new System.Windows.Forms.Label();
            this._lNewMailbox = new System.Windows.Forms.Label();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._tbImap4Server = new System.Windows.Forms.TextBox();
            this._lImap4Server = new System.Windows.Forms.Label();
            this._lPassword = new System.Windows.Forms.Label();
            this._bRenameMailbox = new System.Windows.Forms.Button();
            this._tbUserName = new System.Windows.Forms.TextBox();
            this._tbNewMailbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbNewMailbox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this._tbOldMailbox);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this._lNewMailbox);
            this.splitContainer1.Panel1.Controls.Add(this._tbPassword);
            this.splitContainer1.Panel1.Controls.Add(this._tbImap4Server);
            this.splitContainer1.Panel1.Controls.Add(this._lImap4Server);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._bRenameMailbox);
            this.splitContainer1.Panel1.Controls.Add(this._tbUserName);
            // 
            // _tbOldMailbox
            // 
            this._tbOldMailbox.Location = new System.Drawing.Point(15, 143);
            this._tbOldMailbox.Name = "_tbOldMailbox";
            this._tbOldMailbox.Size = new System.Drawing.Size(270, 20);
            this._tbOldMailbox.TabIndex = 16;
            // 
            // _lUserName
            // 
            this._lUserName.AutoSize = true;
            this._lUserName.Location = new System.Drawing.Point(12, 10);
            this._lUserName.Name = "_lUserName";
            this._lUserName.Size = new System.Drawing.Size(61, 13);
            this._lUserName.TabIndex = 9;
            this._lUserName.Text = "User name:";
            // 
            // _lNewMailbox
            // 
            this._lNewMailbox.AutoSize = true;
            this._lNewMailbox.Location = new System.Drawing.Point(12, 127);
            this._lNewMailbox.Name = "_lNewMailbox";
            this._lNewMailbox.Size = new System.Drawing.Size(93, 13);
            this._lNewMailbox.TabIndex = 15;
            this._lNewMailbox.Text = "Old mailbox name:";
            // 
            // _tbPassword
            // 
            this._tbPassword.Location = new System.Drawing.Point(15, 65);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(270, 20);
            this._tbPassword.TabIndex = 12;
            // 
            // _tbImap4Server
            // 
            this._tbImap4Server.Location = new System.Drawing.Point(15, 104);
            this._tbImap4Server.Name = "_tbImap4Server";
            this._tbImap4Server.Size = new System.Drawing.Size(270, 20);
            this._tbImap4Server.TabIndex = 14;
            // 
            // _lImap4Server
            // 
            this._lImap4Server.AutoSize = true;
            this._lImap4Server.Location = new System.Drawing.Point(12, 88);
            this._lImap4Server.Name = "_lImap4Server";
            this._lImap4Server.Size = new System.Drawing.Size(248, 13);
            this._lImap4Server.TabIndex = 13;
            this._lImap4Server.Text = "IMAP4 server address (will use 143 as default port):";
            // 
            // _lPassword
            // 
            this._lPassword.AutoSize = true;
            this._lPassword.Location = new System.Drawing.Point(12, 49);
            this._lPassword.Name = "_lPassword";
            this._lPassword.Size = new System.Drawing.Size(56, 13);
            this._lPassword.TabIndex = 11;
            this._lPassword.Text = "Password:";
            // 
            // _bRenameMailbox
            // 
            this._bRenameMailbox.Location = new System.Drawing.Point(15, 244);
            this._bRenameMailbox.Name = "_bRenameMailbox";
            this._bRenameMailbox.Size = new System.Drawing.Size(270, 36);
            this._bRenameMailbox.TabIndex = 17;
            this._bRenameMailbox.Text = "Rename mailbox";
            this._bRenameMailbox.UseVisualStyleBackColor = true;
            this._bRenameMailbox.Click += new System.EventHandler(this._bRenameMailbox_Click);
            // 
            // _tbUserName
            // 
            this._tbUserName.Location = new System.Drawing.Point(15, 26);
            this._tbUserName.Name = "_tbUserName";
            this._tbUserName.Size = new System.Drawing.Size(270, 20);
            this._tbUserName.TabIndex = 10;
            // 
            // _tbNewMailbox
            // 
            this._tbNewMailbox.Location = new System.Drawing.Point(15, 182);
            this._tbNewMailbox.Name = "_tbNewMailbox";
            this._tbNewMailbox.Size = new System.Drawing.Size(270, 20);
            this._tbNewMailbox.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "New mailbox name:";
            // 
            // RenameMailbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "RenameMailbox";
            this.Text = "Rename a mailbox";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbOldMailbox;
        private System.Windows.Forms.Label _lUserName;
        private System.Windows.Forms.Label _lNewMailbox;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.TextBox _tbImap4Server;
        private System.Windows.Forms.Label _lImap4Server;
        private System.Windows.Forms.Label _lPassword;
        private System.Windows.Forms.Button _bRenameMailbox;
        private System.Windows.Forms.TextBox _tbUserName;
        private System.Windows.Forms.TextBox _tbNewMailbox;
        private System.Windows.Forms.Label label1;
    }
}
