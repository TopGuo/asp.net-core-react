using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.configs;
using domain.enums;
using domain.models;
using domain.repository;
using infrastructure.extensions;
using infrastructure.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace webAdmin.Controllers.Base
{
    [Route("api/[controller]")]
    public class ApiBaseController : Controller
    {
        SortedDictionary<string, string> ReqParams = new SortedDictionary<string, string>();
        protected const string TOKEN_NAME = "token";
        protected const string Sign = "sign";
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
                    var sign = string.Empty;
                    if (ReqParams.ContainsKey(TOKEN_NAME))
                    {
                        token = ReqParams[TOKEN_NAME];
                    }
                    if (ReqParams.ContainsKey(Sign))
                    {
                        sign = ReqParams[Sign];
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
                        else if (string.IsNullOrEmpty(sign))
                        {
                            context.Result = new ObjectResult(new MyResult<object>(ErrorCode.Unauthorized, "sign is empty you are error！"));
                        }
                        else
                        {
                            var model = CheckToken(token, sign);
                            if (model.Success)
                            {
                                //ok
                            }
                            if (!model.Success)
                            {
                                context.Result = new ObjectResult(model);
                            }

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(token))
                        {
                            TokenModel = new TokenModel();
                        }
                        else
                        {
                            var json = DataProtectionUtil.UnProtect(token);
                            if (string.IsNullOrEmpty(json))
                            {
                                TokenModel = new TokenModel();
                            }
                            else
                            {
                                TokenModel = json.GetModel<TokenModel>();
                            }
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                LogUtil<ApiBaseController>.Error(ex, ex.Message);
                context.Result = new ObjectResult(new MyResult<object>(ErrorCode.SystemError, $"请求失败{ex.Message}"));
            }
            return base.OnActionExecutionAsync(context, next);
        }

        private MyResult<object> CheckToken(string token, string sign)
        {
            MyResult<object> result = new MyResult<object>();
            try
            {
                //sign==
                if (!SecurityUtil.ValidSign(sign, token, Constants.Key))
                {
                    return result.SetStatus(ErrorCode.ReLogin, "sign error 请联系管理员");
                }
                //token==
                string json = DataProtectionUtil.UnProtect(token);
                if (string.IsNullOrEmpty(json))
                {
                    return result.SetStatus(ErrorCode.ReLogin, "token error 请重新登录");
                }
                TokenModel = json.GetModel<TokenModel>();
                if (TokenModel == null)
                {
                    return result.SetStatus(ErrorCode.InvalidToken, "非法token");
                }
                if (TokenModel.Id < 1)
                {
                    return result.SetStatus(ErrorCode.InvalidToken, "无效token");
                }

            }
            catch (System.Exception ex)
            {
                return result.SetStatus(ErrorCode.SystemError, $"请求失败{ex.Message}");
            }
            return result;
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
                LogUtil<ApiBaseController>.Error(context.Exception, context.Exception.Message);
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