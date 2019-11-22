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



    }
}