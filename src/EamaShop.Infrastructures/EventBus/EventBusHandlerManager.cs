using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EamaShop.Infrastructures
{
    public class EventBusHandlerManager : IEventHandlerManager
    {
        private readonly IDictionary<Type, ISet<Type>> _handlerMaps;
        public EventBusHandlerManager()
        {
            _handlerMaps = new Dictionary<Type, ISet<Type>>();
        }

        public void AddHandler<TEvent, THandler>()
            where TEvent : IEventMetadata
            where THandler : IEventBusEventHandler<TEvent>
        {
            var key = typeof(TEvent).Name;
            if (!_handlerMaps.TryGetValue(typeof(TEvent), out var handers))
            {
                handers = new HashSet<Type>();
                handers.Add(typeof(THandler));
                _handlerMaps.Add(typeof(TEvent), handers);
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

        public IEnumerable<Type> GetHandlers(string eventName, out Type eventType)
        {
            eventType = _handlerMaps.FirstOrDefault(x => x.Key.Name == eventName).Key;
            
            var handers = _handlerMaps
                .Where(x => x.Key.Name == eventName)
                .SelectMany(x => x.Value)
                .ToArray();

            if (handers != null)
            {
                return handers.ToList();
            }
            return Enumerable.Empty<Type>();
        }

        public void RemoveHandler<TEvent, THandler>()
            where TEvent : IEventMetadata
            where THandler : IEventBusEventHandler<TEvent>
        {
            _handlerMaps.Remove(typeof(TEvent));
        }
    }
}
