using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class LoginVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Login"; }
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand<object>(Login); }
        }

        private void Login(object par)
        {
            var passwordBox = par as PasswordBox;
            Password = passwordBox.Password;        
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = GetToken();

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new BeheerProductenVM());
            }
            else
            {
                Error = "Gebruikersnaam of paswoord kloppen niet";
            }
        }

        private TokenResponse GetToken()
        {
            OAuth2Client client = new OAuth2Client(new Uri("http://localhost:4730/token"));
            return client.RequestResourceOwnerPasswordAsync(Cryptography.Encrypt(Username), Cryptography.Encrypt(Password)).Result;
        }
    }
}
