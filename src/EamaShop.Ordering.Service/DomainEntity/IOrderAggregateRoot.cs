using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public interface IOrderAggregateRoot
    {
        long Id { get; }
        string OrderNumber { get; }
    }
}
