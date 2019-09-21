using System;
using System.Linq;
using System.Text;

namespace infrastructure.utils
{
    public class SecurityUtil
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

        public static string getMD5(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string getHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
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