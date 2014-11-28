using nmct.ba.cashlessproject.ba.kassa.klant.webcam;
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
using System.Windows.Threading;

namespace nmct.ba.cashlessproject.ba.kassa.klant.View
{
    /// <summary>
    /// Interaction logic for GeldOpladen.xaml
    /// </summary>
    public partial class GeldOpladen : UserControl
    {
        public GeldOpladen()
        {
            InitializeComponent();
            InitTimer();
        }
        private DispatcherTimer _timer = new DispatcherTimer();
        WebCam web = new WebCam();
        private void InitTimer()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(800);
            _timer.Tick += _timer_Tick;

        }
        private int geld = 0;
        void _timer_Tick(object sender, EventArgs e)
        {
            int i;
            string s = Helper.SaveImageCapture((BitmapSource)webcam.Source);
            if (s != null && int.TryParse(s,out i))
            {
                _timer.Stop();              
                web.Stop();
                geld += i;
                txtGeld.Text = "Totaal bedrag: €" + geld;
                btnContinue.IsEnabled = true;
            }
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            web.InitializeWebCam(ref webcam);
            web.Start();
            _timer.Start();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            web.Continue();
        }
    }
}
