using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class BeheerKlantenVM: ObservableObject, IPage
    {
        public BeheerKlantenVM()
        {
            VulCustomersIn();
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

        private async void VulCustomersIn()
        {
            Foutmelding = "";
            Customer = await servicelayer.getCustomer();
            
        }
        private ObservableCollection<Customer> _customer;

        public ObservableCollection<Customer> Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged("Customer"); }
        }

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); if(SelectedCustomer != null) Image = helper.byteArrayToImage(SelectedCustomer.Image); }
        }

        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged("Image"); }
        }
        
        public ICommand Aanpassen
        {
            get { return new RelayCommand(PasAan); }
        }

        private void PasAan()
        {
            Status = "PasAan";
        }
        
        public ICommand Opslaan
        {
            get { return new RelayCommand(SlaOp); }
        }

        private async void SlaOp()
        {

            if (true)
            {
                Boolean b = false;
                if (Status == "PasAan")
                {
                    b = await servicelayer.PutCustomer(SelectedCustomer);
                }

                if (b == true)
                {
                    Status = null;
                    VulCustomersIn();
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
            VulCustomersIn();
        }


        public string Name
        {
            get{return "BeheerKlanten"; }
        }
    }
}
