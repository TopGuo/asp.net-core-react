using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class BackstageUser
    {
        public string Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string LastLoginIp { get; set; }
        public string Email { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public int? Gender { get; set; }
        public string IdCard { get; set; }
        public int AccountType { get; set; }
        public int AccountStatus { get; set; }
        public int SourceType { get; set; }

        public virtual SystemRoles Role { get; set; }
    }
}
