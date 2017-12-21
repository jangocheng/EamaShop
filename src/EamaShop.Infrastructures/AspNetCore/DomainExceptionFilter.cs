using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    public class DomainExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<DomainExceptionFilter> _logger;

        public DomainExceptionFilter(ILogger<DomainExceptionFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled || !(context.Exception is DomainException))
            {
                return;
            }

            _logger.LogWarning(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            context.Result = new ObjectResult(new Dictionary<string, string>() { { "Message", context.Exception.Message } })
            {
                StatusCode = 400
            };
            context.ExceptionHandled = true;
        }
    }
}
