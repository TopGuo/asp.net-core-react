using System;
using System.Collections.Generic;
using System.Linq;
using application.services.bases;
using Dapper;
using domain.configs;
using domain.entitys;
using domain.enums;
using domain.models;
using domain.models.dto;
using domain.repository;
using infrastructure.extensions;
using infrastructure.mvc;
using infrastructure.utils;
using Microsoft.Extensions.Options;

namespace application.services
{
    public class AccountService : BaseService1, IAccountService
    {
        public AccountService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }
        public MyResult<object> GetUserAuth(string name, string password)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                return result.SetError("用户名密码不能为空");
            }
            string auth_sql = $"select au.id,au.username,au.password,au.role_id roleId,ifnull(ar.role_name,'') roleName from admin_users au left join admin_roles ar on au.role_id=ar.id where au.username='{name}' and au.password='{password}'";
            var userInfo = dbConnection.QuerySingleOrDefault(auth_sql);
            if (userInfo == null)
            {
                return result.SetStatus(ErrorCode.ErrorUserNameOrPass, "用户名密码错误");
            }
            var roleId = userInfo.roleId;
            string action_sql = $"select aa.action_name actionName,aa.code from admin_role_action ara left join admin_actions aa on ara.action_id=aa.id and aa.enable=1 where ara.role_id={roleId}";
            var action = dbConnection.Query(action_sql);
            TokenModel tokenModel = new TokenModel();
            tokenModel.Id = userInfo.id;
            tokenModel.Mobile = "";
            tokenModel.Code = "";
            tokenModel.Source = domain.enums.SourceType.Web;
            result.Data = new
            {
                token = DataProtectionUtil.Protect(tokenModel.GetJson()),
                userData = new
                {
                    userInfo = userInfo,
                    action = action
                }
            };
            return result;
        }

        public MyResult<List<domain.entitys.AdminUsers>> GetAdminUsers(int pageIndex, int pageSize)
        {
            MyResult<List<domain.entitys.AdminUsers>> result = new MyResult<List<domain.entitys.AdminUsers>>();
            var sql = $"select au.id,au.username,au.nickname,ar.role_name from admin_users au left join admin_roles ar on au.role_id=ar.id ";
            var users = this.Query<domain.entitys.AdminUsers>();
            var myUsers = dbConnection.Query<domain.entitys.AdminUsers>(sql).AsQueryable();
            users = this.Pages(users, pageIndex, pageSize, out int count, out int pageCount);
            result.PageCount = pageCount;
            result.RecordCount = count;
            result.Data = users.ToList();
            return result;
        }
        #region web admin
        public MyResult<object> Login(BackstageUserAdd model)
        {
            MyResult result = new MyResult();
            string sessionCode = string.Empty;
            try
            {
                var code = CookieUtil.GetCookie(Constants.WEBSITE_VERIFICATION_CODE);
                if (code != null)
                {
                    sessionCode = DataProtectionUtil.UnProtect(code);
                }
            }
            catch (Exception ex)
            {
                LogUtil<AccountService>.Error(ex.Message);
            }
            if (model.ErrCount >= 3)
            {
                if (!model.VerCode.ToString().ToLower().Equals(sessionCode.ToLower()))
                {
                    return result.SetStatus(ErrorCode.NotFound, "验证码输入不正确！");
                }
            }

            BackstageUser account = this.First<BackstageUser>(t => t.LoginName == model.LoginName);
            if (account == null)
            {
                return result.SetStatus(ErrorCode.NotFound, "账号不存在！");
            }
            string pwd = SecurityUtil.MD5(model.Password);
            if (!account.Password.Equals(pwd, StringComparison.OrdinalIgnoreCase))
            {
                return result.SetStatus(ErrorCode.InvalidPassword);
            }
            switch (account.AccountStatus)
            {
                case (int)AccountStatus.Disabled:
                    return result.SetStatus(ErrorCode.AccountDisabled, "账号不可用！");
            }

            account.LastLoginTime = DateTime.Now;
            account.LastLoginIp = "";//MvcHelper.ClientIP;
            this.Update(account, true);
            MvcIdentity identity = new MvcIdentity(account.Id, account.LoginName, account.LoginName, account.Email, (int)account.RoleId, null, account.LastLoginTime);
            identity.Login(Constants.WEBSITE_AUTHENTICATION_SCHEME, x =>
            {
                x.Expires = DateTime.Now.AddHours(5);//滑动过期时间
                x.HttpOnly = true;
            });

            return result;
        }

        public MyResult<object> UpdateAccount(BackstageUserAdd model)
        {
            MyResult result = new MyResult();
            BackstageUser account = base.First<BackstageUser>(t => string.IsNullOrEmpty(model.Id) && t.LoginName.Equals(model.LoginName));
            if (account != null)
            {
                return result.SetStatus(ErrorCode.NotFound, "登录名称已经存在！");
            }
            else
            {
                account = this.First<BackstageUser>(t => t.Id.Equals(model.Id));
                if (account == null)
                {
                    return result.SetStatus(ErrorCode.NotFound, "用户异常操作失败！");
                }
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                string pwd = SecurityUtil.MD5(model.Password);
                account.Password = pwd;
            }
            if (!DataValidUtil.IsIDCard(model.IdCard))
            {
                return result.SetStatus(ErrorCode.InvalidData, "身份证非法！");
            }
            account.LoginName = model.LoginName;
            account.AccountStatus = (int)model.AccountStatus;
            account.FullName = model.FullName;
            account.RoleId = (int)model.AccountType;
            account.Mobile = model.Mobile;
            account.UpdateTime = DateTime.Now;
            account.Gender = model.Gender;
            account.AccountType = (int)model.AccountType;
            account.IdCard = model.IdCard;
            this.Update(account, true);
            return result;
        }

        public MyResult<object> AddAccount(BackstageUserAdd model)
        {
            MyResult result = new MyResult();
            BackstageUser account = this.First<BackstageUser>(t => t.LoginName == model.LoginName);
            if (account != null)
            {
                return result.SetStatus(ErrorCode.NotFound, "登录名称已经存在！");
            }
            else
            {
                account = new BackstageUser();
            }
            if (!DataValidUtil.IsIDCard(model.IdCard))
            {
                return result.SetStatus(ErrorCode.InvalidData, "身份证非法！");
            }
            model.Password = model.Password == "" ? "123456" : model.Password;
            string pwd = SecurityUtil.MD5(model.Password);
            account.Id = Guid.NewGuid().ToString("N");
            account.LoginName = model.LoginName;
            account.FullName = model.FullName;
            account.CreateTime = DateTime.Now;
            account.AccountType = (int)model.AccountType;
            account.RoleId = (int)model.AccountType;
            account.Password = pwd;
            account.Mobile = model.Mobile;
            account.AccountStatus = (int)AccountStatus.Normal;
            account.SourceType = (int)SourceType.Web;
            account.Gender = model.Gender;
            account.IdCard = model.IdCard;
            this.Add(account, true);
            return result;
        }

        public MyResult<object> UpdatePwd(BackstageUserAdd model)
        {
            MyResult result = new MyResult();
            BackstageCookie backUser = GetUserCook();
            BackstageUser backstageModel = this.First<BackstageUser>(t => t.Id == backUser.Id);
            if (backstageModel == null)
            {
                return result.SetStatus(ErrorCode.NotFound, "登录名称不存在！");
            }
            string pwd = SecurityUtil.MD5(model.OldPassword);
            if (pwd.Equals(backstageModel.Password))
            {
                string pwdNew = SecurityUtil.MD5(model.ConfirmPassword);
                backstageModel.Password = pwdNew;
            }
            else
            {
                return result.SetStatus(ErrorCode.NotFound, "您输入的密码不正确！");
            }
            this.Update(backstageModel, true);
            return result;
        }

        public MyResult<object> GetBackstageUser(string id)
        {
            MyResult result = new MyResult();
            BackstageUser backstageUser = this.First<BackstageUser>(t => t.Id == id);
            if (backstageUser == null)
            {
                backstageUser = new BackstageUser();
            }
            else
            {
                backstageUser.Password = "";
            }
            result.Data = backstageUser;
            return result;
        }

        public MyResult<object> GetBackstageUserList(AccountSearchModel model)
        {
            MyResult result = new MyResult();
            var query = base.Query<BackstageUser>();

            if (!string.IsNullOrEmpty(model.FullName))
            {
                query = query.Where(t => t.FullName.Contains(model.FullName));
            }
            if (!string.IsNullOrEmpty(model.Mobile))
            {
                query = query.Where(t => t.Mobile == model.Mobile);
            }
            if (model.AccountStatus != null && (int)model.AccountStatus > 0)
            {
                query = query.Where(t => t.AccountStatus == (int)model.AccountStatus);
            }
            if (model.BeginTime.HasValue)
            {
                query = query.Where(t => t.CreateTime >= model.BeginTime);
            }
            if (model.EndTime.HasValue)
            {
                query = query.Where(t => t.CreateTime <= model.EndTime);
            }
            var objList = query.OrderByDescending(t => t.CreateTime).Pages(model.PageIndex, model.PageSize, out int count).
                Select(t => new
                {
                    t.Id,
                    roleName = t.Role.Name,
                    t.AccountStatus,
                    t.FullName,
                    t.SourceType,
                    t.CreateTime,
                    t.LastLoginTime,
                    t.LastLoginIp,
                    t.Mobile,
                    t.LoginName,
                    t.UpdateTime,
                    t.IdCard,
                    t.AccountType
                }).ToList();
            List<AccountSearchModel> _list = new List<AccountSearchModel>();
            objList.ForEach(t => _list.Add(
                new AccountSearchModel
                {
                    Id = t.Id,
                    RoleName = t.roleName,
                    AccountStatusName = ((AccountStatus)t.AccountStatus).GetEnumToString(),
                    FullName = t.FullName,
                    SourceTypeName = ((SourceType)t.SourceType).GetEnumToString(),
                    CreateTime = t.CreateTime,
                    LastLoginTime = t.LastLoginTime,
                    LastLoginIp = t.LastLoginIp,
                    Mobile = t.Mobile,
                    LoginName = t.LoginName,
                    UpdateTime = t.UpdateTime,
                    AccountStatus = (AccountStatus)t.AccountStatus,
                    IdCard = t.IdCard,
                    AccountType = (AccountType)t.AccountType
                }
                ));
            result.Data = _list;
            result.RecordCount = count;
            return result;
        }

        public BackstageCookie GetUserCook()
        {
            string cookie = DataProtectionUtil.UnProtect(CookieUtil.GetCookie(Constants.WEBSITE_AUTHENTICATION_SCHEME));
            BackstageCookie back = new BackstageCookie();
            back = cookie.GetModel<BackstageCookie>();
            return back;
        }

        public MyResult<object> LogoutUser()
        {
            MyResult result = new MyResult();
            CookieUtil.RemoveCookie(Constants.WEBSITE_AUTHENTICATION_SCHEME);
            return result;
        }
        #endregion
    }

}