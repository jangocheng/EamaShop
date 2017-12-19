using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.Enums
{
    /// <summary>
    /// 商品的状态
    /// </summary>
    public enum ProductStatus
    {
        /// <summary>
        /// 上架售卖中
        /// </summary>
        OnSell,
        /// <summary>
        /// 商品已下架
        /// </summary>
        OffSell,
        /// <summary>
        /// 商品无库存
        /// </summary>
        NoStock
    }
}
