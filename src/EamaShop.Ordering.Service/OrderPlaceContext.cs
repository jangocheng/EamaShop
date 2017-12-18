using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EamaShop.Ordering.Service
{
    /// <summary>
    /// 下单的上下文信息
    /// </summary>
    public class OrderPlaceContext
    {
        public OrderPlaceContext(
            long buyerUId,
            string receivingAddress,
            IEnumerable<OrderPlaceProduct> products,
            string remarks = "")
        {
            BuyerUId = buyerUId;
            Remarks = remarks ?? throw new ArgumentNullException(nameof(remarks));
            ReceivingAddress = receivingAddress ?? throw new ArgumentNullException(nameof(receivingAddress));
            Products = products?.ToArray() ?? throw new ArgumentNullException(nameof(products));
        }

        /// <summary>
        /// 下单的购买者UId
        /// </summary>
        public long BuyerUId { get; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; }
        /// <summary>
        /// 收货地址信息
        /// </summary>
        public string ReceivingAddress { get; }
        /// <summary>
        /// 购买的商品规格列表
        /// </summary>
        public IEnumerable<OrderPlaceProduct> Products { get; }
    }
}
