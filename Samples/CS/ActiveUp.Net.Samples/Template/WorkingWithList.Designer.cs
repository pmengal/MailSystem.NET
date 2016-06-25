namespace ActiveUp.Net.Samples.Template
{
    partial class WorkingWithList
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
            this.smtpServerAddressTextbox = new System.Windows.Forms.TextBox();
            this.smtpServerAddressLabel = new System.Windows.Forms.Label();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this._lvDataItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this._bAdd = new System.Windows.Forms.Button();
            this._bRemove = new System.Windows.Forms.Button();
            this._lDataItems = new System.Windows.Forms.Label();
            this._bShowSource = new System.Windows.Forms.Button();
            this._bShowTemplate = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._bShowSource);
            this.splitContainer1.Panel1.Controls.Add(this._bShowTemplate);
            this.splitContainer1.Panel1.Controls.Add(this._lvDataItems);
            this.splitContainer1.Panel1.Controls.Add(this._bAdd);
            this.splitContainer1.Panel1.Controls.Add(this._bRemove);
            this.splitContainer1.Panel1.Controls.Add(this._lDataItems);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressTextbox);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressLabel);
            this.splitContainer1.Panel1.Controls.Add(this.sendMessageButton);
            this.splitContainer1.TabIndex = 1;
            // 
            // smtpServerAddressTextbox
            // 
            this.smtpServerAddressTextbox.Location = new System.Drawing.Point(12, 229);
            this.smtpServerAddressTextbox.Name = "smtpServerAddressTextbox";
            this.smtpServerAddressTextbox.Size = new System.Drawing.Size(273, 20);
            this.smtpServerAddressTextbox.TabIndex = 5;
            // 
            // smtpServerAddressLabel
            // 
            this.smtpServerAddressLabel.AutoSize = true;
            this.smtpServerAddressLabel.Location = new System.Drawing.Point(12, 213);
            this.smtpServerAddressLabel.Name = "smtpServerAddressLabel";
            this.smtpServerAddressLabel.Size = new System.Drawing.Size(240, 13);
            this.smtpServerAddressLabel.TabIndex = 4;
            this.smtpServerAddressLabel.Text = "SMTP server address (will use 25 as default port):";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(12, 281);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(270, 36);
            this.sendMessageButton.TabIndex = 6;
            this.sendMessageButton.Text = "Send message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // _lvDataItems
            // 
            this._lvDataItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this._lvDataItems.FullRowSelect = true;
            this._lvDataItems.GridLines = true;
            this._lvDataItems.HideSelection = false;
            this._lvDataItems.Location = new System.Drawing.Point(15, 25);
            this._lvDataItems.MultiSelect = false;
            this._lvDataItems.Name = "_lvDataItems";
            this._lvDataItems.Size = new System.Drawing.Size(269, 108);
            this._lvDataItems.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._lvDataItems.TabIndex = 1;
            this._lvDataItems.UseCompatibleStateImageBehavior = false;
            this._lvDataItems.View = System.Windows.Forms.View.Details;
            this._lvDataItems.SelectedIndexChanged += new System.EventHandler(this._lvDataItems_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "First name";
            this.columnHeader1.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Last name";
            this.columnHeader2.Width = 69;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Product";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantity";
            // 
            // _bAdd
            // 
            this._bAdd.Location = new System.Drawing.Point(158, 139);
            this._bAdd.Name = "_bAdd";
            this._bAdd.Size = new System.Drawing.Size(59, 23);
            this._bAdd.TabIndex = 2;
            this._bAdd.Text = "Add";
            this._bAdd.UseVisualStyleBackColor = true;
            this._bAdd.Click += new System.EventHandler(this._bAdd_Click);
            // 
            // _bRemove
            // 
            this._bRemove.Enabled = false;
            this._bRemove.Location = new System.Drawing.Point(223, 139);
            this._bRemove.Name = "_bRemove";
            this._bRemove.Size = new System.Drawing.Size(61, 23);
            this._bRemove.TabIndex = 3;
            this._bRemove.Text = "Remove";
            this._bRemove.UseVisualStyleBackColor = true;
            this._bRemove.Click += new System.EventHandler(this._bRemove_Click);
            // 
            // _lDataItems
            // 
            this._lDataItems.AutoSize = true;
            this._lDataItems.Location = new System.Drawing.Point(12, 9);
            this._lDataItems.Name = "_lDataItems";
            this._lDataItems.Size = new System.Drawing.Size(63, 13);
            this._lDataItems.TabIndex = 0;
            this._lDataItems.Text = "Data items :";
            // 
            // _bShowSource
            // 
            this._bShowSource.Location = new System.Drawing.Point(15, 168);
            this._bShowSource.Name = "_bShowSource";
            this._bShowSource.Size = new System.Drawing.Size(101, 23);
            this._bShowSource.TabIndex = 10;
            this._bShowSource.Text = "Show source";
            this._bShowSource.UseVisualStyleBackColor = true;
            this._bShowSource.Click += new System.EventHandler(this._bShowSource_Click);
            // 
            // _bShowTemplate
            // 
            this._bShowTemplate.Location = new System.Drawing.Point(15, 139);
            this._bShowTemplate.Name = "_bShowTemplate";
            this._bShowTemplate.Size = new System.Drawing.Size(101, 23);
            this._bShowTemplate.TabIndex = 9;
            this._bShowTemplate.Text = "Show template";
            this._bShowTemplate.UseVisualStyleBackColor = true;
            this._bShowTemplate.Click += new System.EventHandler(this._bShowTemplate_Click);
            // 
            // WorkingWithList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "WorkingWithList";
            this.Text = "Working with list";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox smtpServerAddressTextbox;
        private System.Windows.Forms.Label smtpServerAddressLabel;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.ListView _lvDataItems;
        private System.Windows.Forms.Button _bAdd;
        private System.Windows.Forms.Button _bRemove;
        private System.Windows.Forms.Label _lDataItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button _bShowSource;
        private System.Windows.Forms.Button _bShowTemplate;
    }
}
