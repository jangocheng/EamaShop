using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    /// <summary>
    /// 用户注册的表单信息
    /// </summary>
    public class UserRegisterDTO
    {
        /// <summary>
        /// 用于注册的用户名 8~18个字符之间
        /// </summary>
        [Required]
        [StringLength(18, MinimumLength = 8)]
        public string AccountName { get; set; }
        /// <summary>
        /// 用于注册的密码 6~18个字符之间
        /// </summary>
        [Required]
        [StringLength(18, MinimumLength = 6)]
        public string Password { get; set; }
        /// <summary>
        /// 头像地址的绝对路径
        /// </summary>
        [Required]
        [Url]
        public string HeadImageUri { get; set; }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }
    }
}
