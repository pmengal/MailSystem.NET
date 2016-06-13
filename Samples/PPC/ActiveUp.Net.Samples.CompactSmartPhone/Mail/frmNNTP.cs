using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.CompactSP
{
    /// <summary>
    /// Demonstrate the use of NNTP library
    /// </summary>
    public partial class frmNNTP : Form
    {
        /// <summary>
        /// Demonstrate the use of NNTP library
        /// </summary>
        public frmNNTP()
        {
            InitializeComponent();
        }

        private void btnRetreive_Click(object sender, EventArgs e)
        {
            //Retrieve newsgroup entries
            NntpClient nntp = new NntpClient();
            nntp.Connect(txtServer.Text);
            NewsGroup group = nntp.SelectGroup(txtnews.Text);
            //Display the listView1 of entries received
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

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}