﻿using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class Scenic
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string LTitle { get; set; }
        public int LookCount { get; set; }
        public string Content { get; set; }
        public string Pic { get; set; }
        public int Order { get; set; }
        public string Mark1 { get; set; }
        public string Mark2 { get; set; }
        public int IsDel { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
