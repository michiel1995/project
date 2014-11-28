using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class GeldOpKaartVM : ObservableObject, IPage
    {
        private string _hoeveel ="Het bedrag op je kaart bedraagt: ... €";

        public string Hoeveel
        {
            get { return _hoeveel; }
            set { _hoeveel = value;OnPropertyChanged("Hoeveel"); }
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
            if (int.TryParse(RfidNummer, out i) )
            {
                Customer cust =  await servicelayer.GetCustomer(i);
                Hoeveel = "Het bedrag op je kaart bedraagt: €" +cust.Balance;   
            }
            else
            {
                RfidNummer = "";
            }
        }
        public string Name
        {
            get { return "GeldOpKaart"; }
        }
    }
}
