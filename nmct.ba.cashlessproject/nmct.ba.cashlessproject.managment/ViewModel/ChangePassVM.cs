using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
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
        private string _foutmelding;

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; OnPropertyChanged("Foutmelding"); }
        }
        private string _goedgekeurd;

        public string GoedGekeurd
        {
            get { return _goedgekeurd; }
            set { _goedgekeurd = value; OnPropertyChanged("GoedGekeurd"); }
        }
        

        private ChangePassword _pass;
        public ChangePassword Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        
        public ICommand VeranderCommand
        {
            get { return new RelayCommand(Verander); }
        }

        private void Verander()
        {
            
        }

        
        public string Name
        {
            get { return "Verander Parswoord"; }
        }
        public async void Save()
        {
            if(Pass.New != null || Pass.Old != null || Pass.Replay !=null)
            {
                if(Pass.New == Pass.Replay)
                {
                    Pass.New = Cryptography.Encrypt(Pass.New);
                    Pass.Old = Cryptography.Encrypt(Pass.Old);
                    Pass.Replay = Cryptography.Encrypt(Pass.Replay);
                    bool b = await servicelayer.ChangePass(Pass);
                    if(b == true)
                    {
                        Foutmelding = "";
                        GoedGekeurd = "Paswoord is succesvol opgeslagen";
                    }
                    else{
                        Foutmelding = "U huidige paswoord klopt niet";
                    }
                }
                else
                {
                    Foutmelding = "De 2 nieuwe paswoorden moeten gelijk zijn";
                }
            }
            else{
                Foutmelding ="Alle waarden moeten ingevuld worden";
            }
        }
    }
}
