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
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 100)]
        public double Discount { get; set; }
        public bool HasDependency { get; set; }

        [MaxLength(124)]
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public virtual Salon Parent { get; set; }
        //public virtual ICollection<Salon> Children { get; set; }



        /// <summary>
        /// Рассчитывает итоговую цену с учетом скидки текущего салона и родительского салона (если есть)
        /// </summary>
        /// <param name="price">Исходная цена без скидки</param>
        /// <returns>
        /// Итоговая цена после применения комбинированной скидки.
        /// Если родительский салон существует, применяется сумма скидок текущего и родительского салонов.
        /// </returns>
        public double GetPriceCalculate(double price)
        {
            double parentDiscount = 0;
            if (Parent != null) parentDiscount = Parent.Discount;
            return price - (price * ((Discount + parentDiscount) / 100));
        }


        /// <summary>
        /// Преобразование значения свойства HasDependency в целочисленное представление 
        /// для совместимости SQLite, так как он не поддерживает собственный булевый тип данных
        /// </summary>
        /// <returns>
        /// 1, если HasDependency == true;
        /// 0, еслb HasDependency == false;
        /// </returns>
        public int IsDependency() => Convert.ToInt32(HasDependency);
        

    }
    

}
