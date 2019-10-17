using System.Collections.Generic;
using domain.entitys;
using domain.models;
using domain.repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Controllers.bases;

namespace web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : ApiBaseController
    {
        public IAccountService AccountService { get; set; }
        public UserController(IAccountService accountService)
        {
            AccountService = accountService;
        }
        [HttpGet]
        [AllowAnonymous]
        public MyResult<object> WxLogin()
        {
            
            MyResult<object> result = new MyResult<object>();
            
            return result;
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> Login([FromBody]UserModel model)
        {
            return AccountService.GetUserAuth(model.UserName,model.PassWord);
        }

        [HttpPost]
        public MyResult<List<AdminUsers>> GetAdminUsers([FromBody]BaseModel model)
        {
            return AccountService.GetAdminUsers(model.PageIndex, model.PageSize);
        }
    }

}