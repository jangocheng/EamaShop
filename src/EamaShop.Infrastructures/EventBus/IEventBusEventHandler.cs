using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Infrastructures
{
    public interface IEventBusEventHandler<TEvent>
    {
        Task HandleAsync(TEvent @event);
    }
}
