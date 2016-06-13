using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.CompactPPC.Mail.IMAP4
{
    public partial class Imap4Result : Form
    {
        public Imap4Client Imap;
        public Imap4Result()
        {
            InitializeComponent();
        }

        private void lvwMails_ItemActivate(object sender, EventArgs e)
        {
            if (lvwMails.SelectedIndices.Count > 0)
            {
                ListViewItem lvitem = lvwMails.Items[lvwMails.SelectedIndices[0]];
                string MessageID = lvitem.Tag.ToString();
                frmPOP3.ToggleMyStatus(this);
                Message msg = Imap.ExamineMailbox("inbox").Fetch.MessageObject(int.Parse(MessageID));
                frmPOP3.ToggleMyStatus(this);
                frmPOP3.Sleep();

                Imap4ItemView item = new Imap4ItemView();
                item.MessageObject = msg;
                item.ShowDialog();
                frmPOP3.Sleep();
            }
        }

        private void Imap4Result_Load(object sender, EventArgs e)
        {
            Mailbox inbox = Imap.ExamineMailbox("inbox");
            int MessageCount = inbox.MessageCount;
            for (int i = 1; i <= MessageCount; i++)
            {
                Header head = inbox.Fetch.HeaderObject(i);
                ListViewItem litem = new ListViewItem(head.Date.ToShortDateString());
                litem.Tag = i.ToString();
                litem.SubItems.Add(head.Subject);
                litem.SubItems.Add(head.SenderIP.ToString());
                litem.SubItems.Add(head.From.Name);
                lvwMails.Items.Add(litem);

            }
        }
    }
}