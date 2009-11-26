using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ActiveUp.Net.Mail;

/// <summary>
/// Summary description for ImapController
/// </summary>
public class ImapController
{
    #region Attributes

    /// <summary>
    /// Attribute for make connection to IMAP.
    /// </summary>
    private Imap4Client _imap4Client;

    /// <summary>
    /// Attributes for store messages and headers respectively.
    /// </summary>
    private List<Message> _listMessageInbox;
    private List<MailHeader> _listHeaderInbox;

    #endregion

    #region Properties

    /// <summary>
    ///Represents the list of message headers 
    /// </summary>
    public List<MailHeader> ListHeaderInbox
    {
        get { return _listHeaderInbox; }
        set { _listHeaderInbox = value; }
    }

    /// <summary>
    /// Represents the Imap Client
    /// </summary>
    public Imap4Client Imap4Client
    {
        get { return _imap4Client; }
        set { _imap4Client = value; }
    }

    /// <summary>
    /// Represents the list of message
    /// </summary>
    public List<Message> ListMessageInbox
    {
        get { return _listMessageInbox; }
        set { _listMessageInbox = value; }
    }
    #endregion

    #region Constructor

    /// <summary>
    /// The Constructor
    /// </summary>
    public ImapController()
    {
        this._listMessageInbox = new List<Message>();
        this.ListHeaderInbox = new List<MailHeader>();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Connect the imap client.
    /// </summary>
    /// <param name="accountInfo">The information account</param>
    public void Connect(AccountSettings.AccountInfo accountInfo)
    {
        if (this._imap4Client == null || !this._imap4Client.IsConnected)
        {
            if (accountInfo != null && accountInfo.AccType == AccountType.POP3)
            {
                this._imap4Client = new Imap4Client();

                int port = accountInfo.PortIncomingServer;
                bool ssl = accountInfo.IsIncomeSecureConnection;
                string serverName = accountInfo.IncomingNameMailServer;
                string user = accountInfo.EmailAddress;
                string password = accountInfo.Password;
                bool useInPort = accountInfo.PortIncomingChecked;

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
    }

    /// <summary>
    /// Disconnect the imap client.
    /// </summary>
    public void Disconnect()
    {
        if (this._imap4Client != null && this._imap4Client.IsConnected)
        {
            this._imap4Client.Disconnect();
        }   
    }

    /// <summary>
    /// Method for retrieve the mail messages for IMAP protocol.
    /// </summary>
    /// <param name="mailBox">The mail box</param>
    /// <returns>The mail messages</returns>
    public List<Message> RetrieveMessages(string mailBox)
    {
        Mailbox inbox = this._imap4Client.SelectMailbox(mailBox);
        Fetch fetch = inbox.Fetch;
        int messageCount = inbox.MessageCount;
        this._listMessageInbox.Clear();

        for (int i = 1; i <= messageCount; i++)
        {
            Message msg = fetch.MessageObject(i);            
            this._listMessageInbox.Add(msg);
        }
        return this._listMessageInbox;
    }

    /// <summary>
    /// Method for retrieve the mail headers for IMAP protocol.
    /// </summary>
    /// <param name="mailBox">The mail box</param>
    /// <returns>The mail headers</returns>
    public List<MailHeader> RetrieveHeaders(string mailBox)
    {
        Mailbox inbox = this._imap4Client.SelectMailbox(mailBox);
        Fetch fetch = inbox.Fetch;
        int messageCount = inbox.MessageCount;
        this._listMessageInbox.Clear();       

        for (int i = 1; i <= messageCount; i++)
        {
            MailHeader mailHeader = new MailHeader();
            Header header = fetch.HeaderObject(i);
            mailHeader.Index = i.ToString();
            mailHeader.FillHeader(header);
            this._listHeaderInbox.Add(mailHeader);            
        }
        return this._listHeaderInbox;
    }

    /// <summary>
    /// Gets a message through its MessageId
    /// </summary>
    /// <param name="id">The Message id</param>
    /// <returns>A message</returns>
    public Message getMessage(string id)
    {
        foreach (Message msg in this.ListMessageInbox)
        {
            if (msg.MessageId == id)
            {
                return msg;
            }
        }
        return null;
    }
    /// <summary>
    /// Gets a message through its index
    /// </summary>
    /// <param name="id">The index</param>
    /// <returns>A message</returns>
    public Message getMessageByIndex(int index)
    {
        Mailbox inbox = this._imap4Client.SelectMailbox("inbox");
        Fetch fetch = inbox.Fetch;
        return fetch.MessageObject(index);
    }

    #endregion
}
