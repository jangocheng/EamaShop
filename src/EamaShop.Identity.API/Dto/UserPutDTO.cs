using EamaShop.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    /// <summary>
    /// 用户修改个人信息的表单DTO对象
    /// </summary>
    public class UserPutDTO
    {
        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Url]
        [Required]
        public string HeadImageUri { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Sexy { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Province { get; set; }
    }
}
