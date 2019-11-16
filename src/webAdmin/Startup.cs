using System.Collections.Generic;
using application.middleware;
using application.services;
using domain.configs;
using domain.repository;
using infrastructure.extensions;
using infrastructure.mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace webAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISetingService, SetingService>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvcCustomer(Constants.WEBSITE_AUTHENTICATION_SCHEME, mvcOptions =>
             {
                 mvcOptions.AuthorizationSchemes = new List<MvcAuthorizeOptions>
                 {
                    new MvcAuthorizeOptions(){
                         ReturnUrlParameter="from",
                         AccessDeniedPath="/Denied",
                         AuthenticationScheme=Constants.WEBSITE_AUTHENTICATION_SCHEME,
                         LoginPath="/",
                         LogoutPath="/Logout"
                    }
                 };
             });
            services.RegisterService();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseErrorHandlerMiddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
