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
using System.Text.RegularExpressions;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;


namespace SolarSolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Hashtable soGioNangHashtable = new Hashtable(); // đọc data số giờ nắng từ file database
        private NormalConsume normalConsume;// đối tượng thể hiện số tiền chi trả 
        private SolarCal solarCal;
        private SortedList<object, rankElectricWork> rankE;
        private ReportDE reportDe = new ReportDE();
        public string DienKinhDoanh;
        public MainWindow() 
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Loaded_Windows(object sender, RoutedEventArgs e)
        {
          
           
           khuvucComboBox.ItemsSource = soGioNangHashtable.Keys;

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
            rankE = null;
            soGioNangHashtable.Clear();
           rankE = new SortedList<object, rankElectricWork>();

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
                                normalConsume = new NormalConsume(5000000)
                                {
                                    rankElectricWorkList = rankE
                                };
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
                            normalConsume = new NormalConsume(2000,2100,2200)
                            {
                                rankElectricWorkList = rankE
                            };
                        }
                        break;
                    

                }
                // normalConsume = new NormalConsume(700, 3100, 1000)
                // normalConsume = new NormalConsume(5000000)
                // {
                //     rankElectricWorkList = rankE
                // };
                


            }

            

        }

        private void ExistBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            DienKinhDoanh = radioButton.Content.ToString();
            switch (radioButton.Content)
            {
                case "- Hộ gia đình":
                {
                    ReadDataExcel(1);
                    RankTable.RowGroups.Clear();

                        TableRow row = new TableRow();
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Bậc"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Công suất giới hạn"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Đơn giá"))));
                    RankTable.RowGroups.Add(new TableRowGroup());
                    RankTable.RowGroups[0].Rows.Add(row);
                    foreach (var itemElectricWork in rankE)
                    {
                        row = new TableRow();
                        row.Cells.Add(new TableCell(new Paragraph(new Run(itemElectricWork.Key.ToString()))));
                        row.Cells.Add(new TableCell(new Paragraph(new Run(itemElectricWork.Value.quantityAllowed.ToString()))));
                        row.Cells.Add(new TableCell(new Paragraph(new Run(itemElectricWork.Value.Price.ToString()))));
                        RankTable.RowGroups.Add(new TableRowGroup());
                        RankTable.RowGroups[RankTable.RowGroups.Count-1].Rows.Add(row);

                            

                        }
                        break;
                }
                case "- Diện kinh doanh":
                {
                    ReadDataExcel(2);
                    RankTable.RowGroups.Clear();

                    TableRow row = new TableRow();
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Thời điểm"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Đơn giá"))));
                    RankTable.RowGroups.Add(new TableRowGroup());
                    RankTable.RowGroups[0].Rows.Add(row);
                    foreach (var itemElectricWork in rankE)
                    {
                        row = new TableRow();
                        row.Cells.Add(new TableCell(new Paragraph(new Run(itemElectricWork.Key.ToString()))));
                        row.Cells.Add(new TableCell(new Paragraph(new Run(itemElectricWork.Value.Price.ToString()))));
                        RankTable.RowGroups.Add(new TableRowGroup());
                        RankTable.RowGroups[RankTable.RowGroups.Count - 1].Rows.Add(row);



                    }
                        break;
                }
                case "- Diện sản xuất":
                {
                    ReadDataExcel(3);
                    RankTable.RowGroups.Clear();

                    TableRow row = new TableRow();
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Thời điểm"))));
                    row.Cells.Add(new TableCell(new Paragraph(new Run("Đơn giá"))));
                    RankTable.RowGroups.Add(new TableRowGroup());
                    RankTable.RowGroups[0].Rows.Add(row);
                    foreach (var itemElectricWork in rankE)
                    {
                        row = new TableRow();
                        row.Cells.Add(new TableCell(new Paragraph(new Run(itemElectricWork.Key.ToString()))));
                        row.Cells.Add(new TableCell(new Paragraph(new Run(itemElectricWork.Value.Price.ToString()))));
                        RankTable.RowGroups.Add(new TableRowGroup());
                        RankTable.RowGroups[RankTable.RowGroups.Count - 1].Rows.Add(row);



                    }
                        break;
                }
            }
            
        }


        private void PreviousBtn_clicked(object sender, RoutedEventArgs e)
        {
            TabControlGeneral.SelectedIndex -= 1;
        }

        private void NextBtn_clicked(object sender, RoutedEventArgs e)
        {
            TabControlGeneral.SelectedIndex += 1;
        }

        private void TabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int maxtab = TabControlGeneral.Items.Count;
            int curtab = TabControlGeneral.SelectedIndex;
            if (curtab == 0)
            {
                PreviousBtn.IsEnabled=false;
            }
            else if(curtab==maxtab-1)
            {
                NextBtn.IsEnabled = false;
            }
            else
            {
                PreviousBtn.IsEnabled = true;
                NextBtn.IsEnabled = true;
            }
        }

        private void khuvuctxtbox_Changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            SogioNangTxt.Text=soGioNangHashtable[comboBox.SelectedItem].ToString();
           
        }

        public string testthu = "ggxx";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrtFrm a = new PrtFrm();
            a.Show();
            
        }

        private void XemBaoCaoBtn_Click(object sender, RoutedEventArgs e)
        {
            reportDe.tenkhachhang = TenKhachHangtxt.Text;
            reportDe.diachi = DiaChiTxt.Text;
            reportDe.dienkinhdoanh = DienKinhDoanh;
            reportDe.NormalConsume = normalConsume;
            reportDe.SolarCal = solarCal;
            reportDe.CreateDocument();
            DocumentPreviewControl.DocumentSource = reportDe;
            normalConsume.Loaded();

            solarCal = new SolarCal(50, 4.1, 800000000, 1969);
            solarCal.savedMoney(normalConsume);
            solarCal.DoanhThu(25, 3, 3, 0.7);
        }
    }

}
