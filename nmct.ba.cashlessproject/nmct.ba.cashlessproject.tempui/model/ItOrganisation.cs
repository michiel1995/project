using System;
using System.Collections.Generic;
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

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private string _pass;

        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        private string _dbName;

        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }
        private string _dbLogin;

        public string DbLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }
        private string _dbPass;

        public string DBpass
        {
            get { return _dbPass; }
            set { _dbPass = value; }
        }
        private string _organisationName;

        public string OrganistionName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        #endregion  

    }
}
