using System;
using System.DrawingCore;
using System.IO;
using Dapper;
using domain.configs;
using domain.entitys;
using domain.enums;
using domain.models;
using domain.models.dto;
using domain.repository;
using infrastructure.extensions;
using infrastructure.utils;
using Microsoft.Extensions.Options;

namespace application.services
{
    public class Code2SessionRep
    {
        public string Session_Key { get; set; }
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
        public string Access_Token { get; set; }
    }
    public class WxService : bases.BaseService1, IWxService
    {
        public WxService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }
        /// <summary>
        /// 缓存过期获取access_token
        /// </summary>
        public void SetAccessToken()
        {
            var getAccessTokenUrl = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={Constants.WxAppId}&secret={Constants.WxSecret}";
            var rep2 = HttpUtil.GetString(getAccessTokenUrl);
            var repObj2 = rep2.GetModel<Code2SessionRep>();
            MemoryCacheUtil.Set(Constants.WxAccessToken, repObj2?.Access_Token ?? "", 120);
        }

        public MyResult<object> Login(WxLoginDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Code))
            {
                return result.SetStatus(ErrorCode.InvalidData, "code 无效");
            }
            var code2SessionUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid={Constants.WxAppId}&secret={Constants.WxSecret}&js_code={model.Code}&grant_type=authorization_code";
            var rep = HttpUtil.GetString(code2SessionUrl);
            var repObj = rep.GetModel<Code2SessionRep>();
            var openid = repObj.OpenId;
            var user = base.First<User>(predicate => predicate.OpenId == openid);
            if (user == null)
            {
                return result.SetStatus(ErrorCode.NotFound, "用户未注册");
            }
            user.SessionKey = repObj.Session_Key;
            TokenModel tokenModel = new TokenModel();
            tokenModel.Id = (int)user.Id;
            tokenModel.Mobile = user.PhoneNum;
            tokenModel.Code = repObj.OpenId;
            tokenModel.Source = domain.enums.SourceType.WeChat;
            var tokenStr = tokenModel.GetJson();
            var enToken = DataProtectionUtil.Protect(tokenStr);
            result.Data = new
            {
                token = enToken,
                uid = (int)user.Id
            };
            user.Token = enToken;
            base.Update(user, true);
            return result;
        }

        public MyResult<object> Register(WxRegisterDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Code))
            {
                return result.SetStatus(ErrorCode.InvalidData, "code 无效");
            }
            if (string.IsNullOrEmpty(model.EncryptedData))
            {
                return result.SetStatus(ErrorCode.InvalidData, "EncryptedData 无效");
            }
            if (string.IsNullOrEmpty(model.Iv))
            {
                return result.SetStatus(ErrorCode.InvalidData, "Iv 无效");
            }
            //调登陆获取key
            var code2SessionUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid={Constants.WxAppId}&secret={Constants.WxSecret}&js_code={model.Code}&grant_type=authorization_code";
            var rep = HttpUtil.GetString(code2SessionUrl);
            var repObj = rep.GetModel<Code2SessionRep>();
            var session_key = repObj.Session_Key;
            var deCryptedData = SecurityUtil.AES_128_CBC_Decrypt(model.EncryptedData, session_key, model.Iv);
            var deCryptedDataObj = deCryptedData.GetModel<Code2SessionRep>();
            var hasUser = base.First<User>(predicate => predicate.OpenId.Equals(deCryptedDataObj.OpenId));
            if (hasUser != null)
            {
                return result.SetStatus(ErrorCode.HasValued, "数据已存在");
            }
            User user = new User
            {
                OpenId = deCryptedDataObj.OpenId,
                NickName = deCryptedDataObj.NickName,
                SessionKey = session_key,
                Pic = deCryptedDataObj.AvatarUrl
            };
            if (model.referrer.HasValue)
            {
                user.RefId = (int)model.referrer;
            }
            base.Add(user, true);
            return result;
        }

        //获取小程序码
        public MyResult<object> GetUnlimited(int userId = 0)
        {
            MyResult result = new MyResult();
            if (userId <= 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "用户Id为空 请联重新登陆");
            }
            var user = base.First<User>(predicate => predicate.Id == userId);
            if (user == null)
            {
                return result.SetStatus(ErrorCode.InvalidData, "用户不存在");
            }
            if (!string.IsNullOrEmpty(user.UPic))
            {
                result.Data = PathUtil.CombineWithRoot(user.UPic);
                return result;
            }
            var access_token = MemoryCacheUtil.Get(Constants.WxAccessToken);
            if (access_token == null)
            {
                SetAccessToken();
                access_token = MemoryCacheUtil.Get(Constants.WxAccessToken);
            }
            var UnlimitedUrl = $"https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={access_token}";
            var _scene = $"inviter_id={userId}";
            var rep = HttpUtil.PostByte(UnlimitedUrl, new { scene = _scene, is_hyaline = false }.GetJson(), "application/json");
            var url = ImageHandlerUtil.SaveByteImage(rep, $"{Constants.WxPic}/{userId}");
            user.UPic = url;
            base.Update(user, true);
            result.Data = PathUtil.CombineWithRoot(url);
            return result;
        }

        public MyResult<object> CheckToken(int userId = 0)
        {
            MyResult result = new MyResult();
            var user = base.dbConnection.QueryFirstOrDefault<User>($"select token from user where id={userId}");
            if (user == null || string.IsNullOrEmpty(user.Token))
            {
                result.Data = false;
            }
            result.Data = true;
            return result;
        }
    }
}