using System.Collections.Generic;
using System.Data;
using System.Linq;
using application.services.bases;
using domain.entitys;
using domain.models.dto;
using domain.repository;
using Microsoft.EntityFrameworkCore;

namespace application.services
{
    public class AccountService : BaseService1, IAccountService
    {
        public AdminUsers GetAdminUser(int id)
        {
            var users = this.First<AdminUsers>();
            return users;
        }

        public AdminUsers GetAdminUsers()
        {
            var users = this.First<AdminUsers>();
            return users;
        }

        public MyResult<List<AdminUsers>> GetAdminUsers(int pageIndex, int pageSize)
        {
            MyResult<List<AdminUsers>> result = new MyResult<List<AdminUsers>>();
            var sql = $"select au.id,au.username,au.nickname,ar.role_name from admin_users au left join admin_roles ar on au.role=ar.id ";

            var users = this.Query<AdminUsers>();
            users = this.Pages(users, pageIndex, pageSize, out int count, out int pageCount);
            result.PageCount = pageCount;
            result.RecordCount = count;
            result.Data = users.ToList();
            return result;
        }
    }

}