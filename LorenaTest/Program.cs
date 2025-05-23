using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LorenaTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string dbFile = "LorenaTest.db";
            DB db = new DB($"Data Source={dbFile};Version=3");
            if (!File.Exists(dbFile))
            {
                db.Create();
                DataInitialize.DefaultData(db);
            }
            
            
            int count = db.SelectCountFromSalon();
            
            
            for (int i = 1; i <= count; i++)
            {
                int salonId = i;
                Salon salon = db.SelectSalonById(i);

                Console.Write($"Введите цену для {salon.Name}: ");
                double price = Convert.ToDouble(Console.ReadLine());

                int discount = salon.Discount;
                int parentDiscount = 0;
                if(salon.HasDependency)
                     parentDiscount = salon.GetDiscount((int)salon.ParentId);
                double finalprice = Salon.GetPriceCalculate(price, discount, parentDiscount);                
                db.InsertCalculateTable(salonId, price, parentDiscount, finalprice);
               


            }
        }
    }
}
