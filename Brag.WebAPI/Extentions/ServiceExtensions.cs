

using Brag.Domain.Interfaces.IServices;
using Brag.Domain.Model.Configuration;
using Brag.Repositories.DataContext;
using Brag.Repositories.UnitOfWork;
using Brag.SharedServices.Helpers.HttpClientHelper;
using Brag.SharedServices.Helpers.Mappers;
using Brag.SharedServices.Services.TokenService;
using ManagerPINs.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Brag.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection Add_Services(this IServiceCollection services, IConfiguration config)
        {
            ApplicationServices(services, config);

            RepoServices(services, config);
            AddIdentityService(services, config);
            ConfigSetting(services, config);
            return services;
        }



        static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BragDbContext>(opt =>
            {
                object value = opt.UseSqlServer
                (configuration.GetConnectionString("bragcon")
                 );

            });
            
            services.AddIdentity<ApplicationUsers, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                
               
            })
          
           .AddDefaultTokenProviders()
            //  .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<BragDbContext>();
              
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {

                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("appSettings")["JwtKey"])),
                    ValidateIssuer = false, //set true
                    ValidateAudience = false,
                    ValidateLifetime = true,



                };
            });

            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(5));

            //Add default identity
            ////services.AddIdentityCore(op =>
            ////op.User.Identity.GetUserId())

            ////   .AddRoles<IdentityRole>();
            ///https://learn.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-7.0
        }

        //Repo
        static void RepoServices(this IServiceCollection services, IConfiguration config)
        {
             
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           
        }
        static void ConfigSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("appSettings"));
            
 
        }
        
        static void ApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services
                 .AddScoped<IGenerateTokenService, GenerateTokenService>()
                   .AddScoped<IHttpClientService, HttpClientService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }
    }
}
