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
    public partial class SendingToMultipleRecipients : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public SendingToMultipleRecipients(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this._lvToEmail.Text = this.Config.ToEmail;
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

            //Add the recipients e mail ids
            foreach (ListViewItem lvi in _lvToEmail.Items)
            {
                message.To.Add(lvi.Text);
            }

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

        private void _lvToEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lvToEmail.SelectedIndices.Count > 0)
            {
                _bRemove.Enabled = true;
            }

            else
            {
                _bRemove.Enabled = false;
            }
        }

        private void _tbToAddress_TextChanged(object sender, EventArgs e)
        {
            if (_tbToAddress.Text.Length > 0)
            {
                _bAdd.Enabled = true;
            }

            else
            {
                _bAdd.Enabled = false;
            }
        }

        private void _bAdd_Click(object sender, EventArgs e)
        {
            _lvToEmail.Items.Add(_tbToAddress.Text);
            _tbToAddress.Text = string.Empty;
        }

        private void _bRemove_Click(object sender, EventArgs e)
        {
            if (_lvToEmail.SelectedItems.Count > 0)
            {
                _lvToEmail.SelectedItems[0].Remove();
            }
        }

        private void _tbToAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _tbToAddress.Text != string.Empty)
            {
                _lvToEmail.Items.Add(_tbToAddress.Text);
                _tbToAddress.Text = string.Empty;
            }
        }
    }
}

