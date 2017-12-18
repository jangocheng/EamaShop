using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EamaShop.Ordering.Service
{
    /// <summary>
    /// 订单实体信息
    /// </summary>
    /// <threadsafe cref="false"></threadsafe>
    [DebuggerDisplay("Number:{OrderNumber};")]
    public abstract class OrderEntity : IOrderAggregateRoot, IEquatable<OrderEntity>
    {

        public abstract long Id { get; }

        public abstract string OrderNumber { get; }

        public abstract ReceivingAddressValueObject ReceivingAddress { get; }

        public abstract IEnumerable<OrderItemValueObject> Products { get; }

        public abstract BuyerValueObject Buyer { get; }

        public abstract DateTime PlaceTime { get; }

        public abstract DateTime ModifiedTime { get; }

        public abstract string StoreName { get; }

        public abstract long StoreId { get; }

        public abstract Uri StoreLogoUri { get; }
        #region Impl
        public override bool Equals(object obj)
        {
            return Equals(obj as OrderEntity);
        }

        public bool Equals(OrderEntity other)
        {
            return other != null &&
                   Id == other.Id &&
                   OrderNumber == other.OrderNumber;
        }

        public override int GetHashCode()
        {
            var hashCode = 487671510;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderNumber);
            return hashCode;
        }

        public static bool operator ==(OrderEntity entity1, OrderEntity entity2)
        {
            return EqualityComparer<OrderEntity>.Default.Equals(entity1, entity2);
        }

        public static bool operator !=(OrderEntity entity1, OrderEntity entity2)
        {
            return !(entity1 == entity2);
        }
        public override string ToString()
        {
            return OrderNumber;
        }
        #endregion
    }
}
