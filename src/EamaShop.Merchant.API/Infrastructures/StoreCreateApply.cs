using EamaShop.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.Infrastructures
{
    /// <summary>
    /// 店铺创建或修改的申请单据
    /// </summary>
    public class StoreCreateApply
    {
        public long Id { get; set; }
        /// <summary>
        /// 申请人的Id
        /// </summary>
        public long UId { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 店铺描述
        /// </summary>
        public string StoreDescription { get; set; }
        /// <summary>
        /// 店铺的logo地址
        /// </summary>
        public string StoreLogoUri { get; set; }
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
        /// <summary>
        /// 经营范围
        /// </summary>
        public string Scopes { get; set; }
        /// <summary>
        /// 店铺的默认掌柜
        /// </summary>
        public string Manager { get; set; }
    }
}
