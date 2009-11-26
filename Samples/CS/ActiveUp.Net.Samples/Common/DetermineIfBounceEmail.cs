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
    public partial class DetermineIfBounceEmail : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public DetermineIfBounceEmail(SamplesConfiguration config)
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

        private void _bCheckBounce_Click(object sender, EventArgs e)
        {
            // We instantiate the pop3 client.
            Pop3Client pop = new Pop3Client();

            try
            {
                this.AddLogEntry(string.Format("Connection to the pop 3 server : {0}", _tbPop3Server.Text));

                // We connect to the pop3 client
                pop.Connect(_tbPop3Server.Text, _tbUserName.Text, _tbPassword.Text);

                if (pop.MessageCount > 0)
                {
                    // We retrive a message at a particular index (index 1 in this sample)
                    ActiveUp.Net.Mail.Message message = pop.RetrieveMessageObject(1);
                    
                    BounceResult br = message.GetBounceStatus();

                    if (br.Level == 3)
                    {
                        this.AddLogEntry(string.Format("Message sent to  {0} is bounced", br.Email));
                    }
                    
                    else
                    {
                        this.AddLogEntry(string.Format("Message sent to {0} is not bounced", br.Email));
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

