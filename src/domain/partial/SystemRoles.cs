using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain.entitys
{
    public partial class SystemRoles
    {
        [NotMapped]
        public virtual List<string> Menus { get; set; }
    }
}