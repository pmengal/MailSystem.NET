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
    public partial class DisplayMessageCount : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public DisplayMessageCount(SamplesConfiguration config)
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

        private void _bCountMessage_Click(object sender, EventArgs e)
        {
            // We create nntp client object.
            NntpClient nntp = new NntpClient();

            try
            {
                // We connect to the nntp server.
                nntp.Connect(_tbNntpServer.Text);

                // Get a news group on the server
                NewsGroup group = nntp.SelectGroup(_tbNewsgroup.Text);

                // Display the message count.
                string.Format("Message Count: {0}", group.ArticleCount.ToString());
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
    }
}

