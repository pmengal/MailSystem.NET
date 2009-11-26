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
    public partial class RetrieveAsynchronously : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public RetrieveAsynchronously(SamplesConfiguration config)
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

        private void _bRetrieveMessageAsync_Click(object sender, EventArgs e)
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
                    //Retrive the message at a particulat index
                    pop.MessageRetrieved += new MessageRetrievedEventHandler(MessageRetrived);
                    pop.BeginRetrieveMessage(1, null);
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

        /// <summary>
        /// Event of the pop3 message retrived.
        /// </summary>
        /// <param name="sender">The sender objet.</param>
        /// <param name="e">The retrived message.</param>
        private void MessageRetrived(object sender, MessageRetrievedEventArgs e)
        {
            this.AddLogEntry(string.Format("Subject: {0} From :{1} Message Body {2}"
                                , e.Message.Subject, e.Message.From.Email, e.Message.BodyText));
        }
    }
}

