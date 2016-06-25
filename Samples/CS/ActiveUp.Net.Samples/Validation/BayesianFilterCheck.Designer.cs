namespace ActiveUp.Net.Samples.Validation
{
    partial class BayesianFilterCheck
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
            this.isSpamButton = new System.Windows.Forms.Button();
            this.notSpamButton = new System.Windows.Forms.Button();
            this.checkSpamButton = new System.Windows.Forms.Button();
            this.copyPasteText = new System.Windows.Forms.Label();
            this.messageTextbox = new System.Windows.Forms.TextBox();
            this.reportMessage = new System.Windows.Forms.GroupBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.reportMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.reportMessage);
            this.splitContainer1.Panel1.Controls.Add(this.messageTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.copyPasteText);
            this.splitContainer1.Panel1.Controls.Add(this.checkSpamButton);
            // 
            // isSpamButton
            // 
            this.isSpamButton.Location = new System.Drawing.Point(8, 19);
            this.isSpamButton.Name = "isSpamButton";
            this.isSpamButton.Size = new System.Drawing.Size(75, 23);
            this.isSpamButton.TabIndex = 0;
            this.isSpamButton.Text = "Is Spam";
            this.isSpamButton.UseVisualStyleBackColor = true;
            this.isSpamButton.Click += new System.EventHandler(this.isSpamButton_Click);
            // 
            // notSpamButton
            // 
            this.notSpamButton.Location = new System.Drawing.Point(89, 19);
            this.notSpamButton.Name = "notSpamButton";
            this.notSpamButton.Size = new System.Drawing.Size(75, 23);
            this.notSpamButton.TabIndex = 1;
            this.notSpamButton.Text = "Not Spam";
            this.notSpamButton.UseVisualStyleBackColor = true;
            this.notSpamButton.Click += new System.EventHandler(this.notSpamButton_Click);
            // 
            // checkSpamButton
            // 
            this.checkSpamButton.Location = new System.Drawing.Point(59, 391);
            this.checkSpamButton.Name = "checkSpamButton";
            this.checkSpamButton.Size = new System.Drawing.Size(174, 23);
            this.checkSpamButton.TabIndex = 2;
            this.checkSpamButton.Text = "Check Spam";
            this.checkSpamButton.UseVisualStyleBackColor = true;
            this.checkSpamButton.Click += new System.EventHandler(this.checkSpamButton_Click);
            // 
            // copyPasteText
            // 
            this.copyPasteText.AutoSize = true;
            this.copyPasteText.Location = new System.Drawing.Point(9, 9);
            this.copyPasteText.Name = "copyPasteText";
            this.copyPasteText.Size = new System.Drawing.Size(183, 13);
            this.copyPasteText.TabIndex = 3;
            this.copyPasteText.Text = "Copy paste the message body below:";
            // 
            // messageTextbox
            // 
            this.messageTextbox.Location = new System.Drawing.Point(12, 25);
            this.messageTextbox.Multiline = true;
            this.messageTextbox.Name = "messageTextbox";
            this.messageTextbox.Size = new System.Drawing.Size(273, 360);
            this.messageTextbox.TabIndex = 4;
            // 
            // reportMessage
            // 
            this.reportMessage.Controls.Add(this.notSpamButton);
            this.reportMessage.Controls.Add(this.isSpamButton);
            this.reportMessage.Location = new System.Drawing.Point(59, 444);
            this.reportMessage.Name = "reportMessage";
            this.reportMessage.Size = new System.Drawing.Size(174, 58);
            this.reportMessage.TabIndex = 5;
            this.reportMessage.TabStop = false;
            this.reportMessage.Text = "Report Message";
            // 
            // BayesianFilterCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "BayesianFilterCheck";
            this.Text = "BayesianFilterCheck";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.reportMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button isSpamButton;
        private System.Windows.Forms.Button notSpamButton;
        private System.Windows.Forms.TextBox messageTextbox;
        private System.Windows.Forms.Label copyPasteText;
        private System.Windows.Forms.Button checkSpamButton;
        private System.Windows.Forms.GroupBox reportMessage;
    }
}