using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    public class UserPasswordPutDTO
    {
        /// <summary>
        /// 设置的新密码
        /// </summary>
        [Required]
        [StringLength(18, MinimumLength = 6)]
        public string NewPassword { get; set; }
        /// <summary>
        /// 用户修改密码的凭证 短信为验证码，邮箱也为验证码
        /// </summary>
        [Required]
        public string Token { get; set; }
    }
}
