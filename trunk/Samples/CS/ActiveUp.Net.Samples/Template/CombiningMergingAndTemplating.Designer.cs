namespace ActiveUp.Net.Samples.Template
{
    partial class CombiningMergingAndTemplating
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
            this._cbUseSmtpFromTemplate = new System.Windows.Forms.CheckBox();
            this.gbDataItem = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._tbOrderId = new System.Windows.Forms.TextBox();
            this._tbEmail = new System.Windows.Forms.TextBox();
            this._tbLastName = new System.Windows.Forms.TextBox();
            this._lFirstName = new System.Windows.Forms.Label();
            this._lLastName = new System.Windows.Forms.Label();
            this._lEmail = new System.Windows.Forms.Label();
            this._lOrderId = new System.Windows.Forms.Label();
            this._lProduct = new System.Windows.Forms.Label();
            this._tbFirstName = new System.Windows.Forms.TextBox();
            this._comboProduct = new System.Windows.Forms.ComboBox();
            this._bShowSource = new System.Windows.Forms.Button();
            this._bShowTemplate = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbDataItem.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._bShowSource);
            this.splitContainer1.Panel1.Controls.Add(this._bShowTemplate);
            this.splitContainer1.Panel1.Controls.Add(this.gbDataItem);
            this.splitContainer1.Panel1.Controls.Add(this._cbUseSmtpFromTemplate);
            this.splitContainer1.Panel1.Controls.Add(this.sendMessageButton);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressLabel);
            this.splitContainer1.Panel1.Controls.Add(this.smtpServerAddressTextbox);
            this.splitContainer1.TabIndex = 1;
            // 
            // smtpServerAddressTextbox
            // 
            this.smtpServerAddressTextbox.Location = new System.Drawing.Point(12, 235);
            this.smtpServerAddressTextbox.Name = "smtpServerAddressTextbox";
            this.smtpServerAddressTextbox.Size = new System.Drawing.Size(273, 20);
            this.smtpServerAddressTextbox.TabIndex = 3;
            // 
            // smtpServerAddressLabel
            // 
            this.smtpServerAddressLabel.AutoSize = true;
            this.smtpServerAddressLabel.Location = new System.Drawing.Point(12, 219);
            this.smtpServerAddressLabel.Name = "smtpServerAddressLabel";
            this.smtpServerAddressLabel.Size = new System.Drawing.Size(240, 13);
            this.smtpServerAddressLabel.TabIndex = 2;
            this.smtpServerAddressLabel.Text = "SMTP server address (will use 25 as default port):";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(12, 287);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(270, 36);
            this.sendMessageButton.TabIndex = 4;
            this.sendMessageButton.Text = "Send message";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // _cbUseSmtpFromTemplate
            // 
            this._cbUseSmtpFromTemplate.AutoSize = true;
            this._cbUseSmtpFromTemplate.Location = new System.Drawing.Point(12, 199);
            this._cbUseSmtpFromTemplate.Name = "_cbUseSmtpFromTemplate";
            this._cbUseSmtpFromTemplate.Size = new System.Drawing.Size(222, 17);
            this._cbUseSmtpFromTemplate.TabIndex = 1;
            this._cbUseSmtpFromTemplate.Text = "Use the smtp server from the xml template";
            this._cbUseSmtpFromTemplate.UseVisualStyleBackColor = true;
            // 
            // gbDataItem
            // 
            this.gbDataItem.Controls.Add(this.tableLayoutPanel1);
            this.gbDataItem.Location = new System.Drawing.Point(12, 3);
            this.gbDataItem.Name = "gbDataItem";
            this.gbDataItem.Size = new System.Drawing.Size(273, 137);
            this.gbDataItem.TabIndex = 0;
            this.gbDataItem.TabStop = false;
            this.gbDataItem.Text = "Data item";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 222F));
            this.tableLayoutPanel1.Controls.Add(this._tbOrderId, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this._tbEmail, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this._tbLastName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this._lFirstName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._lLastName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._lEmail, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lOrderId, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._lProduct, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this._tbFirstName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._comboProduct, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(267, 117);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _tbOrderId
            // 
            this._tbOrderId.Location = new System.Drawing.Point(85, 70);
            this._tbOrderId.Name = "_tbOrderId";
            this._tbOrderId.ReadOnly = true;
            this._tbOrderId.Size = new System.Drawing.Size(179, 20);
            this._tbOrderId.TabIndex = 7;
            // 
            // _tbEmail
            // 
            this._tbEmail.Location = new System.Drawing.Point(85, 48);
            this._tbEmail.Name = "_tbEmail";
            this._tbEmail.Size = new System.Drawing.Size(179, 20);
            this._tbEmail.TabIndex = 5;
            // 
            // _tbLastName
            // 
            this._tbLastName.Location = new System.Drawing.Point(85, 26);
            this._tbLastName.Name = "_tbLastName";
            this._tbLastName.Size = new System.Drawing.Size(179, 20);
            this._tbLastName.TabIndex = 3;
            // 
            // _lFirstName
            // 
            this._lFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lFirstName.AutoSize = true;
            this._lFirstName.Location = new System.Drawing.Point(18, 0);
            this._lFirstName.Name = "_lFirstName";
            this._lFirstName.Size = new System.Drawing.Size(61, 23);
            this._lFirstName.TabIndex = 0;
            this._lFirstName.Text = "First name :";
            this._lFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lLastName
            // 
            this._lLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lLastName.AutoSize = true;
            this._lLastName.Location = new System.Drawing.Point(17, 23);
            this._lLastName.Name = "_lLastName";
            this._lLastName.Size = new System.Drawing.Size(62, 22);
            this._lLastName.TabIndex = 2;
            this._lLastName.Text = "Last name :";
            this._lLastName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lEmail
            // 
            this._lEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lEmail.AutoSize = true;
            this._lEmail.Location = new System.Drawing.Point(41, 45);
            this._lEmail.Name = "_lEmail";
            this._lEmail.Size = new System.Drawing.Size(38, 22);
            this._lEmail.TabIndex = 4;
            this._lEmail.Text = "Email :";
            this._lEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lOrderId
            // 
            this._lOrderId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lOrderId.AutoSize = true;
            this._lOrderId.Location = new System.Drawing.Point(29, 67);
            this._lOrderId.Name = "_lOrderId";
            this._lOrderId.Size = new System.Drawing.Size(50, 22);
            this._lOrderId.TabIndex = 6;
            this._lOrderId.Text = "Order id :";
            this._lOrderId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lProduct
            // 
            this._lProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lProduct.AutoSize = true;
            this._lProduct.Location = new System.Drawing.Point(29, 89);
            this._lProduct.Name = "_lProduct";
            this._lProduct.Size = new System.Drawing.Size(50, 23);
            this._lProduct.TabIndex = 8;
            this._lProduct.Text = "Product :";
            this._lProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _tbFirstName
            // 
            this._tbFirstName.Location = new System.Drawing.Point(85, 3);
            this._tbFirstName.Name = "_tbFirstName";
            this._tbFirstName.Size = new System.Drawing.Size(179, 20);
            this._tbFirstName.TabIndex = 1;
            // 
            // _comboProduct
            // 
            this._comboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboProduct.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._comboProduct.FormattingEnabled = true;
            this._comboProduct.Items.AddRange(new object[] {
            "Ajax Panel",
            "Calendar",
            "Color Picker",
            "Content Rotator",
            "Form Input",
            "HTML Editor",
            "Image Editor",
            "Ini Manager",
            "Localization",
            "Multiple Upload",
            "Page Loader",
            "Pager",
            "Panel Bar",
            "Popup Engine",
            "Protector",
            "Spell Checker",
            "Tree View",
            "Web Menu",
            "Web Timer",
            "Web Toolbar"});
            this._comboProduct.Location = new System.Drawing.Point(85, 92);
            this._comboProduct.Name = "_comboProduct";
            this._comboProduct.Size = new System.Drawing.Size(179, 21);
            this._comboProduct.Sorted = true;
            this._comboProduct.TabIndex = 9;
            // 
            // _bShowSource
            // 
            this._bShowSource.Location = new System.Drawing.Point(184, 146);
            this._bShowSource.Name = "_bShowSource";
            this._bShowSource.Size = new System.Drawing.Size(101, 23);
            this._bShowSource.TabIndex = 10;
            this._bShowSource.Text = "Show source";
            this._bShowSource.UseVisualStyleBackColor = true;
            this._bShowSource.Click += new System.EventHandler(this._bShowSource_Click);
            // 
            // _bShowTemplate
            // 
            this._bShowTemplate.Location = new System.Drawing.Point(12, 146);
            this._bShowTemplate.Name = "_bShowTemplate";
            this._bShowTemplate.Size = new System.Drawing.Size(101, 23);
            this._bShowTemplate.TabIndex = 9;
            this._bShowTemplate.Text = "Show template";
            this._bShowTemplate.UseVisualStyleBackColor = true;
            this._bShowTemplate.Click += new System.EventHandler(this._bShowTemplate_Click);
            // 
            // CombiningMergingAndTemplating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(910, 514);
            this.Name = "CombiningMergingAndTemplating";
            this.Text = "Combining merging and templating";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.gbDataItem.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox smtpServerAddressTextbox;
        private System.Windows.Forms.Label smtpServerAddressLabel;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.CheckBox _cbUseSmtpFromTemplate;
        private System.Windows.Forms.GroupBox gbDataItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox _tbOrderId;
        private System.Windows.Forms.TextBox _tbEmail;
        private System.Windows.Forms.TextBox _tbLastName;
        private System.Windows.Forms.Label _lFirstName;
        private System.Windows.Forms.Label _lLastName;
        private System.Windows.Forms.Label _lEmail;
        private System.Windows.Forms.Label _lOrderId;
        private System.Windows.Forms.Label _lProduct;
        private System.Windows.Forms.TextBox _tbFirstName;
        private System.Windows.Forms.ComboBox _comboProduct;
        private System.Windows.Forms.Button _bShowSource;
        private System.Windows.Forms.Button _bShowTemplate;
    }
}
