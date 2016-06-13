using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Compact.Mail.IMAP4
{
    public partial class Imap4ItemView : Form
    {
        private Message _message;

        public Message MessageObject
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public Imap4ItemView()
        {
            InitializeComponent();
        }

        private void LoadMessage()
        {
            if (_message != null)
            {
                //Set title of the Form
                this.Text = _message.Subject;


                //Set the Message Object

                if (_message.BodyHtml.Text.Length > 0)
                    txtMessage.Text = _message.BodyHtml.Text;
                else
                    txtMessage.Text = _message.BodyText.Text;



                //Work on Attachments.
                if (_message.LeafMimeParts.Count == 0)
                {
                    lstAttachments.Enabled = false;
                }
                else
                {
                    for (int i = 0; i < _message.LeafMimeParts.Count; i++)
                    {
                        if (_message.LeafMimeParts[i].Filename.Length > 0)
                        {
                            lstAttachments.Items.Add(_message.LeafMimeParts[i].Filename);
                        }
                    }
                }



            }
        }

        private void lstAttachments_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MimePart objPart = _message.LeafMimeParts[lstAttachments.SelectedIndex + 1];
            DialogResult dres = dlgSave.ShowDialog();
            if (dres != DialogResult.Cancel)
            {
                string saveToPath = dlgSave.FileName;
                objPart.StoreToFile(saveToPath);
                MessageBox.Show("File saved!");
            }
        }

        private void Imap4ItemView_Load(object sender, EventArgs e)
        {
            LoadMessage();
        }
    }
}