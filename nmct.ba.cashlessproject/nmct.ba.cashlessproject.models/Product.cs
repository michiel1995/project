using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models
{
    public class Product : IDataErrorInfo
    {
        #region property's
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        private string _productName;
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "tussen 2 en 50 cijfers")]
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
       
        private double _price;
        [Required]
       // [Range(0, 10000)]
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        
        #endregion

        public string Error
        {
            get { return null; }
        }
        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {

                    return ex.Message;
                }
                return String.Empty;
            }
        }
    }
}
