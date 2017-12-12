using EamaShop.Infrastructures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Common extensions for <see cref=""/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDefaultSwagger(this IServiceCollection services, string serviceName, string identityUrl, string scope)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // for api helper doc 
            services.AddSwaggerGen(opt => Configure(opt, serviceName, identityUrl, scope));

            return services;
        }
        /// <summary>
        /// Add All infrastructure services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAll(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // for restful api;
            services.AddMvcCore(Configure)
                .AddApiExplorer()
                .AddDataAnnotations()
                .AddJsonFormatters(Configure)
                .AddFormatterMappings()
                .AddCors(Configure);

            // for authentication 
            services.AddAuthentication(Configure)
                .AddJwtBearer(Configure);

            return services;
        }

        private static void Configure(MvcOptions options)
        {
            var cacheProfile = new CacheProfile()
            {
                Duration = 30,
                Location = ResponseCacheLocation.Any,
                NoStore = false,
                VaryByQueryKeys = new[] { "*" }
            };
            options.CacheProfiles.Add("default", cacheProfile);

            options.Filters.Add<GlobalExceptionFilter>();
            options.Filters.Add<DomainExceptionFilter>();
        }

        private static void Configure(JsonSerializerSettings settings)
        {
            settings.ContractResolver = null;
        }

        private static void Configure(CorsOptions options)
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());
        }

        private static void Configure(SwaggerGenOptions options, string serviceName, string identityUrl, string scope)
        {
            var apiInfo = new Info()
            {
                Title = serviceName,
                Version = "v1",
                Description = $"The HTTP API Microservice of {serviceName}",
                TermsOfService = "Terms Of Service"
            };

            options.SwaggerDoc("v1", apiInfo);

            //options.AddSecurityDefinition("oauth2", new OAuth2Scheme
            //{
            //    Type = "oauth2",
            //    Flow = "authorization_code",
            //    AuthorizationUrl = $"{identityUrl}/authorize/connect/oauth",
            //    TokenUrl = $"{identityUrl}/authorize/connect/oauth",
            //    Scopes = new Dictionary<string, string>()
            //    {
            //        { scope,"当前页面需要的授权范围" }
            //    },
            //    Description = "该页面用于提供给开发者进行调试开放API接口，但是调试之前需要进行OAuth2.0的授权"
            //});

            options.IgnoreObsoleteActions();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml", SearchOption.AllDirectories);
            foreach (var f in files)
            {
                options.IncludeXmlComments(f);
            }
            options.OperationFilter<AuthorizeCheckOperationFilter>();
        }

        private static void Configure(JwtBearerOptions options)
        {
            var parameters = new TokenValidationParameters()
            {
                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role,
                ValidIssuer = "identity",
                ValidAudience = "api",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerSignKey)),
            };
            options.TokenValidationParameters = parameters;
        }

        private static void Configure(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
