using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.managment.ViewModel
{
    class BeheerMedewerkersVM : ObservableObject, IPage
    {
        public BeheerMedewerkersVM()
        {
            if (ApplicationVM.token != null)
            {
                VulMedewerkersIn();
            }
        }
        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }
        private string _foutmelding;
        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; OnPropertyChanged("Foutmelding"); }
        }


        private string _zoek;

        public string Zoek
        {
            get { return _zoek; }
            set
            {
                _zoek = value; OnPropertyChanged("Zoek");
                if (Zoek != "" && Zoek != null)
                {

                    var lijst = voledigeLijst.Where(x => x.Name.ToLower().StartsWith(Zoek.ToLower()));
                    Employee = new ObservableCollection<Employee>(lijst);
                }
                else
                {
                    Employee = voledigeLijst;
                }
            }
        }
        


        private async void VulMedewerkersIn()
        {
            Foutmelding = "";
            Employee = await servicelayer.getEmployee();
            voledigeLijst = Employee;
            Zoek ="";
        }

        private static ObservableCollection<Employee> voledigeLijst;

        private ObservableCollection<Employee> _employee;

        public ObservableCollection<Employee> Employee
        {
            get { return _employee; }
            set { _employee = value; OnPropertyChanged("Employee"); }
        }

        private Employee _selectedemployee;

        public Employee SelectedEmployee
        {
            get { return _selectedemployee; }
            set { _selectedemployee = value; OnPropertyChanged("SelectedEmployee"); if (SelectedEmployee != null) { if (SelectedEmployee.Name != null) Status = "Selected"; } }
        }
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); if (Id != "") ControleerId(); }
        }

        public ICommand Aanpassen
        {
            get { return new RelayCommand(PasAan); }
        }

        private void PasAan()
        {
            Status = "PasAan";
        }
        public ICommand Verwijderen
        {
            get { return new RelayCommand(Verwijder); }
        }

        private async void Verwijder()
        {
            if (SelectedEmployee != null )
            {
                bool b = await servicelayer.DeleteEmployee(SelectedEmployee.Id);
                if (b == true)
                {
                    Status = null;
                    VulMedewerkersIn();
                }
                else
                {
                    Foutmelding = "Er is een fout gebeurd probeer het nog eens opnieuw";
                }
            }
            else
            {
                Foutmelding = "Zijn alle waarden correct ingevuld?";
            }

        }
        public ICommand Toevoegen
        {
            get { return new RelayCommand(VoegToe); }
        }

        private void VoegToe()
        {
            Status = "VoegToe";
            SelectedEmployee = new Employee();
        }
        public ICommand Opslaan
        {
            get { return new RelayCommand(SlaOp); }
        }

        private async void SlaOp()
        {

            if (SelectedEmployee.Address != null && SelectedEmployee.Email != null && SelectedEmployee.Name != null && SelectedEmployee.Phone != null && SelectedEmployee.Id != 0)
            {
                Boolean b = false;
                if (Status == "VoegToe")
                {
                    b = await servicelayer.AddEmployee(SelectedEmployee);
                }
                else if (Status == "PasAan")
                {
                    b = await servicelayer.PutEmployee(SelectedEmployee);
                }

                if (b == true)
                {
                    Status = null;
                    VulMedewerkersIn();
                }
                else
                {
                    Foutmelding = "Er is een fout gebeurd probeer het nog eens opnieuw";
                }
            }
            else
            {
                Foutmelding = "Zijn alle waarden correct ingevuld?";
            }

        }
        public ICommand Anuleren
        {
            get { return new RelayCommand(Anuleer); }
        }

        private void Anuleer()
        {
            Status = null;
            VulMedewerkersIn();
        }


        private void ControleerId()
        {
            try
            {
                int i;
                if (!int.TryParse(Id, out i) && Id.Length != 10)
                {
                    Foutmelding = "er is een fout gebeurd probeer opnieuw";
                    Id = "";
                }
                else
                {
                    SelectedEmployee.Id = i;
                    Foutmelding = "Kaart is Ingelezen";
                }
            }
            catch (Exception ex)
            {
              
            }


        }
        public string Name
        {
            get { return "Medewerkers"; }
        }
    }
}
