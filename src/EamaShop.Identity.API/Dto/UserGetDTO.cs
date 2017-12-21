using EamaShop.Identity.DataModel;
using EamaShop.Infrastructures.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    /// <summary>
    /// 用户描述信息 如果未特殊说明 字段默认为非空
    /// </summary>
    public class UserGetDTO
    {
        private readonly ApplicationUser _user;
        public UserGetDTO(ApplicationUser user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }
        /// <summary>
        /// 用户的Id
        /// </summary>
        public long UserId => _user.Id;
        /// <summary>
        /// 用户的账户名
        /// </summary>
        [StringLength(20, MinimumLength = 8)]
        public string AccountName => _user.AccountName;
        /// <summary>
        /// 用户绑定的手机号码 如果未绑定 则为空
        /// </summary>
        [Phone]
        public string Phone => _user.Phone;
        /// <summary>
        /// 用户绑定的邮箱地址，如果未绑定 则为空
        /// </summary>
        [EmailAddress]
        public string Email => _user.Email;
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string NickName => _user.NickName;
        /// <summary>
        /// 用户的头像uri地址
        /// </summary>
        [Url]
        public string HeadImageUri => _user.HeadImageUri;
        /// <summary>
        /// 用户的性别 枚举 Male：0男  Female：1女 默认为1
        /// </summary>
        public Gender Sexy => _user.Sexy;
        /// <summary>
        /// 用户的最后一次登陆时间
        /// </summary>
        public DateTime LastLoginTime => _user.LastLoginTime ?? DateTime.Now;
        /// <summary>
        /// 用户的角色 枚举值 1 用户 2 商家 4 管理员 8 vip
        /// </summary>
        public UserRole Role => _user.Role;
        /// <summary>
        /// 用户的所在国家
        /// </summary>
        public string Country => _user.Country;
        /// <summary>
        /// 用户的所在城市
        /// </summary>
        public string City => _user.City;
        /// <summary>
        /// 用户的所在省份
        /// </summary>
        public string Province => _user.Province;
    }
}
