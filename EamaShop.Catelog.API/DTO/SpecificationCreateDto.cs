using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.DTO
{
    public class SpecificationCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Range(0.01, 999999999)]
        public decimal Price { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<string> PictureUris { get; set; }
        [Range(1, 999999)]
        public int StockCount { get; set; } = 9999;
    }
}
