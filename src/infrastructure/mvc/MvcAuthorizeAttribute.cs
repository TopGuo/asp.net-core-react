using Microsoft.AspNetCore.Authorization;

namespace infrastructure.mvc
{
    public class MvcAuthorizeAttribute : AuthorizeAttribute
    {
        public string LoginPath
        {
            get;
            set;
        }

        public string AccessDeniedPath
        {
            get;
            set;
        }

        public MvcAuthorizeAttribute()
            : base()
        {
        }
    }
}