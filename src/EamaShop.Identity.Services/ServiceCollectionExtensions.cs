using EamaShop.Identity.Services;
using EamaShop.Identity.Services.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,string connectionString)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.TryAddSingleton<IPasswordEncryptor, PasswordEncryptor>();
            services.TryAddScoped<ILoginService, LoginService>();
            services.TryAddScoped<IRegisterService, RegisterService>();

            services.AddDbContext<UserContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });
            services.AddScoped<IUserRespository>(x => x.GetRequiredService<UserContext>());
            services.AddScoped<DbContext>(x => x.GetRequiredService<UserContext>());
            services.TryAddScoped<IUserInfoService, UserInfoService>();

            services.AddSingleton<IUserTokenFactory, UserTokenFactory>();
            return services;
        }
    }
}
