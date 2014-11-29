using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class GeldOpgeladenGedaanVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Geld Opgeladen"; }
        }

        public ICommand Terug
        {
            get { return new RelayCommand(Teruggaan); }
        }

        private void Teruggaan()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new PortaalVM());
        }
    }
}
