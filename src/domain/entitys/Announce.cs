using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class Announce
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Types { get; set; }
    }
}
