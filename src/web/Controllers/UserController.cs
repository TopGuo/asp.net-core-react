using infrastructure.extensions;
using Microsoft.AspNetCore.Mvc;
using web.Controllers.bases;

namespace web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : ApiBaseController
    {
        [HttpPost]
        public MyResult<object> Login([FromBody]UserModel model)
        {
            MyResult<object> result = new MyResult<object>();

            return result;
        }
    }
    public class UserModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}