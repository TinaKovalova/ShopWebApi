using System;
using System.Collections.Generic;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Goods = new HashSet<Good>();
        }

        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}
