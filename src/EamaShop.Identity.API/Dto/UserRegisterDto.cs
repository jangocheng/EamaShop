using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    /// <summary>
    /// DTO Model for register user.
    /// </summary>
    public class UserRegisterDTO
    {
        /// <summary>
        /// Represent a account name for register a new user.
        /// Account Name is a string which length greater 7 and less than 19.
        /// </summary>
        [Required]
        [StringLength(18, MinimumLength = 8)]
        public string AccountName { get; set; }
        /// <summary>
        /// Represent a password for register a new user.
        /// Password is a string which length greater than 5 and less than 19.
        /// </summary>
        [Required]
        [StringLength(18, MinimumLength = 6)]
        public string Password { get; set; }
        /// <summary>
        /// Absolute uri string for head image.
        /// </summary>
        [Required]
        [Url]
        public string HeadImageUri { get; set; }
        /// <summary>
        /// Gets or sets user's nickname string.
        /// </summary>
        [Required]
        public string NickName { get; set; }
    }
}
