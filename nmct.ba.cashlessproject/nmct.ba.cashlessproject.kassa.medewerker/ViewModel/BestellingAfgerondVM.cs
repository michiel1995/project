using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.kassa.medewerker.ViewModel
{
    class BestellingAfgerondVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Bestelling Afgerond"; }
        }

        public ICommand Volgende
        {
            get { return new RelayCommand(VolgendeKlant); }
        }

        private void VolgendeKlant()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new StartupscreenVM());
        }

    }
}
