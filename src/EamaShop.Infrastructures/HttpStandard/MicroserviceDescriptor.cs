using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Infrastructures.HttpStandard
{
    /// <summary>
    /// 表示系统中微服务的描述信息
    /// </summary>
    public abstract class MicroserviceDescriptor
    {
        /// <summary>
        /// 微服务的名称，通常该名称应该被设置为英文名称/可作为标识的名称
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// 微服务的友好名称，通常是中文，有利于更好找到微服务
        /// </summary>
        public abstract string NormalizedName { get; }
        /// <summary>
        /// 微服务的Host地址 通常是负载均衡的uri地址 如 https://localhost:5555 
        /// 或者 https://localhost:5555/catalog
        /// </summary>
        public abstract Uri Host { get; }
        /// <summary>
        /// 商品微服务
        /// </summary>
        public static CatalogServiceDescriptor Catalog { get; } = new CatalogServiceDescriptor();

        public static MerchantServiceDescriptor Merchant { get; } = new MerchantServiceDescriptor();
    }
}
