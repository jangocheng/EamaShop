using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.Enums
{
    /// <summary>
    /// 审核状态 审核的进度
    /// </summary>
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
