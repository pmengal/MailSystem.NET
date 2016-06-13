using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;
using ActiveUp.Net.Samples.CompactSP.Mail.POP3;

namespace ActiveUp.Net.Samples.CompactSP
{
    public partial class frmPOP3 : Form
    {
        public frmPOP3()
        {
            InitializeComponent();
        }

        public static void Sleep()
        {
            System.Threading.Thread.Sleep(0);
        }

        public static void ToggleMyStatus(Form theForm)
        {
            if (theForm.Enabled)
            {
                //Disable
                theForm.Enabled = false;
                Sleep();
            }
            else
            {
                //Enable
                theForm.Enabled = true;
                //Reenable Controls
                foreach (Control c in theForm.Controls)
                    c.Enabled = true;
                Sleep();
            }
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            ToggleMyStatus(this);//Enable
            try
            {
                Pop3Client client = new Pop3Client();
                if (chkAPOP.Checked)
                {
                    client.APOPConnect(txtHost.Text, txtUser.Text, txtPassword.Text);
                }
                else
                    client.Connect(txtHost.Text, txtUser.Text, txtPassword.Text);

                if (client.MessageCount == 0)
                {
                    client.Disconnect();
                    Sleep();
                    MessageBox.Show("You do not have any new messages");
                }
                else
                {
                    //Load up, POP3Result.
                    //Pass up Pop3Client, for further operations
                    Pop3Result result = new Pop3Result();
                    result.Pop = client;
                    result.ShowDialog();
                    client.Disconnect();
                    Sleep();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            ToggleMyStatus(this);//Disable
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}