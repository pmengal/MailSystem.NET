using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Template
{
    public partial class MailMergingFromDataSources : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public MailMergingFromDataSources(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this.toEmailTextbox.Text = this.Config.ToEmail;
            this.fromEmailTextbox.Text = this.Config.FromEmail;
            this.subjectTextbox.Text = this.Config.DefaultSubject;
            this.smtpServerAddressTextbox.Text = this.Config.MainSmtpServer;
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            // Let us create a data source in this case Hastable that would 
            // used to demonstrate the merging
            // Take the form variables collection as the data source.
            Hashtable dataSource = new Hashtable();
            dataSource.Add("FIRSTNAME", "John");
            dataSource.Add("LASTNAME", "Richards");
            dataSource.Add("MESSAGE", "This is a test mail.");
            dataSource.Add("VAR1", "This is a variable.");

            // We create the message object.
            Message message = new Message();

            //We assign the sender email
            message.From.Email = this.fromEmailTextbox.Text;

            // We assign the recipient email
            message.To.Add(this.toEmailTextbox.Text);

            // We assign the subject
            message.Subject = this.subjectTextbox.Text;
            
            // We create the template.
            System.Text.StringBuilder messageTemplate = new System.Text.StringBuilder();
            messageTemplate.Append("Request posted\n\n");
            messageTemplate.Append("Firstname : $FIRSTNAME$\n");
            messageTemplate.Append("Lastname : $LASTNAME$\n");
            messageTemplate.Append("Message : $MESSAGE$\n");

            message.BodyText.Text = messageTemplate.ToString();

            Merger merger = new Merger();

            // We merge our DataSource
            merger.MergeMessage(message, dataSource, false);

            //Handle the error in case any
            try
            {
                // We send the mail
                message.Send(smtpServerAddressTextbox.Text);

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
    }
}

