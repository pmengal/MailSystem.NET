using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;
using ActiveUp.Net.Groupware;

namespace ActiveUp.Net.Samples.CompactSP.Mail.POP3
{
    public partial class Pop3ItemView : Form
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
        

        public Pop3ItemView()
        {
            InitializeComponent();
        }

        private void Pop3ItemView_Load(object sender, EventArgs e)
        {
            LoadMessage();
        }

        private void LoadMessage()
        {
            if (_message != null)
            {
                //Set title of the Form
                this.Text = _message.Subject;


                //Set the Message Object
                
                if (_message.BodyHtml.Text.Length>0)
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
                            lstAttachments.Items.Add( new ListViewItem(_message.LeafMimeParts[i].Filename));
                        }
                    }
                }



            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


    
}