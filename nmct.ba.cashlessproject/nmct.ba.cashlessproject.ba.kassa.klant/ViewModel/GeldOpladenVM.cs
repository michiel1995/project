using nmct.ba.cashlessproject.ba.kassa.klant.webcam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WebCam_Capture;

namespace nmct.ba.cashlessproject.ba.kassa.klant.ViewModel
{
    class GeldOpladenVM : ObservableObject, IPage
    {
    
        public string Name
        {
            get { return "GeldOpladen"; }
        }
        private BitmapImage _image;

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged("Image"); }
        }

    }
}
