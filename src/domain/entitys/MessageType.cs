using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class MessageType
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Pic { get; set; }
        public int IsDel { get; set; }
        public int Types { get; set; }
        public int? Order { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
