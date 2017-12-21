using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EamaShop.Infrastructures
{
    class BadRequestCheckOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //var flag = operation.Responses.Any(x => x.Key == "400");
            //if (!flag)
            //{
            //    var error = operation.Parameters;
            //    var instances = context.ApiDescription.ParameterDescriptions.Select(x => Activator.CreateInstance(x.Type)).ToArray();
            //    object examples;
            //    if (instances.Length == 1)
            //    {
            //        examples = instances[0];
            //    }
            //    else
            //    {
            //        examples = instances;
            //    }
            //    var response = new Response()
            //    {
            //        Description = "Bad Request",
            //        Examples = examples,
            //        Schema = new Schema()
            //        {
            //            Type = $"object"
            //        }
            //    };
            //    operation.Responses.Add("400", response);
            //}
        }
    }
}
