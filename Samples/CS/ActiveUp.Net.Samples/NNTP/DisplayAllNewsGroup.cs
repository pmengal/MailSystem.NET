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
    public partial class DisplayAllNewsGroup : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public DisplayAllNewsGroup(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            _tbNntpServer.Text = Config.NntpServer;
        }

        private void _bRetrieveAllNewsgroup_Click(object sender, EventArgs e)
        {
            // We create nntp client object.
            NntpClient nntp = new NntpClient();

            try
            {
                // We connect to the nntp server.
                nntp.Connect(_tbNntpServer.Text);

                //Get the news group on the server
                NewsGroupCollection groups = nntp.GetNewsGroups();
                
                foreach (NewsGroup group in groups)
                {
                    this.AddLogEntry(string.Format("Group name : {0}",group.Name));
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

