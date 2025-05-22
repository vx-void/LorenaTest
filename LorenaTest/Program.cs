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
            DataFilling.DefaultData(db); //Заполнение данными по умолчанию
          
        }
    }
}
