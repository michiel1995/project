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

namespace nmct.ba.cashlessproject.kassa.medewerker.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtrfid.Focus();
        }

        private void txtrfid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                focus.Focus();
                txtrfid.Focus();
            }
        }
    }
}
