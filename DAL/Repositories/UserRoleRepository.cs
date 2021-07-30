using Microsoft.EntityFrameworkCore;
using ShopWebApi.DAL.Models;
using ShopWebApi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>
    {
        public UserRoleRepository(DbContext context) : base(context)
        {
        }
    }
}
