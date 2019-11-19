using Microsoft.AspNetCore.Mvc;
using webAdmin.Controllers.Base;
using domain.repository;
using Microsoft.AspNetCore.Authorization;

namespace webAdmin.Controllers
{
    [Route("api/[action]")]
    [Produces("application/json")]
    public class ApiController : ApiBaseController
    {
        // [HttpGet]
        [AllowAnonymous]
        public MyResult<object> WxLogin()
        {
            MyResult result = new MyResult();
            return result;
        }
    }
}