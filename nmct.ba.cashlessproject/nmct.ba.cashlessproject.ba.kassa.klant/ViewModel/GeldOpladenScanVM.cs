using nmct.ba.cashlessproject.helper;
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
            set { _rfidNummer = value; OnPropertyChanged("RfidNummer"); if(RfidNummer != "") ControleerRfid(); }
        }

        private async void ControleerRfid()
        {

            try
            {
                int i;
                if (int.TryParse(RfidNummer, out i) && RfidNummer.Length == 10)
                {
                    Customer cust = await servicelayer.GetCustomer(i);
                    if (cust != null)
                    {
                        ApplicationVM.ingelogdeCustomer = cust;
                        (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new GeldOpladenVM());
                    }
                }
                else
                {
                    RfidNummer = "";
                }
            }
            catch (Exception ex)
            {
                Errorlog err = new Errorlog()
                {
                    Register = ApplicationVM.register,
                    Message = ex.Message,
                    Stacktrace = ex.StackTrace,
                    Timestamp = UnixTimestamp.ToUnixTimestamp(DateTime.Now)
                };
                servicelayer.PostLog(err);
            }
            
        }
        
        public string Name
        {
            get { return "GeldOpladenScan"; }
        }
    }
}
