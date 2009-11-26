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
    public partial class WorkingWithList : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public WorkingWithList(SamplesConfiguration config)
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
            if (_lvDataItems.Items.Count > 0)
            {
                // We prepare the data.
                // We will create a offline datatable to demonstrate , this can veru well
                // be fetched from database.

                DataSet dsOrderData = new DataSet();

                #region Create Data
                //Create a datatable
                DataTable tb = new DataTable("ORDERS");

                tb.Columns.Add("FIRSTNAME");
                tb.Columns.Add("LASTNAME");
                tb.Columns.Add("EMAIL");
                tb.Columns.Add("ORDER_ID");
                tb.Columns.Add("CUSTOMER_ID");
                tb.Columns.Add("PRODUCT");
                tb.Columns.Add("QUANTITY");

                //Create Sample set of data
                for (int i = 0; i < _lvDataItems.Items.Count; i++)
                {
                    Utils.ListRowItem lri = (Utils.ListRowItem)_lvDataItems.Items[i].Tag;

                    DataRow dr = tb.NewRow();
                    dr["FIRSTNAME"] = lri.FirstName;
                    dr["LASTNAME"] = lri.LastName;
                    dr["EMAIL"] = lri.Email;
                    dr["ORDER_ID"] = lri.OrderId;
                    dr["CUSTOMER_ID"] = i;
                    dr["PRODUCT"] = lri.Product;
                    dr["QUANTITY"] = lri.Quantity;
                    tb.Rows.Add(dr);
                }

                #endregion

                this.AddLogEntry("Creating templater.");

                // We create the templater object.
                ActiveUp.Net.Mail.Templater templater = new ActiveUp.Net.Mail.Templater("mailFormat_working_with_list.xml");

                // We instanciante the Merger object by passing the templater data.
                Merger merger = new Merger(templater);

                // We merge the message templates.
                merger.MergeListTemplate("PRODUCTS_TEXT", dsOrderData.Tables["ORDERS"]);
                merger.MergeListTemplate("PRODUCTS_HTML", dsOrderData.Tables["ORDERS"]);


                // We merge our message with the data.
                merger.MergeMessage(dsOrderData.Tables["ORDERS"]);

                this.AddLogEntry("Sending template message.");

                try
                {
                    // We send the mail
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

            else
            {
                this.AddLogEntry("No datas defined.");
            }
        }

        private void _bAdd_Click(object sender, EventArgs e)
        {
            WorkingWithListRowItem itemRow = new WorkingWithListRowItem();

            if (itemRow.ShowDialog() == DialogResult.OK)
            {
                Utils.ListRowItem item = (Utils.ListRowItem)itemRow.Item.Clone();

                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.FirstName;
                lvi.SubItems.AddRange(new string[] {item.LastName,item.Product,item.Quantity.ToString()});
                lvi.Tag = itemRow.Item;

                _lvDataItems.Items.Add(lvi);
            }
        }

        private void _lvDataItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_lvDataItems.SelectedIndices.Count > 0)
            {
                _bRemove.Enabled = true;
            }

            else
            {
                _bRemove.Enabled = false;
            }
        }

        private void _bRemove_Click(object sender, EventArgs e)
        {
            if (_lvDataItems.SelectedItems.Count > 0)
            {
                _lvDataItems.SelectedItems[0].Remove();
            }
        }

        private void _bShowTemplate_Click(object sender, EventArgs e)
        {
            this.LoadXml("mailFormat_working_with_list.xml");        
        }

        private void _bShowSource_Click(object sender, EventArgs e)
        {
            try
            {
                this.HtmlCode = MasterForm.GetResource(this.GetType().ToString() + ".htm");
            }
            catch
            {
                this.HtmlCode = "Document not found.";
            }
        }
    }
}

