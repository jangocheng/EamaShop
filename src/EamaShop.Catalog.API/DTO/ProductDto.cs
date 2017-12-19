using EamaShop.Catalog.API.Respository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.DTO
{
    public class ProductDto
    {
        private readonly Product _product;
        public ProductDto(Product product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));

            PictureUris = JsonConvert.DeserializeObject<IEnumerable<string>>(_product.PictureUris);

            Properties = JsonConvert.DeserializeObject<IEnumerable<string>>(_product.Properties);

            Specifications = _product.Specifications.Select(Selector).ToArray();
        }

        private static SpecificationDto Selector(Specification specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return new SpecificationDto(specification);
        }
        public long Id => _product.Id;

        public long StoreId => _product.StoreId;
        public string Name => _product.Name;

        public IEnumerable<string> PictureUris { get;  }

        public IEnumerable<SpecificationDto> Specifications { get;  }

        public IEnumerable<string> Properties { get;  }

        public string Description => _product.Description;

        public long CategoryId => _product.CategoryId;
    }
}
