using EamaShop.Catalog.API.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.Respository
{
    public class Specification
    {
        public long Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ModifiedTime { get; set; }

        public long ProductId { get; set; }

        public decimal Price { get; set; }

        public int StockCount { get; set; }

        public string Name { get; set; }

        public SpecificationState State { get; set; }
    }
}
