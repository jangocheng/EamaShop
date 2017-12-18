using System;

namespace EamaShop.Ordering.Service
{
    public interface IOrderFactory<TOrder>
        where TOrder : IOrderAggregateRoot
    {

    }
}
