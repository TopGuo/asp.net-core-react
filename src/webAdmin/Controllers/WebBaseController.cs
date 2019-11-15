using System;
using System.Linq;
using System.Reflection;
using domain.configs;
using domain.repository;
using infrastructure.action;
using infrastructure.extensions;
using infrastructure.mvc;
using infrastructure.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace webAdmin.Controllers
{
    [MvcAuthorize(AuthenticationSchemes = Constants.WEBSITE_AUTHENTICATION_SCHEME)]
    public class WebBaseController : Controller
    {
        static WebBaseController()
        {
            try
            {
                var actions = ServiceExtension.Get<IPermissionService>();
                if (actions != null)
                {
                    var provider = ServiceExtension.Get<IActionDescriptorCollectionProvider>();
                    var descriptorList = provider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>()
                        .Where(t => t.MethodInfo.GetCustomAttribute<ActionAttribute>() != null).ToList();
                    actions.RegistAction(descriptorList);
                    actions.RegistRole();
                }
            }
            catch (System.Exception ex)
            {
                LogUtil<WebBaseController>.Error(ex.Message);
            }
        }
        #region
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var provider = ServiceExtension.Get<IActionDescriptorCollectionProvider>();
            var desc1 = (context.ActionDescriptor as ControllerActionDescriptor);
            var desc2 = provider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>()
                .Where(t => t.MethodInfo.GetCustomAttribute<ActionAttribute>() != null && t.DisplayName == desc1.DisplayName).FirstOrDefault();
            var desc3 = desc2 ?? desc1;
            var action = desc3.MethodInfo.GetCustomAttribute<ActionAttribute>();
            if (action != null)
            {
                var actions = ServiceExtension.Get<IPermissionService>();
                if (actions != null && !actions.HasPermission(context, desc3.Id))
                {
                    return;
                }

            }

            if (desc3.ActionName == "Index" && desc3.ControllerName == "Home")
            {
                if (User.Identity.IsAuthenticated)
                {
                    string path = HttpContext.Request.Query["from"];
                    if (string.IsNullOrEmpty(path))
                    {
                        path = CookieUtil.GetCookie(Constants.LAST_LOGIN_PATH);
                    }
                    if (!string.IsNullOrEmpty(path) && path != "/")
                    {
                        context.Result = Redirect(path);
                    }
                }
            }
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                base.OnActionExecuted(context);
                return;
            }
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (!context.HttpContext.IsAjaxRequest() && !action.ActionName.Equals("Index", StringComparison.OrdinalIgnoreCase) && !action.ControllerName.Equals("webapi", StringComparison.OrdinalIgnoreCase))
            {
                CookieUtil.AppendCookie(Constants.LAST_LOGIN_PATH, HttpContext.Request.Path);
            }
            base.OnActionExecuted(context);
        }
        #endregion
    }
}