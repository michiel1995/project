﻿
using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.helper;
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
        public StartupscreenVM()
        {
            if(ApplicationVM.ingelogdeMedewerker !=null)
            {
                Employee = ApplicationVM.ingelogdeMedewerker;
                ApplicationVM.Register_EmployerInvullen();
            }
        }
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


        private Employee _employee;

        public Employee Employee
        {
            get { return _employee; }
            set { _employee = value; }
        }
        
        private Customer _cust;

        public Customer Cust
        {
            get { return _cust; }
            set { _cust = value; OnPropertyChanged("Cust"); }
        }
        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged("Image"); }
        }
        
        private async void ControleerRfid()
        {
            try
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
                            ApplicationVM.ingelogdeCustomer = Customer;
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

        public ICommand BestellenCommand
        {
            get { return new RelayCommand(Bestellen); }
        }

        private void Bestellen()
        {
            ApplicationVM.ingelogdeCustomer = Cust;
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new BestellenVM());
        }

        public ICommand Afmelden
        {
            get { return new RelayCommand(MeldAf); }
        }

        private async void MeldAf()
        {
            try
            {
                ApplicationVM.reg_emp.Until = DateTime.Now;
                bool b = await Servicelayer.SaveReg_Emp(ApplicationVM.reg_emp);
                if (b == true)
                {
                    (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new LoginVM());
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
