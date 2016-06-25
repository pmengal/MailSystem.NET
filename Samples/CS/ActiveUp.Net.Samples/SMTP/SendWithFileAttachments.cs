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
    partial class SendWithFileAttachments : MasterForm
    {
        public SendWithFileAttachments(SamplesConfiguration config)
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
            this.smtpServerAddressTextbox.Text = this.Config.MainSmtpServer.Host;
        }

        private void addAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(fileDialog.FileName))
                attachmentsListbox.Items.Add(fileDialog.FileName);
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (this.attachmentsListbox.Items.Count > 0)
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

                // We now add each attachments
                foreach (string attachmentPath in this.attachmentsListbox.Items)
                {
                    message.Attachments.Add(attachmentPath, false);
                }

                message.BuildMimePartTree();

                // We send the email using the specified SMTP server
                this.AddLogEntry("Sending message.");

                try
                {
                    message.Send(this.smtpServerAddressTextbox.Text);

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
            else
                MessageBox.Show("Please add an attachment before sending this test message.");
        }
    }
}