using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.Net.Samples.CompactSP
{
    public partial class MainHost : Form
    {
        public MainHost()
        {
            InitializeComponent();
        }

        private void mnuItemDNS_DNSAny_Click(object sender, EventArgs e)
        {
            //launch DNS demo form
            frmDnsCheck dns = new frmDnsCheck();
            dns.ShowDialog();
            dns.Dispose();
        }

        private void mnuItemDNS_DNSMX_Click(object sender, EventArgs e)
        {
            //launch DNS MX demo form
            frmDnsMX dnsmx = new frmDnsMX();
            dnsmx.ShowDialog();
            dnsmx.Dispose();
        }

        private void mnuItemMailNNTP_Click(object sender, EventArgs e)
        {
            //launch NNTP demo form
            frmNNTP nntp = new frmNNTP();
            nntp.ShowDialog();
            nntp.Dispose();
        }

        private void mnuItemMailPOP3_Click(object sender, EventArgs e)
        {
            //launch POP3 demo form
            frmPOP3 pop3 = new frmPOP3();
            pop3.ShowDialog();
            pop3.Dispose();
        }

        private void mnuItemMailIMAP4_Click(object sender, EventArgs e)
        {
            //launch IMAP4 demo form
            frmIMAP4 imap = new frmIMAP4();
            imap.ShowDialog();
            imap.Dispose();
        }

        private void mnuItemMailSMTP_Click(object sender, EventArgs e)
        {
            //launch SMTP demo form
            frmSMTP smtp = new frmSMTP();
            smtp.ShowDialog();
            smtp.Dispose();
        }

        private void mnuItemWhoIs_Click(object sender, EventArgs e)
        {
            //launch WHO IS demo form
            frmWhois who = new frmWhois();
            who.ShowDialog();
            who.Dispose();
        }

        private void mnuItemExit_Click(object sender, EventArgs e)
        {
            //Exit the application
            Application.Exit();
        }
    }
}