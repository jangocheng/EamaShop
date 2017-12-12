using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Parameters
{

    public class AuthorizeAccountParameters : AuthorizePasswordParameters
    {
        [Required(AllowEmptyStrings = false)]
        public string Account { get; set; }
    }
}
