using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net.NetworkInformation;
using ActiveUp.MailSystem.DesktopClient.Mail;
using System.Diagnostics;
using System.Threading;
using System.IO;
using ActiveUp.MailSystem.DesktopClient.Folder;
using ActiveUp.MailSystem.DesktopClient.Help;
using ActiveUp.MailSystem.DesktopClient.AccountSetupUI;

namespace ActiveUp.MailSystem.DesktopClient
{
    /// <summary>
    /// This class represents the main form for ActiveUp Desktop Sample application.
    /// </summary>
	public partial class MainForm : Form
	{

        /// <summary>
        /// Singleton instance for MainForm.
        /// </summary>
        private static MainForm mainForm;

		// Message Server
		private	Bitmap _onlineImage;
		private Bitmap _offlineImage;
        private FindSettings _findSettings;
        private bool loadFirstMessageFlag;

        /// <summary>
        /// Mail message string representation for print.
        /// </summary>
        private StringReader mailMessageStringReader;
        
        /// <summary>
        /// MainForm constructor.
        /// </summary>
        private MainForm()
		{
            SplashForm splash = new SplashForm();
            splash.ShowSplashScreen();

            Facade facade = Facade.GetInstance();

            // Retrieve messages from inbox mailbox.
            facade.LoadMessageStore(Constants.Inbox, facade.GetMessageStore());

            // Retrieve messages from sent item mailbox.
            facade.LoadMessageStore(Constants.SentItems, facade.GetMessageStoreSent());

            // Retrieve messages from delete item mailbox.
            facade.LoadMessageStore(Constants.DeletedItems, facade.GetMessageStoreDelete());

            // Retrieve messages from custom items mailbox.
            facade.LoadMessageStore(Constants.CustomFolders, facade.GetMessageStoreCustom());

            // retrieve the messages from inbox mailbox.
            facade.RetrieveMessages(Constants.Inbox);

            // set the flag for load first message.
            loadFirstMessageFlag = true;

            // Use system fonts
            this.Font = SystemFonts.IconTitleFont;

            this._findSettings = new FindSettings();

            // Designer Generated Code
			this.InitializeComponent();

            // select the mail button.
            this.leftSpine1.toolStripButtonMail_Click(this, EventArgs.Empty);
            
            // closes the splash.
            splash.CloseSplashScreen();

            // if there is no mail accounts.
            if (Facade.GetInstance().GetDefaultAccountInfo() == null)
            {
                SettingsReminderForm settingsReminderForm = new SettingsReminderForm();
                DialogResult dr = settingsReminderForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    AccountSettingsForm frm = new AccountSettingsForm();
                    frm.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Gets the singleton instance for Main Form.
        /// </summary>
        /// <returns>The singleton Main Form instance.</returns>
        public static MainForm GetInstance()
        {
            if (mainForm == null)
            {
                mainForm = new MainForm();
            }
            return mainForm;
        }

        /// <summary>
        /// Method for load messages.
        /// </summary>
        /// <param name="messageListType">The Message List Type.</param>
        public void LoadMessages(MessageListType messageListType)
        {
            this.messageArea1.LoadMessages(messageListType);
        }

        /// <summary>
        /// Method for get the selected mailbox.
        /// </summary>
        /// <returns>The current mailbox.</returns>
        public string GetSelectedMailbox()
        {
            return this.leftSpine1.FolderView.GetSelectedMailbox();
        }

        /// <summary>
        /// Method for get the selected folder.
        /// </summary>
        /// <returns>The current selected folder.</returns>
        public string GetSelectedFolder()
        {
            return this.leftSpine1.FolderView.GetSelectedFolder();
        }

        /// <summary>
        /// Method for create the selected as a ActiveUp.Net.Mail.Message.
        /// </summary>
        /// <returns>The ActiveUp.Net.Mail.Message object.</returns>
        private ActiveUp.Net.Mail.Message CreateSelectedMessage()
        {
            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
            MailMessage mailMessage = this.rightSpine1.GetSelectedMessage();
            if (mailMessage != null)
            {
                Facade facade = Facade.GetInstance();
                ActiveUp.MailSystem.DesktopClient.AccountSettings.AccountInfo accInfo =
                    facade.GetDefaultAccountInfo();

                if (accInfo != null)
                {
                    message.From.Email = accInfo.EmailAddress;
                }

                string separator = ",";
                string[] recipients = mailMessage.To.Split(separator.ToCharArray());
                foreach (string r in recipients)
                {
                    // We assign the recipient email
                    message.To.Add(r);
                }

                // We assign the subject
                message.Subject = mailMessage.Subject;

                // We assign the body text
                message.BodyText.Text = this.rightSpine1.GetSelectedMessageBody();
            }
            return message;
        }

        #region Event Handlers
		
        private void Form1_Load(object sender, EventArgs e)
		{
			// Setup Message Server
            MessageStore messageStore = Facade.GetInstance().GetMessageStore();

            // Update message count
            this.itemCountLabel.Text = String.Format(this.itemCountLabel.Text, messageStore.Messages.Count);

            // Setup Online/Offline
            _onlineImage = Properties.Resources.ActiveUp_MailSystem;
            _offlineImage = Properties.Resources.Error;

            // Check for Network Changes
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);

            // Set Status Bar
            UpdateStatusBar();

            // Set icon
            this.Icon = Icon.FromHandle(Properties.Resources.ActiveUp_MailSystem.GetHicon());

            // Track Preference Changes
            Microsoft.Win32.SystemEvents.UserPreferenceChanged += new Microsoft.Win32.UserPreferenceChangedEventHandler(Form1_UserPreferenceChanged);

            // load the selected message
            this.LoadSelectedMessage(Facade.GetInstance().GetMessageStore());
		}

        /// <summary>
        /// Method for load the selected message.
        /// </summary>
        /// <param name="store">The MessageStore.</param>
        public void LoadSelectedMessage(MessageStore store)
        {
            // load the selected message
            this.rightSpine1.LoadSelectedMessage(store);
        }

        /// <summary>
        /// Update message count.
        /// </summary>
        public void UpdateMessageCountLabel()
        {
            int msgCount = this.messageArea1.MessageList.GetVisibleRowsCount();
            this.itemCountLabel.Text = String.Format("{0} Items", msgCount);
        }

		private void Form1_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
		{
			if (this.Font != SystemFonts.IconTitleFont)
			{
				// Only respond at RT
				this.Font = SystemFonts.IconTitleFont;
				this.PerformAutoScale();
			}
		}
		#endregion

		#region Online Handling
		private void UpdateStatusBar()
		{
			if (NetworkInterface.GetIsNetworkAvailable())
			{
				this.connectedStatusLabel.Text = "All Folders are up to date.";
				this.connectedImageLabel.Text = " Connected";
				this.connectedImageLabel.Image = _onlineImage;
			}
			else
			{
				this.connectedStatusLabel.Text = "This folder was last updated on " + DateTime.Now.ToShortDateString() + ".";
				this.connectedImageLabel.Text = " Disconnected";
				this.connectedImageLabel.Image = _offlineImage;
			}
		}

		void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
		{
			this.Invoke(new MethodInvoker(this.UpdateStatusBar));
		}
		#endregion

        /// <summary>
        /// Event handler for manage email account settings.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void emailAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountSettingsForm frm = new AccountSettingsForm();
            frm.ShowDialog();
        }

        /// <summary>
        /// Event for new mail messages.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void mailMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailSenderForm frm = new MailSenderForm();
            frm.Show();
        }

        /// <summary>
        /// Method for delete an item.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (this.leftSpine1.ContainsFocus)
            {
                this.leftSpine1.FolderView.DeleteSelectedFolder();
            }
            else
            {
                this.messageArea1.MessageList.DeleteSelectedMessage();
            }
        }

        /// <summary>
        /// Main form closing event handler.
        /// Disconnect from the current protocol.
        /// 
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Facade facade = Facade.GetInstance();
                facade.SaveAllMailMessages();
                facade.Disconnect();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Button find event handler
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            FindForm frm = new FindForm();
            frm.LoadFindSettings(this._findSettings);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this._findSettings = frm.FindSettings;
                this.messageArea1.MessageList.FilterMessages(this._findSettings);
            }
        }

        /// <summary>
        /// Button reply event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void toolStripButtonReply_Click(object sender, EventArgs e)
        {
            MailSenderForm frm = new MailSenderForm();
            Facade facade = Facade.GetInstance();
            MessageStore messageStore = facade.GetMessageStore();
            MailMessage mailMessage = messageStore.SelectedMessage;
            frm.LoadMessage(mailMessage, MailSenderType.Reply);
            frm.Show();
        }

        /// <summary>
        /// Button reply to all event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void toolStripButtonReplyToAll_Click(object sender, EventArgs e)
        {
            MailSenderForm frm = new MailSenderForm();
            Facade facade = Facade.GetInstance();
            MessageStore messageStore = facade.GetMessageStore();
            MailMessage mailMessage = messageStore.SelectedMessage;
            frm.LoadMessage(mailMessage, MailSenderType.ReplyToAll);
            frm.Show();
        }

        /// <summary>
        /// Button forward event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            MailSenderForm frm = new MailSenderForm();
            Facade facade = Facade.GetInstance();
            MessageStore messageStore = facade.GetMessageStore();
            MailMessage mailMessage = messageStore.SelectedMessage;
            frm.LoadMessage(mailMessage, MailSenderType.Forward);
            frm.Show();
        }

        /// <summary>
        /// Button help event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            Process helpProcess = new Process();
            helpProcess.StartInfo.FileName = "Documentation.chm";
            helpProcess.Start();
        }

        /// <summary>
        /// Button mark message as read event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void markasReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.messageArea1.MessageList.MarkMessageAs(true);
        }

        /// <summary>
        /// Button mark message as read event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void markasUnreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.messageArea1.MessageList.MarkMessageAs(false);
        }

        /// <summary>
        /// Button mark all message as read event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void markAllasReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.messageArea1.MessageList.MarkAllMessageAs(true);
        }

        /// <summary>
        /// Button select all event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.messageArea1.MessageList.SelectAllMessages();
        }

        /// <summary>
        /// Event for paint Main Frame.
        /// verify the loadFirstMessageFlag and load the first selected message properly.
        /// This was done in order to fix the bug reported in msdn:
        /// http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=115341
        /// Related with Web Browser: DocumentStream.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (loadFirstMessageFlag)
            {
                // load the selected message
                this.LoadSelectedMessage(Facade.GetInstance().GetMessageStore());
                loadFirstMessageFlag = false;
            }

            // TODO - test
            //Facade facade = Facade.GetInstance();
            //if (facade.reload)
            //{

            //    MessageStore messageStore = facade.GetMessageStore();
            //    MessageStore retrievedMessages = facade.StoreRetrieved;
            //    foreach (MailMessage mailMessage in retrievedMessages.Messages)
            //    {
            //        if (!messageStore.Messages.Contains(mailMessage))
            //        {
            //            messageStore.Messages.Add(mailMessage);
            //            messageStore.UnreadCount++;
            //        }
            //    }
            //    facade.reload = false;

            //    //if (loadFirstMessageFlag)
            //    //{
            //    //    // load the selected message
            //    //    this.LoadSelectedMessage(Facade.GetInstance().GetMessageStore());
            //    //    loadFirstMessageFlag = false;
            //    //}

            //    //this.messageArea1.MessageList.Invalidate(true);
            //}

        }

        /// <summary>
        /// Event for refresh the mailbox and messages.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facade facade = Facade.GetInstance();

            // Retrieve messages from inbox mailbox.
            facade.LoadMessageStore(Constants.Inbox, facade.GetMessageStore());
            facade.RetrieveMessages(Constants.Inbox);

            // Retrieve messages from sent item mailbox.
            facade.LoadMessageStore(Constants.SentItems, facade.GetMessageStoreSent());

            // Retrieve messages from deleted item mailbox.
            facade.LoadMessageStore(Constants.DeletedItems, facade.GetMessageStoreDelete());

            // Retrieve messages from custom items mailbox.
            facade.LoadMessageStore(Constants.CustomFolders, facade.GetMessageStoreCustom());

            // Setup Message Server
            MessageStore messageStore = facade.GetMessageStore();

            // load the selected message
            this.LoadSelectedMessage(facade.GetMessageStore());
        }

        /// <summary>
        /// Event for delete all messages.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void mailboxCleanupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.messageArea1.MessageList.DeleteAllMessages();
        }

        /// <summary>
        /// Event for save message.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "ActiveUp Email Files |*.eml";
            DialogResult dr = saveDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ActiveUp.Net.Mail.Message message = this.CreateSelectedMessage();
                if (message != null)
                {
                    message.StoreToFile(saveDialog.FileName);
                }
            }
            Directory.SetCurrentDirectory(currentDir);            
        }

        #region FolderEvents
        
        /// <summary>
        /// Event for delete the selected folder.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.FolderView.DeleteSelectedFolder();
        }

        /// <summary>
        /// Event for rename the selected folder.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void renameFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.FolderView.EditSelectedFolder();
        }

        /// <summary>
        /// Event for create new folder.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateFolderForm createFolderForm = new CreateFolderForm();
            createFolderForm.SelectedFolder = this.GetSelectedFolder();
            DialogResult dr = createFolderForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string folderName = createFolderForm.FolderName;
                string selectedFolder = createFolderForm.SelectedFolder;

                // create the new folder.
                this.leftSpine1.FolderView.CreateFolder(selectedFolder, folderName);
            }
        }

        /// <summary>
        /// Event for copy a mail message to a folder.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void copytoFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveToFolderForm moveToFolderForm = new MoveToFolderForm();
            moveToFolderForm.SelectedFolder = this.GetSelectedFolder();
            DialogResult dr = moveToFolderForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Facade facade = Facade.GetInstance();
                MailMessage mailMessage = this.rightSpine1.GetSelectedMessage();
                mailMessage.ParentFolder = moveToFolderForm.SelectedFolder; 
                facade.GetMessageStoreCustom().Messages.Add(mailMessage);
            }
        }

        /// <summary>
        /// Event for move a mail message to a folder.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void toolStripMoveFolder_Click(object sender, EventArgs e)
        {
            MoveToFolderForm moveToFolderForm = new MoveToFolderForm();
            moveToFolderForm.SelectedFolder = this.GetSelectedFolder();
            DialogResult dr = moveToFolderForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Facade facade = Facade.GetInstance();
                MailMessage mailMessage = this.rightSpine1.GetSelectedMessage();
                mailMessage.ParentFolder = moveToFolderForm.SelectedFolder;
                facade.GetMessageStoreCustom().Messages.Add(mailMessage);
                this.messageArea1.MessageList.DeleteSelectedMessageForMove();
            }
        }

        #endregion

        #region PrintEvents

        /// <summary>
        /// Event for print message.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            string strText = string.Empty;//this.CreateMessage().ToMimeString();
            ActiveUp.Net.Mail.Message message = this.CreateSelectedMessage();
            if (message != null)
            {
                strText = message.ToMimeString();
            }
            mailMessageStringReader = new StringReader(strText);
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                this.printDocument.Print();
            }
        }

        /// <summary>
        /// Print preview event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strText = string.Empty;//this.CreateMessage().ToMimeString();
            ActiveUp.Net.Mail.Message message = this.CreateSelectedMessage();
            if (message != null)
            {
                strText = message.ToMimeString();
            }
            mailMessageStringReader = new StringReader(strText);
            printPreviewDialog.ShowDialog();
        }

        /// <summary>
        /// Print page for print document event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void printDocument_PrintPage(object sender, 
            System.Drawing.Printing.PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPosition = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;
            Font printFont = this.Font;
            SolidBrush brush = new SolidBrush(Color.Black);
            // Work out the number of lines per page, using the MarginBounds.
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            // Iterate over the string using the StringReader, printing each line.
            while (count < linesPerPage && ((line = mailMessageStringReader.ReadLine()) != null))
            {
                // calculate the next line position based on the height of the font according to the printing device
                yPosition = topMargin + (count * printFont.GetHeight(ev.Graphics));
                // draw the next line in the rich edit control
                ev.Graphics.DrawString(line, printFont, brush, leftMargin, yPosition, new StringFormat());
                count++;
            }
            // If there are more lines, print another page.
            if (line != null)
            {
                ev.HasMorePages = true;
            }
            else
            {
                ev.HasMorePages = false;
            }
            brush.Dispose();
        }

        #endregion

        #region ViewEvents
        
        /// <summary>
        /// Event for mail menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.toolStripButtonMail_Click(sender, e);
        }

        /// <summary>
        /// Event for calendar menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void calendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.toolStripButtonCalendar_Click(sender, e);
        }

        /// <summary>
        /// Event for contacts menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void contactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.toolStripButtonContacts_Click(sender, e);
        }

        /// <summary>
        /// Event for tasks menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void tasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.toolStripButtonTasks_Click(sender, e);
        }

        /// <summary>
        /// Event for notes menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void notesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.toolStripButtonNotes_Click(sender, e);
        }

        /// <summary>
        /// Event for folder list menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void folderListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.toolStripButtonFolders_Click(sender, e);
        }

        /// <summary>
        /// Event for shortcuts menu item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void shortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.leftSpine1.toolStripButtonShotcuts_Click(sender, e);
        }

        #endregion

        #region TODO_Events

        // TODO Implement methods above.

        private void ShowMessageErrorMethodNotImplemented()
        {
            MessageBox.Show("This is a sample application. This function is not implemented yet.",
                "ActiveUp eMail Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }
 
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        #endregion

        private void toolStripSplitButtonSendReceive_ButtonClick(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void archiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void importandExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void dataFileManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void closeAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void openItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();
        }

        private void navigationPaneShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void appointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void meetingRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void contactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void distributionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void taskRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void journalEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void noteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void internetFaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void chooseFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void ActiveUpMailDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void nothingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        /// <summary>
        /// Event for exit menu item clicked.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void movetoFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void navigationPaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void readingPaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void autoPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void reminderWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void toolbarsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void jornalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void folderToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void addressBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void organizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void rulesandSlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void outofOfficeAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void emptyDeletedItemsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void recoverDeletedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void formsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void macrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void optionsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void optionsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void optionsToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void optionsToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void showtheOfficeAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void microsoftOfficeOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void checkforUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void detectandRepairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void activateProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        private void customerFeedbackOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowMessageErrorMethodNotImplemented();

        }

        /// <summary>
        /// Event handler for about help menu item.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void aboutActiveUpMailSystemClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

    }
}