using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolarSolution
{
    class SolarCal:MainWindow
    {
        private double Kwp;
        private double sunnyTime;
        private double ammountMonney;
        private double sellforEVN;
        public double SurplusWork;
        public double SurplusPrice;
        public SolarCal(double Kwp,double sunnyTime, double ammountMonney, double sellforEVN)
        {
            this.Kwp = Kwp;
            this.sunnyTime = sunnyTime;
            this.ammountMonney =  ammountMonney;
            this.sellforEVN = sellforEVN;
            Loaded();
        }
        SortedList rankElectricWorkPrivate = new SortedList();
        public void Loaded()
        {
           
        }
        public double Kwh_up_Month
        {
            get {return 30 * sunnyTime * Kwp; }

        }
        public void savedMoney(NormalConsume normalConsume)
        {
            double surplus;
            double KwM = Kwh_up_Month;
            for (double i = normalConsume.rankElectricWorkList.Count; i >= 1; i--)
            {
                rankElectricWork E = (rankElectricWork)normalConsume.rankElectricWorkList[i];
                surplus = KwM- E.UsedWork;
                E.SavedWork = surplus >= 0 ? E.UsedWork : KwM;
                KwM = surplus;
                rankElectricWorkPrivate.Add(i,E);
            }

            SurplusWork = KwM >= 0 ? KwM : 0;
            SurplusPrice = SurplusWork * sellforEVN;
        }

        struct DoanhThuStruct
        {
            public double SanLuong;
            public double DoanhThu;
        }
        public void DoanhThu(int soNam, double phantramtanggia, double suygiamcongsuat1, double suygiamcongsuat)
        {
            double cache=0;
            double cache1 = 0;
            SortedList<object,DoanhThuStruct> sortedList= new SortedList<object,DoanhThuStruct>();
            for (int i = 1; i <= soNam; i++)
            {
                DoanhThuStruct doanhThuStruct = new DoanhThuStruct();
                if (i == 1)
                {
                    doanhThuStruct.SanLuong = Kwh_up_Month * 12 * (1 - suygiamcongsuat1 / 100);
                    cache = doanhThuStruct.SanLuong;
                    doanhThuStruct.DoanhThu = doanhThuStruct.SanLuong * sellforEVN;
                    cache1 = sellforEVN;
                    sortedList.Add(i, doanhThuStruct);
                }
                else
                {
                    cache1 = cache1 * (1 + phantramtanggia / 100);
                    doanhThuStruct.SanLuong = cache * (1 - suygiamcongsuat / 100);
                    cache= doanhThuStruct.SanLuong;
                    doanhThuStruct.DoanhThu = doanhThuStruct.SanLuong*cache1;
                    sortedList.Add(i, doanhThuStruct);
                }

            }
            double TongDoanhThu = 0;
                foreach (KeyValuePair<object, DoanhThuStruct> o in sortedList)
                {
                    TongDoanhThu+=o.Value.DoanhThu;
                }
            
        }
    }
}
