using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class AdminRoles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Info { get; set; }
        public sbyte? IsDeleted { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
