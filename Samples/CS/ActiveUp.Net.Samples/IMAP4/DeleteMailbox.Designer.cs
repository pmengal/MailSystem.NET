namespace ActiveUp.Net.Samples.IMAP4
{
    partial class DeleteMailbox
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
            this._tbMailboxToDelete = new System.Windows.Forms.TextBox();
            this._lUserName = new System.Windows.Forms.Label();
            this._lMailboxToDelete = new System.Windows.Forms.Label();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._tbImap4Server = new System.Windows.Forms.TextBox();
            this._lImap4Server = new System.Windows.Forms.Label();
            this._lPassword = new System.Windows.Forms.Label();
            this._bDeleteMailbox = new System.Windows.Forms.Button();
            this._tbUserName = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbMailboxToDelete);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this._tbUserName);
            this.splitContainer1.Panel1.Controls.Add(this._lMailboxToDelete);
            this.splitContainer1.Panel1.Controls.Add(this._bDeleteMailbox);
            this.splitContainer1.Panel1.Controls.Add(this._tbPassword);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._tbImap4Server);
            this.splitContainer1.Panel1.Controls.Add(this._lImap4Server);
            this.splitContainer1.TabIndex = 1;
            // 
            // _tbMailboxToDelete
            // 
            this._tbMailboxToDelete.Location = new System.Drawing.Point(15, 143);
            this._tbMailboxToDelete.Name = "_tbMailboxToDelete";
            this._tbMailboxToDelete.Size = new System.Drawing.Size(270, 20);
            this._tbMailboxToDelete.TabIndex = 7;
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
            // _lMailboxToDelete
            // 
            this._lMailboxToDelete.AutoSize = true;
            this._lMailboxToDelete.Location = new System.Drawing.Point(12, 127);
            this._lMailboxToDelete.Name = "_lMailboxToDelete";
            this._lMailboxToDelete.Size = new System.Drawing.Size(90, 13);
            this._lMailboxToDelete.TabIndex = 6;
            this._lMailboxToDelete.Text = "Mailbox to delete:";
            // 
            // _tbPassword
            // 
            this._tbPassword.Location = new System.Drawing.Point(15, 65);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(270, 20);
            this._tbPassword.TabIndex = 3;
            // 
            // _tbImap4Server
            // 
            this._tbImap4Server.Location = new System.Drawing.Point(15, 104);
            this._tbImap4Server.Name = "_tbImap4Server";
            this._tbImap4Server.Size = new System.Drawing.Size(270, 20);
            this._tbImap4Server.TabIndex = 5;
            // 
            // _lImap4Server
            // 
            this._lImap4Server.AutoSize = true;
            this._lImap4Server.Location = new System.Drawing.Point(12, 88);
            this._lImap4Server.Name = "_lImap4Server";
            this._lImap4Server.Size = new System.Drawing.Size(248, 13);
            this._lImap4Server.TabIndex = 4;
            this._lImap4Server.Text = "IMAP4 server address (will use 143 as default port):";
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
            // _bDeleteMailbox
            // 
            this._bDeleteMailbox.Location = new System.Drawing.Point(15, 208);
            this._bDeleteMailbox.Name = "_bDeleteMailbox";
            this._bDeleteMailbox.Size = new System.Drawing.Size(270, 36);
            this._bDeleteMailbox.TabIndex = 8;
            this._bDeleteMailbox.Text = "Delete mailbox";
            this._bDeleteMailbox.UseVisualStyleBackColor = true;
            this._bDeleteMailbox.Click += new System.EventHandler(this._bDeleteMailbox_Click);
            // 
            // _tbUserName
            // 
            this._tbUserName.Location = new System.Drawing.Point(15, 26);
            this._tbUserName.Name = "_tbUserName";
            this._tbUserName.Size = new System.Drawing.Size(270, 20);
            this._tbUserName.TabIndex = 1;
            // 
            // DeleteMailbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "DeleteMailbox";
            this.Text = "Delete a mailbox";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbMailboxToDelete;
        private System.Windows.Forms.Label _lUserName;
        private System.Windows.Forms.TextBox _tbUserName;
        private System.Windows.Forms.Label _lMailboxToDelete;
        private System.Windows.Forms.Button _bDeleteMailbox;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.Label _lPassword;
        private System.Windows.Forms.TextBox _tbImap4Server;
        private System.Windows.Forms.Label _lImap4Server;
    }
}
