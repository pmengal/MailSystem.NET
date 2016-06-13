using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Compact.Mail.POP3
{
    public partial class Pop3Result : Form
    {
        private Pop3Client _pop;

        public Pop3Client Pop
        {
            get 
            { 
                return _pop; 
            }
            set 
            { 
                _pop = value; 
            }
        }

        /// <summary>
        /// Loads the Messages
        /// </summary>
        private void LoadMessages()
        {
            int MessageSize = (int)(Pop.TotalSize/1024);
            int MessageCount = Pop.MessageCount;

            //Set Title
            this.Text = MessageCount.ToString() + " messages, " + MessageSize.ToString() + " KB.";

            for (int i = 1; i <= MessageCount; i++)
            {
                //Get Header of Each Message
                byte[] data = Pop.RetrieveHeader(i);
                Header head = new Header();
                head.OriginalData = data;
                Parser.ParseHeader(ref head);

                ListViewItem litem = new ListViewItem(head.Date.ToShortDateString());
                litem.Tag = i.ToString();
                litem.SubItems.Add(head.Subject);
                litem.SubItems.Add(head.SenderIP.ToString());
                litem.SubItems.Add(head.From.Name);
                lvwMails.Items.Add(litem);
                
            }
        }

        public Pop3Result()
        {
            InitializeComponent();
        }

        private void Pop3Result_Load(object sender, EventArgs e)
        {
            LoadMessages();
        }

        private void lvwMails_ItemActivate(object sender, EventArgs e)
        {
            if (lvwMails.SelectedIndices.Count > 0)
            {
                ListViewItem lvitem = lvwMails.Items[lvwMails.SelectedIndices[0]];
                string MessageID = lvitem.Tag.ToString();
                frmPOP3.ToggleMyStatus(this);
                Message msg = Pop.RetrieveMessageObject(int.Parse(MessageID), false);
                frmPOP3.ToggleMyStatus(this);
                frmPOP3.Sleep();

                Pop3ItemView item = new Pop3ItemView();
                item.MessageObject = msg;
                item.ShowDialog();
                frmPOP3.Sleep();
            }
        }
    }
}