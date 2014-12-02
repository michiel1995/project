using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models
{
    public class Register_Employee
    {
        private Register _kassa;

        public Register Kassa
        {   
            get { return _kassa; }
            set { _kassa = value; }
        }
        private Employee _medewerker;

        public Employee Medewerker
        {
            get { return _medewerker; }
            set { _medewerker = value; }
        }
        private int _from;

        public int From
        {
            get { return _from; }
            set { _from = value; }
        }
        private int _until;

        public int Until
        {
            get { return _until; }
            set { _until = value; }
        }
        
        
        
    }
}
