using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Dns;

namespace ActiveUp.Net.Samples.Compact
{
    public partial class frmDnsMX : Form
    {
        public frmDnsMX()
        {
            InitializeComponent();
        }

     

        private void btnLookUp_Click_1(object sender, EventArgs e)
        {
            if (txtHost.Text == null || txtHost.Text.Length == 0)
            {
                MessageBox.Show("Enter valid Host!");
                txtHost.Focus();
                return;
            }

            Application.DoEvents();
            sbrResult.Text = "Please wait...";
            this.Enabled = false;
            Application.DoEvents();


            DnsQuery query = new DnsQuery();

            query.Domain = txtHost.Text;
            query.DnsServer = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(txtDNS.Text.Trim()), 53);
            try
            {
                DnsAnswer answer = query.QueryServer(ParseEnumFromCombo());
                if (answer != null)
                {
                    DNS.frmDNSResult result = new ActiveUp.Net.Samples.Compact.DNS.frmDNSResult();
                    result.Output = answer;
                    result.ShowDialog();
                    result.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            sbrResult.Text = "Ready";
            this.Enabled = true;
            EnableControls();
            Application.DoEvents();

        }

        private RecordType ParseEnumFromCombo()
        {
            return RecordType.MX;
        }

        private void EnableControls()
        {
            foreach (Control c in this.Controls)
                c.Enabled = true;
        }
    }
}