using System.ComponentModel;
namespace domain.enums
{
    public enum ErrorCode
    {
        [Description("未授权")]
        Unauthorized = 403,
        [Description("系统错误")]
        SystemError = 503,
        [Description("请重新登录")]
        ReLogin = 10001,
        [Description("非法token")]
        InvalidToken = 10002,
        [Description("sign 签名非法")]
        ErrorSign = 10003,
        [Description("用户名或密码有误")]
        ErrorUserNameOrPass = 10004,
        [Description("不存在")]
        NotFound = 10005,
        [Description("禁止")]
        Forbidden = 10006,
        [Description("无效密码")]
        InvalidPassword = 10007,
        [Description("账户禁用")]
        AccountDisabled = 10008,
        [Description("非法数据")]
        InvalidData=10009
    }
}