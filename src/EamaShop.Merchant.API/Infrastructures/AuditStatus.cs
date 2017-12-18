using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.Infrastructures
{
    public enum AuditStatus
    {
        /// <summary>
        /// 同意
        /// </summary>
        Allowed,
        /// <summary>
        /// 不同意
        /// </summary>
        NotAllowed,
        /// <summary>
        /// 待审核
        /// </summary>
        Waiting
    }
}
