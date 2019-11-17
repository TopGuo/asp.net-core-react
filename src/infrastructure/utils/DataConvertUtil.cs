using System;

namespace infrastructure.utils
{
    public static class DataConvertUtil
    {
        private static readonly DateTime Year1970 = new DateTime(1970, 1, 1);
        /// <summary>
        /// 获取1970年以来的数值(时间戳)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long GetTicket(this DateTime time)
        {
            return (long)(time.ToUniversalTime() - Year1970).TotalMilliseconds;
        }
        static readonly long Long1970 = 621355968000000000;
        /// <summary>
        /// 将数值(时间戳)转换为时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetTimeFromTicket(this long time)
        {
            return new DateTime(time * 10000 + Long1970).ToLocalTime();
        }
        /// <summary>
        /// convert value to decimal
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object value, decimal defaultValue = 0)
        {
            if (value != null)
            {
                if (decimal.TryParse(value.ToString(), out decimal result))
                {
                    return result;
                }
            }
            return defaultValue;

        }
        /// <summary>
        /// convert value to double
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this object value, double defaultValue = 0)
        {
            if (value != null)
            {
                if (double.TryParse(value.ToString(), out double result))
                {
                    return result;
                }
            }
            return defaultValue;

        }
        /// <summary>
        /// convert value to int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this object value, int defaultValue = 0)
        {
            if (value != null)
            {
                if (int.TryParse(value.ToString(), out int result))
                {
                    return result;
                }
            }
            return defaultValue;

        }
        /// <summary>
        /// convert value to long
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this object value, long defaultValue = 0)
        {
            if (value != null)
            {
                if (long.TryParse(value.ToString(), out long result))
                {
                    return result;
                }
            }
            return defaultValue;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static long TwoTimeToSecond(DateTime startTime, DateTime endTime)
        {
            TimeSpan ts1 = new TimeSpan(startTime.Ticks);
            TimeSpan ts2 = new TimeSpan(endTime.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            return (long)ts3.TotalSeconds;
        }
    }
}