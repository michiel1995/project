using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.ba.kassa.klant.webcam;
using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WebCam_Capture;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class GeldOpladenVM : ObservableObject, IPage
    {

        public GeldOpladenVM()
        {
            if (ApplicationVM.ingelogdeCustomer != null)
            {
                Customer = ApplicationVM.ingelogdeCustomer;
                Image = helper.byteArrayToImage(Customer.Image);
            }
        }

        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged("Customer"); }
        }
        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged("Image"); }
        }
        private Double _bedrag;

        public Double Bedrag
        {
            get { return _bedrag; }
            set { _bedrag = value; OnPropertyChanged("Bedrag"); }
        }

        public ICommand GeldOpladenCommand
        {
            get { return new RelayCommand(GeldOpladen); }
        }

        private async void GeldOpladen()
        {
            try
            {
                Customer.Balance += Bedrag;
                bool b = await servicelayer.UpdateCustomer(Customer);
                if (b == true)
                {
                    (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new GeldOpgeladenGedaanVM());
                }
            }
            catch (Exception ex )
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
            get { return "GeldOpladen"; }
        }


    }
}
