namespace ActiveUp.Net.Samples.Template
{
    partial class WorkingWithListRowItem
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._tbOrderId = new System.Windows.Forms.TextBox();
            this._tbEmail = new System.Windows.Forms.TextBox();
            this._tbLastName = new System.Windows.Forms.TextBox();
            this._lFirstName = new System.Windows.Forms.Label();
            this._lLastName = new System.Windows.Forms.Label();
            this._lEmail = new System.Windows.Forms.Label();
            this._lOrderId = new System.Windows.Forms.Label();
            this._lProduct = new System.Windows.Forms.Label();
            this._lQuantity = new System.Windows.Forms.Label();
            this._tbFirstName = new System.Windows.Forms.TextBox();
            this._comboProduct = new System.Windows.Forms.ComboBox();
            this._nudQuantity = new System.Windows.Forms.NumericUpDown();
            this._bCancel = new System.Windows.Forms.Button();
            this._bOk = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nudQuantity)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this._lQuantity, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this._tbFirstName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._comboProduct, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this._nudQuantity, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 136);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _tbOrderId
            // 
            this._tbOrderId.Location = new System.Drawing.Point(85, 70);
            this._tbOrderId.Name = "_tbOrderId";
            this._tbOrderId.ReadOnly = true;
            this._tbOrderId.Size = new System.Drawing.Size(216, 20);
            this._tbOrderId.TabIndex = 7;
            // 
            // _tbEmail
            // 
            this._tbEmail.Location = new System.Drawing.Point(85, 48);
            this._tbEmail.Name = "_tbEmail";
            this._tbEmail.Size = new System.Drawing.Size(216, 20);
            this._tbEmail.TabIndex = 5;
            // 
            // _tbLastName
            // 
            this._tbLastName.Location = new System.Drawing.Point(85, 26);
            this._tbLastName.Name = "_tbLastName";
            this._tbLastName.Size = new System.Drawing.Size(216, 20);
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
            // _lQuantity
            // 
            this._lQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lQuantity.AutoSize = true;
            this._lQuantity.Location = new System.Drawing.Point(27, 112);
            this._lQuantity.Name = "_lQuantity";
            this._lQuantity.Size = new System.Drawing.Size(52, 24);
            this._lQuantity.TabIndex = 10;
            this._lQuantity.Text = "Quantity :";
            this._lQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _tbFirstName
            // 
            this._tbFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tbFirstName.Location = new System.Drawing.Point(85, 3);
            this._tbFirstName.Name = "_tbFirstName";
            this._tbFirstName.Size = new System.Drawing.Size(216, 20);
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
            this._comboProduct.Size = new System.Drawing.Size(216, 21);
            this._comboProduct.Sorted = true;
            this._comboProduct.TabIndex = 9;
            // 
            // _nudQuantity
            // 
            this._nudQuantity.Location = new System.Drawing.Point(85, 115);
            this._nudQuantity.Name = "_nudQuantity";
            this._nudQuantity.Size = new System.Drawing.Size(216, 20);
            this._nudQuantity.TabIndex = 11;
            this._nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _bCancel
            // 
            this._bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._bCancel.Location = new System.Drawing.Point(226, 142);
            this._bCancel.Name = "_bCancel";
            this._bCancel.Size = new System.Drawing.Size(75, 23);
            this._bCancel.TabIndex = 2;
            this._bCancel.Text = "Cancel";
            this._bCancel.UseVisualStyleBackColor = true;
            // 
            // _bOk
            // 
            this._bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._bOk.Location = new System.Drawing.Point(145, 142);
            this._bOk.Name = "_bOk";
            this._bOk.Size = new System.Drawing.Size(75, 23);
            this._bOk.TabIndex = 1;
            this._bOk.Text = "Add";
            this._bOk.UseVisualStyleBackColor = true;
            this._bOk.Click += new System.EventHandler(this._bOk_Click);
            // 
            // WorkingWithListRowItem
            // 
            this.AcceptButton = this._bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._bCancel;
            this.ClientSize = new System.Drawing.Size(304, 172);
            this.Controls.Add(this._bOk);
            this.Controls.Add(this._bCancel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkingWithListRowItem";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add order";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nudQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _lFirstName;
        private System.Windows.Forms.Label _lLastName;
        private System.Windows.Forms.Label _lEmail;
        private System.Windows.Forms.Label _lOrderId;
        private System.Windows.Forms.Label _lProduct;
        private System.Windows.Forms.Label _lQuantity;
        private System.Windows.Forms.TextBox _tbOrderId;
        private System.Windows.Forms.TextBox _tbEmail;
        private System.Windows.Forms.TextBox _tbLastName;
        private System.Windows.Forms.TextBox _tbFirstName;
        private System.Windows.Forms.ComboBox _comboProduct;
        private System.Windows.Forms.NumericUpDown _nudQuantity;
        private System.Windows.Forms.Button _bCancel;
        private System.Windows.Forms.Button _bOk;



    }
}