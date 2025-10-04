using AspNetCoreRateLimit;
using Entites.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Presentation.ActionFilters;
using Presentation.Controllers;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;
using System.Runtime.CompilerServices;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        }
        public static void ConfigureRepositoryManager( this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }
        public static void ConfigureLoggerServiceManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService,ServiceLoggerManager>();
        }
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                opt=>
                {
                    opt.ReportApiVersions=true;
                    opt.AssumeDefaultVersionWhenUnspecified=true;
                    opt.DefaultApiVersion= new ApiVersion(1,2);
                    opt.ApiVersionReader= new HeaderApiVersionReader("api-version");
                    opt.Conventions.Controller<ExerciseController>().HasApiVersion(new ApiVersion(1,2));
                    opt.Conventions.Controller<UserController>().HasApiVersion(new ApiVersion(1, 2));
                    opt.Conventions.Controller<UserProfileController>().HasApiVersion(new ApiVersion(1, 2));
                    opt.Conventions.Controller<UserController>().HasDeprecatedApiVersion(new ApiVersion(1, 1));
                }
                );
        }
        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {

            var rateLimitRules = new List<RateLimitRule>()
            { 
                new RateLimitRule()
                {
                    Endpoint="*",
                    Limit=100,
                    Period="1m"
                }
            };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules=rateLimitRules;
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore,MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration,RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy,AsyncKeyLockProcessingStrategy>();
        }
       //public static void ConfigureResponseCaching(this IServiceCollection services)
       // {
       //     services.AddResponseCaching();
       // }
       public static void ConfigureHttpCacheHeaders(this IServiceCollection services )
        {
            services.AddHttpCacheHeaders
                (
                    cacheOpt=>
                    {
                        cacheOpt.MaxAge=30;
                        cacheOpt.CacheLocation=CacheLocation.Public;
                    },
                    validationOps=>
                    {
                        validationOps.MustRevalidate=true; 
                    }
                );
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<UserBase, IdentityRole>
                (
                    opt=>
                    {
                        opt.Password.RequireDigit=true;
                        opt.Password.RequireLowercase=false;
                        opt.Password.RequireUppercase=false;
                        opt.Password.RequireNonAlphanumeric=false;
                        opt.Password.RequiredLength=5;
                        
                        opt.User.RequireUniqueEmail=true;
                    }
                ).AddEntityFrameworkStores<RepositoryContext>().AddDefaultTokenProviders();
        }

    }

}
