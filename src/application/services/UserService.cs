using domain.configs;
using domain.models.dto;
using domain.repository;
using Microsoft.Extensions.Options;

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
    }
}