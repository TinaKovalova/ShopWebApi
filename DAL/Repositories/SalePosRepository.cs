using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWebApi.DAL.Repositories
{
    public class SalePosRepository : GenericRepository<SalePo>
    {
        public SalePosRepository(DbContext context) : base(context)
        {
        }
    }
}
