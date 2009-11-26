using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.Net.Samples.Template
{
    public partial class WorkingWithListRowItem : Form
    {
        Utils.ListRowItem _item = new Utils.ListRowItem();

        public WorkingWithListRowItem()
        {
            InitializeComponent();

            _tbOrderId.Text = Guid.NewGuid().ToString("d");
            _comboProduct.SelectedIndex = 0;
        }

        public Utils.ListRowItem Item
        {
            get
            {
                return _item;
            }
        }

        private void _bOk_Click(object sender, EventArgs e)
        {
            _item.FirstName = _tbFirstName.Text;
            _item.LastName = _tbLastName.Text;
            _item.Email = _tbEmail.Text;
            _item.OrderId = _tbOrderId.Text;
            _item.Product = (string)_comboProduct.SelectedItem;
            _item.Quantity = (int)_nudQuantity.Value;
        }
    }
}