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
    public partial class SendingWithoutASmtpServer : MasterForm
    {
        public SendingWithoutASmtpServer(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this.toEmailTextbox.Text = this.Config.ToEmail;
            this.fromEmailTextbox.Text = this.Config.FromEmail;
            this.subjectTextbox.Text = this.Config.DefaultSubject;
            this.bodyTextTextbox.Text = this.Config.DefaultBodyText;
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            this.AddLogEntry("Creating message.");

            // We create the message object
            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();

            // We assign the sender email
            message.From.Email = this.fromEmailTextbox.Text;

            // We assign the recipient email
            message.To.Add(this.toEmailTextbox.Text);

            // We assign the subject
            message.Subject = this.subjectTextbox.Text;

            // We assign the body text
            message.BodyText.Text = this.bodyTextTextbox.Text;
            
            // We send the email directly without an SMTP server
            this.AddLogEntry("Sending message.");

            try
            {
                SmtpClient.DirectSend(message);

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