using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Catalog.API.Respository
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class Category
    {
        /// <summary>
        /// 分类的唯一标识Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 创建的门店Id
        /// </summary>
        [Range(1, long.MaxValue)]
        public long StoreId { get; set; }
        /// <summary>
        /// 分类的名称
        /// </summary>
        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Range(1, long.MaxValue)]
        public long ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
