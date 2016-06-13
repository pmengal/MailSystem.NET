using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Dns;

namespace ActiveUp.Net.Samples.Compact.DNS
{
    public partial class frmDNSResult : Form
    {
        Dns.DnsAnswer answer;
        
        public Dns.DnsAnswer Output
        {
            set
            {
                answer = value;
                if (answer != null && answer.Answers.Count>0)
                {
                    this.Text = "Results : " + answer.Answers[0].Domain;
                }
            }
        }
        
        public frmDNSResult()
        {
            InitializeComponent();
        }

        private void frmDNSResult_Load(object sender, EventArgs e)
        {
            if (answer != null)
            {
                //Fill The ListView Items in the Collection
                FillLstView();
            }
        }

        private void FillLstView()
        {
            
            if (answer.Answers != null)
            {
                foreach (Answer ans in answer.Answers)
                {
                    if (ans != null)
                    {
                        ListViewItem lvitem = new ListViewItem(ans.Data.ToString());
                        lvwResults.Items.Add(lvitem);
                    }
                }
            }

            if (answer.Additional != null)
            {
                foreach (Record ans in answer.Additional)
                {
                    if (ans != null)
                    {
                        ListViewItem lvitem = new ListViewItem(ans.Data.ToString());
                        lvwResults.Items.Add(lvitem);
                    }
                }
            }

        }

        private void lvwResults_ItemActivate(object sender, EventArgs e)
        {
            
        }
    }
}