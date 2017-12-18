using EamaShop.Ordering.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EamaShop.Ordering.Service
{
    public sealed class InternalOrderEntity : OrderEntity
    {

        private readonly Order _metadata;
        /// <summary>
        /// don't make it public.
        /// use <see cref="IOrderFactory{TOrder}"/> to create this instance.
        /// </summary>
        /// <param name="metadata"></param>
        internal InternalOrderEntity(Order metadata)
        {
            _metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
            if (_metadata.Products == null)
            {
                throw new ArgumentException("The property of metadata.Products must be an non-null array with at least one element", nameof(_metadata));
            }
        }

        public override long Id => _metadata.Id;
        public override string OrderNumber => _metadata.OrderNumber;
        private ReceivingAddressValueObject _address;
        public override ReceivingAddressValueObject ReceivingAddress
        {
            get
            {
                if (_address == null && _metadata.ReceivingAddress != null)
                {
                    _address = new ReceivingAddressValueObject(_metadata.ReceivingAddress);
                }
                return _address;
            }
        }
        private OrderItemValueObject[] _products;
        public override IEnumerable<OrderItemValueObject> Products
        {
            get
            {
                if (_products == null)
                {
                    OrderItemValueObject Transform(OrderItem item)
                    {
                        return new OrderItemValueObject(item.CatalogName,
                            item.CatalogId,
                            item.ProductName,
                            new Uri(item.ProductPicture),
                            item.SpecificationName,
                            item.SpecificationId,
                            item.Price,
                            item.Property);
                    }
                    _products = _metadata.Products.Select(Transform).ToArray();
                }
                return _products;
            }
        }

        public override BuyerValueObject Buyer { get; }
        public override DateTime PlaceTime => _metadata.CreateTime;
        public override DateTime ModifiedTime => _metadata.ModifiedTime;

        public override string StoreName => _metadata.StoreName;
        public override long StoreId => _metadata.StoreId;
        private Uri _logo;
        public override Uri StoreLogoUri
        {
            get
            {
                if (_logo == null)
                {
                    _logo = new Uri(_metadata.StoreLogoUri);
                }
                return _logo;
            }
        }
    }
}
