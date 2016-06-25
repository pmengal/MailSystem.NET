using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.IMAP4
{
    public partial class FlagMessage : ActiveUp.Net.Samples.Utils.MasterForm
    {
        protected Mailbox inbox = null;
        protected Imap4Client imap = null;

        public FlagMessage(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            _tbUserName.Text = Config.Imap4UserName;
            _tbPassword.Text = Config.Imap4Password;
            _tbImap4Server.Text = Config.Imap4Server;
        }

        private void _bRetrieveMessages_Click(object sender, EventArgs e)
        {
            // We create Imap client
            imap = new Imap4Client();

            try
            {
                // We connect to the imap4 server
                imap.Connect(_tbImap4Server.Text);

                this.AddLogEntry(string.Format("Connection to {0} successfully", _tbImap4Server.Text));

                // Login to mail box
                imap.Login(_tbUserName.Text, _tbPassword.Text);

                this.AddLogEntry(string.Format("Login to {0} successfully", _tbImap4Server.Text));

                inbox = imap.SelectMailbox("inbox");
                if (inbox.MessageCount > 0)
                {
                    for (int i = 1; i < inbox.MessageCount + 1; i++)
                    {
                        ActiveUp.Net.Mail.Message message = inbox.Fetch.MessageObject(i);
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString("0000");
                        lvi.SubItems.AddRange(new string[] { message.Subject});
                        lvi.Tag = message;

                        _lvMessages.Items.Add(lvi);

                        this.AddLogEntry(string.Format("{3} Subject: {0} From :{1} Message Body {2}"
                                        , message.Subject, message.From.Email, message.BodyText, i.ToString("0000")));
                    }
                }

                else
                {
                    this.AddLogEntry("There is no unanswered messages in the imap4 account");
                }

            }

            catch (Imap4Exception iex)
            {
                this.AddLogEntry(string.Format("Imap4 Error: {0}", iex.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }
   
        }

        private void _bSetFlag_Click(object sender, EventArgs e)
        {
            try
            {
                int index = int.Parse(_lvMessages.SelectedItems[0].Text);
                FlagCollection flags = new FlagCollection();
                flags.Add("Answered");
                inbox.SetFlags(1, flags);

                this.AddLogEntry(string.Format("Flat answered setted to the message with index {0}",index.ToString()));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }
        }

        private void _lvMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lvMessages.SelectedIndices.Count > 0)
            {
                _bSetFlag.Enabled = true;
            }

            else
            {
                _bSetFlag.Enabled = false;
            }
        }

        private void _lvMessages_DoubleClick(object sender, EventArgs e)
        {
            if (_lvMessages.SelectedIndices.Count > 0)
            {
                Utils.MessageForm messageForm = new Utils.MessageForm((ActiveUp.Net.Mail.Message)_lvMessages.SelectedItems[0].Tag);
                messageForm.Show();
            }
        }

        private void _lvMessages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_lvMessages.SelectedIndices.Count > 0)
                {
                    Utils.MessageForm messageForm = new Utils.MessageForm((ActiveUp.Net.Mail.Message)_lvMessages.SelectedItems[0].Tag);
                    messageForm.Show();
                }
            }
        }

        private void FlagMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (imap.IsConnected)
                {
                    imap.Disconnect();
                }
            }
            catch { }
        }

    }
}

