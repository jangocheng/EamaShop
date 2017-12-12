using EamaShop.Identity.Services.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Identity.Services
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

            services.AddSingleton<IPasswordEncryptor, PasswordEncryptor>();
            services.AddSingleton<ILoginService, LoginService>();

            services.AddDbContext<UserContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });
            services.AddScoped<IUserRespository>(x => x.GetRequiredService<UserContext>());
            services.AddScoped<DbContext>(x => x.GetRequiredService<UserContext>());

            services.AddSingleton<IUserTokenFactory, UserTokenFactory>();

            return services;
        }
    }
}
