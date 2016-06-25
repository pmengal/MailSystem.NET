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
    public partial class BayesianFilterCheck : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public BayesianFilterCheck(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

           /* _tbUserName.Text = Config.Pop3UserName;
            _tbPassword.Text = Config.Pop3Password;
            _tbPop3Server.Text = Config.Pop3Server;*/
        }

        private void isSpamButton_Click(object sender, EventArgs e)
        {
            ActiveUp.Net.Mail.Message _message = new ActiveUp.Net.Mail.Message();
            _message.BodyText.Text = this.messageTextbox.Text;
            BayesianFilter.ReportMessage(_message, "../../spam.txt");
        }

        private void notSpamButton_Click(object sender, EventArgs e)
        {
            ActiveUp.Net.Mail.Message _message = new ActiveUp.Net.Mail.Message();
            _message.BodyText.Text = this.messageTextbox.Text;
            BayesianFilter.ReportMessage(_message, "../../ham.txt");
        }

        private void checkSpamButton_Click(object sender, EventArgs e)
        {
            bool isSpam = BayesianFilter.AnalyzeMessage(string.Empty, this.messageTextbox.Text, "../../spam.txt",
                "../../ham.txt", "../../ignore.txt");

            MessageBox.Show(string.Format("Result of prediction: {0}", (isSpam ? "Message is spam" : "Message is not spam")));
        }

    }
}