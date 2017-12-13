using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.Respository
{
    public class Category
    {
        public long Id { get; set; }

        public long StoreId { get; set; }

        public string Name { get; set; }

        public long ParentId { get; set; }

        public int Level { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
