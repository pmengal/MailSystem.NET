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

    public enum AccountType
    {
        POP3, IMAP
    }

    [Serializable]
    public class AccountSettings : IList<AccountSettings.AccountInfo>
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

            #region gets e sets

            public bool PortIncomingChecked
            {
                get { return portIncomingChecked; }
                set { portIncomingChecked = value; }
            }

            public bool PortOutgoingChecked
            {
                get { return portOutgoingChecked; }
                set { portOutgoingChecked = value; }
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

        private List<AccountInfo> m_aAccInfoList;

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountSettings()
        {
            m_aAccInfoList = new List<AccountInfo>();
        }

        /// <summary>
        /// Get a list of account information
        /// </summary>
        public AccountInfo[] Accounts
        {
            get
            {
                if (m_aAccInfoList != null)
                    return m_aAccInfoList.ToArray();

                return null;
            }
        }

        /// <summary>
        /// Add an account to the list.
        /// </summary>
        /// <param name="ai"></param>
        public Guid AddAccount(AccountInfo ai)
        {
            if (m_aAccInfoList == null)
                m_aAccInfoList = new List<AccountInfo>();

            ai.ID = Guid.NewGuid();
            m_aAccInfoList.Add(ai);

            return ai.ID;
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
                AccountSettings cfg = new AccountSettings();
                Save(file, cfg);

                return cfg;
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



        #region IList<AccountInfo> Members

        public int IndexOf(AccountSettings.AccountInfo item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, AccountSettings.AccountInfo item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public AccountSettings.AccountInfo this[int index]
        {
            get
            {
                return Accounts[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<AccountInfo> Members

        public void Add(AccountSettings.AccountInfo item)
        {
            this.m_aAccInfoList.Add(item);
        }

        public void Clear()
        {
            this.m_aAccInfoList.Clear();
        }

        public bool Contains(AccountSettings.AccountInfo item)
        {
            return this.m_aAccInfoList.Contains(item);
        }

        public void CopyTo(AccountSettings.AccountInfo[] array, int arrayIndex)
        {
            this.m_aAccInfoList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.m_aAccInfoList.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(AccountSettings.AccountInfo item)
        {
            return this.m_aAccInfoList.Remove(item);
        }

        #endregion

        #region IEnumerable<AccountInfo> Members

        public IEnumerator<AccountSettings.AccountInfo> GetEnumerator()
        {
            return this.m_aAccInfoList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.m_aAccInfoList.GetEnumerator();
        }

        #endregion
    }