using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.Respository
{
    public class Product
    {
        public long Id { get; set; }

        public long StoreId { get; set; }
        public string Name { get; set; }
        
        public string PictureUris { get; set; }

        public IEnumerable<Specification> Specifications { get; set; }

        public string Properties { get; set; }

        public string Description { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifiedTime { get; set; }
    }
}
