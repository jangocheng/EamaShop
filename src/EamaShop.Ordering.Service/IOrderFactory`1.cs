using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Ordering.Service
{
    public interface IOrderFactory<TOrder>
        where TOrder : IOrderAggregateRoot
    {
        Task<TOrder> Place(OrderPlaceContext context);

        Task<TOrder> GetAsync(long id);

        Task<TOrder> GetAsync(string orderNumber);
    }
}
