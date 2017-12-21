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
    /// 用户描述信息
    /// </summary>
    public class UserGetDTO
    {
        private readonly ApplicationUser _user;
        public UserGetDTO(ApplicationUser user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }
        public long UserId => _user.Id;
        [StringLength(20, MinimumLength = 8)]
        public string AccountName => _user.AccountName;
        [Phone]
        public string Phone => _user.Phone;
        [EmailAddress]
        public string Email => _user.Email;
        public string NickName => _user.NickName;
        [Url]
        public string HeadImageUri => _user.HeadImageUri;
        public Gender Sexy => _user.Sexy;

        public DateTime LastLoginTime => _user.LastLoginTime ?? DateTime.Now;

        public UserRole Role => _user.Role;
        /// <summary>
        /// Gets or sets which country user comes from.
        /// </summary>
        public string Country => _user.Country;
        /// <summary>
        /// Gets or sets which city user comes from.
        /// </summary>
        public string City => _user.City;
        /// <summary>
        /// Gets or sets which province user comes from.
        /// </summary>
        public string Province => _user.Province;
    }
}
