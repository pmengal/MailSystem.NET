using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.SMTP
{
    public partial class SendingCryptedEmail : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public SendingCryptedEmail(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this._tbToEmail.Text = this.Config.ToEmail;
            this._tbFromEmail.Text = this.Config.FromEmail;
            this._tbSubject.Text = this.Config.DefaultSubject;
            this._tbBodyText.Text = this.Config.DefaultBodyText;
            this._tbSmtpServer.Text = this.Config.MainSmtpServer;
        }

        private void _tbRecipientCertificate_TextChanged(object sender, EventArgs e)
        {
            if (_tbRecipientCertificate.Text == string.Empty)
            {
                _bRemoveRecipientCertificate.Enabled = false;
            }

            else
            {
                _bRemoveRecipientCertificate.Enabled = true;
            }
        }

        private void _bAddRecipientCertificate_Click(object sender, EventArgs e)
        {
            if (_ofdRecipientCertificate.ShowDialog() == DialogResult.OK)
            {
                _tbRecipientCertificate.Text = _ofdRecipientCertificate.FileName;
            }
        }

        private void _bRemoveRecipientCertificate_Click(object sender, EventArgs e)
        {
            _tbRecipientCertificate.Text = string.Empty;
        }

        private void _tbSignerCertificate_TextChanged(object sender, EventArgs e)
        {
            if (_tbSignerCertificate.Text == string.Empty)
            {
                _bRemoveSignerCertificate.Enabled = false;
            }

            else
            {
                _bRemoveSignerCertificate.Enabled = true;
            }
        }

        private void _bAddSignerCertificate_Click(object sender, EventArgs e)
        {
            if (_ofdSignerCertificate.ShowDialog() == DialogResult.OK)
            {
                _tbSignerCertificate.Text = _ofdSignerCertificate.FileName;
            }
        }

        private void _bRemoveSignerCertificate_Click(object sender, EventArgs e)
        {
            _tbSignerCertificate.Text = string.Empty;
        } 

        private void _bSendMessage_Click(object sender, EventArgs e)
        {
            this.AddLogEntry("Creating message.");

            // We create the message object
            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();

            try
            {
                // We assign the sender email
                message.From.Email = this._tbFromEmail.Text;

                // We assign the recipient email
                message.To.Add(this._tbToEmail.Text);

                // We assign the subject
                message.Subject = this._tbSubject.Text;

                // We assign the body text
                message.BodyText.Text = this._tbBodyText.Text;

                // It is required to build the mime part tree before encrypting
                message.BuildMimePartTree();

                // Encrypt the message. You need the recipient(s) certificate(s) (with public key only).
                X509Certificate2 recipientCertificate = new X509Certificate2(_tbRecipientCertificate.Text);

                CmsRecipient recipient = new CmsRecipient(recipientCertificate);

                message.SmimeEnvelopeAndEncryptFor(recipient);

                // We send the email using the specified SMTP server
                this.AddLogEntry("Sending message.");
                           
                SmtpClient.Send(message, this._tbSmtpServer.Text);

                this.AddLogEntry("Message sent successfully.");
            }

            catch (SmtpException ex)
            {
                this.AddLogEntry(string.Format("Smtp Error: {0}", ex.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }
        }
    }
}