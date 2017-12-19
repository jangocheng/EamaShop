using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Ordering.API.Dto
{
    public class OrderPlaceProductDTO
    {
        public long ProductId { get; set; }
        /// <summary>
        /// 购买的商品的规格Id
        /// </summary>
        public long SpecificationId { get; set; }
    }
}
