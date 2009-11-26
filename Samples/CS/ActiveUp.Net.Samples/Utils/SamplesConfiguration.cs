using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Utils
{
    [System.Xml.Serialization.XmlRootAttribute("config", IsNullable = false)]
    public class SamplesConfiguration
    {
        #region Fields

        private string _fromName, _fromEmail, _defaultSubject, _defaultBodyHtml, _defaultBodyText,
            _toEmail, _toName, _toEmail2, _toName2, _pop3UserName, _pop3Password, _pop3Server, _nntpServer,
            _newsgroup, _nntpSubject, _nntpBody, _confirmReadEmail, _replyToEmail, _returnReceipt,
            _imap4Server, _imap4UserName, _imap4Password, _imap4SearchPattern, _replyTo, _comments,
            _mainSmtpServer, _backupSmtpServer, _dnsServer;

        private const string _FILE_NAME_ = "Config.xml";

        #endregion

        #region Constructors

        public SamplesConfiguration()
        {
        }

        #endregion

        #region Properties

        public string FileName
        {
            get
            {
                return _FILE_NAME_;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("fromname", DataType = "string")]
        public string FromName
        {
            get
            {
                return _fromName;
            }
            set
            {
                _fromName = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("fromemail", DataType = "string")]
        public string FromEmail
        {
            get
            {
                return _fromEmail;
            }
            set
            {
                _fromEmail = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("defaultsubject", DataType = "string")]
        public string DefaultSubject
        {
            get
            {
                return _defaultSubject;
            }
            set
            {
                _defaultSubject = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("defaultbodyhtml", DataType = "string")]
        public string DefaultBodyHtml
        {
            get
            {
                return _defaultBodyHtml;
            }
            set
            {
                _defaultBodyHtml = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("defaultbodytext", DataType = "string")]
        public string DefaultBodyText
        {
            get
            {
                return _defaultBodyText;
            }
            set
            {
                _defaultBodyText = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("toemail", DataType = "string")]
        public string ToEmail
        {
            get
            {
                return _toEmail;
            }
            set
            {
                _toEmail = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("toname", DataType = "string")]
        public string ToName
        {
            get
            {
                return _toName;
            }
            set
            {
                _toName = value;
            }
        }

        /*[System.Xml.Serialization.XmlElementAttribute("toemail2", DataType = "string")]
        public string ToEmail2
        {
            get
            {
                return _toEmail2;
            }
            set
            {
                _toEmail2 = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("toname2", DataType = "string")]
        public string ToName2
        {
            get
            {
                return _toName2;
            }
            set
            {
                _toName2 = value;
            }
        }*/

        [System.Xml.Serialization.XmlElementAttribute("mainsmtpserver", DataType = "string")]
        public string MainSmtpServer
        {
            get
            {
                return _mainSmtpServer;
            }
            set
            {
                _mainSmtpServer = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("backupsmtpserver", DataType = "string")]
        public string BackupSmtpServer
        {
            get
            {
                return _backupSmtpServer;
            }
            set
            {
                _backupSmtpServer = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("pop3username", DataType = "string")]
        public string Pop3UserName
        {
            get
            {
                return _pop3UserName;
            }

            set
            {
                _pop3UserName = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("pop3password", DataType = "string")]
        public string Pop3Password
        {
            get
            {
                return _pop3Password;
            }

            set
            {
                _pop3Password = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("pop3server", DataType = "string")]
        public string Pop3Server
        {
            get
            {
                return _pop3Server;
            }

            set
            {
                _pop3Server = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("nntpserver", DataType = "string")]
        public string NntpServer
        {
            get
            {
                return _nntpServer;
            }

            set
            {
                _nntpServer = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("newsgroup", DataType = "string")]
        public string Newsgroup
        {
            get
            {
                return _newsgroup;
            }

            set
            {
                _newsgroup = value;
            }
        }

        /*[System.Xml.Serialization.XmlElementAttribute("nntpsubject", DataType = "string")]
        public string NntpSubject
        {
            get
            {
                return _nntpSubject;
            }

            set
            {
                _nntpSubject = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("nntpbody", DataType = "string")]
        public string NntpBody
        {
            get
            {
                return _nntpBody;
            }

            set
            {
                _nntpBody = value;
            }
        }*/

        [System.Xml.Serialization.XmlElementAttribute("confirmreademail", DataType = "string")]
        public string ConfirmReadEmail
        {
            get
            {
                return _confirmReadEmail;
            }

            set
            {
                _confirmReadEmail = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("replytoemail", DataType = "string")]
        public string ReplyToEmail
        {
            get
            {
                return _replyToEmail;
            }

            set
            {
                _replyToEmail = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("returnreceipt", DataType = "string")]
        public string ReturnReceipt
        {
            get
            {
                return _returnReceipt;
            }

            set
            {
                _returnReceipt = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("imap4server", DataType = "string")]
        public string Imap4Server
        {
            get
            {
                return _imap4Server;
            }

            set
            {
                _imap4Server = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("imap4username", DataType = "string")]
        public string Imap4UserName
        {
            get
            {
                return _imap4UserName;
            }

            set
            {
                _imap4UserName = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("imap4password", DataType = "string")]
        public string Imap4Password
        {
            get
            {
                return _imap4Password;
            }

            set
            {
                _imap4Password = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("imap4searchpattern", DataType = "string")]
        public string Imap4SearchPattern
        {
            get
            {
                return _imap4SearchPattern;
            }

            set
            {
                _imap4SearchPattern = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("comments", DataType = "string")]
        public string Comments
        {
            get
            {
                return _comments;
            }

            set
            {
                _comments = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("dnsserver", DataType = "string")]
        public string DnsServer
        {
            get
            {
                return _dnsServer;
            }

            set
            {
                _dnsServer = value;
            }
        }


        #endregion

        #region Methods

        public void SetDefaultValue()
        {
            _fromName = "From Name";
            _fromEmail = "user1@example.com.com";
            _defaultSubject = "This is a test message";
            _defaultBodyHtml = "<p>This is a test <b>message</b>.</p>";
            _defaultBodyText = "This is a test message.";
            _mainSmtpServer = "mail.example.com";
            _backupSmtpServer = "backup.mx.example.com";
            _toName = "User 2";
            _toEmail = "user2@example.com";
            _toName2 = "User3";
            _toEmail2 = "user3@example.com";
            _pop3UserName = "user1@example.com";
            _pop3Password = "yourpassword";
            _pop3Server = "mail.example.com";
            _nntpServer = "news.example.com";
            _newsgroup = "alt.example";
            _nntpSubject = "This is a test message";
            _nntpBody = "This is a test message.";
            _confirmReadEmail = "user1@example.com";
            _replyToEmail = "user1@example.com";
            _returnReceipt = "user1@example.com";
            _imap4Server = "mail.example.com";
            _imap4UserName = "user1@example.com";
            _imap4Password = "yourpassword";
            _imap4SearchPattern = "";
            _dnsServer = "127.0.0.1";
        }

        public void Save()
        {
            Save(false);
        }

        public void Save(bool initValue)
        {
            if (initValue)
            {
                SetDefaultValue();
            }

            string configFullPath = Common.GetImagePath(Assembly.GetExecutingAssembly().Location) + @"\" + _FILE_NAME_;
            XmlSerializer serialize = new XmlSerializer(typeof(SamplesConfiguration));
            TextWriter writer = new StreamWriter(_FILE_NAME_);
            serialize.Serialize(writer, (SamplesConfiguration)this);
            writer.Close();
        }

        public bool CheckExistingConfig()
        {
            string configFullPath = Common.GetImagePath(Assembly.GetExecutingAssembly().Location) + @"\" + _FILE_NAME_;
            return File.Exists(configFullPath);
        }

        #endregion
    }
}
