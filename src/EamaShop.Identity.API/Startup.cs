using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace EamaShop.Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAll();
            if (Environment.IsDevelopment())
            {
                services.AddDefaultSwagger("Identity Service", "http://localhost:59322", "auth_base");
            }
            services.AddIdentityServices(Configuration.GetConnectionString("Master"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDefaultSwaggerAndDev("IdentityService");
            }
            app.UseAll();

            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    scope.ServiceProvider
                        .GetRequiredService<DbContext>()
                        .Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
