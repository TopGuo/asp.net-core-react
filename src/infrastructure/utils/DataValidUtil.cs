using System;
using System.Text.RegularExpressions;

namespace infrastructure.utils
{
    public static class DataValidUtil
    {
        /// <summary>
        /// 是否为有效的手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool IsMobile(this string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return false;
            }
            return Regex.IsMatch(mobile, "^1[0-9]{10}$");//^(13[0-9]|15[012356789]|18[0-9]|14[579]|17[135678])[0-9]{8}$//^1[0-9]{10}
        }

        static readonly string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        /// <summary>
        /// 是否为有效的身份证号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsIDCard(this string id)
        {
            long n = 0;
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            if (id.Length != 18)
            {
                return false;
            }
            if (long.TryParse(id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            if (address.IndexOf(id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = sum % 11;
            if (arrVarifyCode[y] != id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
    }
}