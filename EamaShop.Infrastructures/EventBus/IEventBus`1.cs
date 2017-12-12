using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// Event bus.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent eventMessage);

        void Publish<TEvent>(TEvent eventMessage);

        void Unsubscribe<TEvent,TEventHandler>() where TEventHandler:IEventBusEventHandler;

        void Subscribe<TEvent, TEventHandler>() where TEventHandler : IEventBusEventHandler;
    }
}
