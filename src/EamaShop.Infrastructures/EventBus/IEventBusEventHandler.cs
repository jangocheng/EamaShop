using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    public interface IEventBusEventHandler
    {
        void SetServiceContainer(IServiceProvider serviceProvider);
        Task HandleAsync<TEvent>(TEvent @event);
    }
}
