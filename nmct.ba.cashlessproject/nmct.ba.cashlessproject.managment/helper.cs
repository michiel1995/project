using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace nmct.ba.cashlessproject.managment
{
    public class helper
    {
        public static BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn.Length != 0)
            {
                //maakt image van byte array
                MemoryStream ms = new MemoryStream(byteArrayIn);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

                //maakt bitmap van image
                MemoryStream ms2 = new MemoryStream();
                returnImage.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
                ms2.Position = 0;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms2;
                bi.EndInit();
                return bi;
            }
            return new BitmapImage();
        }
    }
}
