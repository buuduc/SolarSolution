using System;
using System.Collections;
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
using OfficeOpenXml;
using System.IO;


namespace SolarSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Hashtable soGioNangHashtable = new Hashtable();
        private NormalConsume normalConsume;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Loaded_Windows(object sender, RoutedEventArgs e)
        {
          
           ReadDataExcel(3);
          
            
        }
        public struct rankElectricWork
        {
            public double Price;
            public double quantityAllowed;
            public double usedPrice;

            public double UsedWork;
            public double SavedWork;
            public double SavedPrice => SavedWork * Price;
            
        }
        private void ReadDataExcel(int index)
        {
            
            SortedList<object, rankElectricWork> rankE = new SortedList<object, rankElectricWork>();
            string path = @"D:\TEMP\DataAppSolar\Data.xlsx";
            using (ExcelPackage MaNS =
                new ExcelPackage(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                ExcelWorksheet workSheet = MaNS.Workbook.Worksheets[0];
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    soGioNangHashtable.Add(workSheet.Cells[i, 2].Value, workSheet.Cells[i, 3].Value);
                }

                
                workSheet = MaNS.Workbook.Worksheets[index];
                switch (index)
                {
                    case 1:
                    {
                        for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                        {
                            rankElectricWork E = new rankElectricWork();
                            E.Price = (double)workSheet.Cells[i, 2].Value;
                            E.quantityAllowed = (double)workSheet.Cells[i, 3].Value;
                            rankE.Add(workSheet.Cells[i, 1].Value, E);
                            // rankElectricWorkPrice.Add(, );
                            // rankElectricWorkCount.Add(workSheet.Cells[i, 1].Value, );
                        }
                        break;

                        }
                    default:
                        for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                        {
                            rankElectricWork E = new rankElectricWork();
                            E.Price = (double)workSheet.Cells[i, 2].Value;
                            rankE.Add(workSheet.Cells[i, 1].Value, E);
                            // rankElectricWorkPrice.Add(, );
                            // rankElectricWorkCount.Add(workSheet.Cells[i, 1].Value, );
                        }
                        break;
                    

                }
                
                
            }

            normalConsume = new NormalConsume(700,3100,1000)
            {
                rankElectricWorkList = rankE
            };
            normalConsume.Loaded();
            SolarCal solarCal = new SolarCal(50, 4.1, 800000000, 1969);
            solarCal.savedMoney(normalConsume);
            solarCal.DoanhThu(25, 3, 3, 0.7);

        }
    }

}
