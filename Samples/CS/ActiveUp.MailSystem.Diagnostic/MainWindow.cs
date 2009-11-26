using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;

namespace ActiveUp.MailSystem.Diagnostic
{
    public partial class MainWindow : Form
    {
        private ActiveUp.Net.Mail.Message _selectedMessage;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void messagesExplorerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.messageRfc822RawTextbox.Text = this.messagesExplorerListBox.SelectedItem.ToString();

            ActiveUp.Net.Mail.Message message = ActiveUp.Net.Mail.Parser.ParseMessage(this.messageRfc822RawTextbox.Text);
            this.messageDetailObjectExplorer.SelectedObject = message;
            _selectedMessage = message;
            this.dataGridView1.DataSource = message.Attachments;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.loadMessagesFilesOpenFileDialog.ShowDialog();

            System.IO.StreamReader reader = new System.IO.StreamReader(this.loadMessagesFilesOpenFileDialog.FileName);
            string rawMessage = reader.ReadToEnd();

            this.messagesExplorerListBox.Items.Add(rawMessage);
        }

        private void saveAttachmentsToDisk_Click(object sender, EventArgs e)
        {
            this.saveAttachmentsToDiskDialog.ShowDialog();

            //_selectedMessage.Attachments[0].StoreToFile("c:\\");
            foreach (ActiveUp.Net.Mail.MimePart mime in this._selectedMessage.Attachments)
            {
                mime.StoreToFile("c:\\" + mime.ContentName);
            }
        }
    }
}