using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Compact
{
    public partial class frmNNTP : Form
    {
        public frmNNTP()
        {
            InitializeComponent();
        }

        private void btnRetreive_Click(object sender, EventArgs e)
        {
            NntpClient nntp = new NntpClient();
            nntp.Connect(txtServer.Text);
            NewsGroup group = nntp.SelectGroup(txtnews.Text);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    listView1.Items.Add(new ListViewItem(group.RetrieveHeaderObject().Subject));
                    group.Next();
                }
                catch
                {
                }
            }
            nntp.Disconnect();
        }
    }
}