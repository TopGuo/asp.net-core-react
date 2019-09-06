using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class AdminActions
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string Code { get; set; }
        public string Info { get; set; }
        public DateTime CreateTime { get; set; }
        public sbyte? Enable { get; set; }
    }
}
