using GalaSoft.MvvmLight.CommandWpf;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;
using nmct.ba.cashlessproject.helper;
using System.Configuration;

namespace nmct.ba.cashlessproject.kassa.medewerker.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public static TokenResponse token = null;
        public static Customer ingelogdeCustomer = null;
        public static Employee ingelogdeMedewerker = null;
        public static Register register = null;
        public static Register_Employee reg_emp = null;

        public ApplicationVM()
        {
            Pages.Add(new LoginVM());
            Pages.Add(new StartupscreenVM());
            Pages.Add(new BestellenVM());
            Pages.Add(new BestellingAfgerondVM());
            getToken();
            GetRegister();      
            CurrentPage = Pages[0];
        }

        public static void Register_EmployerInvullen()
        {
            reg_emp = new Register_Employee();
            reg_emp.Kassa = register;
            reg_emp.Medewerker = ingelogdeMedewerker;
            reg_emp.From = DateTime.Now;
            
        }

        private async void GetRegister()
        {
            register = await Servicelayer.GetRegister(Convert.ToInt32(ConfigurationManager.AppSettings["idRegister"]));
        }

   
        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private List<IPage> pages;
        public List<IPage> Pages
        {
            get
            {
                if (pages == null)
                    pages = new List<IPage>();
                return pages;
            }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        public void ChangePage(IPage page)
        {
            CurrentPage = page;
        }

        public static void getToken()
        {
            OAuth2Client client = new OAuth2Client(new Uri("http://localhost:4730/token"));
            string Password = ConfigurationManager.AppSettings["Password"];
            string Username = ConfigurationManager.AppSettings["Username"];
            token = client.RequestResourceOwnerPasswordAsync(Cryptography.Encrypt(Username), Cryptography.Encrypt(Password)).Result;
        }

        public async void MeldAf()
        {
            try
            {
                if (reg_emp!= null)
                {
                    reg_emp.Until = DateTime.Now;
                    bool b = await Servicelayer.SaveReg_Emp(ApplicationVM.reg_emp);
                    if (b == true)
                    {
                        (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new LoginVM());
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
    }
}
