using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;

namespace ActiveUp.MailSystem.CtsdClient
{
    public partial class ctasdClientForm : Form
    {
        public ctasdClientForm()
        {
            InitializeComponent();
        }

        private void fromFileButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.openMessageFromFileDialog.ShowDialog())
            {
                try
                {
                    this.currentStatusTextbox.Text = "Querying...";
                    CtchResponse response = CtchClient.QueryServer(this.hostTextBox.Text, Convert.ToInt32(this.portNumericUpDown.Value),
                        this.openMessageFromFileDialog.FileName);
                    this.spamClassificationTextbox.Text = response.SpamClassification.ToString();
                    this.vodClassificationTextbox.Text = response.VodClassification.ToString();
                    this.flagTextbox.Text = response.CtchFlag;
                    this.refIdTextbox.Text = response.RefID;
                    this.currentStatusTextbox.Text = string.Format("Done. Last query: {0}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error while contacting server. Please verify it is running\r\n{0}", ex.ToString()));
                }
            }
        }

        private void fromPop3Button_Click(object sender, EventArgs e)
        {
            Pop3Client pop3Client = new Pop3Client();
            pop3Client.ShowDialog();

            try
            {
                this.currentStatusTextbox.Text = "Querying...";
                CtchResponse response = CtchClient.QueryServer(this.hostTextBox.Text, Convert.ToInt32(this.portNumericUpDown.Value),
                    pop3Client._selectedMessage);
                this.spamClassificationTextbox.Text = response.SpamClassification.ToString();
                this.vodClassificationTextbox.Text = response.VodClassification.ToString();
                this.flagTextbox.Text = response.CtchFlag;
                this.refIdTextbox.Text = response.RefID;
                this.currentStatusTextbox.Text = string.Format("Done. Last query: {0}", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error while contacting server. Please verify it is running\r\n{0}", ex.ToString()));
            }
        }
    }
}