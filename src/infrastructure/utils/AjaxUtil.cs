using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace infrastructure.utils
{
    public static class AjaxUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpContext context)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(context.Request.ContentType))
            {
                flag = (context.Request.ContentType.Equals("application/json") || context.Request.ContentType.Equals("text/json"));
            }
            if (!flag)
            {
                StringValues values = context.Request.Headers["X-Requested-With"];
                int num = 0;
                if (values.Count <= 0)
                {
                    values = context.Request.Headers["ASP.NET-Core-Requested-With"];
                    num = (values.Count <= 0 ? 0 : 1);
                }
                else
                {
                    num = 1;
                }
                flag = (num != 0);
            }
            if (!flag)
            {
                flag = (context.Request.HasFormContentType || (!string.IsNullOrEmpty(context.Request.Method) && !context.Request.Method.Equals("get", StringComparison.OrdinalIgnoreCase)));

            }

            return flag;
        }
    }
}