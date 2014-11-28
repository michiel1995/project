using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models
{
   public class Sale
    {
        #region Property's
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _timestamp;

        public int Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set { _customer= value; }
        }
        private Register _register;

        public Register Register
        {
            get { return _register; }
            set { _register = value; }
        }
        private Product _product;

        public Product Product
        {
            get { return _product; }
            set { _product = value; }
        }
        private double _amount;

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        
        #endregion
    }
}
