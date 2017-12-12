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
using Microsoft.AspNetCore.Authentication;

namespace EamaShop.Catelog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters()
                .AddCors()
                .AddFormatterMappings();
            // auth by jwtbeaerer; challenge by jwtbearer; forbid by jwtbearer;
            services.AddAuthentication().AddJwtBearer(opt =>
            {
                opt.Audience = "catalog";
            });
            //services.AddAuthentication()
            //    .AddOAuth("", opt =>
            //    {
            //        opt.AuthorizationEndpoint = "http://www.eamashop.com/gateway.do?method=identity.authorize.connect.oauth.code";
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
