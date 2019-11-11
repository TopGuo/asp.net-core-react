using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class MessageInfo
    {
        public uint Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int LookCount { get; set; }
        public string Pics { get; set; }
        public int Order { get; set; }
        public int Types { get; set; }
        public int IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
