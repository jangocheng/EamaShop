using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Provides extension methods for EamaShop Web Restful Api.
    /// </summary>
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseAll(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            app.UseResponseCaching();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            return app;
        }
        /// <summary>
        /// Add swagger doc Middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultSwaggerAndDev(this IApplicationBuilder app, string serviceName)
        {
            app.UseSwagger()
                .UseSwaggerUI(x => Configure(x, serviceName));

            app.UseDeveloperExceptionPage();

            return app;
        }
        public static void Configure(SwaggerUIOptions options, string serviceName)
        {
            options.SwaggerEndpoint($"/swagger/v1/swagger.json", AppDomain.CurrentDomain.FriendlyName);
            options.ConfigureOAuth2("swaggerui", "", "", AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
