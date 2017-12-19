using EamaShop.Ordering.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EamaShop.Ordering.Service
{
    public class OrderFactory : IOrderFactory<OrderEntity>
    {
        private readonly IOrderRespository _respository;
        private readonly IServiceProvider _serviceProvider;
        public OrderFactory(IOrderRespository respository, IServiceProvider serviceProvider)
        {
            _respository = respository ?? throw new ArgumentNullException(nameof(respository));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public Task<OrderEntity> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetAsync(string orderNumber)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> Place(OrderPlaceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.BuyerUId <= 0)
            {
                throw new ArgumentException("context.BuyerUId must be greater than zero", nameof(context));
            }

            if (!context.Products.Any())
            {
                throw new ArgumentException("context.Products must be has at least one element", nameof(context));
            }
            var provider = (IOrderProductProvider)_serviceProvider.GetService(typeof(IOrderProductProvider));
            var generator = (IOrderNumberGenerator)_serviceProvider.GetService(typeof(IOrderNumberGenerator));
            provider.TryGetProducts(context.Products.Select(x => x.ProductId).ToArray(), out var products);
            // get result is any 
            if (context.Products.All(x => products.Any(d => d.Id == x.ProductId)))
            {
                throw new DomainException("the product was not contain");
            }
            var orders = new List<Order>();
            var items = new List<OrderItem>(products.Count());
            foreach (var p in products)
            {
                items.Add(new OrderItem()
                {

                });
                var ord = orders.FirstOrDefault(x => x.StoreId == p.StoreId);
                if (ord == null)
                {
                    ord = new Order();
                    orders.Add(ord);
                }
                ord.Products.Add(null);
            }


            generator.Gen();
            throw new NotImplementedException();
        }

        
    }
}
