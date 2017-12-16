using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Identity.API.Dto
{
    public class MerchantCreateDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
