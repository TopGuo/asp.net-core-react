using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;

namespace infrastructure.utils
{
    public static class GetParamsUtil
    {
        /// <summary>
        /// get value from RouteData,QueryString,Form,Cookie
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Params(this HttpContext context, string key)
        {
            string value = string.Empty;
            var help = context.Items.SingleOrDefault(t => t.Value is UrlHelper).Value as UrlHelper;
            if (help != null)
            {
                var route = help.ActionContext.RouteData.Values[key];
                if (route != null)
                {
                    return route.ToString();
                }
            }

            if (context.Request.Query.ContainsKey(key))
            {
                value = context.Request.Query[key];
            }
            else if (context.Request.HasFormContentType && context.Request.Form.ContainsKey(key))
            {
                value = context.Request.Form[key];
            }
            else if (context.Request.Cookies.Count > 0 && context.Request.Cookies.ContainsKey(key))
            {
                value = context.Request.Cookies[key];
            }

            return value;
        }

        public static string Params(this RazorPage page, string key)
        {
            return page.Context.Params(key);
        }
        public static string Params(this ControllerBase controller, string key)
        {
            return controller.HttpContext.Params(key);
        }
    }
}