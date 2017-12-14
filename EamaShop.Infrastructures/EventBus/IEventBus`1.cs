using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// Represent a event bus.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventBus
    {
        /// <summary>
        /// Asynchronously publish a event to this event bus.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventMessage"></param>
        /// <returns></returns>
        Task PublishAsync<TEvent>(TEvent eventMessage) where TEvent : IEventMetadata;
        /// <summary>
        /// Synchronously publish a event to this event bus. 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventMessage"></param>
        void Publish<TEvent>(TEvent eventMessage) where TEvent : IEventMetadata;
        /// <summary>
        /// Remove <typeparamref name="TEventHandler"/>.If no handler exists,no thing todo.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : IEventMetadata
            where TEventHandler : IEventBusEventHandler;
        /// <summary>
        /// 订阅指定的事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        void Subscribe<TEvent, TEventHandler>()
            where TEvent : IEventMetadata
            where TEventHandler : IEventBusEventHandler;
    }
}
