using System;
using System.Collections.Generic;
using System.Text;
using ActiveUp.Net.Mail;
using System.ComponentModel;

namespace ActiveUp.MailSystem.DesktopClient
{
    /// <summary>
    /// This controller is used for retrieve, delete and manipulate
    /// mail messages for IMAP4 protocol
    /// </summary>
    public class Imap4Controller
    {

        /// <summary>
        /// The Imap4Client attribute.
        /// </summary>
        private Imap4Client _imap4Client;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="accountInfo">The default account information.</param>
        public Imap4Controller(AccountSettings.AccountInfo accountInfo)
        {
            this.Connect(accountInfo);
        }

        /// <summary>
        /// Connect the imap4 client.
        /// </summary>
        /// <param name="accountInfo">The account information.</param>
        public void Connect(AccountSettings.AccountInfo accountInfo)
        {
            if (accountInfo != null && accountInfo.MailAccountType == AccountType.IMAP)
            {
                this._imap4Client = new Imap4Client();

                int port = accountInfo.InPort;
                bool ssl = accountInfo.IncomingIsSSL;
                string serverName = accountInfo.IncomingServerName;
                string user = accountInfo.EmailAddress;
                string password = accountInfo.Password;
                bool useInPort = accountInfo.InPortEnabled;

                if (ssl)
                {
                    if (useInPort)
                    {
                        this._imap4Client.ConnectSsl(serverName, port);
                    }
                    else
                    {
                        this._imap4Client.ConnectSsl(serverName);
                    }
                }
                else
                {
                    if (useInPort)
                    {
                        this._imap4Client.Connect(serverName, port);
                    }
                    else
                    {
                        this._imap4Client.Connect(serverName);
                    }
                }

                this._imap4Client.Login(user, password);
            }
        }

        /// <summary>
        /// Method for disconnect from Imap4 protocol.
        /// </summary>
        public void Disconnect()
        {
            if (this._imap4Client != null && this._imap4Client.IsConnected)
            {
                this._imap4Client.Disconnect();
            }
        }

        /// <summary>
        /// Method for retrieve the mail messages for IMAP4 protocol.
        /// </summary>
        /// <param name="mailbox">The mailbox for retrieve messages.</param>
        /// <param name="beginIndex">The begin index for retrieve the messages.</param>
        /// <param name="increment">The increment for begin index for retrieve the messages.</param>
        /// <returns>The mail messages.</returns>
        public BindingList<MailMessage> RetrieveMessages(string mailbox, int beginIndex, int increment)
        {
            BindingList<MailMessage> ret = new BindingList<MailMessage>();

            MailMessage mailMessage;
            Header header;

            Mailbox inbox = this._imap4Client.SelectMailbox(mailbox);
            Fetch fetch = inbox.Fetch;
            int messageCount = inbox.MessageCount;
            for (int i = beginIndex; i <= beginIndex + increment; i++)
            {
                if (i <= messageCount)
                {
                    header = fetch.HeaderObject(i);
                    mailMessage = new MailMessage();
                    mailMessage.FillMessage(header);
                    mailMessage.BaseIndex = i;
                    ret.Add(mailMessage);
                }
            }
            return ret;
        }

        /// <summary>
        /// Delete a message basend on message identifier.
        /// </summary>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="mailbox">The mailbox for retrieve messages.</param>
        public void DeleteMessage(string messageId, string mailbox)
        {
            Mailbox inbox = this._imap4Client.SelectMailbox(mailbox);
            Fetch fetch = inbox.Fetch;
            Header header;

            int index = 0;
            int messageCount = inbox.MessageCount;
            for (int i = 1; i <= messageCount; i++)
            {
                header = fetch.HeaderObject(i);

                if (header.MessageId.Equals(messageId))
                {
                    index = i;
                    break;
                }
            }
            
            if (index > 0)
            {
                inbox.DeleteMessage(index, false);
            }
        }

        /// <summary>
        /// Get a mail message basend on message identifier.
        /// </summary>
        /// <param name="mailbox">The mailbox for retrieve the message.</param>
        /// <param name="mailMessage">The mail message.</param>
        /// <returns>The ActiveUp.Net.Mail.Message object.</returns>
        public ActiveUp.Net.Mail.Message GetMessage(string mailbox, MailMessage mailMessage)
        {
            ActiveUp.Net.Mail.Message ret = null;
            ActiveUp.Net.Mail.Message message = null;
            Mailbox inbox = this._imap4Client.SelectMailbox(mailbox);
            Fetch fetch = inbox.Fetch;
            int messageCount = inbox.MessageCount;

            int upIndex = mailMessage.BaseIndex;
            int downIndex = mailMessage.BaseIndex;

            // search up and down.
            while (downIndex > 0 || upIndex <= messageCount)
            {

                if (downIndex > 0 && downIndex <= messageCount)
                {
                    message = fetch.MessageObject(downIndex);
                    if (message.MessageId != null && message.MessageId.Equals(mailMessage.Id))
                    {
                        ret = message;
                        break;
                    }
                }

                if (upIndex <= messageCount && upIndex > 0)
                {
                    message = fetch.MessageObject(upIndex);
                    if (message.MessageId != null && message.MessageId.Equals(mailMessage.Id))
                    {
                        ret = message;
                        break;
                    }                    
                }

                downIndex--;
                upIndex++;
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
            Mailbox inbox = this._imap4Client.SelectMailbox(mailbox);
            return inbox.MessageCount;
        }

        /// <summary>
        /// Method for add a mailbox.
        /// </summary>
        /// <param name="name">The mailbox name.</param>
        public void AddMailbox(string name)
        {
            // We create a mailbox.
            this._imap4Client.CreateMailbox(name);
        }

        /// <summary>
        /// Method for rename a mailbox.
        /// </summary>
        /// <param name="oldName">The old mailbox name.</param>
        /// <param name="newName">The new mailbox name.</param>
        public void RenameMailbox(string oldName, string newName)
        {
            // Rename it.
            this._imap4Client.RenameMailbox(oldName, newName);
        }

        /// <summary>
        /// Method for delete a mailbox.
        /// </summary>
        /// <param name="name">The mailbox name.</param>
        public void DeleteMailbox(string name)
        {
            // We remove all messages contained in mailbox
            Mailbox mailbox = this._imap4Client.SelectMailbox(name);
            mailbox.Empty(false);

            // We delete the mailbox from the account
            this._imap4Client.DeleteMailbox(name);
        }

        /// <summary>
        /// Get all mailbox from a base mailbox.
        /// </summary>
        /// <param name="mailboxBase">The base mail box.</param>
        /// <returns>A MailboxCollection object.</returns>
        public MailboxCollection GetAllMailbox(string mailboxBase)
        {
            return this._imap4Client.GetMailboxes(mailboxBase, "*");
        }

    }
}
