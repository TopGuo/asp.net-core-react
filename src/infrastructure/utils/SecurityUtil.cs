using System;
using System.Linq;
using System.Text;

namespace infrastructure.utils
{
    public static class SecurityUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "Error";
            }
            var md5 = System.Security.Cryptography.MD5.Create();
            string a = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str)));
            a = a.Replace("-", "");
            return a;
        }
        /// <summary>
        /// 生成签名
        /// 需要签名的数组,正序排序后(不区分大小写),md5加密,截取从第5位开始后的24位
        /// </summary>
        /// <param name="strs"></param>
        /// <returns>签名后的字符串</returns>
        public static string Sign(params string[] strs)
        {
            if (strs == null || strs.Length == 0)
            {
                throw new Exception("加密对象不能为空!");
            }
            var list = strs.Select(t => t.ToUpper()).OrderBy(t => t).Where(t => !string.IsNullOrEmpty(t)).ToArray();
            if (list.Length == 0)
            {
                throw new Exception("加密对象不能为空!");
            }
            var value = MD5(string.Join("", list)).ToCharArray(5, 24);
            return new string(value);
        }
        /// <summary>
        /// 验证签名正确性
        /// </summary>
        /// <param name="sign">需要验证的签名</param>
        /// <param name="strs">需要验证的数组</param>
        /// <returns></returns>
        public static bool ValidSign(string sign, params string[] strs)
        {
            var _sign = Sign(strs);
            return _sign.Equals(sign, StringComparison.OrdinalIgnoreCase);
        }

    }
}