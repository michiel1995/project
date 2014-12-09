using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models
{
   public class ItOrganisation
    {
        #region property's
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _login;

        [Required(ErrorMessage="Gelieve een login naam in te geven")]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private string _pass;
       [Required(ErrorMessage = "Gelieve een paswoord in te geven")]
        [StringLength(50, ErrorMessage = "Het paswoord moet minum 6tekens lang zijn", MinimumLength = 6)]
        [RegularExpression(@"((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Het wachtwoord moet een kleine letter,hoofletter en een numeriek teken bevatten")]
     
        
        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        private string _dbName;
       [Required(ErrorMessage = "Gelieve een Database naam in te geven")]
        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }
        private string _dbLogin;
       [Required(ErrorMessage = "Gelieve een Databse Login in te geven")]
        public string DbLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }
        private string _dbPass;
        [Required(ErrorMessage = "Gelieve een Database Paswoord in te geven")]
        [StringLength(50, ErrorMessage = "Het paswoord moet minum 6tekens lang zijn", MinimumLength = 6)]
        [RegularExpression(@"((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage="Het wachtwoord moet een kleine letter,hoofletter en een numeriek teken bevatten")]
     
        public string DBpass
        {
            get { return _dbPass; }
            set { _dbPass = value; }
        }
        private string _organisationName;
       [Required(ErrorMessage = "Gelieve een Organisatie Naam in te geven")]
        public string OrganistionName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }
        private string _email;
       [Required(ErrorMessage = "Gelieve een email-adres in te geven")]
       [EmailAddress]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _address;
       [Required(ErrorMessage = "Gelieve een Adres in te geven")]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _phone;
       [Required(ErrorMessage = "Gelieve een Telefoonnummer in te geven")]
       [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3,4})$", ErrorMessage = "geef een geldig telefoonnummer")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        #endregion  

    }
}
