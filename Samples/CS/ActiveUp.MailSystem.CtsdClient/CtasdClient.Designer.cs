namespace ActiveUp.MailSystem.CtsdClient
{
    partial class ctasdClientForm
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
            this.fromFileButton = new System.Windows.Forms.Button();
            this.fromPop3Button = new System.Windows.Forms.Button();
            this.currentStatusLabel = new System.Windows.Forms.Label();
            this.currentStatusTextbox = new System.Windows.Forms.TextBox();
            this.serverResponseGroupbox = new System.Windows.Forms.GroupBox();
            this.refIdLabel = new System.Windows.Forms.Label();
            this.flagLabel = new System.Windows.Forms.Label();
            this.vodClassificationLabel = new System.Windows.Forms.Label();
            this.refIdTextbox = new System.Windows.Forms.TextBox();
            this.flagTextbox = new System.Windows.Forms.TextBox();
            this.vodClassificationTextbox = new System.Windows.Forms.TextBox();
            this.spamClassificationTextbox = new System.Windows.Forms.TextBox();
            this.spamClassificationLabel = new System.Windows.Forms.Label();
            this.serverSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.portLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.openMessageFromFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.serverResponseGroupbox.SuspendLayout();
            this.serverSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // fromFileButton
            // 
            this.fromFileButton.Location = new System.Drawing.Point(12, 12);
            this.fromFileButton.Name = "fromFileButton";
            this.fromFileButton.Size = new System.Drawing.Size(162, 23);
            this.fromFileButton.TabIndex = 0;
            this.fromFileButton.Text = "Test message from file";
            this.fromFileButton.UseVisualStyleBackColor = true;
            this.fromFileButton.Click += new System.EventHandler(this.fromFileButton_Click);
            // 
            // fromPop3Button
            // 
            this.fromPop3Button.Location = new System.Drawing.Point(180, 12);
            this.fromPop3Button.Name = "fromPop3Button";
            this.fromPop3Button.Size = new System.Drawing.Size(162, 23);
            this.fromPop3Button.TabIndex = 1;
            this.fromPop3Button.Text = "Test message from mail server";
            this.fromPop3Button.UseVisualStyleBackColor = true;
            this.fromPop3Button.Click += new System.EventHandler(this.fromPop3Button_Click);
            // 
            // currentStatusLabel
            // 
            this.currentStatusLabel.AutoSize = true;
            this.currentStatusLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.currentStatusLabel.Location = new System.Drawing.Point(6, 16);
            this.currentStatusLabel.Name = "currentStatusLabel";
            this.currentStatusLabel.Size = new System.Drawing.Size(120, 13);
            this.currentStatusLabel.TabIndex = 2;
            this.currentStatusLabel.Text = "Current Request Status:";
            // 
            // currentStatusTextbox
            // 
            this.currentStatusTextbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.currentStatusTextbox.ForeColor = System.Drawing.Color.Red;
            this.currentStatusTextbox.Location = new System.Drawing.Point(132, 13);
            this.currentStatusTextbox.Name = "currentStatusTextbox";
            this.currentStatusTextbox.ReadOnly = true;
            this.currentStatusTextbox.Size = new System.Drawing.Size(191, 20);
            this.currentStatusTextbox.TabIndex = 3;
            // 
            // serverResponseGroupbox
            // 
            this.serverResponseGroupbox.Controls.Add(this.refIdLabel);
            this.serverResponseGroupbox.Controls.Add(this.flagLabel);
            this.serverResponseGroupbox.Controls.Add(this.vodClassificationLabel);
            this.serverResponseGroupbox.Controls.Add(this.refIdTextbox);
            this.serverResponseGroupbox.Controls.Add(this.flagTextbox);
            this.serverResponseGroupbox.Controls.Add(this.vodClassificationTextbox);
            this.serverResponseGroupbox.Controls.Add(this.spamClassificationTextbox);
            this.serverResponseGroupbox.Controls.Add(this.spamClassificationLabel);
            this.serverResponseGroupbox.Controls.Add(this.currentStatusLabel);
            this.serverResponseGroupbox.Controls.Add(this.currentStatusTextbox);
            this.serverResponseGroupbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverResponseGroupbox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.serverResponseGroupbox.Location = new System.Drawing.Point(13, 109);
            this.serverResponseGroupbox.Name = "serverResponseGroupbox";
            this.serverResponseGroupbox.Size = new System.Drawing.Size(329, 150);
            this.serverResponseGroupbox.TabIndex = 4;
            this.serverResponseGroupbox.TabStop = false;
            this.serverResponseGroupbox.Text = "Server Response";
            // 
            // refIdLabel
            // 
            this.refIdLabel.AutoSize = true;
            this.refIdLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.refIdLabel.Location = new System.Drawing.Point(6, 120);
            this.refIdLabel.Name = "refIdLabel";
            this.refIdLabel.Size = new System.Drawing.Size(38, 13);
            this.refIdLabel.TabIndex = 11;
            this.refIdLabel.Text = "RefID:";
            // 
            // flagLabel
            // 
            this.flagLabel.AutoSize = true;
            this.flagLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.flagLabel.Location = new System.Drawing.Point(6, 94);
            this.flagLabel.Name = "flagLabel";
            this.flagLabel.Size = new System.Drawing.Size(30, 13);
            this.flagLabel.TabIndex = 10;
            this.flagLabel.Text = "Flag:";
            // 
            // vodClassificationLabel
            // 
            this.vodClassificationLabel.AutoSize = true;
            this.vodClassificationLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.vodClassificationLabel.Location = new System.Drawing.Point(6, 68);
            this.vodClassificationLabel.Name = "vodClassificationLabel";
            this.vodClassificationLabel.Size = new System.Drawing.Size(97, 13);
            this.vodClassificationLabel.TabIndex = 9;
            this.vodClassificationLabel.Text = "VOD Classification:";
            // 
            // refIdTextbox
            // 
            this.refIdTextbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.refIdTextbox.ForeColor = System.Drawing.Color.Red;
            this.refIdTextbox.Location = new System.Drawing.Point(132, 117);
            this.refIdTextbox.Name = "refIdTextbox";
            this.refIdTextbox.ReadOnly = true;
            this.refIdTextbox.Size = new System.Drawing.Size(191, 20);
            this.refIdTextbox.TabIndex = 8;
            // 
            // flagTextbox
            // 
            this.flagTextbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flagTextbox.ForeColor = System.Drawing.Color.Red;
            this.flagTextbox.Location = new System.Drawing.Point(132, 91);
            this.flagTextbox.Name = "flagTextbox";
            this.flagTextbox.ReadOnly = true;
            this.flagTextbox.Size = new System.Drawing.Size(191, 20);
            this.flagTextbox.TabIndex = 7;
            // 
            // vodClassificationTextbox
            // 
            this.vodClassificationTextbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.vodClassificationTextbox.ForeColor = System.Drawing.Color.Red;
            this.vodClassificationTextbox.Location = new System.Drawing.Point(132, 65);
            this.vodClassificationTextbox.Name = "vodClassificationTextbox";
            this.vodClassificationTextbox.ReadOnly = true;
            this.vodClassificationTextbox.Size = new System.Drawing.Size(191, 20);
            this.vodClassificationTextbox.TabIndex = 6;
            // 
            // spamClassificationTextbox
            // 
            this.spamClassificationTextbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.spamClassificationTextbox.ForeColor = System.Drawing.Color.Red;
            this.spamClassificationTextbox.Location = new System.Drawing.Point(132, 39);
            this.spamClassificationTextbox.Name = "spamClassificationTextbox";
            this.spamClassificationTextbox.ReadOnly = true;
            this.spamClassificationTextbox.Size = new System.Drawing.Size(191, 20);
            this.spamClassificationTextbox.TabIndex = 5;
            // 
            // spamClassificationLabel
            // 
            this.spamClassificationLabel.AutoSize = true;
            this.spamClassificationLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.spamClassificationLabel.Location = new System.Drawing.Point(6, 42);
            this.spamClassificationLabel.Name = "spamClassificationLabel";
            this.spamClassificationLabel.Size = new System.Drawing.Size(101, 13);
            this.spamClassificationLabel.TabIndex = 4;
            this.spamClassificationLabel.Text = "Spam Classification:";
            // 
            // serverSettingsGroupBox
            // 
            this.serverSettingsGroupBox.Controls.Add(this.portNumericUpDown);
            this.serverSettingsGroupBox.Controls.Add(this.portLabel);
            this.serverSettingsGroupBox.Controls.Add(this.hostTextBox);
            this.serverSettingsGroupBox.Controls.Add(this.hostLabel);
            this.serverSettingsGroupBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.serverSettingsGroupBox.Location = new System.Drawing.Point(12, 41);
            this.serverSettingsGroupBox.Name = "serverSettingsGroupBox";
            this.serverSettingsGroupBox.Size = new System.Drawing.Size(329, 62);
            this.serverSettingsGroupBox.TabIndex = 5;
            this.serverSettingsGroupBox.TabStop = false;
            this.serverSettingsGroupBox.Text = "Server Settings";
            // 
            // portNumericUpDown
            // 
            this.portNumericUpDown.Location = new System.Drawing.Point(234, 32);
            this.portNumericUpDown.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.Size = new System.Drawing.Size(89, 20);
            this.portNumericUpDown.TabIndex = 3;
            this.portNumericUpDown.Value = new decimal(new int[] {
            8088,
            0,
            0,
            0});
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.portLabel.Location = new System.Drawing.Point(234, 16);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port:";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(9, 32);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(218, 20);
            this.hostTextBox.TabIndex = 1;
            this.hostTextBox.Text = "localhost";
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hostLabel.Location = new System.Drawing.Point(6, 16);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(32, 13);
            this.hostLabel.TabIndex = 0;
            this.hostLabel.Text = "Host:";
            // 
            // ctsdClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 269);
            this.Controls.Add(this.serverSettingsGroupBox);
            this.Controls.Add(this.serverResponseGroupbox);
            this.Controls.Add(this.fromPop3Button);
            this.Controls.Add(this.fromFileButton);
            this.Name = "ctsdClientForm";
            this.Text = "Ctsd Client";
            this.serverResponseGroupbox.ResumeLayout(false);
            this.serverResponseGroupbox.PerformLayout();
            this.serverSettingsGroupBox.ResumeLayout(false);
            this.serverSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button fromFileButton;
        private System.Windows.Forms.Button fromPop3Button;
        private System.Windows.Forms.Label currentStatusLabel;
        private System.Windows.Forms.TextBox currentStatusTextbox;
        private System.Windows.Forms.GroupBox serverResponseGroupbox;
        private System.Windows.Forms.GroupBox serverSettingsGroupBox;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.NumericUpDown portNumericUpDown;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label vodClassificationLabel;
        private System.Windows.Forms.TextBox refIdTextbox;
        private System.Windows.Forms.TextBox flagTextbox;
        private System.Windows.Forms.TextBox vodClassificationTextbox;
        private System.Windows.Forms.TextBox spamClassificationTextbox;
        private System.Windows.Forms.Label spamClassificationLabel;
        private System.Windows.Forms.Label refIdLabel;
        private System.Windows.Forms.Label flagLabel;
        private System.Windows.Forms.OpenFileDialog openMessageFromFileDialog;
    }
}

