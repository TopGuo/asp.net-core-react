using System;
using domain.enums;

namespace domain.models.dto
{
    public class AccountViewModel
    {
        public string Id { get; set; }
        public decimal Deposit { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string HeadPicture { get; set; }
        public string IdCard { get; set; }
        public string IdCardPhoto { get; set; }
        public string HandheldIdCardPhoto { get; set; }
        public string DriversLicense { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public int? TryTimes { get; set; }
        public DateTime? LastUseCarTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? ApproveTime { get; set; }
        public string LastLoginIp { get; set; }
        public string Remarks { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public string OpenId { get; set; }
    }
}