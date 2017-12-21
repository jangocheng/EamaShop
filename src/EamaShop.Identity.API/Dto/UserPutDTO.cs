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
        /// 修改后的昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }
        /// <summary>
        /// 修改后的用户头像Uri地址
        /// </summary>
        [Url]
        [Required]
        public string HeadImageUri { get; set; }
        /// <summary>
        /// 修改后的性别
        /// </summary>
        public Gender Sexy { get; set; }
        /// <summary>
        /// 修改后的用户所在国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 修改后的用户所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 修改后的用户所在省份
        /// </summary>
        public string Province { get; set; }
    }
}
