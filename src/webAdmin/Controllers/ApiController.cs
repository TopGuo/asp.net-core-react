using Microsoft.AspNetCore.Mvc;
using webAdmin.Controllers.Base;
using domain.repository;
using Microsoft.AspNetCore.Authorization;
using domain.models.dto;
using System;
using infrastructure.utils;
using domain.configs;
using System.Collections.Generic;
using infrastructure.extensions;
using domain.models;

namespace webAdmin.Controllers
{
    [Route("api/[action]")]
    [Produces("application/json")]
    public class ApiController : ApiBaseController
    {
        public IWxService WxService { get; set; }
        public ISetingService SetingService { get; set; }
        public ApiController(IWxService wxService, ISetingService setingService)
        {
            WxService = wxService;
            SetingService = setingService;
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> WxLogin([FromBody]WxLoginDto model)
        {
            return WxService.Login(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> Register([FromBody]WxRegisterDto model)
        {
            return WxService.Register(model);
        }
        [HttpPost]
        public MyResult<object> Unlimited([FromBody]UnlimitedDto model)
        {
            return WxService.GetUnlimited(base.TokenModel.Id);
        }
        [AllowAnonymous]
        [HttpPost]
        public MyResult<object> Announces([FromBody]AnnounceDto model)
        {
            return SetingService.GetAnnounceTitle();
        }
        [AllowAnonymous]
        [HttpPost]
        public MyResult<object> OneAnnounces([FromBody]AnnounceDto model)
        {
            return SetingService.GetOneAnnounce(model.Id);
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> Banners([FromBody]BannerDto model)
        {
            return SetingService.GetBanner(model);
        }
        //获取景点接口
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> Scenics([FromBody]ScenicDto model)
        {
            return SetingService.GetScenic(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> OneScenic([FromBody] ScenicDto model)
        {
            return SetingService.GetOneScenic(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> MessageType([FromBody]MessageTypeDto model)
        {
            return SetingService.GetMessageType(model);
        }
        [HttpPost]
        public MyResult<object> PubMessage([FromBody]MessageDto model)
        {
            if (string.IsNullOrEmpty(base.TokenModel.Id.ToString()) || base.TokenModel.Id < 0)
            {
                return new MyResult<object>(-1, "请检查是否登录");
            }
            List<string> urlList = new List<string>();
            if (model.BasePics.Count > 0)
            {
                model.BasePics.ForEach(pic =>
                {
                    var fileName = DateTime.Now.GetTicket().ToString();
                    var url = ImageHandlerUtil.SaveBase64Image(pic, $"{fileName}.png", $"{Constants.Message_Path}/{base.TokenModel.Id}");
                    urlList.Add(url);
                });
            }
            model.Pics = urlList.GetJson();
            model.UserId = base.TokenModel.Id;
            return SetingService.AddMessage(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> Message([FromBody]MessageDto model)
        {
            return SetingService.GetMessage(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> GetOneMessage([FromBody]MessageDto model)
        {
            return SetingService.GetOneMessage(model);
        }
        //检查状态
        [HttpGet]
        public MyResult<object> CheckUserStatus()
        {
            return SetingService.CheckUserStatus(base.TokenModel.Id);
        }
        //提交店铺审核
        [HttpPost]
        public MyResult<object> PubShop([FromBody]ShopDto model)
        {
            if (string.IsNullOrEmpty(base.TokenModel.Id.ToString()) || base.TokenModel.Id < 0)
            {
                return new MyResult<object>(-1, "请检查是否登录");
            }
            if (!string.IsNullOrEmpty(model.LogoPic))
            {
                var fileName = DateTime.Now.GetTicket().ToString();
                var url = ImageHandlerUtil.SaveBase64Image(model.LogoPic, $"{fileName}.png", $"{Constants.Shop_Logo_Path}/{base.TokenModel.Id}");
                model.LogoPic = url;
            }
            model.UserId = base.TokenModel.Id;
            return SetingService.AddShop(model);
        }
        //添加店铺详情
        [HttpPost]
        public MyResult<object> PubShopDetail([FromBody] ShopDetailDto model)
        {
            if (string.IsNullOrEmpty(base.TokenModel.Id.ToString()) || base.TokenModel.Id < 0)
            {
                return new MyResult<object>(-1, "请检查是否登录");
            }
            if (!string.IsNullOrEmpty(model.Pic))
            {
                var fileName = DateTime.Now.GetTicket().ToString();
                var url = ImageHandlerUtil.SaveBase64Image(model.Pic, $"{fileName}.png", $"{Constants.Shop_Detail_Path}/{base.TokenModel.Id}");
                model.Pic = url;
            }
            return SetingService.AddShopDetail(model);
        }
        //获取shop信息
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> Shops([FromBody] ShopDto model)
        {
            return SetingService.GetShops(model);
        }
        //获取商铺伤情
        [HttpPost]
        [AllowAnonymous]
        public MyResult<object> ShopDetails([FromBody]ShopDetailDto model)
        {
            return SetingService.GetOneShop(model);
        }
        //获取用户直推下级
        [HttpPost]
        public MyResult<object> GetMyteam([FromBody] UserDto model)
        {
            if (string.IsNullOrEmpty(base.TokenModel.Id.ToString()) || base.TokenModel.Id < 0)
            {
                return new MyResult<object>(-1, "请检查是否登录");
            }
            model.Id = base.TokenModel.Id;
            return SetingService.GetMyteam(model);
        }
        [HttpGet]
        public MyResult<object> CheckToken()
        {
            if (string.IsNullOrEmpty(base.TokenModel.Id.ToString()) || base.TokenModel.Id < 0)
            {
                return new MyResult<object>(-1, "请检查是否登录");
            }
            return WxService.CheckToken(base.TokenModel.Id);
        }
    }
}