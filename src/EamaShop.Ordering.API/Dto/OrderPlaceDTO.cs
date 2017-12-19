using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Ordering.API.Dto
{
    public class OrderPlaceDTO
    {
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        [Required]
        public string Receiver { get; set; }

        [Required]
        [Phone]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 购买的商品规格列表
        /// </summary>
        [Required]
        [MinLength(1)]
        public IEnumerable<OrderPlaceProductDTO> Products { get; set; }
    }
}
