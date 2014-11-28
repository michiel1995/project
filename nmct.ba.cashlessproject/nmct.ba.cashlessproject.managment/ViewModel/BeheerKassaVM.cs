using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class BeheerKassaVM : ObservableObject, IPage
    {
        private string _demo = "Demo";
        public string Demo
        {
            get { return _demo; }
            set { _demo = value; OnPropertyChanged("Demo"); }
        }


        public string Name
        {
            get { return "BeheerKassa"; }
        }
    }
}
