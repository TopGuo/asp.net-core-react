using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.extensions;
using infrastructure.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.mvc
{
    public interface IAuthorizeFilter
    {
        Task OnAuthorizedAsync(AuthorizationFilterContext context, string policy);
    }
    public class MvcAuthorizeFilter : IAsyncAuthorizationFilter
    {
        public bool IsReusable => true;
        public IAuthorizationPolicyProvider PolicyProvider
        {
            get;
            internal set;
        }

        public IEnumerable<IAuthorizeData> AuthorizeData
        {
            get;
            internal set;
        } = new AuthorizeAttribute[0];
        public AuthorizationPolicy Policy
        {
            get;
            internal set;
        }
        public IAuthorizeFilter AuthorizeFilter
        {
            get;
            internal set;
        }
        public MvcAuthorizeFilter()
            : this(new AuthorizeAttribute[1]
            {
                new AuthorizeAttribute()
            })
        {
        }
        public MvcAuthorizeFilter(AuthorizationPolicy policy)
        {
            if (policy == null)
            {
                throw new ArgumentNullException("policy");
            }
            Policy = policy;
        }
        public MvcAuthorizeFilter(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authorizeData)
                    : this(authorizeData)
        {
            if (policyProvider == null)
            {
                throw new ArgumentNullException("policyProvider");
            }
            PolicyProvider = policyProvider;
        }
        public MvcAuthorizeFilter(IEnumerable<IAuthorizeData> authorizeData)
        {
            if (authorizeData == null)
            {
                throw new ArgumentNullException("authorizeData");
            }
            AuthorizeData = authorizeData;
        }
        public MvcAuthorizeFilter(string policy)
            : this(new AuthorizeAttribute[1]
            {
                new AuthorizeAttribute(policy)
            })
        {
        }

        public virtual async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            AuthorizationPolicy effectivePolicy = Policy;
            if (effectivePolicy == null)
            {
                if (PolicyProvider == null)
                {
                    throw new InvalidOperationException("An AuthorizationPolicy cannot be created without a valid instance of IAuthorizationPolicyProvider.");
                }
                effectivePolicy = await AuthorizationPolicy.CombineAsync(PolicyProvider, AuthorizeData);
            }
            if (effectivePolicy != null)
            {
                MvcPrincipal newPrincipal = null;
                string currentScheme = effectivePolicy.AuthenticationSchemes.FirstOrDefault();
                if (!string.IsNullOrEmpty(currentScheme))
                {
                    if (!(context.HttpContext.User.Identity is MvcIdentity) || !context.HttpContext.User.Identity.IsAuthenticated)
                    {
                        string cookie = CookieUtil.GetCookie(currentScheme, true);
                        if (!string.IsNullOrEmpty(cookie))
                        {
                            try
                            {
                                string value = DataProtectionUtil.UnProtect(cookie);
                                MvcIdentity identity = JsonExtension.GetModel<MvcIdentity>(value, "");
                                if (identity != null)
                                {
                                    newPrincipal = identity.GetPrincipal();
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        newPrincipal = (context.HttpContext.User as MvcPrincipal);
                    }
                }
                if (newPrincipal == null)
                {
                    context.HttpContext.User = MvcIdentity.Instance.GetPrincipal();
                }
                else
                {
                    context.HttpContext.User = newPrincipal;
                }
                if (!context.Filters.Any((IFilterMetadata item) => item is IAllowAnonymousFilter))
                {
                    if (context.HttpContext.User.Identity.IsAuthenticated)
                    {
                        if (AuthorizeFilter == null)
                        {
                            AuthorizeFilter = ServiceProviderServiceExtensions.GetService<IAuthorizeFilter>(context.HttpContext.RequestServices);
                        }
                        if (AuthorizeFilter != null)
                        {
                            await AuthorizeFilter.OnAuthorizedAsync(context, currentScheme);
                        }
                    }
                    else
                    {
                        context.Result = new ChallengeResult(effectivePolicy.AuthenticationSchemes.ToArray());
                    }
                }
            }
        }
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            if (Policy != null || (object)PolicyProvider != null)
            {
                return this;
            }
            IAuthorizationPolicyProvider requiredService = ServiceProviderServiceExtensions.GetRequiredService<IAuthorizationPolicyProvider>(serviceProvider);
            if (PolicyProvider == null)
            {
                PolicyProvider = ServiceProviderServiceExtensions.GetRequiredService<IAuthorizationPolicyProvider>(serviceProvider);
            }
            if (Policy == null)
            {
                Policy = AuthorizationPolicy.CombineAsync(requiredService, AuthorizeData).GetAwaiter().GetResult();
            }
            if (AuthorizeFilter == null)
            {
                AuthorizeFilter = ServiceProviderServiceExtensions.GetService<IAuthorizeFilter>(serviceProvider);
            }
            return this;
        }
    }
}