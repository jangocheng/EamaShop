using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// A global handing filter that runs after an action has thrown <see cref="Exception"/>
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IHostingEnvironment _env;
        /// <summary>
        /// Initialize a new <see cref="GlobalExceptionFilter"/> instance.
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public GlobalExceptionFilter(IHostingEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        ///<inheritdoc />
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
            {
                return;
            }

            _logger.LogError(new EventId(context.Exception.HResult, context.Exception.Source),
                context.Exception,
                context.Exception.Message);

            object resultObject;

            if (_env.IsDevelopment())
            {
                resultObject = new
                {
                    Message = "An error occur.Try it again.",
                    DeveloperMessage = context.Exception.ToString()
                };
            }
            else
            {
                resultObject = new
                {
                    Message = "An error occur.Try it again."
                };
            }
            context.Result = new ObjectResult(resultObject) { StatusCode = 500 };

            context.ExceptionHandled = true;
        }
    }
}
