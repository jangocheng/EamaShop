using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Basket.API.Infrastructure
{
    public class Commodity : IEquatable<Commodity>
    {
        public long StoreId { get; set; }
        public string StoreName { get; set; }

        public long ProductId { get; set; }

        public string Price { get; set; }

        public string Picture { get; set; }

        public string SpecificationId { get; set; }

        public string SpecificationName { get; set; }

        public long Quantity { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Commodity);
        }

        public bool Equals(Commodity other)
        {
            return other != null &&
                   StoreId == other.StoreId &&
                   ProductId == other.ProductId &&
                   SpecificationId == other.SpecificationId;
        }

        public override int GetHashCode()
        {
            var hashCode = 903248155;
            hashCode = hashCode * -1521134295 + StoreId.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SpecificationId);
            return hashCode;
        }

        public static bool operator ==(Commodity commodity1, Commodity commodity2)
        {
            return EqualityComparer<Commodity>.Default.Equals(commodity1, commodity2);
        }

        public static bool operator !=(Commodity commodity1, Commodity commodity2)
        {
            return !(commodity1 == commodity2);
        }
    }
}
