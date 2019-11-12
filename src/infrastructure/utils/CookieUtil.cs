using System;
using infrastructure.extensions;
using infrastructure.mvc;
using Microsoft.AspNetCore.Http;

namespace infrastructure.utils
{
    public class CookieUtil
    {
        public const string ClientTimeCookieName = "_Client_Time_Offset_";

        private static string GetName(string name, bool appendScheme)
        {
            if (appendScheme)
            {
                MvcCookieOptions service = ServiceExtension.HttpContext.RequestServices.GetService(typeof(MvcCookieOptions)) as MvcCookieOptions;
                if (service != null && !string.IsNullOrEmpty(service.AuthenticationScheme) && !name.StartsWith($"{service.AuthenticationScheme}.", StringComparison.OrdinalIgnoreCase))
                {
                    name = $"{service.AuthenticationScheme}.{name}";
                }
            }
            return name;
        }

        /// <summary>
        /// add or modify cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="appendScheme">是否追加mvc授权协议Scheme</param>
        /// <param name="action"></param>
        public static void AppendCookie(string name, string value, bool appendScheme = true, Action<CookieOptions> action = null)
        {
            CookieOptions cookieOptions = new CookieOptions();
            action?.Invoke(cookieOptions);

            ServiceExtension.HttpContext.Response.Cookies.Append(GetName(name, appendScheme), value, cookieOptions);
        }

        /// <summary>
        /// remove cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void RemoveCookie(string name, bool appendScheme = true, string value = "")
        {
            ServiceExtension.HttpContext.Response.Cookies.Delete(GetName(name, appendScheme));
        }

        /// <summary>
        /// get cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCookie(string name, bool appendScheme = true)
        {
            return ServiceExtension.HttpContext.Request.Cookies[GetName(name, appendScheme)];
        }
    }
}