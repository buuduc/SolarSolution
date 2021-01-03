using System.Collections;
using System.Collections.Generic;

namespace SolarSolution
{
    public class SolarCal : MainWindow
    {
        public double ammountMonney;
        public double Kwp;
        private readonly SortedList rankElectricWorkPrivate = new SortedList();
        private readonly double sellforEVN;
        public  double sunnyTime;
        public double SurplusPrice;
        public double SurplusWork;

        public SolarCal(double Kwp, double sunnyTime, double ammountMonney, double sellforEVN)
        {
            this.Kwp = Kwp;
            this.sunnyTime = sunnyTime;
            this.ammountMonney = ammountMonney;
            this.sellforEVN = sellforEVN;
            Loaded();
        }

        public double Kwh_up_Month => 30 * sunnyTime * Kwp;

        public void Loaded()
        {
        }

        public void savedMoney(NormalConsume normalConsume)
        {
            double surplus;
            var KwM = Kwh_up_Month;
            if (normalConsume.consumeMonth != 0)
                for (double i = normalConsume.rankElectricWorkList.Count; i >= 1; i--)
                {
                    var E = normalConsume.rankElectricWorkList[i];
                    E.UsedWork = E.usedPrice / E.Price;
                    surplus = KwM - E.UsedWork;
                    E.SavedWork = surplus >= 0 ? E.UsedWork : KwM;
                    KwM = surplus;
                    rankElectricWorkPrivate.Add(i, E);


                    
                }
            else
            {
                var keyList = new string[3] { "Cao điểm", "Bình thường", "Thấp điểm" };
                foreach (var key in keyList)
                {
                    var E = normalConsume.rankElectricWorkList[key];
                    surplus = KwM - E.UsedWork;
                    E.SavedWork = surplus >= 0 ? E.UsedWork : KwM;
                    KwM = surplus;
                    rankElectricWorkPrivate.Add(key, E);
                }
            }

            SurplusWork = KwM >= 0 ? KwM : 0;
            SurplusPrice = SurplusWork * sellforEVN;
        }

        public void DoanhThu(int soNam, double phantramtanggia, double suygiamcongsuat1, double suygiamcongsuat)
        {
            double cache = 0;
            double cache1 = 0;
            var sortedList = new SortedList<object, DoanhThuStruct>();
            for (var i = 1; i <= soNam; i++)
            {
                var doanhThuStruct = new DoanhThuStruct();
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
                    cache = doanhThuStruct.SanLuong;
                    doanhThuStruct.DoanhThu = doanhThuStruct.SanLuong * cache1;
                    sortedList.Add(i, doanhThuStruct);
                }
            }

            double TongDoanhThu = 0;
            foreach (var o in sortedList) TongDoanhThu += o.Value.DoanhThu;
        }

        private struct DoanhThuStruct
        {
            public double SanLuong;
            public double DoanhThu;
        }
    }
}