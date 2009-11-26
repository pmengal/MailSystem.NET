using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml.Serialization;
using System.Reflection;
using ActiveUp.Net.Mail;
   
using ActiveUp.Net.Samples.Utils;
using System.Windows.Forms;

namespace ActiveUp.Net.Samples
{
    public partial class MainMenu : System.Windows.Forms.Form
    {
        private SamplesConfiguration _configuration;

        public MainMenu()
        {
            InitializeComponent();

            SamplesConfiguration config = this.Configuration;
        }

        public SamplesConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new SamplesConfiguration();

                    string configFullPath = Utils.Common.GetImagePath(Assembly.GetExecutingAssembly().Location) + @"\" + _configuration.FileName;

                    if (File.Exists(configFullPath))
                    {
                        TextReader reader = new StreamReader(configFullPath);
                        XmlSerializer serialize = new XmlSerializer(typeof(SamplesConfiguration));
                        _configuration = (SamplesConfiguration)serialize.Deserialize(reader);
                        reader.Close();
                    }
                    else
                    {
                        _configuration.SetDefaultValue();
                        
                        ConfigurationForm configForm = new ConfigurationForm(this.Configuration);
                        configForm.ShowDialog();
                    }
                }
                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }

        #region SMTP

        private void _bSmtpSendingUsingSmtp_Click(object sender, EventArgs e)
        {
            SMTP.SendingUsingAnSmtpServer form = new SMTP.SendingUsingAnSmtpServer(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingWithoutSmtpServer_Click(object sender, EventArgs e)
        {
            SMTP.SendingWithoutASmtpServer form = new SMTP.SendingWithoutASmtpServer(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingWithMultipleFailOverSmtp_Click(object sender, EventArgs e)
        {
            SMTP.SendingWithMultipleFailoverSmtp form = new SMTP.SendingWithMultipleFailoverSmtp(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingWithFileAttachments_Click(object sender, EventArgs e)
        {
            SMTP.SendingWithFileAttachments form = new SMTP.SendingWithFileAttachments(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingWithSmtpAuthentication_Click(object sender, EventArgs e)
        {
            SMTP.SendingWithSmtpAuthentification form = new SMTP.SendingWithSmtpAuthentification(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingWithSecureConnection_Click(object sender, EventArgs e)
        {
            SMTP.SendingWithSecureConnection form = new SMTP.SendingWithSecureConnection(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingToMultipleRecipients_Click(object sender, EventArgs e)
        {
            SMTP.SendingToMultipleRecipients form = new SMTP.SendingToMultipleRecipients(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingWithEmbeddedObjects_Click(object sender, EventArgs e)
        {
            SMTP.SendingWithEmbeddedObjects form = new SMTP.SendingWithEmbeddedObjects(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingWithMultipleBody_Click(object sender, EventArgs e)
        {
            SMTP.SendingWithMultipleBody form = new SMTP.SendingWithMultipleBody(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingAsynchronously_Click(object sender, EventArgs e)
        {
            SMTP.SendingAsynchronously form = new SMTP.SendingAsynchronously(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmtpSendingSignedEmail_Click(object sender, EventArgs e)
        {
            SMTP.SendingSignedEmails form = new SMTP.SendingSignedEmails(this.Configuration);
            form.ShowDialog();
        }

        private void _bSmptSendingCryptedEmail_Click(object sender, EventArgs e)
        {
            SMTP.SendingCryptedEmail form = new SMTP.SendingCryptedEmail(this.Configuration);
            form.ShowDialog();
        }

        #endregion

        #region Template

        private void _bSendingFromAnXMLTemplate_Click(object sender, EventArgs e)
        {
            Template.SendingFromAnXmlTemplate form = new Template.SendingFromAnXmlTemplate(this.Configuration);
            form.ShowDialog();
        }

        private void _bWorkingWithLists_Click(object sender, EventArgs e)
        {
            Template.WorkingWithList form = new Template.WorkingWithList(this.Configuration);
            form.ShowDialog();
        }

        private void _bWorkingWithFieldFormat_Click(object sender, EventArgs e)
        {
            Template.WorkingWithFieldFormat form = new Template.WorkingWithFieldFormat(this.Configuration);
            form.ShowDialog();
        }

        private void _bMailMerginFromDataSources_Click(object sender, EventArgs e)
        {
            Template.MailMergingFromDataSources form = new Template.MailMergingFromDataSources(this.Configuration);
            form.ShowDialog();
        }

        private void _bCombiningMergingAndTemplating_Click(object sender, EventArgs e)
        {
            Template.CombiningMergingAndTemplating form = new Template.CombiningMergingAndTemplating(this.Configuration);
            form.ShowDialog();
        }

        #endregion

        #region Pop3

        private void _bRetrieveMessageCount_Click(object sender, EventArgs e)
        {
            POP3.RetrieveMessageCount form = new POP3.RetrieveMessageCount(this.Configuration);
            form.ShowDialog();
        }

        private void _bRetrieveMessageList_Click(object sender, EventArgs e)
        {
            POP3.RetrieveMessageList form = new POP3.RetrieveMessageList(this.Configuration);
            form.ShowDialog();
        }

        private void _bRetrieveMessageHeaderOnly_Click(object sender, EventArgs e)
        {
            POP3.RetrieveMessageHeaderOnly form = new POP3.RetrieveMessageHeaderOnly(this.Configuration);
            form.ShowDialog();
        }

        private void _bRetrieveSpecificMessage_Click(object sender, EventArgs e)
        {
            POP3.RetrieveSpecificMessage form = new POP3.RetrieveSpecificMessage(this.Configuration);
            form.ShowDialog();
        }

        private void _bRetrieveFromSecureConnection_Click(object sender, EventArgs e)
        {
            POP3.RetrieveFromSecureConnection form = new POP3.RetrieveFromSecureConnection(this.Configuration);
            form.ShowDialog();
        }

        private void _bRetrieveAsynch_Click(object sender, EventArgs e)
        {
            POP3.RetrieveAsynchronously form = new POP3.RetrieveAsynchronously(this.Configuration);
            form.ShowDialog();
        }

        private void _bRetriveToFile_Click(object sender, EventArgs e)
        {
            POP3.RetrieveToFile form = new POP3.RetrieveToFile(this.Configuration);
            form.ShowDialog();
        }

        private void _bSecureConnectionUsingAPOP_Click(object sender, EventArgs e)
        {
            POP3.SecureConnectionUsingApop form = new POP3.SecureConnectionUsingApop(this.Configuration);
            form.ShowDialog();
        }

        private void _bCRAMAuthentication_Click(object sender, EventArgs e)
        {
            POP3.CRAMAuthentication form = new POP3.CRAMAuthentication(this.Configuration);
            form.ShowDialog();
        }
        
        #endregion

        #region Imap4

        private void _bImapRetrieveAllMailboxes_Click(object sender, EventArgs e)
        {
            IMAP4.RetrieveAllMailboxes form = new IMAP4.RetrieveAllMailboxes(this.Configuration);
            form.ShowDialog();
        }

        private void _bImapRetrieveMessageList_Click(object sender, EventArgs e)
        {
            IMAP4.RetrieveMessageList form = new IMAP4.RetrieveMessageList(this.Configuration);
            form.ShowDialog();
        }

        private void _bImapFlagMessage_Click(object sender, EventArgs e)
        {
            IMAP4.FlagMessage form = new IMAP4.FlagMessage(this.Configuration);
            form.ShowDialog();
        }

        private void _bImapRetrieveSpecificMessage_Click(object sender, EventArgs e)
        {
            IMAP4.RetrieveSpecificMessage form = new IMAP4.RetrieveSpecificMessage(this.Configuration);
            form.ShowDialog();
        }

        private void _bImapFlagMessage_Click_1(object sender, EventArgs e)
        {
            IMAP4.FlagMessage form = new IMAP4.FlagMessage(this.Configuration);
            form.ShowDialog();
        }

        private void _bImapRetrieveMessageHeader_Click(object sender, EventArgs e)
        {
            IMAP4.RetrieveMessageHeader form = new IMAP4.RetrieveMessageHeader(this.Configuration);
            form.ShowDialog();
        }

        private void _bRetrieveAsynchronously_Click(object sender, EventArgs e)
        {
            IMAP4.RetrieveAsynchronously form = new IMAP4.RetrieveAsynchronously(this.Configuration);
            form.ShowDialog();
        }

        private void _bImapRetrieveSecureConnection_Click(object sender, EventArgs e)
        {
            IMAP4.RetrieveFromSecureConnection form = new IMAP4.RetrieveFromSecureConnection(this.Configuration);
            form.ShowDialog();
        }

        private void _bSearchMailbox_Click(object sender, EventArgs e)
        {
            IMAP4.SearchMailbox form = new IMAP4.SearchMailbox(this.Configuration);
            form.ShowDialog();
        }

        private void _bCreateMailbox_Click(object sender, EventArgs e)
        {
            IMAP4.CreateMailbox form = new IMAP4.CreateMailbox(this.Configuration);
            form.ShowDialog();
        }

        private void _bDeleteMailbox_Click(object sender, EventArgs e)
        {
            IMAP4.DeleteMailbox form = new IMAP4.DeleteMailbox(this.Configuration);
            form.ShowDialog();
        }

        private void _bEmptyMailbox_Click(object sender, EventArgs e)
        {
            IMAP4.EmptyMailbox form = new IMAP4.EmptyMailbox(this.Configuration);
            form.ShowDialog();
        }

        private void _bImapRenameMailbox_Click(object sender, EventArgs e)
        {
            IMAP4.RenameMailbox form = new IMAP4.RenameMailbox(this.Configuration);
            form.ShowDialog();
        }

        #endregion

        #region NNTP

        private void _bDisplayAllNewsgroup_Click(object sender, EventArgs e)
        {
            NNTP.DisplayAllNewsGroup form = new NNTP.DisplayAllNewsGroup(this.Configuration);
            form.ShowDialog();
        }

        private void _bNntpMessageCount_Click(object sender, EventArgs e)
        {
            NNTP.DisplayMessageCount form = new NNTP.DisplayMessageCount(this.Configuration);
            form.ShowDialog();
        }

        private void _bNntpRetrieveMessageList_Click(object sender, EventArgs e)
        {
            NNTP.RetrieveMessageList form = new NNTP.RetrieveMessageList(this.Configuration);
            form.ShowDialog();
        }

        private void _bNntpRetrieveNewMessages_Click(object sender, EventArgs e)
        {
            NNTP.RetrieveNewMessages form = new NNTP.RetrieveNewMessages(this.Configuration);
            form.ShowDialog();
        }

        private void _bNntpRetrieveSpecificMessage_Click(object sender, EventArgs e)
        {
            NNTP.RetrieveSpecificMessage form = new NNTP.RetrieveSpecificMessage(this.Configuration);
            form.ShowDialog();
        }

        private void _bNntpPostMessage_Click(object sender, EventArgs e)
        {
            NNTP.PostMessage form = new NNTP.PostMessage(this.Configuration);
            form.ShowDialog();
        }

        #endregion

        #region Validation

        private void _bWorkingWithDomainKeys_Click(object sender, EventArgs e)
        {
            Validation.WorkingWithDomainKeys form = new Validation.WorkingWithDomainKeys(this.Configuration);
            form.ShowDialog();
        }

        private void _bWorkingWithBlackList_Click(object sender, EventArgs e)
        {
            Validation.WorkingWithBlackListServers form = new Validation.WorkingWithBlackListServers(this.Configuration);
            form.ShowDialog();
        }

        private void _bValidateEmail_Click(object sender, EventArgs e)
        {
            Validation.ValidateEmail form = new Validation.ValidateEmail(this.Configuration);
            form.ShowDialog();
        }

        private void _bFilterEmails_Click(object sender, EventArgs e)
        {
            Validation.FilterEmails form = new Validation.FilterEmails(this.Configuration);
            form.ShowDialog();
        }

        #endregion

        #region Common

        private void _bSaveAttchment_Click(object sender, EventArgs e)
        {
            Common.SaveAttachmentToDisk form = new Common.SaveAttachmentToDisk(this.Configuration);
            form.ShowDialog();
        }

        private void _bWorkingWithEncoding_Click(object sender, EventArgs e)
        {
            Common.WorkingWithEncodingAndCharset form = new Common.WorkingWithEncodingAndCharset(this.Configuration);
            form.ShowDialog();
        }

        private void _bWorkingWithMessageSettings_Click(object sender, EventArgs e)
        {
            Common.WorkingWithMessageSettings form = new Common.WorkingWithMessageSettings(this.Configuration);
            form.ShowDialog();
        }

        private void _bDetermineBounce_Click(object sender, EventArgs e)
        {
            Common.DetermineIfBounceEmail form = new Common.DetermineIfBounceEmail(this.Configuration);
            form.ShowDialog();
        }

        #endregion

        private void _tsbConfig_Click(object sender, EventArgs e)
        {
            ConfigurationForm configForm = new ConfigurationForm(this.Configuration);
            configForm.ShowDialog();
        }

        private void blockListServersQueryButton_Click(object sender, EventArgs e)
        {
            Validation.BlockListServerQuery form = new Validation.BlockListServerQuery(this.Configuration);
            form.ShowDialog();
        }

        private void readWritevCardButton_Click(object sender, EventArgs e)
        {
            PDI.ReadWritevCard form = new PDI.ReadWritevCard(this.Configuration);
            form.ShowDialog();
        }

        private void readWritevCalendar_Click(object sender, EventArgs e)
        {
            PDI.ReadWritevCalendar form = new PDI.ReadWritevCalendar(this.Configuration);
            form.ShowDialog();
        }

        private void commtouchTechnology_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Commtouch® Technology sample is available as a separated project.");
        }

        private void newMessageNotificationButton_Click(object sender, EventArgs e)
        {
            IMAP4.NewMessageNotification form = new IMAP4.NewMessageNotification(this.Configuration);
            form.ShowDialog();
        }

     

     
    }
}





