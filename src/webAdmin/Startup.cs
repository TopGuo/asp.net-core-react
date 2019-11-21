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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWxService, WxService>();

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
            services.Configure<ConnectionStringList>(Configuration.GetSection("ConnectionStrings"));
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
            app.UseErrorHandlerMiddleware();
            app.UseCors(t =>
            {
                t.WithMethods("POST", "PUT", "GET");
                t.WithHeaders("X-Requested-With", "Content-Type", "User-Agent");
                t.WithOrigins("*");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
