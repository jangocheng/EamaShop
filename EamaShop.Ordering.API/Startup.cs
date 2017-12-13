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
using EamaShop.Ordering.Respository;

namespace EamaShop.Ordering.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IHostingEnvironment environment)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAll();
            services.AddDbContext<OrderContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Master"));
            });
            if (Environment.IsDevelopment())
            {
                services.AddDefaultSwagger("Ordering Service", "", "catalog");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDefaultSwaggerAndDev("Ordering Service");
            }
            app.UseAll();

            // hosting create container.
            // and copy a container.
            // initialize startup by using initialized container.
            // initialize copy container by using startup's ConfigureServices method.
            // build copy container
            // initialize app by using built copy container.
            using (var scop = app.ApplicationServices.CreateScope())
            {
                scop.ServiceProvider
                    .GetRequiredService<OrderContext>()
                    .Database
                    .EnsureCreated();
            }
        }
    }
}
