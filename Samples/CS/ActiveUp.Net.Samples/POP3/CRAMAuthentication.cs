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
    public partial class CRAMAuthentication : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public CRAMAuthentication(SamplesConfiguration config)
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

        private void _bRetrieveMessage_Click(object sender, EventArgs e)
        {
            // We instantiate the pop3 client.
            Pop3Client pop = new Pop3Client();

            try
            {
                this.AddLogEntry(string.Format("Connection to the pop 3 server : {0}", _tbPop3Server.Text));

                // We connect to the pop3 client
                pop.Connect(_tbPop3Server.Text);

                // We authenticate securly
                pop.Authenticate(_tbUserName.Text, _tbPassword.Text, SaslMechanism.CramMd5);

                if (pop.MessageCount > 0)
                {
                    //Retrive a message at a particulat index (index 1 in this sample)
                    ActiveUp.Net.Mail.Message message = pop.RetrieveMessageObject(1);

                    this.AddLogEntry(string.Format("Subject: {0} From :{1} Message Body {2}"
                                    , message.Subject, message.From.Email, message.BodyText));
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

