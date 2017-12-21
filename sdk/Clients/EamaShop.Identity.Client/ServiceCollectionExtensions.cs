using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Rest;
using Microsoft.AspNetCore.Http;
using EamaShop.Identity;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityClient(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddScoped<IIdentityClient>(sp =>
            {
                var contextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var authorization = contextAccessor.HttpContext.Request.Headers["Authorization"];
                var token = String.Empty;
                if ( authorization.Count > 1)
                {
                    token = authorization[1];
                }
                var credentials = new TokenCredentials(token);
                var result= new IdentityClient(credentials);
                result.BaseUri = new Uri("http://localhost:59322");
                return result;
            });

            return services;
        }
    }
}
