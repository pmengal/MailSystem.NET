namespace ActiveUp.Net.Samples.IMAP4
{
    partial class EmptyMailbox
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
            this._tbMailboxToEmpty = new System.Windows.Forms.TextBox();
            this._lUserName = new System.Windows.Forms.Label();
            this._tbUserName = new System.Windows.Forms.TextBox();
            this._lMailboxToEmpty = new System.Windows.Forms.Label();
            this._bEmptyMailbox = new System.Windows.Forms.Button();
            this._tbPassword = new System.Windows.Forms.TextBox();
            this._lPassword = new System.Windows.Forms.Label();
            this._tbImap4Server = new System.Windows.Forms.TextBox();
            this._lImap4Server = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbMailboxToEmpty);
            this.splitContainer1.Panel1.Controls.Add(this._lUserName);
            this.splitContainer1.Panel1.Controls.Add(this._lImap4Server);
            this.splitContainer1.Panel1.Controls.Add(this._tbUserName);
            this.splitContainer1.Panel1.Controls.Add(this._tbImap4Server);
            this.splitContainer1.Panel1.Controls.Add(this._lMailboxToEmpty);
            this.splitContainer1.Panel1.Controls.Add(this._lPassword);
            this.splitContainer1.Panel1.Controls.Add(this._bEmptyMailbox);
            this.splitContainer1.Panel1.Controls.Add(this._tbPassword);
            this.splitContainer1.TabIndex = 1;
            // 
            // _tbMailboxToEmpty
            // 
            this._tbMailboxToEmpty.Location = new System.Drawing.Point(15, 143);
            this._tbMailboxToEmpty.Name = "_tbMailboxToEmpty";
            this._tbMailboxToEmpty.Size = new System.Drawing.Size(270, 20);
            this._tbMailboxToEmpty.TabIndex = 7;
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
            // _tbUserName
            // 
            this._tbUserName.Location = new System.Drawing.Point(15, 26);
            this._tbUserName.Name = "_tbUserName";
            this._tbUserName.Size = new System.Drawing.Size(270, 20);
            this._tbUserName.TabIndex = 1;
            // 
            // _lMailboxToEmpty
            // 
            this._lMailboxToEmpty.AutoSize = true;
            this._lMailboxToEmpty.Location = new System.Drawing.Point(12, 127);
            this._lMailboxToEmpty.Name = "_lMailboxToEmpty";
            this._lMailboxToEmpty.Size = new System.Drawing.Size(89, 13);
            this._lMailboxToEmpty.TabIndex = 6;
            this._lMailboxToEmpty.Text = "Mailbox to empty:";
            // 
            // _bEmptyMailbox
            // 
            this._bEmptyMailbox.Location = new System.Drawing.Point(15, 208);
            this._bEmptyMailbox.Name = "_bEmptyMailbox";
            this._bEmptyMailbox.Size = new System.Drawing.Size(270, 36);
            this._bEmptyMailbox.TabIndex = 8;
            this._bEmptyMailbox.Text = "Empty mailbox";
            this._bEmptyMailbox.UseVisualStyleBackColor = true;
            this._bEmptyMailbox.Click += new System.EventHandler(this._bEmptyMailbox_Click);
            // 
            // _tbPassword
            // 
            this._tbPassword.Location = new System.Drawing.Point(15, 65);
            this._tbPassword.Name = "_tbPassword";
            this._tbPassword.Size = new System.Drawing.Size(270, 20);
            this._tbPassword.TabIndex = 3;
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
            // EmptyMailbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "EmptyMailbox";
            this.Text = "Empty a mailbox";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbMailboxToEmpty;
        private System.Windows.Forms.Label _lUserName;
        private System.Windows.Forms.TextBox _tbUserName;
        private System.Windows.Forms.Label _lMailboxToEmpty;
        private System.Windows.Forms.Button _bEmptyMailbox;
        private System.Windows.Forms.TextBox _tbPassword;
        private System.Windows.Forms.Label _lPassword;
        private System.Windows.Forms.TextBox _tbImap4Server;
        private System.Windows.Forms.Label _lImap4Server;
    }
}
