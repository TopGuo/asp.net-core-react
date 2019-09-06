using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class AdminUsers
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
