using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class PortaalVM : ObservableObject, IPage
    {
        public ICommand RegistreerCommand
        {
            get { return new RelayCommand(Registreer); }
        }

        private void Registreer()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new RegistrerenVM()) ;
        }
        public ICommand GeldOpladenCommand
        {
            get { return new RelayCommand(GeldOpladen); }
        }

        private void GeldOpladen()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new GeldOpladenScanVM());
        }
        public ICommand GeldOpKaartCommand
        {
            get { return new RelayCommand(GeldOpKaart); }
        }

        private void GeldOpKaart()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new GeldOpKaartVM());
        }

        public string Name
        {
            get { return "Portaal"; }
        }
    }
}
