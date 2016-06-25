using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Common
{
    public partial class SaveAttachmentToDisk : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public SaveAttachmentToDisk(SamplesConfiguration config)
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

        private void _bAddDirectory_Click(object sender, EventArgs e)
        {
            if (_fbdSaveDirectory.ShowDialog() == DialogResult.OK)
            {
                _tbAttachmentDirectory.Text = _fbdSaveDirectory.SelectedPath;
            }
        }

        private void _bRemoveDirectory_Click(object sender, EventArgs e)
        {
            _tbAttachmentDirectory.Text = string.Empty;
        }

        private void _tbAttachmentDirectory_TextChanged(object sender, EventArgs e)
        {
            if (_tbAttachmentDirectory.Text == string.Empty)
            {
                _bRemoveDirectory.Enabled = false;
            }

            else
            {
                _bRemoveDirectory.Enabled = true;
            }
        }

        private void _bSaveAttachment_Click(object sender, EventArgs e)
        {
            if (_tbAttachmentDirectory.Text == string.Empty)
            {
                this.AddLogEntry("You have to set a directory to store the attachments.");
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
                        ActiveUp.Net.Mail.Message message = pop.RetrieveMessageObject(1);

                        if (message.Attachments.Count > 0)
                        {
                            // Save the first attachment to the specified directory.
                            message.Attachments[0].StoreToFile(_fbdSaveDirectory.SelectedPath + message.Attachments[0].HeaderFields["FileName"]);
                            this.AddLogEntry(string.Format("Attachments '{0}' saved successfully.", message.Attachments[0].HeaderFields["FileName"]));
                        }

                        else
                        {
                            this.AddLogEntry("There is no attachments in the message.");
                        }
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

