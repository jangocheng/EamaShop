using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.DTO
{
    public class ProductCreateDto
    {
        [Range(1, long.MaxValue)]
        public long StoreId { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<string> PictureUris { get; set; }

        public IEnumerable<PropertyCreateDto> Properties { get; set; } = new PropertyCreateDto[0];

        [Required]
        [MinLength(1)]
        public IEnumerable<SpecificationCreateDto> Specifications { get; set; }

        [StringLength(250)]
        public string Description { get; set; } = string.Empty;

        [Range(1, long.MaxValue)]
        public long CategoryId { get; set; }
    }
}
