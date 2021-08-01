using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebApi.DAL.Repositories
{
    public class SaleRepository : GenericRepository<Sale>
    {
        public SaleRepository(DbContext context) : base(context)
        {
        }
    }
}
