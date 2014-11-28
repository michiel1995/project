using be.belgium.eid;
using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.ba.kassa.klant.idreader;
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

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class RegistrerenVM : ObservableObject, IPage
    {
        private string  _naam;
        public string Naam
        {
            get { return _naam; }
            set { _naam = value; OnPropertyChanged("Naam"); }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged("Address"); }
        }
        private Byte[] _bytes;

        public Byte[] Bytes
        {
            get { return _bytes; }
            set { _bytes = value; OnPropertyChanged("Bytes"); }
        }

        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); ControleerId(); }
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
            int i;
            if (!int.TryParse(Id, out i) && Id.Length != 10)
            {
                //IdFoutmelding = 1;
                Foutmelding = "er is een fout gebeurd probeer opnieuw";
                Id = "";
            }
            else{
                //IdFoutmelding = 2;
                Foutmelding = "Kaart is Ingelezen";
            }
            
        }
        private void LeesKaart()
        {
            ReadId info = ReadId.Lees();
            if (info == null)
            {
                FoutmeldingIdentiteit = "Er is een fout gebeurd bij het inlezen van de identititeitskaart probeer opnieuw!";
            }
            else
            {
                FoutmeldingIdentiteit ="";
                Naam = info.Name;
                Address = info.Adres;
                Bytes = info.Bytes;
                Image = helper.byteArrayToImage(info.Bytes);
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
            if (Naam != null && Address != null && Bytes != null && Id != null)
            {
                Customer cust = new Customer()
                {
                    Address = Address,
                    Balance = 0,
                    Id = Convert.ToInt32(Id),
                    Image = Bytes,
                    Name = Naam                 
                };
                Boolean b = await servicelayer.AddCustomer(cust);
                if(b==true)
                {
                   throw new Exception();
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

        private void ErisEenFoutGebeurd()
        {
            FoutmeldingIdentiteit = "Er is een fout gebeurd bij het inlezen van de identititeitskaart probeer opnieuw!";
            Naam = "";
            Address = "";
            Image = null;
            Id = "";
        }

    }
}
