using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Merchant.API.Infrastructures
{
    /// <summary>
    /// 店铺信息  该对象属于档案性对象 一般只拥有权限过滤的逻辑和数据合法验证逻辑
    /// </summary>
    public class Store
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 所属的用户Id
        /// </summary>
        public long UId { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        /// <summary>
        /// 店铺描述
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        /// <summary>
        /// 店铺的logo 图片地址
        /// </summary>
        [Url]
        public string LogoUri { get; set; }
    }
}
