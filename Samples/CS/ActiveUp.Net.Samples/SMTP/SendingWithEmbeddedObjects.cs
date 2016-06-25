using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.SMTP
{
    public partial class SendingWithEmbeddedObjects : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public SendingWithEmbeddedObjects(SamplesConfiguration config)
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

            // We add the embedded objets.
            string bodyHtml = string.Empty;    
            for (int i = 0; i < _lvEmbeddedObject.Items.Count; i++)
            {
                message.EmbeddedObjects.Add((string)((Utils.ItemTag)_lvEmbeddedObject.Items[i]).Tag, true);
                bodyHtml += "<img src = \"cid:" + message.EmbeddedObjects[i].ContentId + "\" />";
            }
            SmtpClient.Send(message, "mail.example.com", 25, "user1@example.com", "userpassword", SaslMechanism.CramMd5);

            message.BodyHtml.Format = BodyFormat.Html;
            if (bodyHtml.Length > 0)
            {
                message.BodyHtml.Text = bodyHtml;
            }
            else
            {
                message.BodyHtml.Text = "The message doens't contain embedded objects.";
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

        private void _bAddEmbeddedObject_Click(object sender, EventArgs e)
        {
            if (_ofdEmbeddedObject.ShowDialog() == DialogResult.OK)
            {
                Utils.ItemTag it = new Utils.ItemTag();
                it.Text = new FileInfo(_ofdEmbeddedObject.FileName).Name;
                it.Tag = _ofdEmbeddedObject.FileName;
                _lvEmbeddedObject.Items.Add(it);
            }
        }

   
    }
}

