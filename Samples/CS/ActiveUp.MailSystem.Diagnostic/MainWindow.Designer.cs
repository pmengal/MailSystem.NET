namespace ActiveUp.MailSystem.Diagnostic
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.messagesExplorerListBox = new System.Windows.Forms.ListBox();
            this.messageDetailsTabControl = new System.Windows.Forms.TabControl();
            this.rfc822TabPage = new System.Windows.Forms.TabPage();
            this.messageRfc822RawTextbox = new System.Windows.Forms.TextBox();
            this.objectExplorerTabPage = new System.Windows.Forms.TabPage();
            this.messageDetailObjectExplorer = new System.Windows.Forms.PropertyGrid();
            this.messageExplorer = new System.Windows.Forms.TabPage();
            this.saveAttachmentsToDisk = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contentTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentDispositionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originalContentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headerFieldNamesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.headerFieldsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textContentTransferEncodedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.embeddedObjectLinkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.charsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentTransferEncodingDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.embeddedObjectContentIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textContentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.containerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.binaryContentDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.attachmentCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.loadMessagesFilesOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveAttachmentsToDiskDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textInput = new System.Windows.Forms.TextBox();
            this.textResult = new System.Windows.Forms.TextBox();
            this.textInputLabel = new System.Windows.Forms.Label();
            this.textOutputLabel = new System.Windows.Forms.Label();
            this.decodeButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.messageDetailsTabControl.SuspendLayout();
            this.rfc822TabPage.SuspendLayout();
            this.objectExplorerTabPage.SuspendLayout();
            this.messageExplorer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attachmentCollectionBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(846, 437);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(838, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "System Test";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.toolStrip1);
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(838, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Message Parser";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(832, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(112, 22);
            this.toolStripButton1.Text = "Load message file";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(124, 22);
            this.toolStripButton2.Text = "Load POP3 message";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(3, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.messagesExplorerListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.messageDetailsTabControl);
            this.splitContainer1.Size = new System.Drawing.Size(832, 377);
            this.splitContainer1.SplitterDistance = 251;
            this.splitContainer1.TabIndex = 2;
            // 
            // messagesExplorerListBox
            // 
            this.messagesExplorerListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesExplorerListBox.FormattingEnabled = true;
            this.messagesExplorerListBox.Location = new System.Drawing.Point(0, 0);
            this.messagesExplorerListBox.Name = "messagesExplorerListBox";
            this.messagesExplorerListBox.Size = new System.Drawing.Size(251, 368);
            this.messagesExplorerListBox.TabIndex = 0;
            this.messagesExplorerListBox.SelectedIndexChanged += new System.EventHandler(this.messagesExplorerListBox_SelectedIndexChanged);
            // 
            // messageDetailsTabControl
            // 
            this.messageDetailsTabControl.Controls.Add(this.rfc822TabPage);
            this.messageDetailsTabControl.Controls.Add(this.objectExplorerTabPage);
            this.messageDetailsTabControl.Controls.Add(this.messageExplorer);
            this.messageDetailsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageDetailsTabControl.Location = new System.Drawing.Point(0, 0);
            this.messageDetailsTabControl.Name = "messageDetailsTabControl";
            this.messageDetailsTabControl.SelectedIndex = 0;
            this.messageDetailsTabControl.Size = new System.Drawing.Size(577, 377);
            this.messageDetailsTabControl.TabIndex = 1;
            // 
            // rfc822TabPage
            // 
            this.rfc822TabPage.Controls.Add(this.messageRfc822RawTextbox);
            this.rfc822TabPage.Location = new System.Drawing.Point(4, 22);
            this.rfc822TabPage.Name = "rfc822TabPage";
            this.rfc822TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.rfc822TabPage.Size = new System.Drawing.Size(569, 351);
            this.rfc822TabPage.TabIndex = 0;
            this.rfc822TabPage.Text = "Rfc822";
            this.rfc822TabPage.UseVisualStyleBackColor = true;
            // 
            // messageRfc822RawTextbox
            // 
            this.messageRfc822RawTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageRfc822RawTextbox.Location = new System.Drawing.Point(3, 3);
            this.messageRfc822RawTextbox.Multiline = true;
            this.messageRfc822RawTextbox.Name = "messageRfc822RawTextbox";
            this.messageRfc822RawTextbox.Size = new System.Drawing.Size(563, 345);
            this.messageRfc822RawTextbox.TabIndex = 0;
            // 
            // objectExplorerTabPage
            // 
            this.objectExplorerTabPage.Controls.Add(this.messageDetailObjectExplorer);
            this.objectExplorerTabPage.Location = new System.Drawing.Point(4, 22);
            this.objectExplorerTabPage.Name = "objectExplorerTabPage";
            this.objectExplorerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.objectExplorerTabPage.Size = new System.Drawing.Size(569, 351);
            this.objectExplorerTabPage.TabIndex = 1;
            this.objectExplorerTabPage.Text = "Object Explorer";
            this.objectExplorerTabPage.UseVisualStyleBackColor = true;
            // 
            // messageDetailObjectExplorer
            // 
            this.messageDetailObjectExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageDetailObjectExplorer.Location = new System.Drawing.Point(3, 3);
            this.messageDetailObjectExplorer.Name = "messageDetailObjectExplorer";
            this.messageDetailObjectExplorer.Size = new System.Drawing.Size(563, 345);
            this.messageDetailObjectExplorer.TabIndex = 0;
            // 
            // messageExplorer
            // 
            this.messageExplorer.Controls.Add(this.saveAttachmentsToDisk);
            this.messageExplorer.Controls.Add(this.dataGridView1);
            this.messageExplorer.Location = new System.Drawing.Point(4, 22);
            this.messageExplorer.Name = "messageExplorer";
            this.messageExplorer.Size = new System.Drawing.Size(569, 351);
            this.messageExplorer.TabIndex = 2;
            this.messageExplorer.Text = "Message Explorer";
            this.messageExplorer.UseVisualStyleBackColor = true;
            // 
            // saveAttachmentsToDisk
            // 
            this.saveAttachmentsToDisk.Location = new System.Drawing.Point(4, 2);
            this.saveAttachmentsToDisk.Name = "saveAttachmentsToDisk";
            this.saveAttachmentsToDisk.Size = new System.Drawing.Size(75, 23);
            this.saveAttachmentsToDisk.TabIndex = 1;
            this.saveAttachmentsToDisk.Text = "Save to disk";
            this.saveAttachmentsToDisk.UseVisualStyleBackColor = true;
            this.saveAttachmentsToDisk.Click += new System.EventHandler(this.saveAttachmentsToDisk_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contentTypeDataGridViewTextBoxColumn,
            this.contentIdDataGridViewTextBoxColumn,
            this.contentDispositionDataGridViewTextBoxColumn,
            this.originalContentDataGridViewTextBoxColumn,
            this.headerFieldNamesDataGridViewTextBoxColumn,
            this.headerFieldsDataGridViewTextBoxColumn,
            this.contentNameDataGridViewTextBoxColumn,
            this.textContentTransferEncodedDataGridViewTextBoxColumn,
            this.contentLocationDataGridViewTextBoxColumn,
            this.embeddedObjectLinkDataGridViewTextBoxColumn,
            this.charsetDataGridViewTextBoxColumn,
            this.contentDescriptionDataGridViewTextBoxColumn,
            this.contentTransferEncodingDataGridViewTextBoxColumn,
            this.embeddedObjectContentIdDataGridViewTextBoxColumn,
            this.textContentDataGridViewTextBoxColumn,
            this.containerDataGridViewTextBoxColumn,
            this.binaryContentDataGridViewImageColumn});
            this.dataGridView1.DataSource = this.attachmentCollectionBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(3, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(563, 315);
            this.dataGridView1.TabIndex = 0;
            // 
            // contentTypeDataGridViewTextBoxColumn
            // 
            this.contentTypeDataGridViewTextBoxColumn.DataPropertyName = "ContentType";
            this.contentTypeDataGridViewTextBoxColumn.HeaderText = "ContentType";
            this.contentTypeDataGridViewTextBoxColumn.Name = "contentTypeDataGridViewTextBoxColumn";
            // 
            // contentIdDataGridViewTextBoxColumn
            // 
            this.contentIdDataGridViewTextBoxColumn.DataPropertyName = "ContentId";
            this.contentIdDataGridViewTextBoxColumn.HeaderText = "ContentId";
            this.contentIdDataGridViewTextBoxColumn.Name = "contentIdDataGridViewTextBoxColumn";
            // 
            // contentDispositionDataGridViewTextBoxColumn
            // 
            this.contentDispositionDataGridViewTextBoxColumn.DataPropertyName = "ContentDisposition";
            this.contentDispositionDataGridViewTextBoxColumn.HeaderText = "ContentDisposition";
            this.contentDispositionDataGridViewTextBoxColumn.Name = "contentDispositionDataGridViewTextBoxColumn";
            // 
            // originalContentDataGridViewTextBoxColumn
            // 
            this.originalContentDataGridViewTextBoxColumn.DataPropertyName = "OriginalContent";
            this.originalContentDataGridViewTextBoxColumn.HeaderText = "OriginalContent";
            this.originalContentDataGridViewTextBoxColumn.Name = "originalContentDataGridViewTextBoxColumn";
            // 
            // headerFieldNamesDataGridViewTextBoxColumn
            // 
            this.headerFieldNamesDataGridViewTextBoxColumn.DataPropertyName = "HeaderFieldNames";
            this.headerFieldNamesDataGridViewTextBoxColumn.HeaderText = "HeaderFieldNames";
            this.headerFieldNamesDataGridViewTextBoxColumn.Name = "headerFieldNamesDataGridViewTextBoxColumn";
            // 
            // headerFieldsDataGridViewTextBoxColumn
            // 
            this.headerFieldsDataGridViewTextBoxColumn.DataPropertyName = "HeaderFields";
            this.headerFieldsDataGridViewTextBoxColumn.HeaderText = "HeaderFields";
            this.headerFieldsDataGridViewTextBoxColumn.Name = "headerFieldsDataGridViewTextBoxColumn";
            // 
            // contentNameDataGridViewTextBoxColumn
            // 
            this.contentNameDataGridViewTextBoxColumn.DataPropertyName = "ContentName";
            this.contentNameDataGridViewTextBoxColumn.HeaderText = "ContentName";
            this.contentNameDataGridViewTextBoxColumn.Name = "contentNameDataGridViewTextBoxColumn";
            // 
            // textContentTransferEncodedDataGridViewTextBoxColumn
            // 
            this.textContentTransferEncodedDataGridViewTextBoxColumn.DataPropertyName = "TextContentTransferEncoded";
            this.textContentTransferEncodedDataGridViewTextBoxColumn.HeaderText = "TextContentTransferEncoded";
            this.textContentTransferEncodedDataGridViewTextBoxColumn.Name = "textContentTransferEncodedDataGridViewTextBoxColumn";
            this.textContentTransferEncodedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // contentLocationDataGridViewTextBoxColumn
            // 
            this.contentLocationDataGridViewTextBoxColumn.DataPropertyName = "ContentLocation";
            this.contentLocationDataGridViewTextBoxColumn.HeaderText = "ContentLocation";
            this.contentLocationDataGridViewTextBoxColumn.Name = "contentLocationDataGridViewTextBoxColumn";
            // 
            // embeddedObjectLinkDataGridViewTextBoxColumn
            // 
            this.embeddedObjectLinkDataGridViewTextBoxColumn.DataPropertyName = "EmbeddedObjectLink";
            this.embeddedObjectLinkDataGridViewTextBoxColumn.HeaderText = "EmbeddedObjectLink";
            this.embeddedObjectLinkDataGridViewTextBoxColumn.Name = "embeddedObjectLinkDataGridViewTextBoxColumn";
            this.embeddedObjectLinkDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // charsetDataGridViewTextBoxColumn
            // 
            this.charsetDataGridViewTextBoxColumn.DataPropertyName = "Charset";
            this.charsetDataGridViewTextBoxColumn.HeaderText = "Charset";
            this.charsetDataGridViewTextBoxColumn.Name = "charsetDataGridViewTextBoxColumn";
            // 
            // contentDescriptionDataGridViewTextBoxColumn
            // 
            this.contentDescriptionDataGridViewTextBoxColumn.DataPropertyName = "ContentDescription";
            this.contentDescriptionDataGridViewTextBoxColumn.HeaderText = "ContentDescription";
            this.contentDescriptionDataGridViewTextBoxColumn.Name = "contentDescriptionDataGridViewTextBoxColumn";
            // 
            // contentTransferEncodingDataGridViewTextBoxColumn
            // 
            this.contentTransferEncodingDataGridViewTextBoxColumn.DataPropertyName = "ContentTransferEncoding";
            this.contentTransferEncodingDataGridViewTextBoxColumn.HeaderText = "ContentTransferEncoding";
            this.contentTransferEncodingDataGridViewTextBoxColumn.Name = "contentTransferEncodingDataGridViewTextBoxColumn";
            // 
            // embeddedObjectContentIdDataGridViewTextBoxColumn
            // 
            this.embeddedObjectContentIdDataGridViewTextBoxColumn.DataPropertyName = "EmbeddedObjectContentId";
            this.embeddedObjectContentIdDataGridViewTextBoxColumn.HeaderText = "EmbeddedObjectContentId";
            this.embeddedObjectContentIdDataGridViewTextBoxColumn.Name = "embeddedObjectContentIdDataGridViewTextBoxColumn";
            this.embeddedObjectContentIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // textContentDataGridViewTextBoxColumn
            // 
            this.textContentDataGridViewTextBoxColumn.DataPropertyName = "TextContent";
            this.textContentDataGridViewTextBoxColumn.HeaderText = "TextContent";
            this.textContentDataGridViewTextBoxColumn.Name = "textContentDataGridViewTextBoxColumn";
            // 
            // containerDataGridViewTextBoxColumn
            // 
            this.containerDataGridViewTextBoxColumn.DataPropertyName = "Container";
            this.containerDataGridViewTextBoxColumn.HeaderText = "Container";
            this.containerDataGridViewTextBoxColumn.Name = "containerDataGridViewTextBoxColumn";
            // 
            // binaryContentDataGridViewImageColumn
            // 
            this.binaryContentDataGridViewImageColumn.DataPropertyName = "BinaryContent";
            this.binaryContentDataGridViewImageColumn.HeaderText = "BinaryContent";
            this.binaryContentDataGridViewImageColumn.Name = "binaryContentDataGridViewImageColumn";
            // 
            // attachmentCollectionBindingSource
            // 
            this.attachmentCollectionBindingSource.DataSource = typeof(ActiveUp.Net.Mail.AttachmentCollection);
            // 
            // loadMessagesFilesOpenFileDialog
            // 
            this.loadMessagesFilesOpenFileDialog.FileName = "*.eml";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.decodeButton);
            this.tabPage3.Controls.Add(this.textOutputLabel);
            this.tabPage3.Controls.Add(this.textInputLabel);
            this.tabPage3.Controls.Add(this.textResult);
            this.tabPage3.Controls.Add(this.textInput);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(838, 411);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Encoder/Decoder";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textInput
            // 
            this.textInput.Location = new System.Drawing.Point(8, 25);
            this.textInput.Multiline = true;
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(824, 132);
            this.textInput.TabIndex = 0;
            // 
            // textResult
            // 
            this.textResult.Location = new System.Drawing.Point(8, 194);
            this.textResult.Multiline = true;
            this.textResult.Name = "textResult";
            this.textResult.Size = new System.Drawing.Size(822, 209);
            this.textResult.TabIndex = 1;
            // 
            // textInputLabel
            // 
            this.textInputLabel.AutoSize = true;
            this.textInputLabel.Location = new System.Drawing.Point(175, 7);
            this.textInputLabel.Name = "textInputLabel";
            this.textInputLabel.Size = new System.Drawing.Size(31, 13);
            this.textInputLabel.TabIndex = 2;
            this.textInputLabel.Text = "Input";
            // 
            // textOutputLabel
            // 
            this.textOutputLabel.AutoSize = true;
            this.textOutputLabel.Location = new System.Drawing.Point(175, 178);
            this.textOutputLabel.Name = "textOutputLabel";
            this.textOutputLabel.Size = new System.Drawing.Size(39, 13);
            this.textOutputLabel.TabIndex = 3;
            this.textOutputLabel.Text = "Output";
            // 
            // decodeButton
            // 
            this.decodeButton.Location = new System.Drawing.Point(417, 165);
            this.decodeButton.Name = "decodeButton";
            this.decodeButton.Size = new System.Drawing.Size(75, 23);
            this.decodeButton.TabIndex = 4;
            this.decodeButton.Text = "Decode";
            this.decodeButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 437);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainWindow";
            this.Text = "MailSystem Diagnostic";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.messageDetailsTabControl.ResumeLayout(false);
            this.rfc822TabPage.ResumeLayout(false);
            this.rfc822TabPage.PerformLayout();
            this.objectExplorerTabPage.ResumeLayout(false);
            this.messageExplorer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attachmentCollectionBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox messagesExplorerListBox;
        private System.Windows.Forms.TextBox messageRfc822RawTextbox;
        private System.Windows.Forms.OpenFileDialog loadMessagesFilesOpenFileDialog;
        private System.Windows.Forms.TabControl messageDetailsTabControl;
        private System.Windows.Forms.TabPage rfc822TabPage;
        private System.Windows.Forms.TabPage objectExplorerTabPage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.PropertyGrid messageDetailObjectExplorer;
        private System.Windows.Forms.TabPage messageExplorer;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentDispositionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originalContentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn headerFieldNamesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn headerFieldsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textContentTransferEncodedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentLocationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn embeddedObjectLinkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn charsetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentTransferEncodingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn embeddedObjectContentIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textContentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn containerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn binaryContentDataGridViewImageColumn;
        private System.Windows.Forms.BindingSource attachmentCollectionBindingSource;
        private System.Windows.Forms.Button saveAttachmentsToDisk;
        private System.Windows.Forms.SaveFileDialog saveAttachmentsToDiskDialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button decodeButton;
        private System.Windows.Forms.Label textOutputLabel;
        private System.Windows.Forms.Label textInputLabel;
        private System.Windows.Forms.TextBox textResult;
        private System.Windows.Forms.TextBox textInput;
    }
}

