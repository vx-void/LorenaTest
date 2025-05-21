using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LorenaTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DB db = new DB("Data Source=LorenaTest.db;Version=3");
            db.Create();

            List<Salon> SalonList = new List<Salon>
            {
                new Salon("Миасс", 4, false, "Описание Миасс", null),
                new Salon("Амелия", 5, true, "Описание Амелия", null),
                new Salon("Тест1", 2, true, "Описание Тест1", null),
                new Salon("Тест2", 0, true, "Описание Тест2", null),
                new Salon("Курган", 11, false, "Описание Курган", null)
            };

            foreach (var salon in SalonList)
            {
                db.Insert(salon);
            }


        }
    }
}
