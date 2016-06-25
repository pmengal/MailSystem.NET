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
    partial class SendingWithFileAttachments : MasterForm
    {
        public SendingWithFileAttachments(SamplesConfiguration config)
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

        private void addAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            if (!string.IsNullOrEmpty(fileDialog.FileName))
                _lvAttachments.Items.Add(fileDialog.FileName);
        }

        private void _bSendMessage_Click(object sender, EventArgs e)
        {
			if (this._lvAttachments.Items.Count > 0)
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

				// We now add each attachments
				foreach (string attachmentPath in this._lvAttachments.Items)
				{
					message.Attachments.Add(attachmentPath, false);
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

			else
			{
				MessageBox.Show("Please add an attachment before sending this test message.");
			}
        }

      
    }
}