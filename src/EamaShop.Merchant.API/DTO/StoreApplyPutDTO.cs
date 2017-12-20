using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.DTO
{
    public class StoreApplyPutDTO
    {
        /// <summary>
        /// 申请单的id
        /// </summary>
        [Range(1, 9999999, ErrorMessage = "申请单的id必须大于0")]
        public long AuditId { get; set; }
        /// <summary>
        /// 是否同意
        /// </summary>
        public bool Agree { get; set; }
        /// <summary>
        /// 拒绝的原因
        /// </summary>
        public string Reason { get; set; }
    }
}
