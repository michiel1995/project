using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class BeheerKassaVM : ObservableObject, IPage
    {

        public BeheerKassaVM()
        {
            if(ApplicationVM.token != null)
            {
                VulLijstenAan();
            }
        }

        private async void VulLijstenAan()
        {
            VolledigeLijst = await servicelayer.getKassas();
            KeuzeLijst = VolledigeLijst;
        }
        private ObservableCollection<Register_Employee> _volledigeLijst;

        public ObservableCollection<Register_Employee> VolledigeLijst
        {
            get { return _volledigeLijst; }
            set { _volledigeLijst = value; OnPropertyChanged("VolledigeLijst"); }
        }
        private ObservableCollection<Register_Employee> _keuzeLijst;    

        public ObservableCollection<Register_Employee> KeuzeLijst
        {
            get { return _keuzeLijst; }
            set { _keuzeLijst = value; OnPropertyChanged("KeuzeLijst"); }
        }

        private string _selected = "Until";

        public string Selected
        {
            get { return _selected; }
            set { _selected = value; OnPropertyChanged("Selected"); }
        }
        
        
        public string Name
        {
            get { return "BeheerKassa"; }
        }
    }
}
