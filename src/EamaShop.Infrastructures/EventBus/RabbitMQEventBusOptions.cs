using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    public class RabbitMQEventBusOptions
    {
        /// <summary>
        /// Gets or sets the number of times of trying re-publish message on failure. Default is 5.
        /// </summary>
        /// <exception cref="InvalidOperationException">publish retry count must greater than zero</exception>
        public int PublishRetryCount
        {
            get => _publishRetryCount;
            set => _publishRetryCount = value < 1
                ? throw new InvalidOperationException("the value must be greater than zero")
                : value;
        }
        private int _publishRetryCount = 5;
        /// <summary>
        /// Gets or sets the number of times of client tring re-connect RabbitMQ Server on failure. Default is 5
        /// </summary>
        /// <exception cref="InvalidOperationException">connect retry count must greater than zero</exception>
        public int ConnectRetryCount
        {
            get => _connectRetryCount;
            set => _connectRetryCount = value < 1
                ? throw new InvalidOperationException("the value must be greater than zero")
                : value;
        }
        private int _connectRetryCount = 5;
        /// <summary>
        /// Gets or sets the RabbitMQ Server Host. Default is 'localhost'.
        /// </summary>
        public string Host { get; set; } = "localhost";
        /// <summary>
        /// Gets or sets the username used by connect RabbitMQ Server. Default is 'guest'.
        /// </summary>
        public string UserName { get; set; } = "guest";
        /// <summary>
        /// Gets or sets the password used by connect RabbitMQ Server. Default is 'guest'.
        /// </summary>
        public string Password { get; set; } = "guest";
    }
}
