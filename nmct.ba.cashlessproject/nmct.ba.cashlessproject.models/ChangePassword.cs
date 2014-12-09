using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models
{
    public class ChangePassword
    {
        private string _old;

        public string Old
        {
            get { return _old; }
            set { _old = value; }
        }
        private string _new;

        public string New
        {
            get { return _new; }
            set { _new = value; }
        }
        private string _replay;

        public string Replay
        {
            get { return _replay; }
            set { _replay = value; }
        }
        
        
    }
}
