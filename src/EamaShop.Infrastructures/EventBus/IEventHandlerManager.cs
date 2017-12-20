using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    public interface IEventHandlerManager : IDisposable
    {
        void AddHandler<TEvent, THandler>()
            where THandler : IEventBusEventHandler<TEvent>
            where TEvent : IEventMetadata;

        void RemoveHandler<TEvent, THandler>()
            where THandler : IEventBusEventHandler<TEvent>
            where TEvent : IEventMetadata;

        IEnumerable<Type> GetHandlers(string eventName, out Type eventType);
    }
}
