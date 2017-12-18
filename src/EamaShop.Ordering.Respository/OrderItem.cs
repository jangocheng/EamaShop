using System;
using System.Collections.Generic;
using System.Text;

namespace EamaShop.Ordering.Respository
{
    public sealed class OrderItem
    {

        public long Id { get; set; }
        /// <summary>
        /// 冗余字段
        /// </summary>
        public string OrderNumber { get; set; }

        public long OrderId { get; set; }

        public string CatalogName { get; set; }

        public string CatalogId { get; set; }

        public string ProductName { get; set; }

        public string ProductPicture { get; set; }

        public string SpecificationName { get; set; }

        public long SpecificationId { get; set; }

        public decimal Price { get; set; }

        public string Property { get; set; }
    }
}
