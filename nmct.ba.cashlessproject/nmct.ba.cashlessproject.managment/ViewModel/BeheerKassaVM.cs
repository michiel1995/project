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
            if(VolledigeLijst != null)
                VulKeuzeLijstIn();
        }

        private string _selectedList;

        public string SelectedList
        {
            get { return _selectedList; }
            set { _selectedList = value; OnPropertyChanged("SelectedList"); VeranderInfoLijst(); }
        }

        private void VeranderInfoLijst()
        {
            if(SelectedList != null)
            {
                Info = new ObservableCollection<Register_Employee>();
                if (Selected == "System.Windows.Controls.ComboBoxItem: Medewerker")
                {
                    foreach(Register_Employee reg in VolledigeLijst)
                    {
                        if(reg.Medewerker.Name== SelectedList)
                        {
                            Info.Add(reg);
                        }
                    }
                }
                else
                {
                    foreach (Register_Employee reg in VolledigeLijst)
                    {
                        if (reg.Kassa.Registername == SelectedList)
                        {
                            Register_Employee emp = reg;
                            emp.Kassa.Registername = reg.Medewerker.Name;
                            Info.Add(emp);
                        }
                    }
                }
            }
        }
        
        private void VulKeuzeLijstIn()
        {
            KeuzeLijst = new ObservableCollection<string>();
            if (Selected == "System.Windows.Controls.ComboBoxItem: Medewerker")
            {
                foreach(Register_Employee reg  in VolledigeLijst)
                {
                    KeuzeLijst.Add(reg.Medewerker.Name);
                }
            }
            else
            {
                foreach (Register_Employee reg in VolledigeLijst)
                {
                    
                    KeuzeLijst.Add(reg.Kassa.Registername);
                }
            }
        }
        private ObservableCollection<Register_Employee> _volledigeLijst;

        public ObservableCollection<Register_Employee> VolledigeLijst
        {
            get { return _volledigeLijst; }
            set { _volledigeLijst = value; OnPropertyChanged("VolledigeLijst"); }
        }
        private ObservableCollection<string> _keuzeLijst ;    

        public ObservableCollection<string> KeuzeLijst
        {
            get { return _keuzeLijst; }
            set { _keuzeLijst = value; OnPropertyChanged("KeuzeLijst"); }
        }

        private ObservableCollection<Register_Employee> _info;

        public ObservableCollection<Register_Employee> Info
        {
            get { return _info; }
            set { _info = value; OnPropertyChanged("Info"); }
        }
        

        private string _selected = "Medewerker";

        public string Selected
        {
            get { return _selected; }
            set { _selected = value; OnPropertyChanged("Selected"); if (VolledigeLijst != null) VulKeuzeLijstIn(); }
        }
        
        
        public string Name
        {
            get { return "Kassa"; }
        }
    }
}
