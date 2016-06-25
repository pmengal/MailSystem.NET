namespace ActiveUp.Net.Samples.NNTP
{
    partial class PostMessage
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
            this._bPostMessage = new System.Windows.Forms.Button();
            this._tbNntpServer = new System.Windows.Forms.TextBox();
            this._lNntpServer = new System.Windows.Forms.Label();
            this._tbBody = new System.Windows.Forms.TextBox();
            this._bBodyText = new System.Windows.Forms.Label();
            this._tbSubject = new System.Windows.Forms.TextBox();
            this._lSubject = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tbBody);
            this.splitContainer1.Panel1.Controls.Add(this._bBodyText);
            this.splitContainer1.Panel1.Controls.Add(this._tbSubject);
            this.splitContainer1.Panel1.Controls.Add(this._lSubject);
            this.splitContainer1.Panel1.Controls.Add(this._tbNewsgroup);
            this.splitContainer1.Panel1.Controls.Add(this._lNewsgroup);
            this.splitContainer1.Panel1.Controls.Add(this._lNntpServer);
            this.splitContainer1.Panel1.Controls.Add(this._bPostMessage);
            this.splitContainer1.Panel1.Controls.Add(this._tbNntpServer);
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
            // _bPostMessage
            // 
            this._bPostMessage.Location = new System.Drawing.Point(15, 257);
            this._bPostMessage.Name = "_bPostMessage";
            this._bPostMessage.Size = new System.Drawing.Size(270, 36);
            this._bPostMessage.TabIndex = 8;
            this._bPostMessage.Text = "Post message";
            this._bPostMessage.UseVisualStyleBackColor = true;
            this._bPostMessage.Click += new System.EventHandler(this._bPostMessage_Click);
            // 
            // _tbNntpServer
            // 
            this._tbNntpServer.Location = new System.Drawing.Point(15, 216);
            this._tbNntpServer.Name = "_tbNntpServer";
            this._tbNntpServer.Size = new System.Drawing.Size(270, 20);
            this._tbNntpServer.TabIndex = 7;
            // 
            // _lNntpServer
            // 
            this._lNntpServer.AutoSize = true;
            this._lNntpServer.Location = new System.Drawing.Point(12, 200);
            this._lNntpServer.Name = "_lNntpServer";
            this._lNntpServer.Size = new System.Drawing.Size(246, 13);
            this._lNntpServer.TabIndex = 6;
            this._lNntpServer.Text = "NNTP server address (will use 119 as default port):";
            // 
            // _tbBody
            // 
            this._tbBody.Location = new System.Drawing.Point(15, 105);
            this._tbBody.Multiline = true;
            this._tbBody.Name = "_tbBody";
            this._tbBody.Size = new System.Drawing.Size(270, 92);
            this._tbBody.TabIndex = 5;
            // 
            // _bBodyText
            // 
            this._bBodyText.AutoSize = true;
            this._bBodyText.Location = new System.Drawing.Point(12, 89);
            this._bBodyText.Name = "_bBodyText";
            this._bBodyText.Size = new System.Drawing.Size(54, 13);
            this._bBodyText.TabIndex = 4;
            this._bBodyText.Text = "Body text:";
            // 
            // _tbSubject
            // 
            this._tbSubject.Location = new System.Drawing.Point(15, 66);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.Size = new System.Drawing.Size(270, 20);
            this._tbSubject.TabIndex = 3;
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(12, 50);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(46, 13);
            this._lSubject.TabIndex = 2;
            this._lSubject.Text = "Subject:";
            // 
            // PostMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "PostMessage";
            this.Text = "Post a new message";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _tbNewsgroup;
        private System.Windows.Forms.Label _lNewsgroup;
        private System.Windows.Forms.Button _bPostMessage;
        private System.Windows.Forms.TextBox _tbNntpServer;
        private System.Windows.Forms.Label _lNntpServer;
        private System.Windows.Forms.TextBox _tbBody;
        private System.Windows.Forms.Label _bBodyText;
        private System.Windows.Forms.TextBox _tbSubject;
        private System.Windows.Forms.Label _lSubject;
    }
}
