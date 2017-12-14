using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EamaShop.Infrastructures
{
    public class EventBusHandlerManager : IEventHandlerManager
    {
        private readonly IDictionary<string, ISet<Type>> _handlerMaps;
        public EventBusHandlerManager()
        {
            _handlerMaps = new Dictionary<string, ISet<Type>>();
        }

        public void AddHandler<TEvent, THandler>()
            where TEvent : IEventMetadata
            where THandler : IEventBusEventHandler
        {
            var key = typeof(TEvent).Name;
            if (!_handlerMaps.TryGetValue(key, out var handers))
            {
                handers = new HashSet<Type>();
                handers.Add(typeof(THandler));
                _handlerMaps.Add(key, handers);
            }
            else
            {
                if (!handers.Add(typeof(THandler)))
                {
                    throw new InvalidOperationException($"event {key} with handler {typeof(THandler).Name} has already registered");
                }
            }
        }

        public void Dispose()
        {
            _handlerMaps.Clear();
        }

        public IEnumerable<Type> GetHandlers(string eventName)
        {
            if (_handlerMaps.TryGetValue(eventName, out var types))
            {
                return types;
            }
            return Enumerable.Empty<Type>();
        }

        public void RemoveHandler<TEvent, THandler>()
            where TEvent : IEventMetadata
            where THandler : IEventBusEventHandler
        {
            _handlerMaps.Remove(typeof(TEvent).Name);
        }
    }
}
