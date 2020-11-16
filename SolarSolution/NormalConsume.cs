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
    class NormalConsume: MainWindow
    {
        // public struct rankElectricWork
        // {
        //     public double Price;
        //     public double quantityAllowed;
        //     public double usedPrice;
        //
        //     public double UsedWork()
        //     {
        //         return usedPrice / Price;
        //     }
        // }
        public SortedList rankElectricWorkList = new SortedList();
        private double consumeMonth=0;
        public NormalConsume(double consumeMonth)
        {
            this.consumeMonth = consumeMonth;

            
        }

        private double Athapdiem;
        private double Atrungbinh;
        private double Acaodiem;

        public NormalConsume(double Athapdiem, double Atrungbinh, double Acaodiem)
        {
            this.Athapdiem = Athapdiem;
            this.Atrungbinh = Atrungbinh;
            this.Acaodiem = Acaodiem;
        }


        public void Loaded()
        {
            if (consumeMonth==0)
            {
                WorkOtherCaulation();
            }
            else
            {
                DevinePriceWork();
            }
            
            
        }

        private void WorkOtherCaulation()
        {

        }

        private void DevinePriceWork()
        {
            double currentConsume = consumeMonth;
            for (double i =1;i<= rankElectricWorkList.Count;i++)
            {
                var E =(rankElectricWork)rankElectricWorkList[i];
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

        
    }
}
