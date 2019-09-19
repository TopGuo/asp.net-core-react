using System.ComponentModel;
namespace domain.enums
{
    public enum ErrorCode
    {
        [Description("未授权")]
        Unauthorized = 403
    }
}