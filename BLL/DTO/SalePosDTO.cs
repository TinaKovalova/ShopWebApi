using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class SalePosDTO
    {
        public int SalePosId { get; set; }
        public int SaleId { get; set; }
        public int GoodId { get; set; }
        public int CountGood { get; set; }
        public decimal Summa { get; set; }
        public string GoodName { get; set; }
       

    }
}
