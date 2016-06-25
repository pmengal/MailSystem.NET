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
    public partial class RetrieveFromSecureConnection : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public RetrieveFromSecureConnection(SamplesConfiguration config)
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

        private void _bRetrieveMessage_Click(object sender, EventArgs e)
        {
            // We create Imap client
            Imap4Client imap = new Imap4Client();

            try
            {
                // We connect to the imap4 server
                imap.ConnectSsl(_tbImap4Server.Text);

                this.AddLogEntry(string.Format("Connection to {0} successfully", _tbImap4Server.Text));

                // Login to mail box
                imap.Login(_tbUserName.Text, _tbPassword.Text);

                this.AddLogEntry(string.Format("Login to {0} successfully", _tbImap4Server.Text));

                Mailbox inbox = imap.SelectMailbox("inbox");
                if (inbox.MessageCount > 0)
                {
                    Header header = inbox.Fetch.HeaderObject(1);

                    this.AddLogEntry(string.Format("Subject: {0} From :{1} ", header.Subject, header.From.Email));
                }

                else
                {
                    this.AddLogEntry("There is no message in the imap4 account");
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

            finally
            {
                if (imap.IsConnected)
                {
                    imap.Disconnect();
                }
            }
        }
    }
}

