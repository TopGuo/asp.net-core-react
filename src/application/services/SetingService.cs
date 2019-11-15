using System.Linq;
using domain.configs;
using domain.entitys;
using domain.enums;
using domain.models.dto;
using domain.repository;
using infrastructure.extensions;
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

        public MyResult<object> GetAnnounces(AnnounceDto model)
        {
            MyResult result = new MyResult();
            var query = base.Query<Announce>();
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(t => t.Title == model.Title);
            }
            
            query = query.Where(t=>t.IsDel.Equals(0)).Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
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
    }
}