using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class Banner
    {
        public uint Id { get; set; }
        public string Pic { get; set; }
        public string JumpUrl { get; set; }
        public int Types { get; set; }
        public int IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
