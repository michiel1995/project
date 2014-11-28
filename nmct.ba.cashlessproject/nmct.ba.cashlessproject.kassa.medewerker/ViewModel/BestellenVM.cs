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
            set { _klant = value; OnPropertyChanged("Klant");  }
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
            if(SelectedProduct != null)
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
                Verkoop.Add(sale);
            }
        }

        string IPage.Name
        {
            get { return "Bestellen"; }
        }
    }
}
