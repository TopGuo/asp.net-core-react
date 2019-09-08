using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.Extensions
{
    public static class ServiceExtension
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static IServiceProvider _serviceProvider;

        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.BuildServiceProvider().RegisterServiceProvider();
            return services;
        }

        public static IServiceProvider RegisterServiceProvider(this IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new NotImplementedException("IServiceProvider serviceProvider canot be null");
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            return serviceProvider;
        }
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    return _httpContextAccessor.HttpContext.RequestServices;
                }
                return _serviceProvider;
            }
        }
        public static HttpContext HttpContext => _httpContextAccessor?.HttpContext;

        public static object New(Type type)
        {
            return ActivatorUtilities.CreateInstance(ServiceProvider, type, Array.Empty<object>());
        }
        public static T New<T>()
        {
            return ActivatorUtilities.CreateInstance<T>(ServiceProvider, Array.Empty<object>());
        }
        public static T Get<T>()
        {
            T val;
            try
            {
                val = ActivatorUtilities.GetServiceOrCreateInstance<T>(ServiceProvider);
            }
            catch (System.Exception ex)
            {
                try
                {
                    val = ServiceProvider.GetService<T>();
                }
                catch (System.Exception ex2)
                {
                    try
                    {
                        val = default(T);
                    }
                    catch (System.Exception ex3)
                    {

                        throw new Exception($"ex={ex.Message};ex2={ex2.Message};ex3={ex3.Message}");
                    }
                }
            }
            if (val != null)
            {
                return val;
            }
            return default(T);
        }

        public static object Get(Type type)
        {
            try
            {
                return ActivatorUtilities.GetServiceOrCreateInstance(ServiceProvider, type);
            }
            catch
            {
                object service = ServiceProvider.GetService(type);
                if (service == null)
                {
                    return null;
                }
                return service;
            }
        }
    }
}