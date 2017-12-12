using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Parameters
{
    public class AuthorizeEmailParameters : AuthorizePasswordParameters
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
