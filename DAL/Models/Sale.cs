using System;
using System.Collections.Generic;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SalePos = new HashSet<SalePo>();
        }

        public int SaleId { get; set; }
        public int NumberSale { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public DateTime DateSale { get; set; }
        public decimal Summa { get; set; }

        public virtual ICollection<SalePo> SalePos { get; set; }
    }
}
