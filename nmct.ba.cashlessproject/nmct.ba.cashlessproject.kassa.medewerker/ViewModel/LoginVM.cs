using nmct.ba.cashlessproject.helper;
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

        public LoginVM()
        {
            if(ApplicationVM.token ==null)
            {
                ApplicationVM.getToken();
            }
        }

        private string _rfidNummer;

        public string RfidNummer
        {
            get { return _rfidNummer; }
            set { _rfidNummer = value; OnPropertyChanged("RfidNummer"); ControleerRfid(); }
        }

        private async void ControleerRfid()
        {
            try
            {
                int i;
                if (int.TryParse(RfidNummer, out i) && RfidNummer.Length == 10)
                {
                    Employee employee = await Servicelayer.GetMedewerker(i);
                    if (employee != null)
                    {
                        ApplicationVM.ingelogdeMedewerker = employee;
                        (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new StartupscreenVM());
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
                Servicelayer.PostLog(err);
            }
           
        }
    }
}
