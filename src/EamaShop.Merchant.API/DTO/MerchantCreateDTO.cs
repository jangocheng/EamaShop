using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.DTO
{
    public class MerchantCreateDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(80, MinimumLength = 5)]
        public string Name { get; set; }
        [Url]
        [Required]
        public string LogoUri { get; set; }
        [StringLength(260, MinimumLength = 2)]
        [Required]
        public string Description { get; set; }
    }
}
