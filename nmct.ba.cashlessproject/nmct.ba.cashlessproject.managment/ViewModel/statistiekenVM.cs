using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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
    class statistiekenVM : ObservableObject, IPage
    {
        public statistiekenVM()
        {
            if (ApplicationVM.token != null)
            {
                vullijstenin();
            }
        }

        private async void vullijstenin()
        {
            Sales = await servicelayer.getSales();
            Kassas = new ObservableCollection<Register>();
            Producten = new ObservableCollection<Product>();
            foreach (Sale s in Sales)
            {
                Kassas.Add(s.Register);
                Producten.Add(s.Product);
            }
            var kassa = Kassas.GroupBy(k => k.Registername).Select(x => new Register() { Registername = x.Key});
            Kassas = new ObservableCollection<Register>(kassa);

            var product = Producten.GroupBy(k => k.ProductName).Select(x => new Product() { ProductName = x.Key });
            Producten = new ObservableCollection<Product>(product);
            lijstenInladen = true;
        }

        private static Boolean lijstenInladen = false;
        private ObservableCollection<Sale> _sales;

        public ObservableCollection<Sale> Sales
        {
            get { return _sales; }
            set { _sales = value; OnPropertyChanged("Sales"); }
        }
        

        private ObservableCollection<Register> _kassas;

        public ObservableCollection<Register> Kassas
        {
            get { return _kassas; }
            set { _kassas = value; OnPropertyChanged("Kassas"); }
        }

        public ICommand MakeStats
        {
            get { return new RelayCommand(maakStats); }
        }
        private Nullable<DateTime> _van;

        public Nullable<DateTime> Van
        {
            get { return _van; }
            set { _van = value; OnPropertyChanged("Van"); }
        }
        private Nullable<DateTime> _tot;

        public Nullable<DateTime> Tot
        {
            get { return _tot; }
            set { _tot = value; OnPropertyChanged("Tot"); }
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }
        private Register _selectedRegister;

        public Register SelectedRegister
        {
            get { return _selectedRegister; }
            set { _selectedRegister = value; OnPropertyChanged("SelectedRegister"); }
        }
        private string _foutmelding;

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; OnPropertyChanged("Foutmelding"); }
        }
        

        private void maakStats()
        {
            if (lijstenInladen == true)
            {
                if (Van != null && Tot != null)
                {
                    Foutmelding = "";
                    List<Sale> resultaat = new List<Sale>();
                    int van = ConvertToTimestamp(Van.Value);
                    int tot = ConvertToTimestamp(Tot.Value);
                    if (SelectedProduct == null && SelectedRegister == null)
                    {
                        resultaat = Sales.Where(x => x.Timestamp > van && x.Timestamp < tot).ToList();
                        ExporteerNaarExcel(resultaat, "statistiek_" + String.Format("{0:M/d/yyyy}", Van) + "_" + String.Format("{0:M/d/yyyy}", Tot));
                    }
                    else if (SelectedProduct != null && SelectedRegister == null)
                    {
                        resultaat = Sales.Where(x => x.Timestamp > van && x.Timestamp < tot && x.Product.Id == SelectedProduct.Id).ToList();
                        ExporteerNaarExcel(resultaat, "statistiek_" + String.Format("{0:M/d/yyyy}", Van) + "_" + String.Format("{0:M/d/yyyy}", Tot) + "_" + SelectedProduct.ProductName);
                    }
                    else if (SelectedProduct == null && SelectedRegister != null)
                    {
                        resultaat = Sales.Where(x => x.Timestamp > van && x.Timestamp < tot && x.Register.Id == SelectedRegister.Id).ToList();
                        ExporteerNaarExcel(resultaat, "statistiek_" + String.Format("{0:M/d/yyyy}", Van) + "_" + String.Format("{0:M/d/yyyy}", Tot) + "_" + SelectedRegister.Registername);
                    }
                    else
                    {
                        resultaat = Sales.Where(x => x.Timestamp > van && x.Timestamp < tot && x.Product.Id == SelectedProduct.Id && x.Register.Id == SelectedRegister.Id).ToList();
                        ExporteerNaarExcel(resultaat, "statistiek_" + String.Format("{0:M/d/yyyy}", Van) + "_" + String.Format("{0:M/d/yyyy}", Tot) + "_" + SelectedRegister.Registername + "_" + SelectedProduct.ProductName);
                    }
                }
                else
                {
                    Foutmelding = "Gelieve de 2 nodige datums in te vullen";
                }
            }
            else
            {
                Foutmelding = "Gelieve even te wachten nog niet alle statistieken zijn geladen ";
            }

        }

        private void ExporteerNaarExcel(List<Sale> resultaat, string docnaam)
        {

            List<Sale> lijst = resultaat.GroupBy(x => x.Product.ProductName).Select(x => new Sale { Product = new Product { ProductName = x.Key }, Price = x.Sum(y => y.Price), Amount = x.Sum(y => y.Amount) }).ToList();
            string filename = docnaam.Replace(@"/", ".");
            SpreadsheetDocument doc = SpreadsheetDocument.Create( filename+ ".xlsx", SpreadsheetDocumentType.Workbook);

            WorkbookPart wbp = doc.AddWorkbookPart();
            wbp.Workbook = new Workbook();

            WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
            SheetData data = new SheetData();
            wsp.Worksheet = new Worksheet(data);


            //WorkbookStylesPart wbsp = wbp.AddNewPart<WorkbookStylesPart>();
            //wbsp.Stylesheet = CreateStylesheet();
            //wbsp.Stylesheet.Save();

            //Worksheet ws = new Worksheet();
            //Columns columns = new Columns();
            //columns.Append(CreateColumnData(1, 1, 50));
            //columns.Append(CreateColumnData(2, 2, 50));
            //ws.Append(columns);

            //wsp.Worksheet = ws;

            Sheets sheets = doc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = doc.WorkbookPart.GetIdOfPart(wsp),
                SheetId = 1,
                Name = "results"
            };
            sheets.Append(sheet);

            Row info1 = new Row() { RowIndex = 1 };
            Cell VanDatum = new Cell() { CellReference = "A1", DataType = CellValues.String, CellValue = new CellValue("Datum van") };
            Cell res1 = new Cell() { CellReference = "B1", DataType = CellValues.String, CellValue = new CellValue(String.Format("{0:M/d/yyyy}", Van)) };
            info1.Append(VanDatum, res1);

            Row info2 = new Row() { RowIndex = 2 };
            Cell TotDatum = new Cell() { CellReference = "A2", DataType = CellValues.String, CellValue = new CellValue("Datum tot") };
            Cell res2 = new Cell() { CellReference = "B2", DataType = CellValues.String, CellValue = new CellValue(String.Format("{0:M/d/yyyy}", Tot)) };
            info2.Append(TotDatum, res2);

            Row info3 = new Row() { RowIndex = 3 };
            Cell Kassa = new Cell() { CellReference = "A3", DataType = CellValues.String, CellValue = new CellValue("Gebruikte kassa") };
            Cell res3 = new Cell() { CellReference = "B3", DataType = CellValues.String, CellValue = new CellValue(SelectedRegister == null? "":SelectedRegister.Registername) };
            info3.Append(Kassa, res3);

            Row info4 = new Row() { RowIndex = 4 };
            Cell Product = new Cell() { CellReference = "A4", DataType = CellValues.String, CellValue = new CellValue("Product") };
            Cell res4 = new Cell() { CellReference = "B4", DataType = CellValues.String, CellValue = new CellValue(SelectedProduct == null ? "" : SelectedProduct.ProductName) };
            info4.Append(Product, res4);


            Row header = new Row() { RowIndex = 6 };
            Cell product = new Cell() { CellReference = "A6", DataType = CellValues.String, CellValue = new CellValue("Product") };
            Cell amount = new Cell() { CellReference = "B6", DataType = CellValues.String, CellValue = new CellValue("Aantal") };
            Cell prijs = new Cell() { CellReference = "C6", DataType = CellValues.String, CellValue = new CellValue("Prijs") };
            header.Append(product, amount,prijs);
            data.Append(info1,info2,info3,info4,header);

            int laatsterij = 0;
            double totaleverkoop = 0, aantal = 0;
            for (int i = 7; i < lijst.Count + 7; i++ )
            {
                Row info = new Row() { RowIndex = Convert.ToUInt32(i) };
                Cell cel1 = new Cell() { CellReference = "A"+i, DataType = CellValues.String, CellValue = new CellValue(lijst[i-7].Product.ProductName) };
                Cell cel2 = new Cell() { CellReference = "B" + i, DataType = CellValues.Number, CellValue = new CellValue("" +lijst[i - 7].Amount) };
                Cell cel3 = new Cell() { CellReference = "C" + i, DataType = CellValues.Number, CellValue = new CellValue(""+ lijst[i - 7].Price) };
                info.Append(cel1, cel2,cel3);
                data.Append(info);
                laatsterij = i +1;
                totaleverkoop += lijst[i - 7].Amount;
                aantal += lijst[i - 7].Price;

            }
            if (laatsterij > 8)
            {
                Row totaal = new Row() { RowIndex = Convert.ToUInt32(laatsterij) };
                Cell woord = new Cell() { CellReference = "A" + laatsterij, DataType = CellValues.String, CellValue = new CellValue("totaal:") };
                Cell totver = new Cell() { CellReference = "B" + laatsterij, DataType = CellValues.Number, CellValue = new CellValue("" + totaleverkoop) };
                Cell totPri = new Cell() { CellReference = "C" + laatsterij, DataType = CellValues.Number, CellValue = new CellValue("" + aantal) };
                totaal.Append(woord, totver, totPri);
                data.Append(totaal);
            }

            wbp.Workbook.Save();
            doc.Close();
        }

        private Stylesheet CreateStylesheet()
        {
            throw new NotImplementedException();
        }

        private static Column CreateColumnData(UInt32 StartColumnIndex, UInt32 EndColumnIndex, double ColumnWidth)
        {
            Column column;
            column = new Column();
            column.Min = StartColumnIndex;
            column.Max = EndColumnIndex;
            column.Width = ColumnWidth;
            column.CustomWidth = true;
            return column;
        }



        private ObservableCollection<Product> _producten;

        public ObservableCollection<Product> Producten
        {
            get { return _producten; }
            set { _producten = value; OnPropertyChanged("Producten"); }
        }

        private int ConvertToTimestamp(DateTime value)
        {
            TimeZoneInfo NYTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            DateTime NyTime = TimeZoneInfo.ConvertTime(value, NYTimeZone);
            TimeZone localZone = TimeZone.CurrentTimeZone;
            System.Globalization.DaylightTime dst = localZone.GetDaylightChanges(NyTime.Year);
            NyTime = NyTime.AddHours(-1);
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (NyTime - epoch);
            return (int)Convert.ToDouble(span.TotalSeconds);
        }
        
        public string Name
        {
            get { return "Statistieken"; }
        }
    }
}
