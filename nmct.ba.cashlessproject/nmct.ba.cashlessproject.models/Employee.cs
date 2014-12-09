using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models
{
    public class Employee : IDataErrorInfo
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
      
        private string _addres;
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "tussen 2 en 50 cijfers")]
        public string Address
        {
            get { return _addres; }
            set { _addres = value; }
        }
    
        private string _email;
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
       
        private string _phone;
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3,4})$", ErrorMessage = "geef een geldig telefoonnummer")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
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
