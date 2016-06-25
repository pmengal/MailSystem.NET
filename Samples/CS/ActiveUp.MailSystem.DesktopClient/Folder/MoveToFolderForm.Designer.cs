namespace ActiveUp.MailSystem.DesktopClient.Folder
{
    partial class MoveToFolderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveToFolderForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Deleted Items");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Read");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Unread");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Drafts");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Inbox");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Junk E-mail");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Outbox");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Sent Items");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Sync Failures");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Root Folder");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Search Folders", new System.Windows.Forms.TreeNode[] {
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Mail Home", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Empty");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Archive Folders", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            this.lblName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderImages = new System.Windows.Forms.ImageList(this.components);
            this.folderTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(179, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Move the selected item to the folder:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(118, 270);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(199, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // folderImages
            // 
            this.folderImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("folderImages.ImageStream")));
            this.folderImages.TransparentColor = System.Drawing.Color.Transparent;
            this.folderImages.Images.SetKeyName(0, "Task.bmp");
            this.folderImages.Images.SetKeyName(1, "Address.bmp");
            this.folderImages.Images.SetKeyName(2, "Archive.bmp");
            this.folderImages.Images.SetKeyName(3, "Delete.bmp");
            this.folderImages.Images.SetKeyName(4, "Drafts.bmp");
            this.folderImages.Images.SetKeyName(5, "Error.bmp");
            this.folderImages.Images.SetKeyName(6, "Find.bmp");
            this.folderImages.Images.SetKeyName(7, "Folder.bmp");
            this.folderImages.Images.SetKeyName(8, "Forward.bmp");
            this.folderImages.Images.SetKeyName(9, "Help.bmp");
            this.folderImages.Images.SetKeyName(10, "Inbox.bmp");
            this.folderImages.Images.SetKeyName(11, "Junk.bmp");
            this.folderImages.Images.SetKeyName(12, "Move.bmp");
            this.folderImages.Images.SetKeyName(13, "New.bmp");
            this.folderImages.Images.SetKeyName(14, "Notes.bmp");
            this.folderImages.Images.SetKeyName(15, "Outbox.bmp");
            this.folderImages.Images.SetKeyName(16, "Outlook.bmp");
            this.folderImages.Images.SetKeyName(17, "Post.bmp");
            this.folderImages.Images.SetKeyName(18, "Print.bmp");
            this.folderImages.Images.SetKeyName(19, "Read.bmp");
            this.folderImages.Images.SetKeyName(20, "Send.bmp");
            this.folderImages.Images.SetKeyName(21, "Sent.bmp");
            this.folderImages.Images.SetKeyName(22, "OutlookToday.bmp");
            this.folderImages.Images.SetKeyName(23, "Recycle.bmp");
            this.folderImages.Images.SetKeyName(24, "Search.bmp");
            this.folderImages.Images.SetKeyName(25, "Unread.gif");
            // 
            // folderTreeView
            // 
            this.folderTreeView.HideSelection = false;
            this.folderTreeView.ImageIndex = 0;
            this.folderTreeView.ImageList = this.folderImages;
            this.folderTreeView.Location = new System.Drawing.Point(15, 48);
            this.folderTreeView.Name = "folderTreeView";
            treeNode1.ImageKey = "Recycle.bmp";
            treeNode1.Name = "Node1";
            treeNode1.SelectedImageKey = "Recycle.bmp";
            treeNode1.Tag = "Bold";
            treeNode1.Text = "Deleted Items";
            treeNode2.ImageKey = "Read.bmp";
            treeNode2.Name = "NodeRead";
            treeNode2.SelectedImageKey = "Read.bmp";
            treeNode2.Text = "Read";
            treeNode3.ImageKey = "Unread.gif";
            treeNode3.Name = "NodeUnread";
            treeNode3.SelectedImageKey = "Unread.gif";
            treeNode3.Text = "Unread";
            treeNode4.ImageKey = "Drafts.bmp";
            treeNode4.Name = "Node2";
            treeNode4.SelectedImageKey = "Drafts.bmp";
            treeNode4.Tag = "Bold";
            treeNode4.Text = "Drafts";
            treeNode5.ImageKey = "Inbox.bmp";
            treeNode5.Name = "Node3";
            treeNode5.SelectedImageKey = "Inbox.bmp";
            treeNode5.Tag = "Bold";
            treeNode5.Text = "Inbox";
            treeNode6.ImageKey = "Junk.bmp";
            treeNode6.Name = "Node4";
            treeNode6.SelectedImageKey = "Junk.bmp";
            treeNode6.Text = "Junk E-mail";
            treeNode7.ImageKey = "Outbox.bmp";
            treeNode7.Name = "Node5";
            treeNode7.SelectedImageKey = "Outbox.bmp";
            treeNode7.Text = "Outbox";
            treeNode8.ImageKey = "Sent.bmp";
            treeNode8.Name = "Node6";
            treeNode8.SelectedImageKey = "Sent.bmp";
            treeNode8.Text = "Sent Items";
            treeNode9.ImageKey = "Forward.bmp";
            treeNode9.Name = "Node8";
            treeNode9.SelectedImageKey = "Forward.bmp";
            treeNode9.Text = "Sync Failures";
            treeNode10.ImageKey = "Folder.bmp";
            treeNode10.Name = "Node15";
            treeNode10.Text = "Root Folder";
            treeNode11.ImageKey = "Search.bmp";
            treeNode11.Name = "Node9";
            treeNode11.SelectedImageKey = "Search.bmp";
            treeNode11.Text = "Search Folders";
            treeNode12.ImageKey = "OutlookToday.bmp";
            treeNode12.Name = "Node0";
            treeNode12.SelectedImageKey = "OutlookToday.bmp";
            treeNode12.Text = "Mail Home";
            treeNode13.ImageKey = "Folder.bmp";
            treeNode13.Name = "Node17";
            treeNode13.SelectedImageKey = "Folder.bmp";
            treeNode13.Text = "Empty";
            treeNode14.ImageKey = "Archive.bmp";
            treeNode14.Name = "Node16";
            treeNode14.SelectedImageKey = "Archive.bmp";
            treeNode14.Text = "Archive Folders";
            this.folderTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode14});
            this.folderTreeView.SelectedImageIndex = 0;
            this.folderTreeView.Size = new System.Drawing.Size(259, 203);
            this.folderTreeView.TabIndex = 6;
            this.folderTreeView.TabStop = false;
            // 
            // MoveToFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 311);
            this.Controls.Add(this.folderTreeView);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveToFolderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Move Items";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList folderImages;
        private System.Windows.Forms.TreeView folderTreeView;
    }
}