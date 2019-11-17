using System.ComponentModel;

namespace domain.enums
{
    public enum AccountType
    {
        [Description("超级管理员")]
        Admin = 1,
        [Description("普通管理员")]
        JustSoSoAdmin = 2
    }
}