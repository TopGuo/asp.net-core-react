using System;
using application.services;
using domain.configs;
using domain.entitys;
using domain.enums;
using domain.models;
using domain.models.dto;
using domain.repository;
using infrastructure.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webAdmin.Controllers.Base;

namespace webAdmin.Controllers
{
    [Route("webapi/[action]")]
    public class WebApiController : WebBaseController
    {
        public IAccountService AccountService { get; set; }
        public IPermissionService PermissionService { get; set; }
        public IUserService UserService { get; set; }
        public ISetingService SetingService { get; set; }
        public IShopService ShopService { get; set; }
        public WebApiController(IAccountService accountService, IPermissionService permissionService, ISetingService setingService, IUserService userService,IShopService shopService)
        {
            AccountService = accountService;
            PermissionService = permissionService;
            SetingService = setingService;
            UserService = userService;
            ShopService = shopService;
        }

        #region 登录模块
        [AllowAnonymous]
        [HttpPost]
        public MyResult<object> Login([FromBody]BackstageUserAdd model)
        {
            return AccountService.Login(model);
        }

        [HttpGet]
        public MyResult<object> Logout()
        {
            MyResult result = new MyResult();
            return AccountService.LogoutUser();
        }
        #endregion

        #region 添加后台管理员
        [HttpPost]
        public MyResult<object> AddMemberAdd_Update([FromBody]BackstageUserAdd model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                return AccountService.UpdateAccount(model);
            }
            return AccountService.AddAccount(model);
        }
        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public MyResult<object> MemberPwd_Update([FromBody]BackstageUserAdd model)
        {
            return AccountService.UpdatePwd(model);
        }
        [HttpGet]
        public MyResult<object> GetBackstageUser(string id)
        {
            return AccountService.GetBackstageUser(id);
        }
        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public MyResult<object> SetMemberState([FromBody]BackstageUserAdd model)
        {
            if (model.AccountStatus == AccountStatus.Normal)
            {
                model.AccountStatus = AccountStatus.Disabled;
            }
            else
            {
                model.AccountStatus = AccountStatus.Normal;
            }
            return AccountService.UpdateAccount(model);
        }

        #endregion


        #region 后台用户列表
        public MyResult<object> BackstageUser([FromBody]AccountSearchModel model)
        {
            return AccountService.GetBackstageUserList(model);
        }
        #endregion


        #region  微信用户列表
        public MyResult<object> WechatUser([FromBody]UserModel model)
        {
            return UserService.GetUserList(model);
        }
        /// <summary>
        /// 修改微信用户状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public MyResult<object> UpdateUserStatus([FromBody]User model)
        {
            if (model.Status == 1)
            {
                model.Status = 0;
            }
            else
            {
                model.Status = 1;
            }
            return UserService.UpdateStatusUser(model);
        }
        #endregion

        #region 角色模块
        [HttpPost]
        public MyResult GetRoles()
        {
            return PermissionService.GetRoles();
        }
        [HttpPost]
        public MyResult<object> SaveRoles([FromBody]RoleModel model)
        {
            return PermissionService.SaveRoles(model);
        }
        [HttpPost]
        public MyResult<object> DeleteRoles([FromBody]RoleModel model)
        {
            return PermissionService.DeleteRoles(model);
        }
        #endregion

        #region Seting模块
        [HttpPost]
        public MyResult<object> AnnounceAdd_Update([FromBody]AnnounceDto model)
        {
            if (!string.IsNullOrEmpty(model.Id.ToString()) && model.Id > 0)
            {
                return SetingService.UpdateAnnounce(model);
            }
            return SetingService.AddAnnounce(model);
        }
        //获取公告
        [HttpPost]
        public MyResult<object> GetAnnounces([FromBody]AnnounceDto model)
        {
            return SetingService.GetAnnounces(model);
        }

        //添加公告
        [HttpPost]
        public MyResult<object> AddAnnounce([FromBody]AnnounceDto model)
        {
            return SetingService.AddAnnounce(model);
        }
        //删除公告
        [HttpGet]
        public MyResult<object> DelAnnounce(int id)
        {
            return SetingService.DelAnnounce(id);
        }
        //修改公告
        [HttpPost]
        public MyResult<object> UpdateAnnounce([FromBody]AnnounceDto model)
        {
            return SetingService.UpdateAnnounce(model);
        }
        //获取一条公告
        [HttpGet]
        public MyResult<object> GetOneAnnounce(int id)
        {
            return SetingService.GetOneAnnounce(id);
        }
        //获取公告标题
        [HttpGet]
        public MyResult<object> GetAnnounceTitle()
        {
            return SetingService.GetAnnounceTitle();
        }

        //获取banner
        [HttpPost]
        public MyResult<object> BannerManagerList([FromBody]BannerDto model)
        {
            return SetingService.GetBanner(model);
        }
        [HttpPost]
        public MyResult<object> DelBannerPic([FromBody]BannerDto model)
        {
            return SetingService.DelBanner(model.Id);
        }
        [HttpPost]
        public MyResult<object> BannerAdd_Updata([FromBody] BannerDto model)
        {
            if (!string.IsNullOrEmpty(model.Pic) && model.Pic.Length > 1000)
            {
                var fileName = DateTime.Now.GetTicket().ToString();
                model.Pic = ImageHandlerUtil.SaveBase64Image(model.Pic, $"{fileName}.png", Constants.BANNER_PATH);
            }
            return SetingService.AddBanner(model);
        }
        [HttpPost]
        public MyResult<object> ScenicList([FromBody]ScenicDto model)
        {
            return SetingService.GetScenic(model);
        }
        [HttpPost]
        public MyResult<object> DelScenic([FromBody]ScenicDto model)
        {
            return SetingService.DelScenic(model);
        }
        [HttpPost]
        public MyResult<object> ScenicAdd_Updata([FromBody] ScenicDto model)
        {
            if (!string.IsNullOrEmpty(model.Pic) && model.Pic.Length > 1000)
            {
                var fileName = DateTime.Now.GetTicket().ToString();
                model.Pic = ImageHandlerUtil.SaveBase64Image(model.Pic, $"{fileName}.png", Constants.SCENIC_PATH);
            }
            if (model.Id.HasValue)
            {
                return SetingService.UpdateScenic(model);
            }
            return SetingService.AddScenic(model);
        }
        public MyResult<object> GetOneScenic(int id)
        {
            ScenicDto model = new ScenicDto { Id = id };
            return SetingService.GetOneScenic(model);
        }
        [HttpPost]
        public MyResult<object> MessType([FromBody]MessageTypeDto model)
        {
            return SetingService.GetMessageType(model);
        }
        [HttpPost]
        public MyResult<object> SaveMessageType([FromBody]MessageTypeDto model)
        {
            if (!string.IsNullOrEmpty(model.Pic) && model.Pic.Length > 1000)
            {
                var fileName = DateTime.Now.GetTicket().ToString();
                model.Pic = ImageHandlerUtil.SaveBase64Image(model.Pic, $"{fileName}.png", Constants.MessageType_PATH);
            }
            if (model.Id.HasValue)
            {
                return SetingService.UpdateMessageType(model);
            }
            return SetingService.AddMessageType(model);
        }
        [HttpPost]
        public MyResult<object> DelMessageType([FromBody]MessageTypeDto model)
        {
            return SetingService.DelMessageType(model);
        }

        #endregion

        #region
        /// <summary>
        /// 店铺列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MyResult<object> ShopList([FromBody]ShopModel model)
        {
            return ShopService.GetShopList(model);
        }
        #endregion
    }
}