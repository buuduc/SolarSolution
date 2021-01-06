using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.XtraReports.UI;

namespace SolarSolution
{
    public partial class ReportDE : XtraReport
    {
        public ReportDE()
        {
            InitializeComponent();
        }
        CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
        public NormalConsume NormalConsume { get; set; }
        public SolarCal SolarCal { get; set; }

        public string tenkhachhang
        {
            set => tenkhachhanglb.Text = $"{value}";
        }

        public string diachi
        {
            set => diachilb.Text = $"{value}";
        }

        public string dienkinhdoanh
        {
            set => dienkinhdoanhlb.Text = $"{value}";
        }

        public void InitData()
        {
            tienDientxt.Text = $"{NormalConsume.consumeMonth} VNĐ";
            congSuattxt.Text = $"{SolarCal.Kwp.ToString("0,0", elGR)} Kwp";
            kinhphitxt.Text = $"{SolarCal.ammountMonney.ToString("0,0", elGR)} VNĐ";
            soGionangtxt.Text = $"{SolarCal.sunnyTime} giờ/ngày";
            tuoithotxt.Text = $"{SolarCal.soNam} năm";
            setRanktable();
            TableWattagePerYears();
            TongDoanhThu();
            // tenkhachhanglb.Text = tenkhachhang;
            // diachilb.Text = diachi;
            // dienkinhdoanhlb.Text = dienkinhdoanh;
        }

        public void setRanktable()
        {
            var headTableRow = new XRTableRow();
            rankTable.Rows.Clear();
            switch (NormalConsume.consumeMonth)
            {
                default:
                {
                    var list = new List<string> { "Bậc", "Công suất giới hạn", "Đơn giá" }; 
                    foreach (var VARIABLE in list)
                    {
                        var cell = new XRTableCell(){Text = VARIABLE};
                        headTableRow.Cells.Add(cell);
                    }
                    rankTable.Rows.Add(headTableRow);
                    foreach (var k in SolarCal.rankElectricWorkPrivate)
                    {
                        var row = new XRTableRow();
                        var cell1 = new XRTableCell();
                        cell1.Text = $"{k.Key}";
                        row.Cells.Add(cell1);
                        var cell2 = new XRTableCell();
                        cell2.Text = $"{k.Value.quantityAllowed.ToString("0,0", elGR)}";
                        row.Cells.Add(cell2);
                        var cell3 = new XRTableCell();
                        cell3.Text = $"{k.Value.Price.ToString("0,0", elGR)}";
                        row.Cells.Add(cell3);
                        rankTable.Rows.Add(row);
                    }



                        break;
                }
                case 0:
                {
                    var list = new List<string> { "Thời điểm", "Đơn giá" };
                    foreach (var VARIABLE in list)
                    {
                        var cell = new XRTableCell() { Text = VARIABLE };
                        headTableRow.Cells.Add(cell);
                    }

                    rankTable.Rows.Add(headTableRow);
                    foreach (var k in SolarCal.rankElectricWorkPrivate)
                    {
                        var row = new XRTableRow();
                        var cell1 = new XRTableCell();
                        cell1.Text = $"{k.Key}";
                        row.Cells.Add(cell1);
                        var cell2 = new XRTableCell();
                        cell2.Text = $"{k.Value.Price.ToString("0,0", elGR)}";
                        row.Cells.Add(cell2);
                        
                        rankTable.Rows.Add(row);
                    }
                        break;
                }
            }

            //foreach (var k in SolarCal.rankElectricWorkPrivate)
            //{
            //    var row = new XRTableRow();
            //    var cell1 = new XRTableCell();
            //    cell1.Text = $"{k.Key}";
            //    row.Cells.Add(cell1);
            //    var cell2 = new XRTableCell();

            //    cell2.Text = $"{k.Value.UsedWork}";
            //    row.Cells.Add(cell2);
            //    xrTable1.Rows.Add(row);
            //}
        }

        void TongDoanhThu()
        {
            TongDoanhThutxt.Text =
                $"Như vậy với tuổi thọ của pin năng lượng mặt trời này, thì doanh thu nhận được sau {SolarCal.soNam.ToString("0,0", elGR)} năm xấp xỉ {SolarCal.TongDoanhThu.ToString("0,0", elGR)} VNĐ.";
        }
        public void TableWattagePerYears()
        {
            
            //xrTable1.BeginInit();
            xrTable1.Rows.Clear();
            var headTableRow = new XRTableRow();
            var list = new List<string> { "Năm","Sản lượng","Doanh thu" };
            foreach (var VARIABLE in list)
            {
                var cell = new XRTableCell() { Text = VARIABLE };
                headTableRow.Cells.Add(cell);
            }
            xrTable1.Rows.Add(headTableRow);
            foreach (var k in SolarCal.doanhthuList)
            {
                var row = new XRTableRow();

                var cell1 = new XRTableCell();
                cell1.Text = $"{k.Key}";
                row.Cells.Add(cell1);
                
                var cell2 = new XRTableCell();
                cell2.Text = $"{Math.Round( k.Value.SanLuong,0).ToString("0,0", elGR)}";
                row.Cells.Add(cell2);
                

                var cell3 = new XRTableCell();
                cell3.Text = $"{Math.Round(k.Value.DoanhThu, 0).ToString("0,0", elGR)}";
                row.Cells.Add(cell3);

                xrTable1.Rows.Add(row);
            }
            //W_Per_Years_Table.EndInit();


            //xrTableCell4.Text = "fggg";
            //xrTableCell5.Text = "fggg";
            //xrTable1.EndInit();
        }
    }
}