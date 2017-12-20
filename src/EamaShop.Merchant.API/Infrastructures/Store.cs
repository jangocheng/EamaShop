using EamaShop.Infrastructures.Enums;
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
        /// 掌柜名称 默认创建的时候是当前用户的昵称
        /// </summary>
        [Required]
        public string Manager { get; set; }
        /// <summary>
        /// 店铺的编号 可空
        /// </summary>
        public string StoreNumber { get; set; }
        /// <summary>
        /// 联系人的移动电话
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 联系人的客服座机号码
        /// </summary>
        public string Telephone { get; set; }
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
        /// <summary>
        /// 获取或设置门店的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 门店的经营状态
        /// </summary>
        public StoreStatus Status { get; set; }

        public string Scopes { get; set; }
    }
}
