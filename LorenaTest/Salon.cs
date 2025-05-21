using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public double Discount { get; private set; }
        public bool HasDependency { get; private set; }

        [MaxLength(124)]
        public string Description { get; private set; }

        public int? ParentId { get; private set; }

        public Salon(string name, double discount, bool hasDependency, string description, int? parentId)
        {
            Name = name;
            Discount = discount;
            HasDependency = hasDependency;
            Description = description;
            ParentId = parentId;
        }



        /// <summary>
        /// Рассчитывает итоговую цену с учетом скидки текущего салона и родительского салона (если есть)
        /// </summary>
        /// <param name="price">Исходная цена без скидки</param>
        /// <returns>
        /// Итоговая цена после применения комбинированной скидки.
        /// Если родительский салон существует, применяется сумма скидок текущего и родительского салонов.
        /// </returns>
        public double GetPriceCalculate(double price, double? parentDiscount )
        {
            if (parentDiscount == null) 
                parentDiscount = 0;
            return price - (price * ((Discount + (double)parentDiscount) / 100));
        }

    }
    

}
