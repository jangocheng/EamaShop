using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EamaShop.Infrastructures.Enums
{
    /// <summary>
    /// 订单状态枚举
    /// <para></para>
    /// 流程如下
    /// 1.用户已下单 2.用户未支付 3.用户已支付 4.商家已发货 5.物流配送中 6.用户确认收货 7.用户已评价 8.订单已完成
    /// Other.用户已取消  申请取消中
    /// </summary>
    [Flags]
    public enum OrderStatus
    {
        /// <summary>
        /// 订单已创建
        /// </summary>
        [DisplayName("订单已创建")]
        Placed = 1,
        /// <summary>
        /// 等待支付
        /// </summary>
        [DisplayName("等待支付")]
        WaitForPay = 2,
        /// <summary>
        /// 已支付
        /// </summary>
        [DisplayName("已支付")]
        Paid = 2 * 2,
        /// <summary>
        /// 已发货
        /// </summary>
        [DisplayName("已发货")]
        Ship = 2 * 2 * 2,
        /// <summary>
        /// 订单已完成
        /// </summary>
        [DisplayName("订单已完成")]
        Delivery = 2 * 2 * 2 * 2,
        /// <summary>
        /// 确认收货
        /// </summary>
        [DisplayName("已确认收货")]
        Accept = 2 * 2 * 2 * 2 * 2,
        /// <summary>
        /// 用户已评价
        /// </summary>
        [DisplayName("已评价")]
        Evaluated = 2 * 2 * 2 * 2 * 2 * 2,
        /// <summary>
        /// 订单已完成
        /// </summary>
        [DisplayName("订单已完成")]
        Completed = 2 * 2 * 2 * 2 * 2 * 2 * 2,
        /// <summary>
        /// 订单已取消
        /// </summary>
        [DisplayName("订单已取消")]
        Cancel = 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2,
        /// <summary>
        /// 申请取消中
        /// </summary>
        [DisplayName("申请取消中")]
        ApplyCancel = 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2 * 2
    }
}
