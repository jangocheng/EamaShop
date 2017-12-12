using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EamaShop.Infrastructures
{
    public enum Gender
    {
        [DisplayName("正太")]
        Male,
        /// <summary>
        /// 掏出来都比你大，还萌妹 ←.←
        /// </summary>
        [DisplayName("萌妹")]
        Female
    }
}
