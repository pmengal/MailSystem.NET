using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveUp.Net.Samples.Utils
{
    #region ListRowItem

    public class ListRowItem : ICloneable
    {
        #region Fields

        string _firstName;
        string _lastName;
        string _email;
        string _orderId;
        string _product;
        int _quantity;

        #endregion

        #region Constructors

        public ListRowItem()
        {
        }

        #endregion

        #region Properties

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public string OrderId
        {
            get
            {
                return _orderId;
            }

            set
            {
                _orderId = value;
            }
        }

        public string Product
        {
            get
            {
                return _product;
            }

            set
            {
                _product = value;
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
            }
        }

        #endregion

        #region Interface IClonable

        public object Clone()
        {
            ListRowItem item = new ListRowItem();

            item.FirstName = this.FirstName;
            item.LastName = this.LastName;
            item.Email = this.Email;
            item.OrderId = this.OrderId;
            item.Quantity = this.Quantity;
            item.Product = this.Product;

            return item;
        }     

        #endregion

    }

    #endregion
}
