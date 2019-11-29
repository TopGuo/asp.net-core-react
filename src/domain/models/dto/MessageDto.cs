using System;
using System.Collections.Generic;

namespace domain.models.dto
{
    public class MessageDto : BaseModel
    {
        public int? Id { get; set; }
        public int Types { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Pics { get; set; }
        public List<string> BasePics { get; set; }
        public int Order { get; set; }
        public int LookCount { get; set; }

        public string Pic { get; set; }
        public string NickName { get; set; }
    }

    public class MessageDto2
    {
        public int? Id { get; set; }
        public int Types { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Pics { get; set; }
        public int Order { get; set; }
        public int LookCount { get; set; }

        public string Pic { get; set; }
        public string NickName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}