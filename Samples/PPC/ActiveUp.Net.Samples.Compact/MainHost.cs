using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.Net.Samples.Compact
{
    public partial class MainHost : Form
    {
        public MainHost()
        {
            InitializeComponent();
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            frmDnsCheck dns = new frmDnsCheck();
            dns.ShowDialog();
            dns.Dispose();
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            frmDnsMX dnsmx = new frmDnsMX();
            dnsmx.ShowDialog();
            dnsmx.Dispose();
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            frmvCard card = new frmvCard();
            card.ShowDialog();
            card.Dispose();
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            frmVcalendar cal = new frmVcalendar();
            cal.ShowDialog();
            cal.Dispose();
        }

        private void menuItem13_Click(object sender, EventArgs e)
        {
            frmvEvent ev = new frmvEvent();
            ev.ShowDialog();
            ev.Dispose();
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            frmvToDo todo = new frmvToDo();
            todo.ShowDialog();
            todo.Dispose();
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            frmNNTP nntp = new frmNNTP();
            nntp.ShowDialog();
            nntp.Dispose();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            frmPOP3 pop3 = new frmPOP3();
            pop3.ShowDialog();
            pop3.Dispose();
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            frmIMAP4 imap = new frmIMAP4();
            imap.ShowDialog();
            imap.Dispose();
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            frmSMTP smtp = new frmSMTP();
            smtp.ShowDialog();
            smtp.Dispose();
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            frmBinary binary = new frmBinary();
            binary.ShowDialog();
            binary.Dispose();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            frmWhois who = new frmWhois();
            who.ShowDialog();
            who.Dispose();
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}