using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.kassa.medewerker.ViewModel
{
    class BestellenVM : ObservableObject, IPage
    {
        public BestellenVM()
        {
            if (ApplicationVM.token != null)
            {
                LijstenOphalen();
            }
        }
        private string _foutmelding;

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value;OnPropertyChanged("Foutmelding");  }
        }
        

        private async void LijstenOphalen()
        {
            VolledigeLijst = await Servicelayer.getProducts();
            Producten = VolledigeLijst;
            Klant = ApplicationVM.ingelogdeCustomer;
            Verkoop = new ObservableCollection<Sale>();
        }
        private Customer _klant;

        public Customer Klant
        {
            get { return _klant; }
            set{ _klant = value; OnPropertyChanged("Klant");  }
        }
        private Double _totaal;

        public Double Totaal
        {
            get { return _totaal; }
            set { _totaal = value; OnPropertyChanged("Totaal"); }
        }
        

        public string Name
        {
            get { return "Bestellen"; }
        }
        private ObservableCollection<Product> _volledigelijst;

        
        public ObservableCollection<Product> VolledigeLijst
        {
            get { return _volledigelijst; }
            set { _volledigelijst = value; }
        }
        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }
        
        private ObservableCollection<Product> _producten;

        public ObservableCollection<Product> Producten
        {
            get { return _producten; }
            set { _producten = value; OnPropertyChanged("Producten"); }
        }
        private ObservableCollection<Sale> _verkoop;

        public ObservableCollection<Sale> Verkoop
        {
            get { return _verkoop; }
            set { _verkoop = value; OnPropertyChanged("Verkoop"); }
        }
        private Sale _selectedVerkoop;

        public Sale SelectedVerkoop
        {
            get { return _selectedVerkoop; }
            set { _selectedVerkoop = value; OnPropertyChanged("SelectedVerkoop"); }
        }
        
        
        public ICommand SorterenCommand
        {
            get { return new RelayCommand<string>(Sorteren); }
        }

        private void Sorteren(string letter)
        {
            var prod = VolledigeLijst.Where(x => x.ProductName.StartsWith(letter) || x.ProductName.StartsWith(letter.ToLower()));
            Producten = new ObservableCollection<Product>(prod);

        }

        public ICommand AantalCommand
        {
            get { return new RelayCommand<string>(Aantal); }
        }

        private void Aantal(string aantal)
        {
            try
            {
                if (SelectedProduct != null)
                {

                    Sale sale = new Sale()
                    {
                        Customer = Klant,
                        Product = SelectedProduct,
                        Amount = Convert.ToUInt32(aantal),
                        Price = Convert.ToUInt32(aantal) * SelectedProduct.Price,
                        Timestamp = UnixTimestamp.ToUnixTimestamp(DateTime.Now),
                        Register = ApplicationVM.register
                    };
                    // Klant.Balance -= sale.Price;
                    if (Klant.Balance - sale.Price <= 0)
                    {
                        Foutmelding = "Er staat te weinig geld op kaart";
                    }
                    else
                    {
                        Customer cust = Klant;
                        cust.Balance -= sale.Price;
                        Klant = cust;
                        Totaal += sale.Price;
                        Verkoop.Add(sale);
                    }
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
        public ICommand Delete
        {
            get { return new RelayCommand(verwijderVerkoop); }
        }

        private void verwijderVerkoop()
        {
            try
            {
                if (SelectedVerkoop != null)
                {
                    Customer cust = Klant;
                    cust.Balance += SelectedVerkoop.Price;
                    Klant = cust;
                    Totaal -= SelectedVerkoop.Price;
                    Verkoop.Remove(SelectedVerkoop);
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

        public ICommand Opslaan
        {
            get { return new RelayCommand(OpslaanSale); }
        }

        private async void OpslaanSale()
        {
            try
            {
                Boolean b = await Servicelayer.PutCustomer(Klant);
                Boolean b2 = await Servicelayer.SaveSales(Verkoop);
                if (b == true && b2 == true)
                {
                    ApplicationVM.ingelogdeCustomer = null;
                    (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new BestellingAfgerondVM());
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

        public ICommand Terug
        {
            get { return new RelayCommand(GaTerug); }
        }

        private void GaTerug()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new StartupscreenVM());
        }

        string IPage.Name
        {
            get { return "Bestellen"; }
        }
    }
}
