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
    public partial class WorkingWithDomainKeys : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public WorkingWithDomainKeys(SamplesConfiguration config)
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

        private void _bDomainKey_Click(object sender, EventArgs e)
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
                    //Retrive a message at a particulat index
                    ActiveUp.Net.Mail.Message message = pop.RetrieveMessageObject(1);
                    if (message.HasDomainKeySignature)
                    {
                        bool signatureValid = message.Signatures.DomainKeys.Verify();

                        if (signatureValid)
                        {
                            this.AddLogEntry("The domain key signature is valid.");
                        }
                        else
                        {
                            this.AddLogEntry("The domain key signature is invalid.");
                        }
                    }
                    else
                    {
                        this.AddLogEntry("The message hasn't domain key signature.");
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
    }
}

