using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LorenaTest
{
    public static class DataFilling
    {
        public static void DefaultData(DB db)
        {
            List<Salon> SalonList = new List<Salon>
            {
                new Salon("Миасс", 4, false, "Описание Миасс", null),
                new Salon("Амелия", 5, true, "Описание Амелия", null),
                new Salon("Тест1", 2, true, "Описание Тест1", null),
                new Salon("Тест2", 0, true, "Описание Тест2", null),
                new Salon("Курган", 11, false, "Описание Курган", null)
            };

            Dictionary<string, string> salonDictionary = new Dictionary<string, string>
            {
                { "Миасс" , null },
                { "Амелия" , "Миасс" },
                { "Тест1", "Амелия" },
                { "Тест2", "Миасс"  },
                { "Курган", null }

            };

            foreach (var salon in SalonList)
            {
                db.Insert(salon);
            }

            foreach (var kv in salonDictionary)
            {
                db.UpdateParentId(kv.Key, kv.Value);
            }
        }
    }
}
