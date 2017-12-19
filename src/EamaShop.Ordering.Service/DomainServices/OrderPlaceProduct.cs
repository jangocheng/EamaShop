using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Service
{
    /// <summary>
    /// 下单购买的商品信息
    /// </summary>
    public class OrderPlaceProduct
    {
        public OrderPlaceProduct(long productId, long specificationId)
        {
            ProductId = productId;
            SpecificationId = specificationId;
        }

        public long ProductId { get; }
        /// <summary>
        /// 购买的商品的规格Id
        /// </summary>
        public long SpecificationId { get; }
    }
}
