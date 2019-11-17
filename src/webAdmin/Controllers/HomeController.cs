using application.services;
using domain.configs;
using domain.models.dto;
using infrastructure.action;
using infrastructure.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webAdmin.Controllers
{
    public class HomeController : WebBaseController
    {
        [AllowAnonymous]
        public IActionResult Index()
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
                    return Redirect(System.Web.HttpUtility.UrlDecode(path));
                }
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult ValidateCode()
        {
            ValidateCode _vierificationCodeServices = new ValidateCode();
            string code = "";
            System.IO.MemoryStream ms = _vierificationCodeServices.Create(out code);
            CookieUtil.AppendCookie(Constants.WEBSITE_VERIFICATION_CODE, DataProtectionUtil.Protect(code));
            return File(ms.ToArray(), @"image/png");
        }
        [Route("Welcome")]
        public ViewResult Welcome()
        {
            return View();
        }
        [AllowAnonymous]
        [Route("Denied")]
        public ViewResult Denied()
        {
            return View();
        }

        [Action("角色管理", ActionType.SystemManager, 1)]
        public ViewResult Roles()
        {
            var result = PermissionService.Menus;
            return View(result);
        }
        [Action("操作员管理", ActionType.SystemManager, 2)]
        public ViewResult BackstageUser(AccountViewModel model)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewResult AddMember(string id = "0")
        {
            if (id.Equals("0"))
            {
                ViewBag.title = "添加管理员";
            }
            else
            {
                ViewBag.title = "修改管理员";
            }
            return View();
        }
        [Action("Banner管理", ActionType.ShiChangManager, 1)]
        public ViewResult BannerManager()
        {
            return View();//RegistActions.Menus
        }
        public ViewResult BannerAdd_Updata(int? id)
        {
            string title = "添加广告";
            ViewBag.title = title;
            return View();
        }
        [Action("公告管理", ActionType.ShiChangManager, 2)]
        public ViewResult Announce()
        {
            return View();
        }
        public ViewResult AddAnnounce()
        {
            return View();
        }
    }
}
