using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace nmct.ba.cashlessproject.models
{
    public class Customer : IDataErrorInfo
    {
        #region property's
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "tussen 2 en 50 cijfers")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _address;
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "tussen 2 en 50 cijfers")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private Byte[] _image;

        public Byte[] Image
        {
            get { return _image; }
            set { _image = value; }
        }
        private double _balance;
        [Range(0,100000)]
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
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
