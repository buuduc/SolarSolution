using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SolarSolution
{
	public partial class ReportDE : DevExpress.XtraReports.UI.XtraReport
	{
       public NormalConsume NormalConsume { get; set; }
       public SolarCal SolarCal { get; set; }

       public ReportDE()
		{
			InitializeComponent();
            
		}

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
            tienDientxt.Text = $"{NormalConsume.consumeMonth}";
            congSuattxt.Text = $"{SolarCal.Kwp}";
            kinhphitxt.Text = $"{SolarCal.ammountMonney}";
            soGionangtxt.Text = $"{SolarCal.sunnyTime}";
            TableWattagePerYears();
            // tenkhachhanglb.Text = tenkhachhang;
            // diachilb.Text = diachi;
            // dienkinhdoanhlb.Text = dienkinhdoanh;

        }
       public void setRanktable()
       {
           // rankTable.
       }

       public void TableWattagePerYears()
       {
            //xrTable1.BeginInit();
            xrTable1.Rows.Clear();
            foreach (var k in SolarCal.rankElectricWorkPrivate)
            {
                XRTableRow row = new XRTableRow();
                XRTableCell cell1 = new XRTableCell();
                cell1.Text = $"{k.Key}";
                row.Cells.Add(cell1);
                XRTableCell cell2 = new XRTableCell();
                
                cell2.Text = $"{k.Value.}";
                row.Cells.Add(cell2);
                xrTable1.Rows.Add(row);

            }
            //W_Per_Years_Table.EndInit();



            //xrTableCell4.Text = "fggg";
            //xrTableCell5.Text = "fggg";
            //xrTable1.EndInit();
            

        }
	}
}
