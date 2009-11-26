using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.WhoIs;

namespace ActiveUp.Net.Samples.CompactPPC
{
    public partial class frmWhois : Form
    {
        /// <summary>
        /// Get Who Is details for a domain
        /// </summary>
        public frmWhois()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            System.Threading.Thread.Sleep(0);
            WhoIs.WhoIs who = new ActiveUp.Net.WhoIs.WhoIs();
            who.Servers.Add(textBox1.Text, 43, txtDomainName.Text);
            txtresult.Text = who.Query(txtDomainName.Text);
            this.Enabled = true;
        }
    }
}