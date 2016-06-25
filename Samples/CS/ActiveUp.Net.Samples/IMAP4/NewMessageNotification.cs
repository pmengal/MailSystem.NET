using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;
using System.Threading;

namespace ActiveUp.Net.Samples.IMAP4
{
    public partial class NewMessageNotification : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public static Imap4Client imap;
        public static NewMessageNotification thisForm;
        public BackgroundWorker worker;

        public NewMessageNotification(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this.imap4ServerTextbox.Text = Config.Imap4Server;
            this.usernameTextbox.Text = Config.Imap4UserName;
            this.passwordTextbox.Text = Config.Imap4Password;
            this.emailAddressTextbox.Text = Config.FromEmail;

            thisForm = this;

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(StartIdleProcess);
        }

        private void sendTestMessageButton_Click(object sender, EventArgs e)
        {
            this.AddLogEntry("Sending test message using DirectSend()");

            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
            message.From.Email = this.emailAddressTextbox.Text;
            message.To.Add(this.emailAddressTextbox.Text);
            message.Subject = "This is a notification test.";
            message.BodyText.Text = "This is a notification test.";

            SmtpClient.DirectSend(message);

            this.AddLogEntry("Notification test message sent.");
        }

        private void connectNotificationReceiver_Click(object sender, EventArgs e)
        {
            if (worker.IsBusy)
                worker.CancelAsync();

            worker.RunWorkerAsync();

            this.AddLogEntry("Notification Received Started. Waiting for a new message...");
        }

        private void StartIdleProcess(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            //BackgroundWorker worker = sender as BackgroundWorker;

            if (imap != null && imap.IsConnected)
            {
                imap.StopIdle();
                imap.Disconnect();
            }

            imap = new Imap4Client();
            imap.NewMessageReceived += new NewMessageReceivedEventHandler(NewMessageReceived);
            //worker.ReportProgress(1, "Connection...");
            imap.Connect(thisForm.imap4ServerTextbox.Text);
            //worker.ReportProgress(0, "Login...");
            imap.Login(thisForm.usernameTextbox.Text, thisForm.passwordTextbox.Text);
            //worker.ReportProgress(0, "Select 'inbox'...");
            imap.SelectMailbox("inbox");
            //worker.ReportProgress(0, "Start idle...");
            imap.StartIdle();
        }

        public static void NewMessageReceived(object source, NewMessageReceivedEventArgs e)
        {
            //thisForm.AddLogEntry("New message received :" + e.MessageCount);
            MessageBox.Show("New message received :" + e.MessageCount);

            //imap4.StopIdle();
        }
    }
}