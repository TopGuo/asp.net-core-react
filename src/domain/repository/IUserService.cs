using domain.models.dto;

namespace domain.repository
{
    public interface IUserService
    {
        //wxLogin
        MyResult<object> WxLogin(WxLoginDto model);

    }
}