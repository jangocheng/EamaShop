using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.DTO
{
    public class PropertyCreateDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public IEnumerable<string> Values { get; set; }
    }
}
