using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class PhotoDTO
    {
        public int PhotoId { get; set; }
        public int? GoodId { get; set; }
        public string PhotoPath { get; set; }
    }
}
