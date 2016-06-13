using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WebProxyTest.WS_SMTP;

namespace ActiveUp.Net.Samples.SMTPWebProxy
{
    public partial class SendMail
    {
        public SendMail()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            WS_SMTP WSSender = new WS_SMTP();
            //Setup a cokkie store so that the state can be maintained
            WSSender.CookieContainer = new System.Net.CookieContainer();

            WSSender.SetFrom(txtFrom.Text);
            //Setup To addresses
            string[] addresses = txtTo.Text.Split(new char[]{',',';'});
            foreach(string address in addresses)
                WSSender.AddToAddress(address);

            //Setup CC Addresses
            string[] CCaddresses = txtCc.Text.Split(new char[] { ',', ';' });
            foreach (string address in CCaddresses)
                WSSender.AddCCAddress(address);

            //Setup SMTP server settings
            WSSender.AddHost(txtServer.Text);
            int port = 0;
            if (int.TryParse(txtPort.Text,out port))
            {
                WSSender.SetPort(port);
            }
            WSSender.SetUser(txtUserName.Text, txtPassword.Text);

            //Setup Mail content
            WSSender.SetSubject(txtSubject.Text);
            WSSender.SetBodyText(txtMessage.Text);

            //Send Mail
            if(WSSender.SendSessionedMail(false, false, SslProtocols.Default))
                MessageBox.Show("Message sent successfully");
            else
                MessageBox.Show("Message failed.");
        }
    }
}