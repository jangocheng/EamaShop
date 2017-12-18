using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public interface IOrderNumberGenerator
    {
        string Gen();
        string Gen(int storeId);
    }
}
