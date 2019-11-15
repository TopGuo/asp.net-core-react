using System.ComponentModel;

namespace domain.enums
{
    public enum AccountStatus
    {
        [Description("禁用")]
        Disabled = 0,
        /// <summary>
        /// 注册
        /// </summary>
        [Description("正常/注册")]
        Normal = 1,
        /// <summary>
        /// 黑名单
        /// </summary>
        [Description("黑名单")]
        Blacklist = 2,
    }
}