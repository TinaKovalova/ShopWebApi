using System;
using System.Collections.Generic;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public int? GoodId { get; set; }
        public string PhotoPath { get; set; }
    }
}
