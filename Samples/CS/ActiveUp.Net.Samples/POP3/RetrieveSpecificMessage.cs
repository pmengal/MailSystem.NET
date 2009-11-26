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
    public partial class RetrieveSpecificMessage : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public RetrieveSpecificMessage(SamplesConfiguration config)
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

        private void _bRetrieveSpecificMessage_Click(object sender, EventArgs e)
        {
            // We instantiate the pop3 client.
            Pop3Client pop = new Pop3Client();

            try
            {
                _lvMessages.Items.Clear();

                this.AddLogEntry(string.Format("Connection to the pop 3 server : {0}", _tbPop3Server.Text));

                // Connect to the pop3 client
                pop.Connect(_tbPop3Server.Text, _tbUserName.Text, _tbPassword.Text);

                if (pop.MessageCount > 0)
                {
                    //Retrive a messaheader at a particulat index (index 1 in this sample)
                    for (int i = 1 ; i < pop.MessageCount + 1; i++)
                    {
                        ActiveUp.Net.Mail.Message message = pop.RetrieveMessageObject(i);

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString("0000");
                        lvi.SubItems.AddRange(new string[] { message.Subject });
                        lvi.Tag = message;

                        _lvMessages.Items.Add(lvi);

                        this.AddLogEntry(string.Format("{3} Subject: {0} From :{1} Message Body {2}"
                                        , message.Subject, message.From.Email, message.BodyText,i.ToString("0000")));

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

        private void _lvMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lvMessages.SelectedIndices.Count > 0)
            {
                _bStoreToFile.Enabled = true;
            }

            else
            {
                _bStoreToFile.Enabled = false;
            }
        }

        private void _bStoreToFile_Click(object sender, EventArgs e)
        {
            if (_sfdStoreMessage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ActiveUp.Net.Mail.Message message = (ActiveUp.Net.Mail.Message)_lvMessages.SelectedItems[0].Tag;

                    this.AddLogEntry("Message saved successfully.");
                }

                catch (Exception ex)
                {
                    this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
                }
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

  
    }
}

