using System.Collections.Generic;
using domain.entitys;
using domain.enums;
using domain.models;
using domain.repository;
using infrastructure.extensions;
using infrastructure.utils;
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
            // MyDapper myDapper=new MyDapper()
            MyResult<object> result = new MyResult<object>();
            if (!model.UserName.Equals("1") || !model.PassWord.Equals("1"))
            {
                return result.SetStatus(ErrorCode.ErrorUserNameOrPass);
            }
            TokenModel tokenModel = new TokenModel();
            tokenModel.Id = 1001;
            tokenModel.Mobile = "18333103619";
            tokenModel.Code = "";
            tokenModel.Source = domain.enums.SourceType.Web;
            var data = new
            {
                token = DataProtectionUtil.Protect(tokenModel.GetJson()),
                userName = "鸟窝"
            };
            result.Data = data;
            return result;
        }

        [HttpPost]
        public MyResult<List<AdminUsers>> GetAdminUsers([FromBody]BaseModel model)
        {
            return AccountService.GetAdminUsers(model.PageIndex, model.PageSize);
        }
    }

}