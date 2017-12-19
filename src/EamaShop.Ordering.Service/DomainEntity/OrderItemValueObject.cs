using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EamaShop.Ordering.Service
{
    [DebuggerDisplay("{CatalogName}:{CatalogId} {ProductName} {SpecificationName}:规格Id-{SpecificationId}")]
    public class OrderItemValueObject : IEquatable<OrderItemValueObject>
    {
        public OrderItemValueObject(string catalogName,
            string catalogId,
            string productName,
            Uri productPicture,
            string specificationName,
            long specificationId,
            decimal price,
            string property)
        {
            CatalogName = catalogName ?? throw new ArgumentNullException(nameof(catalogName));
            CatalogId = catalogId ?? throw new ArgumentNullException(nameof(catalogId));
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            ProductPicture = productPicture ?? throw new ArgumentNullException(nameof(productPicture));
            SpecificationName = specificationName ?? throw new ArgumentNullException(nameof(specificationName));
            SpecificationId = specificationId;
            Price = price;
            Property = property ?? throw new ArgumentNullException(nameof(property));
        }

        public string CatalogName { get; }

        public string CatalogId { get; }

        public string ProductName { get; }

        public Uri ProductPicture { get; }

        public string SpecificationName { get; }

        public long SpecificationId { get; }

        public decimal Price { get; }

        public string Property { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrderItemValueObject);
        }

        public bool Equals(OrderItemValueObject other)
        {
            return other != null &&
                   CatalogName == other.CatalogName &&
                   CatalogId == other.CatalogId &&
                   ProductName == other.ProductName &&
                   EqualityComparer<Uri>.Default.Equals(ProductPicture, other.ProductPicture) &&
                   SpecificationName == other.SpecificationName &&
                   SpecificationId == other.SpecificationId &&
                   Price == other.Price &&
                   Property == other.Property;
        }

        public override int GetHashCode()
        {
            var hashCode = -132621161;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CatalogName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CatalogId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + EqualityComparer<Uri>.Default.GetHashCode(ProductPicture);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SpecificationName);
            hashCode = hashCode * -1521134295 + SpecificationId.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Property);
            return hashCode;
        }

        public static bool operator ==(OrderItemValueObject object1, OrderItemValueObject object2)
        {
            return EqualityComparer<OrderItemValueObject>.Default.Equals(object1, object2);
        }

        public static bool operator !=(OrderItemValueObject object1, OrderItemValueObject object2)
        {
            return !(object1 == object2);
        }
        /// <summary>
        /// 耳机/bose g33型号 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{CatalogName}/{ProductName} {SpecificationName}";
        }
    }
}
