using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace EamaShop.Infrastructures
{
    class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var hasAuthorize = (context.ApiDescription.ControllerAttributes().OfType<AuthorizeAttribute>().Any() ||
                context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>().Any()) && !context.ApiDescription.ActionAttributes().OfType<AllowAnonymousAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });

                operation.Responses.Add("403", new Response { Description = "Forbidden" });
                var parameter = new NonBodyParameter()
                {
                    Description = "身份认证的授权token",
                    Required = true,
                    Type = "string",
                    Name = "Authorization",
                    In = "header",
                    Format = "Bearer {0}"
                };
                
                operation.Parameters = operation.Parameters ?? new List<IParameter>();
                operation.Parameters.Add(parameter);
                //operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                //operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                //{
                //    { "oauth2", new [] { context.GetType().Assembly.FullName } }
                //});
            }
        }
    }
}
