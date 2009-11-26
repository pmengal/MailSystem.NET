using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.SMTP
{
    public partial class SendingAsynchronously : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public SendingAsynchronously(SamplesConfiguration config)
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

            // We send the email using the specified SMTP server
            this.AddLogEntry("Sending message.");

            try
            {
                //Send the message specifying the address of the smtp server, asynchronously
                //SendingDone method gets the notification once the message sending is done.
                message.BeginSend(this._tbSmtpServer.Text, new AsyncCallback(this.SendingDone));
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

        /// <summary>
        /// Call og BeginSend
        /// </summary>
        /// <param name="ar"></param>
        private void SendingDone(IAsyncResult ar)
        {
            this.AddLogEntry("Message sent successfully.");
        }
    }
}

