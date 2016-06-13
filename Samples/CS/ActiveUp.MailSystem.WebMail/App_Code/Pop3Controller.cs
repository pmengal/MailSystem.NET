using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ActiveUp.Net.Mail;
using System.ComponentModel;
using System.Collections.Generic;

/// <summary>
/// This controller is used for retrieve, delete and manipulate
/// mail messages for POP3 protocol
/// </summary>

public class Pop3Controller
{
    #region Attributes

    /// <summary>
    /// Attribute for make connection to POP.
    /// </summary>
    private Pop3Client _pop3Client;

    /// <summary>
    /// Attributes for store messages and headers respectively.
    /// </summary>
    private List<Message> ListMessageInbox;
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
    /// Represents the list of messages
    /// </summary>
    public List<Message> ListMessageInbox1
    {
        get { return ListMessageInbox; }
        set { ListMessageInbox = value; }
    }    

    /// <summary>
    /// Represents the POP client
    /// </summary>
    public Pop3Client Pop3Client
    {
        get { return _pop3Client; }
        set { _pop3Client = value; }
    }

    #endregion

    #region Contructors

    public Pop3Controller()
	{
        this.ListMessageInbox = new List<Message>();
        this.ListHeaderInbox = new List<MailHeader>();
    }   

    public Pop3Controller(AccountSettings.AccountInfo accountInfo)
    {
        //this.Connect(accountInfo);
        this.ListMessageInbox = new List<Message>();
        this.ListHeaderInbox = new List<MailHeader>();
    }

    #endregion

    #region Methods
    
    /// <summary>
    /// Connect the pop client.
    /// </summary>
    /// <param name="accountInfo">The information account</param>
    public void Connect(AccountSettings.AccountInfo accountInfo)
    {
      

            if (this._pop3Client == null || !this._pop3Client.IsConnected)
            {
                if (accountInfo != null && accountInfo.AccType == AccountType.POP3)
                {
                    this._pop3Client = new Pop3Client();

                    int port = accountInfo.PortIncomingServer;
                    bool ssl = accountInfo.IsIncomeSecureConnection;
                    string serverName = accountInfo.IncomingNameMailServer;
                    string user = EncryptDescript.CriptDescript(accountInfo.EmailAddress);
                    string password = EncryptDescript.CriptDescript(accountInfo.Password);
                    bool useInPort = accountInfo.PortIncomingChecked;

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
        }

    /// <summary>
    /// Clear the list of message
    /// </summary>
    public void clearList()
    {
        this.ListMessageInbox.Clear();
    }

    /// <summary>
    /// Add a Message into list
    /// </summary>
    /// <param name="msg">The Message object</param>
    public void add(Message msg)
    {
        this.ListMessageInbox.Add(msg);
    }

    /// <summary>
    /// Disconnect the pop client.
    /// </summary>
    public void Disconnect()
    {
        if (this._pop3Client != null && this._pop3Client.IsConnected)
        {
            this._pop3Client.Disconnect();
        }
    }

    /// <summary>
    /// Gets the message count into pop client
    /// </summary>
    /// <returns>The message count</returns>
    public int getMessageCount()
    {
        return this._pop3Client.MessageCount;
    }
    
    /// <summary>
    ///  Method for retrieve the mail messages for POP protocol
    /// </summary>
    /// <returns>A list of messages</returns>
    public List<Message> RetrieveMessages()
    {
        this.ListMessageInbox.Clear();

        Message msg = null;        
        int messageCount = this._pop3Client.MessageCount;
        //messageCount = (int)messageCount / 3;

        for (int i = 1; i <= messageCount; i = i + 1)
        {
            msg = this._pop3Client.RetrieveMessageObject(i);
            this.add(msg);            
        }

        return this.ListMessageInbox;
    }

    /// <summary>
    /// Method for retrieve the mail headers for POP protocol.
    /// </summary>   
    /// <returns>The mail headers</returns>
    public List<MailHeader> RetrieveHeaders()
    {       
        this.ListMessageInbox.Clear();
        
        int messageCount = this._pop3Client.MessageCount;        

        for (int i = 1; i <= messageCount; i = i + 1)
        {
            MailHeader mailHeader = new MailHeader();
            Header header = new Header();
            header = this._pop3Client.RetrieveHeaderObject(i);
            mailHeader.Index = i.ToString();
            mailHeader.FillHeader(header);
            this._listHeaderInbox.Add(mailHeader);
        }

        return this.ListHeaderInbox;
    }

    /// <summary>
    /// Method for delete a message into pop client
    /// </summary>
    /// <param name="messageId">The id of the message</param>
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
    /// Method for get the message count.
    /// </summary>
    /// <returns>The message count number.</returns>
    public int GetMessageCount()
    {
        return this._pop3Client.MessageCount;
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
            if(msg.MessageId == id) {
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
    public Message getMessageByIndex(int id)
    {
        return this._pop3Client.RetrieveMessageObject(id);
    }

    #endregion
}
