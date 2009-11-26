using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Validation
{
    public partial class WorkingWithBlackListServers : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public WorkingWithBlackListServers(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();

        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            _tbUserName.Text = Config.Pop3UserName;
            _tbPassword.Text = Config.Pop3Password;
            _tbPop3Server.Text = Config.Pop3Server;
        }

        private void _bAddBlackListServer_Click(object sender, EventArgs e)
        {
            _lvBlackListServers.Items.Add(_tbBlackListServer.Text);
            _tbBlackListServer.Text = string.Empty;
        }

        private void _bRemoveBlackListServer_Click(object sender, EventArgs e)
        {
            if (_lvBlackListServers.SelectedItems.Count > 0)
            {
                _lvBlackListServers.SelectedItems[0].Remove();
            }
        }

        private void _tbBlackListServer_TextChanged(object sender, EventArgs e)
        {
            if (_tbBlackListServer.Text.Length > 0)
            {
                _bAddBlackListServer.Enabled = true;
            }

            else
            {
                _bAddBlackListServer.Enabled = false;
            }
        }

        private void _bCheckMessage_Click(object sender, EventArgs e)
        {
            // We instantiate the pop3 client
            Pop3Client pop = new Pop3Client();

            try
            {
                this.AddLogEntry(string.Format("Connection to the pop 3 server : {0}", _tbPop3Server.Text));

                // We connect to the pop3 client
                pop.Connect(_tbPop3Server.Text, _tbUserName.Text, _tbPassword.Text);

                if (pop.MessageCount > 0)
                {
                    // We retrive a message at a particulat index
                    ActiveUp.Net.Mail.Message message = pop.RetrieveMessageObject(1);

                    bool blackList = false;
                    // We verify if the message is from black listed server
                    foreach (System.Windows.Forms.ListViewItem lvi in _lvDefinedBlackList.Items)
                    {
                        if (message.From.Email.EndsWith(lvi.Text))
                        {
                            this.AddLogEntry(string.Format("Messages from {0} are not allowed", lvi.Text));
                            blackList = true;
                        }
                        else
                        {
                            this.AddLogEntry(string.Format("Messages from {0} are allowed", lvi.Text));
                        }

                        if (blackList)
                            break;
                    }

                    if (!blackList)
                    {
                        foreach (string blackServer in _lvBlackListServers.Items)
                        {
                            if (message.From.Email.EndsWith(blackServer))
                            {
                                this.AddLogEntry(string.Format("Messages from {0} are not allowed", blackServer));
                                blackList = true;
                            }
                            else
                            {
                                this.AddLogEntry(string.Format("Messages from {0} are allowed", blackServer));
                            }

                            if (blackList)
                                break;
                        }
                    }

                    
                }

                else
                {
                    this.AddLogEntry("There is no messages in the pop3 account.");
                }
            }

            catch (Pop3Exception pexp)
            {
                this.AddLogEntry(string.Format("Pop3 Error: {0}", pexp.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }

            finally
            {
                if (pop.IsConnected)
                {
                    pop.Disconnect();
                }
            }
        }

        private void _lvBlackListServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lvBlackListServers.SelectedIndices.Count > 0)
            {
                _bRemoveBlackListServer.Enabled = true;
            }

            else
            {
                _bRemoveBlackListServer.Enabled = false;
            }
        }
    }
}

