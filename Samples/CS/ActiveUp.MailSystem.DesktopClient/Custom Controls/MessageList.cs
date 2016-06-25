#region Using directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using ActiveUp.Net.Mail;
using System.IO;
using System.Xml.Serialization;

#endregion

namespace ActiveUp.MailSystem.DesktopClient
{

    public enum MessageListType
    {
        Inbox, Unread, Read, SentItems, DeletedItems, Drafts, JunkEmail, Outbox, SyncFailure,
        SearchFolders, RootFolder, ArchiveFolder, Empty, MailHome, Custom
    }

    /// <summary>
    /// This class represents a control for mail message list.
    /// </summary>
    public class MessageList : System.Windows.Forms.UserControl
	{
		private Bitmap _readImage;
		private Bitmap _unreadImage;
		private MessageStore _store;
		private	Font _font;

		private IContainer components;
		private BindingSource messageBS;
        private DataGridViewImageColumn SentColumn;
        private DataGridViewTextBoxColumn To;
        private DataGridViewTextBoxColumn SentDate;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem markAsUnreadToolStripMenuItem;
        private ToolStripMenuItem markAsReadToolStripMenuItem;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem replyToolStripMenuItem;
        private ToolStripMenuItem replyToAllToolStripMenuItem;
        private ToolStripMenuItem forwardToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem moveToFolderToolStripMenuItem;
		private DataGridView dataGridView;

        /// <summary>
        /// Constructor.
        /// </summary>
		public MessageList()
		{
			// Initialize UI
			InitializeComponent();
		}

        #region Methods
        
        /// <summary>
        /// Method for get the visible rows count.
        /// </summary>
        /// <returns>The number of visible rows.</returns>
        public int GetVisibleRowsCount()
        {
            int ret = 0;
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                if (row.Visible)
                {
                    ret++;
                }
            }
            return ret;
        }

        /// <summary>
        /// Method for remove the selected mail message.
        /// </summary>
        public void DeleteAllMessages()
        {
            Facade facade = Facade.GetInstance();

            DataGridViewRowCollection rows = this.dataGridView.Rows;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                MailMessage mailMessage = (MailMessage)rows[i].DataBoundItem;
                facade.DeleteMessage(mailMessage, Constants.Inbox);
                this.dataGridView.Rows.Remove(rows[i]);
            }
        }

        /// <summary>
        /// Method for remove the selected mail message.
        /// </summary>
        public void DeleteSelectedMessage()
        {
            Facade facade = Facade.GetInstance();
            string mailbox = MainForm.GetInstance().GetSelectedMailbox();

            DataGridViewSelectedRowCollection rows = this.dataGridView.SelectedRows;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                MailMessage mailMessage = (MailMessage)rows[i].DataBoundItem;
                facade.DeleteMessage(mailMessage, mailbox);
                this.dataGridView.Rows.Remove(rows[i]);
            }

            this.Refresh();
        }

        /// <summary>
        /// Method for remove the selected mail message for move it.
        /// </summary>
        public void DeleteSelectedMessageForMove()
        {
            DataGridViewSelectedRowCollection rows = this.dataGridView.SelectedRows;
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                MailMessage mailMessage = (MailMessage)rows[i].DataBoundItem;
                this.dataGridView.Rows.Remove(rows[i]);
            }
            
            this.Refresh();
        }

        /// <summary>
        /// Method for filter the messages.
        /// </summary>
        /// <param name="findSettings">The filter settings.</param>
        public void FilterMessages(FindSettings findSettings)
        {
            this.dataGridView.CurrentCell = null;

            CurrencyManager cm = (CurrencyManager)this.dataGridView.BindingContext[this.dataGridView.DataSource];
            cm.SuspendBinding();

            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                MailMessage mailMessage = (MailMessage)row.DataBoundItem;

                if (findSettings.IsMessageFiltered(mailMessage))
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
            }

            cm.ResumeBinding();

            this.Invalidate(true);
        }

        /// <summary>
        /// Method for mark the messages as read or unread. 
        /// </summary>
        /// <param name="read">The read boolean.</param>
        public void MarkMessageAs(bool read)
        {
            DataGridViewSelectedRowCollection rows = this.dataGridView.SelectedRows;
            if (rows.Count > 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    MailMessage mailMessage = (MailMessage)row.DataBoundItem;

                    if (mailMessage.Read != read)
                    {
                        mailMessage.Read = read;

                        if (read)
                        {
                            this._store.UnreadCount--;
                        }
                        else
                        {
                            this._store.UnreadCount++;
                        }
                    }
                }
                this.Invalidate(true);
            }
        }

        /// <summary>
        /// Method for mark all the messages as read or unread. 
        /// </summary>
        /// <param name="read">The read boolean.</param>
        public void MarkAllMessageAs(bool read)
        {
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                MailMessage mailMessage = (MailMessage)row.DataBoundItem;
                mailMessage.Read = read;
            }

            int count = 0;
            if (!read)
            {
                count = this.dataGridView.Rows.Count;
            }

            this._store.UnreadCount = count;
            this.Invalidate(true);
        }

        /// <summary>
        /// Method for select all messages.
        /// </summary>
        public void SelectAllMessages()
        {
            this.dataGridView.SelectAll();
        }

        /// <summary>
        /// Method for load messages.
        /// </summary>
        /// <param name="messageListType">The Message List Type.</param>
        public void LoadMessages(MessageListType messageListType)
        {
            Facade facade = Facade.GetInstance();
            string selectedFolder = string.Empty;

            if (messageListType == MessageListType.Inbox ||
                messageListType == MessageListType.Unread ||
                messageListType == MessageListType.Read)
            {
                // Attach to Message Store
                this._store = facade.GetMessageStore();
            }
            else if (messageListType == MessageListType.SentItems)
            {
                // Attach to Message Store Sent items
                this._store = facade.GetMessageStoreSent();
            }
            else if (messageListType == MessageListType.DeletedItems)
            {
                // Attach to Message Store Sent items
                this._store = facade.GetMessageStoreDelete();
            }
            else if (messageListType == MessageListType.Custom)
            {
                // Attach to Message Store Sent items
                this._store = facade.GetMessageStoreCustom();
                selectedFolder = MainForm.GetInstance().GetSelectedFolder();
            }
            else
            {
                this._store = new MessageStore();
            }

            // Reset DataSource
            this.messageBS.DataSource = this._store.Messages;
            this.dataGridView.DataSource = this.messageBS;

            this.dataGridView.CurrentCell = null;

            CurrencyManager cm = (CurrencyManager)this.dataGridView.BindingContext[this.dataGridView.DataSource];
            cm.SuspendBinding();

            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                MailMessage mailMessage = (MailMessage)row.DataBoundItem;

                if (messageListType == MessageListType.Unread && mailMessage.Read)
                {
                    row.Visible = false;
                }
                else if (messageListType == MessageListType.Read && !mailMessage.Read)
                {
                    row.Visible = false;
                }
                else if (messageListType == MessageListType.Custom && 
                    !mailMessage.ParentFolder.Equals(selectedFolder))
                {
                    row.Visible = false;
                }
                else
                {
                    row.Visible = true;
                }
            }

            cm.ResumeBinding();

            if (this.messageBS.Count > 0)
            {
                this.dataGridView.Sort(this.dataGridView.Columns[2], ListSortDirection.Descending);
            }

            MainForm mainForm = this.ParentForm as MainForm;
            mainForm.LoadSelectedMessage(this._store);

            this.Invalidate(true);
        }

        #endregion

		#region Event Handlers
		private void MessageList_Load(object sender, EventArgs e)
		{
			// Auto dock fill
			this.Dock = DockStyle.Fill;

			// Get images
			_readImage = Properties.Resources.Read;
			_readImage.MakeTransparent(Color.FromArgb(238, 238, 238));
			_unreadImage = Properties.Resources.Unread;

			// Set font
			SetFont();

			// Add UPChanged
			SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);

            // Attach to Message Store
            this._store = Facade.GetInstance().GetMessageStore();

            // Reset DataSource
            this.messageBS.DataSource = this._store.Messages;
            this.dataGridView.DataSource = this.messageBS;

            // sort.
            //this.dataGridView.Sort(this.dataGridView.Columns[2], ListSortDirection.Descending);
        }

		void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			// Reset the font (if required)
			SetFont();
		}

		private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			// Set the Sent Column
			if (e.ColumnIndex == 0)
			{
				// Get Image
                Object obj = ((this.messageBS[e.RowIndex] as MailMessage).Read ? _readImage : _unreadImage);
                if (obj != null)
                {
                    e.Value = obj;
                }
                else
                {
                    e.Value = Properties.Resources.Unread;
                }
			}
		}

		private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			// Format the Date Column
			if (e.ColumnIndex == 2)
			{
				DateTime date = (DateTime)e.Value;
				DateTime today = DateTime.Now;

				if ((date.Day == today.Day) && (date.Month == today.Month) && (date.Year == today.Year))
				{
					e.CellStyle.Format = "t";
				}
			}
		}

		private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if ((e.ColumnIndex == 1) && (e.RowIndex >= 0))
			{
				// Draw Merged Cell
				Graphics g = e.Graphics;
				bool selected = ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected);
				Color fcolor = (selected ? e.CellStyle.SelectionForeColor : e.CellStyle.ForeColor);
				Color bcolor = (selected ? e.CellStyle.SelectionBackColor : e.CellStyle.BackColor);
				Font font = e.CellStyle.Font;

				if (!(this.messageBS[e.RowIndex] as MailMessage).Read)
				{
					font = new Font(font, FontStyle.Bold);
				}

				// Get size information
				string from = (this.messageBS[e.RowIndex] as MailMessage).From;
				string subject = (this.messageBS[e.RowIndex] as MailMessage).Subject;
				Size size = TextRenderer.MeasureText(e.Graphics, from, font);

				// Note that this always aligns top, right
				// Also this should use the ClipBounds but that is not currently working
				int x = e.CellBounds.Left + e.CellStyle.Padding.Left;
				int y = e.CellBounds.Top + e.CellStyle.Padding.Top;
				int width = e.CellBounds.Width - (e.CellStyle.Padding.Left + e.CellStyle.Padding.Right);
				int height = size.Height + (e.CellStyle.Padding.Top + e.CellStyle.Padding.Bottom);

				// Draw background
				g.FillRectangle(new SolidBrush(bcolor), e.CellBounds);

				// Draw first line
				TextRenderer.DrawText(e.Graphics, from, font, new Rectangle(x, y, width, height), fcolor, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

				// Use grey for second line if not selected
				if (!selected)
				{
					fcolor = Color.Gray;
				}

				// Reset font and y location
				font = e.CellStyle.Font;
				y = y + height - 1;

				TextRenderer.DrawText(e.Graphics, subject, font, new Rectangle(x, y, width, height), fcolor, TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.EndEllipsis);

				// Let them know we handled it
				e.Handled = true;
			}
			else if ((e.ColumnIndex == 0) && (e.RowIndex >= 0))
			{
				e.Paint(e.ClipBounds, e.PaintParts & ~DataGridViewPaintParts.Focus);
				e.Handled = true;
			}
		}

		private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			if (e.RowIndex == this.messageBS.Position)
			{
				e.DrawFocus(e.RowBounds, true);
			}
			else
			{
				// Draw background
				Pen p;
				using (p = new Pen(Color.LightGray))
				{
					e.Graphics.DrawRectangle(p, e.RowBounds);
				}
			}
		}

        /// <summary>
        /// Event for position changed.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
		private void messageBS_PositionChanged(object sender, EventArgs e)
		{

            MailMessage mailMessage = null;

            // select the first message as default.
            if (this.messageBS.Count > 0)
            {
                mailMessage = this.messageBS[0] as MailMessage;
            }

            // if there is a current message select it.
            try
            {
                mailMessage = this.messageBS.Current as MailMessage;
            }
            catch (Exception)
            {
            }

            // if the message is not null select it.
            if (mailMessage != null)
            {
                // Let the message store know
                _store.SelectedMessage = mailMessage;
                MainForm.GetInstance().LoadSelectedMessage(_store);
            }
        }

        /// <summary>
        /// Context menu event handler for mark message as unread.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void markAsUnreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MarkMessageAs(false);
        }

        /// <summary>
        /// Context menu event handler for mark message as read.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void markAsReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MarkMessageAs(true);
        }


        /// <summary>
        /// Event for print message.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GetInstance().toolStripButtonPrint_Click(sender, e);
        }

        /// <summary>
        /// Menu item reply event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void replyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GetInstance().toolStripButtonReply_Click(sender, e);
        }

        /// <summary>
        /// Menu item reply to all event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void replyToAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GetInstance().toolStripButtonReplyToAll_Click(sender, e);
        }

        /// <summary>
        /// Menu item forward event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GetInstance().toolStripButtonForward_Click(sender, e);
        }

        /// <summary>
        /// Event handler for delete menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeleteSelectedMessage();
        }

        /// <summary>
        /// Move message to folder menu item click event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void moveToFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.GetInstance().toolStripMoveFolder_Click(sender, e);
        }

        #endregion

		#region Public Properties
		public int SelectedRow
		{
			set
			{
				this.dataGridView.Rows[value].Selected = true;
			}
		}
		#endregion

		#region Private Implementation
		private void SetFont()
		{
			// Set the font
			_font = SystemFonts.IconTitleFont;

			if (this.Font != _font)
			{
				// Set font
				this.Font = _font;

				// Set default row height
				this.dataGridView.RowTemplate.Height = (TextRenderer.MeasureText("I", _font).Height + 5) * 2;
				Debug.WriteLine(this.dataGridView.RowTemplate.Height.ToString());
			}
		}
		#endregion

		#region Designer Code
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.SentColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.markAsReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsUnreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.replyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.replyToAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.moveToFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageBS)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SentColumn,
            this.To,
            this.SentDate});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView.DataSource = this.messageBS;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.FormatProvider = new System.Globalization.CultureInfo("en-US");
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(385, 274);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.VirtualMode = true;
            this.dataGridView.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dataGridView1_CellValueNeeded);
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // SentColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.FormatProvider = new System.Globalization.CultureInfo("en-US");
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4, 4, 2, 2);
            this.SentColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.SentColumn.HeaderText = "";
            this.SentColumn.Name = "SentColumn";
            this.SentColumn.ReadOnly = true;
            this.SentColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SentColumn.Width = 25;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem,
            this.toolStripSeparator1,
            this.markAsReadToolStripMenuItem,
            this.markAsUnreadToolStripMenuItem,
            this.toolStripSeparator2,
            this.replyToolStripMenuItem,
            this.replyToAllToolStripMenuItem,
            this.forwardToolStripMenuItem,
            this.toolStripSeparator3,
            this.deleteToolStripMenuItem,
            this.moveToFolderToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(170, 220);
            // 
            // markAsReadToolStripMenuItem
            // 
            this.markAsReadToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Read;
            this.markAsReadToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.markAsReadToolStripMenuItem.Name = "markAsReadToolStripMenuItem";
            this.markAsReadToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.markAsReadToolStripMenuItem.Text = "Mark as &Read";
            this.markAsReadToolStripMenuItem.Click += new System.EventHandler(this.markAsReadToolStripMenuItem_Click);
            // 
            // markAsUnreadToolStripMenuItem
            // 
            this.markAsUnreadToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Unread;
            this.markAsUnreadToolStripMenuItem.Name = "markAsUnreadToolStripMenuItem";
            this.markAsUnreadToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.markAsUnreadToolStripMenuItem.Text = "Mark as &Unread";
            this.markAsUnreadToolStripMenuItem.Click += new System.EventHandler(this.markAsUnreadToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Print;
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // replyToolStripMenuItem
            // 
            this.replyToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Reply;
            this.replyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.replyToolStripMenuItem.Name = "replyToolStripMenuItem";
            this.replyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.replyToolStripMenuItem.Text = "Reply";
            this.replyToolStripMenuItem.Click += new System.EventHandler(this.replyToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // replyToAllToolStripMenuItem
            // 
            this.replyToAllToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.ReplyAll;
            this.replyToAllToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.replyToAllToolStripMenuItem.Name = "replyToAllToolStripMenuItem";
            this.replyToAllToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.replyToAllToolStripMenuItem.Text = "Reply to All";
            this.replyToAllToolStripMenuItem.Click += new System.EventHandler(this.replyToAllToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Forward;
            this.forwardToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.forwardToolStripMenuItem.Text = "Forward";
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.forwardToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Delete;
            this.deleteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            // 
            // moveToFolderToolStripMenuItem
            // 
            this.moveToFolderToolStripMenuItem.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Move;
            this.moveToFolderToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.moveToFolderToolStripMenuItem.Name = "moveToFolderToolStripMenuItem";
            this.moveToFolderToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.moveToFolderToolStripMenuItem.Text = "Move to Folder...";
            this.moveToFolderToolStripMenuItem.Click += new System.EventHandler(this.moveToFolderToolStripMenuItem_Click);
            // 
            // To
            // 
            this.To.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.To.DataPropertyName = "To";
            dataGridViewCellStyle3.FormatProvider = new System.Globalization.CultureInfo("en-US");
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.To.DefaultCellStyle = dataGridViewCellStyle3;
            this.To.HeaderText = "Arranged By: Date";
            this.To.Name = "To";
            this.To.ReadOnly = true;
            this.To.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // SentDate
            // 
            this.SentDate.DataPropertyName = "SentDate";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "ddd M/dd";
            dataGridViewCellStyle4.FormatProvider = new System.Globalization.CultureInfo("en-US");
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2, 2, 4, 2);
            this.SentDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.SentDate.HeaderText = "Newest on top";
            this.SentDate.Name = "SentDate";
            this.SentDate.ReadOnly = true;
            this.SentDate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SentDate.Width = 105;
            // 
            // messageBS
            // 
            this.messageBS.DataSource = typeof(ActiveUp.MailSystem.DesktopClient.MailMessage);
            this.messageBS.PositionChanged += new System.EventHandler(this.messageBS_PositionChanged);
            // 
            // MessageList
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridView);
            this.Name = "MessageList";
            this.Size = new System.Drawing.Size(385, 274);
            this.Load += new System.EventHandler(this.MessageList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageBS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

    }
}
