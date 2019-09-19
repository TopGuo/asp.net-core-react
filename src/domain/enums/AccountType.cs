using System.ComponentModel;

namespace domain.enums
{
    public enum AccountType
    {
        [Description("管理员")]
        Admin = 1,
        [Description("普通会员")]
        StandarUser = 2
    }
}