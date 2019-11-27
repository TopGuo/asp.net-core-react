using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class ShopsDetail
    {
        public uint Id { get; set; }
        public int ShopId { get; set; }
        public string Pic { get; set; }
        public string Content { get; set; }
        public int IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
