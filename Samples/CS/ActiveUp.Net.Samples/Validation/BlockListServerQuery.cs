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
    public partial class BlockListServerQuery  : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public BlockListServerQuery(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();
        }

        private void queryServerButton_Click(object sender, EventArgs e)
        {
            RblServer rbl = new RblServer();

            RblStatus status = rbl.GetRblStatus(this.rblServersListbox.SelectedItem.ToString(), this.serverIpTextbox.Text);

            MessageBox.Show(status.ToString());
        }
    }
}