using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures
{
    public interface IEventHandlerManager : IDisposable
    {
        void AddHandler<TEvent, THandler>()
            where THandler : IEventBusEventHandler
            where TEvent : IEventMetadata;

        void RemoveHandler<TEvent, THandler>()
            where THandler : IEventBusEventHandler
            where TEvent : IEventMetadata;

        IEnumerable<Type> GetHandlers(string eventName);
    }
}
