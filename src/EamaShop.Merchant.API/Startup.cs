using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EamaShop.Merchant.API.Infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EamaShop.Merchant.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAll(Configuration);
            if (Environment.IsDevelopment())
            {
                services.AddDefaultSwagger("Merchant Service", "http://localhost:59322", "auth_base");
            }
            services.AddDbContext<MerchantContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Master"));
            });
            services.AddIdentityClient();
            // services.AddIdentityServices(Configuration.GetConnectionString("Master"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDefaultSwaggerAndDev("IdentityService");
            }
            app.UseAll();

            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    scope.ServiceProvider
                        .GetRequiredService<MerchantContext>()
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
