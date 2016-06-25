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
    public partial class RetrieveNewMessages : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public RetrieveNewMessages(SamplesConfiguration config)
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

        private void _bNewMessages_Click(object sender, EventArgs e)
        {
            // We create nntp client object.
            NntpClient nntp = new NntpClient();

            try
            {
                // We connect to the nntp server.
                nntp.Connect(_tbNntpServer.Text);
                
                // Get a news group on the server
                // Gets article ids that belong to the "myhost.info" newsgroup and have been added less than one month ago.
                string[] newids = nntp.GetNewArticleIds(_tbNewsgroup.Text, System.DateTime.Now.AddMonths(-1));

                // We get each article as an object.
                foreach (string articleId in newids)
                {
                    ActiveUp.Net.Mail.Message message = nntp.RetrieveArticleObject(articleId);
                    //Display the subject.
                    this.AddLogEntry(string.Format("Subject : {0}",message.Subject));
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
    }
}

