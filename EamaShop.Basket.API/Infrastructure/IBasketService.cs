using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Basket.API.Infrastructure
{
    public interface IBasketService
    {
        ShopBasket GetBasketByUId(long uid);
    }
}
