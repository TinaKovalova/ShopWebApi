using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class Good
    {
        public Good()
        {
            SalePos = new HashSet<SalePo>();
        }
        [Key]
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public int? ManufacturerId { get; set; }
        public int? CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal GoodCount { get; set; }

        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<SalePo> SalePos { get; set; }
    }
}
