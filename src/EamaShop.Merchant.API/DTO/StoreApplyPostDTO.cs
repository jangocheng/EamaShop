using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EamaShop.Merchant.API.DTO
{
    public class StoreApplyPostDTO
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Required]
        [StringLength(250)]
        public string StoreName { get; set; }
        /// <summary>
        /// 店铺描述
        /// </summary>
        [Required]
        [StringLength(250)]
        public string StoreDescription { get; set; }
        /// <summary>
        /// 店铺的logo地址
        /// </summary>
        [Required]
        [Url]
        public string StoreLogoUri { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        [Required]
        [MinLength(1)]
        public string[] Scopes { get; set; }
    }
}
