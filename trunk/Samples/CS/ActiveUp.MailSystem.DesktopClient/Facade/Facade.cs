using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;
using ActiveUp.Net.Mail;
using System.Threading;

namespace ActiveUp.MailSystem.DesktopClient
{
    /// <summary>
    /// This class represents a Facade.
    /// It's used to manipulate the comunication between the ClientDesktop message objects and
    /// the ActiveUp Mail objects. This class have access methods for managa the message retrieve,
    /// message send, and others message manipulations.
    /// </summary>
    public class Facade
    {

        // Region for class attributes.
        #region Attributes

        /// <summary>
        /// Singleton instance for Facade
        /// </summary>
        private static Facade facade;

        /// <summary>
        /// Attribute for messages store all items.
        /// </summary>
        private MessageStore _store;

        /// <summary>
        /// Attribute for messages store for sent items.
        /// </summary>
        private MessageStore _storeSent;

        /// <summary>
        /// Attribute for messages store for deleted items.
        /// </summary>
        private MessageStore _storeDelete;

        /// <summary>
        /// Attribute for messages store for custom items.
        /// </summary>
        private MessageStore _storeCustom;

        /// <summary>
        /// Attribute for mail account settings informations.
        /// </summary>
        private AccountSettings _accSettings;

        /// <summary>
        /// Attribute for client settings.
        /// </summary>
        private ClientSettings _clientSettings;

        /// <summary>
        /// Attribute for the imap4 controller.
        /// </summary>
        private Imap4Controller _imap4Controller;

        /// <summary>
        /// Attribute for the Pop3 controller.
        /// </summary>
        private Pop3Controller _pop3Controller;

        /// <summary>
        /// Attribute for the Smtp controller.
        /// </summary>
        private SmtpController _smtpController;

        #endregion

        // Region for Construcor
        #region Constructor
        
        /// <summary>
        /// Constructor for Facade class.
        /// Use GetInstance() instead.
        /// </summary>
        private Facade()
        {
            // Load Account Settings.
            this._accSettings = AccountSettings.Load("Account.Settings");

            // Load Client Settings.
            this._clientSettings = ClientSettings.Load("Client.Settings");

            // Setup Message Server.
            this._store = new MessageStore();

            // Setup Message Server Sent Items.
            this._storeSent = new MessageStore();

            // Setup Message Server Deleted Items.
            this._storeDelete = new MessageStore();

            // Setup Message Server Custom Folders.
            this._storeCustom = new MessageStore();

            // Imap4 controller instance.
            this._imap4Controller = new Imap4Controller(this.GetDefaultAccountInfo());

            // Pop3 controller instance.
            this._pop3Controller = new Pop3Controller(this.GetDefaultAccountInfo());

            // Smtp controller instance.
            this._smtpController = new SmtpController();
        }

        #endregion

        // Region for Singleton
        #region Singleton
        
        /// <summary>
        /// Gets the singleton instance for the Facade.
        /// </summary>
        /// <returns>The Facade instance.</returns>
        public static Facade GetInstance()
        {
            if (facade == null)
            {
                facade = new Facade();
            }
            return facade;
        }

        #endregion

        // Region for Account Settings methods.
        #region AccountSettings

        /// <summary>
        /// Method for add an account information.
        /// </summary>
        /// <param name="accountInfo">The account information object.</param>
        public void AddAccountInfo(AccountSettings.AccountInfo accountInfo)
        {
            this._accSettings.AddAccount(accountInfo);
            this.Connect();
        }

        /// <summary>
        /// Method for remove an account information.
        /// </summary>
        /// <param name="accountInfo">The account information object.</param>
        public void DeleteAccountInfo(AccountSettings.AccountInfo accountInfo)
        {
            this._accSettings.Remove(accountInfo);
            this.Connect();
        }

        /// <summary>
        /// Method for get all account information.
        /// </summary>
        /// <returns>The account settings collection with account info.</returns>
        public AccountSettings GetAllAccountInfo()
        {
            return this._accSettings;
        }

        /// <summary>
        /// Method for set the default account information.
        /// </summary>
        /// <param name="accountInfo">The account information object.</param>
        public void SetDefaultAccountInfo(AccountSettings.AccountInfo accountInfo)
        {
            this._clientSettings.DefaultAccountID = accountInfo.ID;
            this.Connect();
        }

        /// <summary>
        /// Property for get the default account information.
        /// </summary>
        /// <returns>The default AccountInfo object.</returns>
        public AccountSettings.AccountInfo GetDefaultAccountInfo()
        {
            AccountSettings.AccountInfo ret = null;
            foreach (AccountSettings.AccountInfo info in this._accSettings)
            {
                if (this._clientSettings.DefaultAccountID == info.ID)
                {
                    ret = info;
                    break;
                }
            }

            // if there is no default account use the first one.
            if (ret == null && this._accSettings.Count > 0)
            {
                ret = this._accSettings[0];
            }

            return ret;
        }
        
        /// <summary>
        /// Method for save the account settings.
        /// </summary>
        public void SaveAccountSettings()
        {
            AccountSettings.Save("Account.Settings", this._accSettings);
            this.Connect();
        }

        #endregion

        // Region for Client settings.
        #region ClientSettings

        /// <summary>
        /// Method for save the client settings.
        /// </summary>
        public void SaveClientSettings()
        {
            ClientSettings.Save("Client.Settings", this._clientSettings);
        }

        #endregion

        // Region for Message Store
        #region MessageStore

        /// <summary>
        /// Method for access the message store.
        /// </summary>
        /// <returns>The MessageStore Object.</returns>
        public MessageStore GetMessageStore()
        {
            return this._store;
        }

        /// <summary>
        /// Method for access the message store sent.
        /// </summary>
        /// <returns>The MessageStore Object.</returns>
        public MessageStore GetMessageStoreSent()
        {
            return this._storeSent;
        }

        /// <summary>
        /// Method for access the message store delete.
        /// </summary>
        /// <returns>The MessageStore Object.</returns>
        public MessageStore GetMessageStoreDelete()
        {
            return this._storeDelete;
        }

        /// <summary>
        /// Method for access the message store custom folders.
        /// </summary>
        /// <returns>The MessageStore Object.</returns>
        public MessageStore GetMessageStoreCustom()
        {
            return this._storeCustom;
        }

        /// <summary>
        /// Save all mail messages to the selected mailbox.
        /// </summary>
        public void SaveAllMailMessages()
        {
            this.SaveMailMessages(Constants.Inbox, this.GetMessageStore().Messages);
            this.SaveMailMessages(Constants.SentItems, this.GetMessageStoreSent().Messages);
            this.SaveMailMessages(Constants.DeletedItems, this.GetMessageStoreDelete().Messages);
            this.SaveMailMessages(Constants.CustomFolders, this.GetMessageStoreCustom().Messages);
        }

        /// <summary>
        /// Save the mail messages to the selected mailbox.
        /// </summary>
        /// <param name="mailbox">The mail box.</param>
        /// <param name="mailMessages">The mail messages.</param>
        public void SaveMailMessages(string mailbox, BindingList<MailMessage> mailMessages)
        {
            // verify if the messages directory exist, if not create it.
            string directory = Constants.Messages;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string path = Path.Combine(directory, mailbox + ".xml");
            StreamWriter sw = new StreamWriter(path);
            XmlSerializer xs = new XmlSerializer(typeof(BindingList<MailMessage>));
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            xs.Serialize(sw, mailMessages, xsn);
            sw.Close();
        }

        /// <summary>
        /// Method used for load the messages store.
        /// </summary>
        /// <param name="mailbox">The mail box name.</param>
        /// <param name="messageStore">The message store object for fill.</param>
        public void LoadMessageStore(string mailbox, MessageStore messageStore)
        {
            // Create the data connector.
            messageStore.Messages = new SortableBindingList<MailMessage>();

            // Load data from Inbox (XML file).
            Assembly assembly = this.GetType().Assembly;
            MailMessage message;
            DataSet ds = new DataSet();
            DataView view;
            int unread = 0;

            string resourceName = string.Concat(this.GetType().Namespace, ".Mail.Inbox.xsd");
            ds.ReadXmlSchema(assembly.GetManifestResourceStream(resourceName));

            string inboxPath = Path.Combine(Constants.Messages, mailbox + ".xml");
            string directory = Constants.Messages;

            if (Directory.Exists(directory) && File.Exists(inboxPath))
            {
                ds.ReadXml(inboxPath);
                view = ds.Tables[0].DefaultView;

                foreach (DataRowView row in view)
                {
                    // Creat the message
                    message = new MailMessage();
                    message.Id = row["Id"] as string;
                    message.Cc = row["CC"] as string;
                    message.From = row["From"] as string;
                    message.To = row["To"] as string;
                    message.SentDate = (DateTime)row["SentDate"];
                    message.Subject = row["Subject"] as string;
                    message.Path = row["Path"] as string;
                    message.Read = (bool)row["Read"];
                    message.BaseIndex = (int)row["BaseIndex"];
                    message.ParentFolder = (string)row["ParentFolder"];

                    // Add the message
                    messageStore.Messages.Add(message);

                    // Update count
                    if (!message.Read)
                    {
                        unread++;
                    }
                }

                messageStore.UnreadCount = unread;

                //// Select first message
                //if (messageStore.Messages.Count > 0)
                //{
                //    messageStore.SelectedMessage = (messageStore.Messages[0] as MailMessage);
                //}
            }
        }

        #region TODO
        
        // these code bellow was created to try to fix the message retrieve delay.

        //private MessageStore _storeRetrieved;

        //public delegate string DelegateRetrieveMessagesAsynch(string mailbox);

        //public MessageStore StoreRetrieved
        //{
        //    get { return this._storeRetrieved; }
        //}
        //public bool reload = false;

        #endregion

        /// <summary>
        /// Method for retrieve messages for a mailbox.
        /// </summary>
        /// <param name="mailbox">The mailbox.</param>
        public void RetrieveMessages(string mailbox)
        {
            //DelegateRetrieveMessagesAsynch delegateMessage = RetrieveMessagesAsynch;
            //delegateMessage.BeginInvoke(mailbox, null, null);

            BindingList<MailMessage> mailMessages = new SortableBindingList<MailMessage>();
            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            if (accountInfo != null)
            {
                int messageCount = this.GetMessageCount(mailbox);

                if (accountInfo.MailAccountType == AccountType.POP3)
                {
                    mailMessages = this._pop3Controller.RetrieveMessages(1, messageCount);
                }
                else if (accountInfo.MailAccountType == AccountType.IMAP)
                {
                    mailMessages = this._imap4Controller.RetrieveMessages(mailbox, 1, messageCount);
                }

                if (this._store.Messages != null)
                {
                    foreach (MailMessage mailMessage in mailMessages)
                    {
                        if (!this._store.Messages.Contains(mailMessage))
                        {
                            this._store.Messages.Add(mailMessage);
                            this._store.UnreadCount++;
                        }
                    }
                }
                else
                {
                    this._store.Messages = mailMessages;
                    this._store.UnreadCount = mailMessages.Count;
                }
            }
            this.SaveMailMessages(mailbox, this._store.Messages);
        }

        /// <summary>
        /// Method for retrieve messages for a mailbox.
        /// </summary>
        /// <param name="mailbox">The mailbox.</param>
        public string RetrieveMessagesAsynch(string mailbox)
        {
            //// Setup Message Server.            
            //this._storeRetrieved = new MessageStore();
            //this._storeRetrieved.Messages = new SortableBindingList<MailMessage>();

            //BindingList<MailMessage> mailMessages = new SortableBindingList<MailMessage>();
            //AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            //if (accountInfo != null)
            //{
            //    int increment = 10;
            //    int count = this.GetMessageCount(mailbox);

            //    for (int i = 1; i < count; i = i + increment)
            //    {

            //        if (accountInfo.MailAccountType == AccountType.POP3)
            //        {
            //            mailMessages = this._pop3Controller.RetrieveMessages(i, increment);
            //        }
            //        else if (accountInfo.MailAccountType == AccountType.IMAP)
            //        {
            //            mailMessages = this._imap4Controller.RetrieveMessages(mailbox, i, increment);
            //        }

            //        if (this._storeRetrieved != null)
            //        {

            //            foreach (MailMessage mailMessage in mailMessages)
            //            {
            //                if (!this._storeRetrieved.Messages.Contains(mailMessage))
            //                {
            //                    this._storeRetrieved.UnreadCount++;
            //                    this._storeRetrieved.Messages.Add(mailMessage);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            this._storeRetrieved.Messages = mailMessages;
            //            this._storeRetrieved.UnreadCount = mailMessages.Count;
            //        }

            //        reload = true;

            //        //if (this._store.Messages != null)
            //        //{

            //        //    foreach (MailMessage mailMessage in mailMessages)
            //        //    {
            //        //        if (!this._store.Messages.Contains(mailMessage))
            //        //        {
            //        //            this._store.UnreadCount++;
            //        //            this._store.Messages.Add(mailMessage);
            //        //        }
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    this._store.Messages = mailMessages;
            //        //    this._store.UnreadCount = mailMessages.Count;
            //        //}
            //    }
            //}
            ////this.SaveMailMessages(mailbox, this._store.Messages);

            return string.Empty;
        }

        /// <summary>
        /// Create the message body ".eml" file.
        /// </summary>
        /// <param name="mailMessage">The mail message object.</param>
        /// <param name="mailbox">The mailbox.</param>
        public void CreateMessageBodyFile(MailMessage mailMessage, string mailbox)
        {
            this.CreateMessageBodyFile(mailMessage, null, mailbox);
        }

        /// <summary>
        /// Create the message body ".eml" file.
        /// </summary>
        /// <param name="mailMessage">The mail message object.</param>
        /// <param name="message">ActiveUp.Net.Mail.Message message</param>
        /// <param name="mailbox">The mailbox.</param>
        public void CreateMessageBodyFile(MailMessage mailMessage, ActiveUp.Net.Mail.Message message,
            string mailbox)
        {
            // verify if the messages directory exist, if not create it.
            string directory = Constants.Messages;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // create the file with the message.
            string fileName = string.Concat(mailMessage.Id, ".eml");
            string path = System.IO.Path.Combine(directory, fileName);
            if (!File.Exists(path))
            {
                if (message == null)
                {
                    message = this.GetMessage(mailMessage, mailbox);
                }

                if (message != null)
                {
                    FileStream fileStream = File.Create(path);
                    StreamWriter sw = new StreamWriter(fileStream);
                    sw.Write(message.BodyText.Text);
                    sw.Close();
                }
            }

            mailMessage.Path = path;
        }

        /// <summary>
        /// Method for the the stream message body.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        /// <param name="mailbox">The mailbox.</param>
        /// <returns>The string with the message body.</returns>
        public string GetMessageBodyString(MailMessage mailMessage, string mailbox)
        {
            string ret = string.Empty;
            Stream stream = this.GetMessageBodyStream(mailMessage, mailbox);
            if (stream != null)
            {
                StreamReader reader = new StreamReader(stream);
                ret = reader.ReadToEnd();
            }
            return ret;
        }

        /// <summary>
        /// Method for the the stream message body.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        /// <param name="mailbox">The mailbox.</param>
        /// <returns>The IO stream with the message body.</returns>
        public Stream GetMessageBodyStream(MailMessage mailMessage, string mailbox)
        {
            Stream ret = null;

            // load the message file and set path here if it's null.
            if (!File.Exists(mailMessage.Path))
            {
                this.CreateMessageBodyFile(mailMessage, mailbox);
            }

            // load the current stream.
            if (File.Exists(mailMessage.Path))
            {
                ret = File.OpenRead(mailMessage.Path);
            }

            return ret;
        }

        /// <summary>
        /// Method used for send a message.
        /// </summary>
        /// <param name="recipient">The recipients email.</param>
        /// <param name="subject">The message subject.</param>
        /// <param name="body">The message body.</param>
        /// <param name="attachments">The message attachements.</param>
        public void SendMessage(string recipient, string subject, string body, string[] attachments)
        {
            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            if (accountInfo != null)
            {
                ActiveUp.Net.Mail.Message message = this._smtpController.SendMessage(accountInfo, 
                    recipient, subject, body, attachments);

                if (message != null)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.SentDate = DateTime.Now;
                    mailMessage.From = accountInfo.EmailAddress;
                    mailMessage.To = recipient;
                    mailMessage.Subject = subject;
                    mailMessage.Read = true;
                    mailMessage.Id = message.MessageId;

                    this.CreateMessageBodyFile(mailMessage, message, Constants.SentItems);
                    this._storeSent.Messages.Add(mailMessage);
                    this.SaveMailMessages(Constants.SentItems, this._storeSent.Messages);
                }
            }
        }

        /// <summary>
        /// Method for delete message.
        /// </summary>
        /// <param name="messageIdentifier">The mail message.</param>
        /// <param name="mailbox">The mailbox.</param>
        public void DeleteMessage(MailMessage mailMessage, string mailbox)
        {
            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            if (accountInfo != null)
            {
                try
                {
                    if (accountInfo.MailAccountType == AccountType.POP3)
                    {
                        this._pop3Controller.DeleteMessage(mailMessage.Id);
                    }
                    else if (accountInfo.MailAccountType == AccountType.IMAP)
                    {
                        this._imap4Controller.DeleteMessage(mailMessage.Id, mailbox);
                    }
                }
                catch (Exception)
                {
                }

                // add to delete items.
                if (!mailbox.Equals(Constants.DeletedItems))
                {
                    this._storeDelete.Messages.Add(mailMessage);
                    this.SaveMailMessages(Constants.DeletedItems, this._storeDelete.Messages);
                }
            }
        }

        /// <summary>
        /// Get a mail message basend on message identifier.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        /// <param name="mailbox">The mailbox for get the message.</param>
        /// <returns>The ActiveUp.Net.Mail.Message object.</returns>
        public ActiveUp.Net.Mail.Message GetMessage(MailMessage mailMessage, string mailbox)
        {
            ActiveUp.Net.Mail.Message ret = null;
            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            if (accountInfo != null)
            {
                if (accountInfo.MailAccountType == AccountType.POP3)
                {
                    ret = this._pop3Controller.GetMessage(mailMessage);
                }
                else if (accountInfo.MailAccountType == AccountType.IMAP)
                {
                    ret = this._imap4Controller.GetMessage(mailbox, mailMessage);
                }
            }

            return ret;
        }

        /// <summary>
        /// Method for get the message count.
        /// </summary>
        /// <param name="mailbox">The mailbox name.</param>
        /// <returns>The message count number.</returns>
        public int GetMessageCount(string mailbox)
        {
            int messageCount = 0;
            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            if (accountInfo != null)
            {
                if (accountInfo.MailAccountType == AccountType.POP3)
                {
                    messageCount = this._pop3Controller.GetMessageCount();
                }
                else if (accountInfo.MailAccountType == AccountType.IMAP)
                {
                    messageCount = this._imap4Controller.GetMessageCount(mailbox);
                }
            }
            return messageCount;
        }

        /// <summary>
        /// Method for connect to the right protocol 
        /// using the default account information.
        /// </summary>
        public void Connect()
        {
            this.Disconnect();

            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            if (accountInfo != null)
            {
                if (accountInfo.MailAccountType == AccountType.POP3)
                {
                    this._pop3Controller.Connect(accountInfo);
                }
                else if (accountInfo.MailAccountType == AccountType.IMAP)
                {
                    this._imap4Controller.Connect(accountInfo);
                }
            }
        }

        /// <summary>
        /// Method for disconnect the current protocol.
        /// </summary>
        public void Disconnect()
        {
            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

            if (accountInfo != null)
            {
                if (accountInfo.MailAccountType == AccountType.POP3)
                {
                    this._pop3Controller.Disconnect();
                }
                else if (accountInfo.MailAccountType == AccountType.IMAP)
                {
                    this._imap4Controller.Disconnect();
                }
            }
        }

        #endregion

        // Region for Mailboxes folders.
        #region MailBoxes

        /// <summary>
        /// Get all mailbox folders.
        /// </summary>
        /// <returns></returns>
        public MailboxCollection GetAllMailbox()
        {
            AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();
            MailboxCollection ret = new MailboxCollection();

            if (accountInfo != null)
            {
                if (accountInfo.MailAccountType == AccountType.POP3)
                {
                    // TODO... check if it's possible.
                    //ret = this._pop3Controller.();
                }
                else if (accountInfo.MailAccountType == AccountType.IMAP)
                {
                    ret = this._imap4Controller.GetAllMailbox(Constants.Inbox);
                }
            }
            return ret;
        }

        #endregion

    }
}
