using be.belgium.eid;
using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.ba.kassa.klant.idreader;
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
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class RegistrerenVM : ObservableObject, IPage
    {
        private Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged("Customer"); }
        }
        
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); if(Id != "") ControleerId(); }
        }
        private string _Foutmelding;

        public string Foutmelding
        {
            get { return _Foutmelding; }
            set { _Foutmelding = value; OnPropertyChanged("Foutmelding"); }
        }
        //private int _idFoutmelding;

        //public int IdFoutmelding
        //{
        //    get { return _idFoutmelding; }
        //    set { _idFoutmelding = value; OnPropertyChanged("IdFoutmelding"); }
        //}

        private string _foutmeldingIdentiteit;

        public string FoutmeldingIdentiteit
        {
            get { return _foutmeldingIdentiteit; }
            set { _foutmeldingIdentiteit = value; OnPropertyChanged("FoutmeldingIdentiteit"); }
        }
        
        
        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged("Image"); }
        }
        public ICommand LeesIdentiteitskaart
        {
            get { return new RelayCommand(LeesKaart); }
        }

        private void ControleerId()
        {
            try
            {
                int i;
                if (!int.TryParse(Id, out i) && Id.Length != 10)
                {
                    //IdFoutmelding = 1;
                    Foutmelding = "er is een fout gebeurd probeer opnieuw";
                    Id = "";
                }
                else
                {
                    //IdFoutmelding = 2;
                    Customer.Id = i;
                    Foutmelding = "Kaart is Ingelezen";
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
        private void LeesKaart()
        {
            try
            {
                ReadId info = ReadId.Lees();
                if (info == null)
                {
                    FoutmeldingIdentiteit = "Er is een fout gebeurd bij het inlezen van de identititeitskaart probeer opnieuw!";
                }
                else
                {
                    Customer = new Customer()
                    {
                        Name = info.Name,
                        Address = info.Adres,
                        Image = info.Bytes,
                        Balance = 0
                    };
                    FoutmeldingIdentiteit = "";

                    Image = helper.byteArrayToImage(info.Bytes);
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
        public string Name
        {
            get { return "Registreren"; }
        }
        public ICommand Registreer
        {
            get { return new RelayCommand(SlaCustomerOp); }
        }
        private async void SlaCustomerOp() {

            try
            {
                if (Customer != null)
                {

                    Boolean b = await servicelayer.AddCustomer(Customer);
                    if (b == true)
                    {
                        (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new RegistrerenGedaanVM());
                    }
                    else
                    {
                        ErisEenFoutGebeurd();
                    }
                }
                else
                {
                    ErisEenFoutGebeurd();
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

        private void ErisEenFoutGebeurd()
        {
            FoutmeldingIdentiteit = "Er is een fout gebeurd bij het inlezen van de identititeitskaart probeer opnieuw!";
            Customer = null;
            Image = null;
            Id = "";
        }

        public ICommand Terug
        {
            get { return new RelayCommand(GaTerug); }
        }

        private void GaTerug()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new PortaalVM());
        }

    }
}
