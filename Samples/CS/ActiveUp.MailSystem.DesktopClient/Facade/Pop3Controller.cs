using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ActiveUp.Net.Mail;

namespace ActiveUp.MailSystem.DesktopClient
{
    /// <summary>
    /// This controller is used for retrieve, delete and manipulate
    /// mail messages for POP3 protocol
    /// </summary>
    public class Pop3Controller
    {

        /// <summary>
        /// Attribute for Pop3Client.
        /// </summary>
        private Pop3Client _pop3Client;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="accountInfo">The default account information.</param>
        public Pop3Controller(AccountSettings.AccountInfo accountInfo)
        {
            this.Connect(accountInfo);
        }

        /// <summary>
        /// Connect the pop3 client.
        /// </summary>
        /// <param name="accountInfo">The account information.</param>
        public void Connect(AccountSettings.AccountInfo accountInfo)
        {
            if (accountInfo != null && accountInfo.MailAccountType == AccountType.POP3)
            {
                this._pop3Client = new Pop3Client();

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
                        this._pop3Client.ConnectSsl(serverName, port, user, password);
                    }
                    else
                    {
                        this._pop3Client.ConnectSsl(serverName, user, password);
                    }
                }
                else
                {
                    if (useInPort)
                    {
                        this._pop3Client.Connect(serverName, port, user, password);
                    }
                    else
                    {
                        this._pop3Client.Connect(serverName, user, password);
                    }
                }
            }
        }

        /// <summary>
        /// Method for disconnect from Pop3 protocol.
        /// </summary>
        public void Disconnect()
        {
            if (this._pop3Client != null && this._pop3Client.IsConnected)
            {
                this._pop3Client.Disconnect();
            }
        }

        /// <summary>
        /// Method for retrieve the mail messages for POP3 protocol.
        /// </summary>
        /// <param name="beginIndex">The begin index for retrieve the message.</param>
        /// <param name="increment">The increment for begin index for retrieve messages.</param>
        /// <returns>The mail messages.</returns>
        public BindingList<MailMessage> RetrieveMessages(int beginIndex, int increment)
        {
            BindingList<MailMessage> ret = new BindingList<MailMessage>();

            MailMessage mailMessage;
            Header header;

            int messageCount = this._pop3Client.MessageCount;
            for (int i = beginIndex; i <= beginIndex + increment; i++)
            {
                if (i <= messageCount)
                {
                    header = this._pop3Client.RetrieveHeaderObject(i);
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
        public void DeleteMessage(string messageId)
        {
            int index = 0;
            int messageCount = this._pop3Client.MessageCount;
            for (int i = 1; i <= messageCount; i++)
            {
                Header header = this._pop3Client.RetrieveHeaderObject(i);
                if (header.MessageId.Equals(messageId))
                {
                    index = i;
                    break;
                }
            }

            if (index > 0)
            {
                this._pop3Client.DeleteMessage(index);
            }
        }

        /// <summary>
        /// Get a mail message basend on message identifier.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        /// <returns>The ActiveUp.Net.Mail.Message object.</returns>
        public ActiveUp.Net.Mail.Message GetMessage(MailMessage mailMessage)
        {
            ActiveUp.Net.Mail.Message ret = null;
            ActiveUp.Net.Mail.Message message = null;
            int messageCount = this._pop3Client.MessageCount;

            int upIndex = mailMessage.BaseIndex;
            int downIndex = mailMessage.BaseIndex;

            // search up and down.
            while (downIndex > 0 || upIndex <= messageCount)
            {

                if (downIndex > 0 && downIndex <= messageCount)
                {
                    message = this._pop3Client.RetrieveMessageObject(downIndex);
                    if (message.MessageId != null && message.MessageId.Equals(mailMessage.Id))
                    {
                        ret = message;
                        break;
                    }
                }

                if (upIndex <= messageCount && upIndex > 0)
                {
                    message = this._pop3Client.RetrieveMessageObject(upIndex);
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
        /// <returns>The message count number.</returns>
        public int GetMessageCount()
        {
            return this._pop3Client.MessageCount;
        }
        
        //public string[] GetAllMailBoxes()
        //{
        //    //this._pop3Client.
        //}

    }
}
