using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EamaShop.Infrastructures.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [DisplayName("正太")]
        Male,
        /// <summary>
        /// 掏出来都比你大，还萌妹 ←.←
        /// </summary>
        [DisplayName("萌妹")]
        Female
    }
}
