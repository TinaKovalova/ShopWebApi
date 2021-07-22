using System;
using System.Collections.Generic;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class Category
    {
        public Category()
        {
            Goods = new HashSet<Good>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}
