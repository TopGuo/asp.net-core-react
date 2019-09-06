using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class AdminRoleAction
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ActionId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
