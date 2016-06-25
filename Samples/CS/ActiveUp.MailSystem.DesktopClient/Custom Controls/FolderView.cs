#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;
using ActiveUp.MailSystem.DesktopClient.Folder;

#endregion

namespace ActiveUp.MailSystem.DesktopClient
{
	public class FolderView : System.Windows.Forms.UserControl
	{
		private ImageList	folderImages;
		private TreeView	folderTreeView;
		private IContainer	components;

		private	Font		_font;
		private	Font		_boldFont;

		MessageStore		_store = null;
		private int			_deletedCount;
		private int			_unreadCount;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem newFolderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
		private int			_draftsCount;

		public FolderView()
		{
			// Set the Font
			SetFont();

			// Initialize
			this.InitializeComponent();

			// Set Node Fonts
			SetNodeFonts(this.folderTreeView.Nodes);

			// Set dock to top
			this.Dock = DockStyle.Fill;

            // load the tree.
            this.LoadFolders();
		}

		#region Designer Code
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderView));
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
            this.folderImages = new System.Windows.Forms.ImageList(this.components);
            this.folderTreeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.folderTreeView.AllowDrop = true;
            this.folderTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.folderTreeView.ContextMenuStrip = this.contextMenuStrip;
            this.folderTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTreeView.HideSelection = false;
            this.folderTreeView.ImageIndex = 0;
            this.folderTreeView.ImageList = this.folderImages;
            this.folderTreeView.LabelEdit = true;
            this.folderTreeView.Location = new System.Drawing.Point(0, 4);
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
            this.folderTreeView.Size = new System.Drawing.Size(281, 274);
            this.folderTreeView.TabIndex = 0;
            this.folderTreeView.TabStop = false;
            this.folderTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.folderTreeView_DragDrop);
            this.folderTreeView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.folderTreeView_DrawNode);
            this.folderTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.folderTreeView_AfterSelect);
            this.folderTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.folderTreeView_AfterLabelEdit);
            this.folderTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.folderTreeView_DragEnter);
            this.folderTreeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.folderTreeView_BeforeLabelEdit);
            this.folderTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.folderTreeView_ItemDrag);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(140, 76);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Folder;
            this.newFolderToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Drafts;
            this.renameToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Delete;
            this.deleteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // FolderView
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.folderTreeView);
            this.Name = "FolderView";
            this.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.Size = new System.Drawing.Size(281, 278);
            this.Load += new System.EventHandler(this.FolderView_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		int UnreadCount
		{
			get { return _unreadCount; }
			set
			{
				if (value != _unreadCount)
				{
					_unreadCount = value;
					this.folderTreeView.Invalidate();
				}
			}
		}

		int DraftsCount
		{
			get { return _draftsCount; }
			set
			{
				if (value != _draftsCount)
				{
					_draftsCount = value;
					this.folderTreeView.Invalidate();
				}
			}
		}

		int DeletedCount
		{
			get { return _deletedCount; }
			set
			{
				if (value != _deletedCount)
				{
					_deletedCount = value;
					this.folderTreeView.Invalidate();
				}
			}
		}
		#endregion

		#region TreeView Drawing
		private void DrawAnnotatedText(DrawTreeNodeEventArgs e, int count, Color exColor, string left, string right)
		{
			// Setup
			Font	font = ((0 == count) ? this._font : this._boldFont);
			Color	foreColor = (((e.Node.IsSelected) && (e.Node.TreeView.Focused)) ? SystemColors.HighlightText : e.Node.ForeColor);

			// Draw Text
			TextRenderer.DrawText(e.Graphics, e.Node.Text, font, new Point(e.Bounds.X, e.Bounds.Y), foreColor);

			// Draw count in regular font
			if (0 != count)
			{
				string exText = left + count.ToString() + right;
				int width = TextRenderer.MeasureText((e.Node.Text + "  "), font).Width;

				TextRenderer.DrawText(e.Graphics, exText, _font, new Point(e.Bounds.X + width, e.Bounds.Y), exColor);
			}
		}

		private void folderTreeView_DrawNode(object sender, System.Windows.Forms.DrawTreeNodeEventArgs e)
		{
			// Draw default by default
			e.DrawDefault = true;
			if (e.Node.Text.Contains("Deleted"))
			{
				// Draw Deleted
				DrawAnnotatedText(e, this.DeletedCount, Color.Blue, "(", ")");

				// Turn off drawing
				e.DrawDefault = false;
			}
			else if (e.Node.Text.Contains("Inbox"))
			{
				// Draw Inbox
				DrawAnnotatedText(e, this.UnreadCount, Color.Green, "[", "]");

				// Turn off drawing
				e.DrawDefault = false;
			}
			else if (e.Node.Text.Contains("Drafts"))
			{
				// Draw Drafts
				DrawAnnotatedText(e, this.DraftsCount, Color.Blue, "(", ")");

				// Turn off drawing
				e.DrawDefault = false;
			}
		}

		private void SetFont()
		{
			_font = SystemFonts.IconTitleFont;
			_boldFont = new Font(_font, FontStyle.Bold);

			this.Font = _boldFont;
		}

		private void SetNodeFonts(TreeNodeCollection nodes)
		{
			this.folderTreeView.Font = _boldFont;

			foreach (TreeNode node in nodes)
			{
				if (!(node.Tag is string) || ((node.Tag as string) != "Bold"))
				{
					// Set Node font
					node.NodeFont = _font;
				}

				// Set child fonts
				if (null != node.Nodes)
				{
					SetNodeFonts(node.Nodes);
				}
			}
		}
		#endregion

		#region Event Handlers
		private void FolderView_Load(object sender, EventArgs e)
		{
			// Set Owner Draw Mode to Text
			if ((null == this.Site) || (!this.Site.DesignMode))
			{
				this.folderTreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
			}

			// Set Selected Item
			foreach (TreeNode node in this.folderTreeView.Nodes[0].Nodes)
			{
				if (node.Text.Contains("Inbox"))
				{
					this.folderTreeView.SelectedNode = node;
					node.Expand();
					break;
				}
			}

            // Attach to the MessageStore
            this._store = Facade.GetInstance().GetMessageStore();

            this._unreadCount = _store.UnreadCount;
            this._draftsCount = _store.DraftsCount;
            this._deletedCount = _store.DeletedCount;

            // Check for changes
            this._store.PropertyChanged += new PropertyChangedEventHandler(MessageStore_PropertyChanged);
		}

		void MessageStore_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "UnreadCount")
			{
				this.UnreadCount = _store.UnreadCount;
			}
			else if (e.PropertyName == "DraftsCount")
			{
				this.DraftsCount = _store.DraftsCount;
			}
			else if (e.PropertyName == "DeletedCount")
			{
				this.DeletedCount = _store.DeletedCount;
			}
		}

        /// <summary>
        /// Event for tree view after select.
        /// </summary>
        /// <param name="sender">The object sender.</param>
        /// <param name="e">The event arguments.</param>
        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MainForm mainForm = (MainForm)this.ParentForm;

            if (e.Node.Text.Equals(Constants.Unread))
            {
                mainForm.LoadMessages(MessageListType.Unread);
            }
            else if (e.Node.Text.Equals(Constants.Read))
            {
                mainForm.LoadMessages(MessageListType.Read);
            }
            else if (e.Node.Text.Equals(Constants.Inbox))
            {
                mainForm.LoadMessages(MessageListType.Inbox);
            }
            else if (e.Node.Text.Equals(Constants.SentItems))
            {
                mainForm.LoadMessages(MessageListType.SentItems);
            }
            else if (e.Node.Text.Equals(Constants.Outbox))
            {
                mainForm.LoadMessages(MessageListType.Outbox);
            }
            else if (e.Node.Text.Equals(Constants.DeletedItems))
            {
                mainForm.LoadMessages(MessageListType.DeletedItems);
            }
            else if (e.Node.Text.Equals(Constants.Drafts))
            {
                mainForm.LoadMessages(MessageListType.Drafts);
            }
            else if (e.Node.Text.Equals(Constants.JunkEmail))
            {
                mainForm.LoadMessages(MessageListType.JunkEmail);
            }
            else if (e.Node.Text.Equals(Constants.SyncFailures))
            {
                mainForm.LoadMessages(MessageListType.SyncFailure);
            }
            else if (e.Node.Text.Equals(Constants.SearchFolders))
            {
                mainForm.LoadMessages(MessageListType.SearchFolders);
            }
            else if (e.Node.Text.Equals(Constants.RootFolder))
            {
                mainForm.LoadMessages(MessageListType.RootFolder);
            }
            else if (e.Node.Text.Equals(Constants.ArquiveFolders))
            {
                mainForm.LoadMessages(MessageListType.ArchiveFolder);
            }
            else if (e.Node.Text.Equals(Constants.Empty))
            {
                mainForm.LoadMessages(MessageListType.Empty);
            }
            else if (e.Node.Text.Equals(Constants.MailHome))
            {
                mainForm.LoadMessages(MessageListType.MailHome);
            }
            else
            {
                mainForm.LoadMessages(MessageListType.Custom);
            }

            MainForm.GetInstance().UpdateMessageCountLabel();
        }

        /// <summary>
        /// After label edit event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void folderTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Facade facade = Facade.GetInstance();
            MessageStore messageStore = facade.GetMessageStoreCustom();
            foreach (MailMessage mail in messageStore.Messages)
            {
                if (mail.ParentFolder.Equals(e.Node.Text))
                {
                    mail.ParentFolder = e.Label;
                }
            }

            e.Node.Text = e.Label;
            this.folderTreeView_AfterSelect(this.folderTreeView, new TreeViewEventArgs(e.Node));
            this.SaveFolders();
        }

        /// <summary>
        /// Method for get the selected mailbox.
        /// </summary>
        /// <returns>The current mailbox.</returns>
        public string GetSelectedMailbox()
        {
            string ret = Constants.Inbox;

            if (this.folderTreeView.SelectedNode != null &&
                this.folderTreeView.SelectedNode.Text.Equals(Constants.SentItems))
            {
                ret = Constants.SentItems;
            }
            else if (this.folderTreeView.SelectedNode != null &&
                this.folderTreeView.SelectedNode.Text.Equals(Constants.DeletedItems))
            {
                ret = Constants.DeletedItems;
            }

            return ret;
        }

        /// <summary>
        /// Method for get the selected folder.
        /// </summary>
        /// <returns>The current selected folder.</returns>
        public string GetSelectedFolder()
        {
            string ret = string.Empty;

            if (this.folderTreeView.SelectedNode != null)
            {
                ret = this.folderTreeView.SelectedNode.Text;
            }

            return ret;
        }

        /// <summary>
        /// Method for create new folder.
        /// </summary>
        /// <param name="folderParent">The parent folder name.</param>
        /// <param name="folderName">The folder name.</param>
        public void CreateFolder(string parentFolder, string folderName)
        {
            foreach (TreeNode node in this.folderTreeView.Nodes)
            {
                this.CreateFolderRecursive(node, parentFolder, folderName);
            }

            this.SaveFolders();
        }

        /// <summary>
        /// Recursive method for create new folder.
        /// </summary>
        /// <param name="parent">The parent tree node.</param>
        /// <param name="parentFolder">The parent folder name.</param>
        /// <param name="folderName">The folder name.</param>
        private void CreateFolderRecursive(TreeNode parent, string parentFolder, string folderName)
        {
            if (parent.Text.Contains(parentFolder))
            {
                TreeNode treeNode = new TreeNode(folderName, 7, 7);
                treeNode.ImageKey = "Folder.bmp";
                treeNode.SelectedImageKey = "Folder.bmp";
                parent.Nodes.Add(treeNode);
            }
            else
            {
                foreach (TreeNode node in parent.Nodes)
                {
                    this.CreateFolderRecursive(node, parentFolder, folderName);
                }
            }
        }

        /// <summary>
        /// Method for load the folders in tree view.
        /// </summary>
        public void LoadFolders()
        {
            FolderUtil.LoadTree(this.folderTreeView);
        }

        /// <summary>
        /// Method for save the folders in tree view.
        /// </summary>
        public void SaveFolders()
        {
            FolderUtil.SaveTree(this.folderTreeView);
        }

        /// <summary>
        /// Method for delete the selected folder.
        /// </summary>
        public void DeleteSelectedFolder()
        {
            TreeNode node = this.folderTreeView.SelectedNode;
            if (node != null && node.ImageKey.Equals("Folder.bmp"))
            {
                this.DeleteNodesRecursive(node);
                this.SaveFolders();
            }
        }

        /// <summary>
        /// Method for delete the tree nodes recursively.
        /// </summary>
        /// <param name="node">The tree node for delete.</param>
        public void DeleteNodesRecursive(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                this.DeleteNodesRecursive(child);
            }

            Facade facade = Facade.GetInstance();
            MessageStore messageStore = facade.GetMessageStoreCustom();
            for (int i = 0; i < messageStore.Messages.Count; i++)
            {
                MailMessage mail = messageStore.Messages[i];
                if (mail.ParentFolder.Equals(node.Text))
                {
                    messageStore.Messages.Remove(mail);
                }
            }

            node.Remove();
        }

        /// <summary>
        /// Method for edit the selected folder.
        /// </summary>
        public void EditSelectedFolder()
        {
            TreeNode node = this.folderTreeView.SelectedNode;
            if (node != null && node.ImageKey.Equals("Folder.bmp"))
            {
                node.BeginEdit();
            }
        }

        #endregion

        /// <summary>
        /// Rename folder event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.EditSelectedFolder();
        }

        /// <summary>
        /// Delete folder event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeleteSelectedFolder();
        }

        /// <summary>
        /// Event handler for new folder tool strip menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GetInstance().newFolderToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Event handler for before tree node label edit.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void folderTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            TreeNode node = this.folderTreeView.SelectedNode;
            if (node != null && !node.ImageKey.Equals("Folder.bmp"))
            {
                e.CancelEdit = true;
            }
        }

        /// <summary>
        /// Item drag event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void folderTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is TreeNode && ((TreeNode)e.Item).ImageKey.Equals("Folder.bmp"))
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Drag enter event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void folderTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Drag drop event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void folderTreeView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = ((TreeView)sender).GetNodeAt(pt);
                TreeNode draggedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
                {
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);

                    this.SaveFolders();
                }
            }
        }

        /// <summary>
        /// Determine whether one node is a parent or ancestor of a second node.
        /// </summary>
        /// <param name="node1">The tree node #1.</param>
        /// <param name="node2">The tree node #2.</param>
        /// <returns>The contains result as a boolean.</returns>
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            // Check the parent node of the second node.
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;

            // If the parent node is not null or equal to the first node, 
            // call the ContainsNode method recursively using the parent of 
            // the second node.
            return ContainsNode(node1, node2.Parent);
        }
    }
}
