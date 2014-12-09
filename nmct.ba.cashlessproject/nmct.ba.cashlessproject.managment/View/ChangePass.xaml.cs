using nmct.ba.cashlessproject.managment.ViewModel;
using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nmct.ba.cashlessproject.managment.View
{
    /// <summary>
    /// Interaction logic for ChangePass.xaml
    /// </summary>
    public partial class ChangePass : UserControl
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SavePass();
        }

        private void SavePass()
        {
            ChangePassword pas = new ChangePassword()
            {
                Old = OldPass.Password,
                New = NewPass.Password,
                Replay = ReplayPass.Password
            };
            (this.DataContext as ChangePassVM).Pass = pas;
            (this.DataContext as ChangePassVM).Save();
            NewPass.Password = "";
            ReplayPass.Password = "";
        }
    }
}
