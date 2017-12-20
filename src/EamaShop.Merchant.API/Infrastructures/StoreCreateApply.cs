using EamaShop.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.Infrastructures
{
    /// <summary>
    /// 店铺创建或修改的申请
    /// </summary>
    public class StoreCreateApply
    {
        public long Id { get; set; }
        /// <summary>
        /// 是否是创建店铺的申请
        /// </summary>
        public bool IsCreate { get; set; }
        /// <summary>
        /// 申请人的Id
        /// </summary>
        public long UId { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 店铺描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 店铺的logo地址
        /// </summary>
        public string LogoUri { get; set; }
        /// <summary>
        /// 要修改的门店Id 如果不是创建店铺的申请，则为具体值，否则为null
        /// </summary>
        public long? StoreId { get; set; }
        /// <summary>
        /// 审核状态，同意/不同意
        /// </summary>
        public AuditStatus AuditStatus { get; set; }
        /// <summary>
        /// 不同意的愿意，当auditstatus的值为<see cref="AuditStatus.Allowed"/>时返回null
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 申请的提交时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 申请的审批时间
        /// </summary>
        public DateTime? AuditTime { get; set; }
    }
}
