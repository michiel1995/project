using GalaSoft.MvvmLight.CommandWpf;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public static Customer ingelogdeCustomer = null;
        public ApplicationVM()
        {
            Pages.Add(new PortaalVM());
            Pages.Add(new GeldOpKaartVM());
            Pages.Add(new GeldOpladenScanVM());
            Pages.Add(new GeldOpladenVM());
            Pages.Add(new RegistrerenVM());
            // Add other pages

            CurrentPage = Pages[3];
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
    }
}
