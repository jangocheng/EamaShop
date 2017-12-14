using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(18, MinimumLength = 8)]
        public string AccountName { get; set; }
        [Required]
        [StringLength(18, MinimumLength = 6)]
        public string Password { get; set; }


    }
}
