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
        private readonly IServiceProvider _container;
        private readonly string _queueName = AppDomain.CurrentDomain.FriendlyName;
        private readonly IList<IEventBusEventHandler> _handlers;
        public RabbitMQEventBus(
            IRabbitMQPersistentConnection persistentConnection,
            ILogger<RabbitMQEventBus> logger,
            IServiceProvider serviceProvider,
            int retryCount = 5)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
            _container = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _handlers = new List<IEventBusEventHandler>();
        }
        public void Publish<TEvent>(TEvent eventMessage)
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
        {
            await Task.Run(() => Publish(eventMessage));
        }


        public void Dispose()
        {
            throw new NotImplementedException();
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

        }

        public void Unsubscribe<TEvent, TEventHandler>() where TEventHandler : IEventBusEventHandler
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEventHandler : IEventBusEventHandler
        {
            if (_handlers.OfType<TEventHandler>().Any())
            {
                throw new ArgumentException(
                    $"Handler Type {typeof(TEventHandler).Name} already registered", nameof(TEventHandler));
            }
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                channel.QueueBind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: typeof(TEvent).Name);
            }
        }
    }
}
