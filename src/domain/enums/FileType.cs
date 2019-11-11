using System.ComponentModel;

namespace domain.enums
{
    /// <summary>
    /// 上传文件类别枚举
    /// </summary>
    public enum FileType
    {
        [Description("头像")]
        Head = 1,
        [Description("身份证正面照片")]
        IdCardFace = 2,
        [Description("身份证反面照片")]
        IdCardBack = 3,
        [Description("意见反馈")]
        Feedbacks = 4,
        [Description("店铺")]
        Shop = 5,
        [Description("消息")]
        Message = 6,
        [Description("其他")]
        Other = 7,
    }
}