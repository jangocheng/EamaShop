using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EamaShop.Infrastructure.Tests
{
    public class LogTests
    {
        [Fact]
        public void Log_Test()
        {
            var provider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = provider.GetRequiredService<ILoggerFactory>();
            factory.AddDefaultLog();

            var logger = factory.CreateLogger<LogTests>();

            var excetion = new Exception("Error Message");
            for (int index = 0; index < 2; index++)
            {
                logger.LogTrace("trace");
                logger.LogDebug("debug");
                logger.LogInformation(new EventId(excetion.HResult), excetion, "infosadasdsadasddddddddddddddddddd---{Name}---{Age}");
                logger.LogError("errrrr");
            }

        }
    }
}
