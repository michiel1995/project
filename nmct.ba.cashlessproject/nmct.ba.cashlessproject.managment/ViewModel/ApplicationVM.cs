﻿using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public static TokenResponse token = null;
        public ApplicationVM()
        {
            Pages.Add(new BeheerKassaVM());
            Pages.Add(new BeheerKlantenVM());
            Pages.Add(new BeheerMedewerkersVM());
            Pages.Add(new BeheerProductenVM());
            //Pages.Add(new LoginVM());
            Pages.Add(new statistiekenVM());
            CurrentPage = new LoginVM();
            Login = true;
        }

        private Boolean _login;

        public Boolean Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("Login"); }
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
            Login = false;
            CurrentPage = page;
        }
        public ICommand Changepass
        {
            get { return new RelayCommand(Veranderpaswoord); }
        }

        public void Veranderpaswoord()
        {
            (App.Current.MainWindow.DataContext as ApplicationVM).ChangePage(new ChangePassVM());
        }
        public ICommand Afmelden
        {
            get { return new RelayCommand(MeldAf); }
        }

        public void MeldAf()
        {
            Login = true;
            CurrentPage = new LoginVM();
        }
    }
}
