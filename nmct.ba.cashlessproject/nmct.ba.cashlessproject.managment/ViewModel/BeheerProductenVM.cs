using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class BeheerProductenVM : ObservableObject, IPage
    {
        public BeheerProductenVM()
        {
            if (ApplicationVM.token != null)
            {
                VulProductsIn();
            }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        private string _foutmelding;

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; OnPropertyChanged("Foutmelding"); }
        }
        
        private async void VulProductsIn()
        {
            Foutmelding = "";
            Products = await servicelayer.getProducts();
            voledigeLijst = Products;
            Zoek = "";
        }


        private string _zoek;


        public string Zoek
        {
            get { return _zoek; }
            set
            {
                _zoek = value; OnPropertyChanged("Zoek");
                if (Zoek != "" && Zoek != null)
                {

                    var lijst = voledigeLijst.Where(x => x.ProductName.ToLower().StartsWith(Zoek.ToLower()));
                    Products = new ObservableCollection<Product>(lijst);
                }
                else
                {
                    Products = voledigeLijst;
                }
            }
        }
        private static ObservableCollection<Product> voledigeLijst;


        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged("Products"); }
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); if (SelectedProduct != null) { if (SelectedProduct.ProductName != null) Status = "Selected"; } }
        }

        public ICommand Aanpassen
        {
            get { return new RelayCommand(PasAan); }
        }

        private void PasAan()
        {
            Status = "PasAan";
        }
        public ICommand Verwijderen
        {
            get { return new RelayCommand(Verwijder); }
        }

        private async void Verwijder()
        {
            if (SelectedProduct != null )
            {
                bool b = await servicelayer.DeleteProduct(SelectedProduct.Id);
                if (b == true)
                {
                    Status = null;
                    VulProductsIn();
                }
                else
                {
                    Foutmelding = "Er is een fout opgetreden probeer het nog eens opnieuw";
                }
            }
            else{
                Foutmelding = "Selecteer een product";
            }
        }
        public ICommand Toevoegen
        {
            get { return new RelayCommand(VoegToe); }
        }

        private void VoegToe()
        {
            Status = "VoegToe";
            SelectedProduct = new Product();
        }
        public ICommand Opslaan
        {
            get { return new RelayCommand(SlaOp); }
        }

        private async void SlaOp()
        {

            if (SelectedProduct.Price != 0 && SelectedProduct.ProductName != null && SelectedProduct.ProductName != "")
            {
                Boolean b = false;
                if (Status == "VoegToe")
                {
                    b= await servicelayer.AddProduct(SelectedProduct);
                }
                else if (Status == "PasAan")
                {
                    b= await servicelayer.PutProduct(SelectedProduct);
                }

                if (b == true)
                {
                    Status = null;
                    VulProductsIn();
                    SelectedProduct = null;
                }
                else
                {
                    Foutmelding = "Er is een fout gebeurd probeer het nog eens opnieuw";
                }
            }
            else
            {
                Foutmelding = "Zijn alle waarden correct ingevuld?";
            }

        }
        public ICommand Anuleren
        {
            get { return new RelayCommand(Anuleer); }
        }

        private void Anuleer()
        {
            Status = null;
            VulProductsIn();
        }

        public string Name
        {
            get { return "Producten"; }
        }


    }
}
