using System.Linq;
using System.Reflection;
using domain.configs;
using domain.repository;
using infrastructure.action;
using infrastructure.extensions;
using infrastructure.mvc;
using infrastructure.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
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
    }
}