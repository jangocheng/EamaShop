using EamaShop.Infrastructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace EventBusTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new ServiceCollection();
            service.AddLogging();
            var configer = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            service.TryAddSingleton<IConfiguration>(configer);
            // for event bus
            service.TryAddSingleton<IEventBus>(sp =>
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
            service.TryAddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();
                var config = sp.GetRequiredService<IConfiguration>();

                var host = config["RabbitMQHost"];
                var userName = config["RabbitMQUserName"] ?? "guest";
                var password = config["RabbitMQPassword"] ?? "guest";
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = host ?? "localhost",
                    UserName = userName,
                    Password = password
                };

                var retryCount = 5;
                if (!string.IsNullOrEmpty(config["RabbitMQConnectionRetry"]))
                {
                    retryCount = int.Parse(config["RabbitMQConnectionRetry"]);
                }
                return new RabbitMQPersistentConnection(connectionFactory, logger, retryCount);
            });
            service.TryAddSingleton<IEventHandlerManager, EventBusHandlerManager>();
            var p = service.BuildServiceProvider();
            var pro = p.CreateScope().ServiceProvider;
            DoListen(pro);
            while (true)
            {
                Console.WriteLine("enter message");
                var message = Console.ReadLine();
                var eventBus = pro.GetRequiredService<IEventBus>();
                eventBus.Publish(new DoEvent()
                {
                    Message = message
                });
            }
        }
        private static void DoListen(IServiceProvider service)
        {
            var eventBus = service.GetRequiredService<IEventBus>();
            eventBus.Subscribe<DoEvent, DoEventHandler>();
            eventBus.Subscribe<DoEvent, DoEventHandler2>();
        }
        public class DoEvent : IEventMetadata
        {
            public Guid Id { get; set; }
            public string Message { get; set; }

            public override string ToString()
            {
                return Message;
            }
        }
        public class DoEventHandler : IEventBusEventHandler<DoEvent>
        {
            public Task HandleAsync(DoEvent @event)
            {
                Console.WriteLine(@event);
                return Task.CompletedTask;
            }

        }
        public class DoEventHandler2 : IEventBusEventHandler<DoEvent>
        {
            public Task HandleAsync(DoEvent @event)
            {
                Console.WriteLine(@event);
                return Task.CompletedTask;
            }
        }
    }
}
