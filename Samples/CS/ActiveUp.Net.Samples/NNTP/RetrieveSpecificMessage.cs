using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.NNTP
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

            _tbNewsgroup.Text = Config.Newsgroup;
            _tbNntpServer.Text = Config.NntpServer;
        }

        private void _bRetrieveSpecificMessage_Click(object sender, EventArgs e)
        {
            // We create nntp client object.
            NntpClient nntp = new NntpClient();

            try
            {
                // We connect to the nntp server.
                nntp.Connect(_tbNntpServer.Text);

                // Get a news group on the server
                NewsGroup group = nntp.SelectGroup(_tbNewsgroup.Text);

                if (group.ArticleCount > 0)
                {
                    for (int i = 1; i < group.ArticleCount + 1; i++)
                    {
                        ActiveUp.Net.Mail.Message message = group.RetrieveArticleObject(i);

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString("0000");
                        lvi.SubItems.AddRange(new string[] { message.Subject });
                        lvi.Tag = message;

                        _lvMessages.Items.Add(lvi);

                        this.AddLogEntry(string.Format("{1} Subject: {0}"
                                        , message.Subject, i.ToString("0000")));
                    }
                    
                }

                else
                {
                    this.AddLogEntry("There is no message in the newsgroup.");
                }
            }

            catch (NntpException pexp)
            {
                this.AddLogEntry(string.Format("Nntp Error: {0}", pexp.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }

            finally
            {
                if (nntp.IsConnected)
                {
                    nntp.Disconnect(); ;
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

