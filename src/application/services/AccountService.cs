using System.Collections.Generic;
using System.Data;
using System.Linq;
using application.services.bases;
using Dapper;
using domain.configs;
using domain.entitys;
using domain.models.dto;
using domain.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace application.services
{
    public class AccountService : BaseService1, IAccountService
    {
        public AccountService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }

        // private readonly IDbConnection dbConnection;
        // public AccountService(IOptions<ConnectionStringList> connectionStrings)
        // {
        //     dbConnection = new MySqlConnection(connectionStrings.Value.baixiaosheng1);
        // }
        // public void Dispose()
        // {
        //     dbConnection.Close();
        // }
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
            var sql = $"select au.id,au.username,au.nickname,ar.role_name from admin_users au left join admin_roles ar on au.role_id=ar.id ";
            var users = this.Query<AdminUsers>();
            var myUsers = dbConnection.Query<AdminUsers>(sql).AsQueryable();
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