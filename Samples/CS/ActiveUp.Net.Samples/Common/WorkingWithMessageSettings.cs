using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Common
{
    public partial class WorkingWithMessageSettings : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public WorkingWithMessageSettings(SamplesConfiguration config)
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
            this._tbConfirmReadEmail.Text = this.Config.ConfirmReadEmail;
            this._tbReplyTo.Text = this.Config.ReplyToEmail;
            this._tbReturnReceipt.Text = this.Config.ReturnReceipt;
            this._tbSubject.Text = this.Config.DefaultSubject;
            this._tbBodyText.Text = this.Config.DefaultBodyText;
            this._tbComments.Text = this.Config.Comments;
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

            // We assign the return recipient email
            message.ReturnReceipt.Email = this._tbReturnReceipt.Text;

            // We assign the confirmation read email
            message.ConfirmRead.Email = this._tbConfirmReadEmail.Text;

            // We assign the reply to email
            message.ReplyTo.Email = this._tbReplyTo.Text;

            // We assign the comments
            message.Comments = this._tbComments.Text;

            // We assign the mime type.
            message.ContentType.MimeType = "text/html";

            // We assign the subject
            message.Subject = this._tbSubject.Text;

            // We assign the body text
            message.BodyText.Text = this._tbBodyText.Text;

            // We send the email using the specified SMTP server
            this.AddLogEntry("Sending message.");

            try
            {
                message.Send(this._tbSmtpServer.Text);

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

