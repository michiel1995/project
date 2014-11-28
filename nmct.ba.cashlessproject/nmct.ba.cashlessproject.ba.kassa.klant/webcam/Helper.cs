using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OnBarcode.Barcode.BarcodeScanner;
using System.IO;
using ZXing;
using System.Drawing;

namespace nmct.ba.cashlessproject.ba.kassa.klant.webcam
{
    class Helper
    {
        //Block Memory Leak
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr handle);
        public static BitmapSource bs;
        public static IntPtr ip;

        public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
        {
            ip = source.GetHbitmap();
            bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, System.Windows.Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bs;
        }
        public static string SaveImageCapture(BitmapSource bitmap)
        {
            //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bitmap));
            //encoder.QualityLevel = 100;


            //// Configure save file dialog box
            //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.FileName = "QR.jpg"; // Default file name
            //dlg.DefaultExt = ".Jpg"; // Default file extension
            //dlg.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            //string filename = dlg.FileName;
            //FileStream fstream = new FileStream(filename, FileMode.Create);
            //encoder.Save(fstream);
            //fstream.Close();

            // create a barcode reader instance
            IBarcodeReader reader = new BarcodeReader();
            // load a bitmap
            //BitmapImage bmi = new BitmapImage();
            // bmi.BeginInit();
            // bmi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "QR.jpg");
            // bmi.EndInit();

            //var barcodeBitmap = bmi;
            // detect and decode the barcode inside the bitmap
            var result = reader.Decode(BitmapImage2Bitmap(bitmap));
            // do something with the result
            if (result != null)
            {
                //MessageBox.Show(result.BarcodeFormat.ToString());
                return result.Text;
            }
            else return null;

            //string[] datas = BarcodeScanner.Scan("QR.jpg", BarcodeType.QRCode);
            //MessageBox.Show(datas[0]);

            // Show save file dialog box
            //Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            /*if (result == true)
            {
                // Save Image
                string filename = dlg.FileName;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                encoder.Save(fstream);
                fstream.Close();
            }*/
        }

        private static Bitmap BitmapImage2Bitmap(BitmapSource bts)
        {

            BitmapSource bitmapSource = bts;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            BitmapImage bImg = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);

            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(memoryStream.ToArray());
            bImg.EndInit();

            memoryStream.Close();

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bImg));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

    }
}
