using System;
using System.Collections.Generic;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class SalePo
    {
        public int SalePosId { get; set; }
        public int SaleId { get; set; }
        public int GoodId { get; set; }
        public int CountGood { get; set; }
        public decimal Summa { get; set; }

        public virtual Good Good { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
