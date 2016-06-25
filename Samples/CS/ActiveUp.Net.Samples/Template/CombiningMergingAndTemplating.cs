using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Template
{
    public partial class CombiningMergingAndTemplating : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public CombiningMergingAndTemplating(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this._tbOrderId.Text = new Guid().ToString();
            this._comboProduct.SelectedIndex = 0;
            this.smtpServerAddressTextbox.Text = this.Config.MainSmtpServer;
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {

            try
            {
                //Let us create a data source in this case Hastable that would 
                //used to demonstrate the merging
                // Take the form variables collection as the data source.
                Hashtable dataSource = new Hashtable();
                dataSource.Add("FIRSTNAME", _tbFirstName.Text);
                dataSource.Add("LASTNAME", _tbLastName.Text);
                dataSource.Add("EMAIL", _tbEmail.Text);
                dataSource.Add("ORDER_ID", _tbOrderId.Text);
                dataSource.Add("CUSTOMER_ID", "1");
                dataSource.Add("PRODUCTS_TEXT", (string)_comboProduct.SelectedItem);

                // We create the templater object.
                ActiveUp.Net.Mail.Templater templater = new Templater(@"MailTemplate_for_datasource.xml");

                // We instanciante the Merger object by passing the templater data.
                Merger merger = new Merger();

                // We merge our DataSource and send the mail.
                merger.MergeMessage(templater.Message, dataSource, false);

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

        private void _bShowTemplate_Click(object sender, EventArgs e)
        {
            this.LoadXml(@"MailTemplate_for_datasource.xml");    
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

