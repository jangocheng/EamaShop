﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// Represent a persistent connction for RabbitMQ.
    /// </summary>
    public interface IRabbitMQPersistentConnection: IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
