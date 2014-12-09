using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            set { _rfidNummer = value; OnPropertyChanged("RfidNummer"); if(RfidNummer !="") ControleerRfid(); }
        }

        private async void ControleerRfid()
        {
            int i;
            try
            {              
                if (int.TryParse(RfidNummer, out i))
                {
                    Customer cust = await servicelayer.GetCustomer(i);
                    Hoeveel = "Het bedrag op je kaart bedraagt: €" + cust.Balance;
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


        public ICommand Terug
        {
            get { return new RelayCommand(GaTerug); }
        }

        private void GaTerug()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new PortaalVM());
        }


        public string Name
        {
            get { return "GeldOpKaart"; }
        }
    }
}
