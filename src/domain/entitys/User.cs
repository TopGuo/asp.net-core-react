using System;
using System.Collections.Generic;

namespace domain.entitys
{
    public partial class User
    {
        public uint Id { get; set; }
        public string NickName { get; set; }
        public string PhoneNum { get; set; }
        public string PassWord { get; set; }
        public string Pic { get; set; }
        public string Token { get; set; }
        public string Uid { get; set; }
        public int Status { get; set; }
        public int RefId { get; set; }
        public decimal Amount { get; set; }
        public string OpenId { get; set; }
        public string SessionKey { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
