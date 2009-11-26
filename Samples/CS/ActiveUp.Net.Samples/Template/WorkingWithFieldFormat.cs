using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Template
{
    public partial class WorkingWithFieldFormat : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public WorkingWithFieldFormat(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this.smtpServerAddressTextbox.Text = this.Config.MainSmtpServer;
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {

            // Prepare the data.
            //We will create a offline datatable to demonstrate , this can veru well
            //be fetched from database.

            this.AddLogEntry("Creating the data.");

            #region Create Data

            DataSet dsOrderData = new DataSet();

            #region Create Data
            // We create a datatable
            DataTable tb = new DataTable("ORDERS");

            tb.Columns.Add("FIRSTNAME");
            tb.Columns.Add("DATE", typeof(DateTime));
            tb.Columns.Add("CODEFORMATEDDATE", typeof(DateTime));
            tb.Columns.Add("CODEFORMATEDNUMBER", typeof(float));

            // We create sample set of data
            DataRow dr = tb.NewRow();
            dr["FIRSTNAME"] = "John";
            dr["DATE"] = DateTime.Now;
            dr["CODEFORMATEDDATE"] = DateTime.Now;
            dr["CODEFORMATEDNUMBER"] = 0.58;
            tb.Rows.Add(dr);
            dsOrderData.Tables.Add(tb);

            #endregion

            #endregion

            this.AddLogEntry("Creating templater.");

            try
            {
                ActiveUp.Net.Mail.Templater templater =
                    new ActiveUp.Net.Mail.Templater("mailformat_working_with_field_format.xml");

                // We instanciante the Merger object by passing the templater data.
                Merger merger = new Merger(templater);


                //Either you can specifiy the field format in the XML using <fieldformat>
                //tags are you can add the field format in the code

                merger.FieldsFormats.Add("CODEFORMATEDDATE", "{0:dd mmm yy}");
                merger.FieldsFormats.Add("CODEFORMATEDNUMBER", "{0:0#.##0}");

                // We merge our message with the data.
                merger.MergeMessage(dsOrderData.Tables[0]);
                
                this.AddLogEntry("Sending template message.");

                //Handle the error in case any
            
                merger.Message.Send(smtpServerAddressTextbox.Text);

                this.AddLogEntry("Message sent successfully.");
            }

            catch (SmtpException ex)
            {
                this.AddLogEntry(string.Format("Smtp Error: {0}", ex.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }
        }
    }
}

