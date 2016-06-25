namespace ActiveUp.MailSystem.DesktopClient.AccountSetupUI
{
    partial class SettingsReminderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsReminderForm));
            this.btnConfigureAccounts = new System.Windows.Forms.Button();
            this.btnNotNow = new System.Windows.Forms.Button();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConfigureAccounts
            // 
            this.btnConfigureAccounts.Location = new System.Drawing.Point(15, 96);
            this.btnConfigureAccounts.Name = "btnConfigureAccounts";
            this.btnConfigureAccounts.Size = new System.Drawing.Size(142, 23);
            this.btnConfigureAccounts.TabIndex = 0;
            this.btnConfigureAccounts.Text = "Configure Accounts";
            this.btnConfigureAccounts.UseVisualStyleBackColor = true;
            this.btnConfigureAccounts.Click += new System.EventHandler(this.btnConfigureAccounts_Click);
            // 
            // btnNotNow
            // 
            this.btnNotNow.Location = new System.Drawing.Point(163, 96);
            this.btnNotNow.Name = "btnNotNow";
            this.btnNotNow.Size = new System.Drawing.Size(142, 23);
            this.btnNotNow.TabIndex = 1;
            this.btnNotNow.Text = "Not now, thanks";
            this.btnNotNow.UseVisualStyleBackColor = true;
            this.btnNotNow.Click += new System.EventHandler(this.btnNotNow_Click);
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Location = new System.Drawing.Point(12, 9);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(293, 13);
            this.labelWelcome.TabIndex = 2;
            this.labelWelcome.Text = "Welcome to ActiveUp Desktop Client sample for MailSystem.";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(12, 32);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(293, 13);
            this.labelMessage.TabIndex = 3;
            this.labelMessage.Text = "In order to use the application you have to configure at least ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "one working mail account.";
            // 
            // SettingsReminderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 136);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.btnNotNow);
            this.Controls.Add(this.btnConfigureAccounts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsReminderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Settings Reminder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfigureAccounts;
        private System.Windows.Forms.Button btnNotNow;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label label1;
    }
}