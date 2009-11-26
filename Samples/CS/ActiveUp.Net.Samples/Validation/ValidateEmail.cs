using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Validation
{
    public partial class ValidateEmail : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public ValidateEmail(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            _tbDnsServer.Text = this.Config.DnsServer;
        }

        private void _bValidateEmail_Click(object sender, EventArgs e)
        {
            bool result = false;
            
            if (_cbUseDnsToValidateEmail.Checked && _tbDnsServer.Text != string.Empty)
                result = Validator.Validate(_tbEmail.Text, _tbDnsServer.Text);
            else
                result = Validator.Validate(_tbEmail.Text);

            if (result)
            { 
                this.AddLogEntry("Valid Email address"); 
            }
            else
            {
                this.AddLogEntry("Invalid Email address"); 
            }
        }
    }
}

