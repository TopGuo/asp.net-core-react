using System.ComponentModel;

namespace domain.enums
{
    public enum SourceType
    {
        [Description("未知")]
        Unknown = 0,

        [Description("网站")]
        Web = 1,

        [Description("微信")]
        WeChat = 2,

        [Description("Android")]
        Android = 3,

        [Description("iOS")]
        IOS = 4,

        [Description("小程序")]
        WeChatApp = 5
    }
}