using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.ba.kassa.klant.webcam;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
            set { _image = value; OnPropertyChanged("Customer"); }
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
            Customer.Balance += Bedrag;
            bool b = await servicelayer.UpdateCustomer(Customer);
            if(b==true)
            {
                throw new Exception();
            }
        }
        
        public string Name
        {
            get { return "GeldOpladen"; }
        }

    }
}
