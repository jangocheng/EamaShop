using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.Infrastructures
{
    public class Store
    {
        public long Id { get; set; }

        public long UId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LogoUri { get; set; }
    }
}
