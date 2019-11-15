using System;
using domain.enums;

namespace domain.models.dto
{
    public class AccountSearchModel:BaseModel
    {
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public AccountStatus? AccountStatus { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Id { get; set; }
        public int PayType { get; set; }
        public int IsDisabled { get; set; }
        public ApproveModel ApproveModel { get; set; }

        public string OpenId { get; set; }

        public string RoleName { get; set; }
        public string AccountStatusName { get; set; }
        public string SourceTypeName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIp { get; set; }
        public string LoginName { get; set; }
        public DateTime? UpdateTime { get; set; }

        public string IdCard { get; set; }

        public AccountType AccountType { get; set; }
    }
}