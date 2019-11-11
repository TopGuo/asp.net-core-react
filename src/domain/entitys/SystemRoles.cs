using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class SystemRoles
    {
        public SystemRoles()
        {
            BackstageUser = new HashSet<BackstageUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BackstageUser> BackstageUser { get; set; }
    }
}
