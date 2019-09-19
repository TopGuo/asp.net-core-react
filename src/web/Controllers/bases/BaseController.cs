using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.enums;
using domain.models;
using infrastructure.extensions;
using infrastructure.Extensions;
using infrastructure.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace web.Controllers.bases
{
    public class ApiBaseController : Controller
    {
        SortedDictionary<string, string> ReqParams = new SortedDictionary<string, string>();
        protected const string TOKEN_NAME = "token";
        protected SourceType SourceType { get; set; }
        protected TokenModel TokenModel { get; set; }

        public override Task OnActionExecutionAsync(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context, Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate next)
        {
            try
            {
                var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
                if (userAgent.Contains("MicroMessenger"))
                {
                    SourceType = SourceType.WeChatApp;
                }
                else if (userAgent.Contains("iPhone") || userAgent.Contains("iPod") || userAgent.Contains("iPad"))
                {
                    SourceType = SourceType.IOS;
                }
                else if (userAgent.Contains("Android"))
                {
                    SourceType = SourceType.Android;
                }
                else
                {
                    //TODO:the last del
                    SourceType = SourceType.Web;
                }
                foreach (var kv in context.HttpContext.Request.Query)
                {
                    ReqParams[kv.Key] = kv.Value.ToString();
                }
                if (context.HttpContext.Request.HasFormContentType)
                {
                    foreach (var kv in context.HttpContext.Request.Form)
                    {
                        ReqParams[kv.Key] = kv.Value.ToString();
                    }
                }
                var values = context.HttpContext.GetContextDict();
                foreach (var kv in values)
                {
                    ReqParams[kv.Key] = kv.Value.ToString();
                }
                if (SourceType == SourceType.Unknown)
                {
                    context.Result = new ObjectResult(new MyResult<object>().SetStatus(ErrorCode.Unauthorized, "请设置User-Agent请求头: 如:iPhone 或者 Android 或则web"));
                }
                else
                {
                    var token = string.Empty;
                    if (ReqParams.ContainsKey(TOKEN_NAME))
                    {
                        token = ReqParams[TOKEN_NAME];
                    }
                    //can get token from server redis now only get form params
                    // ..
                    //
                    if (!context.ActionDescriptor.FilterDescriptors.Any(t => t.Filter is AllowAnonymousFilter))//need check token
                    {
                        if (string.IsNullOrEmpty(token))
                        {
                            context.Result = new ObjectResult(new MyResult<object>(ErrorCode.Unauthorized, "token is empty you are error！"));
                        }
                        else
                        {
                            //checktoken

                        }
                    }
                    else
                    {

                    }
                }

            }
            catch (System.Exception ex)
            {
                //log record
                throw ex;
            }
            return base.OnActionExecutionAsync(context, next);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                context.ExceptionHandled = true;
                //log record
                if (context.HttpContext.IsAjaxRequest())
                {
#if DEBUG
                    context.Result = Json(new MyResult<object>(context.Exception, true));
#else
                    context.Result=Json(new MyResult<object>(context.Exception));
#endif
                }
                else
                {
#if DEBUG
                    context.Result = View("Error", new MyResult<object>(context.Exception, true));
#else
                    context.Result=View("Error",new MyResult<object>(context.Exception));
#endif
                }
            }
            base.OnActionExecuted(context);
        }
    }
}