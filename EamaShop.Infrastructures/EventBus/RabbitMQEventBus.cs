using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private readonly JsonSerializerSettings _setting = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto
        };
        private const string BROKER_NAME = "eamashop_event_bus";
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<RabbitMQEventBus> _logger;
        private readonly int _retryCount;
        private readonly string _queueName = AppDomain.CurrentDomain.FriendlyName;
        private IModel _channel;
        private readonly IEventHandlerManager _manager;
        private readonly IServiceProvider service;

        public RabbitMQEventBus(
            IRabbitMQPersistentConnection persistentConnection,
            ILogger<RabbitMQEventBus> logger,
            IEventHandlerManager manager,
            IServiceProvider service,
            int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            _channel = CreateConsumer();
        }
        public void Publish<TEvent>(TEvent eventMessage)
            where TEvent : IEventMetadata
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = eventMessage.GetType().Name;

                channel.ExchangeDeclare(exchange: BROKER_NAME,
                    type: ExchangeType.Direct);

                var message = JsonConvert.SerializeObject(eventMessage, _setting);

                var body = Encoding.UTF8.GetBytes(message);
                for (var time = 0; time < _retryCount; time++)
                {
                    try
                    {
                        channel.BasicPublish(exchange: BROKER_NAME,
                            routingKey: eventName,
                            basicProperties: null,
                            body: body);

                        break;
                    }
                    catch (BrokerUnreachableException ex)
                    {
                        _logger.LogWarning("Rabbit Client publish message fail");
                        _logger.LogWarning(ex.ToString());
                    }
                    catch (SocketException ex)
                    {
                        _logger.LogWarning("Rabbit Client publish message fail");
                        _logger.LogWarning(ex.ToString());
                    }
                    finally
                    {
                        time++;
                    }
                }

            }


        }

        public async Task PublishAsync<TEvent>(TEvent eventMessage)
            where TEvent : IEventMetadata
        {
            await Task.Run(() => Publish(eventMessage));
        }


        public void Dispose()
        {
            _persistentConnection.Dispose();
            _channel.Dispose();
            _manager.Dispose();
        }
        /// <summary>
        /// Create a default consumer that don't accept any event message.
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumer()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME,
                type: ExchangeType.Direct);

            channel.QueueDeclare(queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;

            channel.BasicConsume(_queueName, false, consumer);

            channel.CallbackException += Channel_CallbackException;

            return channel;
        }

        private void Channel_CallbackException(object sender, CallbackExceptionEventArgs e)
        {
            _channel.Dispose();
            _channel = CreateConsumer();
        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body);

            await ProcessEventAsync(eventName, message);
        }

        // get event handler that consume the event
        private async Task ProcessEventAsync(string eventName, string jsonMessage)
        {
            var handlerTypes = _manager.GetHandlers(eventName);
            using (var scope = service.CreateScope())
            {
                foreach (var t in handlerTypes)
                {
                    var instance = ActivatorUtilities.GetServiceOrCreateInstance(scope.ServiceProvider, t);

                    var methodInfo = typeof(IEventBusEventHandler)
                        .GetMethod(nameof(IEventBusEventHandler.HandleAsync));

                    var type = methodInfo.GetGenericArguments()[0];
                    var @event = JsonConvert.DeserializeObject(jsonMessage, type, _setting);

                    var task = (Task)methodInfo.Invoke(instance, new[] { @event });

                    await task;
                }
            }
        }

        public void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : IEventMetadata
            where TEventHandler : IEventBusEventHandler
        {
            _manager.RemoveHandler<TEvent, TEventHandler>();
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IEventMetadata
            where TEventHandler : IEventBusEventHandler
        {
            _manager.AddHandler<TEvent, TEventHandler>();

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }
            // 在指定的连接上开启通道并将当前的队列绑定到指定的路由上
            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: typeof(TEvent).Name);
            }
        }
    }
}
