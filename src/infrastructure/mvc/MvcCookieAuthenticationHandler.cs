using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace infrastructure.mvc
{
    public class MvcCookieAuthenticationHandler : CookieAuthenticationHandler
    {
        private IOptions<MvcApplicationOptions> _mvcApplicationOptions;
        public MvcCookieAuthenticationHandler(IOptions<MvcApplicationOptions> mvcApplicationOptions, IOptionsMonitor<CookieAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _mvcApplicationOptions = mvcApplicationOptions;
        }
        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            MvcAuthorizeOptions schemes = _mvcApplicationOptions.Value.AuthorizationSchemes.SingleOrDefault(x => x.AuthenticationScheme == base.Scheme.Name);
            string returnUrl = properties.RedirectUri;
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = base.OriginalPathBase + base.Request.Path + base.Request.QueryString;
            }
            PathString? nullable = schemes?.AccessDeniedPath;
            string deniedPath = nullable.HasValue ? PathString.FromUriComponent(nullable.GetValueOrDefault()) : null;
            if (string.IsNullOrEmpty(deniedPath))
            {
                deniedPath = PathString.FromUriComponent(base.Options.AccessDeniedPath);
            }
            string returnParameter = schemes?.ReturnUrlParameter;
            if (string.IsNullOrEmpty(returnParameter))
            {
                returnParameter = base.Options.ReturnUrlParameter;
            }
            string accessDeniedUri = string.Concat(deniedPath, QueryString.Create(returnParameter, returnUrl));
            RedirectContext<CookieAuthenticationOptions> redirectContext = new RedirectContext<CookieAuthenticationOptions>(base.Context, base.Scheme, base.Options, properties, BuildRedirectUri(accessDeniedUri));
            await this.Events.RedirectToAccessDenied(redirectContext);
        }
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            MvcAuthorizeOptions schemes = _mvcApplicationOptions.Value.AuthorizationSchemes.SingleOrDefault((MvcAuthorizeOptions x) => x.AuthenticationScheme == base.Scheme.Name);
            string redirectUri = properties.RedirectUri;
            if (string.IsNullOrEmpty(redirectUri))
            {
                redirectUri = base.OriginalPathBase + base.Request.Path + base.Request.QueryString;
            }
            PathString? nullable = schemes?.LoginPath;
            string path = nullable.HasValue ? PathString.FromUriComponent(nullable.GetValueOrDefault()) : null;
            if (string.IsNullOrEmpty(path))
            {
                path = PathString.FromUriComponent(base.Options.LoginPath);
            }
            string returnParameter = schemes?.ReturnUrlParameter;
            if (string.IsNullOrEmpty(returnParameter))
            {
                returnParameter = base.Options.ReturnUrlParameter;
            }
            string loginUri = string.Concat(path, QueryString.Create(returnParameter, redirectUri));
            RedirectContext<CookieAuthenticationOptions> redirectContext = new RedirectContext<CookieAuthenticationOptions>(base.Context, base.Scheme, base.Options, properties, BuildRedirectUri(loginUri));
            await this.Events.RedirectToLogin(redirectContext);
        }
        protected new string BuildRedirectUri(string targetPath)
        {
            if (targetPath.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || targetPath.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return targetPath;
            }
            return string.Concat(base.Request.Scheme, "://", base.Request.Host) + base.OriginalPathBase + targetPath;
        }
    }
}