using EamaShop.Catalog.API.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.DTO
{
    public class SpecificationDto
    {
        private readonly Specification _specification;
        public SpecificationDto(Specification specification)
        {
            _specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }
        public long Id => _specification.Id;

        public decimal Price => _specification.Price;

        public int StockCount => _specification.StockCount;

        public string Name => _specification.Name;
    }
}
