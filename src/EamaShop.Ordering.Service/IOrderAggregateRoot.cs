using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public interface IOrderAggregateRoot
    {
        string OrderNumber { get; }
    }
}
