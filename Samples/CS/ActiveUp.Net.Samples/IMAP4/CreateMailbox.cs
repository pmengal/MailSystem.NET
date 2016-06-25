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
    public partial class CreateMailbox : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public CreateMailbox(SamplesConfiguration config)
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

        private void _bCreateMailbox_Click(object sender, EventArgs e)
        {
            // We create Imap client
            Imap4Client imap = new Imap4Client();

            try
            {
                // We connect to the imap4 server
                imap.Connect(_tbImap4Server.Text);

                this.AddLogEntry(string.Format("Connection to {0} successfully", _tbImap4Server.Text));

                // Login to mail box
                imap.Login(_tbUserName.Text, _tbPassword.Text);

                this.AddLogEntry(string.Format("Login to {0} successfully", _tbImap4Server.Text));

                if (_tbNewMailbox.Text.Length > 0)
                {
                    imap.CreateMailbox(_tbNewMailbox.Text);
                    this.AddLogEntry("Mailbox {0} successfully created");
                }

                else
                {
                    this.AddLogEntry("You have to set a new mailbox name");
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

