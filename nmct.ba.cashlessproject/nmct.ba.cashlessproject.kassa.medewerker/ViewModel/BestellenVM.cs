using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.kassa.medewerker.ViewModel
{
    class BestellenVM : ObservableObject, IPage
    {
        private string _demo = "Demo";
        public string Demo
        {
            get { return _demo; }
            set { _demo = value; OnPropertyChanged("Demo"); }
        }


        public string Name
        {
            get { return "Bestellen"; }
        }

        public ICommand SorterenCommand
        {
            get { return new RelayCommand<string>(Sorteren); }
        }

        private void Sorteren(string letter)
        {
            
        }

        
    }
}
