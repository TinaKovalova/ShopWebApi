using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>
    {
        public PhotoRepository(DbContext context) : base(context)
        {
        }
    }
}
