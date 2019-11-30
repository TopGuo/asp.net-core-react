using System;
using System.Linq;
using Dapper;
using domain.configs;
using domain.entitys;
using domain.enums;
using domain.models.dto;
using domain.repository;
using infrastructure.extensions;
using infrastructure.utils;
using Microsoft.Extensions.Options;

namespace application.services
{
    public class SetingService : bases.BaseService1, ISetingService
    {
        public SetingService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }

        public MyResult<object> AddAnnounce(AnnounceDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Title))
            {
                return result.SetStatus(ErrorCode.InvalidData, "公告标题不能为空");
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return result.SetStatus(ErrorCode.InvalidData, "公告内容不能为空");
            }
            if (string.IsNullOrEmpty(model.Type.ToString()) || model.Type < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "公告类型非法");
            }
            Announce announce = new Announce
            {
                Title = model.Title,
                Content = model.Content,
                Types = model.Type
            };
            base.Add(announce, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> AddBanner(BannerDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Pic))
            {
                return result.SetStatus(ErrorCode.InvalidData, "图片数据非法");
            }
            if (string.IsNullOrEmpty(model.Types.ToString()) || model.Types < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "类型数据非法");
            }
            Banner banner = new Banner
            {
                Pic = model.Pic,
                Types = (int)model.Types
            };
            if (!string.IsNullOrEmpty(model.JumpUrl))
            {
                banner.JumpUrl = model.JumpUrl;
            }
            base.Add(banner, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> AddMessage(MessageDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Content))
            {
                return result.SetStatus(ErrorCode.InvalidData, "内容数据非法");
            }
            if (string.IsNullOrEmpty(model.Pics))
            {
                return result.SetStatus(ErrorCode.InvalidData, "图片数据非法");
            }
            // if (string.IsNullOrEmpty(model.Title))
            // {
            //     return result.SetStatus(ErrorCode.InvalidData, "标题数据非法");
            // }
            if (string.IsNullOrEmpty(model.Types.ToString()) || model.Types < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "类型数据非法");
            }
            MessageInfo messageInfo = new MessageInfo
            {
                Title = model?.Title ?? "",
                Content = model.Content,
                Pics = model.Pics,
                Types = model.Types,
                UserId = model.UserId
            };
            if (!string.IsNullOrEmpty(model.Order.ToString()) && model.Order > 0)
            {
                messageInfo.Order = model.Order;
            }
            if (!string.IsNullOrEmpty(model.LookCount.ToString()) && model.LookCount > 0)
            {
                messageInfo.LookCount = model.LookCount;
            }
            base.Add(messageInfo, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> AddMessageType(MessageTypeDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Pic))
            {
                return result.SetStatus(ErrorCode.InvalidData, "图片数据非法");
            }
            if (string.IsNullOrEmpty(model.Title))
            {
                return result.SetStatus(ErrorCode.InvalidData, "标题数据非法");
            }
            if (string.IsNullOrEmpty(model.Types.ToString()) || model.Types < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "类型数据非法");
            }
            var type = base.First<MessageType>(predicate => predicate.Types.Equals(model.Types));
            if (type != null)
            {
                return result.SetStatus(ErrorCode.InvalidData, "信息类别重复");
            }
            MessageType messageType = new MessageType
            {
                Title = model.Title,
                Pic = model.Pic,
                Types = model.Types
            };
            if (!string.IsNullOrEmpty(model.Order.ToString()) && model.Order > 0)
            {
                messageType.Order = model.Order;
            }
            base.Add(messageType, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> AddScenic(ScenicDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Pic))
            {
                return result.SetStatus(ErrorCode.InvalidData, "图片数据非法");
            }
            if (string.IsNullOrEmpty(model.Title))
            {
                return result.SetStatus(ErrorCode.InvalidData, "标题数据非法");
            }
            if (string.IsNullOrEmpty(model.LTitle))
            {
                return result.SetStatus(ErrorCode.InvalidData, "L标题数据非法");
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return result.SetStatus(ErrorCode.InvalidData, "内容数据非法");
            }
            Scenic scenic = new Scenic
            {
                Title = model.Title,
                Pic = model.Pic,
                LTitle = model.LTitle,
                Content = model.Content
            };
            if (!string.IsNullOrEmpty(model.Order.ToString()) && model.Order > 0)
            {
                scenic.Order = model.Order;
            }
            if (!string.IsNullOrEmpty(model.LookCount.ToString()) && model.LookCount > 0)
            {
                scenic.LookCount = (int)model.LookCount;
            }
            if (!string.IsNullOrEmpty(model.Mark1))
            {
                scenic.Mark1 = model.Mark1;
            }
            if (!string.IsNullOrEmpty(model.Mark2))
            {
                scenic.Mark2 = model.Mark2;
            }
            base.Add(scenic, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> AddShop(ShopDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.UserId.ToString()) || model.UserId < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "用户ID无效");
            }
            if (string.IsNullOrEmpty(model.Title))
            {
                return result.SetStatus(ErrorCode.InvalidData, "店铺名称无效");
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return result.SetStatus(ErrorCode.InvalidData, "店铺简介无效");
            }
            if (string.IsNullOrEmpty(model.LogoPic))
            {
                return result.SetStatus(ErrorCode.InvalidData, "店铺Logo无效");
            }
            if (!model.PhoneNum.IsMobile())
            {
                return result.SetStatus(ErrorCode.InvalidData, "手机号无效");
            }
            var isHasShop = base.First<Shop>(Predicate => Predicate.UserId.Equals(model.UserId));
            if (isHasShop != null)
            {
                return result.SetStatus(ErrorCode.InvalidData, "店铺信息已存在");
            }
            Shop shop = new Shop
            {
                UserId = model.UserId,
                Status = 0,
                Title = model.Title,
                Content = model.Content,
                LogoPic = model.LogoPic,
                OpenTime = model.OpenTime,
                CloseTime = model.CloseTime,
                PhoneNum = model.PhoneNum
            };
            if (model.Latitude.HasValue)
            {
                shop.Latitude = model.Latitude;
            }
            if (model.Longitude.HasValue)
            {
                shop.Longitude = model.Longitude;
            }
            base.Add(shop, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> AddShopDetail(ShopDetailDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.ShopId.ToString()) || model.ShopId < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "商品ID无效");
            }
            if (string.IsNullOrEmpty(model.Pic))
            {
                return result.SetStatus(ErrorCode.InvalidData, "图片内容非法");
            }
            var conuts = base.Count<ShopsDetail>(predicate => predicate.ShopId.Equals(model.ShopId));
            if (conuts > 4)
            {
                return result.SetStatus(ErrorCode.InvalidData, "添加图片数量超出限制 如需扩容请联系管理员");
            }
            ShopsDetail shopsDetail = new ShopsDetail
            {
                ShopId = (int)model.ShopId,
                Pic = model.Pic
            };
            if (!string.IsNullOrEmpty(model.Content))
            {
                shopsDetail.Content = model.Content;
            }
            base.Add(shopsDetail, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> CheckUserStatus(int userId)
        {
            MyResult result = new MyResult();
            if (userId <= 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "用户非法");
            }
            var userShop = base.First<Shop>(predicate => predicate.UserId.Equals(userId));
            if (userShop == null)
            {
                result.Data = new { Id = -1, Status = -1 };
                return result;
            }
            result.Data = new { Id = userShop.Id, Status = userShop.Status };
            return result;
        }

        public MyResult<object> DelAnnounce(int id)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(id.ToString()) || id < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id非法");
            }
            var announce = base.First<Announce>(t => t.Id == id);
            announce.IsDel = 1;
            base.Update(announce, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> DelBanner(int? id)
        {
            MyResult result = new MyResult();
            if (!id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id 非法");
            }
            var banner = base.First<Banner>(predicate => predicate.Id == id);
            if (banner != null)
            {
                base.Delete(banner, true);
            }
            result.Data = true;
            return result;
        }

        public MyResult<object> DelMessage(MessageDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id 非法");
            }
            var messageInfo = base.First<MessageInfo>(predicate => predicate.Id == model.Id);
            if (messageInfo != null)
            {
                base.Delete(messageInfo, true);
            }
            result.Data = true;
            return result;
        }

        public MyResult<object> DelMessageType(MessageTypeDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id 非法");
            }
            var messageType = base.First<MessageType>(predicate => predicate.Id == model.Id);
            if (messageType != null)
            {
                base.Delete(messageType, true);
            }
            result.Data = true;
            return result;
        }

        public MyResult<object> DelScenic(ScenicDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id 非法");
            }
            var scenic = base.First<Scenic>(predicate => predicate.Id == model.Id);
            if (scenic != null)
            {
                base.Delete(scenic, true);
            }
            result.Data = true;
            return result;
        }

        public MyResult<object> DelShop(ShopDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id 非法");
            }
            var shop = base.First<Shop>(predicate => predicate.Id == model.Id);
            if (shop != null)
            {
                base.Delete(shop, true);
            }
            result.Data = true;
            return result;
        }

        public MyResult<object> DelShopDetail(ShopDetailDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id 非法");
            }
            var shopsDetail = base.First<ShopsDetail>(predicate => predicate.Id == model.Id);
            if (shopsDetail != null)
            {
                base.Delete(shopsDetail, true);
            }
            result.Data = true;
            return result;
        }

        public MyResult<object> GetAnnounces(AnnounceDto model)
        {
            MyResult result = new MyResult();
            var query = base.Query<Announce>();
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(t => t.Title == model.Title);
            }
            query = query.Where(t => t.IsDel.Equals(0)).Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
            result.Data = query;
            result.RecordCount = count;
            result.PageCount = pageCount;
            return result;
        }

        public MyResult<object> GetAnnounceTitle()
        {
            MyResult result = new MyResult();
            var announce = base.Where<Announce>(t => t.IsDel == 0).Select(selector => new { selector.Id, selector.Title });
            result.Data = announce;
            return result;
        }

        public MyResult<object> GetBanner(BannerDto model)
        {
            MyResult result = new MyResult();
            var query = base.Query<Banner>();
            if (model.Types.HasValue)
            {
                query = query.Where(predicate => predicate.Types.Equals(model.Types));
            }
            var banner = query.Where<Banner>(predicate => predicate.IsDel.Equals(0));
            result.Data = banner;
            return result;
        }

        public MyResult<object> GetMessage(MessageDto model)
        {
            MyResult result = new MyResult();
            var sql = $"select miu.*,mt.title from (select mi.id,mi.content,mi.lookCount,mi.Pics,mi.types,u.nickName,u.pic,mi.createTime,mi.isDel from messageInfo mi left join user u on mi.userId=u.id) miu left join messageType mt on miu.types=mt.types where miu.isDel=0";
            if (model.Types > 0)
            {
                sql = sql + $" and miu.types={model.Types}";
            }
            sql = sql + $" order by miu.createTime desc";
            var query = base.dbConnection.Query<MessageDto2>(sql).AsQueryable();
            query = query.Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
            result.PageCount = pageCount;
            result.RecordCount = count;
            result.Data = query;
            return result;
        }

        public MyResult<object> GetMessageType(MessageTypeDto model)
        {
            MyResult result = new MyResult();
            var query = base.Query<MessageType>();
            query = query.Where(predicate => predicate.IsDel.Equals(0)).OrderBy(keySelector => keySelector.Order);
            result.Data = query;
            return result;
        }

        public MyResult<object> GetMyteam(UserDto model)
        {
            MyResult result = new MyResult();
            var user = base.Where<User>(predicate => predicate.RefId.Equals(model.Id)).Select(selector => new { selector.NickName, selector.Pic, selector.CreateTime }).Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
            result.PageCount = pageCount;
            result.RecordCount = count;
            result.Data = user;
            return result;
        }

        public MyResult<object> GetOneAnnounce(int id)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(id.ToString()) || id < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id非法");
            }
            var announce = base.First<Announce>(predicate => predicate.Id == id);
            result.Data = announce;
            return result;
        }

        public MyResult<object> GetOneMessage(MessageDto model)
        {
            MyResult result = new MyResult();
            var sql = $"select miu.*,mt.title from (select mi.id,mi.content,mi.lookCount,mi.Pics,mi.types,u.nickName,u.pic,mi.createTime,mi.isDel from messageInfo mi left join user u on mi.userId=u.id) miu left join messageType mt on miu.types=mt.types where miu.isDel=0 and miu.id={model.Id}";
            var query = base.dbConnection.QueryFirstOrDefault<MessageDto2>(sql);
            var messageInfo = base.dbConnection.QueryFirstOrDefault<MessageInfo>($"select * from messageInfo where id={model.Id}");
            if (messageInfo != null)
            {
                messageInfo.LookCount += 1;
            }
            base.Update(messageInfo, true);
            result.Data = query;
            return result;
        }

        public MyResult<object> GetOneScenic(ScenicDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "scenice id 非法");
            }
            var query = base.First<Scenic>(predicate => predicate.IsDel.Equals(0) && predicate.Id == model.Id);
            if (query != null)
            {
                query.LookCount += 1;
            }
            base.Update(query, true);
            result.Data = query;
            return result;
        }

        public MyResult<object> GetOneShop(ShopDetailDto model)
        {
            MyResult result = new MyResult();
            if (!model.ShopId.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "商户ID无效");
            }
            var shopsDetail = base.Where<ShopsDetail>(predicate => predicate.ShopId.Equals(model.ShopId));
            var shop = base.dbConnection.QueryFirstOrDefault<Shop>($"select * from shop where id={model.ShopId}");
            if (shop != null)
            {
                shop.LookCount += 1;
            }
            base.Update(shop, true);
            result.Data = shopsDetail;
            return result;
        }

        public MyResult<object> GetScenic(ScenicDto model)
        {
            MyResult result = new MyResult();
            var sql = "select id,userId,title,lTitle,lookCount,pic,mark1,mark2,createTime from scenic where isDel=0;";
            var scenic = base.dbConnection.Query<ScenicDto2>(sql).AsQueryable().Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
            result.PageCount = pageCount;
            result.RecordCount = count;
            result.Data = scenic;
            return result;
        }

        public MyResult<object> GetShops(ShopDto model)
        {
            MyResult result = new MyResult();
            var sql = $"select s.id,s.title,s.content,s.logoPic,s.types,s.openTime,s.lookCount,s.longitude,s.latitude,s.closeTime,s.phoneNum,u.pic,u.nickName,st_distance_sphere(point({model.Longitude},{model.Latitude}),point(s.longitude,s.latitude))/1000 distance from shop s left join user u on s.userId=u.id where s.status=1 order by distance;";
            var scenic = base.dbConnection.Query<ShopDto2>(sql).AsQueryable().Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
            result.PageCount = pageCount;
            result.RecordCount = count;
            result.Data = scenic;
            return result;
        }

        public MyResult<object> UpdateAnnounce(AnnounceDto model)
        {
            MyResult result = new MyResult();
            if (string.IsNullOrEmpty(model.Title))
            {
                return result.SetStatus(ErrorCode.InvalidData, "标题非法");
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return result.SetStatus(ErrorCode.InvalidData, "内容非法");
            }
            if (string.IsNullOrEmpty(model.Type.ToString()) || model.Type < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "类型非法");
            }
            if (string.IsNullOrEmpty(model.Id.ToString()) || model.Id < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "id非法");
            }
            var announce = base.First<Announce>(predicate => predicate.Id == model.Id);
            announce.Title = model.Title;
            announce.Content = model.Content;
            announce.Types = model.Type;
            base.Update(announce, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> UpdateMessage(MessageDto model)
        {
            throw new System.NotImplementedException();
        }

        public MyResult<object> UpdateMessageType(MessageTypeDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "Id非法");
            }
            if (string.IsNullOrEmpty(model.Pic))
            {
                return result.SetStatus(ErrorCode.InvalidData, "图片数据非法");
            }
            if (string.IsNullOrEmpty(model.Title))
            {
                return result.SetStatus(ErrorCode.InvalidData, "标题数据非法");
            }
            if (string.IsNullOrEmpty(model.Types.ToString()) || model.Types < 0)
            {
                return result.SetStatus(ErrorCode.InvalidData, "类型数据非法");
            }
            var type = base.First<MessageType>(predicate => predicate.Types.Equals(model.Types));
            if (type != null)
            {
                return result.SetStatus(ErrorCode.InvalidData, "信息类别重复");
            }
            var type2 = base.dbConnection.QueryFirstOrDefault<MessageType>($"select * from messageType where isDel=0 and id={model.Id}");
            // var type2 = base.First<MessageType>(predicate => predicate.Id.Equals(model.Id));
            if (type2 == null)
            {
                return result.SetStatus(ErrorCode.NotFound, "信息不存在");
            }
            type2.Title = model.Title;
            type2.Pic = model.Pic;
            type2.Types = model.Types;
            type2.UpdateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(model.Order.ToString()) && model.Order > 0)
            {
                type2.Order = model.Order;
            }
            base.Update(type2, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> UpdateScenic(ScenicDto model)
        {
            MyResult result = new MyResult();
            if (!model.Id.HasValue)
            {
                return result.SetStatus(ErrorCode.InvalidData, "ID数据非法");
            }
            var scenic = base.First<Scenic>(predicate => predicate.Id == model.Id);
            if (string.IsNullOrEmpty(model.Pic))
            {
                return result.SetStatus(ErrorCode.InvalidData, "图片数据非法");
            }
            if (string.IsNullOrEmpty(model.Title))
            {
                return result.SetStatus(ErrorCode.InvalidData, "标题数据非法");
            }
            if (string.IsNullOrEmpty(model.LTitle))
            {
                return result.SetStatus(ErrorCode.InvalidData, "L标题数据非法");
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                return result.SetStatus(ErrorCode.InvalidData, "内容数据非法");
            }

            scenic.Title = model.Title;
            scenic.Pic = model.Pic;
            scenic.LTitle = model.LTitle;
            scenic.Content = model.Content;
            if (!string.IsNullOrEmpty(model.Order.ToString()) && model.Order > 0)
            {
                scenic.Order = model.Order;
            }
            if (!string.IsNullOrEmpty(model.LookCount.ToString()) && model.LookCount > 0)
            {
                scenic.LookCount = (int)model.LookCount;
            }
            if (!string.IsNullOrEmpty(model.Mark1))
            {
                scenic.Mark1 = model.Mark1;
            }
            if (!string.IsNullOrEmpty(model.Mark2))
            {
                scenic.Mark2 = model.Mark2;
            }
            base.Update(scenic, true);
            result.Data = true;
            return result;
        }

        public MyResult<object> UpdateShop(ShopDto model)
        {
            throw new System.NotImplementedException();
        }

        public MyResult<object> UpdateShopDetail(ShopDetailDto model)
        {
            throw new System.NotImplementedException();
        }

        public bool UserStatus(int userId)
        {
            var user = base.Count<User>(predicate => predicate.Id == userId && predicate.Status == 0);
            if (user > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}