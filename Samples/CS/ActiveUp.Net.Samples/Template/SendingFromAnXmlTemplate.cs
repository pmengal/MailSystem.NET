using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;


namespace ActiveUp.Net.Samples.Template
{
    public partial class SendingFromAnXmlTemplate : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public SendingFromAnXmlTemplate(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this.smtpServerAddressTextbox.Text = this.Config.MainSmtpServer;
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            this.AddLogEntry("Creating templater.");

            // We create the templater objet.
            try
            {
                ActiveUp.Net.Mail.Templater templater = new Templater(@"MailFormat.xml");

                this.AddLogEntry("Sending template message.");

                string smtp = string.Empty;

                if (_cbUseSmtpFromTemplate.Checked)
                {
                    smtp = templater.SmtpServers[0].Host;
                }
                else
                {
                    smtp = smtpServerAddressTextbox.Text;
                }

                SmtpClient.Send(templater.Message, smtp);

                this.AddLogEntry("Message sent successfully.");
            }

            catch (SmtpException ex)
            {
                this.AddLogEntry(string.Format("Smtp Error: {0}", ex.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }
        }

        private void _cbUseSmtpFromTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (_cbUseSmtpFromTemplate.Checked)
            {
                smtpServerAddressTextbox.Enabled = false;
            }

            else
            {
                smtpServerAddressTextbox.Enabled = true;
            }
        }

        private void _bAddXmlTemplate_Click(object sender, EventArgs e)
        {
            if (_ofdXmlTemplate.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(_ofdXmlTemplate.FileName);
                if (fi.Extension.ToLower() == ".xml")
                {
                    _tbXmlTemplate.Text = _ofdXmlTemplate.FileName;
                }
            }
        }

        private void _bShowTemplate_Click(object sender, EventArgs e)
        {
            this.LoadXml(_tbXmlTemplate.Text);        
        }

        private void _bShowSource_Click(object sender, EventArgs e)
        {
            try
            {
                this.HtmlCode = MasterForm.GetResource(this.GetType().ToString() + ".htm");
            }
            catch
            {
                this.HtmlCode = "Document not found.";
            }
        }
    }
}

