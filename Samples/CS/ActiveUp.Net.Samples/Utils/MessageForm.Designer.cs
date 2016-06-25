namespace ActiveUp.Net.Samples.Utils
{
    partial class MessageForm
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
            this._lFromEmail = new System.Windows.Forms.Label();
            this._tbFromEmail = new System.Windows.Forms.TextBox();
            this._tbToEmail = new System.Windows.Forms.TextBox();
            this._lToEmail = new System.Windows.Forms.Label();
            this._tbSubject = new System.Windows.Forms.TextBox();
            this._lSubject = new System.Windows.Forms.Label();
            this._lBody = new System.Windows.Forms.Label();
            this._wbBody = new System.Windows.Forms.WebBrowser();
            this._bOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lFromEmail
            // 
            this._lFromEmail.AutoSize = true;
            this._lFromEmail.Location = new System.Drawing.Point(12, 9);
            this._lFromEmail.Name = "_lFromEmail";
            this._lFromEmail.Size = new System.Drawing.Size(63, 13);
            this._lFromEmail.TabIndex = 0;
            this._lFromEmail.Text = "From email :";
            // 
            // _tbFromEmail
            // 
            this._tbFromEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbFromEmail.Location = new System.Drawing.Point(15, 25);
            this._tbFromEmail.Name = "_tbFromEmail";
            this._tbFromEmail.ReadOnly = true;
            this._tbFromEmail.Size = new System.Drawing.Size(391, 20);
            this._tbFromEmail.TabIndex = 1;
            // 
            // _tbToEmail
            // 
            this._tbToEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbToEmail.Location = new System.Drawing.Point(15, 64);
            this._tbToEmail.Name = "_tbToEmail";
            this._tbToEmail.ReadOnly = true;
            this._tbToEmail.Size = new System.Drawing.Size(391, 20);
            this._tbToEmail.TabIndex = 3;
            // 
            // _lToEmail
            // 
            this._lToEmail.AutoSize = true;
            this._lToEmail.Location = new System.Drawing.Point(12, 48);
            this._lToEmail.Name = "_lToEmail";
            this._lToEmail.Size = new System.Drawing.Size(53, 13);
            this._lToEmail.TabIndex = 2;
            this._lToEmail.Text = "To email :";
            // 
            // _tbSubject
            // 
            this._tbSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbSubject.Location = new System.Drawing.Point(15, 103);
            this._tbSubject.Name = "_tbSubject";
            this._tbSubject.ReadOnly = true;
            this._tbSubject.Size = new System.Drawing.Size(391, 20);
            this._tbSubject.TabIndex = 5;
            // 
            // _lSubject
            // 
            this._lSubject.AutoSize = true;
            this._lSubject.Location = new System.Drawing.Point(12, 87);
            this._lSubject.Name = "_lSubject";
            this._lSubject.Size = new System.Drawing.Size(49, 13);
            this._lSubject.TabIndex = 4;
            this._lSubject.Text = "Subject :";
            // 
            // _lBody
            // 
            this._lBody.AutoSize = true;
            this._lBody.Location = new System.Drawing.Point(12, 126);
            this._lBody.Name = "_lBody";
            this._lBody.Size = new System.Drawing.Size(37, 13);
            this._lBody.TabIndex = 6;
            this._lBody.Text = "Body :";
            // 
            // _wbBody
            // 
            this._wbBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._wbBody.Location = new System.Drawing.Point(15, 142);
            this._wbBody.MinimumSize = new System.Drawing.Size(20, 20);
            this._wbBody.Name = "_wbBody";
            this._wbBody.Size = new System.Drawing.Size(391, 98);
            this._wbBody.TabIndex = 7;
            // 
            // _bOk
            // 
            this._bOk.Location = new System.Drawing.Point(331, 246);
            this._bOk.Name = "_bOk";
            this._bOk.Size = new System.Drawing.Size(75, 23);
            this._bOk.TabIndex = 8;
            this._bOk.Text = "Ok";
            this._bOk.UseVisualStyleBackColor = true;
            this._bOk.Click += new System.EventHandler(this._bOk_Click);
            // 
            // MessageForm
            // 
            this.AcceptButton = this._bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 274);
            this.Controls.Add(this._bOk);
            this.Controls.Add(this._wbBody);
            this.Controls.Add(this._lBody);
            this.Controls.Add(this._tbSubject);
            this.Controls.Add(this._lSubject);
            this.Controls.Add(this._tbToEmail);
            this.Controls.Add(this._lToEmail);
            this.Controls.Add(this._tbFromEmail);
            this.Controls.Add(this._lFromEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lFromEmail;
        private System.Windows.Forms.TextBox _tbFromEmail;
        private System.Windows.Forms.TextBox _tbToEmail;
        private System.Windows.Forms.Label _lToEmail;
        private System.Windows.Forms.TextBox _tbSubject;
        private System.Windows.Forms.Label _lSubject;
        private System.Windows.Forms.Label _lBody;
        private System.Windows.Forms.WebBrowser _wbBody;
        private System.Windows.Forms.Button _bOk;
    }
}