using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.Net.Samples.Utils
{
    public partial class ConfigurationForm : Form
    {
        private SamplesConfiguration _config = null;

        public ConfigurationForm(SamplesConfiguration config)
        {
            _config = config;

            InitializeComponent();

            InitConfiguration();
        }

        public void InitConfiguration()
        {
            if (_config != null)
            {
                // Message
                _tbFromName.Text = _config.FromName;
                _tbFromEmail.Text = _config.FromEmail;
                _tbToName.Text = _config.ToName;
                _tbToEmail.Text = _config.ToEmail;
                /*_tbToName2.Text = _config.ToName2;
                _tbToEmail2.Text = _config.ToEmail2;*/
                _tbConfirmRead.Text = _config.ConfirmReadEmail;
                _tbReplyTo.Text = _config.ReplyToEmail;
                _tbReturnReceipt.Text = _config.ReturnReceipt;
                _tbSubject.Text = _config.DefaultSubject;
                _tbBodyHtml.Text = _config.DefaultBodyHtml;
                _tbBodyText.Text = _config.DefaultBodyText;

                // Pop3
                _tbPop3Username.Text = _config.Pop3UserName;
                _tbPop3Password.Text = _config.Pop3Password;
                _tbPop3Server.Text = _config.Pop3Server;

                // Imap4
                _tbImap4Username.Text = _config.Imap4UserName;
                _tbImap4Password.Text = _config.Imap4Password;
                _tbImap4Server.Text = _config.Imap4Server;
                _tbImap4SearchPattern.Text = _config.Imap4SearchPattern;

                // Smtp
                _tbSmtpServer.Text = _config.MainSmtpServer;
                _tbSmtpBackupServer.Text = _config.BackupSmtpServer;

                // Nntp
                _tbNewsgroup.Text = _config.Newsgroup;
                _tbNntpServer.Text = _config.NntpServer;
                
                // Validation
                _tbDnsServer.Text = _config.DnsServer;
            }
        }

        private void _bSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Message
                _config.FromName = _tbFromName.Text;
                _config.FromEmail = _tbFromEmail.Text;
                _config.ToName = _tbToName.Text;
                _config.ToEmail = _tbToEmail.Text;
                //_config.ToName2 = _tbToName2.Text;
                //_config.ToEmail2 = _tbToEmail2.Text;
                _config.ConfirmReadEmail = _tbConfirmRead.Text;
                _config.ReplyToEmail = _tbReplyTo.Text;
                _config.ReturnReceipt = _tbReturnReceipt.Text;
                _config.DefaultSubject = _tbSubject.Text;
                _config.DefaultBodyHtml = _tbBodyHtml.Text;
                _config.DefaultBodyText = _tbBodyText.Text;

                // Pop3
                _config.Pop3UserName = _tbPop3Username.Text;
                _config.Pop3Password = _tbPop3Password.Text;
                _config.Pop3Server = _tbPop3Server.Text;

                // Imap4
                _config.Imap4UserName = _tbImap4Username.Text;
                _config.Imap4Password = _tbImap4Password.Text;
                _config.Imap4Server = _tbImap4Server.Text;
                _config.Imap4SearchPattern = _tbImap4SearchPattern.Text;

                // Smtp
                _config.MainSmtpServer = _tbSmtpServer.Text;
                _config.BackupSmtpServer = _tbSmtpBackupServer.Text;

                // Nntp
                _config.Newsgroup = _tbNewsgroup.Text;
                _config.NntpServer = _tbNntpServer.Text;

                // Validation
                _config.DnsServer = _tbDnsServer.Text;

                _config.Save();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                this.Close();
            }
        }

        private void _bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}