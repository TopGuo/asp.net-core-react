using domain.configs;
using infrastructure.mvc;
using Microsoft.AspNetCore.Mvc;

namespace webAdmin.Controllers
{
    [MvcAuthorize(AuthenticationSchemes = Constants.WEBSITE_AUTHENTICATION_SCHEME)]
    public class WebBaseController : Controller
    {
        
    }
}