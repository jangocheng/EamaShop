using EamaShop.Identity.Common;
using EamaShop.Infrastructures;
using EamaShop.Infrastructures.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace EamaShop.Identity.DataModel
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class ApplicationUser
    {
        public long Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string AccountName { get; set; }
        [Required]
        [ClaimIgnoreField]
        public string Password { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NickName { get; set; }
        [Required]
        [Url]
        public string HeadImageUri { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public Gender Sexy { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public DateTime? LastLoginTime { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
