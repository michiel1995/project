using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class GeldOpladenScanVM : ObservableObject, IPage
    {
        private string _rfidNummer;

        public string RfidNummer
        {
            get { return _rfidNummer; }
            set { _rfidNummer = value; OnPropertyChanged("RfidNummer"); ControleerRfid(); }
        }

        private async void ControleerRfid()
        {
            int i;
            if(int.TryParse(RfidNummer, out i) && RfidNummer.Length ==10)
            {
                Customer cust =  await servicelayer.GetCustomer(i);
                if(cust != null)
                {
                    (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new GeldOpladenVM());
                }
            }
            else
            {
                RfidNummer = "";
            }
        }
        
        public string Name
        {
            get { return "GeldOpladenScan"; }
        }
    }
}
