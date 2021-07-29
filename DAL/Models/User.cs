using System;
using System.Collections.Generic;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string PasswordHash { get; set; }
        public int? RoleId { get; set; }

        public virtual UserRole Role { get; set; }
    }
}
