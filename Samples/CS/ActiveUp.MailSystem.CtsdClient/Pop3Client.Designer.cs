namespace ActiveUp.MailSystem.CtsdClient
{
    partial class Pop3Client
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fromDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headerCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.retrieveMessagesButton = new System.Windows.Forms.Button();
            this.pop3ServerHostLabel = new System.Windows.Forms.Label();
            this.pop3ServerHostTextbox = new System.Windows.Forms.TextBox();
            this.pop3ServerSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.pop3PasswordLabel = new System.Windows.Forms.Label();
            this.pop3ServerPasswordTextbox = new System.Windows.Forms.TextBox();
            this.pop3PortLabel = new System.Windows.Forms.Label();
            this.pop3ServerPortNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pop3ServerUsernameLabel = new System.Windows.Forms.Label();
            this.pop3ServerUsernameTextbox = new System.Windows.Forms.TextBox();
            this.useSelectedButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerCollectionBindingSource)).BeginInit();
            this.pop3ServerSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pop3ServerPortNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fromDataGridViewTextBoxColumn,
            this.Subject,
            this.dateDataGridViewTextBoxColumn,
            this.Size});
            this.dataGridView1.DataSource = this.headerCollectionBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 129);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(392, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // fromDataGridViewTextBoxColumn
            // 
            this.fromDataGridViewTextBoxColumn.DataPropertyName = "From";
            this.fromDataGridViewTextBoxColumn.HeaderText = "From";
            this.fromDataGridViewTextBoxColumn.Name = "fromDataGridViewTextBoxColumn";
            // 
            // Subject
            // 
            this.Subject.HeaderText = "Subject";
            this.Subject.Name = "Subject";
            this.Subject.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // Size
            // 
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            // 
            // headerCollectionBindingSource
            // 
            this.headerCollectionBindingSource.DataSource = typeof(ActiveUp.Net.Mail.HeaderCollection);
            // 
            // retrieveMessagesButton
            // 
            this.retrieveMessagesButton.Location = new System.Drawing.Point(272, 36);
            this.retrieveMessagesButton.Name = "retrieveMessagesButton";
            this.retrieveMessagesButton.Size = new System.Drawing.Size(120, 59);
            this.retrieveMessagesButton.TabIndex = 1;
            this.retrieveMessagesButton.Text = "Retrieve Messages";
            this.retrieveMessagesButton.UseVisualStyleBackColor = true;
            this.retrieveMessagesButton.Click += new System.EventHandler(this.retrieveMessagesButton_Click);
            // 
            // pop3ServerHostLabel
            // 
            this.pop3ServerHostLabel.AutoSize = true;
            this.pop3ServerHostLabel.Location = new System.Drawing.Point(8, 20);
            this.pop3ServerHostLabel.Name = "pop3ServerHostLabel";
            this.pop3ServerHostLabel.Size = new System.Drawing.Size(32, 13);
            this.pop3ServerHostLabel.TabIndex = 2;
            this.pop3ServerHostLabel.Text = "Host:";
            // 
            // pop3ServerHostTextbox
            // 
            this.pop3ServerHostTextbox.Location = new System.Drawing.Point(11, 36);
            this.pop3ServerHostTextbox.Name = "pop3ServerHostTextbox";
            this.pop3ServerHostTextbox.Size = new System.Drawing.Size(170, 20);
            this.pop3ServerHostTextbox.TabIndex = 3;
            // 
            // pop3ServerSettingsGroupBox
            // 
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3PasswordLabel);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.retrieveMessagesButton);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3ServerPasswordTextbox);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3PortLabel);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3ServerPortNumericUpDown);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3ServerUsernameLabel);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3ServerUsernameTextbox);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3ServerHostLabel);
            this.pop3ServerSettingsGroupBox.Controls.Add(this.pop3ServerHostTextbox);
            this.pop3ServerSettingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.pop3ServerSettingsGroupBox.Name = "pop3ServerSettingsGroupBox";
            this.pop3ServerSettingsGroupBox.Size = new System.Drawing.Size(398, 111);
            this.pop3ServerSettingsGroupBox.TabIndex = 4;
            this.pop3ServerSettingsGroupBox.TabStop = false;
            this.pop3ServerSettingsGroupBox.Text = "POP3 Server Settings";
            // 
            // pop3PasswordLabel
            // 
            this.pop3PasswordLabel.AutoSize = true;
            this.pop3PasswordLabel.Location = new System.Drawing.Point(159, 59);
            this.pop3PasswordLabel.Name = "pop3PasswordLabel";
            this.pop3PasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.pop3PasswordLabel.TabIndex = 9;
            this.pop3PasswordLabel.Text = "Password:";
            // 
            // pop3ServerPasswordTextbox
            // 
            this.pop3ServerPasswordTextbox.Location = new System.Drawing.Point(162, 75);
            this.pop3ServerPasswordTextbox.Name = "pop3ServerPasswordTextbox";
            this.pop3ServerPasswordTextbox.Size = new System.Drawing.Size(104, 20);
            this.pop3ServerPasswordTextbox.TabIndex = 8;
            // 
            // pop3PortLabel
            // 
            this.pop3PortLabel.AutoSize = true;
            this.pop3PortLabel.Location = new System.Drawing.Point(184, 20);
            this.pop3PortLabel.Name = "pop3PortLabel";
            this.pop3PortLabel.Size = new System.Drawing.Size(29, 13);
            this.pop3PortLabel.TabIndex = 7;
            this.pop3PortLabel.Text = "Port:";
            // 
            // pop3ServerPortNumericUpDown
            // 
            this.pop3ServerPortNumericUpDown.Location = new System.Drawing.Point(187, 36);
            this.pop3ServerPortNumericUpDown.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.pop3ServerPortNumericUpDown.Name = "pop3ServerPortNumericUpDown";
            this.pop3ServerPortNumericUpDown.Size = new System.Drawing.Size(79, 20);
            this.pop3ServerPortNumericUpDown.TabIndex = 6;
            this.pop3ServerPortNumericUpDown.Value = new decimal(new int[] {
            110,
            0,
            0,
            0});
            // 
            // pop3ServerUsernameLabel
            // 
            this.pop3ServerUsernameLabel.AutoSize = true;
            this.pop3ServerUsernameLabel.Location = new System.Drawing.Point(8, 59);
            this.pop3ServerUsernameLabel.Name = "pop3ServerUsernameLabel";
            this.pop3ServerUsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.pop3ServerUsernameLabel.TabIndex = 5;
            this.pop3ServerUsernameLabel.Text = "Username:";
            // 
            // pop3ServerUsernameTextbox
            // 
            this.pop3ServerUsernameTextbox.Location = new System.Drawing.Point(11, 75);
            this.pop3ServerUsernameTextbox.Name = "pop3ServerUsernameTextbox";
            this.pop3ServerUsernameTextbox.Size = new System.Drawing.Size(145, 20);
            this.pop3ServerUsernameTextbox.TabIndex = 4;
            // 
            // useSelectedButton
            // 
            this.useSelectedButton.Location = new System.Drawing.Point(298, 285);
            this.useSelectedButton.Name = "useSelectedButton";
            this.useSelectedButton.Size = new System.Drawing.Size(106, 23);
            this.useSelectedButton.TabIndex = 5;
            this.useSelectedButton.Text = "Use Selected";
            this.useSelectedButton.UseVisualStyleBackColor = true;
            this.useSelectedButton.Click += new System.EventHandler(this.useSelectedButton_Click);
            // 
            // Pop3Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 316);
            this.Controls.Add(this.useSelectedButton);
            this.Controls.Add(this.pop3ServerSettingsGroupBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Pop3Client";
            this.Text = "Pop3Client";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerCollectionBindingSource)).EndInit();
            this.pop3ServerSettingsGroupBox.ResumeLayout(false);
            this.pop3ServerSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pop3ServerPortNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button retrieveMessagesButton;
        private System.Windows.Forms.Label pop3ServerHostLabel;
        private System.Windows.Forms.TextBox pop3ServerHostTextbox;
        private System.Windows.Forms.GroupBox pop3ServerSettingsGroupBox;
        private System.Windows.Forms.Label pop3PortLabel;
        private System.Windows.Forms.NumericUpDown pop3ServerPortNumericUpDown;
        private System.Windows.Forms.Label pop3ServerUsernameLabel;
        private System.Windows.Forms.TextBox pop3ServerUsernameTextbox;
        private System.Windows.Forms.Label pop3PasswordLabel;
        private System.Windows.Forms.TextBox pop3ServerPasswordTextbox;
        private System.Windows.Forms.Button useSelectedButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.BindingSource headerCollectionBindingSource;
    }
}