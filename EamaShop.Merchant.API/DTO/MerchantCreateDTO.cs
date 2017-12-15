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
        public string Name { get; set; }
    }
}
