using System;
using System.ComponentModel;
using domain.enums;

namespace domain.models.dto
{
    public class BackstageUserAdd
    {
        public string Id { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public int Gender { get; set; }
        public string VerCode { get; set; }
        public int ErrCount { get; set; }
        [Description("旧密码")]
        public string OldPassword { get; set; }
        public string OpenId { get; set; }
        public AccountType AccountType { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
    }
}
