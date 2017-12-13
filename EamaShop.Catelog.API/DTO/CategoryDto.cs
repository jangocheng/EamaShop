using EamaShop.Catalog.API.Respository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.DTO
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long ParentId { get; set; }

        public int Level { get; set; }
        public CategoryDto()
        {

        }
        public CategoryDto(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            Debug.Assert(category.Products == null || !category.Products.Any());
            Id = category.Id;
            Name = category.Name;
            ParentId = category.ParentId;
            Level = category.Level;
        }
    }
}
