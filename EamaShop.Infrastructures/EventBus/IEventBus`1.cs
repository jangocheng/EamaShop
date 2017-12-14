using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    /// <summary>
    /// 事件总线
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventBus
    {
        /// <summary>
        /// 异步的方式，发布事件到事件总线
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventMessage"></param>
        /// <returns></returns>
        Task PublishAsync<TEvent>(TEvent eventMessage) where TEvent : IEventMetadata;
        /// <summary>
        /// 同步的方式，发布事件到事件总线
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventMessage"></param>
        void Publish<TEvent>(TEvent eventMessage) where TEvent : IEventMetadata;
        /// <summary>
        /// 取消订阅指定事件
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
