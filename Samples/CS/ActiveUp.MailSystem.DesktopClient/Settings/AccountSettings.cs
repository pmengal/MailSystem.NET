using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ActiveUp.Net.Mail;

namespace ActiveUp.MailSystem.DesktopClient
{

    /// <summary>
    /// Account Types.
    /// </summary>
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
            private string _UserName;
            private string _Password;
            private bool _IncomingIsSSL;
            private bool _OutgoingIsSSL;
            private AccountType _AccType;
            private string _InServer;
            private int _InPort;
            private bool _InPortEnabled;
            private string _OutServer;
            private int _OutPort;
            private bool _OutPortEnabled;
            private string _EmailAddress;
            private string _LoginName;
            private bool _RememberPassword;
            private EncryptionType _EncType;
            private bool _OutgoingNeedsAuthentication;
            private Guid _AccountID;

            /// <summary>
            /// Account Username
            /// </summary>
            public string UserName
            {
                get { return _UserName;  }
                set { _UserName = value; }
            }

            /// <summary>
            /// Account Password
            /// </summary>
            public string Password
            {
                get { return _Password; }
                set { _Password = value; }
            }

            /// <summary>
            /// Is the Incoming server for this account requests SSL.
            /// </summary>
            public bool IncomingIsSSL
            {
                get { return _IncomingIsSSL; }
                set { _IncomingIsSSL = value; }
            }

            /// <summary>
            /// Is the Outgoing server for this account requests SSL.
            /// </summary>
            public bool OutgoingIsSSL
            {
                get { return _OutgoingIsSSL; }
                set { _OutgoingIsSSL = value; }
            }

            /// <summary>
            /// The incoming Port number
            /// </summary>
            public int InPort
            {
                get { return _InPort; }
                set { _InPort = value; }
            }

            /// <summary>
            /// The incoming Port is enabled value.
            /// </summary>
            public bool InPortEnabled
            {
                get { return _InPortEnabled; }
                set { _InPortEnabled = value; }
            }

            /// <summary>
            /// The outgoing Port is enabled value.
            /// </summary>
            public bool OutPortEnabled
            {
                get { return _OutPortEnabled; }
                set { _OutPortEnabled = value; }
            }

            /// <summary>
            /// The outgoing Port number.
            /// </summary>
            public int OutPort
            {
                get { return _OutPort; }
                set { _OutPort = value; }
            }

            /// <summary>
            /// The email account type
            /// </summary>
            public AccountType MailAccountType
            {
                get { return _AccType; }
                set { _AccType = value; }
            }


            /// <summary>
            /// Email address
            /// </summary>
            public string EmailAddress
            {
                get { return _EmailAddress; }
                set { _EmailAddress = value; }
            }


            /// <summary>
            /// Login name for account.
            /// </summary>
            public string LoginName
            {
                get { return _LoginName; }
                set { _LoginName = value; }
            }

            /// <summary>
            /// Is the password saved.
            /// </summary>
            public bool RememberPassword
            {
                get { return _RememberPassword; }
                set { _RememberPassword = value; }
            }

            /// <summary>
            /// Encryption Type for connection.
            /// </summary>
            public EncryptionType EncType
            {
                get { return _EncType; }
                set { _EncType = value; }
            }

            /// <summary>
            /// Flag to indicate if the outgoing server needs authentication
            /// </summary>
            public bool OutgoingServerNeedsAuthentication
            {
                get { return _OutgoingNeedsAuthentication; }
                set { _OutgoingNeedsAuthentication = value; }
            }

            /// <summary>
            /// Name of Incoming mail server.
            /// </summary>
            public string IncomingServerName
            {
                get { return _InServer; }
                set { _InServer = value; }
            }

            /// <summary>
            /// Outgoing Server name
            /// </summary>
            public string OutgoingServerName
            {
                get { return _OutServer; }
                set { _OutServer = value; }
            }

            /// <summary>
            /// Account Identifier
            /// </summary>
            public Guid ID
            {
                get { return _AccountID; }
                set { _AccountID = value; }
            }

            /// <summary>
            /// Override the object.Equals method.
            /// </summary>
            /// <returns>The string result.</returns>
            public override string ToString()
            {
                return this.EmailAddress;
            }

        }

        private List<AccountInfo> m_aAccInfoList;

        /// <summary>
        /// Default constructor
        /// </summary>
        private AccountSettings()
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
        public static void Save(string file,AccountSettings c)
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
}
