using domain.models.dto;

namespace domain.repository
{
    public interface IWxService
    {
        MyResult<object> Login(WxLoginDto model);
        MyResult<object> Register(WxRegisterDto model);
        MyResult<object> GetUnlimited(int userId = 0);
    }
}