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
    public partial class FilterEmails : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public FilterEmails(SamplesConfiguration config)
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

        private void _bFilterEmails_Click(object sender, EventArgs e)
        {
            // We instantiate the pop3 client.
            Pop3Client pop = new Pop3Client();

            try
            {
                this.AddLogEntry(string.Format("Connection to the pop 3 server : {0}", _tbPop3Server.Text));

                // Connect to the pop3 client
                pop.Connect(_tbPop3Server.Text, _tbUserName.Text, _tbPassword.Text);

                AddressCollection ac;
                HeaderCollection hc = new HeaderCollection();

                //Retrive a message headers
                for (int n = 1; n < pop.MessageCount + 1; n++)
                {
                    Header h = pop.RetrieveHeaderObject(n);
                    ac = new AddressCollection();
                    ac.Add(h.From);
                    ac = Validator.Filter(ac);

                    //If address is not filtered
                    if (ac.Count > 0)
                    {
                        hc.Add(h);
                    }
                }

                this.AddLogEntry(string.Format(" {0} messages passed the filter", hc.Count.ToString()));
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

