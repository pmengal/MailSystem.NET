using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;
using ActiveUp.Net.Samples.CompactSP.Mail.IMAP4;

namespace ActiveUp.Net.Samples.CompactSP
{
    public partial class frmIMAP4 : Form
    {
        public frmIMAP4()
        {
            InitializeComponent();
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Imap4Client client = new Imap4Client();
            client.Connect(txtHost.Text, int.Parse(txtPort.Text), txtUser.Text, txtPassword.Text);
            Imap4Result result = new Imap4Result();
            result.Imap = client;
            this.Enabled = true;
            result.ShowDialog();
            client.Disconnect();
            System.Threading.Thread.Sleep(0);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}