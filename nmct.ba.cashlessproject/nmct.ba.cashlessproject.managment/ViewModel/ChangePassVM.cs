using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class ChangePassVM : ObservableObject, IPage
    {
        private string _oldPass;

        public string OldPass
        {
            get { return _oldPass; }
            set { _oldPass = value; OnPropertyChanged("OldPass"); }
        }
        private string _newPass;

        public string NewPass
        {
            get { return _newPass; }
            set { _newPass = value; OnPropertyChanged("OldPass"); }
        }
        private string _newPass2;

        public string NewPass2
        {
            get { return _newPass2; }
            set { _newPass2 = value; OnPropertyChanged("OldPass"); }
        }

        public ICommand VeranderCommand
        {
            get { return new RelayCommand(Verander); }
        }

        private void Verander()
        {
            throw new NotImplementedException();
        }
        public ICommand AnnuleerCommand
        {
            get { return new RelayCommand(Annuleer); }
        }

        private void Annuleer()
        {
            throw new NotImplementedException();
        }
        
        public string Name
        {
            get { return "Verander Parswoord"; }
        }
    }
}
