using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LorenaTest
{
    public class Salon
    {
        [StringLength(100)]
        public string Name { get; private set; }

        [Range(0, 100)]
        public int Discount { get; private set; }
        public bool HasDependency { get; private set; }

        [MaxLength(124)]
        public string Description { get; private set; }

        public int? ParentId { get; private set; }

        private DB _db { get; set; }

        public Salon(DB db, string name, int discount, bool hasDependency, string description, int? parentId)
        {
            _db = db;
            Name = name;
            Discount = discount;
            HasDependency = hasDependency;
            Description = description;
            ParentId = parentId;
        }

        public static double GetPriceCalculate(double price, int discount, int? parentDiscount)
        {
            if (parentDiscount == null)
                parentDiscount = 0;
            return price - (price * ((discount + (double)parentDiscount) / 100));
        }


        public int GetDiscount(int id)
        {
            return _db.SelectSalonById(id).Discount;
        }
    }
}
   

