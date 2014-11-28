
using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.kassa.medewerker.ViewModel
{
    class StartupscreenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "StartupScreen"; }
        }
        private string _rfidNummer;

        public string RfidNummer
        {
            get { return _rfidNummer; }
            set { _rfidNummer = value; OnPropertyChanged("RfidNummer"); ControleerRfid(); }
        }
        private string _naam ;

        public string Naam 
        {
            get { return _naam; }
            set { _naam = value; OnPropertyChanged("Naam"); }
        }
        private double _bedrag ;

        public double Bedrag
        {
            get { return _bedrag; }
            set { _bedrag = value; OnPropertyChanged("Bedrag"); }
        }
        private Customer _cust;

        public Customer Cust
        {
            get { return _cust; }
            set { _cust = value; }
        }
        private string _adres;

        public string Adres
        {
            get { return _adres; }
            set { _adres = value; OnPropertyChanged("Adres"); }
        }
        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged("Image"); }
        }
        
        private async void ControleerRfid()
        {
            if (RfidNummer != "")
            {
                int i;
                if (int.TryParse(RfidNummer, out i)) // && RfidNummer.Length == 10
                {
                    Customer Customer = await Servicelayer.GetCustomer(i);
                    if (Customer != null)
                    {
                        Cust = Customer;
                        Naam =  Customer.Name;
                        Adres =  Customer.Address;
                        Bedrag =  Customer.Balance;
                        if (Customer.Image != null)
                            Image = helper.byteArrayToImage(Customer.Image);
                    }
                    RfidNummer = "";
                }
                else
                {
                    RfidNummer = "";
                }
            }
        }

        public ICommand BestellenCommand
        {
            get { return new RelayCommand(Bestellen); }
        }

        private void Bestellen()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new BestellenVM());
        }

    }
}
