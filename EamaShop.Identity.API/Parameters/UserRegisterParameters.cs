using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Parameters
{
    public class UserRegisterParameters
    {
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string AccountName { get; set; }
        [StringLength(18, MinimumLength = 6)]
        [Required]
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
    }
}
