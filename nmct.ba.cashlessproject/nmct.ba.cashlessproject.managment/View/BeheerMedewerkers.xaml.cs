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
    /// Interaction logic for BeheerMedewerkers.xaml
    /// </summary>
    public partial class BeheerMedewerkers : UserControl
    {
        public BeheerMedewerkers()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtkaart.Text = "Hou kaart op scanner";
            txtkaart.Visibility = Visibility.Visible;
            progress.Visibility = Visibility.Visible;

            txtrfid.Focus();
        }
        private void txtrfid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                focus.Focus();
                progress.Visibility = Visibility.Hidden;
                txtkaart.Visibility = Visibility.Hidden;
            }
        }
    }
}
