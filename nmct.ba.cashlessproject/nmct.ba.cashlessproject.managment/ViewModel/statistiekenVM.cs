using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class statistiekenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Statistieken"; }
        }
    }
}
