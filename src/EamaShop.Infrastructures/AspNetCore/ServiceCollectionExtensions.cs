using EamaShop.Infrastructures;
using EamaShop.Infrastructures.HttpStandard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Common extensions for <see cref="IServiceCollection"/>
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
                .AddCors(Configure)
                .AddAuthorization();

            // for authentication 
            services.AddAuthentication(Configure)
                .AddJwtBearer(Configure);

            // for event bus
            services.TryAddSingleton<IEventBus>(sp =>
            {
                var connection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<RabbitMQEventBus>>();
                var manager = sp.GetRequiredService<IEventHandlerManager>();
                var config = sp.GetRequiredService<IConfiguration>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(config["EventBusPublishRetryCount"]))
                {
                    retryCount = int.Parse(config["EventBusPublishRetryCount"]);
                }

                return new RabbitMQEventBus(connection, logger, manager, sp, retryCount);
            });
            services.TryAddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();
                var config = sp.GetRequiredService<IConfiguration>();

                var host = config["RabbitMQHost"];
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = host ?? "localhost"
                };

                var retryCount = 5;
                if (!string.IsNullOrEmpty(config["RabbitMQConnectionRetry"]))
                {
                    retryCount = int.Parse(config["RabbitMQConnectionRetry"]);
                }
                return new RabbitMQPersistentConnection(connectionFactory, logger, retryCount);
            });
            services.TryAddSingleton<IEventHandlerManager, EventBusHandlerManager>();

            services.AddResponseCaching();

            services.TryAddSingleton(typeof(IHttpClient<>), typeof(StandardHttpClient<>));
            services.TryAddSingleton(MicroserviceDescriptor.Catalog);
            services.TryAddSingleton(MicroserviceDescriptor.Merchant);

            return services;
        }

        #region Mvc
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
        #endregion

        #region Cors
        private static void Configure(CorsOptions options)
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());
        }
        #endregion

        #region Swagger
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

            // OAuth 还没实现呢，先将就用JwtBearer手动认证吧~~~
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
        #endregion

        #region JwtBearer
        private static void Configure(JwtBearerOptions options)
        {
            var parameters = new TokenValidationParameters()
            {
                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role,
                ValidIssuer = ClaimsIdentity.DefaultIssuer,
                ValidAudience = EamaDefaults.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerSignKey)),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EamaDefaults.JwtBearerTokenKey))
            };

            options.TokenValidationParameters = parameters;
        }
        #endregion

        #region Authentication
        private static void Configure(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        #endregion

    }
}
