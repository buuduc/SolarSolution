using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using SolarSolution.Properties;

namespace SolarSolution
{
    class NormalConsume
    {
        struct rankElectricWork
        {
            public double Price;
            public double quantityAllowed;
            public double usedPrice;

            public double UsedWork()
            {
                return usedPrice / Price;
            }
        }
        private SortedList rankElectricWorkList = new SortedList();
        public Hashtable soGioNangHashtable= new Hashtable();
        private double consumeMonth;
        public NormalConsume(double consumeMonth)
        {
            this.consumeMonth = consumeMonth;
            Loaded();
        }

        private void Loaded()
        {
            ReadDataExcel();
            DevinePriceWork();
            savedMoney();
        }

        private void savedMoney()
        {
            for (double i = rankElectricWorkList.Count; i >= 1; i--)
            {
                rankElectricWork E = (rankElectricWork)rankElectricWorkList[i];

            }
        }

        private void DevinePriceWork()
        {
            double currentConsume = consumeMonth;
            for (double i =1;i<= rankElectricWorkList.Count;i++)
            {
                rankElectricWork E =(rankElectricWork)rankElectricWorkList[i];
                double moneyEachRank = E.Price * E.quantityAllowed;

                if (currentConsume < moneyEachRank)
                {
                    E.usedPrice = currentConsume;
                    currentConsume = 0;
                    break;
                }
                else
                {
                    currentConsume -= moneyEachRank;
                    E.usedPrice = moneyEachRank;
                }

                rankElectricWorkList[i] = E;
                



            }

            double index = rankElectricWorkList.Count;
            rankElectricWork El = (rankElectricWork)rankElectricWorkList[index];
            El.usedPrice += currentConsume;
            rankElectricWorkList[index] = El;

        }

        private void ReadDataExcel()
        {
            string path = @"D:\TEMP\DataAppSolar\Data.xlsx";
            using (ExcelPackage MaNS =
                new ExcelPackage(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                ExcelWorksheet workSheet = MaNS.Workbook.Worksheets[0];
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    soGioNangHashtable.Add(workSheet.Cells[i, 2].Value, workSheet.Cells[i, 3].Value);
                }

                workSheet = MaNS.Workbook.Worksheets[1];
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    rankElectricWork E= new rankElectricWork();
                    E.Price = (double) workSheet.Cells[i, 2].Value;
                    E.quantityAllowed = (double) workSheet.Cells[i, 3].Value;
                    rankElectricWorkList.Add(workSheet.Cells[i, 1].Value,E);
                    // rankElectricWorkPrice.Add(, );
                    // rankElectricWorkCount.Add(workSheet.Cells[i, 1].Value, );
                }
            }
        }
    }
}
