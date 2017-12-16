using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.DTO
{
    public class CategoryCreateDto
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public long? ParentId { get; set; }

        [Range(1, long.MaxValue)]
        public long StoreId { get; set; }
    }
}
