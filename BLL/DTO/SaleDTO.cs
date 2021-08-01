using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public int NumberSale { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public DateTime DateSale { get; set; }
        public decimal Summa { get; set; }

        public ICollection<SalePosDTO> SalePos { get; set; }
    }
}
