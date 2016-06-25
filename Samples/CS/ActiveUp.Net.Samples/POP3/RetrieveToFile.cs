using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.POP3
{
    public partial class RetrieveToFile : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public RetrieveToFile(SamplesConfiguration config)
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
            
        private void _tbMessageFilename_TextChanged(object sender, EventArgs e)
        {
            if (_tbMessageFilename.Text == string.Empty)
            {
                _bRemoveMessageFilename.Enabled = false;
            }

            else
            {
                _bRemoveMessageFilename.Enabled = true;
            }
        }

        private void _bAddMessageFilename_Click(object sender, EventArgs e)
        {
            if (_sfdMessageFilename.ShowDialog() == DialogResult.OK)
            {
                _tbMessageFilename.Text = _sfdMessageFilename.FileName;
            }
        }

        private void _bRemoveMessageFilename_Click(object sender, EventArgs e)
        {
            _tbMessageFilename.Text = string.Empty;
        }

        private void _bRetriveToFile_Click(object sender, EventArgs e)
        {
            if (_tbMessageFilename.Text == string.Empty)
            {
                this.AddLogEntry("You have to set a message filename.");
            }

            else
            {
                // We instantiate the pop3 client.
                Pop3Client pop = new Pop3Client();

                try
                {
                    this.AddLogEntry(string.Format("Connection to the pop 3 server : {0}", _tbPop3Server.Text));

                    // Connect to the pop3 client
                    pop.Connect(_tbPop3Server.Text, _tbUserName.Text, _tbPassword.Text);

                    if (pop.MessageCount > 0)
                    {
                        // Store the message in the specified filename.
                        pop.StoreMessage(1, false, _tbMessageFilename.Text);

                        this.AddLogEntry("Message saved successfully.");
                    }

                    else
                    {
                        this.AddLogEntry("There is no message in this pop3 account");
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
        }
    }
}

