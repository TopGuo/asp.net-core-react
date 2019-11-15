namespace domain.configs
{
    public class Constants
    {
        #region 默认头像
        /// <summary>
        /// 默认头像图片相对服务器路径
        /// </summary>
        public const string DefaultHeadPicture = "/images/head.png";
        #endregion

        /// <summary>
        /// sign token key
        /// </summary>
        public static string Key = "xingchenwuxian";

        /// <summary>
        /// 公司明
        /// </summary>
        public static string Company = "河北星辰无限科技有限公司";
        /// <summary>
        /// 网站授权协议
        /// </summary>
        public const string WEBSITE_AUTHENTICATION_SCHEME = "Web";
        /// <summary>
        /// 上次登录路径
        /// </summary>
        public const string LAST_LOGIN_PATH = "LAST_LOGIN_PATH";

        public const string ShowAllDataCookie = "ShowAllData";
        /// <summary>
        /// 验证码图片
        /// </summary>
        public const string WEBSITE_VERIFICATION_CODE = "ValidateCode";
        
        /// <summary>
        /// 
        /// </summary>
        public const string UPLOAD_TEMP_PATH = "Upload_Temp";
    }
}