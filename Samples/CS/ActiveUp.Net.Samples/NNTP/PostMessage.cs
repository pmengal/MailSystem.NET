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
    public partial class PostMessage : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public PostMessage(SamplesConfiguration config)
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
            _tbSubject.Text = Config.DefaultSubject;
            _tbBody.Text = Config.DefaultBodyText;
        }

        private void _bPostMessage_Click(object sender, EventArgs e)
        {
            // We create nntp client object.
            NntpClient nntp = new NntpClient();

            try
            {
                // We connect to the nntp server.
                nntp.Connect(_tbNntpServer.Text);

                // We create the message to post.
                ActiveUp.Net.Mail.Message msg = new ActiveUp.Net.Mail.Message();
                msg.Subject = _tbSubject.Text;
                msg.BodyText.Text = _tbBody.Text;
                msg.To.Add(_tbNewsgroup.Text);
                nntp.Post(msg);  
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

