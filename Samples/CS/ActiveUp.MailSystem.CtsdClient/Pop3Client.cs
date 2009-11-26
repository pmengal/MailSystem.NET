using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ActiveUp.Net.Mail;

namespace ActiveUp.MailSystem.CtsdClient
{
    public partial class Pop3Client : Form
    {
        public ActiveUp.Net.Mail.Message _selectedMessage;

        public Pop3Client()
        {
            InitializeComponent();
        }

        private void retrieveMessagesButton_Click(object sender, EventArgs e)
        {
            ActiveUp.Net.Mail.Pop3Client client = new ActiveUp.Net.Mail.Pop3Client();
            client.Connect(this.pop3ServerHostTextbox.Text, Convert.ToInt32(this.pop3ServerPortNumericUpDown.Value),
                this.pop3ServerUsernameTextbox.Text, this.pop3ServerPasswordTextbox.Text);
            
            HeaderCollection headers = new HeaderCollection();
            for(int index = 1; index <= client.MessageCount; index++)
                headers.Add(client.RetrieveHeaderObject(index));

            client.Close();

            this.dataGridView1.DataSource = headers;
        }

        private void useSelectedButton_Click(object sender, EventArgs e)
        {
            ActiveUp.Net.Mail.Pop3Client client = new ActiveUp.Net.Mail.Pop3Client();
            client.Connect(this.pop3ServerHostTextbox.Text, Convert.ToInt32(this.pop3ServerPortNumericUpDown.Value),
                this.pop3ServerUsernameTextbox.Text, this.pop3ServerPasswordTextbox.Text);

            Header gridSelection = (Header)this.dataGridView1.SelectedRows[0].DataBoundItem;

            _selectedMessage = client.RetrieveMessageObject(gridSelection.IndexOnServer);

            client.Close();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}