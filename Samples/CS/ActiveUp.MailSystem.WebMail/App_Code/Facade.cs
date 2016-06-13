using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using ActiveUp.Net.Mail;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// This class represents a Facade for business logic manipulation.
/// It is used for allow send emails by SMTP, receive email by POP or IMAP.
/// Also it is used to manage the account settings information.
/// </summary>
public class Facade
{
    //Region for attributes
    #region Attributes 

    /// <summary>
    /// Attribute for the Facade singleton instance.
    /// </summary>
    private static Facade facade;

    /// <summary>
    /// Attributes for control pop, imap and smtp.
    /// </summary>
    
    private Pop3Controller popController;
    private ImapController _imapController;
    private SmtpController smtpController;
    private bool _changeImcoming;


    /// <summary>
    /// Attribute for the Account Settings.
    /// </summary>
    private AccountSettings _accSettings;

    /// <summary>
    /// The last email displayed on MailShow page
    /// </summary>
    private Message _currentMailShow;

    /// <summary>
    /// Gets or sets the current mail.
    /// </summary>
    /// 

    #endregion

    #region Properties


    public AccountSettings AccSettings
    {
        get { return _accSettings; }
        set { _accSettings = value; }
    }

    public Message CurrentMailShow
    {
        get { return _currentMailShow; }
        set { _currentMailShow = value; }
    }

    public bool ChangeImcoming
    {
        get { return _changeImcoming; }
        set { _changeImcoming = value; }
    }
   
    #endregion

    //Region for singleton
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

    #region Constructor

    public Facade()
	{
        this._accSettings = AccountSettings.Load(Constants.ACCOUNT_FILE_NAME_SETTINGS);
        this.popController = new Pop3Controller();
        this._imapController = new ImapController();
        this.smtpController = new SmtpController();
        this._changeImcoming = false;
    }

    #endregion

    /// <summary>
    /// Method for gets the default account information.
    /// </summary>
    /// <returns>The AccountInfo object.</returns>
    public AccountSettings.AccountInfo GetDefaultAccountInfo()
    {
        if (_accSettings != null)
        {
            return this._accSettings.Acc_Info;
        }
        else { return null;}
    }  
    
    /// <summary>
    /// Method for gets all Headers message.
    /// </summary>
    /// <returns>A List of MailHeader</returns>
    public List<MailHeader> Retrieves()
    {
        string incoming = this.GetDefaultAccountInfo().IncomingMailServer;

        if (incoming == Constants.POP3)
        {            
            return this.popController.RetrieveHeaders();
        }
        else
        {
            return this._imapController.RetrieveHeaders(Constants.MAILBOX);
        }
    }
     

    /// <summary>
    /// Method for gets all Messages.
    /// </summary>
    /// <returns>A List of Message</returns>
    public List<Message> getListMessages() {
        
        AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

        if (Constants.POP3 == accountInfo.IncomingMailServer)
        {
            return this.popController.ListMessageInbox1;
        }
        else
        {
            return this._imapController.ListMessageInbox;
        }
    }
    /// <summary>
    /// Method for sets an Account Information.
    /// </summary>
    /// <param name="acc">The Account information</param>
    public void setAccountInfo(AccountSettings.AccountInfo acc)
    {
        if (this._accSettings != null)
        {
        this._accSettings.Acc_Info = acc;
        }
        else 
        {
            this.AccSettings = new AccountSettings();
            this.AccSettings.Acc_Info = acc;
        }
    }

    /// <summary>
    /// Method for save Account Settings.
    /// </summary>
    public void SaveAccountSettings()
    {
        AccountSettings.Save(Constants.ACCOUNT_FILE_NAME_SETTINGS, this._accSettings);       
    }

    /// <summary>
    ///Method for reconnected the imcoming mail.
    /// </summary>
    public void ReConnect()
    {
        this.Disconnect();
        this.Connect();
    }

    public void deleteAccountSettings() {
    
        if(File.Exists(Constants.ACCOUNT_FILE_NAME_SETTINGS))
        {
        File.Delete(Constants.ACCOUNT_FILE_NAME_SETTINGS);
        }
    }

    /// <summary>
    /// Method for connect the imcoming mail, Pop or Imap.
    /// </summary>
    public void Connect()
    {
        AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

        if (accountInfo != null)
        {
            if (accountInfo.IncomingMailServer == Constants.POP3)
            {
                if (popController.Pop3Client == null || !popController.Pop3Client.IsConnected) this.popController.Connect(accountInfo);
                else
                {
                    this.Disconnect();
                    this.popController.Connect(accountInfo);
                }
            }
            else {
                if (_imapController.Imap4Client == null || !_imapController.Imap4Client.IsConnected) this._imapController.Connect(accountInfo);
                else
                {
                    this.Disconnect();
                    this._imapController.Connect(accountInfo);
                }
            }
        }
    }

    /// <summary>
    ///Method for disconnect the imcoming mail, Pop or Imap.
    /// </summary>
    public void Disconnect()
    {
        AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();
        
       
        if (accountInfo != null)
        {
            if (accountInfo.IncomingMailServer == Constants.POP3 && (this.popController.Pop3Client != null && this.popController.Pop3Client.IsConnected))
            {
                this.popController.Disconnect();
            }
            else if(this._imapController.Imap4Client != null && this._imapController.Imap4Client.IsConnected)
            {
                this._imapController.Disconnect();
            }
        }
        }


    /// <summary>
    /// Method for gets a message through an id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A message</returns>
    public Message getMessage(string id) {

        AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

        if (accountInfo.IncomingMailServer == Constants.POP3)
        {
            return this.popController.getMessage(id);
        }
        else
        {
            return this._imapController.getMessage(id);
        }
    }

    /// <summary>
    /// Method for send mail.
    /// </summary>
    /// <param name="message">The message will be send</param>
    public void sendMail(Message message)
    {
        smtpController.SendMail(message, _accSettings);
    }

    /// <summary>
    /// Method for gets a message through its location index.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>A message</returns>
    public Message getMessageByIndex(int index)
    {
        AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

        if (accountInfo.IncomingMailServer == Constants.POP3)
        {
            return this.popController.getMessageByIndex(index);
        }
        else
        {
            return this._imapController.getMessageByIndex(index);
        }
    }

    public List<MailHeader> GetSentMailList()
    {

        string path = Path.Combine(Path.GetTempPath(), Constants.SENT_MAILS_FOLDER);
        List<MailHeader> ret = null;

        if (Directory.Exists(path))
        {
            string[] arquivos = Directory.GetFiles(path);
            ret = new List<MailHeader>();

            foreach (string name in arquivos)
            {
                Message msg = Parser.ParseMessageFromFile(Path.Combine(path, name));
                MailHeader header = new MailHeader();
                header.FillHeader(msg, name);
                ret.Add(header);
            }
        }

        return ret;
    }


    /// <summary>
    /// Method for gets all Headers message.
    /// </summary>
    /// <returns>A list of headers</returns>
    public object getListHeaders()
    {
        AccountSettings.AccountInfo accountInfo = this.GetDefaultAccountInfo();

        if (Constants.POP3 == accountInfo.IncomingMailServer)
        {
            return this.popController.ListHeaderInbox;
        }
        else
        {
            return this._imapController.ListHeaderInbox;
        }
    }

    public Message getSentMail(string id)
    {
        string path = Path.Combine(Path.GetTempPath(), Constants.SENT_MAILS_FOLDER);        
        Message ret = null;

        if (Directory.Exists(path))
        {
            string[] arquivos = Directory.GetFiles(path);
            

            foreach (string name in arquivos)
            {
                string fileName = System.IO.Path.GetFileName(name);

                if (fileName == id)
                {
                    ret = Parser.ParseMessageFromFile(Path.Combine(path, fileName));
                    ret.From.Email = String.Empty;
                    break;
                }                               
            }
        }

        return ret;
    }
}
