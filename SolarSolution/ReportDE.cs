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
           set => tenkhachhanglb.Text = Value.ToString();
       }
       public string diachi
       {
           set => diachilb.Text = Value.ToString();
       }
        public string dienkinhdoanh
        {
            set => dienkinhdoanhlb.Text = Value.ToString();
        }
       //  public void InitData()
       // {
       //     // tenkhachhanglb.Text = tenkhachhang;
       //     // diachilb.Text = diachi;
       //     // dienkinhdoanhlb.Text = dienkinhdoanh;
       //     
       // }
       public void setRanktable()
       {
           rankTable.
       }
	}
}
