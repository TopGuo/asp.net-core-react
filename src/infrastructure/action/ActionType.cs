using System.ComponentModel;

namespace infrastructure.action
{
    /// <summary>
    /// 父菜单类型-自定义系统模块
    /// </summary>
    public enum ActionType
    {
        [Description("系统管理")]
        SystemManager = 1,
        [Description("用户管理")]
        UsersManager,
        [Description("财务管理")]
        CaiwuManager,
        [Description("市场设置")]
        ShiChangManager,

    }
}