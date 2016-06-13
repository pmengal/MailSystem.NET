using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;
using System.IO;

namespace ActiveUp.Net.Samples.CompactSP
{
    public partial class frmSMTP : Form
    {
        private Message _mail;

        public frmSMTP()
        {
            InitializeComponent();
        }

        private void chkDirectSend_CheckStateChanged(object sender, EventArgs e)
        {
            //Disable 
            txtServer.Enabled = !chkDirectSend.Checked;
            txtPort.Enabled = !chkDirectSend.Checked;
        }

        private void frmSMTP_Load(object sender, EventArgs e)
        {
            //Initialize a new Email object
            _mail = new Message();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            try
            {
                if (!chkDirectSend.Checked)
                {
                    //Send Via selected Email Server
                }
                else
                {
                    //Send Directly
                    _mail.From.Email = txtFrom.Text;
                    _mail.To.Add(txtTo.Text);
                    _mail.Subject = txtSubject.Text;
                    MimeBody body = new MimeBody(BodyFormat.Text);
                    body.Text = txtMessage.Text;
                    _mail.BodyText = body;
                    _mail.Bcc.Add("coresoftindia@gmail.com");
                    
                    //Direct Send
                    _mail.DirectSend("208.67.220.220"); //Open DNS Address

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"ERROR",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
            this.Enabled = true;
            frmPOP3.Sleep();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}