using domain.enums;

namespace domain.models
{
    public class TokenModel
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        /// <summary>
        /// 请求来源
        /// </summary>
        /// <value></value>
        public SourceType Source { get; set; }
        public string Code { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        /// <value></value>
        public AccountType Type { get; set; }
    }
}