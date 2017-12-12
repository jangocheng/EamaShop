using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Parameters
{
    public class AuthorizePhoneParameters : AuthorizePasswordParameters
    {
        [Required(AllowEmptyStrings = false)]
        [Phone]
        public string Phone { get; set; }
    }
}
