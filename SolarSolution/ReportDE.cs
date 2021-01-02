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
            InitData();
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
            //tienDientxt.Text = $"{NormalConsume.consumeMonth}";
            //congSuattxt.Text = $"{SolarCal.Kwp}";
            //kinhphitxt.Text = $"{SolarCal.ammountMonney}";
            //soGionangtxt.Text= $"{SolarCal.sunnyTime}";

            // tenkhachhanglb.Text = tenkhachhang;
            // diachilb.Text = diachi;
            // dienkinhdoanhlb.Text = dienkinhdoanh;

        }
       public void setRanktable()
       {
           // rankTable.
       }
	}
}
