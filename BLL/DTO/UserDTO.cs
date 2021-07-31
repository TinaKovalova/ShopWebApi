using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
    }
}
