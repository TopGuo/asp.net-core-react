using Dapper;
using domain.configs;
using domain.entitys;
using domain.enums;
using domain.models;
using domain.repository;
using infrastructure.extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace application.services
{
    public class ShopService : bases.BaseService1, IShopService
    {
        public ShopService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }
        /// <summary>
        /// 店铺列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public MyResult<object> GetShopList(ShopModel model)
        {
            MyResult result = new MyResult();
            var sql = $"SELECT s.id,userId,s.`status`,s.logoPic,title,s.phoneNum, s.createTime,u.nickName from shop s LEFT JOIN `user`  u on s.userId = u.id where 1=1";
            if (!string.IsNullOrEmpty(model.NickName))
            {
                sql = sql + $" and u.nickName  like '%{model.NickName}%'";
            }
            if (!string.IsNullOrEmpty(model.PhoneNum))
            {
                sql = sql + $" and s.phoneNum like '%{model.PhoneNum}%'";
            }
            if (model.Status >= 0)
            {
                sql = sql + $" and s.`status` = {model.Status} ";
            }
            var query = base.dbConnection.Query<ShopModel>(sql).AsQueryable();
            query = query.Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
            result.Data = query;
            result.RecordCount = count;
            result.PageCount = pageCount;
            return result;
        }

        public MyResult<object> UpdateShopStatus(int? Id, int? Status)
        {
            MyResult result = new MyResult();
            if (!Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "ID非法");
            }
            if (!Status.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "Status非法");
            }
            // var shop = base.First<Shop>(predicate => predicate.Id.Equals(Id));
            var shop = base.dbConnection.QueryFirstOrDefault<Shop>($"select * from shop where id={Id}");
            if (shop != null)
            {
                shop.Status = (int)Status;
                base.Update(shop, true);
            }
            return result;
        }

        public MyResult<object> UpdateStatus(Shop model)
        {
            MyResult result = new MyResult();
            //var announce = base.First<User>(predicate => predicate.Id == model.Id);
            //announce.Status = model.Status;
            base.Update(model, true);
            result.Data = true;
            return result;
        }

    }
}
