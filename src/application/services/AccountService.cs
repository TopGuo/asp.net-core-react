using System.Collections.Generic;
using System.Linq;
using application.services.bases;
using Dapper;
using domain.configs;
using domain.entitys;
using domain.enums;
using domain.models;
using domain.repository;
using infrastructure.extensions;
using infrastructure.utils;
using Microsoft.Extensions.Options;

namespace application.services
{
    public class AccountService : BaseService1, IAccountService
    {
        public AccountService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }
        public AdminUsers GetAdminUser(int id)
        {
            var users = this.First<domain.entitys.AdminUsers>();
            return users;
        }

        public AdminUsers GetAdminUsers()
        {
            var users = this.First<domain.entitys.AdminUsers>();
            return users;
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}