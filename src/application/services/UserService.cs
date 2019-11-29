using domain.configs;
using domain.entitys;
using domain.models;
using domain.models.dto;
using domain.repository;
using infrastructure.extensions;
using Microsoft.Extensions.Options;
using System.Linq;

namespace application.services
{
    public class UserService : bases.BaseService1, IUserService
    {
        public UserService(IOptions<ConnectionStringList> connectionStrings) : base(connectionStrings)
        {
        }

        public MyResult<object> WxLogin(WxLoginDto model)
        {
            throw new System.NotImplementedException();
        }

        public MyResult<object> GetUserById(int id)
        {
            MyResult result = new MyResult();
            User user = this.First<User>(t => t.Id == id);
            result.Data = user;
            return result;
        }
        public MyResult<object> GetUserList(UserModel model)
        {
            MyResult result = new MyResult();
            var query = base.Query<User>();

            if (!string.IsNullOrEmpty(model.UserName))
            {
                query = query.Where(t => t.NickName.Contains(model.UserName));
            }
            if (!string.IsNullOrEmpty(model.PhoneNum))
            {
                query = query.Where(t => t.PhoneNum.Contains(model.PhoneNum));
            }
            query = query.Pages(model.PageIndex, model.PageSize, out int count, out int pageCount);
            result.Data = query;
            result.RecordCount = count;
            result.PageCount = pageCount;
            return result;
        }
        
        public MyResult<object> UpdateStatusUser(User model)
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