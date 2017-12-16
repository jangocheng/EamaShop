using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EamaShop.Identity.API.Controllers;
using System.ComponentModel.DataAnnotations;

namespace EamaShop.Identity.API.Dto
{
    /// <summary>
    /// The view model or dto model for <see cref="UserController.Details"/>
    /// </summary>
    public class UserDto
    {
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string AccountName { get; set; }
        [Required]
        public string NickName { get; set; }

        [Required]
        [Url]
        public string HeadImageUri { get; set; }
    }
}
