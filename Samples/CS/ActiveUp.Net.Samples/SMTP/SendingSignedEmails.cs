using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates; 
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.SMTP
{
    public partial class SendingSignedEmails : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public SendingSignedEmails(SamplesConfiguration config)
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

        private void _bSendMessage_Click(object sender, EventArgs e)
        {
            this.AddLogEntry("Creating message.");

            // We create the message object
            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();

            // We assign the sender email
            message.From.Email = this._tbFromEmail.Text;

            // We assign the recipient email
            message.To.Add(this._tbToEmail.Text);

            // We assign the subject
            message.Subject = this._tbSubject.Text;

            // We assign the body text
            message.BodyText.Text = this._tbBodyText.Text;

            // It is required to build the mime part tree before signing
            message.BuildMimePartTree();

            if (_tbCertificate.Text != string.Empty)
            {
                CmsSigner signer = new CmsSigner(new X509Certificate2(_tbCertificate.Text));

                // Here we only want the signer's certificate to be sent along. Not the whole chain.
                signer.IncludeOption = X509IncludeOption.EndCertOnly;

                message.SmimeAttachSignatureBy(signer);
            }

            // We send the email using the specified SMTP server
            this.AddLogEntry("Sending message.");

            try
            {
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

        private void _tbSignature_TextChanged(object sender, EventArgs e)
        {
            if (_tbCertificate.Text == string.Empty)
            {
                _bRemoveSignature.Enabled = false;
            }

            else
            {
                _bRemoveSignature.Enabled = true;
            }
        }

        private void _bAddSignature_Click(object sender, EventArgs e)
        {
            if (_ofdSignature.ShowDialog() == DialogResult.OK)
            {
                _tbCertificate.Text = _ofdSignature.FileName;
            }
        }

        private void _bRemoveSignature_Click(object sender, EventArgs e)
        {
            _tbCertificate.Text = string.Empty;
        }


    }
}

