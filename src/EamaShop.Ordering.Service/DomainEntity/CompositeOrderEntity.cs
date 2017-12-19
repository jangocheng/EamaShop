using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public class CompositeOrderEntity : OrderEntity
    {
        public CompositeOrderEntity(IEnumerable<OrderEntity> orderEntities)
        {
            OrderEntities = orderEntities?.ToArray() ?? throw new ArgumentNullException(nameof(orderEntities));
            if (OrderEntities.Count() < 1)
            {
                throw new ArgumentException("composite order entity must has at least one element",nameof(orderEntities));
            }
        }
        
        public IEnumerable<OrderEntity> OrderEntities { get; }
        public override long Id => OrderEntities.First().Id;
        public override string OrderNumber => OrderEntities.First().OrderNumber;
        public override ReceivingAddressValueObject ReceivingAddress => OrderEntities.First().ReceivingAddress;
        public override IEnumerable<OrderItemValueObject> Products => OrderEntities.First().Products;
        public override BuyerValueObject Buyer => OrderEntities.First().Buyer;
        public override DateTime PlaceTime => OrderEntities.First().PlaceTime;
        public override DateTime ModifiedTime => OrderEntities.First().ModifiedTime;
        public override string StoreName => OrderEntities.First().StoreName;
        public override long StoreId => OrderEntities.First().StoreId;
        public override Uri StoreLogoUri => OrderEntities.First().StoreLogoUri;
    }
}
