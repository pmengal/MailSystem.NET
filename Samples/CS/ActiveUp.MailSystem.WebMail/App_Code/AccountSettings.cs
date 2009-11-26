using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Windows.Forms;

/// <summary>
/// Summary description for AccountSettings
/// </summary>
///


/// <summary>
/// Account Types.
/// </summary>
public enum AccountType
{
    POP3, IMAP
}

[Serializable]
public class AccountSettings
{
    // Private Members and Public properties

    [Serializable]
    public class AccountInfo
    {

        private string emailAddress;
        private string password;
        private string displayName;
        private string incomingMailServer;
        private string outgoingServer;
        private string loginId;
        private int portIncomingServer;
        private int portOutgoingServer;
        private bool isIncomeSecureConnection;
        private bool isOutgoingSecureConnection;
        private bool isOutgoingWithAuthentication;
        private bool portIncomingChecked;
        private bool portOutgoingChecked;
        
        private Guid _AccountID;
        private AccountType _AccType;
        private string incomingNameMailServer;
                 
        #region gets and sets

        public bool PortIncomingChecked
        {
            get { return portIncomingChecked; }
            set { portIncomingChecked = value; }
        }

        public string IncomingNameMailServer
        {
            get { return incomingNameMailServer; }
            set { incomingNameMailServer = value; }
        }

        public bool PortOutgoingChecked
        {
            get { return portOutgoingChecked; }
            set { portOutgoingChecked = value; }
        }

        public AccountType AccType
        {
            get { return _AccType; }
            set { _AccType = value; }
        }


        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public string IncomingMailServer
        {
            get { return incomingMailServer; }
            set { incomingMailServer = value; }
        }

        public string OutgoingServer
        {
            get { return outgoingServer; }
            set { outgoingServer = value; }
        }

        public string LoginId
        {
            get { return loginId; }
            set { loginId = value; }
        }

        public int PortIncomingServer
        {
            get { return portIncomingServer; }
            set { portIncomingServer = value; }
        }

        public int PortOutgoingServer
        {
            get { return portOutgoingServer; }
            set { portOutgoingServer = value; }
        }

        public bool IsIncomeSecureConnection
        {
            get { return isIncomeSecureConnection; }
            set { isIncomeSecureConnection = value; }
        }

        public bool IsOutgoingSecureConnection
        {
            get { return isOutgoingSecureConnection; }
            set { isOutgoingSecureConnection = value; }
        }

        public bool IsOutgoingWithAuthentication
        {
            get { return isOutgoingWithAuthentication; }
            set { isOutgoingWithAuthentication = value; }
        }

        public Guid ID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }

        #endregion
    }

    /// <summary>
    /// Represents the account info
    /// </summary>
    private AccountInfo acc_Info;
    
    /// <summary>
    ///Account Info 
    /// </summary>
    public AccountInfo Acc_Info
    {
        get { return acc_Info; }
        set { acc_Info = value; }
    } 

    /// <summary>
    /// Default constructor
    /// </summary>
    public AccountSettings()
    {
        this.acc_Info = new AccountInfo();
    }

    /// <summary>
    /// Saves the Email accont settings to a file.
    /// </summary>
    /// <param name="file">The file path to save to.</param>
    /// <param name="c">AccountSettings object.</param>
    public static void Save(string file, AccountSettings c)
    {
        try
        {
            if (File.Exists(file))
                File.Delete(file);

            // TODO: MAke this binary
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(c.GetType());
            StreamWriter writer = File.CreateText(file);
            xs.Serialize(writer, c);
            writer.Flush();
            writer.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves the Email Account Settings from a file.
    /// </summary>
    /// <param name="file">File path to load from.</param>
    /// <returns>An instance of AccountSettings object.</returns>
    public static AccountSettings Load(string file)
    {
        if (!File.Exists(file))
        {
            return null;
        }

        // TODO: MAke this binary
        System.Xml.Serialization.XmlSerializer xs
           = new System.Xml.Serialization.XmlSerializer(
              typeof(AccountSettings));
        StreamReader reader = File.OpenText(file);
        AccountSettings c = new AccountSettings();
        try
        {
            c = (AccountSettings)xs.Deserialize(reader);
        }
        catch (Exception)
        {

        }
        reader.Close();
        return c;
    }
    
}