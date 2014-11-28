using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.kassa.medewerker.ViewModel
{
    class LoginVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Login"; }
        }

        private string _rfidNummer;

        public string RfidNummer
        {
            get { return _rfidNummer; }
            set { _rfidNummer = value; OnPropertyChanged("RfidNummer"); ControleerRfid(); }
        }

        private async void ControleerRfid()
        {
            int i;
            if (int.TryParse(RfidNummer, out i) && RfidNummer.Length == 10)
            {
                Customer cust = await Servicelayer.GetCustomer(i);
                if (cust != null)
                {                  
                    (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new StartupscreenVM());
                }
            }
            else
            {
                RfidNummer = "";
            }
        }
    }
}
