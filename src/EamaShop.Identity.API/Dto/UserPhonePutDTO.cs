using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    /// <summary>
    /// 绑定手机号参数上下文
    /// </summary>
    public class UserPhonePutDTO
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [Phone]
        public string Phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string VerifyCode { get; set; }
    }
}
